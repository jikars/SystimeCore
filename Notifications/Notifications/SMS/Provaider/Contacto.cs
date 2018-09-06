using Newtonsoft.Json;
using Notifications.Notifications.Config;
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

        public bool SendSms(string destinatiion, SmsConfig config, string jsonProviderConfig, out string errorMessage)
        {
            errorMessage = "Not object config sms";
            if (config != null)
            {
                return Send(destinatiion, config, JsonConvert.DeserializeObject<ConfigMsMFromContacto>(jsonProviderConfig), out errorMessage);
            }
            return false;
        }

        public bool SendAll(string[] destinatiions, SmsConfig config, string jsonProviderConfig, out string errorMessage)
        {
            throw new NotImplementedException();
        }

        public Task<bool> SendAsync(string destinatiion, SmsConfig config, string jsonProviderConfig)
        {
            throw new NotImplementedException();
        }

        public Task<bool> SendAllAsync(string[] destinatiions, SmsConfig config, string jsonProviderConfig)
        {
            throw new NotImplementedException();
        }


        private Boolean Send(string destinatiion, SmsConfig config, ConfigMsMFromContacto jsonConfig, out string errorMessage)
        {

            errorMessage = "Not operation contacto provaider";
            List<Tuple<String, String>> parameters = new List<Tuple<String, String>>
             {
                 new Tuple<string, string>("usuario", jsonConfig.User),
                 new Tuple<string, string>("clave", jsonConfig.Password),
                 new Tuple<string, string>("numero", destinatiion),
                 new Tuple<string, string>("mensaje", config.Message),
                 new Tuple<string, string>("nota", config.Title)
             };

            ResponseServiceRequest rsponseService = ServiceRequestHttp.Get(jsonConfig.PthBase, jsonConfig.ResourceBase, parameters, null);
            if (rsponseService.HttpStatusCode.HasValue && rsponseService.HttpStatusCode == HttpStatusCode.OK)
            {
                errorMessage = rsponseService.StreamReaderResult;
                return rsponseService.StreamReaderResult.Contains("DATO CARGADO EXITOSAMENTE");
            }


            return false;
        }


        private async Task<Boolean> SendAsync(string destinatiion, SmsConfig config, ConfigMsMFromContacto jsonConfig)
        {
            List<Tuple<String, String>> parameters = new List<Tuple<String, String>>
             {
                 new Tuple<string, string>("usuario", jsonConfig.User),
                 new Tuple<string, string>("clave", jsonConfig.Password),
                 new Tuple<string, string>("numero", destinatiion),
                 new Tuple<string, string>("mensaje", config.Message),
                 new Tuple<string, string>("nota", config.Title)
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
