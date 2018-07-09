using GlobalResources.ModelResources;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;

namespace Models
{
    public class Person
    {
        public long? IdPerson { get; set; }


        [Range(1, int.MaxValue, ErrorMessageResourceName = nameof(DataAnotations.IdCityRangeError), ErrorMessageResourceType = typeof(DataAnotations))]
        public int? IdCity { get; set; }


        [StringLength(2, ErrorMessageResourceType = typeof(DataAnotations), ErrorMessageResourceName = nameof(DataAnotations.IdContryMaxLegthExceeded))]
        public String IdCountry { get; set; }


        public String IdentityNumber { get; set; }


        [Range(1, int.MaxValue, ErrorMessageResourceName = nameof(DataAnotations.IdRhRageError), ErrorMessageResourceType = typeof(DataAnotations))]
        public int? IdRH { get; set; }

        [Range(1, int.MaxValue, ErrorMessageResourceName = nameof(DataAnotations.IdGenderRangeError), ErrorMessageResourceType = typeof(DataAnotations))]
        public int? IdGender { get; set; }

        

        [StringLength(75, ErrorMessageResourceType = typeof(DataAnotations), ErrorMessageResourceName = nameof(DataAnotations.NameMaxLegthExceeded))]
        public String FullName { get; set; }

        [StringLength(75, ErrorMessageResourceType = typeof(DataAnotations), ErrorMessageResourceName = nameof(DataAnotations.AdressMaxLegthExceeded))]
        public String Address { get; set; }

        [StringLength(100, ErrorMessageResourceType = typeof(DataAnotations), ErrorMessageResourceName = nameof(DataAnotations.EmailMaxLegthExceeded))]
        [RegularExpression(DataAnotationConst.RegularExpresionEmail, ErrorMessageResourceType = typeof(DataAnotations), ErrorMessageResourceName = nameof(DataAnotations.EmailIncorrectFormat))]
        public String Email { get; set; }


        public DateTime? Birhdate { get; set; }

        public Boolean? HasPhoto { get; set; }


        public List<String> Pictures { get; set; }

    }
}
