using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Notifications.Notifications.SMS
{
    interface IProvaiderSms
    {
        Boolean SendSms(string destinatiion, string message, string title, string jsonConfig, out string errorMessage);
        Boolean SendSmsAll(String[] destinatiion, string message, string title, string jsonConfig, out string errorMessage);

        Task<Boolean> SendSmsAsync(string destinatiion, string message, string title, string jsonConfig);
        Task<Boolean> SendSmsAllAsync(String[] destinatiion, string message, string title, string jsonConfig);

    }
}
