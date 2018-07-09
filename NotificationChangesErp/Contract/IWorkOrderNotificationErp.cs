using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NotificationChangesErp.Contract
{
   public interface IWorkOrderNotificationErp
    {
        void UpdateWorkOrderAutorizateAtAndObservationsStart(string conectionString, String conectionStringErp, string erp, String[] supportShop, string language);
        void StopUdateWorkOrder();

    }
}
