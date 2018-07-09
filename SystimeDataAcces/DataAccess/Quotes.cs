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
    
    public partial class Quotes
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Quotes()
        {
            this.QuoteDetails = new HashSet<QuoteDetails>();
            this.ShopAppointments = new HashSet<ShopAppointments>();
        }
    
        public int IdQuote { get; set; }
        public int IdShop { get; set; }
        public Nullable<int> IdQuoteRequest { get; set; }
        public Nullable<decimal> IdWorkOrder { get; set; }
        public byte[] Title { get; set; }
        public string Observations { get; set; }
        public Nullable<System.DateTime> AutorizedAt { get; set; }
        public string AuthorizedById { get; set; }
        public bool IsPhoto { get; set; }
        public System.DateTime CreatedAt { get; set; }
        public string CreatedById { get; set; }
        public Nullable<System.DateTime> UpdatedAt { get; set; }
        public string UpdatedById { get; set; }
        public byte[] Version { get; set; }
        public bool Deleted { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<QuoteDetails> QuoteDetails { get; set; }
        public virtual QuoteRequest QuoteRequest { get; set; }
        public virtual WorkOrders WorkOrders { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ShopAppointments> ShopAppointments { get; set; }
    }
}
