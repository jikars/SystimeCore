using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SystimeDataAcces.DataAccess
{
    public class LogErpToSystime
    {

        /// <summary>
        /// Cadena de coneccion contra la base de datos
        /// </summary>
        private string ConectionString { get; set; }


        /// <summary>
        /// Consturctor de la clase
        /// </summary>
        public LogErpToSystime(String conectionString)
        {
            ConectionString = conectionString;
        }

        /// <summary>
        /// Metoto encargado de grabar o actualizar un registro de la base de
        /// datos 
        /// </summary>
        /// <param name="entityInsurancecompany"></param>
        /// <returns></returns>
        public Boolean SaveLogChargeErpToSystime()
        {

            //using (SystimedbEntities Systimedb = new SystimedbEntities(ConectionString))
            //{

            //    Systimedb..Add(new CloudCatalogInsuranceCompanies);
            //    if (Systimedb.SaveChanges() > 0)
            //        return true;


            //    return false;
            //}
            return false;

        }

    }
}
