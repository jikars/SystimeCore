using ErpDataAccessFromSystime.Contract;
using ErpDataAccessFromSystime.Contract.ErpDataAccessFromSystime.Contract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using SystimeDataAcces;
using SystimeDataAcces.DataAccess;

namespace ErpDataAccessFromSystime.Erps.DmsV1.DataAcces
{

    /// <summary>
    /// Calse singlenton
    /// </summary>
    internal class ToolsDmsV1
    {

        private ParamsContract ParamsContract { get; set; }


        //TODO : worker 0 y woker 00 erp  
        /// <summary>
        /// Constructor privado de la clase
        /// </summary>
        internal ToolsDmsV1(ParamsContract paramsContract)
        {
            ParamsContract = paramsContract;
        }


        /// <summary>
        /// Variable que contiene query para poder encontrar un 
        /// nombre en la tabla de terceros de autostock
        /// </summary>
        private const string FULLNAMEQUERY = "SELECT[nombres] FROM [AutoStok].[dbo].[Terceros] WHERE [nit] = {0}";


        /// <summary>
        /// Ajusta el pais del dealer
        /// </summary>
        /// <param name="dealerCountry"></param>
        /// <returns></returns>
        internal String AjusCountrie(String dealerCountry)
        {
            String idCountrySystime = ConstantsSystimeDataAccess.ID_COUNTRY_UNKONOWN;//valor apra la ciudad desconocida
            using (DmsV1Entities dbErp = new DmsV1Entities(ParamsContract.ConectionStringErp))
            {
                dbErp.Database.CommandTimeout = 1000;
                y_paises countryDealer;

                if (!String.IsNullOrEmpty(dealerCountry))
                {

                    if (int.TryParse(dealerCountry, out int idCityCatalogDms))
                        countryDealer = dbErp.y_paises.FirstOrDefault(p => p.pais == dealerCountry);
                    else
                        countryDealer = dbErp.y_paises.FirstOrDefault(p => p.descripcion == dealerCountry);

                    if (!String.IsNullOrEmpty(countryDealer?.descripcion))
                    {
                        using (SystimedbEntities dbSystime = new SystimedbEntities(ParamsContract.ConectionStringSystime))
                        {
                            List<String> countries = dbSystime.AjustCountry(ParamsContract.Language, countryDealer.descripcion).ToList();
                            if (countries?.Count == 1)
                                idCountrySystime = countries[0];
                        }
                    }
                }
                return idCountrySystime;
            }
        }

        /// <summary>
        /// Metoto encargado de ajustar el trabajador para systime
        /// </summary>
        /// <param name="v"></param>
        /// <returns></returns>
        internal string AjusWorker(string idWorkerErp)
        {
            if (idWorkerErp.ToUpper().Trim() == ConstantsSystimeDataAccess.WORKER_ERP_NOT_ASSIGNED.Trim() || idWorkerErp.ToUpper().Trim() == ConstantsSystimeDataAccess.WORKER_ERP_NOT_ASSIGNED_2.Trim())
            {
                return ConstantsSystimeDataAccess.ID_WORKER_SYSTIME_DEFAULT;
            }
            return idWorkerErp;
        }

        /// <summary>
        /// Metodo encargado de ajustar el jobtitle 
        /// </summary>
        /// <param name="actividad_cree"></param>
        /// <returns></returns>
        internal string AjustJobTitle(string actividad_cree, Boolean isDealerRepresentative)
        {
            if (isDealerRepresentative)
                return ConstantsSystimeDataAccess.DEALER_REPRESENTATIVE_DEGAULT_JOB_TITLE;

            if (!String.IsNullOrEmpty(actividad_cree))
            {
                SystimedbEntities systimedb = new SystimedbEntities(ParamsContract.ConectionStringSystime);
                CatalogJobTitleByDealer jobtitle = systimedb.CatalogJobTitleByDealer.FirstOrDefault(j => j.IdJobTitle.ToUpper() == actividad_cree.ToUpper());
                if (jobtitle != null)
                    return jobtitle.IdJobTitle;
                systimedb.Dispose();
            }
            return ConstantsSystimeDataAccess.ID_JOB_TITLE_DEFAULT;
        }

