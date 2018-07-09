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
    
    public partial class CatalogOperationsByDealer
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public CatalogOperationsByDealer()
        {
            this.CatalogExternalJobsByDealer = new HashSet<CatalogExternalJobsByDealer>();
            this.CatalogSubOperationByDealer = new HashSet<CatalogSubOperationByDealer>();
            this.JobsByWorkOrder = new HashSet<JobsByWorkOrder>();
            this.OperationByWorkOrder = new HashSet<OperationByWorkOrder>();
        }
    
        public int IdOperationByDealer { get; set; }
        public int IdOperation { get; set; }
        public string IdDealerShop { get; set; }
        public Nullable<int> IdNextOperationByDealer { get; set; }
        public string IdDefaultWorker { get; set; }
        public string Name { get; set; }
        public Nullable<bool> IsAssignable { get; set; }
        public bool IsDefault { get; set; }
        public System.DateTime CreatedAt { get; set; }
        public string CreatedById { get; set; }
        public Nullable<System.DateTime> UpdatedAt { get; set; }
        public string UpdatedById { get; set; }
        public Nullable<double> Effort { get; set; }
        public Nullable<double> DefaultPercentage { get; set; }
        public byte[] Version { get; set; }
        public bool Deleted { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<CatalogExternalJobsByDealer> CatalogExternalJobsByDealer { get; set; }
        public virtual CloudCatalogOperations CloudCatalogOperations { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<CatalogSubOperationByDealer> CatalogSubOperationByDealer { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<JobsByWorkOrder> JobsByWorkOrder { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<OperationByWorkOrder> OperationByWorkOrder { get; set; }
    }
}
