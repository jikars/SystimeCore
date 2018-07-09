using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utils.DataAccess;

namespace SystimeDataAcces.DataAccess
{
    public class WorOrdersSystime
    {
        /// <summary>
        /// Cadena de coneccion a la base de datos 
        /// </summary>
        private string ConectionString { get; set; }


        /// <summary>
        /// Consturctor de la clase
        /// </summary>
        public WorOrdersSystime(String conectionString)
        {
            ConectionString = conectionString;
        }

        /// <summary>
        /// Metoo encargado de grabar y actualizar una orden de trabajo en systime
        /// </summary>
        /// <param name="entityWorkOrder"></param>
        /// <param name="userModify"></param>
        /// <returns></returns>
        public WorkOrders SaveWorkOrder(WorkOrders entityWorkOrder, String userModify)
        {
            Boolean changeUpdateEntity = false;

            if (entityWorkOrder != null)
            {
                using (SystimedbEntities Systimedb = new SystimedbEntities(ConectionString))
                {
                    WorkOrders entitieCurrent = Systimedb.WorkOrders.FirstOrDefault(w => w.WorkOrderNumber == entityWorkOrder.WorkOrderNumber && w.IdDealerShop == entityWorkOrder.IdDealerShop);
                    if (entitieCurrent != null)
                    {

                        entitieCurrent.AuthorizedAt = UtilsDataAcces.ValidateDiferentDateTime(entitieCurrent.AuthorizedAt, entityWorkOrder.AuthorizedAt, changeUpdateEntity, out changeUpdateEntity);

                        entitieCurrent.FeedBack = UtilsDataAcces.ValidateDiferentString(entitieCurrent.FeedBack, entityWorkOrder.FeedBack, false, changeUpdateEntity, out changeUpdateEntity);

                        entitieCurrent.HasTotalLoss = UtilsDataAcces.ValidateDiferentBoolean(entitieCurrent.HasTotalLoss, entityWorkOrder.HasTotalLoss, changeUpdateEntity, out changeUpdateEntity) ?? false;

                        entitieCurrent.IdCurrentOperation = UtilsDataAcces.ValidateDiferentInt(entitieCurrent.IdCurrentOperation, entityWorkOrder.IdCurrentOperation, true, changeUpdateEntity, out changeUpdateEntity);

                        entitieCurrent.IdCurrentWorker = UtilsDataAcces.ValidateDiferentString(entitieCurrent.IdCurrentWorker, entityWorkOrder.IdCurrentWorker, true, changeUpdateEntity, out changeUpdateEntity);

                        entitieCurrent.IdCustomer = (UtilsDataAcces.ValidateDiferentDecimal(entitieCurrent.IdCustomer, entityWorkOrder.IdCustomer, true, changeUpdateEntity, out changeUpdateEntity)).Value;

                        entitieCurrent.IdDealerShop = UtilsDataAcces.ValidateDiferentString(entitieCurrent.IdDealerShop, entityWorkOrder.IdDealerShop, true, changeUpdateEntity, out changeUpdateEntity);

                        entitieCurrent.IdErpQuote = UtilsDataAcces.ValidateDiferentInt(entitieCurrent.IdErpQuote, entityWorkOrder.IdErpQuote, true, changeUpdateEntity, out changeUpdateEntity);

                        entitieCurrent.IdInsuranceCompany = UtilsDataAcces.ValidateDiferentInt(entitieCurrent.IdInsuranceCompany, entityWorkOrder.IdInsuranceCompany, true, changeUpdateEntity, out changeUpdateEntity);

                        entitieCurrent.IdPhysicalTag = UtilsDataAcces.ValidateDiferentInt(entitieCurrent.IdPhysicalTag, entityWorkOrder.IdPhysicalTag, true, changeUpdateEntity, out changeUpdateEntity);

                        entitieCurrent.IdSalesRepresentative = UtilsDataAcces.ValidateDiferentString(entitieCurrent.IdSalesRepresentative, entityWorkOrder.IdSalesRepresentative, true, changeUpdateEntity, out changeUpdateEntity);

                        entitieCurrent.IdVinNumber = UtilsDataAcces.ValidateDiferentString(entitieCurrent.IdVinNumber, entityWorkOrder.IdVinNumber, false, changeUpdateEntity, out changeUpdateEntity);

                        entitieCurrent.InvoicedAt = UtilsDataAcces.ValidateDiferentDateTime(entitieCurrent.InvoicedAt, entityWorkOrder.InvoicedAt, changeUpdateEntity, out changeUpdateEntity);

                        entitieCurrent.Note = UtilsDataAcces.ValidateDiferentString(entitieCurrent.Note, entityWorkOrder.Note, false, changeUpdateEntity, out changeUpdateEntity);

                        entitieCurrent.OpenOtAt = UtilsDataAcces.ValidateDiferentDateTime(entitieCurrent.OpenOtAt, entityWorkOrder.OpenOtAt, changeUpdateEntity, out changeUpdateEntity);

                        entitieCurrent.PartsCompletedAt = UtilsDataAcces.ValidateDiferentDateTime(entitieCurrent.PartsCompletedAt, entityWorkOrder.PartsCompletedAt, changeUpdateEntity, out changeUpdateEntity);

                        entitieCurrent.PickedUpAt = UtilsDataAcces.ValidateDiferentDateTime(entitieCurrent.PickedUpAt, entityWorkOrder.PickedUpAt, changeUpdateEntity, out changeUpdateEntity);

                        entitieCurrent.IsInvoiced = UtilsDataAcces.ValidateDiferentBoolean(entitieCurrent.IsInvoiced, entityWorkOrder.IsInvoiced, changeUpdateEntity, out changeUpdateEntity) ?? false;


                        entitieCurrent.PromisedAt = UtilsDataAcces.ValidateDiferentDateTime(entitieCurrent.PromisedAt, entityWorkOrder.PromisedAt, changeUpdateEntity, out changeUpdateEntity);

                        entitieCurrent.Canceled = UtilsDataAcces.ValidateDiferentBoolean(entitieCurrent.Canceled, entityWorkOrder.Canceled, changeUpdateEntity, out changeUpdateEntity);



                        if (changeUpdateEntity)
                        {
                            entitieCurrent.UpdatedAt = DateTime.Now;
                            entitieCurrent.UpdatedById = userModify;
                            Systimedb.SaveChanges();
                        }

                        return entitieCurrent;
                    }
                    else
                    {
                        entityWorkOrder.CreatedAt = DateTime.Now;
                        entityWorkOrder.CreatedById = userModify;
                        Systimedb.WorkOrders.Add(entityWorkOrder);
                        if (Systimedb.SaveChanges() > 0)
                            return entityWorkOrder;
                    }
                }
            }
            return null;
        }


    }
}
