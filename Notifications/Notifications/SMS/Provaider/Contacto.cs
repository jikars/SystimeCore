using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Utils.Contract.ServiceAccess.Http;
using Utils.ServicesAccess.Http;

namespace Notifications.Notifications.SMS.Provaider
{
    public class Contacto : IProvaiderSms
    {
        private IServiceRequestHttp ServiceRequestHttp { get; set; }

        public Contacto()
        {
            ServiceRequestHttp = new ServiceRequestHttp();
        }

        public bool SendSms(string destinatiion, string message, string title, string jsonConfig, out string errorMessage)
        {
            return Send(destinatiion, message, title, JsonConvert.DeserializeObject<ConfigMsMFromContacto>(jsonConfig), out errorMessage);
        }

        public bool SendSmsAll(string[] destinatiion, string message, string title, string jsonConfig, out string errorMessage)
        {
            throw new NotImplementedException();
        }
        public async  Task<bool> SendSmsAsync(string destinatiion, string message, string title, string jsonConfig)
        {
            return await SendAsync(destinatiion, message, title, JsonConvert.DeserializeObject<ConfigMsMFromContacto>(jsonConfig));
        }

        public Task<bool> SendSmsAllAsync(string[] destinatiion, string message, string title, string jsonConfig)
        {
            throw new NotImplementedException();
        }

        private Boolean Send(string destinatiion, string message, string title, ConfigMsMFromContacto jsonConfig, out string errorMessage)
        {

            errorMessage = "Not operation contacto provaider";
            List<Tuple<String, String>> parameters = new List<Tuple<String, String>>
            {
                new Tuple<string, string>("usuario", jsonConfig.User),
                new Tuple<string, string>("clave", jsonConfig.Password),
                new Tuple<string, string>("numero", destinatiion),
                new Tuple<string, string>("mensaje", message),
                new Tuple<string, string>("nota", title)
            };

            ResponseServiceRequest rsponseService = ServiceRequestHttp.Get(jsonConfig.PthBase, jsonConfig.ResourceBase, parameters, null);
            if (rsponseService.HttpStatusCode.HasValue && rsponseService.HttpStatusCode == HttpStatusCode.OK)
            {
                errorMessage = rsponseService.StreamReaderResult;
                return rsponseService.StreamReaderResult.Contains("DATO CARGADO EXITOSAMENTE");
            }


            return false;
        }

        private async Task<Boolean> SendAsync(string destinatiion, string message, string title, ConfigMsMFromContacto jsonConfig)
        {
            List<Tuple<String, String>> parameters = new List<Tuple<String, String>>
            {
                new Tuple<string, string>("usuario", jsonConfig.User),
                new Tuple<string, string>("clave", jsonConfig.Password),
                new Tuple<string, string>("numero", destinatiion),
                new Tuple<string, string>("mensaje", message),
                new Tuple<string, string>("nota", title)
            };
            ResponseServiceRequest serviceRequest = await ServiceRequestHttp.GetAsync(jsonConfig.PthBase, jsonConfig.ResourceBase, parameters, null);
            if (serviceRequest != null && serviceRequest.HttpStatusCode.HasValue && serviceRequest.HttpStatusCode.Value == HttpStatusCode.OK)
                return true;

            return false;
        }


    }

    public class ConfigMsMFromContacto
    {
        public String  User { get; set; }
        public String Password { get; set; }

        public String PthBase { get; set; }

        public String ResourceBase { get; set; }


    }
}
