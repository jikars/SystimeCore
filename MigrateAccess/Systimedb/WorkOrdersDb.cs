using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MigrateAccess.Systimedb
{
    public  class WorkOrdersDb
    {
        public decimal? ExistsWorkOrder(String workOrdernnumber, String idShp)
        {
            using (SystimedbEntities con = new SystimedbEntities())
            {
                return con.WorkOrders.FirstOrDefault(w => w.WorkOrderNumber == workOrdernnumber && w.IdDealerShop == idShp)?.IdWorkOrder;
            }
        }


        public int? SAveOperation(OperationByWorkOrder woperationbyWorkOrder)
        {
            using (SystimedbEntities con = new SystimedbEntities())
            {
                try
                {
                    return con.SP_WCF_SetOperationByWorkOrder(woperationbyWorkOrder.IdOperationByDealer, woperationbyWorkOrder.IdWorkOrder, null, null, null, null, woperationbyWorkOrder.CreatedById).FirstOrDefault();

                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }

        //public bool IsAssinableSubOperation(SubOperationByWorkOrder subOperationByWorkOrder)
        //{
        //    using (SystimedbEntities con = new SystimedbEntities())
        //    {
        //        try
        //        {
        //            if (subOperationByWorkOrder.AssignedAt != null)
        //                return con.SP_WCF_SetSubOperationByWorkOrderMigrate(subOperationByWorkOrder.IdOperationByWorkOrder, subOperationByWorkOrder.IdSubOperationByDealer, subOperationByWorkOrder.AssignedTo, subOperationByWorkOrder.AssignedById, subOperationByWorkOrder.OperationTimePercentage, subOperationByWorkOrder.WorkedTime, subOperationByWorkOrder.CreatedById, subOperationByWorkOrder.AssignedAt)?.FirstOrDefault();
        //            else
        //                return null;

        //        }
        //        catch (Exception ex)
        //        {
        //            throw ex;
        //        }

        //    }
        //}

        public int? SAveOperSaveSubOperation(SubOperationByWorkOrder subOperationByWorkOrder)
        {
            using (SystimedbEntities con = new SystimedbEntities())
            {
                try
                {
                    if(subOperationByWorkOrder.AssignedAt != null)
                        return con.SP_WCF_SetSubOperationByWorkOrderMigrate(subOperationByWorkOrder.IdOperationByWorkOrder,subOperationByWorkOrder.IdSubOperationByDealer,subOperationByWorkOrder.AssignedTo,subOperationByWorkOrder.AssignedById,subOperationByWorkOrder.OperationTimePercentage,subOperationByWorkOrder.WorkedTime,subOperationByWorkOrder.CreatedById,subOperationByWorkOrder.AssignedAt)?.FirstOrDefault();
                    else
                        return null;

                }   
                catch(Exception ex)
                {
                    throw ex;
                }
            
            }
        }

        internal bool ExistSubOperation(int? idSubOperation)
        {
            using (SystimedbEntities con = new SystimedbEntities())
            {
                return con.CatalogSubOperationByDealer.FirstOrDefault(w => w.IdSubOperationByDealer == idSubOperation) != null;                  
            }
        }

        internal bool IdSubOperationIsAssignable(int? idSubOperation)
        {
            using (SystimedbEntities con = new SystimedbEntities())
            {
                return con.CatalogSubOperationByDealer.FirstOrDefault(w => w.IdSubOperationByDealer == idSubOperation)?.IsAssignable ?? false;
            }
        }

        public bool SaveError(MigrationAccessError subOperationByWorkOrder)
        {
            using (SystimedbEntities con = new SystimedbEntities())
            {

                con.MigrationAccessError.Add(subOperationByWorkOrder);
                if (con.SaveChanges() > 0)
                    return true;
            }
            return false;
        }



        internal string GetIdDealerRepresentativeFromWorkOrder(string ordenDeTrabajo, int? idshop)
        {
            using (SystimedbEntities con = new SystimedbEntities())
            {
                return con.WorkOrders.FirstOrDefault(w => w.WorkOrderNumber == ordenDeTrabajo && w.IdDealerShop == idshop.Value.ToString())?.IdSalesRepresentative;
            }
        }


        internal decimal? SaveWorkORderTrackingAndTracckingDeatail(WorkOrderTracking workORdeTracking,String idWokrer, String idCurrentWorker, bool isWaiting, bool hasPhoto,String note)
        {
            using (SystimedbEntities con = new SystimedbEntities())
            {
                return con.InsertNewTrackingMigrate(workORdeTracking.IdWorkOrder.ToString(), workORdeTracking.IdSubOperationByDealer, idWokrer, idCurrentWorker, isWaiting, hasPhoto, note, workORdeTracking.CreatedById,workORdeTracking.InitiatedAt,workORdeTracking.InitiatedAt)?.FirstOrDefault();
            }
        }
    }
}
