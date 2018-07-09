using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using NotificationDependecy.Contract;
using NotificationDependecy.Models;
using NotificationFromSytimeSQL;
using NotificationFromSytimeSQL.Contract;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Reflection;
using System.Reflection.Emit;
using System.Threading;
using System.Threading.Tasks;
using SystimeDataAcces.NotificationEntity;
using TableDependency.SqlClient;

namespace NotificationDependecy.Notification.Tables
{
    public class Notify : INotify, INotificationTable,IDisposable
    {

        private List<SqlNotificationInfo> EventSupportFromQuery { get; set; }

        private List<SystimeDataAcces.NotificationEntity.Enums.NotificationSqlTypes> EventSupportFromTable { get; set; }


        private List<RecipientType> RecipientType { get; set; }

        private Boolean NoficationFromTable { get; set; }


        private SqlConnection Connection { get; set; }

        private String ConectionString { get; set; }
        private string TableEvnet { get;  set; }
        private SqlDependency Dependecy { get; set; }

        private String Fillter { get; set; }


        private String NotificationName { get; set; }

        private List<DynamicQueryParam> ParamsQuery { get; set; }

        private String FillterValidatePostScript { get; set; }

        private String QueryPostNotification { get; set; }

        String ColumsNotify = "";

        private INotificationTable NotificationTable { get; set; }

        private int DelayNotification = 0;

        private NotifyTable NotifyTable { get; set; }

        public Notify()
        {
            EventSupportFromQuery = new List<SqlNotificationInfo>();
            EventSupportFromTable = new List<SystimeDataAcces.NotificationEntity.Enums.NotificationSqlTypes>();

        }

        public void StopNotification()
        {
            if (Dependecy != null)
                Dependecy.OnChange -= Onchange;
            if (Connection != null)
                Connection.Close();
        }

        public void StartNotifycation(string conectionString, string tableEvent, String notificationName, List<string> columNotify, List<EvnetFillterDataBase> eventDatabase, List<RecipientType> recipientsType, String fillterValidation,String queryPostNotificaiton,Boolean isNnotificationFromTable, int? delayNotification)
        {

            NotificationName = notificationName;
            RecipientType = recipientsType;
            ConectionString = conectionString;
            TableEvnet = tableEvent;
            FillterValidatePostScript = fillterValidation;
            NoficationFromTable = isNnotificationFromTable;
            QueryPostNotification = queryPostNotificaiton;
            if (delayNotification.HasValue)
                DelayNotification = delayNotification.Value;


            if(!NoficationFromTable){
                String where = "";

                int countColum = 0;
                columNotify.ForEach(c => {
                    if (countColum == 0)
                        ColumsNotify += c;
                    else
                        ColumsNotify += "," + c;
                    countColum++;
                });

                int countFillger = 0;
                eventDatabase.ForEach(t => {
                    if (!where.ToUpper().Contains(t.FillterDatabsase.ToUpper()))
                    {
                        if (countFillger == 0)
                            where += "(" + t.FillterDatabsase + ")";
                        else
                            where += " OR " + "(" + t.FillterDatabsase + ")";

                    }
                    countFillger++;

                });


                eventDatabase.ForEach(e => { EventSupportFromQuery.Add(CastEventFromQuery(e.EventDatabse)); });

                Fillter = String.Format("Select {0} from dbo.{1} Where {2}", ColumsNotify, tableEvent, where);

                if (String.IsNullOrEmpty(where))
                    Fillter = Fillter.Replace("Where", "");

                StartServiceQueryDependecy();
            }
            else{
                eventDatabase.ForEach(e => { EventSupportFromTable.Add(CastEventFromTable(e.EventDatabse)); });
                StartServiceTableDependecy(ConectionString, EventSupportFromTable);
            }
   
        }


