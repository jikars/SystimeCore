using NotificationFromSytimeSQL.Contract;
using NotificationFromSytimeSQL.DataAcces;
using NotificationFromSytimeSQL.Logic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NotificationFromSytimeSQL
{
    public class NotificationSql : INotificationSql
    {
        public List<NotificationMessageBase> GetAllNotificationMessage(string conectionString)
        {
            return new NotificationQuerys(conectionString).GetNotificationMessage();
        }

        public List<string> GetAllTableNotification(string conectionString)
        {
            return new NotificationQuerys(conectionString).GetAllTablesDestination();
        }

        public List<Tuple<int,string, string>> GetProvaiderNotification(string conectionString)
        {
            List<Tuple<int,string, string>> notificationProvaider = new List<Tuple<int,string, string>>();
            List<CatalogNotificationType> listnotificationType =  new NotificationQuerys(conectionString).GetProvaiderNotification();
            if (listnotificationType != null)
                listnotificationType.ForEach(t=> notificationProvaider.Add(new Tuple<int,string, string>(t.IdNotificationType,t.NotificationType, t.RegularExpression)) );
            return notificationProvaider;
        }

        public void Notification(string eventName, string tableName, string acction,string jsonTableData,string conectionString)
        {
            if(Enum.TryParse(acction, out ActionTable tableAction))
                new SendNotification(conectionString).SendNotificationEvent(tableName, eventName, tableAction, jsonTableData);
        }
    }
}
