using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;
using Notifications;
using Notifications.Contract;
using Notifications.Notifications.Config;
using Notifications.Notifications.SMS.Provaider;
using Notifications.Notifications.Whatsapp.Provider;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestSystimeCore
{
    [TestClass]
    public  class Notification
    {
        [TestMethod]
        public void SendNotificationSms()
        {
            String jsonConfig =  JsonConvert.SerializeObject(new ConfigMsMFromContacto() { Password = "3sF3R4c0", User = "SMSESFERAC", PthBase = "http://www.appcontacto.com.co", ResourceBase = "wsurl" });
            SmsConfig MessageConfig = new SmsConfig() { Title = "titulo", Message = "esto es el modulo de pruebass para sms" };
            INotify notify = new ResolverNotify();
            Assert.IsTrue(notify.Send("3107695759", MessageConfig, jsonConfig, TypeNotification.SMS, "Contacto", out String error));
        }


        [TestMethod]
        public void SendNotificationWhatsApp()
        {
            String jsonConfig = JsonConvert.SerializeObject(new ConfigWahtsAppFromWabBox() { Key = "598da6b0579da2b1624d024cb3e0595c5b9147c19af76", Phone = "573174354217", PathBase = "https://www.waboxapp.com" });
            WhatsAppConfig MessageConfig = new WhatsAppConfig() { Message = "esto es un mensaje de prueba con imagen", TypeSend = TypeSend.TextImage, UrlFile = "https://androidzone.org/wp-content/uploads/2012/11/logo-android2-720x540.jpg" };
            INotify notify = new ResolverNotify();
            Assert.IsTrue(notify.Send("573107695759", MessageConfig, jsonConfig, TypeNotification.WHATSAPP, "Waboxapp", out String error));
        }
    }
}
