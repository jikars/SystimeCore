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
    
    public partial class Translations
    {
        public int IdTranslation { get; set; }
        public int IdTransalationReference { get; set; }
        public string IdLanguage { get; set; }
        public string TranslationText { get; set; }
        public System.DateTime CreatedAt { get; set; }
        public Nullable<System.DateTime> UpdatedAT { get; set; }
        public byte[] Version { get; set; }
        public bool Deleted { get; set; }
    
        public virtual CloudCatalogLanguage CloudCatalogLanguage { get; set; }
        public virtual TranslationReferences TranslationReferences { get; set; }
    }
}
