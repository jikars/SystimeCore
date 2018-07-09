using ErpDataAccessFromSystime.Erps.DmsV1.DataAcces;
using MigrateAccess.ExcelMapping;
using MigrateAccess.Systimedb;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static SystimeCore.Config.Enums;

namespace MigrateAccess
{
    static class Program
    {
        static void Main(string[] args)
        {
            SeguimientosEnAlfaExcel excel = new SeguimientosEnAlfaExcel();
            WorkOrdersDb workORder = new WorkOrdersDb();
            List<SeguimientoEnAlfaModel> lsitSave = excel.GetAllSewguimientoEnAlfa();
            int? idshop = null;
            String json = "";
            decimal? idWorkOrder = null;
            int? idOperationByWorkOrder = null;
            int? idSubOperationByWorkOrder = null;
            decimal? idTrackingDeatail = null;
            bool isAssinable = false;
            int contador = 0;
            lsitSave.ForEach(s =>
            {
                contador++;

                //if (s.IdSeguimientoOrdenDeTrabajo == "14829")
                //{
                Console.WriteLine(contador);
                try
                {
                    if (s.Estado.ToUpper().Trim() == "0")
                        s.Estado = "Imprevistos (En Espera)";

                    if (long.TryParse(s.OrdenDeTrabajo, out long ordernumber) && ordernumber > 15000)
                        idshop = 9;
                    else if (long.TryParse(s.OrdenDeTrabajo, out long ordernuum) && ordernuum < 15000)
                        idshop = 11;

                    if (String.IsNullOrEmpty(s.IdSeguimientoPor))
                        s.IdSeguimientoPor = workORder.GetIdDealerRepresentativeFromWorkOrder(s.OrdenDeTrabajo, idshop);
                    //Consulte el aseor

                    if (s.IdSeguimientoPor.ToUpper().Trim() == "Vehículos Entregados".ToUpper().Trim())
                        s.IdSeguimientoPor = s.IdResponsable;


                    if (!String.IsNullOrEmpty(s.OrdenDeTrabajo))
                    {
                        idWorkOrder = workORder.ExistsWorkOrder(s.OrdenDeTrabajo, idshop.Value.ToString());



                        if (idWorkOrder == null)
                        {
                            json = JsonConvert.SerializeObject(new WorkOrderDmsV1Keys() { IdErpShop = idshop.ToString(), WorkOrderNumber = s.OrdenDeTrabajo });
                            RequestSql.FillterTestSql(TableName.WORK_ORDERS.ToString(), TableAction.I.ToString(), json);
                            idWorkOrder = workORder.ExistsWorkOrder(s.OrdenDeTrabajo, idshop.Value.ToString());
                        }



                        if (excel.GetTrackingTraslation(s.Estado, out int? idOp, out int? idCatSubOpeDelaer, out bool? isWaiting) && idWorkOrder != null)
                        {

                            //debe validar si la subOperatccion by dealer es asingable 
                            if (workORder.ExistSubOperation(idCatSubOpeDelaer))
                                isAssinable = workORder.IdSubOperationIsAssignable(idCatSubOpeDelaer);

                            idOperationByWorkOrder = workORder.SAveOperation(new OperationByWorkOrder()
                            {
                                IdOperationByDealer = idOp.Value,
                                IdWorkOrder = idWorkOrder.Value,
                                CreatedById = "MigracionAcces"
                            });
                            if (String.IsNullOrEmpty(s.IdSeguimientoPor))
                            {
                                s.IdSeguimientoPor = workORder.GetIdDealerRepresentativeFromWorkOrder(s.OrdenDeTrabajo, idshop);
                            }


                            if (idOperationByWorkOrder.HasValue && excel.GetWorkerbyName(s.IdResponsable, out long? idWorkerTo) && excel.GetWorkerbyName(s.IdSeguimientoPor, out long? idwokrBy))
                            {
                                if (isAssinable)
                                    idSubOperationByWorkOrder = workORder.SAveOperSaveSubOperation(new SubOperationByWorkOrder()
                                    {
                                        IdOperationByWorkOrder = idOperationByWorkOrder.Value,
                                        IdSubOperationByDealer = idCatSubOpeDelaer.Value,
                                        AssignedTo = idWorkerTo.Value.ToString(),
                                        AssignedById = idwokrBy.Value.ToString(),
                                        OperationTimePercentage = 100,
                                        AssignedAt = DateTime.Parse(s.FechaSeguimiento),
                                        CreatedAt = DateTime.Parse(s.FechaSeguimiento),
                                        WorkedTime = 0.0,
                                        CreatedById = "MigracionAcces",

                                    });


                                if (idSubOperationByWorkOrder != null || !isAssinable)
                                {
                                    idTrackingDeatail = workORder.SaveWorkORderTrackingAndTracckingDeatail(new WorkOrderTracking()
                                    {
                                        IdWorkOrder = idWorkOrder.Value,
                                        IdSubOperationByDealer = idCatSubOpeDelaer.Value,
                                        InitiatedAt = DateTime.Parse(s.FechaSeguimiento),
                                        CreatedById = "MigracionAcces",
                                    }, idwokrBy.ToString(), idWorkerTo.ToString(), isWaiting.Value, false, s.Observaciones);
                                }
                            }

                        }


                        if (idTrackingDeatail == null && s.Estado != "TOT (En Espera)")
                            workORder.SaveError(new MigrationAccessError()
                            {
                                WorkOrderNumber = s.IdSeguimientoOrdenDeTrabajo,
                                IdShop = idshop.Value
                            });


                        idshop = null;
                        json = "";
                        idWorkOrder = null;
                        idOperationByWorkOrder = null;
                        idSubOperationByWorkOrder = null;
                        isAssinable = false;
                        idTrackingDeatail = null;

                    }
                }
                catch (Exception ex)
                {
                    workORder.SaveError(new MigrationAccessError()
                    {
                        WorkOrderNumber = s.IdSeguimientoOrdenDeTrabajo,
                        IdShop = idshop.Value,
                        ExceptionMessage = ex.Message
                    });
                }
                //}





            });
            Console.ReadLine();
        }
    }
}
