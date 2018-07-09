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

        private String MessageBase { get; set; }
        private String TitleMessage { get; set; }

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


                    using (Connection = new SqlConnection(ConectionString))
                    {
                        Connection.Open();
#pragma warning disable S3649 // User-provided values should be sanitized before use in SQL statements
                        using (Command = new SqlCommand(QueryReturn, Connection))
#pragma warning restore S3649 // User-provided values should be sanitized before use in SQL statements
                        {
                            MessageBase = item.Message.Message;
                            TitleMessage = item.Message.TitleMessage;
                            DataAdapter = new SqlDataAdapter(Command);
                            DataTable = new DataTable();
                            DataAdapter.Fill(DataTable);
                            String table = QueryReturn.ToUpper();
                            if(table.Contains("FROM"))
                                table = table.Substring(table.LastIndexOf("FROM"));
                            if (table.Contains("WHERE"))
                                table = table.Substring(0,table.LastIndexOf("WHERE"));

                            table = table.Replace("FROM","").Replace("WHERE","");
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
                                table = table.Substring(0,index+1);
                            }

                            String valueReplace = "";
                            foreach (DataRow dtRow in DataTable.Rows)
                            {
                                IdTables = SchemaTables.GetColumKeys(ConectionString, Connection.Database, table);
                                JsonRow = JsonConvert.SerializeObject(dtRow.Table).Replace("[","").Replace("]","");
                                ObjectJson = (JObject)JsonConvert.DeserializeObject(JsonRow);
                                IdTables.ForEach(i => { ObjectJson.Property(i)?.Remove(); });
                                JsonRow = ObjectJson.ToString();
                               
                                item.Message.ConfigMessage.ForEach(c =>
                                {

                                    valueReplace = "";
                                    if (String.IsNullOrEmpty(c.DefinitiveValue))
                                    {
                                        c.NameColum.ForEach(d =>
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
                                            c.ExpressionRegular.ForEach(f =>
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
                                        valueReplace = c.DefinitiveValue;

                                    if (!String.IsNullOrEmpty(valueReplace))
                                        MessageBase = MessageBase.Replace(c.DinamycParam, valueReplace);

                                });
                                r.TypeNotification.ForEach(t => {
                                    string errorMessage = "Type Notification not support";
                                    bool sendNotification = false;
                                    Regex = new Regex(t.ExpressionRegularMach);
                                    MachtExpression = Regex.Match(JsonRow);




                                    if (Enum.TryParse(t.TypeNotification, out TypeNotification typeNotification) &&  !String.IsNullOrEmpty(MachtExpression.Value))
                                        sendNotification = Notification.Send(MachtExpression.Value, TitleMessage, MessageBase, t.JsonNotificationConfig, typeNotification, t.Provaider, out errorMessage);

                                    if (sendNotification)
                                        errorMessage = "message send";
                                    else if(String.IsNullOrEmpty(MachtExpression.Value))
                                        errorMessage = "no se encontro en la consulta un destinatario";



                                    Connection.Insert(new NotificationLog()
                                    {
                                        Destination = MachtExpression.Groups[0].Value,
                                        MessageErrorProvaider = errorMessage,
                                        CreatedById = "NotifyDll",
                                        MessageSend = MessageBase,
                                        NotificationName = NotficationName,
                                        NameRecipientsType = item.RecipientName,
                                        FillterRecipenttype =QueryReturn,
                                        Provaider = t.Provaider,
                                        TypeNotification = t.TypeNotification,
                                        TitleMessage = TitleMessage,
                                        CreatedAt = DateTime.Now,
                                        IsSend = sendNotification
                                    });
                                    //debe enviarse a grabar a la base de datos esta informacion 

                                });
                            }                      
                            MessageBase = null;
                            TitleMessage = null;

                        }
                    }
                });
            });
        }
    }
}

