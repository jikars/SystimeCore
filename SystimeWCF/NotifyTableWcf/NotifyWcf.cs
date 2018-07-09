using System;
using SystimeDataAcces.DataAccess;
using SystimeWCF.Contract;
using TableDependency.Enums;
using TableDependency.EventArgs;
using TableDependency.SqlClient;
using System.Linq;
using SystimeWCF.NotifyTableWcf.WcfNotification;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace SystimeWCF.NotifyTableWcf
{
    public  class NotifyWcf<T>: INotifyWcf<T> where T:class
    {  
        private SqlTableDependency<T> DependyTable { get; set; }
        private NotifyToWcf NotificationWcf { get; set; }
        private ChangeType[] EventSupport { get; set; }

        private String NameNotification { get; set; }

        private String UrlWcf { get; set; }

        private String TableName { get; set; }


        public NotifyWcf()
        {
            NotificationWcf = new NotifyToWcf();
        }

        public void NotifyWcfStart(string conectionString, string urlWcf, string[] propiertyEvent, ChangeType[] eventSupport,string  nameNotication)
        {
            if(DependyTable == null && !String.IsNullOrEmpty(conectionString) && !String.IsNullOrEmpty(urlWcf)  && eventSupport != null)
            {
                NameNotification = nameNotication;
                TableName = typeof(T).Name;
                EventSupport = eventSupport;
                UrlWcf = urlWcf;
                conectionString = conectionString.Substring(conectionString.IndexOf(@"connection string='"));
                conectionString = conectionString.Replace("connection string='","").Replace("'","");
                try
                {
                    DependyTable = new SqlTableDependency<T>(conectionString);

                    DependyTable.OnChanged += NotifyChange;
                    DependyTable.Start();
                }
                catch
                {
                    DependyTable?.Stop();
                    DependyTable = null;
                }
              

            }          
        }
        
        public void NotifyWcfStop()
        {
            DependyTable?.Stop();
            DependyTable = null;
        }

        private void NotifyChange(object sender, RecordChangedEventArgs<T> e)
        {
            if (DependyTable != null && EventSupport.Contains(e.ChangeType))
            {
                Task.Factory.StartNew( () => {
                    NotificationWcf.SendToWcf(new SystimeServiceWcf.Notification()
                    {
                        EntityJson = JsonConvert.SerializeObject(e.Entity),
                        Event = e.ChangeType.ToString(),
                        TableName = TableName
                    });

                });
            }

        }




    }
}
