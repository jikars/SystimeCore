using ErpDataAccessFromSystime.Erps.DmsV1.DataAcces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SystimeDataAcces.DataAccess;
using static ErpDataAccessFromSystime.TypesErpConfig;

namespace ErpDataAccessFromSystime.Contract
{
    namespace ErpDataAccessFromSystime.Contract
    {
        public interface IDataAccesErpContract
        {
            WorkOrders GetWorkOrder(String jsonKeysErp, ErpsTypes nameErp, ParamsContract paramsContract, out List<Tuple<Type, String>> paramsAditional);
            Workers GetWorker(String jsonKeysErp, ErpsTypes nameErp, ParamsContract paramsContract, out List<Tuple<Type, String>> paramsAditional);
            Vehicles GetVehicle(String jsonKeysErp, ErpsTypes nameErp, ParamsContract paramsContract, out List<Tuple<Type, String>> paramsAditional);
            CloudCatalogInsuranceCompanies GetInsuranceCompany(String jsonKeysErp, ErpsTypes nameErp, ParamsContract paramsContract);
            CloudCatalogDealerShops GetDealerShop(String jsonKeysErp, ErpsTypes nameErp, ParamsContract paramsContract);
            Customers GetCustomer(String jsonKeysErp, ErpsTypes nameErp, ParamsContract paramsContract);

            List<String> GetAllJsonKeysWorkORder(ErpsTypes nameErp, ParamsContract paramsContract, DateTime dateTime);

            Boolean DeleteNotificationSystime(String idNotificationSystime, ParamsContract paramsContract, ErpsTypes nameErp);


            List<NotificationSystimeIntegrate> GetNotificationSystime(ParamsContract paramsContract, ErpsTypes nameErp);

            Boolean DeleteNotifcationSystimeGroup(String jsonKeys, String tableName, String eventTable, DateTime createdAt, ParamsContract paramsContract, ErpsTypes nameErp);


            Boolean SupportItemWorkOrder(String jsonKeysErp, ErpsTypes nameErp, ParamsContract paramsContract);
        }



        public class ParamsContract
        {
            public String ConectionStringSystime { get; set; }
            public String ConectionStringErp { get; set; }
            public String Language { get; set; }
            public String[] SupportShop { get; set; }
        }


        public class NotificationSystimeIntegrate
        {

            public String IdNotification { get; set; }
            public String TableName { get; set; }

            public String JsonKeys { get; set; }

            public String Event { get; set; }


            public DateTime CreatedAt { get; set; }

        }
    }
}