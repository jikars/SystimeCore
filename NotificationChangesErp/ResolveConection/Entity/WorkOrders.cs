using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NotificationChangesErp.ResolveConection.Entity
{
    public class WorkOrders
    {
        public decimal IdWorkOrder { get; set; }
        public string WorkOrderNumber { get; set; }
        public string IdDealerShop { get; set; }
    
        public DateTime? AuthorizedAt { get; set; }
       
        public String Note { get; set; }
    }
}
