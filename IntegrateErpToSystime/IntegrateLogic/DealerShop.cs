using ErpDataAccessFromSystime.Contract;
using ErpDataAccessFromSystime.Contract.ErpDataAccessFromSystime.Contract;
using IntegrateErpToSystime.ModelIntegrate;
using ServicesAccessUbicar.cs.Contract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using SystimeDataAcces.DataAccess;
using static ErpDataAccessFromSystime.TypesErpConfig;
using static Models.ResponseServices.MessageError.CodeResponseServices;

namespace IntegrateErpToSystime.IntegrateLogic
{
    public class DealerShop<T> : IIntegrate<T> where T : DealerShopIntegrate
    {
        private readonly IDataAccesErpContract DataAccesErpContract;
        private readonly IServiceClientUbicar ServiceUbicar;


        public DealerShop()
        {
            DataAccesErpContract = DependencyFactory.Resolve<IDataAccesErpContract>();
            ServiceUbicar = DependencyFactory.Resolve<IServiceClientUbicar>();
        }

        T IIntegrate<T>.Save(string jsonKeys, ParamsIntegrateErp configErp, List<Object> parameterAditional)
        {
            DealerShopIntegrate dealerShopIntegrate = null;
            CloudCatalogDealerShops dealerShop = null;
            if (Enum.TryParse(configErp.DllType, out ErpsTypes dllType))
            {
                dealerShop = DataAccesErpContract.GetDealerShop(jsonKeys, dllType, configErp.ConfigParamsToErp);
                if (dealerShop != null)
                    dealerShop = new DealerShopsSystime(configErp.ConectionStringToSystime).SaveDealerShop(dealerShop);
            }

            if (dealerShop != null)
            {
                dealerShopIntegrate = new DealerShopIntegrate()
                {
                    DealerShopSystime = dealerShop
                };
                if (configErp.SaveInAzure)
                    dealerShopIntegrate.DealerShopUbicar = SaveInAzure(dealerShop, configErp);

                return (T)Convert.ChangeType(dealerShopIntegrate, typeof(T));
            }
            return default(T);
        }


        public Models.DealerShop SaveInAzure(CloudCatalogDealerShops dealerShop, ParamsIntegrateErp configErp)
        {
            ServiceUbicar.Save(new Models.DealerShop()
            {
                IdCity = dealerShop?.IdCity,
                IdDealer = configErp.IdDealerUbicarService,
                IdErpShop = dealerShop?.IdDealerShop,
                Email = dealerShop?.Email,
                Latitude = dealerShop?.Latitude,
                Longitude = dealerShop?.Longitude,
                Address = dealerShop?.Address,
                Mobile = dealerShop?.Mobile,
                Phone = dealerShop?.Phone,
                FullName = dealerShop?.Shop
            }, configErp.ReturnModelService, configErp.UrlService, configErp.LanguageDb, configErp.Token);

            if (ServiceUbicar.CodErrorRequest().HasValue && (ServiceUbicar.CodErrorRequest().Value == TypeErrorWebServicess.OperationOk.GetHashCode() || ServiceUbicar.CodErrorRequest().Value == TypeErrorWebServicess.NotDataFromUpdate.GetHashCode()))
                return ServiceUbicar.CastObjectResponse<Models.DealerShop>();

            return null;
        }

        public bool SaveAll(ParamsIntegrateErp configErp, DateTime dateTimeMin)
        {
            throw new NotImplementedException();
        }

        public bool SuportItem(string jsonKeys, ParamsIntegrateErp configErp)
        {
            throw new NotImplementedException();
        }
    }
}
