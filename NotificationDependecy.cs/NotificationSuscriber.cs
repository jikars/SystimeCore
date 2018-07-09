using NotificationDependecy.Contract;
using NotificationDependecy.Notification.Tables;
using System;
using System.Collections.Generic;
using System.Linq;

using System.Data.SqlClient;
using System.Security.Permissions;
using Newtonsoft.Json;
using SystimeCore.Config;

namespace NotificationDependecy
{
    public class NotificationSuscriber : INotificationSuscriber
    {
        private readonly List<Models.Notification> Notifcations;
        private String ConectionString { get; set; }
        private const string NAME_FILE = "NotificationConfig.json";
        public NotificationSuscriber()
        {
            Notifcations = JsonConvert.DeserializeObject<List<Models.Notification>>(System.IO.File.ReadAllText(ConfigInstall.GetPathNotification(NAME_FILE)));
        }
        private static Dictionary<String, INotify> TablesInNotify = new Dictionary<string, INotify>();

        public void StartSucriber(string conectionString)
        {
            ConectionString = conectionString;
            ConectionString = ConectionString.Substring(conectionString.IndexOf(@"connection string='"));
            ConectionString = ConectionString.Replace("connection string='", "").Replace("'", "");
            SqlClientPermission clientPermission = new SqlClientPermission(PermissionState.Unrestricted);
            clientPermission.Demand();
            SqlDependency.Stop(ConectionString);
            SqlDependency.Start(ConectionString);
            foreach (Models.Notification item in Notifcations)
            {
                TablesInNotify.Add(item.NotificationName.ToUpper(), new Notify());
                TablesInNotify.FirstOrDefault(v =>
                v.Key == item.NotificationName.ToUpper()).Value?.StartNotifycation(ConectionString, item.TableName, item.NotificationName, item.ColumNotify, item.EventDatabase, item.RecipientsType, item.FillterValidatePostScript, item.QueryPostEvent,item.NotificationModuleTable, item.DelayTimeOut);
            }
        }

        public void StopAllNotifcation()
        {
            TablesInNotify?.ToList()?.ForEach(e => e.Value.StopNotification());
        }

        public void StopSuscriber(string nameNotification)
        {
            SqlDependency.Stop(ConectionString);
            if (TablesInNotify.TryGetValue(nameNotification, out INotify value))
                value.StopNotification();
        }
    }
}
