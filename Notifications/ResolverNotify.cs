using Notifications.Contract;
using Notifications.Notifications;
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

        public bool Send(string destinatiion, string jsonMessageConfig, string jsonProviderConfig, TypeNotification typeNotification, string provaider, out string errorMessage)
        {
            errorMessage = "not Send";
            return ResolveTypeNotification<INotification>(typeNotification)?.Send(destinatiion, jsonMessageConfig, jsonProviderConfig, provaider, out errorMessage) ?? false;
        }

        public bool SendAll(string[] destinatiions, string jsonMessageConfig, string jsonProviderConfig, TypeNotification typeNotification, string provaider, out string errorMessage)
        {
            errorMessage = "not Send";
            return ResolveTypeNotification<INotification>(typeNotification)?.SendAll(destinatiions, jsonMessageConfig, jsonProviderConfig, provaider, out errorMessage) ?? false;
        }

        public async Task<bool> SendAsync(string destinatiion, string jsonMessageConfig, string jsonProviderConfig, TypeNotification typeNotification, string provaider)
        {
            return await ResolveTypeNotification<INotification>(typeNotification)?.SendAsync(destinatiion, jsonMessageConfig, jsonProviderConfig, provaider);
        }

        public async Task<bool> SendAllAsync(string[] destinatiions, string jsonMessageConfig, string jsonProviderConfig, TypeNotification typeNotification, string provaider)
        {
            return await ResolveTypeNotification<INotification>(typeNotification)?.SendAllAsync(destinatiions, jsonMessageConfig, jsonProviderConfig, provaider);
        }
    }
}
