using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Notifications.Notifications.Config;
using Utils.Contract.ServiceAccess.Http;
using Utils.ServicesAccess.Http;

namespace Notifications.Notifications.Whatsapp.Provider
{
    class WaboxApp : IWhatsappProvider
    {

        private IServiceRequestHttp ServiceRequestHttp { get; set; }


        private static Dictionary<TypeSend, String> Dictionary = new Dictionary<TypeSend, string>()
        {
                { TypeSend.Text, "api/send/chat" },
                { TypeSend.Image, "api/send/image" },
                { TypeSend.Media, "api/send/media" },
        };


        public WaboxApp()
        {
            ServiceRequestHttp = new ServiceRequestHttp();
        }

        public bool SendAll(string[] destinatiions, WhatsAppConfig config, string jsonProviderConfig, out string errorMessage)
        {
            throw new NotImplementedException();
        }

        public Task<bool> SendAllAsync(string[] destinatiions, WhatsAppConfig config, string jsonProviderConfig)
        {
            throw new NotImplementedException();
        }

        public Task<bool> SendAsync(string destinatiion, WhatsAppConfig config, string jsonProviderConfig)
        {
            throw new NotImplementedException();
        }

        public bool SendWhatsApp(string destinatiion, WhatsAppConfig config, string jsonProviderConfig, out string errorMessage)
        {
            errorMessage = "Not object config sms";
            if (config != null)
            {
                return Send(destinatiion, config, JsonConvert.DeserializeObject<ConfigWahtsAppFromWabBox>(jsonProviderConfig), out errorMessage);
            }
            return false;
        }
        private Boolean Send(string destinatiion, WhatsAppConfig config, ConfigWahtsAppFromWabBox jsonConfig, out string errorMessage)
        {

            errorMessage = "Not operation WabappBox provaider";
            List<Tuple<String, String>> parameters = new List<Tuple<String, String>>
             {
                 new Tuple<string, string>("key", jsonConfig.Key),
                 new Tuple<string, string>("uid", jsonConfig.Phone),
                 new Tuple<string, string>("to", destinatiion),
                 new Tuple<string, string>("custom_uid", DateTime.Now.ToFileTime().ToString()),
             };

            switch (config.TypeSend)
            {
                case TypeSend.Text:
                    return SendText(jsonConfig.PathBase, parameters, config);
                case TypeSend.Image:
                    return SendImage(jsonConfig.PathBase, parameters, config);
                case TypeSend.TextImage:
                    return SendTextImage(jsonConfig.PathBase, parameters, config);
                case TypeSend.Media:
                    return SendTextImage(jsonConfig.PathBase, parameters, config);
            }

            return false;
        }

        private Boolean SendText(String pathBase, List<Tuple<String, String>> parameters, WhatsAppConfig message)
        {
            if(Dictionary.TryGetValue(TypeSend.Text,out String reource)){
                parameters.Add(new Tuple<string, string>("text", message.Message));
                ResponseServiceRequest responseService =  ServiceRequestHttp.Post(pathBase, reource, parameters, null, null, EnumsServiceRequestHttp.ContentTypeBody.FormUrlencoded);
                if (responseService.HttpStatusCode.HasValue && responseService.HttpStatusCode == HttpStatusCode.OK)
                {
                    return responseService.StreamReaderResult.Contains("success") && responseService.StreamReaderResult.Contains("true");
                }
            }
            return false;
        }


        private Boolean SendImage(String pathBase, List<Tuple<String, String>> parameters, WhatsAppConfig message)
        {
            if (Dictionary.TryGetValue(TypeSend.Image, out String reource))
            {
                parameters.Add(new Tuple<string, string>("url", message.UrlFile));
                ResponseServiceRequest responseService = ServiceRequestHttp.Post(pathBase, reource, parameters, null, null, EnumsServiceRequestHttp.ContentTypeBody.FormUrlencoded);
                if (responseService.HttpStatusCode.HasValue && responseService.HttpStatusCode == HttpStatusCode.OK)
                {
                    return responseService.StreamReaderResult.Contains("success") && responseService.StreamReaderResult.Contains("true");
                }
            }
            return false;
        }

        private Boolean SendTextImage(String pathBase, List<Tuple<String, String>> parameters, WhatsAppConfig message)
        {
            Boolean result = false;
            if (!String.IsNullOrEmpty(message.Message))
            {
                result = SendText(pathBase, parameters, message);
            }

            if (!String.IsNullOrEmpty(message.UrlFile))
            {
                result = SendImage(pathBase, parameters, message);
            }
            return result;
        }

        private Boolean SendMedia(String pathBase, List<Tuple<String, String>> parameters, WhatsAppConfig message)
        {
            if (Dictionary.TryGetValue(TypeSend.Media, out String reource))
            {
                parameters.Add(new Tuple<string, string>("url", message.UrlFile));
                ResponseServiceRequest responseService = ServiceRequestHttp.Post(pathBase, reource, parameters, null, null, EnumsServiceRequestHttp.ContentTypeBody.FormUrlencoded);
                if (responseService.HttpStatusCode.HasValue && responseService.HttpStatusCode == HttpStatusCode.OK)
                {
                    return responseService.StreamReaderResult.Contains("success") && responseService.StreamReaderResult.Contains("true");
                }
            }
            return false;
        }
    }

    public class ConfigWahtsAppFromWabBox
    {
        public String Phone{ get; set; }
        public String Key { get; set; }
        public String PathBase { get; set; }
    }

}




