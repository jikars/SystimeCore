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
    
    public partial class CloudCatalogDealerShops
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public CloudCatalogDealerShops()
        {
            this.WorkOrders = new HashSet<WorkOrders>();
        }
    
        public string IdDealerShop { get; set; }
        public int IdCity { get; set; }
        public string Shop { get; set; }
        public string Address { get; set; }
        public Nullable<double> Latitude { get; set; }
        public Nullable<double> Longitude { get; set; }
        public string Email { get; set; }
        public string Mobile { get; set; }
        public string Phone { get; set; }
        public bool HasPhoto { get; set; }
        public System.DateTime CreatedAt { get; set; }
        public Nullable<System.DateTime> UpdatedAt { get; set; }
        public byte[] Version { get; set; }
        public bool Deleted { get; set; }
    
        public virtual CloudCatalogCities CloudCatalogCities { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<WorkOrders> WorkOrders { get; set; }
    }
}
