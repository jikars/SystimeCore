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

        public Boolean Send(string destinatiion, string title, string message, string jsonConfig, string provaider, out string errorMessage)
        {
            errorMessage = "ProvaiderNotSupport";
            if (Enum.TryParse(provaider, out Provaiders provaiderout))
               return (Boolean)ResolverIntanceProvaider<IProvaiderSms>(provaiderout)?.SendSms(destinatiion,message,title,jsonConfig, out errorMessage);
            return false;
        }

        public bool SendAll(string[] destinatiions, string title, string message, string jsonConfig, string provaider, out string errorMessage)
        {
            errorMessage = "ProvaiderNotSupport";
            if (Enum.TryParse(provaider, out Provaiders provaiderout))
                return (Boolean)ResolverIntanceProvaider<IProvaiderSms>(provaiderout)?.SendSmsAll(destinatiions, message, title, jsonConfig, out errorMessage);
            return false;
        }


        public async Task<Boolean> SendAsync(string destinatiion, string title, string message, string jsonConfig, string provaider)
        {
            if (Enum.TryParse(provaider, out Provaiders provaiderout))
                return await ResolverIntanceProvaider<IProvaiderSms>(provaiderout)?.SendSmsAsync(destinatiion, message, title, jsonConfig);
            return false;
        }

        public async Task<bool> SendAllAsync(string[] destinatiions, string title, string message, string jsonConfig, string provaider)
        {
            if (Enum.TryParse(provaider, out Provaiders provaiderout))
                return await ResolverIntanceProvaider<IProvaiderSms>(provaiderout)?.SendSmsAllAsync(destinatiions, message, title, jsonConfig);
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
