using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SystimeDataAcces.DataAccess;

namespace SystimeDataAcces.NotificationEntity.EntityNotify
{
    class WorkOrdersNotify : NotificationBase<WorkOrders>, ITableEntityNotify
    {
        public void StartNotification(string conectionString, List<Enums.NotificationSqlTypes> suportEvent, INotificationTable notificationTable)
        {
            StarNotification(conectionString, suportEvent, notificationTable);
        }

        string ITableEntityNotify.GetValuePropierty(string key)
        {
            return GetValuePropierty(key);
        }

        void ITableEntityNotify.StopNotification()
        {
            StopNotification();
        }
    }
}
