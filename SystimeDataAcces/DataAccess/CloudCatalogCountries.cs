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
    
    public partial class CloudCatalogCountries
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public CloudCatalogCountries()
        {
            this.CatalogProviders = new HashSet<CatalogProviders>();
            this.CloudCatalogCities = new HashSet<CloudCatalogCities>();
            this.Customers = new HashSet<Customers>();
            this.CloudCatalogInsuranceCompanies = new HashSet<CloudCatalogInsuranceCompanies>();
            this.Vehicles = new HashSet<Vehicles>();
        }
    
        public string IdCountry { get; set; }
        public int Country { get; set; }
        public string CountryTranslate { get; set; }
        public string Alpha3 { get; set; }
        public Nullable<int> PhoneCode { get; set; }
        public string IdCurrency { get; set; }
        public string IdLanguage { get; set; }
        public string IdDistanceUnit { get; set; }
        public Nullable<double> TraveledDistanceAverage { get; set; }
        public bool HasPhoto { get; set; }
        public System.DateTime CreatedAt { get; set; }
        public Nullable<System.DateTime> UpdatedAt { get; set; }
        public byte[] Version { get; set; }
        public bool Deleted { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<CatalogProviders> CatalogProviders { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<CloudCatalogCities> CloudCatalogCities { get; set; }
        public virtual CloudCatalogLanguage CloudCatalogLanguage { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Customers> Customers { get; set; }
        public virtual CloudCatalogUnits CloudCatalogUnits { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<CloudCatalogInsuranceCompanies> CloudCatalogInsuranceCompanies { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Vehicles> Vehicles { get; set; }
    }
}
