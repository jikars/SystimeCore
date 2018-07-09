using System;
using System.Collections.Generic;
using SystimeDataAcces.DataAccess;
using SystimeDataAcces.NotificationEntity.EntityNotify;

namespace SystimeDataAcces.NotificationEntity
{
    public  class NotifyTable : IDisposable
    {

        private static readonly object syncLock = new object();


        private static Dictionary<String, Type> ListTableFromName = null;
        private ITableEntityNotify NotificationTable { get; set; }

        public static NotifyTable GetInstance(){

            lock (syncLock)
            {
                if (ListTableFromName == null)
                {
                    ListTableFromName = new Dictionary<string, Type>();
                    ListTableFromName.Add(typeof(WorkOrderTracking).Name.ToUpper().Trim(), typeof(WorkOrderTrackingNotify));
                    ListTableFromName.Add(typeof(WorkOrders).Name.ToUpper().Trim(), typeof(WorkOrdersNotify));
                }
            }
            return new NotifyTable();
        }


        public String GetValuePropiertyName(String name){
            return NotificationTable?.GetValuePropierty(name);
        }

        public void Start(String tableName,String conectionString, List<Enums.NotificationSqlTypes> suportEvent,INotificationTable inotificationTable){
            NotificationTable = ResolveInstanceType(tableName);
            NotificationTable?.StartNotification(conectionString, suportEvent, inotificationTable); 
        }

        public void Stop()
        {
            NotificationTable?.StopNotification();
        }

        private ITableEntityNotify ResolveInstanceType(string  tableName){
            if (ListTableFromName.TryGetValue(tableName,out  Type type))
               return (ITableEntityNotify)Activator.CreateInstance(type);
            return null;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposing){
                Stop();
            }
        }


    }
}
