using GlobalResources.ModelResources;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Models
{
    public class WorkOrder
    {
        public long? IdWorkOrder { get; set; }


        public long? IdVehicle { get; set; }

        [Range(1, int.MaxValue, ErrorMessageResourceName = nameof(DataAnotations.IdInsuranceCompanyRangeError), ErrorMessageResourceType = typeof(DataAnotations))]
        public int? IdInsuraceConpany { get; set; }


        [Required(ErrorMessageResourceType = typeof(DataAnotations), ErrorMessageResourceName = nameof(DataAnotations.IdDealerShopRequired))]
        [Range(1, int.MaxValue, ErrorMessageResourceName = nameof(DataAnotations.IdDealerShopRangeError), ErrorMessageResourceType = typeof(DataAnotations))]
        public int? IdDealerShop { get; set; }


        [Required(ErrorMessageResourceType = typeof(DataAnotations), ErrorMessageResourceName = nameof(DataAnotations.IdCustomerRequeired))]
        [Range(1, long.MaxValue, ErrorMessageResourceName = nameof(DataAnotations.IdCustomerRangeError), ErrorMessageResourceType = typeof(DataAnotations))]
        public long? IdCustomer { get; set; }


        [Required(ErrorMessageResourceType = typeof(DataAnotations), ErrorMessageResourceName = nameof(DataAnotations.IdDealerRepresentativeRequired))]
        [Range(1, int.MaxValue, ErrorMessageResourceName = nameof(DataAnotations.IdDealerRepresentativeRangeError), ErrorMessageResourceType = typeof(DataAnotations))]
        public int? IdDealerRepresentative { get; set; }


        [StringLength(100, ErrorMessageResourceType = typeof(DataAnotations), ErrorMessageResourceName = nameof(DataAnotations.ProggresMessageLengthExceeded))] 
        public String ProgressMessaje { get; set; }


        [StringLength(50, ErrorMessageResourceType = typeof(DataAnotations), ErrorMessageResourceName = nameof(DataAnotations.IdErpQuoteLegthExceeded))]
        public String IdErpQuote { get; set; }



        [StringLength(100, ErrorMessageResourceType = typeof(DataAnotations), ErrorMessageResourceName = nameof(DataAnotations.NoteLegthExceeded))]
        public String Note { get; set; }


        [StringLength(50, ErrorMessageResourceType = typeof(DataAnotations), ErrorMessageResourceName = nameof(DataAnotations.IdErpWorkOrderLeghtExceeded))]
        public String WorkOrderIdErp { get; set; }

        public DateTime? EnteredAt { get; set; }
        public DateTime? PromisedAt { get; set; }
        public DateTime? AutorizedAt { get; set; }

        public DateTime? PartsCompletedAt { get; set; }

        public Boolean? IsTotalLoss { get; set; }

        [StringLength(150, ErrorMessageResourceType = typeof(DataAnotations), ErrorMessageResourceName = nameof(DataAnotations.FeedbackLeghtExceeded))]
        public String Feedback { get; set; }

        [Range(0, 5, ErrorMessageResourceName = nameof(DataAnotations.ShopRaitngRangeError), ErrorMessageResourceType = typeof(DataAnotations))]
        public Int16? ShopRating { get; set; }

        [Range(0, 5, ErrorMessageResourceName = nameof(DataAnotations.InsuranceCompanyRatingRangeError), ErrorMessageResourceType = typeof(DataAnotations))]
        public Int16? InsuranceCompanyRating { get; set; }

        public DateTime? ReadyToBePickedUpAt { get; set; }


        public DateTime? PickupConfrimatedAt { get; set; }

        public DateTime? PickedUpAt { get; set; }


        public Boolean? HasPhoto { get; set; }

        public List<String> Pictures { get; set; }


     }   
}