        /// <summary>
        /// Metoto encargado de ajustar la compañia de seugros que proviene 
        /// desde el erp
        /// </summary>
        /// <param name="v"></param>
        /// <param name="isInsured"></param>
        /// <returns></returns>
        internal String AjustInsuranceCompany(String idnsuranceCompanyErp, out bool isInsured)
        {
            isInsured = true;
            if (!String.IsNullOrEmpty(idnsuranceCompanyErp))
            {
                if (idnsuranceCompanyErp.ToUpper() == ConstantsSystimeDataAccess.ID_INSURANCE_COMPANY_IS_INSURANCE_ERP.ToString().ToUpper())
                {
                    isInsured = false;
                    idnsuranceCompanyErp = null;
                }
            }
            else
                idnsuranceCompanyErp = null;

            return idnsuranceCompanyErp;
        }




        /// <summary>
        /// Metoto encargado de determinar cual es el tefono movil y celular 
        /// en la posicion uno devuelve el telofono celular y en la posicion 2 devuelve el telefono movil 
        /// </summary>
        /// <param name="phone1"></param>
        /// <param name="phone2"></param>
        /// <returns></returns>
        internal String[] AjustPhone(string phone1, string phone2)
        {
            List<string> extencions = new List<string> { "EXTENCIÓN","EXT","EXTENSION","EXT.","#","EXTENCION","EXTN","-","_","EX","X","E"};
            phone1 = phone1?.Replace(" ","");
            phone2 = phone2?.Replace(" ", "");

            String[] phones = new String[2];
            int contExt = 0;
            if (!String.IsNullOrEmpty(phone1))
            {
                if (IsMovilPhone(phone1))
                    phones[0] = phone1;
                else
                {
                    extencions.ForEach(e =>
                    {
                        if (phone1.ToUpper().Contains(e))
                            contExt++;

                        if (contExt == 1)
                            phone1 = phone1.ToUpper().Replace(e, ",");
                        else
                            phone1 = phone1.ToUpper().Replace(e, "");
                    });
                    phones[1] = phone1;
                }
            }

            contExt = 0;
            if (!String.IsNullOrEmpty(phone2) && phone2 != phone1)
            {
                if (IsMovilPhone(phone2) && String.IsNullOrEmpty(phones[0]))
                    phones[0] = phone2;
                else if (String.IsNullOrEmpty(phones[1]))
                {
                    extencions.ForEach(e =>
                    {
                        if (phone2.ToUpper().Contains(e))
                            contExt++;

                        if (contExt == 1)
                            phone2 = phone2.ToUpper().Replace(e, ",");
                        else
                            phone2 = phone2.ToUpper().Replace(e, "");
                    });
                    phones[1] = phone2;
                }
            }

            return phones;
        }

        /// <summary>
        /// Metoto encargado de validar si un telefono tiene formato movil o no
        /// </summary>
        /// <param name="phone"></param>
        /// <returns></returns>
        private Boolean IsMovilPhone(String phone)
        {
            if (!String.IsNullOrEmpty(phone))
            {
                //TODO : este filtro debe tener mas detalle cuando se hace por pais por que el coidgo de pais afecta el telefono por ahora funciona para dmsv1 colombia
                phone = phone.TrimEnd().TrimStart().Replace(" ", "");
                if (phone.Length == 10)
                {
                    return true;
                }
            }
            return false;
        }

        /// <summary>
        /// MEtoto para ajustar el id dealer shop
        /// </summary>
        /// <param name="idDealerShopErp"></param>
        /// <returns></returns>
        internal String AjustDealerShop(String idDealerShopErp)
        {
            throw new ArgumentNullException("idDealerShopErp", "not implement");
        }

