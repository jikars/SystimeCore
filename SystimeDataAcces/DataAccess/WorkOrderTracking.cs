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
    
    public partial class WorkOrderTracking
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public WorkOrderTracking()
        {
            this.WorkOrderTrackingDetail = new HashSet<WorkOrderTrackingDetail>();
        }
    
        public decimal IdWorkOrderTracking { get; set; }
        public decimal IdWorkOrder { get; set; }
        public int IdSubOperationByDealer { get; set; }
        public System.DateTime InitiatedAt { get; set; }
        public Nullable<System.DateTime> CompletedAt { get; set; }
        public System.DateTime CreatedAt { get; set; }
        public string CreatedById { get; set; }
        public Nullable<System.DateTime> UpdatedAt { get; set; }
        public string UpdatedById { get; set; }
        public byte[] Version { get; set; }
        public bool Deleted { get; set; }
    
        public virtual WorkOrders WorkOrders { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<WorkOrderTrackingDetail> WorkOrderTrackingDetail { get; set; }
    }
}
