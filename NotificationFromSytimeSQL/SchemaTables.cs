using NotificationFromSytimeSQL.Contract;
using NotificationFromSytimeSQL.DataAcces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Data;

namespace NotificationFromSytimeSQL
{
    public class SchemaTables : ISchemaTables
    {
        public DataTable ExeucteQuery<T>(string conectionString, string query, T contextCall) where T:INotificiationError
        {
            return new Schema(conectionString, contextCall).ExecuteQuery(query);
        }
          
        public List<string> GetAllColumFromTable(string conectionString, string databaseName, string tableName)
        {
            return new Schema(conectionString).GetColumFromTable(databaseName, tableName);
        }

        public List<string> GetAllTableFromDatabase(string conectionString, string databaseName)
        {
            return new Schema(conectionString).GetAllTables(databaseName);
        }

        public List<string> GetColumKeys(string conectionString, string databaseName, string tableName)
        {
            return new Schema(conectionString).GetKeysTable(databaseName, tableName);
        }
    }
}
