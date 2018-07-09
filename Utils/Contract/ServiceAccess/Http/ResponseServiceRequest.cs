using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Utils.Contract.ServiceAccess.Http
{
    /// <summary>
    /// Clase de respuesta para el servicio web
    /// donde responde el codigo de estado del protocolo
    /// http y la respuesta completa como un string 
    /// </summary>
    public class ResponseServiceRequest
    {
        public HttpStatusCode? HttpStatusCode { get; set; }

        public WebExceptionStatus? HttpStatusWeb { get; set; }
        public String StreamReaderResult { get; set; }
    }
}
