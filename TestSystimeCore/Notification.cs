using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;
using Notifications;
using Notifications.Contract;
using Notifications.Notifications.SMS.Provaider;
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
        public void SendNotification()
        {
            String json =  JsonConvert.SerializeObject(new ConfigMsMFromContacto() { Password = "3sF3R4c0", User = "SMSESFERAC", PthBase = "http://www.appcontacto.com.co", ResourceBase = "wsurl" });
            INotify notify = new ResolverNotify();
            Assert.IsTrue(notify.Send("3183913780","aqui","puufff", json, TypeNotification.SMS, "Contacto", out String error));
        }
    }
}
