using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SystimeWCF.Contract
{
    internal interface INotifyTable
    {
        void NotifyWcfStart(String conectionString, String urlWcf, String[] propiertyEvent, TableDependency.Enums.ChangeType[] eventSupport,String nameNotication);

        void NotifyWcfStop();
    }
}