        /// <summary>
        /// Ajusta la ciduad del dealer a la ciudad de tipo sytime si coninciden en la informacion
        /// </summary>
        /// <param name="dealerCountry"></param>
        /// <returns></returns>
        internal int AjusCity(String idCityDealer, String idDealerState, String idDealerCountrie)
        {
            int idCitySystime = ConstantsSystimeDataAccess.ID_CITY_UNKOWN;//valor apra la ciudad desconocida
            using (DmsV1Entities dbErp = new DmsV1Entities(ParamsContract.ConectionStringErp))
            {
                dbErp.Database.CommandTimeout = 1000;

                y_ciudades cityDealer = null;
                y_paises countrieDealer = null;
                y_departamentos stateDealer = null;

                if (!String.IsNullOrEmpty(idCityDealer))
                {
                    ///Ajusta el paise
                    if (!String.IsNullOrEmpty(idDealerCountrie))
                    {
                        if ((int.TryParse(idDealerCountrie, out int idCountry)))
                            countrieDealer = dbErp.y_paises.FirstOrDefault(p => p.pais == idDealerCountrie);
                        else
                            countrieDealer = dbErp.y_paises.FirstOrDefault(p => p.descripcion.Contains(idDealerCountrie));
                    }

                    ///Ajusta el departamento
                    if (!String.IsNullOrEmpty(idDealerState))
                    {
                        if ((int.TryParse(idDealerState, out int idState)) && countrieDealer != null)
                            stateDealer = dbErp.y_departamentos.FirstOrDefault(p => p.departamento == idDealerState && p.pais == countrieDealer.pais);
                        else if (countrieDealer != null)
                            stateDealer = dbErp.y_departamentos.FirstOrDefault(p => p.descripcion.Contains(idDealerState) && p.pais == countrieDealer.pais);


                        //Busca el valor asi no se halla encontrado su integrisdad refeesncial
                        if (stateDealer == null)
                            if ((int.TryParse(idDealerState, out int idStates)))
                                stateDealer = dbErp.y_departamentos.FirstOrDefault(p => p.departamento == idDealerState);
                            else
                                stateDealer = dbErp.y_departamentos.FirstOrDefault(p => p.descripcion.Contains(idDealerState));
                    }

                    if (int.TryParse(idCityDealer, out int idCityCatalogDms))
                    {
                        if (countrieDealer != null)
                            if (stateDealer != null)
                                cityDealer = dbErp.y_ciudades.FirstOrDefault(p => p.ciudad == idCityDealer && p.departamento == stateDealer.departamento && p.pais == countrieDealer.pais);
                            else
                                cityDealer = dbErp.y_ciudades.FirstOrDefault(p => p.ciudad == idCityDealer && p.pais == countrieDealer.pais);
                        else if (stateDealer != null)
                            cityDealer = dbErp.y_ciudades.FirstOrDefault(p => p.ciudad == idCityDealer && p.departamento == stateDealer.departamento);
                    }
                    else
                    {
                        if (countrieDealer != null)
                            if (stateDealer != null)
                                cityDealer = dbErp.y_ciudades.FirstOrDefault(p => p.descripcion.Contains(idCityDealer) && p.departamento == stateDealer.departamento && p.pais == countrieDealer.pais);
                            else
                                cityDealer = dbErp.y_ciudades.FirstOrDefault(p => p.descripcion.Contains(idCityDealer) && p.pais == countrieDealer.pais);
                        else if (stateDealer != null)
                            cityDealer = dbErp.y_ciudades.FirstOrDefault(p => p.descripcion.Contains(idCityDealer) && p.departamento == stateDealer.departamento);
                    }

                    //REajusta en caso que el valor no se halla encontrado segun la integridad referncial
                    if (cityDealer == null)
                    {
                        if (int.TryParse(idCityDealer, out int idCityCatalogDm))
                            cityDealer = dbErp.y_ciudades.FirstOrDefault(p => p.ciudad == idCityDealer);
                        else
                            cityDealer = dbErp.y_ciudades.FirstOrDefault(p => p.descripcion.Contains(idCityDealer));
                    }


                    if (cityDealer != null)
                    {
                        String DealerCityDescription = cityDealer.descripcion;
                        if (!String.IsNullOrEmpty(cityDealer.descripcion.Trim()))
                        {
                            using (SystimedbEntities dbSystime = new SystimedbEntities(ParamsContract.ConectionStringSystime))
                            {
                                List<String> cities = dbSystime.AjustCity(FormatStringFromDatabase(DealerCityDescription)).ToList();
                                if (cities != null)
                                {
                                    if (cities.Count == 1)
                                    {
                                        if (int.TryParse(cities[0], out int id))
                                            idCitySystime = id;
                                    }
                                    else if (cities.Count > 1)
                                    {
                                        //Ha respondicio mucho entonces debe de seguir filtando 
                                    }
                                }
                            }
                        }
                    }
                }
            }
            return idCitySystime;
        }




