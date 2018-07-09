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
    public class Customer<T> : IIntegrate<T> where T : CustomerIntegrate
    {

        private readonly IDataAccesErpContract DataAccesErpContract;
        private readonly IServiceClientUbicar ServiceUbicar;


        public Customer()
        {
            DataAccesErpContract = DependencyFactory.Resolve<IDataAccesErpContract>();
            ServiceUbicar = DependencyFactory.Resolve<IServiceClientUbicar>();
        }
        T IIntegrate<T>.Save(string jsonKeys, ParamsIntegrateErp configErp, List<Object> parameterAditional)
        {
            CustomerIntegrate customerIntegrate = null;
            Customers customer = null;
            if (Enum.TryParse(configErp.DllType, out ErpsTypes dllType))
            {
                customer = DataAccesErpContract.GetCustomer(jsonKeys, dllType, configErp.ConfigParamsToErp);
                if (customer != null)
                    customer = new CustomersSystime(configErp.ConectionStringToSystime).SaveCustomer(customer, configErp.UserModify);
            }

            if (customer != null)
            {
                customerIntegrate = new CustomerIntegrate()
                {
                    CustomerSystime = customer
                };
                if (configErp.SaveInAzure)
                    customerIntegrate.CustomerUbicar = SaveAzure(customer, configErp);

                return (T)Convert.ChangeType(customerIntegrate, typeof(T));
            }

            return default(T);
        }

        public Models.Person SaveAzure(Customers customers, ParamsIntegrateErp configErp)
        {        
            ServiceUbicar.Save(new Models.Person()
            {
                IdCity = customers?.IdCity,
                IdCountry = customers?.IdCountry,
                Email = customers?.Email,
                Address = customers?.Address,
                FullName = customers?.FullName,
                IdentityNumber = customers?.DocumentNumber
            }, configErp.ReturnModelService, configErp.UrlService,configErp.LanguageDb, configErp.Token);

            if (ServiceUbicar.CodErrorRequest().HasValue && (ServiceUbicar.CodErrorRequest().Value == TypeErrorWebServicess.OperationOk.GetHashCode() || ServiceUbicar.CodErrorRequest().Value == TypeErrorWebServicess.NotDataFromUpdate.GetHashCode()))
                return ServiceUbicar.CastObjectResponse<Models.Person>();

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
