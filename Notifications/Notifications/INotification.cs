using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Notifications.Notifications
{
    internal interface INotification
    {
        Boolean Send(string destinatiion, string title, string message, string jsonConfig, string provaider,out string errorMessage);
        Boolean SendAll(String[] destinatiions, string title, string message, string jsonConfig, string provaider, out string errorMessage);

        Task<Boolean> SendAsync(string destinatiion, string title, string message, string jsonConfig, string provaider);
        Task<Boolean> SendAllAsync(String[] destinatiions, string title, string message, string jsonConfig, string provaider);

    }
}
