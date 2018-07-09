using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SystimeWCF.Contract
{
    public interface IWcfSystime
    {
        Boolean NotifyChangeDatabaseWcf(String table, String action,String id,String urlWcfHttp);
    }
}
