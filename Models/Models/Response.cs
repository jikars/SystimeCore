using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Models
{
    /// <summary>
    /// Modelo de datos implementado 
    /// cuando pueden ocurrir diferente objetos como respuesta
    /// ya sean errores como respuestas de diferente tipo sobre un 
    /// action result
    /// </summary>
    public class Response
    {
        public int? CodError { get; set; }
        public String MessageErrorFromUser { get; set; }

        public Object ResponseObject { get; set; }
    }


   
}