using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Utils.Contract.ServiceAccess.Http;
using static Utils.Contract.ServiceAccess.Http.EnumsServiceRequestHttp;

namespace Utils.ServicesAccess.Http
{
    public class ServiceRequestHttp : IServiceRequestHttp
    {
        /// <summary>
        /// Enumarable que contiene los metodos del protocolo http para esta
        /// clase
        /// </summary>
        private enum MethodHttp
        {
            POST,
            GET
        }

        /// <summary>
        /// Consturctor de la case
        /// </summary>
        public ServiceRequestHttp()
        {
        }



        /// <summary>
        /// Metoto encargado de enviar un post por medio de un contrato
        /// </summary>
        /// <param name="urlBase"></param>
        /// <param name="resourceBase"></param>
        /// <param name="parameters"></param>
        /// <param name="headers"></param>
        /// <param name="bodyRequest"></param>
        /// <param name="contentType"></param>
        /// <returns></returns>
        ResponseServiceRequest IServiceRequestHttp.Post(string urlBase, string resourceBase, List<Tuple<string, string>> parameters, List<Tuple<string, string>> headers, string bodySerilizer, ContentTypeBody contentType)
        {
            return Post(urlBase, resourceBase, parameters, headers, bodySerilizer, contentType);
        }
       

        /// <summary>
        /// metodo encargado de realizar un post a un servicio web
        /// </summary>
        /// <param name="urlBase"></param>
        /// <param name="resourceBase"></param>
        /// <param name="parameters"></param>
        /// <param name="headers"></param>
        /// <param name="bodySerilizer"></param>
        /// <param name="contentType"></param>
        /// <returns></returns>
        private ResponseServiceRequest Post(string urlBase, string resourceBase, List<Tuple<string, string>> parameters, List<Tuple<string, string>> headers, String bodySerilizer, ContentTypeBody contentType)
        {
            ResponseServiceRequest reponseServiceRequets = null;
            try
            {
                if (!String.IsNullOrEmpty(resourceBase) && !String.IsNullOrEmpty(urlBase))
                {
                    if (urlBase[urlBase.Length - 1] == '/' || urlBase[urlBase.Length - 1] == '\\')
                        urlBase = urlBase.Remove(urlBase.Length - 1);


                    if (resourceBase[0] == '/' || resourceBase[0] == '\\')
                        resourceBase = resourceBase.Remove(0);



                    urlBase = String.Concat(urlBase, "/", resourceBase);

                    urlBase = AddParameterUrl(urlBase, parameters);


                    HttpWebRequest request = (HttpWebRequest)WebRequest.Create(urlBase);

                    request.Method = MethodHttp.POST.ToString();

                    request.ContentType = GetContentTypeFromEnum(contentType);

                    if (headers != null && headers.Count > 0)
                    {
                        foreach (Tuple<String, String> header in headers)
                        {
                            request.Headers.Add(header.Item1, header.Item2);
                        }
                    }

                    if(String.IsNullOrEmpty(bodySerilizer))
                        request.ContentLength = 0;

                    if (bodySerilizer != null)
                    {
                        using (var streamWriter = new StreamWriter(request.GetRequestStream()))
                        {
                            streamWriter.Write(bodySerilizer);
                            streamWriter.Flush();
                            streamWriter.Close();
                        }
                    }


                    HttpWebResponse httpResponse = (HttpWebResponse)request.GetResponse();

                    reponseServiceRequets = new ResponseServiceRequest()
                    {
                        HttpStatusCode = httpResponse.StatusCode,
                    };

                    using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
                    {
                        reponseServiceRequets.StreamReaderResult = streamReader.ReadToEnd();
                        streamReader.Close();
                    }
                }
            }
            catch (WebException exceptionWeb)
            {
                reponseServiceRequets = GetResponseException(exceptionWeb);
            }
            catch(UriFormatException)
            {
                reponseServiceRequets = GetResponseExceptionUrlFormat();
            }
            return reponseServiceRequets;
        }



        /// <summary>
        /// Retorna error por error en el formato de url
        /// </summary>
        /// <returns></returns>
        private ResponseServiceRequest GetResponseExceptionUrlFormat()
        {
            return new ResponseServiceRequest()
            {
                HttpStatusCode = HttpStatusCode.BadRequest,
                HttpStatusWeb = WebExceptionStatus.SendFailure
            };
        }



        /// <summary>
        /// Metoto postAsyncrono encargado 
        /// </summary>
        /// <param name="urlBase"></param>
        /// <param name="resourceBase"></param>
        /// <param name="parameters"></param>
        /// <param name="headers"></param>
        /// <param name="bodySerilizer"></param>
        /// <param name="contentType"></param>
        /// <returns></returns>
        public async Task<ResponseServiceRequest> PostAsync(string urlBase, string resourceBase, List<Tuple<string, string>> parameters, List<Tuple<string, string>> headers, string bodySerilizer, ContentTypeBody contentType)
        {
            return await Task.Factory.StartNew(() => Post(urlBase, resourceBase, parameters, headers, bodySerilizer, contentType));
        }




