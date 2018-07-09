using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SystimeDataAcces.DataAccess;

namespace IntegrateErpToSystime.ModelIntegrate
{
    public class InsuranceCompanyIntegrate
    {
        public CloudCatalogInsuranceCompanies InsuranceCompnaySystime { get; set; }
        public Models.InsuranceCompany InsuranceCompnayUbicar { get; set; }


    }
}
