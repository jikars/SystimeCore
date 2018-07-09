using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SystimeWCF.NotifyTableWcf.WcfNotification
{
    public class NotifyToWcf 
    {
        private  System.ServiceModel.InstanceContext Contetext { get; set; }
        private  SystimeServiceWcf.SysTimeServiceClient SytstimeServiceClinet { get; set; }

        private CallBackSysTimeService CallBack { get; set; }
        public  NotifyToWcf()
        {
            CallBack = new CallBackSysTimeService();
            Contetext = new System.ServiceModel.InstanceContext(CallBack);
            SytstimeServiceClinet = new SystimeServiceWcf.SysTimeServiceClient(Contetext);
        }


        public   Boolean SendToWcf(SystimeServiceWcf.Notification notification)
        {
            try
            {
                if (notification != null)
                    return SytstimeServiceClinet.NotifyTableChanged(notification);

                return false;
            }
            catch(Exception ex)
            {
                return false;
            }
          
        }
    }
}
