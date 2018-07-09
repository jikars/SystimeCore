using ErpDataAccessFromSystime;
using ErpDataAccessFromSystime.Contract;
using ErpDataAccessFromSystime.Contract.ErpDataAccessFromSystime.Contract;
using IntegrateErpToSystime.ModelIntegrate;
using Models;
using Newtonsoft.Json;
using ServicesAccessUbicar.cs.Contract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SystimeDataAcces.DataAccess;
using Unity.Attributes;
using static ErpDataAccessFromSystime.TypesErpConfig;
using static Models.ResponseServices.MessageError.CodeResponseServices;

namespace IntegrateErpToSystime.IntegrateLogic
{
    public class WorkOrder<T> : IIntegrate<T> where T : WorkOrderIntegrate
    {
        private readonly IDataAccesErpContract DataAccesErpContract;
        private readonly IServiceClientUbicar ServiceUbicar;



        public WorkOrder()
        {
            DataAccesErpContract = DependencyFactory.Resolve<IDataAccesErpContract>();
            ServiceUbicar = DependencyFactory.Resolve<IServiceClientUbicar>();
        }


        public T Save(string jsonKeys, ParamsIntegrateErp configErp, List<object> parameterAditional)
        {       
            WorkOrders workOrder = null;      
             if (Enum.TryParse(configErp.DllType, out ErpsTypes dllType))
            {
                workOrder = DataAccesErpContract.GetWorkOrder(jsonKeys, dllType, new ParamsContract()
                {
                    ConectionStringErp = configErp.ConectionStringErp,
                    ConectionStringSystime = configErp.ConectionStringToSystime,
                    Language = configErp.LanguageDb,
                    SupportShop = configErp.IdShopsErpArray
                }, out List<Tuple<Type, String>> listOtherAccionPreIntegrate);
                return Saving(workOrder, listOtherAccionPreIntegrate, configErp);
            }
            return default(T);
        }



