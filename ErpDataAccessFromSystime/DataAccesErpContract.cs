using ErpDataAccessFromSystime.Contract;
using ErpDataAccessFromSystime.Erps;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SystimeDataAcces.DataAccess;
using static ErpDataAccessFromSystime.TypesErpConfig;
using ErpDataAccessFromSystime.Erps.DmsV1.DataAcces;
using ErpDataAccessFromSystime.Contract.ErpDataAccessFromSystime.Contract;

namespace ErpDataAccessFromSystime
{
    public   class DataAccesErpContract : IDataAccesErpContract, IDataAccessSystimeToErp
    {
        public bool DeleteNotifcationSystimeGroup(string jsonKeys, string tableName, string eventTable, DateTime createdAt, ParamsContract paramsContract, ErpsTypes nameErp)
        {
            IErpDataAccess ErpDataAccess = ResolveIntance<IErpDataAccess>(nameErp);
            return ErpDataAccess.DeleteNotificationsSystimeGroup(jsonKeys, tableName,eventTable, createdAt, paramsContract);
        }

        public bool DeleteNotificationSystime(String idNotificationSystime, ParamsContract paramsContract, ErpsTypes nameErp)
        {
            IErpDataAccess ErpDataAccess = ResolveIntance<IErpDataAccess>(nameErp);
            return ErpDataAccess.DeletenotificationSystime(paramsContract, idNotificationSystime);
        }

        public List<String> GetAllJsonKeysWorkORder(ErpsTypes nameErp, ParamsContract paramsContract,DateTime dateTime)
        {
            IErpDataAccess ErpDataAccess = ResolveIntance<IErpDataAccess>(nameErp);
            return ErpDataAccess.GetAllJsonKeysWorkOrder(paramsContract, dateTime);
        }

        public List<NotificationSystimeIntegrate> GetNotificationSystime(ParamsContract paramsContract, ErpsTypes nameErp)
        {
            IErpDataAccess ErpDataAccess = ResolveIntance<IErpDataAccess>(nameErp);
            return ErpDataAccess.GetAllNotificationSystime(paramsContract);
        }

        public Workers GetWorker(string jsonKeysErp, ErpsTypes nameErp, ParamsContract paramsContract, out List<Tuple<Type, string>> paramsAditional)
        {
            IErpDataAccess ErpDataAccess = ResolveIntance<IErpDataAccess>(nameErp);
            return ErpDataAccess.GetWorker(jsonKeysErp, paramsContract, out paramsAditional);
        }

        public bool SupportItemWorkOrder(string jsonKeysErp, ErpsTypes nameErp, ParamsContract paramsContract)
        {
            IErpDataAccess ErpDataAccess = ResolveIntance<IErpDataAccess>(nameErp);
            return ErpDataAccess.SupportItemWorkOrder(jsonKeysErp, paramsContract);
        }

        public bool UpdateAutorizateAtObservationsSystime(string idShpo, string workOrderNumber, string observations, DateTime? autorizatheAt, String nameErp, ParamsContract paramsContract)
        {
            if(Enum.TryParse(nameErp,out  ErpsTypes erp))
            {
                IErpDataAccess ErpDataAccess = ResolveIntance<IErpDataAccess>(erp);
                return ErpDataAccess.UpdateAutorizationAtAndObservations(idShpo, workOrderNumber, observations, autorizatheAt, paramsContract);
            }
            return false;
        }

        Customers IDataAccesErpContract.GetCustomer(string jsonKeysErp, ErpsTypes nameErp, ParamsContract paramsContract)
        {
            IErpDataAccess ErpDataAccess = ResolveIntance<IErpDataAccess>(nameErp);
            return ErpDataAccess.GetCustomer(jsonKeysErp, paramsContract);
        }

        CloudCatalogDealerShops IDataAccesErpContract.GetDealerShop(string jsonKeysErp, ErpsTypes nameErp, ParamsContract paramsContract)
        {
            IErpDataAccess ErpDataAccess = ResolveIntance<IErpDataAccess>(nameErp);
            return ErpDataAccess.GetDealerShop(jsonKeysErp, paramsContract);
        }

        CloudCatalogInsuranceCompanies IDataAccesErpContract.GetInsuranceCompany(string jsonKeysErp, ErpsTypes nameErp, ParamsContract paramsContract)
        {
            IErpDataAccess ErpDataAccess = ResolveIntance<IErpDataAccess>(nameErp);
            return ErpDataAccess.GetInsuranceCompany(jsonKeysErp, paramsContract);
        }

      

        Vehicles IDataAccesErpContract.GetVehicle(string jsonKeysErp, ErpsTypes nameErp, ParamsContract paramsContract, out List<Tuple<Type, string>> paramsAditional)
        {
            IErpDataAccess ErpDataAccess = ResolveIntance<IErpDataAccess>(nameErp);
            return ErpDataAccess.GetVehicle(jsonKeysErp, paramsContract,out paramsAditional);
        }


        WorkOrders IDataAccesErpContract.GetWorkOrder(string jsonKeysErp, ErpsTypes nameErp, ParamsContract paramsContract, out List<Tuple<Type, String>> paramsAditional)
        {
            IErpDataAccess ErpDataAccess = ResolveIntance<IErpDataAccess>(nameErp);
            return ErpDataAccess.GetWorkOrder(jsonKeysErp, paramsContract, out  paramsAditional);
        }





    }
}
