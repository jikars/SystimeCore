//------------------------------------------------------------------------------
// <auto-generated>
//     Este código se generó a partir de una plantilla.
//
//     Los cambios manuales en este archivo pueden causar un comportamiento inesperado de la aplicación.
//     Los cambios manuales en este archivo se sobrescribirán si se regenera el código.
// </auto-generated>
//------------------------------------------------------------------------------

namespace SystimeDataAcces.DataAccess
{
    using System;
    using System.Collections.Generic;
    
    public partial class ProgressByWorkOrder
    {
        public int IdOperationByDealer { get; set; }
        public string IdWorkOrder { get; set; }
        public double SpecificProgress { get; set; }
        public double CumulativeProgress { get; set; }
        public double Progress { get; set; }
        public bool HasPhoto { get; set; }
        public string Description { get; set; }
        public Nullable<System.DateTime> CreatedAt { get; set; }
        public System.DateTime UpdatedAt { get; set; }
        public byte[] Version { get; set; }
        public bool Deleted { get; set; }
    }
}