        /// <summary>
        /// Metodo para ajustar el codigo de de modelo del vehiculo tambien crea en caso de no exister 
        /// la marca y el modelo 
        /// </summary>
        /// <param name="delaerModel">dealer model</param>
        /// <returns>id modelo del vehiculo segun los catalogos</returns>
        internal int AjustModel(String idModelYmodel, String tallModel)
        {
            int? idVehicleModel = ConstantsSystimeDataAccess.ID_VEHICLE_MODEL_UNKONOWN;
            using (DmsV1Entities dbErp = new DmsV1Entities(ParamsContract.ConectionStringErp))
            {
                dbErp.Database.CommandTimeout = 1000;
                idModelYmodel = FormatStringFromDatabase(idModelYmodel)?.Trim().ToUpper();
                tallModel = FormatStringFromDatabase(tallModel)?.Trim().ToUpper();
                using (SystimedbEntities dbSystime = new SystimedbEntities(ParamsContract.ConectionStringSystime))
                {
                    if (!String.IsNullOrEmpty(tallModel))
                    {
                        CloudCatalogVehicleBrands brandsVehiclesSystime = dbSystime.CloudCatalogVehicleBrands.FirstOrDefault(w => tallModel.ToUpper().Contains(w.VehicleBrand.ToUpper()));
                        if (brandsVehiclesSystime != null)
                        {
                            tallModel = tallModel.ToUpper().Replace(brandsVehiclesSystime.VehicleBrand.ToUpper(), "").ToUpper();
                            if (brandsVehiclesSystime.CloudCatalogVehicleModels != null)
                            {
                                CloudCatalogVehicleModels vehicleModel = brandsVehiclesSystime.CloudCatalogVehicleModels.FirstOrDefault(m => tallModel.ToUpper().Contains(m.VehicleModel.ToUpper()));
                                if (vehicleModel != null)
                                    idVehicleModel = vehicleModel.IdVehicleModel;
                            }
                        }
                    }

                    if (!String.IsNullOrEmpty(idModelYmodel) && /*(*/idVehicleModel == ConstantsSystimeDataAccess.ID_VEHICLE_MODEL_UNKONOWN /*|| idVehicleModel == null)*/ )
                    {
                        vh_modelo_ano modelYearDealer = dbErp.vh_modelo_ano.FirstOrDefault(my => my.id_modano.ToUpper() == idModelYmodel);

                        if (modelYearDealer?.vh_modelo?.vh_marcas != null)
                        {
                            List<AjustVehicleBrands_Result> vehicleBrands = dbSystime.AjustVehicleBrands(FormatStringFromDatabase(modelYearDealer.vh_modelo.vh_marcas.descripcion)).ToList();
                            if (vehicleBrands != null)
                            {
                                if (vehicleBrands.Count == 1)
                                {
                                    String vehicleBrandSearch = vehicleBrands[0].IdVehicleBrand;
                                    List<CloudCatalogVehicleModels> vehiclesModelsbtBrand = dbSystime.CloudCatalogVehicleModels.Where(m => m.IdVehicleBrand == vehicleBrandSearch).ToList();
                                    String modelSearch = FormatStringFromDatabase(modelYearDealer.vh_modelo.descripcion);
                                    if (vehiclesModelsbtBrand != null)
                                    {
                                        CloudCatalogVehicleModels vehicleModel = vehiclesModelsbtBrand.FirstOrDefault(m => modelSearch.Contains(FormatStringFromDatabase(m.VehicleModel).ToUpper()));
                                        if (vehicleModel != null)
                                            idVehicleModel = vehicleModel.IdVehicleModel;

                                    }
                                }
                                else if (vehicleBrands.Count > 1)
                                {
                                    //Se debe contnuar con el filtro 
                                }
                            }
                        }
                    }
                }
            }
            return (int)idVehicleModel;
        }


