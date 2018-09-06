using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Notifications.Notifications
{
    internal interface INotification
    {
        Boolean Send(string destinatiion,  string notifySendConfig, string jsonProviderConfig, string provaider,out string errorMessage);
        Boolean SendAll(String[] destinatiions, string notifySendConfig, string jsonProviderConfig, string provaider, out string errorMessage);
        Task<Boolean> SendAsync(string destinatiion, string notifySendConfig, string jsonProviderConfig, string provaider);
        Task<Boolean> SendAllAsync(String[] destinatiions, string notifySendConfig, string jsonProviderConfig, string provaider);
    }
}
