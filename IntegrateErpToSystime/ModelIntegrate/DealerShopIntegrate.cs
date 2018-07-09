using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SystimeDataAcces.DataAccess;

namespace IntegrateErpToSystime.ModelIntegrate
{
    public class DealerShopIntegrate
    {
        public CloudCatalogDealerShops DealerShopSystime { get; set; }
        public Models.DealerShop DealerShopUbicar { get; set; }
    }
}
