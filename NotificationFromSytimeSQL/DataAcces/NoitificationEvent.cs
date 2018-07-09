using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NotificationFromSytimeSQL.DataAcces
{
    internal class NoitificationEvent : IDisposable
    {
        readonly NotificationDbEntities DbNotification = null;
        internal NoitificationEvent(String coenctionString)
        {
            DbNotification = new NotificationDbEntities(coenctionString);
        }


        internal NotificationEvents GetNotificationEvent(String tableName,String nameEvent)
        {
           return  DbNotification.NotificationEvents.FirstOrDefault(n => n.TableName == tableName && n.EventName == nameEvent);           
        }


        /// <summary>
        /// Metoto encargado de elimibnar
        /// </summary>
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }


        /// <summary>
        /// Liberar recursos adminsitrados del sistema
        /// </summary>
        /// <param name="disposing"></param>
        protected virtual void Dispose(bool disposing)
        {
            if (disposing && DbNotification != null)
            {
                DbNotification.Database.Connection.Close();
                DbNotification.Dispose();
            }
        }
    }
}
