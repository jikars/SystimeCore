using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Notifications.Contract
{
    public interface INotify
    {
        Boolean Send(string destinatiion, string jsonMessageConfig, string jsonProviderConfig, TypeNotification typeNotification, string provaider,out string errorMessage);
        Boolean SendAll(String[] destinatiions, string jsonMessageConfig, string jsonProviderConfig, TypeNotification typeNotification, string provaider, out string errorMessage);

        Task<Boolean> SendAsync(string destinatiion, string jsonMessageConfig, string jsonProviderConfig, TypeNotification typeNotification, string provaider);
        Task<Boolean> SendAllAsync(String[] destinatiions, string jsonMessageConfig, string jsonProviderConfig, TypeNotification typeNotification, string provaider);
    }

    public enum TypeNotification
    {
        SMS,EMAIL,WHATSAPP,PUSH,GOOGLECHROME,EDGE
    }
}
