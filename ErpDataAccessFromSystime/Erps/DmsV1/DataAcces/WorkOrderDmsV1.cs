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
    /// Clase encargada de la logica de la coneccion para el erp
    /// tipo dll tipo 1
    /// </summary>
    internal class WorkOrderDmsV1 : IDisposable
    {

        private DmsV1Entities DataBase { get; set; }

       
        private ParamsContract ParamsContract { get; set; }

        /// <summary>
        /// Cosntructor de la clase
        /// </summary>
        internal WorkOrderDmsV1(ParamsContract paramsContract)
        {
            ParamsContract = paramsContract;
             DataBase = new DmsV1Entities(paramsContract.ConectionStringErp);
        }

        /// <summary>
        /// De
        /// </summary>
        /// <param name="idWorkOrder"></param>
        /// <returns></returns>
        internal WorkOrders GetWorkOrder(String  jsonKeys, out List<Tuple<Type, String>> paramsAditional)
        {
            paramsAditional = null;
            WorkOrderDmsV1Keys WorkOrderKeys = JsonConvert.DeserializeObject<WorkOrderDmsV1Keys>(jsonKeys);
            if (int.TryParse(WorkOrderKeys?.WorkOrderNumber, out int number) && short.TryParse(WorkOrderKeys?.IdErpShop, out short shop))
            {

                DataBase.Database.CommandTimeout = 10000;
                tall_encabeza_orden worOrderErp = null;
                if (ParamsContract?.SupportShop?.Length > 0)
                {
                    DateTime dateTime = new DateTime(2016, 1, 1);
                    if (ParamsContract.SupportShop.Contains(shop.ToString().ToUpper()))
                        worOrderErp = DataBase.tall_encabeza_orden.Where(w => w.numero == number && w.bodega == shop && w.fecha >= dateTime).OrderByDescending(w => w.fecha).FirstOrDefault();
                }

                if (worOrderErp != null)
                    return ParsheWorkOrder(worOrderErp , out paramsAditional);

            }
            return null;
        }

        internal List<string> GetAllJsonKeysWorkOrder(DateTime dateTimeMin)
        {
            List<String> listWorkOrders = null;
            DataBase.Database.CommandTimeout = 4000;

            List<tall_encabeza_orden> worOrdersErp = DataBase.tall_encabeza_orden.Where(w => ParamsContract.SupportShop.Contains(w.bodega.ToString()) && !w.facturada && w.fecha_hora_entrega_real == null && !w.facturo_deducible && !w.anulada && w.fecha >= dateTimeMin).ToList();

            worOrdersErp?.ForEach(w => {

                if (listWorkOrders == null)
                    listWorkOrders = new List<string>();

                listWorkOrders.Add(JsonConvert.SerializeObject(new WorkOrderDmsV1Keys()
                {
                    IdErpShop = w.bodega.ToString(),
                    WorkOrderNumber = w.numero.ToString()
                }));
            });

            return listWorkOrders;
        }

        internal bool SupporItem(string jsonKeys)
        {
            WorkOrderDmsV1Keys WorkOrderKeys = JsonConvert.DeserializeObject<WorkOrderDmsV1Keys>(jsonKeys);
            return ParamsContract.SupportShop.Contains(WorkOrderKeys.IdErpShop);
        }



        internal bool UpdateAuutizatheAtAndObservations(string idShpo, string workOrderNumber, string observations, DateTime? autorizatheAt)
        {
            if (short.TryParse(idShpo, out short shop) && int.TryParse(workOrderNumber, out int number) && (autorizatheAt.HasValue  ||  !String.IsNullOrEmpty(observations)))
                using (DataBase)
                {
                    tall_encabeza_orden currentEntity = DataBase.tall_encabeza_orden.FirstOrDefault(w=> w.bodega == shop && w.numero == number);

                    if(currentEntity != null)
                    {
                            if (autorizatheAt.HasValue && currentEntity.fecha_hora_autoriza != autorizatheAt)
                                currentEntity.fecha_hora_autoriza = autorizatheAt;

                            if (!String.IsNullOrEmpty(observations) && currentEntity.notas != observations)
                                currentEntity.notas = observations;

                            if (DataBase.Entry(currentEntity).State == System.Data.Entity.EntityState.Modified && DataBase.SaveChanges() > 0)
                                return true;
                     }
                }

            return false;
        }



        /// <summary>
        /// Hace el parceo de la orden de trabajo 
        /// </summary>
        /// <param name="worOrderErp"></param>
        /// <returns></returns>
        private WorkOrders ParsheWorkOrder(tall_encabeza_orden worOrderErp, out List<Tuple<Type, String>> paramsAditional)
        {
            paramsAditional = null;
            WorkOrders workOrder = null;
            if (worOrderErp != null)
            {
                String insuranceCompany = new ToolsDmsV1(ParamsContract).AjustInsuranceCompany(worOrderErp.aseguradora?.ToString(), out Boolean isInsured);
               
                long.TryParse(worOrderErp.nit.ToString(), out long outIdCustomer);
                workOrder = new WorkOrders()
                {
                    WorkOrderNumber = worOrderErp.numero.ToString(),
                    IdDealerShop = worOrderErp.bodega.ToString(),
                    IdVinNumber = worOrderErp.serie,
                    OpenOtAt = worOrderErp.fecha,
                    EnteredAt = worOrderErp.entrada,
                    IdCustomer = outIdCustomer,
                    PromisedAt = worOrderErp.promesa_entrega,
                    AuthorizedAt = worOrderErp.fecha_hora_autoriza,
                    HasTotalLoss = (worOrderErp.perdida_total == "s" || worOrderErp.perdida_total == "S"),
                    IdSalesRepresentative = worOrderErp.vendedor?.ToString(),
                    IsInsured = isInsured,
                    PickedUpAt = worOrderErp.fecha_hora_entrega_real,                  
                    Canceled = worOrderErp.anulada,
                    Note = worOrderErp.notas,
                    IsInvoiced = worOrderErp.facturada
                };


                paramsAditional = new List<Tuple<Type, string>>
                {
                    new Tuple<Type, string>(typeof(Workers), JsonConvert.SerializeObject(new WorkerKeysDmsV1(){ IdWorker =worOrderErp.vendedor?.ToString(),IdDealerShopWorkOrder =  worOrderErp.bodega.ToString()})),
                    new Tuple<Type, string>(typeof(Vehicles), JsonConvert.SerializeObject(new VehiclesKeysDmsV1(){ VinNumber =worOrderErp.serie})),
                    new Tuple<Type, string>(typeof(Customers), JsonConvert.SerializeObject(new CustomerKeys(){ IdCustomer = worOrderErp.nit.ToString()})),
                    new Tuple<Type, string>(typeof(CloudCatalogDealerShops), JsonConvert.SerializeObject(new DelaerShopKeysDmsV1(){ IdShop =  worOrderErp.bodega.ToString()})),
                    new Tuple<Type, string>(typeof(CloudCatalogInsuranceCompanies), JsonConvert.SerializeObject(new InsuranceComnayKeysDmsV1(){ Tin = insuranceCompany })),      
                };
                if (worOrderErp.kilometraje.HasValue && worOrderErp.kilometraje.Value > 0)
                    paramsAditional.Add(new Tuple<Type, string>(typeof(int), worOrderErp.kilometraje?.ToString()));
                return workOrder;
            }
            return null;
        }






        /// <summary>
        /// Metoto encargado de elimibnar
        /// </summary>
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }


        /// <summary>
        /// Liberar recursos adminsitrados del sistema
        /// </summary>
        /// <param name="disposing"></param>
        protected virtual void Dispose(bool disposing)
        {
            if (disposing && DataBase != null)
            {
                DataBase.Database.Connection.Close();
                DataBase.Dispose();
                DataBase = null;
            }
        }
    }



    public class WorkOrderDmsV1Keys
    {
        public String WorkOrderNumber { get; set; }

        public String IdErpShop { get; set; }

    }
}
