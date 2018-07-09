using GlobalResources.ModelResources;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Models
{
    /// <summary>
    /// Modelo de datos para dealer representative
    /// </summary>
    public class DealerRepresentative
    {

        public int? IdDealerRepresentative { get; set; }


        [Required(ErrorMessageResourceType = typeof(DataAnotations), ErrorMessageResourceName = nameof(DataAnotations.IdDealerRequired))]
        [Range(1, int.MaxValue, ErrorMessageResourceName = nameof(DataAnotations.IdDealerRangeError), ErrorMessageResourceType = typeof(DataAnotations))]
        public int? IdDealer { get; set; } = 0;

        [Required(ErrorMessageResourceType = typeof(DataAnotations), ErrorMessageResourceName = nameof(DataAnotations.IdentityNumberIsRequired))]
        public String IdentityNumber { get; set; }


        [Required(ErrorMessageResourceType = typeof(DataAnotations), ErrorMessageResourceName = nameof(DataAnotations.IdCityRequired))]
        [Range(1, int.MaxValue, ErrorMessageResourceName = nameof(DataAnotations.IdCityRangeError), ErrorMessageResourceType = typeof(DataAnotations))]
        public int? IdCity { get; set; } = 0;


        [StringLength(2, ErrorMessageResourceType = typeof(DataAnotations), ErrorMessageResourceName = nameof(DataAnotations.IdContryMaxLegthExceeded))]
        public String IdCountry { get; set; }



        [Required(ErrorMessageResourceType = typeof(DataAnotations), ErrorMessageResourceName = nameof(DataAnotations.IdDealerShopRequired))]
        [Range(1, int.MaxValue, ErrorMessageResourceName = nameof(DataAnotations.IdDealerShopRangeError), ErrorMessageResourceType = typeof(DataAnotations))]
        public int? IdDealerShop { get; set; } = 0;

        [Required(ErrorMessageResourceType = typeof(DataAnotations), ErrorMessageResourceName = nameof(DataAnotations.IdErpDealerRepresentativeIsRequired))]
        [StringLength(50, ErrorMessageResourceType = typeof(DataAnotations), ErrorMessageResourceName = nameof(DataAnotations.IdErpDealerRepresentativeLegthExceeded))]
        public String IdErpRepresentative { get; set; }

        [Required(ErrorMessageResourceType = typeof(DataAnotations), ErrorMessageResourceName = nameof(DataAnotations.NameIsRequired))]
        [StringLength(50, ErrorMessageResourceType = typeof(DataAnotations), ErrorMessageResourceName = nameof(DataAnotations.MaxLegthExceeded))]
        public String FullName { get; set; }

        [StringLength(25, ErrorMessageResourceType = typeof(DataAnotations), ErrorMessageResourceName = nameof(DataAnotations.PhoneMaxLegthExceeded))]
        public String Phone { get; set; }



        [StringLength(25, ErrorMessageResourceType = typeof(DataAnotations), ErrorMessageResourceName = nameof(DataAnotations.PhoneMobileMaxLegthExceeded))]
         public String Mobile { get; set; }

        [StringLength(100, ErrorMessageResourceType = typeof(DataAnotations), ErrorMessageResourceName = nameof(DataAnotations.EmailMaxLegthExceeded))]
        [RegularExpression(DataAnotationConst.RegularExpresionEmail, ErrorMessageResourceType = typeof(DataAnotations), ErrorMessageResourceName = nameof(DataAnotations.EmailIncorrectFormat))]
        public String Email { get; set; }

        public Boolean? HasPhoto { get; set; }
        public  List<String> Pictures { get; set; }

    }
}