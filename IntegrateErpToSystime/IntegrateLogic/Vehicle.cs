using ErpDataAccessFromSystime.Contract;
using ErpDataAccessFromSystime.Contract.ErpDataAccessFromSystime.Contract;
using IntegrateErpToSystime.ModelIntegrate;
using ServicesAccessUbicar.cs;
using ServicesAccessUbicar.cs.Contract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using SystimeDataAcces.DataAccess;
using Utils.Enums;
using Utils.Operator;
using static ErpDataAccessFromSystime.TypesErpConfig;
using static Models.ResponseServices.MessageError.CodeResponseServices;

namespace IntegrateErpToSystime.IntegrateLogic
{
    public class Vehicle<T> : IIntegrate<T> where T : VehicleIntegrate
    {
        private readonly IDataAccesErpContract DataAccesErpContract;
        private readonly IServiceClientUbicar ServiceUbicar;

        public Vehicle()
        {
            DataAccesErpContract = DependencyFactory.Resolve<IDataAccesErpContract>();
            ServiceUbicar = DependencyFactory.Resolve<IServiceClientUbicar>();
        }


        T IIntegrate<T>.Save(string jsonKeys, ParamsIntegrateErp configErp, List<Object> parameterAditional)
        {
            VehicleIntegrate vehicleIntegrate = null;
            Vehicles vehicles = null;
            CustomerIntegrate customerParam = null;
            InsuranceCompanyIntegrate integrateinsuranceCompany = null;
            InsuranceCompanyIntegrate insuranceCompanyParam = null;
            int? mileageParam= null;
            if (Enum.TryParse(configErp.DllType, out ErpsTypes dllType))
            {
                vehicles = DataAccesErpContract.GetVehicle(jsonKeys, dllType, configErp.ConfigParamsToErp, out List<Tuple<Type, string>> listOtherAccionPreIntegrate);
         

                if(vehicles != null)
                {
                    integrateinsuranceCompany = UtilsIIntegrateErpToSystime.IntegrateOtherTypes<InsuranceCompanyIntegrate>(listOtherAccionPreIntegrate, configErp,null);
                    insuranceCompanyParam = UtilsIIntegrateErpToSystime.ResolveTypeParameter<InsuranceCompanyIntegrate>(parameterAditional);
                    customerParam = UtilsIIntegrateErpToSystime.ResolveTypeParameter<CustomerIntegrate>(parameterAditional);
                    mileageParam = UtilsIIntegrateErpToSystime.ResolveTypeParameter<int>(parameterAditional);

                    if (customerParam?.CustomerSystime?.IdCustomer > 0 && customerParam?.CustomerSystime?.IdCustomer != vehicles.IdCustomerOwner)
                        vehicles.IdCustomerOwner = customerParam.CustomerSystime.IdCustomer;

                    if (mileageParam.HasValue && mileageParam.Value > vehicles.LastMileage)
                        vehicles.LastMileage = mileageParam.Value;

                    if (insuranceCompanyParam?.InsuranceCompnaySystime?.IdInsuranceCompany != null && insuranceCompanyParam?.InsuranceCompnaySystime?.IdInsuranceCompany != integrateinsuranceCompany?.InsuranceCompnaySystime?.IdInsuranceCompany)
                        integrateinsuranceCompany = insuranceCompanyParam;

                    vehicles.IdInsuranceCompany = integrateinsuranceCompany?.InsuranceCompnaySystime.IdInsuranceCompany;

                    vehicles = new VehiclesSystime(configErp.ConectionStringToSystime).SaveVehicle(vehicles, configErp.UserModify);

                    if(vehicles != null )
                    {
                        vehicleIntegrate = new VehicleIntegrate()
                        {
                            VehicleSystime = vehicles
                        };
                        if (configErp.SaveInAzure)
                        {
                            List<Object> modelsParams = new List<object>();
                            if (insuranceCompanyParam?.InsuranceCompnayUbicar != null)
                                modelsParams.Add(insuranceCompanyParam?.InsuranceCompnayUbicar);
                            if(customerParam?.CustomerUbicar != null)
                                modelsParams.Add(customerParam?.CustomerUbicar);

                            vehicleIntegrate.VehicleUbicar = SaveInAzure(vehicles, modelsParams, configErp);
                        }                
                    };                   
                }
            }

            if (vehicleIntegrate != null)
                return (T)Convert.ChangeType(vehicleIntegrate, typeof(T));

            return default(T);
        }



        public Models.Vehicle SaveInAzure(Vehicles vehicle,List<Object> paramsService, ParamsIntegrateErp configErp)
        {
            ServiceUbicar.Save(new Models.Vehicle()
            {
                IdDealer = configErp.IdDealerUbicarService,
                IdCity = vehicle?.IdCity,
                IdCountry = vehicle?.IdCountry,
                IdVehicleModel = vehicle?.IdVehicleModel,
                IdInsuranceCompany = UtilsIIntegrateErpToSystime.ResolveTypeParameter<Models.InsuranceCompany>(paramsService)?.IdInsuranceCompany ?? ConstansServiceUbicar.ID_DEFAULT_INSURANCECOMPANYCLOUD,
                IdVehicleServiceType = vehicle?.IdVehicleServiceType,
                Vin = String.IsNullOrEmpty(vehicle?.IdVinNumber) ? vehicle?.Plate : vehicle?.IdVinNumber,
                IdVehicleType = vehicle?.IdVehicleType,
                ModelYear = vehicle?.ModelYear,
                Plate = vehicle?.Plate
            }, configErp.ReturnModelService, configErp.UrlService, configErp.LanguageDb, configErp.Token);

            if (ServiceUbicar.CodErrorRequest().HasValue && (ServiceUbicar.CodErrorRequest().Value == TypeErrorWebServicess.OperationOk.GetHashCode() || ServiceUbicar.CodErrorRequest().Value == TypeErrorWebServicess.NotDataFromUpdate.GetHashCode()))
                return ServiceUbicar.CastObjectResponse<Models.Vehicle>();

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