        private T Saving(WorkOrders workOrder, List<Tuple<Type, String>> listOtherAccionPreIntegrate, ParamsIntegrateErp configErp)
        {
            WorkOrderIntegrate workOrderIntegrate = null;
            InsuranceCompanyIntegrate integrateinsuranceCompany = null;
            CustomerIntegrate integrateCustoemr = null;
            WorkerIntegrate integrateWorker = null;
            DealerShopIntegrate integrateDalerShop = null;
            VehicleIntegrate vehicleIntegrate = null;
            int milageWorkOrder = 0;
            if (workOrder != null)
            {
                integrateinsuranceCompany = UtilsIIntegrateErpToSystime.IntegrateOtherTypes<InsuranceCompanyIntegrate>(listOtherAccionPreIntegrate, configErp, null);
                integrateCustoemr = UtilsIIntegrateErpToSystime.IntegrateOtherTypes<CustomerIntegrate>(listOtherAccionPreIntegrate, configErp, null);
                integrateDalerShop = UtilsIIntegrateErpToSystime.IntegrateOtherTypes<DealerShopIntegrate>(listOtherAccionPreIntegrate, configErp, null);
                List<Object> paramsFromWorker = new List<object>
                    {
                       integrateDalerShop
                    };
                integrateWorker = UtilsIIntegrateErpToSystime.IntegrateOtherTypes<WorkerIntegrate>(listOtherAccionPreIntegrate, configErp, paramsFromWorker);
                milageWorkOrder = UtilsIIntegrateErpToSystime.SearchValueFromType<int>(listOtherAccionPreIntegrate);

                List<Object> paramsFromVehicle = new List<object>
                    {
                        integrateinsuranceCompany,
                        milageWorkOrder,
                        integrateCustoemr
                    };


                vehicleIntegrate = UtilsIIntegrateErpToSystime.IntegrateOtherTypes<VehicleIntegrate>(listOtherAccionPreIntegrate, configErp, paramsFromVehicle);

                if (integrateCustoemr?.CustomerSystime != null && integrateWorker?.WokerSystime != null
                    && integrateDalerShop?.DealerShopSystime != null)
                {

                    workOrder.IdVinNumber = vehicleIntegrate?.VehicleSystime?.IdVinNumber;
                    workOrder.IdSalesRepresentative = integrateWorker?.WokerSystime?.IdWorker;
                    workOrder.IdDealerShop = integrateDalerShop?.DealerShopSystime?.IdDealerShop;
                    workOrder.IdInsuranceCompany = integrateinsuranceCompany?.InsuranceCompnaySystime?.IdInsuranceCompany;
                    workOrder.IdCustomer = integrateCustoemr?.CustomerSystime?.IdCustomer ?? 0;

                    workOrder = new WorOrdersSystime(configErp.ConectionStringToSystime).SaveWorkOrder(workOrder, configErp.UserModify);
                    if (workOrder != null)
                    {
                        new WorkOrderLoadErrorLogSystime(configErp.ConectionStringToSystime).DeleteWorkOrderError(workOrder.WorkOrderNumber, workOrder.IdDealerShop);
                        workOrderIntegrate = new WorkOrderIntegrate()
                        {
                            WorkOrderSystime = workOrder
                        };

                        if (configErp.SaveInAzure)
                        {
                            List<Object> modelsParams = new List<object>();
                            if (integrateinsuranceCompany?.InsuranceCompnayUbicar != null)
                                modelsParams.Add(integrateinsuranceCompany?.InsuranceCompnayUbicar);
                            if (integrateCustoemr?.CustomerUbicar != null)
                                modelsParams.Add(integrateCustoemr?.CustomerUbicar);
                            if (integrateDalerShop?.DealerShopUbicar != null)
                                modelsParams.Add(integrateDalerShop?.DealerShopUbicar);
                            if (vehicleIntegrate?.VehicleUbicar != null)
                                modelsParams.Add(vehicleIntegrate?.VehicleUbicar);
                            if (integrateWorker?.WorkerUbicar != null)
                                modelsParams.Add(integrateWorker?.WorkerUbicar);
                            workOrderIntegrate.WorkOrderUbicar = SaveInAzure(workOrder, modelsParams, configErp);
                        }
                        
                    }
                }
                else if(workOrder != null)
                {
                    new WorkOrderLoadErrorLogSystime(configErp.ConectionStringToSystime).SaveWorkOrderError(new WorkOrderLoadErrorLog() {
                        idCustomerOT = workOrder.IdCustomer.ToString(),
                        idVehicle = workOrder.IdVinNumber,
                        IdWorkOrderNumberErp = workOrder.WorkOrderNumber,
                        idDealerShopOtErp = workOrder.IdDealerShop,
                        idWorkerOT = workOrder.WorkOrderNumber,
                    }, configErp.UserModify);
                }

                if (workOrderIntegrate != null)
                    return (T)Convert.ChangeType(workOrderIntegrate, typeof(T));
            }
            return default(T);
        }



