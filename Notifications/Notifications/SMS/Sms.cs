using Newtonsoft.Json;
using Notifications.Notifications.Config;
using Notifications.Notifications.SMS.Provaider;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Notifications.Notifications.SMS
{
    public class Sms : INotification
    {
        private static Dictionary<Provaiders, Type> LisProvaiderSms = new Dictionary<Provaiders, Type>
            {
                { Provaiders.Contacto, typeof(Contacto) }
            };



        private enum Provaiders
        {
            Contacto
        }

        public bool Send(string destinatiion, string notifySendConfig, string jsonProviderConfig, string provaider, out string errorMessage)
        {
            errorMessage = "ProvaiderNotSupport";
            if (Enum.TryParse(provaider, out Provaiders provaiderout))
            {
                SmsConfig config = JsonConvert.DeserializeObject<SmsConfig>(notifySendConfig);
                return (Boolean)ResolverIntanceProvaider<IProvaiderSms>(provaiderout)?.SendSms(destinatiion, config, jsonProviderConfig, out errorMessage);
            }
            return false;
        }

        public bool SendAll(string[] destinatiions, string notifySendConfig, string jsonProviderConfig, string provaider, out string errorMessage)
        {
            errorMessage = "ProvaiderNotSupport";
            if (Enum.TryParse(provaider, out Provaiders provaiderout))
            {
                SmsConfig config = JsonConvert.DeserializeObject<SmsConfig>(notifySendConfig);
                return (Boolean)ResolverIntanceProvaider<IProvaiderSms>(provaiderout)?.SendAll(destinatiions, config, jsonProviderConfig, out errorMessage);
            }
            return false;
        }

        public async Task<bool> SendAsync(string destinatiion, string notifySendConfig, string jsonProviderConfig, string provaider)
        {
            if (Enum.TryParse(provaider, out Provaiders provaiderout))
            {
                SmsConfig config = JsonConvert.DeserializeObject<SmsConfig>(notifySendConfig);
                return await ResolverIntanceProvaider<IProvaiderSms>(provaiderout)?.SendAsync(destinatiion, config, jsonProviderConfig);
            }
            return false;
        }

        public async Task<bool> SendAllAsync(string[] destinatiions, string notifySendConfig, string jsonProviderConfig, string provaider)
        {

            if (Enum.TryParse(provaider, out Provaiders provaiderout))
            {
                SmsConfig config = JsonConvert.DeserializeObject<SmsConfig>(notifySendConfig);
                return await ResolverIntanceProvaider<IProvaiderSms>(provaiderout)?.SendAllAsync(destinatiions, config, jsonProviderConfig);
            }
            return false;
        }


        private static T ResolverIntanceProvaider<T>(Provaiders provaider)
        {
            if (LisProvaiderSms.ContainsKey(provaider))
            {
                Object Obj = Activator.CreateInstance(LisProvaiderSms.FirstOrDefault(t => t.Key == provaider).Value);

                if (Obj is T)
                    return (T)Obj;
            }
            return default(T);
        }
    }
}
