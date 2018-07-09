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
    public class InsuranceCompany<T> : IIntegrate<T> where T : InsuranceCompanyIntegrate
    {

        private readonly IDataAccesErpContract DataAccesErpContract;
        private readonly IServiceClientUbicar ServiceUbicar;


        public InsuranceCompany()
        {
            DataAccesErpContract = DependencyFactory.Resolve<IDataAccesErpContract>();
            ServiceUbicar = DependencyFactory.Resolve<IServiceClientUbicar>();
        }

        T IIntegrate<T>.Save(string jsonKeys, ParamsIntegrateErp configErp, List<Object> parameterAditional)
        {
            InsuranceCompanyIntegrate insuranceCompanyIntegrate = null;
            CloudCatalogInsuranceCompanies insuranceCompany = null;
            if (Enum.TryParse(configErp.DllType, out ErpsTypes dllType))
            {
                insuranceCompany = DataAccesErpContract.GetInsuranceCompany(jsonKeys, dllType, configErp.ConfigParamsToErp);
                if (insuranceCompany != null)
                    insuranceCompany = new InsuranceCompaniesSystime(configErp.ConectionStringToSystime).SaveInsuranceCompay(insuranceCompany);
            }

            if (insuranceCompany != null)
            {
                insuranceCompanyIntegrate = new InsuranceCompanyIntegrate()
                {
                    InsuranceCompnaySystime = insuranceCompany
                };

                if (configErp.SaveInAzure)
                    insuranceCompanyIntegrate.InsuranceCompnayUbicar = SaveInAzure(insuranceCompany, configErp);
                return (T)Convert.ChangeType(insuranceCompanyIntegrate, typeof(T));
            }
            return default(T);
        }


        public Models.InsuranceCompany SaveInAzure(CloudCatalogInsuranceCompanies insuranceCompany, ParamsIntegrateErp configErp)
        {
            ServiceUbicar.Save(new Models.InsuranceCompany()
            {
                IdCity = insuranceCompany?.IdCity,
                IdCountry = insuranceCompany?.IdCountry,
                Email = insuranceCompany?.Email,
                Phone = insuranceCompany?.Phone,
                Phone2 = insuranceCompany?.Mobile,
                Address = insuranceCompany?.Address,
                FullName = insuranceCompany?.InsuranceCompany,
                Tin = insuranceCompany?.TIN,
                WebSite = insuranceCompany?.Website
            }, configErp.ReturnModelService, configErp.UrlService, configErp.LanguageDb, configErp.Token);

            if (ServiceUbicar.CodErrorRequest().HasValue && (ServiceUbicar.CodErrorRequest().Value == TypeErrorWebServicess.OperationOk.GetHashCode() || ServiceUbicar.CodErrorRequest().Value == TypeErrorWebServicess.NotDataFromUpdate.GetHashCode()))
                return ServiceUbicar.CastObjectResponse<Models.InsuranceCompany>();

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