        void StartServiceQueryDependecy()
        {
            using (Connection = new SqlConnection(ConectionString))
            { 
#pragma warning disable S3649 // User-provided values should be sanitized before use in SQL statements
                using (SqlCommand command = new SqlCommand(Fillter, Connection))
#pragma warning restore S3649 // User-provided values should be sanitized before use in SQL statements
                {
                    command.Notification = null;
                    Dependecy = new SqlDependency(command);
                    Dependecy.OnChange += new OnChangeEventHandler(Onchange);
                    Connection.Open();
                    // Execute the command.  
                    using (SqlDataReader reader = command.ExecuteReader())
#pragma warning disable S108 // Nested blocks of code should not be left empty
                    {
                    }
#pragma warning restore S108 // Nested blocks of code should not be left empty

                }
            }         
        }


        void StartServiceTableDependecy(string conectionString, List<SystimeDataAcces.NotificationEntity.Enums.NotificationSqlTypes> eventSuport)
        {
           NotifyTable = NotifyTable.GetInstance();
           NotifyTable.Start(TableEvnet, conectionString, eventSuport,this);
        }



        private void Onchange(object sender, SqlNotificationEventArgs e)
        {
            if (EventSupportFromQuery.Contains(e.Info) && e.Source == SqlNotificationSource.Data)
            {
                Task.Factory.StartNew(() => {
                    DateTime dateTime = DateTime.Now;
                    List<DynamicQueryParam> lisparams = new SchemaCondition().GetParamsDynamicTableDataEventQuery(JsonConvert.DeserializeObject<SqlNotificationEventArgs>(JsonConvert.SerializeObject(e)), ConectionString, QueryPostNotification, Connection, FillterValidatePostScript, TableEvnet, ColumsNotify, dateTime);
                    if (lisparams != null)
                    {
                        NotifierClient notifierClient = new NotifierClient(ConectionString, lisparams, RecipientType, NotificationName);
                        notifierClient.SendNotifcations();
                    }
                });
            }
                
            SqlDependency sqlDependecy = (SqlDependency)sender;
            sqlDependecy.OnChange -= new OnChangeEventHandler(Onchange);
            StartServiceQueryDependecy();
        }



        public SqlNotificationInfo CastEventFromQuery(Contract.Enums.EventDataBase evnet)
        {
            switch (evnet)
            {
                case Contract.Enums.EventDataBase.Insert:
                    return SqlNotificationInfo.Insert;
                case Contract.Enums.EventDataBase.Update:
                    return SqlNotificationInfo.Update;
                case Contract.Enums.EventDataBase.Delete:
                    return SqlNotificationInfo.Delete;
                case Contract.Enums.EventDataBase.SelectWhere:
                    return SqlNotificationInfo.Query;
                default:
                    return SqlNotificationInfo.Unknown;

            }
        }

        public SystimeDataAcces.NotificationEntity.Enums.NotificationSqlTypes CastEventFromTable(Contract.Enums.EventDataBase evnet)
        {
            switch (evnet)
            {
                case Contract.Enums.EventDataBase.Insert:
                    return SystimeDataAcces.NotificationEntity.Enums.NotificationSqlTypes.I;
                case Contract.Enums.EventDataBase.Update:
                    return SystimeDataAcces.NotificationEntity.Enums.NotificationSqlTypes.U;
                case Contract.Enums.EventDataBase.Delete:
                    return SystimeDataAcces.NotificationEntity.Enums.NotificationSqlTypes.D;
                default:
                    return SystimeDataAcces.NotificationEntity.Enums.NotificationSqlTypes.U;

            }
        }

        void INotificationTable.NotificationTable<T>(T entity, SystimeDataAcces.NotificationEntity.Enums.NotificationSqlTypes notificationEvent)
        {
            //TODO: se debe devolver el valor 
            Task.Factory.StartNew(() => {
                DateTime dateTime = DateTime.Now;
                List<DynamicQueryParam> lisparams = new SchemaCondition().GetParamsDynamicTableDataEvenTable(entity, ConectionString, Connection, FillterValidatePostScript,QueryPostNotification, TableEvnet, ColumsNotify, dateTime);
                if (lisparams != null)
                {
                    NotifierClient notifierClient = new NotifierClient(ConectionString, lisparams, RecipientType, NotificationName);
                    notifierClient.SendNotifcations();
                }
            });
        }



        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposing){
                Dependecy = null;
                NotifyTable?.Stop();
                NotifyTable = null;
            }
        }
    }
}
