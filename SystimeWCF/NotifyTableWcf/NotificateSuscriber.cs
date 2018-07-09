using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SystimeDataAcces.DataAccess;
using SystimeWCF.Contract;
using SystimeWCF.NotifyTableWcf.TablesNotify;
using TableDependency.Enums;

namespace SystimeWCF.NotifyTableWcf
{
    public class NotificateSuscriber : INotificateSucriber
    {
        private static Dictionary<String,INotifyTable> TablesInNotify = new Dictionary<string, INotifyTable>();

        private static Dictionary<String,Type> Dictionary = new Dictionary<String,Type>() {
            { "WorkOrders",typeof(WorkOrdertNotify)},
            { "WorkOrderTraking",typeof(WorkOrderTracking)},
            { "OperationWorkOrder",typeof(OperationByWorkOrder)},
        };                                                                                                                                                                         


        public void StartNofity(List<NotifyConfig> notifyWcf,String urlWcf,String conectionString)
        {
            List<ChangeType> changesTyps = null;
            Type tableNotifycation = null;
            foreach(NotifyConfig item in notifyWcf)
            {
                changesTyps =new  List<ChangeType>();
                tableNotifycation = Dictionary.Where(t => t.Key.ToUpper() == item.TableName.ToUpper() && !TablesInNotify.ContainsKey(t.Key.ToUpper()))?.FirstOrDefault().Value;
                if(tableNotifycation!= null)
                {
                    INotifyTable notifyTable = (INotifyTable)Activator.CreateInstance(tableNotifycation);
                    item.EnventArray.ToList().ForEach(e => { changesTyps.Add(CastChange(e)); });
                    notifyTable.NotifyWcfStart(conectionString, urlWcf, item.PropiertyChange, changesTyps.ToArray(), item.NameNotification);
                    TablesInNotify.Add(item.TableName.ToUpper(), notifyTable);
                }
            }
        }

        public ChangeType CastChange(Enums.EventDataBase evnet)
        {
            switch(evnet)
            {
                case Enums.EventDataBase.Insert:
                    return ChangeType.Insert;
                case Enums.EventDataBase.Update:
                    return ChangeType.Update;
                case Enums.EventDataBase.Delete:
                    return ChangeType.Delete;
              default:
                    return ChangeType.None;

            }
        }

       

        public void StopSuscriber()
        {
            TablesInNotify?.ToList()?.ForEach(e => e.Value.NotifyWcfStop());
        }

        public void StopNotify(string tableName)
        {
            if (TablesInNotify.TryGetValue(tableName, out INotifyTable value))
                value.NotifyWcfStop();
        }
    }
}
