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
    
    public partial class CloudCatalogVehicleModels
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public CloudCatalogVehicleModels()
        {
            this.Vehicles = new HashSet<Vehicles>();
        }
    
        public int IdVehicleModel { get; set; }
        public string IdVehicleBrand { get; set; }
        public string VehicleModel { get; set; }
        public bool HasPhoto { get; set; }
        public System.DateTime CreatedAt { get; set; }
        public Nullable<System.DateTime> UpdatedAt { get; set; }
        public byte[] Version { get; set; }
        public bool Deleted { get; set; }
    
        public virtual CloudCatalogVehicleBrands CloudCatalogVehicleBrands { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Vehicles> Vehicles { get; set; }
    }
}
