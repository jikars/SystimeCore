using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SystimeWCF.Contract;
using Utils.Contract.ServiceAccess.Http;

namespace SystimeWCF
{
    public class WcfSystime : IWcfSystime
    {

        private const String Reourcce = "NotifyTableChanged";

        private IServiceRequestHttp ServiceRequestHttp { get; set; }

        public WcfSystime()
        {
            ServiceRequestHttp = DependencyFactory.Resolve<IServiceRequestHttp>();
        }

        public  bool NotifyChangeDatabaseWcf(string table, string action,string id, string urlWcfHttp)
        {
            List<Tuple<String, String>> parameters = new List<Tuple<string, string>>()
            {
                {new Tuple<string, string>("tableName",table)},
                {new Tuple<string, string>("idKey",id)},
                {new Tuple<string, string>("action",action)},
            };

            ResponseServiceRequest response =   ServiceRequestHttp.Post(urlWcfHttp, Reourcce, parameters, null,null,EnumsServiceRequestHttp.ContentTypeBody.Json);

            if (response?.HttpStatusCode == System.Net.HttpStatusCode.OK)
                return true;
            return false;
           
        }
    }
}
