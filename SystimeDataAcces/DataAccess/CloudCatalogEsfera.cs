using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SystimeDataAcces.DataAccess
{
    public  class CloudCatalogEsfera
    {
        /// <summary>
        /// Cadena de coneccion contra la bse de datos
        /// </summary>
        private string ConectionString { get; set; }


        /// <summary>
        /// Cosntrucotyr de la clase
        /// </summary>
        public CloudCatalogEsfera(String conectionString)
        {
            ConectionString = conectionString;
        }


        /// <summary>
        /// Metoto encargado de obtener el path con el cual se
        /// construira la url
        /// </summary>
        /// <param name="idPath"></param>
        /// <returns></returns>
        public GET_PATH_Result GetPahtById(String idPath)
        {
            using (SystimedbEntities SystimeDb = new SystimedbEntities(ConectionString))
            {
                return SystimeDb.GET_PATH(idPath).FirstOrDefault();
            }
        }

    }
}
