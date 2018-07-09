using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NotificationDependecy.Models;
using System.Threading.Tasks;

namespace NotificationDependecy.Contract
{
    public interface INotificationSuscriber
    {
        void StartSucriber(string conectionString);
        void StopSuscriber(string nameNotification);

        void StopAllNotifcation();
    }
}
