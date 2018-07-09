using ErpDataAccessFromSystime.Contract;
using ErpDataAccessFromSystime.Contract.ErpDataAccessFromSystime.Contract;
using ErpDataAccessFromSystime.Erps.DmsV1.DataAcces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SystimeDataAcces.DataAccess;

namespace ErpDataAccessFromSystime.Erps.DmsV1
{
    internal class DmsV1 : IErpDataAccess
    {
        public bool DeleteNotificationsSystimeGroup(string jsonKeys, string tableName, string eventTable, DateTime cretedAtLimit, ParamsContract paramsContract)
        {
             return new NotificationSystimeDmsV1(paramsContract).DeleteNotificationSystimeGrouup(jsonKeys,tableName,eventTable,cretedAtLimit);
        }

        public bool DeletenotificationSystime(ParamsContract paramsContract, String idNotificationsystime)
        {
            if (int.TryParse(idNotificationsystime, out int id))
                return new NotificationSystimeDmsV1(paramsContract).DeleteNotificationSystime(id);
            else
                return false;
        }

        public List<string> GetAllJsonKeysWorkOrder(ParamsContract paramsContract, DateTime dateTimeMin)
        {
            return new WorkOrderDmsV1(paramsContract).GetAllJsonKeysWorkOrder(dateTimeMin);
        }

        public List<NotificationSystimeIntegrate> GetAllNotificationSystime(ParamsContract paramsContract)
        {
            return new NotificationSystimeDmsV1(paramsContract).GetAllNotification();
        }

        public Customers GetCustomer(string jsonKeys, ParamsContract paramsContract)
        {
            return new CustomersDmsV1(paramsContract).GetCustomer(jsonKeys);
        }

        public CloudCatalogDealerShops GetDealerShop(string jsonKeys, ParamsContract paramsContract)
        {
            return new DealerShopsDmsV1(paramsContract).GetDealerShop(jsonKeys);
        }

        public CloudCatalogInsuranceCompanies GetInsuranceCompany(string jsonKeys, ParamsContract paramsContract)
        {
            return new InsuranceCompanyDmsV1(paramsContract).GetInsuranceCompany(jsonKeys);
        }


        public Vehicles GetVehicle(string jsonKeys, ParamsContract paramsContract, out List<Tuple<Type, string>> paramsAditional)
        {
            return new VehiclesDmsV1(paramsContract).GetVehicle(jsonKeys,out paramsAditional);
        }

        public Workers GetWorker(string jsonKeys, ParamsContract paramsContract, out List<Tuple<Type, String>> paramsAditional)
        {
            return new WorkerDmsV1(paramsContract).GetWorker(jsonKeys, out paramsAditional);
        }

        public WorkOrders GetWorkOrder(string jsonKeys, ParamsContract paramsContract, out List<Tuple<Type, String>> paramsAditional)
        {
            return new WorkOrderDmsV1(paramsContract).GetWorkOrder(jsonKeys, out paramsAditional);
        }

        public bool SupportItemWorkOrder(string jsonKeys, ParamsContract paramsContract)
        {
            return new WorkOrderDmsV1(paramsContract).SupporItem(jsonKeys);
        }

        public bool UpdateAutorizationAtAndObservations(string idShpo, string workOrderNumber, string observations, DateTime? autorizatheAt, ParamsContract paramsContract)
        {
            return new WorkOrderDmsV1(paramsContract).UpdateAuutizatheAtAndObservations(idShpo, workOrderNumber, observations,autorizatheAt);
        }
    }
}
