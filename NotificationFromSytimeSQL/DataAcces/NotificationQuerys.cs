using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity.Core.EntityClient;
using System.Data.Entity.Core.Objects;
using System.Data.Entity.Infrastructure;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NotificationFromSytimeSQL.DataAcces
{
    internal class NotificationQuerys : IDisposable
    {
        readonly NotificationDbEntities DbNotification = null;
        internal NotificationQuerys(String coenctionString)
        {
            DbNotification = new NotificationDbEntities(coenctionString);
        }

        internal List<CatalogNotificationType> GetProvaiderNotification()
        {
            return DbNotification.CatalogNotificationType.ToList();
        }

        internal List<NotificationMessageBase> GetNotificationMessage()
        {
            return DbNotification.NotificationMessageBase.ToList();
        }

        internal int ExecuteCommand(String query)
        {
            return DbNotification.Database.ExecuteSqlCommand(query);
        }

        internal List<String> GetAllTablesDestination()
        {
            List<String> litDestinationn = new List<string>();
            List<NotificationTableDestination> table = DbNotification.NotificationTableDestination.ToList();
            table?.ToList().ForEach(t=> litDestinationn.Add(t.TableDestination));
            return litDestinationn;
        }



        internal Boolean ExecuteCondition(String query)
        {

            using (var connection = new SqlConnection(DbNotification.Database.Connection.ConnectionString))
            {
                SqlCommand command = connection.CreateCommand();
#pragma warning disable S3649 // User-provided values should be sanitized before use in SQL statements
                command.CommandText = query;
#pragma warning restore S3649 // User-provided values should be sanitized before use in SQL statements
                connection.Open();
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    if (reader.HasRows)
                        return true;
                }
            }         
            return false;
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
