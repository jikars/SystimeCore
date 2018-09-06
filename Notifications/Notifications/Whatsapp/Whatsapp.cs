using Newtonsoft.Json;
using Notifications.Notifications.Config;
using Notifications.Notifications.Whatsapp.Provider;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Notifications.Notifications.Whatsapp
{
    public class Whatsapp : INotification
    {

        private static Dictionary<Provaiders, Type> LisProvaiderWhatsApp = new Dictionary<Provaiders, Type>
            {
                { Provaiders.Waboxapp, typeof(WaboxApp) }
            };


        private enum Provaiders
        {
            Waboxapp
        }

        public bool Send(string destinatiion, ConfigNotification messageConfig, string jsonProviderConfig, string provaider, out string errorMessage)
        {
            errorMessage = "ProvaiderNotSupport";
            if (Enum.TryParse(provaider, out Provaiders provaiderout) && messageConfig is WhatsAppConfig)
            {
                return (Boolean)ResolverIntanceProvaider<IWhatsappProvider>(provaiderout)?.SendWhatsApp(destinatiion, messageConfig as WhatsAppConfig, jsonProviderConfig, out errorMessage);
            }
            return false;
        }

        public bool SendAll(string[] destinatiions, ConfigNotification messageConfig, string jsonProviderConfig, string provaider, out string errorMessage)
        {
            errorMessage = "ProvaiderNotSupport";
            if (Enum.TryParse(provaider, out Provaiders provaiderout) && messageConfig is WhatsAppConfig)
            {
                return (Boolean)ResolverIntanceProvaider<IWhatsappProvider>(provaiderout)?.SendAll(destinatiions, messageConfig as WhatsAppConfig, jsonProviderConfig, out errorMessage);
            }
            return false;
        }

        public async Task<bool> SendAllAsync(string[] destinatiions, ConfigNotification messageConfig, string jsonProviderConfig, string provaider)
        {
            if (Enum.TryParse(provaider, out Provaiders provaiderout) && messageConfig is WhatsAppConfig)
            {
                return await ResolverIntanceProvaider<IWhatsappProvider>(provaiderout)?.SendAllAsync(destinatiions, messageConfig as WhatsAppConfig, jsonProviderConfig);
            }
            return false;
        }

        public async Task<bool> SendAsync(string destinatiion,ConfigNotification messageConfig, string jsonProviderConfig, string provaider)
        {
            if (Enum.TryParse(provaider, out Provaiders provaiderout) && messageConfig is WhatsAppConfig)
            {
                return await ResolverIntanceProvaider<IWhatsappProvider>(provaiderout)?.SendAsync(destinatiion, messageConfig as WhatsAppConfig, jsonProviderConfig);
            }
            return false;
        }


        private static T ResolverIntanceProvaider<T>(Provaiders provaider)
        {
            if (LisProvaiderWhatsApp.ContainsKey(provaider))
            {
                Object Obj = Activator.CreateInstance(LisProvaiderWhatsApp.FirstOrDefault(t => t.Key == provaider).Value);

                if (Obj is T)
                    return (T)Obj;
            }
            return default(T);
        }
    }
}
