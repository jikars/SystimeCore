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

    /// <summary>
    /// Clase encargada de manejar la logica de vehiculos contra el erp
    /// </summary>
    internal class VehiclesDmsV1
    {


        private ParamsContract ParamsContract { get; set; }
        private String ConectionStringErp { get; set; }

        /// <summary>
        /// Constructor de la clase
        /// </summary>
        internal VehiclesDmsV1(ParamsContract paramsContract)
        {
            ParamsContract = paramsContract;
            ConectionStringErp = paramsContract?.ConectionStringErp;
        }


        /// <summary>
        /// Metoto encargado de consultar un vehiculo en el erp
        /// </summary>
        /// <param name="idVinVehicle"></param>
        /// <returns></returns>
        internal Vehicles GetVehicle(String jsonKeys, out List<Tuple<Type, string>> paramsAditional)
        {
            paramsAditional = null;
            VehiclesKeysDmsV1 vehicle = JsonConvert.DeserializeObject<VehiclesKeysDmsV1>(jsonKeys);

            if (vehicle != null && !String.IsNullOrEmpty(vehicle?.VinNumber))
            {
                using (DmsV1Entities DataBase = new DmsV1Entities(ConectionStringErp))
                {
                    DataBase.Database.CommandTimeout = 1000;
                    referencias_imp vehicleErp = DataBase.referencias_imp.FirstOrDefault(v => v.codigo == vehicle.VinNumber);
                    if (vehicleErp != null)
                        return ParseVehicle(vehicleErp, out paramsAditional);
                }

            }
            return null;
        }

        /// <summary>
        /// Metoto encargado de convertir un vehiculo del erp a systime
        /// </summary>
        /// <param name="vehicleErp"></param>
        /// <returns></returns>
        internal Vehicles ParseVehicle(referencias_imp vehicleErp, out List<Tuple<Type, string>> paramsAditional)
        {
            paramsAditional = null;
            if (vehicleErp != null)
            {


                long.TryParse(vehicleErp?.nit_comprador?.ToString() ?? "00", out long idcustomer);
                String modelDealer = String.Empty;

                String countryDescription = null;

                using (DmsV1Entities DataBase = new DmsV1Entities(ConectionStringErp))
                {
                    String paisErp = DataBase.y_ciudades.FirstOrDefault(c => c.ciudad == vehicleErp.ciudad_placa)?.pais?.ToString();
                    countryDescription = DataBase.y_paises.FirstOrDefault(p => p.pais == paisErp)?.descripcion?.ToString();
                    modelDealer = DataBase.vh_modelo_taller.FirstOrDefault(tm => tm.modelo_taller == vehicleErp.modelo_taller)?.descripcion?.ToString();
                }

                paramsAditional = new List<Tuple<Type, string>>
                {
                    new Tuple<Type, string>(typeof(CloudCatalogInsuranceCompanies), JsonConvert.SerializeObject(new InsuranceComnayKeysDmsV1(){ Tin = vehicleErp?.nit_aseguradora?.ToString()})),
                };
                return new Vehicles()
                {
                    IdVinNumber = vehicleErp.serie,
                    Plate = new ToolsDmsV1(ParamsContract).AdjustPlate(vehicleErp.placa),
                    DealerVehicleModel = modelDealer,
                    ModelYear = vehicleErp.modelo_ano == null ? 0 : Convert.ToInt32(vehicleErp.modelo_ano),
                    Color = vehicleErp.color,
                    LastMileage = vehicleErp.kilometraje == null ? 0 : Convert.ToInt32(vehicleErp.kilometraje),
                    IdCity = new ToolsDmsV1(ParamsContract).AjusCity(vehicleErp.ciudad_placa, null, null),
                    IdVehicleModel = new ToolsDmsV1(ParamsContract).AjustModel(vehicleErp.id_modano, modelDealer),
                    DealerCity = vehicleErp.ciudad_placa,
                    IdCustomerOwner = idcustomer,
                    IdCountry = new ToolsDmsV1(ParamsContract).AjusCountrie(countryDescription),
                    SaleGuaranteeAt = vehicleErp.fecha_fin_garantia,
                    GuaranteedSaleDistanceTraveled = vehicleErp.Km_Garantia
                };             
            }
            return null;
        }

    }


    public class VehiclesKeysDmsV1
    {
        public String  VinNumber { get; set; }
    }
}

