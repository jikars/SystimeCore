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
    internal class OperationByWorkOrderNotify : INotifyTable
    {
        private INotifyWcf<OperationByWorkOrder> NofigycationTable { get; set; }

        public OperationByWorkOrderNotify()
        {
            NofigycationTable = new NotifyWcf<OperationByWorkOrder>();
        }

        public void NotifyWcfStart(string conectionString, string urlWcf, string[] propiertyEvent, ChangeType[] eventSupport, string nameNotication)
        {
            NofigycationTable?.NotifyWcfStart(conectionString, urlWcf, propiertyEvent, eventSupport, nameNotication);
        }

        public void NotifyWcfStop()
        {
            NofigycationTable?.NotifyWcfStop();
        }
    }
}

