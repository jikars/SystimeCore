using ErpDataAccessFromSystime.Contract;
using ErpDataAccessFromSystime.Contract.ErpDataAccessFromSystime.Contract;
using ErpDataAccessFromSystime.Erps.DmsV1.DataAcces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SystimeDataAcces.DataAccess;

namespace ErpDataAccessFromSystime.Erps
{
    internal interface IErpDataAccess
    {

        Boolean SupportItemWorkOrder(string jsonKeys, ParamsContract paramsContract);

        WorkOrders GetWorkOrder(string jsonKeys, ParamsContract paramsContract, out List<Tuple<Type, String>> paramsAditional);

        Boolean DeleteNotificationsSystimeGroup(String jsonKeys,String tableName,String eventTable,DateTime cretedAtLimit,ParamsContract paramsContract);

        Workers GetWorker(string jsonKeys, ParamsContract paramsContract, out List<Tuple<Type, String>> paramsAditional);

        Vehicles GetVehicle(string jsonKeys, ParamsContract paramsContract, out List<Tuple<Type, String>> paramsAditional);

        Customers GetCustomer(string jsonKeys, ParamsContract paramsContract);

        CloudCatalogInsuranceCompanies GetInsuranceCompany(string jsonKeys, ParamsContract paramsContract);

        CloudCatalogDealerShops GetDealerShop(string jsonKeys, ParamsContract paramsContract);


        List<String> GetAllJsonKeysWorkOrder(ParamsContract paramsContract,DateTime dateTimeMin);


        Boolean DeletenotificationSystime(ParamsContract paramsContract, String idNotificationsystime);


        List<NotificationSystimeIntegrate> GetAllNotificationSystime(ParamsContract paramsContract);


        bool UpdateAutorizationAtAndObservations(string idShpo, string workOrderNumber, string observations, DateTime? autorizatheAt, ParamsContract paramsContract);
    }
}
