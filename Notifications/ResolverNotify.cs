using Notifications.Contract;
using Notifications.Notifications;
using Notifications.Notifications.SMS;
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
                { TypeNotification.SMS, typeof(Sms) }

            };


        public ResolverNotify()
        {
        }

        public bool Send(string destinatiion, string title, string message, string jsonConfig, TypeNotification typeNotification, string provaider, out string  errorMessage)
        {
            errorMessage = "not Send";
            return ResolveTypeNotification<INotification>(typeNotification)?.Send(destinatiion, title, message, jsonConfig, provaider,out errorMessage) ?? false;
        }

        public bool SendAll(string[] destinatiions, string title, string message, string jsonConfig, TypeNotification typeNotification, string provaider, out string errorMessage)
        {
            errorMessage = "not Send";
            return ResolveTypeNotification<INotification>(typeNotification)?.SendAll(destinatiions, title, message, jsonConfig, provaider, out errorMessage) ?? false;
        }

        public async Task<Boolean> SendAllAsync(string[] destinatiions, string title, string message, string jsonConfig, TypeNotification typeNotification, string provaider)
        {
            return await ResolveTypeNotification<INotification>(typeNotification)?.SendAllAsync(destinatiions, title, message, jsonConfig, provaider);
        }

        public async Task<Boolean> SendAsync(string destinatiion, string title, string message, string jsonConfig, TypeNotification typeNotification, string provaider)
        {
            return await ResolveTypeNotification<INotification>(typeNotification)?.SendAsync(destinatiion, title, message, jsonConfig, provaider);
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
    }
}
