using LinqToExcel;
using LinqToExcel.Query;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MigrateAccess.ExcelMapping
{

    public class SeguimientosEnAlfaExcel
    {

        private ExcelQueryFactory Excel { get; set; }

        public SeguimientosEnAlfaExcel()
        {
            Excel = new ExcelQueryFactory(@"C:\SysTime Access\MigracionTracking");
        }


        public List<SeguimientoEnAlfaModel> GetAllSewguimientoEnAlfa()
        {
            return Excel.Worksheet<SeguimientoEnAlfaModel>(2)?.ToList();
        }

        public bool GetWorkerbyName(String name, out long? idWork)
        {

            idWork = null;
            String idWorker;
            if (!long.TryParse(name, out long idWorks))
                idWorker = Excel.Worksheet<Trabajador>(1).ToList().FirstOrDefault(w => w.Nombre.ToUpper().Trim() == name.ToUpper().Trim())?.IdTrabajador;
            else
                idWorker = name;

            if (!String.IsNullOrEmpty(idWorker) && long.TryParse(idWorker, out long id))
            {
                idWork = id;
                return true;
            }
            return false;
        }

        public bool GetTrackingTraslation(String state, out int? idOperacion, out int? idCatalogSuboperationByDealer, out bool? isWaiting)
        {
            idOperacion = null;
            idCatalogSuboperationByDealer = null;
            isWaiting = null;
            if (!String.IsNullOrEmpty(state))
            {
                EstadoSeguimientoModel trackingState = Excel.Worksheet<EstadoSeguimientoModel>(0).ToList().FirstOrDefault(w => w.EstadoSeguimiento.ToUpper().Trim() == state.ToUpper().Trim());
                
               
                if (trackingState != null && int.TryParse(trackingState.IdOperacion, out int idop) && int.TryParse(trackingState.IdCatalogSubOperationByDealer, out int idSubOp) && CastToBool(trackingState.IsWaiting, out bool isWait))
                {
                    idOperacion = idop;
                    idCatalogSuboperationByDealer = idSubOp;
                    isWaiting = isWait;
                    return true;
                }

            }
            return false;
        }








        private bool CastToBool(String condition, out Boolean value)
        {
            value = condition.ToUpper().Trim() == "VERDADERO" || condition.ToUpper().Trim() == "TRUE";
            if (condition.ToUpper().Trim() == "VERDADERO" || condition.ToUpper().Trim() == "TRUE")
                return true;
            else if (condition.ToUpper().Trim() == "FALSO" || condition.ToUpper().Trim() == "FALSE")
                return true;

            return false;
        }

    }

    public class SeguimientoEnAlfaModel
    {
        public String IdSeguimientoOrdenDeTrabajo { get; set; }
        public String OrdenDeTrabajo { get; set; }
        public String FechaSeguimiento { get; set; }
        public String IdResponsable { get; set; }
        public String Estado { get; set; }
        public String IdSeguimientoPor { get; set; }
        public String Observaciones { get; set; }

        public String TOT { get; set; }


    }


    public class EstadoSeguimientoModel
    {
        public String IdEstadoSeguimiento { get; set; }
        public String EstadoSeguimiento { get; set; }
        public String Orden { get; set; }
        public String ProcesoTaller { get; set; }
        public String IdDealerShop { get; set; }
        public String IdOperacion { get; set; }

        public String IdCatalogSubOperationByDealer { get; set; }

        public String IsWaiting { get; set; }

        public String SiguienteSubProceso { get; set; }

    }


    public class Trabajador
    {
        public String IdTrabajador { get; set; }
        public String Nombre { get; set; }
        public String IdERP { get; set; }
        public String IdCargo { get; set; }
        public String SobreNombre { get; set; }
        public String Telefono { get; set; }

        public String Visible { get; set; }

        public String Activo { get; set; }

        public String ValorHora { get; set; }

        public String User { get; set; }


    }
}


