using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SystimeDataAcces.DataAccess;

namespace IntegrateErpToSystime.ModelIntegrate
{
    public class CustomerIntegrate
    {
        public Customers CustomerSystime { get; set; }
        public Person CustomerUbicar { get; set; }

    }
}
