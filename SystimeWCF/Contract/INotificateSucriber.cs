using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SystimeWCF.Contract
{
    public interface INotificateSucriber
    {
        void StartNofity(List<NotifyConfig> notifyWcf, String urlWcf, String conectionString);

        void StopSuscriber();

        void StopNotify(String tableName);



    }
}
