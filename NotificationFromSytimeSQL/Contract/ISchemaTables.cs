using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NotificationFromSytimeSQL.Contract
{
    public interface ISchemaTables
    {
        List<String> GetAllTableFromDatabase(String conectionString, String databaseName);
       DataTable ExeucteQuery<T>(String conectionString, String query,T contextCall) where T : INotificiationError;

        List<String> GetAllColumFromTable(String conectionString, String databaseName,String tableName);
        List<String> GetColumKeys(String conectionString, String databaseName, String tableName);


    }
}
