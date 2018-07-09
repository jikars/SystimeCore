using IntegrateErpToSystime;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using SystimeCore.Managers;
using static SystimeCore.Config.Enums;

namespace SystimeCore
{
    internal static class UtilsSystimeCore
    {
        private readonly static Dictionary<TableName, Type> IntanceFromTable = null;



        /// <summary>
        /// Cosntructor estatico
        /// </summary>
        static UtilsSystimeCore()
        {
            if (IntanceFromTable == null)
            {
                IntanceFromTable = new Dictionary<TableName, Type>
                {
                    { TableName.WORK_ORDERS,  typeof(WorkOrderManager) },
                    { TableName.CUSTOMER, typeof(CustomerManager) },
                    { TableName.INSURANCE_COMPANY, typeof(InsuranceCompanyManager) }
                };
            }
        }


        /// <summary>
        /// MEtto encargado de resolver la instancia por el espacio de nombre de un table 
        /// </summary>
        /// <param name="tablename"></param>
        /// <returns></returns>
        public static T ResolverIntanceFromTable<T>(TableName tablename)
        {
            if (IntanceFromTable.ContainsKey(tablename))
            {
                Object Obj = Activator.CreateInstance(IntanceFromTable.FirstOrDefault(t => t.Key == tablename).Value);

                if (Obj is T)
                    return (T)Obj;
            }
                    
            return default(T);
        }
    }
}