        public WorkOrder SaveInAzure(WorkOrders workOrder, List<Object> paramsService, ParamsIntegrateErp configErp)
        {
            ServiceUbicar.Save(new WorkOrder()
            {
                IdCustomer = UtilsIIntegrateErpToSystime.ResolveTypeParameter<Person>(paramsService)?.IdPerson,
                IdDealerRepresentative = UtilsIIntegrateErpToSystime.ResolveTypeParameter<DealerRepresentative>(paramsService)?.IdDealerRepresentative,
                IdDealerShop = UtilsIIntegrateErpToSystime.ResolveTypeParameter<DealerShop>(paramsService)?.IdDealerShop,
                IdInsuraceConpany = UtilsIIntegrateErpToSystime.ResolveTypeParameter<InsuranceCompany>(paramsService)?.IdInsuranceCompany,
                IdVehicle = UtilsIIntegrateErpToSystime.ResolveTypeParameter<Vehicle>(paramsService)?.IdVehicle,
                IdErpQuote = workOrder?.IdErpQuote?.ToString(),
                WorkOrderIdErp = workOrder?.WorkOrderNumber,
                EnteredAt = workOrder?.EnteredAt,
                AutorizedAt = workOrder?.AuthorizedAt,
                IsTotalLoss = workOrder?.HasTotalLoss,
                Note = workOrder?.Note,
                ProgressMessaje = workOrder?.Note,
                PartsCompletedAt = workOrder?.PartsCompletedAt,
                ReadyToBePickedUpAt = workOrder?.ReadyToPickUpAt,
                PickedUpAt = workOrder?.PickedUpAt,
                PromisedAt = workOrder?.PromisedAt
            }, configErp.ReturnModelService, configErp.UrlService, configErp.LanguageDb, configErp.Token);

            if (ServiceUbicar.CodErrorRequest().HasValue && (ServiceUbicar.CodErrorRequest().Value == TypeErrorWebServicess.OperationOk.GetHashCode() || ServiceUbicar.CodErrorRequest().Value == TypeErrorWebServicess.NotDataFromUpdate.GetHashCode()))
            {
                new WorkOrderLoadErrorLogSystime(configErp.ConectionStringToSystime).DeleteWorkOrderError(workOrder.WorkOrderNumber, workOrder.IdDealerShop);
            }
            else
                new WorkOrderLoadErrorLogSystime(configErp.ConectionStringToSystime).SaveWorkOrderError(new WorkOrderLoadErrorLog()
                {
                    idCustomerOT = workOrder.IdCustomer.ToString(),
                    idVehicle = workOrder.IdVinNumber,
                    IdWorkOrderNumberErp = workOrder.WorkOrderNumber,
                    idDealerShopOtErp = workOrder.IdDealerShop,
                    idWorkerOT = workOrder.WorkOrderNumber,
                    CloudUpdateError = String.Concat(ServiceUbicar.CodErrorRequest(), "-", ServiceUbicar.MessageError())
                }, configErp.UserModify);

            return null;
        }

        public bool SaveAll(ParamsIntegrateErp configErp, DateTime dateTimeMin)
        {
            List<String> listKsonKeys = null;
            int? countTotal = 0;
            int countOk = 0;
            if (Enum.TryParse(configErp.DllType, out ErpsTypes dllType))
            {
                listKsonKeys = DataAccesErpContract.GetAllJsonKeysWorkORder(dllType, new ParamsContract()
                {
                    ConectionStringErp = configErp.ConectionStringErp,
                    ConectionStringSystime = configErp.ConectionStringToSystime,
                    Language = configErp.LanguageDb,
                    SupportShop = configErp.IdShopsErpArray
                }, dateTimeMin);

                countTotal = listKsonKeys?.Count;

                Parallel.ForEach(listKsonKeys, k => {
                    try
                    {
                        WorkOrderIntegrate wokrOrerIntegrarte = null;
                        wokrOrerIntegrarte = Save(k, configErp, null);
                        if (wokrOrerIntegrarte?.WorkOrderSystime != null)
                            countOk++;
                    }
#pragma warning disable S2486 // Generic exceptions should not be ignored
                    catch (Exception ex)
#pragma warning disable S108 // Nested blocks of code should not be left empty
                    {

                    }
#pragma warning restore S108 // Nested blocks of code should not be left empty
#pragma warning restore S2486 // Generic exceptions should not be ignored

                });

                //listKsonKeys.ForEach(

                if (countOk != 0 && countTotal != 0)
                    return countOk == countTotal;

            }
            return false;
        }

        public bool SuportItem(string jsonKeys, ParamsIntegrateErp configErp)
        {
            if (Enum.TryParse(configErp.DllType, out ErpsTypes dllType))
            {
                return DataAccesErpContract.SupportItemWorkOrder(jsonKeys, dllType, new ParamsContract()
                {
                    ConectionStringErp = configErp.ConectionStringErp,
                    ConectionStringSystime = configErp.ConectionStringToSystime,
                    Language = configErp.LanguageDb,
                    SupportShop = configErp.IdShopsErpArray
                });  
            }
            return false;
        }
    }
}
