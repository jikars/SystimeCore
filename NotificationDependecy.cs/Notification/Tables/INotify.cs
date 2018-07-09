using NotificationDependecy.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NotificationDependecy.Notification.Tables
{
    internal interface INotify
    {
        void StartNotifycation(string conectionString, string tableEvent, String notificationName, List<string> columNotify, List<EvnetFillterDataBase> eventDatabase, List<RecipientType> recipientsType, String fillterValidation, String queryPostNotificaiton, Boolean isNnotificationFromTable, int? delayNotification);
        void StopNotification();
    }
}
