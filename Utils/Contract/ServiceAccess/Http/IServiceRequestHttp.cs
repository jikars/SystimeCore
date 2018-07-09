using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Utils.Contract.ServiceAccess.Http.EnumsServiceRequestHttp;

namespace Utils.Contract.ServiceAccess.Http
{
    public interface IServiceRequestHttp
    {

        ResponseServiceRequest Post(String urlBase, String resourceBase, List<Tuple<String, String>> parameters, List<Tuple<String, String>> headers, String bodySerilizer, ContentTypeBody contentType);

        Task<ResponseServiceRequest> PostAsync(String urlBase, String resourceBase, List<Tuple<String, String>> parameters, List<Tuple<String, String>> headers, String bodySerilizer, ContentTypeBody contentType);

        ResponseServiceRequest Get(String urlBase, String resourceBase, List<Tuple<String, String>> parameters, List<Tuple<String, String>> headers);

        Task<ResponseServiceRequest> GetAsync(String urlBase, String resourceBase, List<Tuple<String, String>> parameters, List<Tuple<String, String>> headers);

    }

}
