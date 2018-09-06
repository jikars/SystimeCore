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

        public bool Send(string destinatiion, string notifySendConfig, string jsonProviderConfig, string provaider, out string errorMessage)
        {
            errorMessage = "ProvaiderNotSupport";
            if (Enum.TryParse(provaider, out Provaiders provaiderout))
            {
                WhatsAppConfig config = JsonConvert.DeserializeObject<WhatsAppConfig>(notifySendConfig);
                return (Boolean)ResolverIntanceProvaider<IWhatsappProvider>(provaiderout)?.SendWhatsApp(destinatiion, config, jsonProviderConfig, out errorMessage);
            }
            return false;
        }

        public bool SendAll(string[] destinatiions, string notifySendConfig, string jsonProviderConfig, string provaider, out string errorMessage)
        {
            errorMessage = "ProvaiderNotSupport";
            if (Enum.TryParse(provaider, out Provaiders provaiderout))
            {
                WhatsAppConfig config = JsonConvert.DeserializeObject<WhatsAppConfig>(notifySendConfig);
                return (Boolean)ResolverIntanceProvaider<IWhatsappProvider>(provaiderout)?.SendAll(destinatiions, config, jsonProviderConfig, out errorMessage);
            }
            return false;
        }

        public async Task<bool> SendAllAsync(string[] destinatiions, string notifySendConfig, string jsonProviderConfig, string provaider)
        {
            if (Enum.TryParse(provaider, out Provaiders provaiderout))
            {
                WhatsAppConfig config = JsonConvert.DeserializeObject<WhatsAppConfig>(notifySendConfig);
                return await ResolverIntanceProvaider<IWhatsappProvider>(provaiderout)?.SendAllAsync(destinatiions, config, jsonProviderConfig);
            }
            return false;
        }

        public async Task<bool> SendAsync(string destinatiion, string notifySendConfig, string jsonProviderConfig, string provaider)
        {
            if (Enum.TryParse(provaider, out Provaiders provaiderout))
            {
                WhatsAppConfig config = JsonConvert.DeserializeObject<WhatsAppConfig>(notifySendConfig);
                return await ResolverIntanceProvaider<IWhatsappProvider>(provaiderout)?.SendAsync(destinatiion, config, jsonProviderConfig);
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
