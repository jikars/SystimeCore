using Dapper.Contrib.Extensions;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using NotificationDependecy.Models;
using NotificationDependecy.Notification.DataAccess;
using NotificationFromSytimeSQL;
using NotificationFromSytimeSQL.Contract;
using Notifications;
using Notifications.Contract;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace NotificationDependecy.Notification
{
    internal class NotifierClient
    {
        private ISchemaTables SchemaTables { get; set; }
        private INotify Notification { get; set; }
        private List<RecipientType> RecipientsType { get; set; }
        private SqlConnection Connection { get; set; }

        private SqlCommand Command { get; set; }

        private DataTable DataTable { get; set; }

        private SqlDataAdapter DataAdapter { get; set; }

        private Match MachtExpression { get; set; }

        private String JsonRow { get; set; }

        private Regex Regex { get; set; }

        private String ConectionString { get; set; }

        private JObject ObjectJson { get; set; }

        private String NotficationName { get; set; }

        private List<DynamicQueryParam> ParamsDynamicTableDataEvent { get; set; }


        private String QueryReturn { get; set; }

        List<String> IdTables { get; set; }
         public NotifierClient(String conectionString, List<DynamicQueryParam> paramsDynamicTableDataEvent, List<RecipientType> recipientsType,String notificationName)
        {
            ParamsDynamicTableDataEvent = paramsDynamicTableDataEvent;
            RecipientsType = recipientsType;
            ConectionString = conectionString;
            NotficationName = notificationName;
            Notification = new ResolverNotify();
            SchemaTables = new SchemaTables();
        }

        internal void SendNotifcations()
        {
            RecipientsType.ForEach(item =>
            {
              
                item.FilttersRecipient.ForEach(r =>
                {
                    QueryReturn = "";
                    ParamsDynamicTableDataEvent.ForEach(d => {
                        QueryReturn = r.Filtter.Replace(d.IdParam, d.ValueParam); 
                    });

                    String messageBase = "";

                    using (Connection = new SqlConnection(ConectionString))
                    {
                        Connection.Open();
#pragma warning disable S3649 // User-provided values should be sanitized before use in SQL statements
                        using (Command = new SqlCommand(QueryReturn, Connection))
#pragma warning restore S3649 // User-provided values should be sanitized before use in SQL statements
                        {
                            item.Messages.ForEach(mss => {

                                messageBase = mss.Message.Message;
                                DataAdapter = new SqlDataAdapter(Command);
                                DataTable = new DataTable();
                                DataAdapter.Fill(DataTable);
                                String table = QueryReturn.ToUpper();
                                if (table.Contains("FROM"))
                                    table = table.Substring(table.LastIndexOf("FROM"));
                                if (table.Contains("WHERE"))
                                    table = table.Substring(0, table.LastIndexOf("WHERE"));

                                table = table.Replace("FROM", "").Replace("WHERE", "");
                                String[] items;
                                if (table.Contains(","))
                                {
                                    items = table.Split(',');
                                    table = items[0].Trim();
                                }
                                else
                                    table = table.Trim();



                                if (table.Contains(" "))
                                {
                                    int index = QueryReturn.IndexOf(" ");
                                    table = table.Substring(0, index + 1);
                                }

                                String valueReplace = "";
                                foreach (DataRow dtRow in DataTable.Rows)
                                {
                                    IdTables = SchemaTables.GetColumKeys(ConectionString, Connection.Database, table);
                                    JsonRow = JsonConvert.SerializeObject(dtRow.Table).Replace("[", "").Replace("]", "");
                                    ObjectJson = (JObject)JsonConvert.DeserializeObject(JsonRow);
                                    IdTables.ForEach(i => { ObjectJson.Property(i)?.Remove(); });
                                    JsonRow = ObjectJson.ToString();

                                    valueReplace = "";
                                    if (String.IsNullOrEmpty(mss.ConfigMessage.DefinitiveValue))
                                    {
                                        mss.ConfigMessage.NameColum.ForEach(d =>
                                        {

                                            foreach (var prop in ObjectJson)
                                            {
                                                if (prop.Key.ToUpper().Contains(d.ToUpper()))
                                                {
                                                    valueReplace = prop.Value?.ToString();
                                                    break;
                                                }
                                            }

                                        });
                                        if (String.IsNullOrEmpty(valueReplace))
                                        {
                                            mss.ConfigMessage.ExpressionRegular.ForEach(f =>
                                            {
                                                Regex = new Regex(f);
                                                MachtExpression = Regex.Match(JsonRow);
                                                if (MachtExpression.Groups.Count > 0)
                                                    valueReplace = MachtExpression.Groups[0].Value;
                                                Regex = null;
                                                MachtExpression = null;
                                            });
                                        }
                                    }
                                    else
                                        valueReplace = mss.ConfigMessage.DefinitiveValue;

                                    if (!String.IsNullOrEmpty(valueReplace))
                                        messageBase = messageBase.Replace(valueReplace = mss.ConfigMessage.DinamycParam, valueReplace);


                                    mss.ConfigMessage.ExpressionRegular.ForEach(t =>
                                    {
                                        Regex = new Regex(t);
                                        MachtExpression = Regex.Match(JsonRow);
                                    });

                                    string destination = "3502365335";

                                    bool sendNotification = false;
                                    string errorMessage = "not send message";
                                    mss.Message.Message = messageBase;

                                    if(MachtExpression.Length == 10  && mss.Type == "WHATSAPP")
                                    {
                                        destination = "57" + destination;
                                    }

                                    if (Enum.TryParse(mss.Type, out TypeNotification typeNotification) && !String.IsNullOrEmpty(destination) && !String.IsNullOrEmpty(mss.JsonConfig) && !String.IsNullOrEmpty(mss.Provider))
                                        sendNotification = Notification.Send(destination, mss.Message, mss.JsonConfig, typeNotification, mss.Provider, out errorMessage);

                                    if (sendNotification)
                                        errorMessage = "message send";
                                    else if (String.IsNullOrEmpty(MachtExpression.Value))
                                        errorMessage = "no se encontro en la consulta un destinatario";

                                    Connection.Insert(new NotificationLog()
                                    {
                                        Destination = MachtExpression.Groups[0].Value,
                                        MessageErrorProvaider = errorMessage,
                                        CreatedById = "NotifyDll",
                                        MessageSend = messageBase,
                                        NotificationName = NotficationName,
                                        NameRecipientsType = item.RecipientName,
                                        FillterRecipenttype = QueryReturn,
                                        Provaider = mss.Provider,
                                        TypeNotification = mss.Type,
                                        TitleMessage = mss.TitleNotify,
                                        CreatedAt = DateTime.Now,
                                        IsSend = sendNotification
                                    });
                                }
                            });
                            messageBase = null;
                        }
                    }
                });
            });
        }
    }
}

