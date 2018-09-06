using Notifications.Notifications.Config;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Notifications.Notifications
{
    internal interface INotification
    {
        Boolean Send(string destinatiion,  ConfigNotification messageConfig, string jsonProviderConfig, string provaider,out string errorMessage);
        Boolean SendAll(String[] destinatiions, ConfigNotification messageConfig, string jsonProviderConfig, string provaider, out string errorMessage);
        Task<Boolean> SendAsync(string destinatiion, ConfigNotification messageConfig, string jsonProviderConfig, string provaider);
        Task<Boolean> SendAllAsync(String[] destinatiions, ConfigNotification messageConfig, string jsonProviderConfig, string provaider);
    }
}
