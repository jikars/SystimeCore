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
    
    public partial class PendingTask
    {
        public int IdPendingTask { get; set; }
        public string PendingTask1 { get; set; }
        public string Description { get; set; }
        public string IdWorker { get; set; }
        public string IdWorkOrder { get; set; }
        public Nullable<int> PendingTaskGroup { get; set; }
        public short Priority { get; set; }
        public Nullable<System.DateTime> ExpirationAt { get; set; }
        public Nullable<System.DateTime> CompletedAt { get; set; }
        public System.DateTime CreatedAt { get; set; }
        public string CreatedById { get; set; }
        public Nullable<System.DateTime> UpdatedAt { get; set; }
        public string UpdatedById { get; set; }
        public byte[] Version { get; set; }
        public bool Deleted { get; set; }
    
        public virtual Workers Workers { get; set; }
    }
}
