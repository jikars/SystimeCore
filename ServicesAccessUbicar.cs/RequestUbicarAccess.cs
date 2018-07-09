using ServicesAccessUbicar.cs.Contract;
using System;
using System.Threading.Tasks;
using Models;
using Unity;
using Utils.Contract.ServiceAccess.Http;
using Utils.ServicesAccess.Http;
using Newtonsoft.Json;
using static Utils.Contract.ServiceAccess.Http.EnumsServiceRequestHttp;
using static Models.ResponseServices.MessageError.CodeResponseServices;

namespace ServicesAccessUbicar.cs
{
    public class RequestUbicarAccess : IServiceClientUbicar
    {  /// <summary>
       /// Variable encargada de resolver depencias y actua
       /// como contenedor de dependencias
       /// </summary>
        private UnityContainer ContainerUnity { get; set; }
        /// <summary>
        /// Interface que implementa la logica
        /// del servicio sirve como contrato
        /// </summary>
        private readonly IServiceRequestHttp iServiceRequest;


        /// <summary>
        /// Objeto de respuesta del servicio weeb 
        /// </summary>
        private Response ResponseWs = null;

        /// <summary>
        /// Variable encargad de almacenar 
        /// si se responde el modelo de datos o no 
        /// </summary>
        private Boolean HasModelResponse = false;


        public RequestUbicarAccess()
        {
            ContainerUnity = new UnityContainer();
            ContainerUnity.RegisterType<IServiceRequestHttp, ServiceRequestHttp>();
            iServiceRequest = ContainerUnity.Resolve<IServiceRequestHttp>();
        }


        /// <summary>
        /// Metotoe enccargado de convertir cualquier objeto respuesto por el servicio web
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public T CastObjectResponse<T>()
        {
            if (ResponseWs?.ResponseObject != null && HasModelResponse)
                return JsonConvert.DeserializeObject<T>(JsonConvert.SerializeObject(ResponseWs?.ResponseObject));
            return default(T);
        }

        /// <summary>
        /// Metotoe ncargado de grabar un objeto en el servicio web
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="Obj"></param>
        /// <param name="hasModelResponse"></param>
        /// <param name="urlService"></param>
        /// <param name="language"></param>
        /// <param name="token"></param>
        /// <returns></returns>
        public Response Save<T>(T Obj, bool hasModelResponse, string urlService, string language, string token)
        {
            HasModelResponse = hasModelResponse;
            ResponseWs = UtilsSevicesAcceessUbicar.CastResponseService(iServiceRequest.Post(urlService, UtilsSevicesAcceessUbicar.ResolverUrlSourceBaseByTypeModel<T>(), UtilsSevicesAcceessUbicar.GetParameters(hasModelResponse), UtilsSevicesAcceessUbicar.GetHeaders(language, token), JsonConvert.SerializeObject(Obj), ContentTypeBody.Json), language);
            return ResponseWs;
        }


        /// <summary>
        /// Metotoe ncargado de grabar un objeto en servicio 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="Obj"></param>
        /// <param name="hasModelResponse"></param>
        /// <param name="urlService"></param>
        /// <param name="language"></param>
        /// <param name="token"></param>
        /// <returns></returns>
        public async Task<Response> SaveAsync<T>(T Obj, bool hasModelResponse, string urlService, string language, string token)
        {
            HasModelResponse = hasModelResponse;
            ResponseWs = UtilsSevicesAcceessUbicar.CastResponseService(await iServiceRequest.PostAsync(urlService, UtilsSevicesAcceessUbicar.ResolverUrlSourceBaseByTypeModel<T>(), UtilsSevicesAcceessUbicar.GetParameters(hasModelResponse), UtilsSevicesAcceessUbicar.GetHeaders(language, token), JsonConvert.SerializeObject(Obj), ContentTypeBody.Json), language);
            return ResponseWs;
        }

        public int? CodErrorRequest()
        {
            if (ResponseWs?.ResponseObject != null)
                return ResponseWs.CodError;
            return null;
        }

        public string MessageError()
        {
            if (!String.IsNullOrEmpty(ResponseWs?.MessageErrorFromUser))
                return ResponseWs?.MessageErrorFromUser;

            return null;
        }
    }
}
