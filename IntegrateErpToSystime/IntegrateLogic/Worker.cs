using ErpDataAccessFromSystime.Contract;
using ErpDataAccessFromSystime.Contract.ErpDataAccessFromSystime.Contract;
using IntegrateErpToSystime.ModelIntegrate;
using ServicesAccessUbicar.cs.Contract;
using System;
using System.Collections.Generic;
using SystimeDataAcces.DataAccess;
using static ErpDataAccessFromSystime.TypesErpConfig;
using static Models.ResponseServices.MessageError.CodeResponseServices;

namespace IntegrateErpToSystime.IntegrateLogic
{
    public class Worker<T> : IIntegrate<T> where T : WorkerIntegrate
    {


        private readonly IDataAccesErpContract DataAccesErpContract;
        private readonly IServiceClientUbicar ServiceUbicar;


        public Worker()
        {
            DataAccesErpContract = DependencyFactory.Resolve<IDataAccesErpContract>();
            ServiceUbicar = DependencyFactory.Resolve<IServiceClientUbicar>();
        }



        public T Save(string jsonKeys, ParamsIntegrateErp configErp, List<object> parameterAditional)
        {
            WorkerIntegrate workerIntegrate = null;
            Workers worker = null;
            DealerShopIntegrate dealerShopIntegrate = null;
            if (Enum.TryParse(configErp.DllType, out ErpsTypes dllType))
            {
                worker = DataAccesErpContract.GetWorker(jsonKeys, dllType, new ParamsContract()
                {
                    ConectionStringErp = configErp.ConectionStringErp,
                    ConectionStringSystime = configErp.ConectionStringToSystime,
                    Language = configErp.LanguageDb,
                    SupportShop = configErp.IdShopsErpArray
                }, out List<Tuple<Type,String>> listOtherAccionPreIntegrate);

                dealerShopIntegrate = UtilsIIntegrateErpToSystime.IntegrateOtherTypes<DealerShopIntegrate>(listOtherAccionPreIntegrate, configErp,null);
                if(dealerShopIntegrate == null)
                    dealerShopIntegrate = UtilsIIntegrateErpToSystime.ResolveTypeParameter<DealerShopIntegrate>(parameterAditional);

                if (worker != null)
                {
                    if (worker?.IdDealerShop != dealerShopIntegrate?.DealerShopSystime?.IdDealerShop)
                        worker.IdDealerShop = dealerShopIntegrate?.DealerShopSystime?.IdDealerShop;
                    worker = new WorkersSystime(configErp.ConectionStringToSystime).SaveWorker(worker, configErp.UserModify);

                    if(worker != null)
                    {
                        workerIntegrate = new WorkerIntegrate()
                        {
                            WokerSystime = worker
                        };
                        if (configErp.SaveInAzure)
                        {
                            List<Object> modelsParams = new List<object>();
                            if (dealerShopIntegrate?.DealerShopUbicar != null)
                                modelsParams.Add(dealerShopIntegrate?.DealerShopUbicar);

                            workerIntegrate.WorkerUbicar = SaveInAzure(worker, modelsParams, configErp);
                        }
                    }                
                }
            }

            if (workerIntegrate != null)
                return (T)Convert.ChangeType(workerIntegrate, typeof(T));

            return default(T);
        }

        public bool SaveAll(ParamsIntegrateErp configErp, DateTime dateTimeMin)
        {
            throw new NotImplementedException();
        }

        public Models.DealerRepresentative SaveInAzure(Workers worker, List<Object> paramsService, ParamsIntegrateErp configErp)
        {
            ServiceUbicar.Save(new Models.DealerRepresentative()
            {
                IdCity = worker?.IdCity,
                IdDealer = configErp.IdDealerUbicarService,
                IdDealerShop = UtilsIIntegrateErpToSystime.ResolveTypeParameter<Models.DealerShop>(paramsService)?.IdDealerShop,
                IdErpRepresentative = worker?.IdWorker,
                IdentityNumber = worker?.IdWorker,
                Email = worker?.Email,
                FullName = worker?.FullName,
                Mobile = worker?.Mobile,
                Phone = worker?.Phone
            }, configErp.ReturnModelService, configErp.UrlService, configErp.LanguageDb, configErp.Token);

            if (ServiceUbicar.CodErrorRequest().HasValue && (ServiceUbicar.CodErrorRequest().Value == TypeErrorWebServicess.OperationOk.GetHashCode() || ServiceUbicar.CodErrorRequest().Value == TypeErrorWebServicess.NotDataFromUpdate.GetHashCode()))
                return ServiceUbicar.CastObjectResponse<Models.DealerRepresentative>();

            return null;
        }

        public bool SuportItem(string jsonKeys, ParamsIntegrateErp configErp)
        {
            throw new NotImplementedException();
        }
    }
}
