using GlobalResources.ModelResources;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Models
{
    /// <summary>
    /// Modelo de datos para compañia de seguros
    /// </summary>
    public class InsuranceCompany
    {
        public int? IdInsuranceCompany { get; set; }

        [DataType(DataType.Text)]
        [Required(ErrorMessageResourceType = typeof(DataAnotations), ErrorMessageResourceName = nameof(DataAnotations.CountryRequerid))]
        [StringLength(2, ErrorMessageResourceType = typeof(DataAnotations), ErrorMessageResourceName = nameof(DataAnotations.IdContryMaxLegthExceeded))]
        public String IdCountry { get; set; }



        [Required(ErrorMessageResourceType = typeof(DataAnotations), ErrorMessageResourceName = nameof(DataAnotations.IdCityRequired))]
        [Range(1, int.MaxValue, ErrorMessageResourceName = nameof(DataAnotations.IdCityRangeError), ErrorMessageResourceType = typeof(DataAnotations))]
        public int? IdCity { get; set; } = 0;


        [DataType(DataType.Text)]
        [Required(ErrorMessageResourceType = typeof(DataAnotations), ErrorMessageResourceName = nameof(DataAnotations.TINReqired))]
        [StringLength(50, ErrorMessageResourceType = typeof(DataAnotations), ErrorMessageResourceName = nameof(DataAnotations.TINMaxLegthExceeded))]
        public String Tin { get; set; }


        [Required(ErrorMessageResourceType = typeof(DataAnotations), ErrorMessageResourceName = nameof(DataAnotations.NameIsRequired))]
        [StringLength(50, ErrorMessageResourceType = typeof(DataAnotations), ErrorMessageResourceName = nameof(DataAnotations.MaxLegthExceeded))]
        public String FullName { get; set; }


        [StringLength(100, ErrorMessageResourceType = typeof(DataAnotations), ErrorMessageResourceName = nameof(DataAnotations.EmailMaxLegthExceeded))]
        [RegularExpression(DataAnotationConst.RegularExpresionEmail, ErrorMessageResourceType = typeof(DataAnotations), ErrorMessageResourceName = nameof(DataAnotations.EmailIncorrectFormat))]
        public String Email { get; set; }


        [StringLength(25, ErrorMessageResourceType = typeof(DataAnotations), ErrorMessageResourceName = nameof(DataAnotations.PhoneMaxLegthExceeded))]
        [RegularExpression(DataAnotationConst.RegularExpresionPhone, ErrorMessageResourceType = typeof(DataAnotations), ErrorMessageResourceName = nameof(DataAnotations.PhoneIncorrectFormat))]
        public String Phone { get; set; }


        [StringLength(25, ErrorMessageResourceType = typeof(DataAnotations), ErrorMessageResourceName = nameof(DataAnotations.PhoneMaxLegthExceeded))]
        [RegularExpression(DataAnotationConst.RegularExpresionMobile, ErrorMessageResourceType = typeof(DataAnotations), ErrorMessageResourceName = nameof(DataAnotations.PhoneIncorrectFormat))]
        public String Phone2 { get; set; }


        [StringLength(100, ErrorMessageResourceType = typeof(DataAnotations), ErrorMessageResourceName = nameof(DataAnotations.WebSiteMaxLegthExceeded))]
        [RegularExpression(DataAnotationConst.RegularExpresionUrl, ErrorMessageResourceType = typeof(DataAnotations), ErrorMessageResourceName = nameof(DataAnotations.WebSiteIncorrectFormat))]
        public String WebSite { get; set; }

        [StringLength(75, ErrorMessageResourceType = typeof(DataAnotations), ErrorMessageResourceName = nameof(DataAnotations.AdressMaxLegthExceeded))]
        public String Address { get; set; }

        public Boolean? HasPhoto { get; set; }

        public List<String> Pictures { get; set; }
    }
}