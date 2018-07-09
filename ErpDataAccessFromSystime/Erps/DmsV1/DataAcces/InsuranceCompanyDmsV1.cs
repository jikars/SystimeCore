using ErpDataAccessFromSystime.Contract;
using ErpDataAccessFromSystime.Contract.ErpDataAccessFromSystime.Contract;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SystimeDataAcces.DataAccess;

namespace ErpDataAccessFromSystime.Erps.DmsV1.DataAcces
{
    internal class InsuranceCompanyDmsV1
    {
        private ParamsContract ParamsContract { get; set; }
        private String ConectionStringErp { get; set; }


        /// <summary>
        /// Constructor de la clase
        /// </summary>
        internal InsuranceCompanyDmsV1(ParamsContract paramsContract)
        {
            ParamsContract = paramsContract;
            ConectionStringErp = paramsContract?.ConectionStringErp;
        }



        /// <summary>
        /// Metoto encargado de obtener la compañia de seguros 
        /// </summary>
        /// <param name="tinInsuranceCompany"></param>
        /// <returns></returns>
        internal CloudCatalogInsuranceCompanies GetInsuranceCompany(String jsonKeys)
        {
            InsuranceComnayKeysDmsV1 InsuranceCompanyKeys = JsonConvert.DeserializeObject<InsuranceComnayKeysDmsV1>(jsonKeys);
            if (InsuranceCompanyKeys != null  && decimal.TryParse(InsuranceCompanyKeys?.Tin, out decimal nit))
            {

                using (DmsV1Entities DataBase = new DmsV1Entities(ConectionStringErp))
                {
                    DataBase.Database.CommandTimeout = 1000;
                    terceros InsuranceCompanyErp = DataBase.terceros.FirstOrDefault(i => i.nit == nit);
                    if (InsuranceCompanyErp != null)
                        return ParsheInsuranceCompany(InsuranceCompanyErp);
                }

            }
            return null;

        }

        /// <summary>
        /// Metoto encargado de convertir una compañia de seguros 
        /// de erp a systime
        /// </summary>
        /// <param name="inSuranceCompanyErp"></param>
        /// <returns></returns>
        internal CloudCatalogInsuranceCompanies ParsheInsuranceCompany(terceros inSuranceCompanyErp)
        {
            if (inSuranceCompanyErp != null)
            {
                String[] phones = new ToolsDmsV1(ParamsContract).AjustPhone(inSuranceCompanyErp.telefono_1, inSuranceCompanyErp.telefono_2);
                CloudCatalogInsuranceCompanies insuranceCompany = new CloudCatalogInsuranceCompanies()
                {
                    TIN = inSuranceCompanyErp.nit.ToString(),
                    InsuranceCompany = inSuranceCompanyErp.nombres,
                    Address = inSuranceCompanyErp.direccion,
                    Website = inSuranceCompanyErp.paginaweb,
                    Mobile = phones[0],
                    Phone = phones[1],
                    Email = inSuranceCompanyErp.email2,
                    IdCity = new ToolsDmsV1(ParamsContract).AjusCity(inSuranceCompanyErp.y_ciudad, inSuranceCompanyErp.y_dpto, inSuranceCompanyErp.y_pais),
                    IdCountry = new ToolsDmsV1(ParamsContract).AjusCountrie(inSuranceCompanyErp.y_pais),
                };
                return insuranceCompany;
            }
            return null;
        }
    }

    public class InsuranceComnayKeysDmsV1
    {
        public String Tin { get; set; }
    }
}