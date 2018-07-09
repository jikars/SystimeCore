using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SystimeDataAcces.NotificationEntity
{
    public interface INotificationTable
    {
        void NotificationTable<T>(T entity, Enums.NotificationSqlTypes notificationEvent) where T : class, new();
    }
}