        /// <summary>
        /// Metodo para ajustar la placa del vehiculo
        /// </summary>
        /// <param name="dealerPlate">dealer plate</param>
        /// <returns>dealer plate ajuste ajustado</returns>
        internal string AdjustPlate(String dealerPlate)
        {
            if (!String.IsNullOrEmpty(dealerPlate))
            {
                Regex reg = new Regex("[^a-zA-Z]");
                string letras = reg.Replace(dealerPlate.Normalize(NormalizationForm.FormD), "");
                if (letras.Length > 3) letras = letras.Substring(0, 3);

                reg = new Regex("[^0-9]");
                string numeros = reg.Replace(dealerPlate.Normalize(NormalizationForm.FormD), "");
                if (numeros.Length > 3) numeros = numeros.Substring(0, 3);

                string allPlate = letras + numeros;
                return allPlate.ToUpper();
            }
            return dealerPlate;
        }


        /// <summary>
        /// Metoto para obtener el nombre completo 
        /// a patar id del customer
        /// </summary>
        /// <param name="IdCustomer">Id customer</param>
        /// <returns>nomrbe completo si lo encuentra si no el mismo id  customer</returns>
        internal String GetFullName(String IdCustomer)
        {
            String names = "";
            using (DmsV1Entities dbErp = new DmsV1Entities(ParamsContract.ConectionStringErp))
            {
                dbErp.Database.CommandTimeout = 1000;
                if (!String.IsNullOrEmpty(IdCustomer))
                    names = dbErp.Database.SqlQuery<String>(FULLNAMEQUERY, IdCustomer).FirstOrDefault();
                if (String.IsNullOrEmpty(names))
                    names = IdCustomer;
            }
            return names;
        }

        /// <summary>
        /// metodo que convierte cualqueir estrin a forma de fecha
        /// </summary>
        /// <param name="date"></param>
        /// <returns></returns>
        internal DateTime? TransformDates(String date)
        {
            if (!String.IsNullOrEmpty(date) && DateTime.TryParse(date.Trim(), out DateTime datetime))

                return datetime;
            return null;
        }


        /// <summary>
        /// Valida si un dato es nulo y lo ajusta 
        /// ya que el dato esta como strin en la ase de datos
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        internal bool ValidateNullText(string value)
        {
            return value?.ToUpper().Replace(" ", "") != "NULO" ? false : true;
        }

        /// <summary>
        /// Metoto para fomatear string 
        /// </summary>
        /// <param name="stringToFormat"></param>
        /// <returns></returns>
        private String FormatStringFromDatabase(String stringToFormat)
        {
            if (!String.IsNullOrEmpty(stringToFormat))
            {
                stringToFormat = RemoveDiacritics(stringToFormat);
                stringToFormat = stringToFormat.ToUpper();
            }
            return stringToFormat;
        }


        /// <summary>
        /// Meoto encargado de remover las tildes
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        private string RemoveDiacritics(String input)
        {
            string stFormD = input.Normalize(NormalizationForm.FormD);
            int len = stFormD.Length;
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < len; i++)
            {
                System.Globalization.UnicodeCategory uc = System.Globalization.CharUnicodeInfo.GetUnicodeCategory(stFormD[i]);
                if (uc != System.Globalization.UnicodeCategory.NonSpacingMark)
                {
                    sb.Append(stFormD[i]);
                }
            }
            return (sb.ToString().Normalize(NormalizationForm.FormC));
        }
    }
}
