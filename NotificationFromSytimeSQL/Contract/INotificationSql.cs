using NotificationFromSytimeSQL.DataAcces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NotificationFromSytimeSQL.Contract
{
    public interface INotificationSql
    {
        void Notification(string eventName, string tableName, string acction, string jsonTableData,string conectionString);

        List<String> GetAllTableNotification(string conectionString);


        List<Tuple<int,String, String>> GetProvaiderNotification(string conectionString);


        List<NotificationMessageBase> GetAllNotificationMessage(string conectionString);

    }



    public enum ActionTable
    {
        I,U,D
    }
}
