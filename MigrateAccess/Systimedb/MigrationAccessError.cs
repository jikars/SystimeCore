//------------------------------------------------------------------------------
// <auto-generated>
//     Este código se generó a partir de una plantilla.
//
//     Los cambios manuales en este archivo pueden causar un comportamiento inesperado de la aplicación.
//     Los cambios manuales en este archivo se sobrescribirán si se regenera el código.
// </auto-generated>
//------------------------------------------------------------------------------

namespace MigrateAccess.Systimedb
{
    using System;
    using System.Collections.Generic;
    
    public partial class MigrationAccessError
    {
        public int IdMigrationAccess { get; set; }
        public string WorkOrderNumber { get; set; }
        public int IdShop { get; set; }
        public string ExceptionMessage { get; set; }
        public Nullable<System.DateTime> CreatedAt { get; set; }
        public string CreatedById { get; set; }
    }
}
