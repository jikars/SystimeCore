//------------------------------------------------------------------------------
// <auto-generated>
//     Este código se generó a partir de una plantilla.
//
//     Los cambios manuales en este archivo pueden causar un comportamiento inesperado de la aplicación.
//     Los cambios manuales en este archivo se sobrescribirán si se regenera el código.
// </auto-generated>
//------------------------------------------------------------------------------

namespace ErpDataAccessFromSystime.Erps.DmsV1.DataAcces
{
    using System;
    using System.Collections.Generic;
    
    public partial class tall_operarios
    {
        public decimal nit { get; set; }
        public string escalafon { get; set; }
        public string actividad { get; set; }
        public Nullable<short> bodega { get; set; }
        public bool contratista { get; set; }
        public string patio { get; set; }
        public string codigo_op { get; set; }
        public Nullable<bool> activo { get; set; }
    
        public virtual terceros terceros { get; set; }
    }
}
