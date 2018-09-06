using JsonSubTypes;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Notifications.Notifications.Config
{
    [JsonConverter(typeof(JsonSubtypes))]
    [JsonSubtypes.KnownSubTypeWithProperty(typeof(SmsConfig), "Title")]
    [JsonSubtypes.KnownSubTypeWithProperty(typeof(WhatsAppConfig), "UrlFile")]
    public class ConfigNotification
    {
        public String Message { get; set; }
    }
}
