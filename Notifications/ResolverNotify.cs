using Notifications.Contract;
using Notifications.Notifications;
using Notifications.Notifications.Config;
using Notifications.Notifications.SMS;
using Notifications.Notifications.Whatsapp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Notifications
{
    public class ResolverNotify : INotify
    {

        private static Dictionary<TypeNotification, Type> LisProvaiderNotify = new Dictionary<TypeNotification, Type>
            {
                { TypeNotification.SMS, typeof(Sms) },
                { TypeNotification.WHATSAPP, typeof(Whatsapp) }
            };


        public ResolverNotify()
        {
        }

        private static  T ResolveTypeNotification<T>(TypeNotification typeNotification)
        {
            if (LisProvaiderNotify.ContainsKey(typeNotification))
            {
                Object Obj = Activator.CreateInstance(LisProvaiderNotify.FirstOrDefault(t => t.Key == typeNotification).Value);

                if (Obj is T)
                    return (T)Obj;
            }

            return default(T);
        }

        public bool Send(string destinatiion, ConfigNotification messageConfig, string jsonProviderConfig, TypeNotification typeNotification, string provaider, out string errorMessage)
        {
            errorMessage = "not Send";
            return ResolveTypeNotification<INotification>(typeNotification)?.Send(destinatiion, messageConfig, jsonProviderConfig, provaider, out errorMessage) ?? false;
        }

        public bool SendAll(string[] destinatiions, ConfigNotification messageConfig, string jsonProviderConfig, TypeNotification typeNotification, string provaider, out string errorMessage)
        {
            errorMessage = "not Send";
            return ResolveTypeNotification<INotification>(typeNotification)?.SendAll(destinatiions, messageConfig, jsonProviderConfig, provaider, out errorMessage) ?? false;
        }

        public async Task<bool> SendAsync(string destinatiion, ConfigNotification messageConfig, string jsonProviderConfig, TypeNotification typeNotification, string provaider)
        {
            return await ResolveTypeNotification<INotification>(typeNotification)?.SendAsync(destinatiion, messageConfig, jsonProviderConfig, provaider);
        }

        public async Task<bool> SendAllAsync(string[] destinatiions, ConfigNotification messageConfig, string jsonProviderConfig, TypeNotification typeNotification, string provaider)
        {
            return await ResolveTypeNotification<INotification>(typeNotification)?.SendAllAsync(destinatiions, messageConfig, jsonProviderConfig, provaider);
        }
    }
}
