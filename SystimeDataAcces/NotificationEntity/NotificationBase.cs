using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using TableDependency.Enums;
using TableDependency.EventArgs;
using TableDependency.SqlClient;
using static SystimeDataAcces.NotificationEntity.Enums;

namespace SystimeDataAcces.NotificationEntity
{

    internal class NotificationBase <T> where T: class,new() 
    {
        private INotificationTable NotificationTable { get; set; }
        public T  Entity { get; set; }
        public String JsonEntity { get; set; }
        private JObject JsonObject { get; set; }
        private SqlTableDependency<T> Dependencytable { get; set; }
        private List<ChangeType> ChangesSupport { get; set; }


        internal void StarNotification(String conectionString, List<NotificationSqlTypes> notificationTypes, INotificationTable notificationTable)
        {
            NotificationTable = notificationTable;
            ConvertNotificationType(notificationTypes);
            using (Dependencytable = new SqlTableDependency<T>(conectionString)){
                Dependencytable.Start();
            }
        }

        internal void Changed(object sender, RecordChangedEventArgs<T> e){
            if (ChangesSupport.Contains(e.ChangeType)){
                Entity = e.Entity;
                JsonEntity = JsonConvert.SerializeObject(Entity);
                JsonObject = JObject.Parse(JsonEntity);
                NotificationTable?.NotificationTable(e.Entity, ConvertType(e.ChangeType));
            }
        }


        internal String GetValuePropierty(String key){
            return JsonObject[key].ToString();
        }

        internal void StopNotification()
        {
            Dependencytable?.Stop();
            Dependencytable = null;
            ChangesSupport = null;
            JsonObject = null;
            JsonEntity = null;
            NotificationTable = null;
        }

       void  ConvertNotificationType(List<NotificationSqlTypes> notificationTypes)
       {
           ChangesSupport = new List<ChangeType>();
           notificationTypes?.ForEach(t => ChangesSupport.Add(ConvertType(t))); 
       }

        ChangeType ConvertType(NotificationSqlTypes t)
        {
            switch(t)
            {
                case NotificationSqlTypes.I:
                    return ChangeType.Insert;
                case NotificationSqlTypes.D:
                    return ChangeType.Delete;
                case NotificationSqlTypes.U:
                    return ChangeType.Update;
                default:
                    return ChangeType.None;
            }
        }

        NotificationSqlTypes ConvertType(ChangeType t)
        {
            switch (t)
            {
                case ChangeType.Insert:
                    return NotificationSqlTypes.I;
                case ChangeType.Delete:
                    return NotificationSqlTypes.U;
                case ChangeType.Update:
                    return NotificationSqlTypes.D;
                default:
                    return NotificationSqlTypes.N;
            }
        }
    }
}


