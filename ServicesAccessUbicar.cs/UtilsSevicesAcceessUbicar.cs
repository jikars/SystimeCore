using Models;
using Newtonsoft.Json;
using ServicesAccessUbicar.cs.Resources;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Utils.Contract.ServiceAccess.Http;

namespace ServicesAccessUbicar.cs
{
    internal static class UtilsSevicesAcceessUbicar
    {
        private readonly static Dictionary<Type, String> UrlResolve = null;


        /// <summary>
        /// Cosntructor estatico
        /// </summary>
        static UtilsSevicesAcceessUbicar()
        {
            if (UrlResolve == null)
            {
                UrlResolve = new Dictionary<Type, string>
                {
                    { typeof(WorkOrder), "api/WorkOrder" },
                    { typeof(Vehicle), "api/vehicle" },
                    { typeof(Person), "api/person" },
                    { typeof(DealerRepresentative), "api/dealerrepresnetaive" },
                    { typeof(DealerShop), "api/dealershop" },
                    { typeof(Dealer), "api/dealer" },
                    { typeof(InsuranceCompany), "api/insurancecompany" },
                };
            }
        }


        /// <summary>
        /// Tipo de errroes provenientes desde el servicio web 
        /// </summary>


        /// <summary>
        /// Metoto encargado de resolver los parametros que 
        /// se envia en las petciones a ubicar servicio web 
        /// </summary>
        /// <param name="hasModelResponse"></param>
        /// <returns></returns>
        internal static List<Tuple<string, string>> GetParameters(Boolean hasModelResponse)
        {
            List<Tuple<string, string>> parameters = new List<Tuple<string, string>>();
            Tuple<string, string> parameter1 = new Tuple<string, string>("hasModelResponse", hasModelResponse.ToString().ToLower());
            parameters.Add(parameter1);
            return parameters;
        }

        /// <summary>
        /// Metoto encargado de obtener los headers 
        /// </summary>
        /// <param name="hasModelResponse"></param>
        /// <returns></returns>
        internal static List<Tuple<string, string>> GetHeaders(String language, String token)
        {
            List<Tuple<string, string>> Headers = new List<Tuple<string, string>>();
            if (!String.IsNullOrEmpty(language))
            {
                Tuple<string, string> header1 = new Tuple<string, string>("LANGUAGE", language);
                Headers.Add(header1);
            }

            if (!String.IsNullOrEmpty(token))
            {
                Tuple<string, string> header1 = new Tuple<string, string>("X-ZUMO-AUTH", token);
                Headers.Add(header1);
            }

            Tuple<string, string> header2 = new Tuple<string, string>("TIME-ZONE", ResolveCurrentTimeZone());
            Headers.Add(header2);

            return Headers;
        }


        /// <summary>
        /// Metoto encargado de convertir cualquier respuesta de
        /// la libreria de consumo de servicios a un objeto de tipo response 
        /// </summary>
        /// <returns></returns>
        internal static Response CastResponseService(ResponseServiceRequest ObjectResponse, String language)
        {
            ChangeLanguage(language);

            if (ObjectResponse.HttpStatusCode == HttpStatusCode.OK ||
               ObjectResponse.HttpStatusCode == HttpStatusCode.Created || ObjectResponse.HttpStatusCode == HttpStatusCode.Accepted)
                return JsonConvert.DeserializeObject<Response>(ObjectResponse?.StreamReaderResult);
            else if (ObjectResponse.HttpStatusCode != null)
                return AjustResponseFromStatusCode(ObjectResponse.HttpStatusCode);
            else
                return AjustResponseFromHttpStatus(ObjectResponse.HttpStatusWeb);
        }


        /// <summary>
        /// Metoto encargado de cambiar el lenguage 
        /// </summary>
        /// <param name="language"> id del lenguage</param>
        private static void ChangeLanguage(String language)
        {
            CultureInfo cultureInfo = new CultureInfo(language);
            Message.Culture = cultureInfo;
        }


