using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SystimeCore.Config;
using static SystimeCore.Config.Enums;

namespace SystimeCore.Managers
{
    public interface IManager
    {
        Object FillterAccionSql(Config.Config config,String jsonKeys, TableAction action);

        Boolean MigrateAll(Config.Config config,DateTime? datetimeMin, int year);

        Boolean RefresgFromTime(Config.Config config, DateTime dateTime);

        Boolean? SuportItemData(Config.Config config, String jsonKeys, TableAction action);
    }

    public interface INotificationErpToSystime
    {
        Boolean ErpSyncToSystime(Config.Config config);
    }


    public interface INotifyManager
    {
        void CreateNotificationTrigger(String table, String conectionString,  int istest,String database = Config.Config.DATA_BASE_SYSTIME);
        void DeleteTrigger(String table, String conectionString);

    }
}
