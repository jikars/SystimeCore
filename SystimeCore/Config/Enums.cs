using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SystimeCore.Config
{
    public static class Enums
    {
        public enum TableName
        {
            WORK_ORDERS, WORKERS, VEHICLES,
            CUSTOMER,
            INSURANCE_COMPANY
        }

        public enum TableAction
        {
            I, U,D
        }
    }
 }
