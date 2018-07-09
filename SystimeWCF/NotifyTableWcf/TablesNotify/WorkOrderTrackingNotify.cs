using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SystimeDataAcces.DataAccess;
using SystimeWCF.Contract;
using TableDependency.Enums;

namespace SystimeWCF.NotifyTableWcf.TablesNotify
{
    internal class WorkOrderTrackingNotify : INotifyTable
    {
        private INotifyWcf<WorkOrderTracking> NofigycationTable { get; set; }

        public WorkOrderTrackingNotify()
        {
            NofigycationTable = new NotifyWcf<WorkOrderTracking>();
        }

        public void NotifyWcfStart(string conectionString, string urlWcf, string[] propiertyEvent, ChangeType[] eventSupport,string nameNotication)
        {
            NofigycationTable?.NotifyWcfStart(conectionString, urlWcf, propiertyEvent, eventSupport, nameNotication);
        }

        public void NotifyWcfStop()
        {
            NofigycationTable?.NotifyWcfStop();
        }
    }
}

