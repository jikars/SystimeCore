using GlobalResources.ModelResources;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Models
{
    /// <summary>
    /// Modelo de datos para vehicle
    /// </summary>
    public class Vehicle
    {
        public long? IdVehicle { get; set; }

        [DataType(DataType.Text)]
        [Required(ErrorMessageResourceType = typeof(DataAnotations), ErrorMessageResourceName = nameof(DataAnotations.CountryRequerid))]
        [StringLength(2, ErrorMessageResourceType = typeof(DataAnotations), ErrorMessageResourceName = nameof(DataAnotations.IdContryMaxLegthExceeded))]
        public String IdCountry { get; set; }

        [Range(1, int.MaxValue, ErrorMessageResourceName = nameof(DataAnotations.IdCityRangeError), ErrorMessageResourceType = typeof(DataAnotations))]
        public int? IdCity { get; set; }

        [Range(1, int.MaxValue, ErrorMessageResourceName = nameof(DataAnotations.IdVehcileRangeError), ErrorMessageResourceType = typeof(DataAnotations))]
        public int? IdVehicleModel { get; set; }

        [Range(1, int.MaxValue, ErrorMessageResourceName = nameof(DataAnotations.ModelYearRangeError), ErrorMessageResourceType = typeof(DataAnotations))]
        public int? ModelYear { get; set; }

        [Range(1, int.MaxValue, ErrorMessageResourceName = nameof(DataAnotations.IdInsuranceCompanyRangeError), ErrorMessageResourceType = typeof(DataAnotations))]
        public int? IdInsuranceCompany { get; set; }

        [Range(1, int.MaxValue, ErrorMessageResourceName = nameof(DataAnotations.IdFuelTypeRangeError), ErrorMessageResourceType = typeof(DataAnotations))]
        public int? IdFuelType { get; set; }


        [Range(1, int.MaxValue, ErrorMessageResourceName = nameof(DataAnotations.IdVehicleTypeRangeError), ErrorMessageResourceType = typeof(DataAnotations))]
        public int? IdVehicleType { get; set; }


        [Range(1, int.MaxValue, ErrorMessageResourceName = nameof(DataAnotations.IdVehiccleServiceTypeRangeError), ErrorMessageResourceType = typeof(DataAnotations))]
        public int? IdVehicleServiceType { get; set; }

        [Range(1, int.MaxValue, ErrorMessageResourceName = nameof(DataAnotations.IdDealerRangeError), ErrorMessageResourceType = typeof(DataAnotations))]
        public int? IdDealer { get; set; }

        [DataType(DataType.Text)]
        [Required(ErrorMessageResourceType = typeof(DataAnotations), ErrorMessageResourceName = nameof(DataAnotations.VinRequired))]
        [StringLength(50, ErrorMessageResourceType = typeof(DataAnotations), ErrorMessageResourceName = nameof(DataAnotations.VinMaxLegthExceeded))]
        public String Vin { get; set; }


        [DataType(DataType.Text)]
        [Required(ErrorMessageResourceType = typeof(DataAnotations), ErrorMessageResourceName = nameof(DataAnotations.PlateRequired))]
        [StringLength(20, ErrorMessageResourceType = typeof(DataAnotations), ErrorMessageResourceName = nameof(DataAnotations.PlateMaxLegthExceeded))]
        public String Plate { get; set; }

        public string VehicleBrand { get; set; }

        public string VehicleModel { get; set; }

        public Boolean? HasPhoto { get; set; }


        public List<String> Pictures { get; set; }
    }
}