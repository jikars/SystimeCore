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
    
    public partial class CloudCatalogCities
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public CloudCatalogCities()
        {
            this.CatalogProviders = new HashSet<CatalogProviders>();
            this.CloudCatalogInsuranceCompanies = new HashSet<CloudCatalogInsuranceCompanies>();
            this.CloudCatalogDealerShops = new HashSet<CloudCatalogDealerShops>();
            this.Customers = new HashSet<Customers>();
            this.Vehicles = new HashSet<Vehicles>();
            this.Workers = new HashSet<Workers>();
        }
    
        public int IdCity { get; set; }
        public string IdCountry { get; set; }
        public Nullable<long> IdState { get; set; }
        public string CityCode { get; set; }
        public string City { get; set; }
        public Nullable<double> Latitud { get; set; }
        public Nullable<double> Logitud { get; set; }
        public string TimeZoneName { get; set; }
        public System.DateTime CreatedAt { get; set; }
        public Nullable<System.DateTime> UpdatedAt { get; set; }
        public byte[] Version { get; set; }
        public bool Deleted { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<CatalogProviders> CatalogProviders { get; set; }
        public virtual CloudCatalogCountries CloudCatalogCountries { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<CloudCatalogInsuranceCompanies> CloudCatalogInsuranceCompanies { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<CloudCatalogDealerShops> CloudCatalogDealerShops { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Customers> Customers { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Vehicles> Vehicles { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Workers> Workers { get; set; }
    }
}
