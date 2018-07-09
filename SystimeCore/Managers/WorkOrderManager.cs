using IntegrateErpToSystime;
using ServicesAccessUbicar.cs.Contract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SystimeDataAcces.DataAccess;
using Unity.Attributes;
using SystimeCore.Config;
using IntegrateErpToSystime.IntegrateLogic;
using static SystimeCore.Config.Enums;
using IntegrateErpToSystime.ModelIntegrate;

namespace SystimeCore.Managers
{
    public class WorkOrderManager  : IManager
    {

        private IIntegrate<WorkOrderIntegrate> Integrate { get; set; }

        public WorkOrderManager()
        {
            Integrate = new WorkOrder<WorkOrderIntegrate>();
        }

        public object FillterAccionSql(Config.Config config, string jsonKeys, TableAction action)
        {
            if (!config.OperationContinue)
                return null;

            switch (action)
            {
                case TableAction.I:
                case TableAction.U:
                    return Integrate.Save(jsonKeys, config.GetConfigIntegrate(), null);
            }
            return null;
        }


        public bool MigrateAll(Config.Config config, DateTime? datetimeMin, int year)
        {
            if (!config.OperationContinue)
                return false;
            if(year > 0)
                datetimeMin = new DateTime(year, 1, 1);
            else if (!datetimeMin.HasValue)
                datetimeMin = new DateTime(DateTime.Now.Year - 1, 1, 1);
            return Integrate.SaveAll(config.GetConfigIntegrate(), datetimeMin.Value);
        }

        public bool RefresgFromTime(Config.Config config, DateTime dateTime)
        {
            return Integrate.SaveAll(config.GetConfigIntegrate(), dateTime);
        }

        public bool? SuportItemData(Config.Config config, string jsonKeys, TableAction action)
        {
            switch (action)
            {
                case TableAction.I:
                case TableAction.U:
                case TableAction.D:
                    return Integrate.SuportItem(jsonKeys, config.GetConfigIntegrate());
            }
            return null;
        }
    }
}
