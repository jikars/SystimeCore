using ErpDataAccessFromSystime.Contract;
using IntegrateErpToSystime.IntegrateLogic;
using IntegrateErpToSystime.ModelIntegrate;
using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using SystimeDataAcces.DataAccess;
using Utils.Operator;

namespace IntegrateErpToSystime
{
    internal static class UtilsIIntegrateErpToSystime
    {

        private readonly static List<Tuple<Type,Type, Type>> IntanceFromTypeReturnParameters = null;


        static UtilsIIntegrateErpToSystime()
        {
            if (IntanceFromTypeReturnParameters == null)
            {
                IntanceFromTypeReturnParameters = new List<Tuple<Type, Type, Type>>
                {
                    new Tuple<Type, Type, Type>(typeof(InsuranceCompanyIntegrate), typeof(InsuranceCompany<InsuranceCompanyIntegrate>), typeof(CloudCatalogInsuranceCompanies)),
                    new Tuple<Type, Type, Type>(typeof(VehicleIntegrate), typeof(Vehicle<VehicleIntegrate>), typeof(Vehicles)),
                    new Tuple<Type, Type, Type>(typeof(DealerShopIntegrate), typeof(DealerShop<DealerShopIntegrate>), typeof(CloudCatalogDealerShops)),
                    new Tuple<Type, Type, Type>(typeof(CustomerIntegrate), typeof(Customer<CustomerIntegrate>), typeof(Customers)),
                    new Tuple<Type, Type, Type>(typeof(WorkerIntegrate), typeof(Worker<WorkerIntegrate>), typeof(Workers))
                };
            }
        }



        /// <summary>
        /// MEtto encargado de resolver la intancia de una clase para la integracion
        /// </summary>
        /// <param name="tablename"></param>
        /// <returns></returns>
        internal static Object ResolverIntanceFromParameterType<T>()
        {
            return  Activator.CreateInstance(IntanceFromTypeReturnParameters.FirstOrDefault(t => t.Item1 == typeof(T)).Item2);
        }

        internal static Type SearchTypeFromParameterType<T>()
        {       
           return IntanceFromTypeReturnParameters.FirstOrDefault(t => t.Item1 == typeof(T)).Item3;         
        }

        public static  T IntegrateOtherTypes<T>(List<Tuple<Type, string>> listOtherAccionPreIntegrate, ParamsIntegrateErp configErp, List<Object> listParameters)
        {
            Type typeSearch = SearchTypeFromParameterType<T>();
            String json = null;
            if (typeSearch != null)
            {
                json = listOtherAccionPreIntegrate.FirstOrDefault(t => t.Item1 == typeSearch).Item2;
            }
            if (!String.IsNullOrEmpty(json))
            {
                IIntegrate<T> IIntegrate = (IIntegrate<T>)ResolverIntanceFromParameterType<T>();
                return IIntegrate.Save(json, configErp, listParameters);
            }
            return default(T);
        }

        public static T SearchValueFromType<T>(List<Tuple<Type, string>> listOtherAccionPreIntegrate) where T : IConvertible
        {
           
            String value = listOtherAccionPreIntegrate.FirstOrDefault(t => t.Item1 == typeof(T))?.Item2;
            if (value != null)
                return (T)Convert.ChangeType(value, typeof(T));
            else
                return default(T);
        }

        public static T ResolveTypeParameter<T>(List<Object> listObjectResolve)
        { 
            return (T)listObjectResolve.FirstOrDefault(t => t?.GetType() == typeof(T));
        }


    }
}
