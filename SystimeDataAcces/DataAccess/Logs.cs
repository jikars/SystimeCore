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
    
    public partial class Logs
    {
        public int IdLog { get; set; }
        public string OSName { get; set; }
        public string OSVersion { get; set; }
        public string DeviceBrand { get; set; }
        public string DeviveModel { get; set; }
        public string DeviceName { get; set; }
        public string App { get; set; }
        public string AppUser { get; set; }
        public string EventType { get; set; }
        public string ClassName { get; set; }
        public string MethodName { get; set; }
        public string SourceMethodEvent { get; set; }
        public int NumberLine { get; set; }
        public string SystemMessage { get; set; }
        public string ControlledMessage { get; set; }
        public System.DateTime TimeOfEventClient { get; set; }
        public string ReviewedBy { get; set; }
        public Nullable<System.DateTime> ReviewedAt { get; set; }
        public bool ErrorSolved { get; set; }
        public string CreatedById { get; set; }
        public Nullable<System.DateTime> UpdatedAt { get; set; }
        public string UpdatedById { get; set; }
        public byte[] Version { get; set; }
        public bool Deleted { get; set; }
    }
}
