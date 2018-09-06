using Notifications.Notifications.Config;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Notifications.Notifications.Whatsapp
{
    interface IWhatsappProvider
    {
        Boolean SendWhatsApp(string destinatiion, WhatsAppConfig config, string jsonProviderConfig, out string errorMessage);
        Boolean SendAll(String[] destinatiions, WhatsAppConfig config, string jsonProviderConfig, out string errorMessage);
        Task<Boolean> SendAsync(string destinatiion, WhatsAppConfig config, string jsonProviderConfig);
        Task<Boolean> SendAllAsync(String[] destinatiions, WhatsAppConfig config, string jsonProviderConfig);
    }
}