        /// <summary>
        /// Metoto encargado de contruir los paramteros de uan url para 
        /// que sean añadidos a la url
        /// </summary>
        /// <param name="url"></param>
        /// <param name="parameters"></param>
        /// <returns></returns>
        private String AddParameterUrl(String url, List<Tuple<string, string>> parameters)
        {
            if (parameters != null && parameters.Count > 0)
            {
                int contadorPArametros = 0;

                foreach (Tuple<String, String> parameter in parameters)
                {
                    if (contadorPArametros == 0)
                        url = String.Concat(url, "?", parameter.Item1, "=", parameter.Item2);
                    else
                        url = String.Concat(url, "&", parameter.Item1, "=", parameter.Item2);

                    contadorPArametros++;
                }
            }
            return url;
        }

        /// <summary>
        /// Get description cotentType
        /// </summary>
        /// <param name="contentType"></param>
        /// <returns></returns>
        private string GetContentTypeFromEnum(ContentTypeBody contentType)
        {
            FieldInfo fi = contentType.GetType().GetField(contentType.ToString());
            DescriptionAttribute[] attributes = (DescriptionAttribute[])fi.GetCustomAttributes(typeof(DescriptionAttribute), false);
            if (attributes.Length > 0)
                return attributes[0].Description;
            else
                return contentType.ToString();
        }

        /// <summary>
        /// Metoto encargado de convertir 
        /// una  excepcion web 
        /// a una respuesta del servicio 
        /// </summary>
        /// <param name="exceptionWeb"></param>
        /// <returns></returns>
        private ResponseServiceRequest GetResponseException(WebException exceptionWeb)
        {
            return new ResponseServiceRequest()
            {
                HttpStatusCode = (exceptionWeb.Response as HttpWebResponse)?.StatusCode,
                HttpStatusWeb = exceptionWeb?.Status
            };
        }


        /// <summary>
        /// Metoto encargado de hacer una llamado e un 
        /// get para el protocolo http
        /// </summary>
        /// <param name="urlBase"></param>
        /// <param name="resourceBase"></param>
        /// <param name="parameters"></param>
        /// <param name="headers"></param>
        /// <returns></returns>
        public ResponseServiceRequest Get(string urlBase, string resourceBase, List<Tuple<string, string>> parameters, List<Tuple<string, string>> headers)
        {
            ResponseServiceRequest reponseServiceRequets = null;
            try
            {
                if (!String.IsNullOrEmpty(resourceBase) && !String.IsNullOrEmpty(urlBase))
                {
                    if (urlBase[urlBase.Length - 1] == '/' || urlBase[urlBase.Length - 1] == '\\')
                        urlBase = urlBase.Remove(urlBase.Length - 1);


                    if (resourceBase[0] == '/' || resourceBase[0] == '\\')
                        resourceBase = resourceBase.Remove(0);



                    urlBase = String.Concat(urlBase, "/", resourceBase);

                    urlBase = AddParameterUrl(urlBase, parameters);


                    HttpWebRequest request = (HttpWebRequest)WebRequest.Create(urlBase);

                    request.Method = MethodHttp.GET.ToString();


                    if (headers != null && headers.Count > 0)
                    {
                        foreach (Tuple<String, String> header in headers)
                        {
                            request.Headers.Add(header.Item1, header.Item2);
                        }
                    }


                    HttpWebResponse httpResponse = (HttpWebResponse)request.GetResponse();

                    reponseServiceRequets = new ResponseServiceRequest()
                    {
                        HttpStatusCode = httpResponse.StatusCode,
                    };

                    using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
                    {
                        reponseServiceRequets.StreamReaderResult = streamReader.ReadToEnd();
                        streamReader.Close();
                    }
                }
            }
            catch (WebException exceptionWeb)
            {
                reponseServiceRequets = GetResponseException(exceptionWeb);
            }
            return reponseServiceRequets;
        }

        /// <summary>
        /// Meopoto dencargado de llamar un get
        /// para una peticion asynctona 
        /// </summary>
        /// <param name="urlBase"></param>
        /// <param name="resourceBase"></param>
        /// <param name="parameters"></param>
        /// <param name="headers"></param>
        /// <returns></returns>
        public async Task<ResponseServiceRequest> GetAsync(string urlBase, string resourceBase, List<Tuple<string, string>> parameters, List<Tuple<string, string>> headers)
        {
            return await Task.Factory.StartNew(() => Get(urlBase, resourceBase, parameters, headers));
        }
    }
}
