using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Utils.Contract.ServiceAccess.Http
{
    public  class EnumsServiceRequestHttp
    {
        /// <summary>
        /// Enumerables tipo de 
        /// contenido
        /// </summary>
        public enum ContentTypeBody
        {
            [Description("application/json; charset=utf-8")]
            Json,
            [Description("application/x-www-form-urlencoded")]
            FormUrlencoded
        }
    }
}
