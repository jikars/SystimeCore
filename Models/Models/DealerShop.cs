using GlobalResources.ModelResources;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Models
{
    public class DealerShop
    {
        public int? IdDealerShop { get; set; }

        [Required(ErrorMessageResourceType = typeof(DataAnotations), ErrorMessageResourceName = nameof(DataAnotations.IdDealerRequired))]
        [Range(1, int.MaxValue, ErrorMessageResourceName = nameof(DataAnotations.IdDealerRangeError), ErrorMessageResourceType = typeof(DataAnotations))]
        public int? IdDealer { get; set; } = 0;


        [Required(ErrorMessageResourceType = typeof(DataAnotations), ErrorMessageResourceName = nameof(DataAnotations.IdCityRequired))]
        [Range(1, int.MaxValue, ErrorMessageResourceName = nameof(DataAnotations.IdCityRangeError), ErrorMessageResourceType = typeof(DataAnotations))]
        public int? IdCity { get; set; } = 0;


        [DataType(DataType.Text)]
        [Required(ErrorMessageResourceType = typeof(DataAnotations), ErrorMessageResourceName = nameof(DataAnotations.IdErpShop))]
        [StringLength(25, ErrorMessageResourceType = typeof(DataAnotations), ErrorMessageResourceName = nameof(DataAnotations.IdErpLegthExceeded))]
        public String IdErpShop { get; set; }


        [DataType(DataType.Text)]
        [Required(ErrorMessageResourceType = typeof(DataAnotations), ErrorMessageResourceName = nameof(DataAnotations.NameIsRequired))]
        [StringLength(50, ErrorMessageResourceType = typeof(DataAnotations), ErrorMessageResourceName = nameof(DataAnotations.NameMaxLegthExceeded))]
        public String FullName { get; set; }


        [StringLength(75, ErrorMessageResourceType = typeof(DataAnotations), ErrorMessageResourceName = nameof(DataAnotations.AdressMaxLegthExceeded))]
        public String Address { get; set; }

        public double? Latitude { get; set; }
        public double? Longitude { get;set; }

        [StringLength(100, ErrorMessageResourceType = typeof(DataAnotations), ErrorMessageResourceName = nameof(DataAnotations.EmailMaxLegthExceeded))]
        [RegularExpression(DataAnotationConst.RegularExpresionEmail, ErrorMessageResourceType = typeof(DataAnotations), ErrorMessageResourceName = nameof(DataAnotations.EmailIncorrectFormat))]
        public String Email { get; set; }

        [StringLength(25, ErrorMessageResourceType = typeof(DataAnotations), ErrorMessageResourceName = nameof(DataAnotations.PhoneMaxLegthExceeded))]
        [RegularExpression(DataAnotationConst.RegularExpresionPhone, ErrorMessageResourceType = typeof(DataAnotations), ErrorMessageResourceName = nameof(DataAnotations.PhoneIncorrectFormat))]
        public String Phone { get; set; }

        [StringLength(25, ErrorMessageResourceType = typeof(DataAnotations), ErrorMessageResourceName = nameof(DataAnotations.PhoneMobileMaxLegthExceeded))]
        [RegularExpression(DataAnotationConst.RegularExpresionMobile, ErrorMessageResourceType = typeof(DataAnotations), ErrorMessageResourceName = nameof(DataAnotations.PhoneMobileIncorrectFormat))]
        public String Mobile { get; set; }

        public Boolean? HasPhoto { get; set; }

        public List<String> Pictures { get; set; }


    }
}