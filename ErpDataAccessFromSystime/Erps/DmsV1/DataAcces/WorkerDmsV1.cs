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
{ /// <summary>
  /// Clase ncargada de manejar la logica para dealer representative erp
  /// </summary>
    internal class WorkerDmsV1
    {


        private ParamsContract ParamsContract { get; set; }
        private String ConectionStringErp { get; set; }


        /// <summary>
        /// Constructor de la clase
        /// </summary>
        internal WorkerDmsV1(ParamsContract paramsContract)
        {
            ParamsContract = paramsContract;
            ConectionStringErp = paramsContract?.ConectionStringErp;
        }


        /// <summary>
        /// Metoto encargado de consutlar el dealerRepresentative en el erp 
        /// </summary>
        /// <param name="idDealer"></param>
        /// <param name="idDealerShop"></param>
        /// <returns></returns>
        internal Workers GetWorker(String jsonKeys, out List<Tuple<Type, String>> paramsAditional)
        {
            paramsAditional = null;
            String DealerShop = null;
            Boolean isDealerRepresentatives = false;
            String activityWorker = "";
            Boolean? isActive = true;
            WorkerKeysDmsV1 workerKeys = JsonConvert.DeserializeObject<WorkerKeysDmsV1>(jsonKeys);
            if (workerKeys!= null && !String.IsNullOrEmpty(workerKeys?.IdWorker))
            {
                using (DmsV1Entities DataBase = new DmsV1Entities(ConectionStringErp))
                {
                    DataBase.Database.CommandTimeout = 1000;
                    if (int.TryParse(workerKeys.IdWorker, out int id))
                    {
                        tall_operarios operarioErp = DataBase.tall_operarios.FirstOrDefault(w => w.nit == id);
                        terceros workerErp = null;
                        if (operarioErp != null)
                        {
                            activityWorker = operarioErp.actividad;
                            isActive = operarioErp.activo;
                            //Si esto ocurre es operario
                            workerErp = operarioErp.terceros;
                            DealerShop = operarioErp.bodega?.ToString();
                        }
                        else
                        {
                            isDealerRepresentatives = true;
                            workerErp = DataBase.terceros.FirstOrDefault(w => w.nit == id);
                        }


                        if (String.IsNullOrEmpty(DealerShop))
                            DealerShop = workerKeys.IdDealerShopWorkOrder;

                        if (workerErp != null)
                            return ParseWorker(workerErp, DealerShop, isDealerRepresentatives, activityWorker, out paramsAditional,isActive);
                    }
                }

            }
            return null;
        }

        /// <summary>
        /// Metoto encarghado de convertir el dealer representative erp
        /// to dealer representative Systime
        /// </summary>
        /// <param name="workerErp"></param>
        /// <param name="idDealerShop"></param>
        /// <returns></returns>
        internal Workers ParseWorker(terceros workerErp, String idDealerShop, Boolean isDEalerRepresentative, String activityWorker, out List<Tuple<Type, String>> paramsAditional, Boolean? isActive = true)
        {
            paramsAditional = null;
            if (workerErp != null)
            {
                String idJobTitle = new ToolsDmsV1(ParamsContract).AjustJobTitle(activityWorker, isDEalerRepresentative);

                String[] phones = new ToolsDmsV1(ParamsContract).AjustPhone(workerErp.telefono_1, workerErp.telefono_2);
                 Workers worker = new Workers()
                {
                    //Ajusta la tienda en systime 
                    IdWorker = new ToolsDmsV1(ParamsContract).AjusWorker(workerErp.nit.ToString()),
                    FullName = workerErp.nombres,
                    Mobile = phones[0],
                    IdDealerShop = idDealerShop,
                    Phone = phones[1],
                    Email = workerErp.mail,
                    IdCity = new ToolsDmsV1(ParamsContract).AjusCity(workerErp.y_ciudad, workerErp.y_dpto, workerErp.y_pais),
                    Address = workerErp.direccion,
                    //TODO :  se debe validar la activacion 
                    Active = true

                };

                paramsAditional = new List<Tuple<Type, string>>
                {
                    new Tuple<Type, string>(typeof(CloudCatalogDealerShops), JsonConvert.SerializeObject(new DelaerShopKeysDmsV1(){ IdShop =idDealerShop }))
                };
                return worker;

            }
            return null;
        }


       
    }

    public class WorkerKeysDmsV1
    {
        public String IdWorker { get; set; }

        public String IdDealerShopWorkOrder { get; set; }
    }
}
