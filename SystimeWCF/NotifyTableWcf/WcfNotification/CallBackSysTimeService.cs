using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SystimeWCF.SystimeServiceWcf;

namespace SystimeWCF.NotifyTableWcf.WcfNotification
{
    public class CallBackSysTimeService : ISysTimeServiceCallback
    {
        public void AckBackResult(int data)
        {
            throw new NotImplementedException();
        }

        public void NotifyCustomerCreatedOrUpdated(long idCustomer, Customer customerModel)
        {
            throw new NotImplementedException();
        }

        public void NotifyNewSubOperationByWorkOrder(long idWorkOrder, SubOperationByWorkOrder subOperationByWorkOrder)
        {
            throw new NotImplementedException();
        }

        public void NotifyNewTracking(long idWorkOrder, TrackingByWorkOrder newTracking)
        {
            throw new NotImplementedException();
        }

        public void NotifyNewWorkOrderPosition(long idWorkOrder, Location newPosition)
        {
            throw new NotImplementedException();
        }

        public void NotifyVehicleCreatedOrUpdated(long idVinNumber, Vehicle vehicleModel)
        {
            throw new NotImplementedException();
        }

        public void NotifyWorkOrderCreatedOrUpdated(long idWorkOrder, SubOperationByWorkOrder workOrderModel)
        {
            throw new NotImplementedException();
        }
    }
}