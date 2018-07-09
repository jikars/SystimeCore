using NotificationFromSytimeSQL;
using NotificationFromSytimeSQL.Contract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Unity.Attributes;

namespace SystimeCore.Managers
{
    public class NotificationManager : INotifyManager,INotificiationError
    {
        private ISchemaTables NotificationSql { get; set; }

        [InjectionConstructor]
        public NotificationManager(ISchemaTables notificationSql)
        {
            NotificationSql = notificationSql;
        }

        public void CreateNotificationTrigger(string table, string conectionString,int istest, string database = Config.Config.DATA_BASE_SYSTIME)
        {
            List<String> columKey =  NotificationSql.GetColumKeys(conectionString, database, table);
            String query = String.Format("CREATE TRIGGER [dbo].[NotifyWcf{0}] ON [dbo].{0} AFTER UPDATE, INSERT, DELETE AS" +
                " DECLARE @idTable NVARCHAR(200),@action NVARCHAR(10) IF EXISTS(SELECT * FROM INSERTED) and EXISTS(SELECT* FROM DELETED)" +
                " BEGIN" +
                " SET @action = 'U';" +
                " SELECT @idTable = {1} FROM INSERTED i;" +
                " END" +
                " ELSE IF EXISTS(SELECT * FROM INSERTED) AND NOT EXISTS(SELECT * FROM DELETED)" +
                " BEGIN" +
                " SET @action = 'I';" +
                " SELECT @idTable = {1} FROM INSERTED i;" +
                " END" +
                " ELSE IF EXISTS(SELECT* FROM DELETED) AND NOT EXISTS(SELECT * FROM INSERTED)" +
                " BEGIN" +
                " SET @action = 'D';" +
                " SELECT @idTable = {1} from deleted i;" +
                " END " +
                " EXEC [NotifyDllToWcf] '{0}',@action,@idTable,{2}" , table, columKey[0], istest);

            NotificationSql.ExeucteQuery(conectionString, query, this);
        }

        public void DeleteTrigger(string table, string conectionString)
        {
            String query = String.Format("DROP TRIGGER [dbo].[NotifyWcf{0}]", table);
            NotificationSql.ExeucteQuery(conectionString, query, this);
        }

        public void NotificationError<T>(T exception) where T : Exception
        {
            // Method intentionally left empty.
        }

      
    }
}
