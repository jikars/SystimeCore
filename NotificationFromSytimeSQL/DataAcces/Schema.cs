using NotificationFromSytimeSQL.Contract;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NotificationFromSytimeSQL.DataAcces
{
    internal  class Schema : IDisposable
    {
        readonly NotificationDbEntities DbNotification = null;
        readonly INotificiationError NotificationError = null;
        internal Schema(String coenctionString, INotificiationError notificationErrror = null)
        {
            DbNotification = new NotificationDbEntities(coenctionString);
            NotificationError = notificationErrror;
        }

        internal List<String> GetAllTables(String database)
        {
            List<String> listTablesNames = null;
            using (var connection = new SqlConnection(DbNotification.Database.Connection.ConnectionString))
            {
                SqlCommand command = connection.CreateCommand();
                command.Parameters.Add("@param", SqlDbType.NVarChar);
                command.Parameters["@param"].Value = database;
                command.CommandText = "SELECT TABLE_NAME FROM INFORMATION_SCHEMA.TABLES where Table_Catalog = @param and TABLE_NAME != 'sysdiagrams'";            
                connection.Open();
                using (SqlDataReader reader = command.ExecuteReader())
                {
                   if(reader.HasRows)
                    {
                        listTablesNames = new List<string>();
                        while (reader.Read())
                        {
                            listTablesNames.Add(Convert.ToString(reader["TABLE_NAME"]));
                        }
                    }
                   
                }
            }
            return listTablesNames;
        }


        internal List<String> GetColumFromTable(String database,String table)
        {
            List<String> listColum = null;

            using (var connection = new SqlConnection(DbNotification.Database.Connection.ConnectionString))
            {
                SqlCommand command = connection.CreateCommand();
                command.Parameters.Add("@paramDatabase", SqlDbType.NVarChar);
                command.Parameters["@paramDatabase"].Value = database;
                command.Parameters.Add("@paramsTable", SqlDbType.NVarChar);
                command.Parameters["@paramsTable"].Value = table;
                command.CommandText = "SELECT COLUMN_NAME FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME=@paramsTable AND TABLE_CATALOG = @paramDatabase";
                connection.Open();
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    if (reader.HasRows)
                    {
                        listColum = new List<string>();
                        while (reader.Read())
                        {
                            listColum.Add(Convert.ToString(reader["COLUMN_NAME"]));
                        }
                    }

                }
            }
            return listColum;
        }

        internal List<String> GetKeysTable(String dataTaBaseName, String tableName)
        {
            List<String> listColum = null;
            using (var connection = new SqlConnection(DbNotification.Database.Connection.ConnectionString))
            {
                SqlCommand command = connection.CreateCommand();
                command.Parameters.Add("@tableName", SqlDbType.NVarChar);
                command.Parameters["@tableName"].Value = tableName;
                command.CommandText = "SELECT COLUMN_NAME FROM INFORMATION_SCHEMA.KEY_COLUMN_USAGE WHERE TABLE_NAME LIKE @tableName AND CONSTRAINT_NAME LIKE 'PK%'";
                connection.Open();
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    if (reader.HasRows)
                    {
                        listColum = new List<string>();
                        while (reader.Read())
                        {
                            listColum.Add(Convert.ToString(reader["COLUMN_NAME"]));
                        }
                    }

                }
            }
            return listColum;

        }

        internal DataTable ExecuteQuery(String query)
        {
            DataTable dataTeble = null;
            try
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

                        dataTeble = new DataTable();
                        dataTeble.Load(reader);

                    }
                }
            }
            catch (SqlException ex)
            {
                NotificationError?.NotificationError(ex);
            }
          
            return dataTeble; ;
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
