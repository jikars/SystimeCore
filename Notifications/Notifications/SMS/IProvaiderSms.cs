using Notifications.Notifications.Config;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Notifications.Notifications.SMS
{
    interface IProvaiderSms
    {
        Boolean SendSms(string destinatiion, SmsConfig config , string jsonProviderConfig, out string errorMessage);
        Boolean SendAll(String[] destinatiions, SmsConfig config, string jsonProviderConfig, out string errorMessage);
        Task<Boolean> SendAsync(string destinatiion, SmsConfig config, string jsonProviderConfig);
        Task<Boolean> SendAllAsync(String[] destinatiions, SmsConfig config, string jsonProviderConfig);
    }
}
