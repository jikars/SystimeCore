using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NotificationFromSytimeSQL.DataAcces
{
    internal class NotificationLogs : IDisposable
    {
        readonly NotificationDbEntities DbNotification = null;
        internal NotificationLogs(String coenctionString)
        {
            DbNotification = new NotificationDbEntities(coenctionString);
        }



        internal NotificationLog SaveNotificationLogs(NotificationLog entity)
        {
            NotificationLog currentNotificationLog = null;
            currentNotificationLog = DbNotification.NotificationLog.FirstOrDefault(n => n.TaleName == entity.TaleName && n.TableKeys == entity.TableKeys
            && n.EventName == entity.EventName && n.DateSend == entity.DateSend && n.TableEvent == entity.TableEvent && n.IdNotificationCondition == entity.IdNotificationCondition
            && n.IdTypeNotification == entity.IdTypeNotification);
            //TODO notificacion debe actualizar el envio para dejarrlo en true 
            if (currentNotificationLog != null)
            {
                currentNotificationLog.CreatedAt = DateTime.Now;
                currentNotificationLog.WasSend = entity.WasSend;
                currentNotificationLog.UpdatedById = "DllSystime";
            }
            else
            {
                entity.CreatedAt = DateTime.Now;
                entity.CreatedById = "DllSystime";
                DbNotification.NotificationLog.Add(entity);
            }

            if (DbNotification.SaveChanges() > 0)
                return entity;
            return null;
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
