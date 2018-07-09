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
    
    public partial class Workers
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Workers()
        {
            this.CatalogSubOperationByDealer = new HashSet<CatalogSubOperationByDealer>();
            this.PendingTask = new HashSet<PendingTask>();
            this.Users = new HashSet<Users>();
            this.WorkerShedule = new HashSet<WorkerShedule>();
            this.WorkOrders = new HashSet<WorkOrders>();
            this.WorkOrderTrackingDetail = new HashSet<WorkOrderTrackingDetail>();
        }
    
        public string IdWorker { get; set; }
        public string IdDealerShop { get; set; }
        public string FullName { get; set; }
        public string NickName { get; set; }
        public string Mobile { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public string Address { get; set; }
        public Nullable<int> IdCity { get; set; }
        public bool HasPendingTask { get; set; }
        public bool HasPhoto { get; set; }
        public bool Active { get; set; }
        public string CreatedById { get; set; }
        public System.DateTime CreatedAt { get; set; }
        public Nullable<System.DateTime> UpdatedAt { get; set; }
        public string UpdatedById { get; set; }
        public byte[] Version { get; set; }
        public bool Deleted { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<CatalogSubOperationByDealer> CatalogSubOperationByDealer { get; set; }
        public virtual CloudCatalogCities CloudCatalogCities { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<PendingTask> PendingTask { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Users> Users { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<WorkerShedule> WorkerShedule { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<WorkOrders> WorkOrders { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<WorkOrderTrackingDetail> WorkOrderTrackingDetail { get; set; }
    }
}
