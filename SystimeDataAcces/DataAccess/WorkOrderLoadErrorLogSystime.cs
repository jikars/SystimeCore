using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utils.DataAccess;

namespace SystimeDataAcces.DataAccess
{
    public  class WorkOrderLoadErrorLogSystime
    {
        /// Cadena de coneccion a la base de datos 
        /// </summary>
        private string ConectionString { get; set; }


        /// <summary>
        /// Constructor de la clase
        /// </summary>
        public  WorkOrderLoadErrorLogSystime(String conectionString)
        {
            ConectionString = conectionString;
        }

        /// <summary>
        /// Metoto encargado de guardar o actualizar un error de carga de ordenes de trabajo 
        /// </summary>
        /// <param name="workOrderErrorEntity"></param>
        /// <param name="userModify"></param>
        /// <returns></returns>
        public Boolean SaveWorkOrderError(WorkOrderLoadErrorLog workOrderErrorEntity, String userModify)
        {
            if (workOrderErrorEntity != null)
            {
                Boolean isUpdate = false;
                using (SystimedbEntities Systimedb = new SystimedbEntities(ConectionString))
                {
                    WorkOrderLoadErrorLog currentEntity = Systimedb.WorkOrderLoadErrorLog.FirstOrDefault(w => w.IdWorkOrderNumberErp == workOrderErrorEntity.IdWorkOrderNumberErp && w.idDealerShopOtErp == workOrderErrorEntity.idDealerShopOtErp);
                    if (currentEntity != null)
                    {
                        //Update 
                        currentEntity.idCustomerOT = UtilsDataAcces.ValidateDiferentString(currentEntity.idCustomerOT, workOrderErrorEntity.idCustomerOT, true, isUpdate, out isUpdate);
                        currentEntity.idVehicle = UtilsDataAcces.ValidateDiferentString(currentEntity.idVehicle, workOrderErrorEntity.idVehicle, false, isUpdate, out isUpdate);
                        currentEntity.idWorkerOT = UtilsDataAcces.ValidateDiferentString(currentEntity.idWorkerOT, workOrderErrorEntity.idWorkerOT, true, isUpdate, out isUpdate);
                        currentEntity.idWorkerOT = UtilsDataAcces.ValidateDiferentString(currentEntity.idWorkerOT, workOrderErrorEntity.idWorkerOT, true, isUpdate, out isUpdate);
                        currentEntity.CloudUpdateError = UtilsDataAcces.ValidateDiferentString(currentEntity.CloudUpdateError, workOrderErrorEntity.CloudUpdateError, true, isUpdate, out isUpdate);

                        if (isUpdate)
                        {
                            currentEntity.UpdatedAt = DateTime.Now;
                            currentEntity.UpdatedById = userModify;
                            Systimedb.SaveChanges();
                        }
                        return true;
                    }
                    else
                    {
                        //Insert
                        workOrderErrorEntity.CreatedAt = DateTime.Now;
                        workOrderErrorEntity.CreatedById = userModify;
                        Systimedb.WorkOrderLoadErrorLog.Add(workOrderErrorEntity);
                        if (Systimedb.SaveChanges() > 0)
                            return true;
                    }
                }
            }
            return false;
        }



        /// <summary>
        /// Metodo encargado de eliminar un error de cargar de orden de trabajo 
        /// </summary>
        /// <param name="idWorkOrder"></param>
        /// <returns></returns>
        public Boolean DeleteWorkOrderError(String idWorkOrderErp, String iddealerShopErp)
        {
            if (!String.IsNullOrEmpty(idWorkOrderErp) && !String.IsNullOrEmpty(iddealerShopErp))
            {
                using (SystimedbEntities Systimedb = new SystimedbEntities(ConectionString))
                {
                    WorkOrderLoadErrorLog currentEntity = Systimedb.WorkOrderLoadErrorLog.FirstOrDefault(w => w.IdWorkOrderNumberErp == idWorkOrderErp && w.idDealerShopOtErp == iddealerShopErp);
                    if (currentEntity != null)
                    {
                        Systimedb.WorkOrderLoadErrorLog.Remove(currentEntity);
                        if (Systimedb.SaveChanges() > 0)
                            return true;
                    }
                }
            }
            return false;
        }

    }
}
