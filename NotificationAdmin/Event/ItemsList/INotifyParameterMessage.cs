using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static NotificationAdmin.Constants;

namespace NotificationAdmin.Event.ItemsList
{
    interface INotifyParameterMessage
    {
        void NotifyParamterMessageChange(TypeParameterMessage parameterMessage,String parameter, String columParameter);
    }

    interface INotifyMessage
    {
        void NotifyChangeTitle(Dictionary<String,String> listParamterTitle);
        void NotifyChangeMessage(Dictionary<String, String> ListParamterMessage);


    }
}
