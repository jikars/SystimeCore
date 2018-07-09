using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static NotificationDependecy.Contract.Enums;

namespace NotificationDependecy.Models
{
    public class Notification
    {
        public String NotificationName { get; set; }
        public String TableName { get; set; }

        public List<String> ColumNotify { get; set; }

        public List<EvnetFillterDataBase> EventDatabase { get; set; }

        public List<RecipientType> RecipientsType { get; set; }

        public string FillterValidatePostScript { get; set; }

        public int? DelayTimeOut { get; set; }

        public bool NotificationModuleTable { get; set; } = false;

        public String QueryPostEvent { get; set; }


    }


    public class EvnetFillterDataBase
    {
        public String EventName { get; set; }
        public EventDataBase EventDatabse { get; set; }

        public String FillterDatabsase { get; set; }

    }


    public class RecipientType
    {
        public String RecipientName { get; set; }


        public MessageSend Message { get; set; }

        public List<RecipientSend> FilttersRecipient { get; set; }
    }

    public class RecipientSend
    {
        public String Filtter { get; set; }

        public List<NotifycatioType> TypeNotification { get; set; }
    }

    public class DynamicQueryParam
    {
        public String IdParam { get; set; }
        public String ValueParam { get; set; }

    }

    public class MessageSend
    {
        public String TitleMessage { get; set; }

        public String Message { get; set; }

        public List<MessageConfig> ConfigMessage { get; set; }
       
    }

    public class NotifycatioType
    {
        public String TypeNotification { get; set; }
        public String Provaider { get; set; }
        public String JsonNotificationConfig { get; set; }
        public String ExpressionRegularMach { get; set; }

    }

    public class MessageConfig
    {
        public String DinamycParam { get; set; }

        public String DefinitiveValue { get; set; }
        public List<String> NameColum { get; set; }
        public List<String> ExpressionRegular { get; set; }

    }
}
