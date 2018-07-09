using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using Dapper.Contrib.Extensions;

namespace NotificationDependecy.Notification.DataAccess
{
    public class NotificationLog
    {
        [Key]
        public int IdNotificationLog { get; set; }
        public String Destination { get; set; }
        public String TypeNotification { get; set; }

        public String NotificationName { get; set; }

        public String FillterRecipenttype { get; set; }
        public String NameRecipientsType { get; set; }


        public String Provaider { get; set; }

        public String MessageSend { get; set; }

        public String TitleMessage { get; set; }

        public String MessageErrorProvaider { get; set; }

        public bool? IsSend { get; set; }
        public DateTime? CreatedAt { get; set; }

        public String CreatedById { get; set; }
    }
}