        /// <summary>
        /// Metopto encargado de crear una respuesta a partir
        /// de httpStatus
        /// </summary>
        /// <param name="httpStatus"></param>
        /// <returns></returns>
        private static Response AjustResponseFromHttpStatus(WebExceptionStatus? httpStatus)
        {
            String messageError = "";
            int? codError = 600;
            codError += httpStatus?.GetHashCode() ?? 1000;


            switch (httpStatus)
            {
                case WebExceptionStatus.ConnectFailure:
                case WebExceptionStatus.ConnectionClosed:
                case WebExceptionStatus.KeepAliveFailure:
                    messageError = Message.ConnectFailure;
                    break;
                case WebExceptionStatus.Timeout:
                    messageError = Message.RequestTimeout;
                    break;
                case WebExceptionStatus.MessageLengthLimitExceeded:
                    messageError = Message.MessageLengthLimitExceeded;
                    break;
                case WebExceptionStatus.NameResolutionFailure:
                case WebExceptionStatus.ProxyNameResolutionFailure:
                case WebExceptionStatus.Pending:
                case WebExceptionStatus.ReceiveFailure:
                case WebExceptionStatus.ServerProtocolViolation:
                case WebExceptionStatus.TrustFailure:
                    messageError = Message.ServiceError;
                    break;
                case WebExceptionStatus.PipelineFailure:
                case WebExceptionStatus.ProtocolError:
                case WebExceptionStatus.SecureChannelFailure:
                    messageError = Message.ComunicationError;
                    break;
                case WebExceptionStatus.RequestCanceled:
                case WebExceptionStatus.SendFailure:
                    messageError = Message.RequestCancel;
                    break;
                case WebExceptionStatus.RequestProhibitedByCachePolicy:
                case WebExceptionStatus.RequestProhibitedByProxy:
                case WebExceptionStatus.UnknownError:
                    messageError = Message.RequestError;
                    break;
                default:
                    messageError = Message.ComunicationError;
                    break;
            }
            return new Response()
            {
                CodError = codError ?? 1000,
                MessageErrorFromUser = messageError
            };
        }


        /// <summary>
        /// Metoto encarrgado de ajustar una 
        /// Respuesta para el servicio a partir del 
        /// del ccodigo de estado del protocolo http
        /// </summary>
        /// <param name="statusCode"></param>
        /// <returns></returns>
        private static Response AjustResponseFromStatusCode(HttpStatusCode? statusCode)
        {
            String messageError = "";
            int? codError = statusCode?.GetHashCode() ?? 1000;
            switch (statusCode)
            {

                //Errores del cliente
                case HttpStatusCode.BadRequest:
                case HttpStatusCode.MethodNotAllowed:
                case HttpStatusCode.LengthRequired:
                case HttpStatusCode.RequestEntityTooLarge:
                case HttpStatusCode.RequestUriTooLong:
                case HttpStatusCode.UnsupportedMediaType:
                case HttpStatusCode.RequestedRangeNotSatisfiable:
                case HttpStatusCode.ExpectationFailed:
                    messageError = Message.RequestError;
                    break;
                case HttpStatusCode.Unauthorized:
                case HttpStatusCode.Forbidden:
                case HttpStatusCode.ProxyAuthenticationRequired:
                    messageError = Message.Unauthorized;
                    break;
                case HttpStatusCode.PaymentRequired:
                case HttpStatusCode.NotAcceptable:
                case HttpStatusCode.Conflict:
                    messageError = Message.ComunicationError;
                    break;
                case HttpStatusCode.NotFound:
                case HttpStatusCode.Gone:
                    messageError = Message.NotFound;
                    break;
                case HttpStatusCode.RequestTimeout:
                    messageError = Message.RequestTimeout;
                    break;
                case HttpStatusCode.PreconditionFailed:
                    messageError = Message.ServiceError;
                    break;
                //Errroes del servidor
                case HttpStatusCode.InternalServerError:
                case HttpStatusCode.NotImplemented:
                case HttpStatusCode.BadGateway:
                case HttpStatusCode.GatewayTimeout:
                case HttpStatusCode.HttpVersionNotSupported:
                    messageError = Message.ServerInternalError;
                    break;
                case HttpStatusCode.ServiceUnavailable:
                    messageError = Message.ServiceUnavailable;
                    break;
                default:
                    messageError = Message.ServerInternalError;
                    break;

            }

            return new Response()
            {
                CodError = codError ?? 1000,
                MessageErrorFromUser = messageError
            };
        }

        /// <summary>
        /// Metoto encargado de resolver la url del servicio web 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static String ResolverUrlSourceBaseByTypeModel<T>()
        {
            if (UrlResolve.ContainsKey(typeof(T)))
                return UrlResolve.FirstOrDefault(t => t.Key == typeof(T)).Value;

            return String.Empty;
        }

        /// <summary>
        /// Metoto encargado de entregar la zona horarioa
        /// </summary>
        /// <returns></returns>
        private static String ResolveCurrentTimeZone()
        {
            return TimeZone.CurrentTimeZone.StandardName;
        }
    }

}

