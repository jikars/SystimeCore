using GlobalResources.ModelResources;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{

    /// <summary>
    /// Modelo de datos para dealer
    /// </summary>
    public class Dealer
    {
        public int? IdDealer { get; set; }


        [DataType(DataType.Text)]
        [Required(ErrorMessageResourceType = typeof(DataAnotations), ErrorMessageResourceName = nameof(DataAnotations.CountryRequerid))]
        [StringLength(2, ErrorMessageResourceType = typeof(DataAnotations), ErrorMessageResourceName = nameof(DataAnotations.IdContryMaxLegthExceeded))]
        public String IdCountry { get; set; }


        public int? IdBrandIdentity { get; set; }


        [DataType(DataType.Text)]
        [Required(ErrorMessageResourceType = typeof(DataAnotations), ErrorMessageResourceName = nameof(DataAnotations.TINReqired))]
        [StringLength(50, ErrorMessageResourceType = typeof(DataAnotations), ErrorMessageResourceName = nameof(DataAnotations.TINMaxLegthExceeded))]
        public String Tin { get; set; }


        [StringLength(100, ErrorMessageResourceType = typeof(DataAnotations), ErrorMessageResourceName = nameof(DataAnotations.AliasMaxLegthExceeded))]
        public String Alias { get; set; }


        [DataType(DataType.Text)]
        [Required(ErrorMessageResourceType = typeof(DataAnotations), ErrorMessageResourceName = nameof(DataAnotations.NameIsRequired))]
        [StringLength(50, ErrorMessageResourceType = typeof(DataAnotations), ErrorMessageResourceName = nameof(DataAnotations.NameMaxLegthExceeded))]
        public String Name { get; set; }

        [StringLength(100, ErrorMessageResourceType = typeof(DataAnotations), ErrorMessageResourceName = nameof(DataAnotations.EmailMaxLegthExceeded))]
        [RegularExpression(DataAnotationConst.RegularExpresionEmail, ErrorMessageResourceType = typeof(DataAnotations), ErrorMessageResourceName = nameof(DataAnotations.EmailIncorrectFormat))]
        public String Email { get; set; }


        [StringLength(25, ErrorMessageResourceType = typeof(DataAnotations), ErrorMessageResourceName = nameof(DataAnotations.PhoneMaxLegthExceeded))]
        [RegularExpression(DataAnotationConst.RegularExpresionPhone, ErrorMessageResourceType = typeof(DataAnotations), ErrorMessageResourceName = nameof(DataAnotations.PhoneIncorrectFormat))]
        public String Phone { get; set; }


        [StringLength(25, ErrorMessageResourceType = typeof(DataAnotations), ErrorMessageResourceName = nameof(DataAnotations.PhoneMobileMaxLegthExceeded))]
        [RegularExpression(DataAnotationConst.RegularExpresionMobile, ErrorMessageResourceType = typeof(DataAnotations), ErrorMessageResourceName = nameof(DataAnotations.PhoneMobileIncorrectFormat))]
        public String Mobile { get; set; }

         
        [StringLength(100, ErrorMessageResourceType = typeof(DataAnotations), ErrorMessageResourceName = nameof(DataAnotations.WebSiteMaxLegthExceeded))]
        [RegularExpression(DataAnotationConst.RegularExpresionUrl, ErrorMessageResourceType = typeof(DataAnotations), ErrorMessageResourceName = nameof(DataAnotations.WebSiteIncorrectFormat))]
        public String WebSite { get; set; }

        public Boolean? HasPhoto { get; set; }

        public List<String> Pictures { get; set; }


    }
}
