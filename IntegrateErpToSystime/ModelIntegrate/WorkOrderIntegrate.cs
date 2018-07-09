using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SystimeDataAcces.DataAccess;

namespace IntegrateErpToSystime.ModelIntegrate
{
    public class WorkOrderIntegrate
    {
        public WorkOrders WorkOrderSystime { get; set; }
        public WorkOrder  WorkOrderUbicar { get; set; }
    }
}
