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
    
    public partial class ShopAppointments
    {
        public int IdShopAppointments { get; set; }
        public Nullable<int> IdQuote { get; set; }
        public string IdAppUser { get; set; }
        public System.DateTime DateOfAppointment { get; set; }
        public string IdUserRevised { get; set; }
        public Nullable<System.DateTime> DateOfRevised { get; set; }
        public System.DateTime CreatedAt { get; set; }
        public string CreatedById { get; set; }
        public Nullable<System.DateTime> UpdatedAt { get; set; }
        public string UpdatedById { get; set; }
        public byte[] Version { get; set; }
        public bool Deleted { get; set; }
    
        public virtual Quotes Quotes { get; set; }
    }
}
