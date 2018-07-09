using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SystimeDataAcces.NotificationEntity.EntityNotify
{
    internal interface ITableEntityNotify
    {
        void StartNotification(string conectionString, List<Enums.NotificationSqlTypes> suportEvent, INotificationTable notificationTable);
        void StopNotification();
        String GetValuePropierty(String key);
    }
}
