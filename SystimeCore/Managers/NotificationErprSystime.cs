using ErpDataAccessFromSystime.Contract.ErpDataAccessFromSystime.Contract;
using IntegrateErpToSystime;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static ErpDataAccessFromSystime.TypesErpConfig;
using static SystimeCore.Config.Enums;

namespace SystimeCore.Managers
{
    public class NotificationErprSystime : INotificationErpToSystime
    {


        private readonly IDataAccesErpContract DataAccesErpContract;


        public NotificationErprSystime()
        {
            DataAccesErpContract = DependencyFactory.Resolve<IDataAccesErpContract>();
        }

        public bool ErpSyncToSystime(Config.Config config)
        {
            ParamsIntegrateErp configIntegrateErp = config.GetConfigIntegrate();
            int countTotal = 0;
            int countTotalOk = 0;
            if (Enum.TryParse(configIntegrateErp.DllType, out ErpsTypes dllType))
            {
                List<NotificationSystimeIntegrate> listIntegrateSystime = DataAccesErpContract.GetNotificationSystime(configIntegrateErp.ConfigParamsToErp, dllType);
                List<NotificationSystimeIntegrate> deleteItems = new List<NotificationSystimeIntegrate>();
                NotificationSystimeIntegrate itemSave = null;
                if (listIntegrateSystime.Count == 0)
                    return true;
                countTotal = listIntegrateSystime.Count;
                listIntegrateSystime?.ForEach(/*listIntegrateSystime, new ParallelOptions { MaxDegreeOfParallelism = 4 },*/ l =>
                {
                    try
                    {
                        if (!deleteItems.Contains(l))
                        {
                            itemSave = listIntegrateSystime.OrderByDescending(i => i.CreatedAt).FirstOrDefault(i => i.TableName == l.TableName && i.JsonKeys == l.JsonKeys && i.Event == l.Event);
                            deleteItems.Add(l);
                            if (SyncNotifyItemErp(config, itemSave))
                            {
                                countTotalOk++;
                            }
                            if (DataAccesErpContract.DeleteNotifcationSystimeGroup(itemSave.JsonKeys, itemSave.TableName, itemSave.Event, itemSave.CreatedAt, configIntegrateErp.ConfigParamsToErp, dllType))
                                deleteItems.AddRange(listIntegrateSystime.Where(i => i.TableName == itemSave.TableName && i.JsonKeys == itemSave.JsonKeys && i.Event == itemSave.Event && i.CreatedAt < itemSave.CreatedAt));
                        }
                    }
#pragma warning disable S2486 // Generic exceptions should not be ignored
                    catch (Exception ex)
#pragma warning disable S108 // Nested blocks of code should not be left empty
                    {

                    }
#pragma warning restore S108 // Nested blocks of code should not be left empty
#pragma warning restore S2486 // Generic exceptions should not be ignored

                });



                if (countTotal == countTotalOk)
                    return true;
            }
            return false;
        }


        public bool SyncNotifyItemErp(Config.Config config, NotificationSystimeIntegrate itemNotify)
        {
            ParamsIntegrateErp configIntegrateErp = config.GetConfigIntegrate();
            Boolean? SupoortItem = null;
            Object objectResponseSync = null;
            if (Enum.TryParse(itemNotify.TableName, out TableName table) && Enum.TryParse(itemNotify.Event, out TableAction actionTable))
            {
                SupoortItem = UtilsSystimeCore.ResolverIntanceFromTable<IManager>(table).SuportItemData(config, itemNotify.JsonKeys, actionTable);
                if (SupoortItem.HasValue && SupoortItem.Value)
                    objectResponseSync = UtilsSystimeCore.ResolverIntanceFromTable<IManager>(table).FillterAccionSql(config, itemNotify.JsonKeys, actionTable);
            }
            else
                SupoortItem = false;
            if ((Enum.TryParse(configIntegrateErp.DllType, out ErpsTypes dllType) && objectResponseSync != null) || (SupoortItem.HasValue && !SupoortItem.Value))
                return DataAccesErpContract.DeleteNotificationSystime(itemNotify.IdNotification, configIntegrateErp.ConfigParamsToErp, dllType);
            return false;
        }
    }
}

