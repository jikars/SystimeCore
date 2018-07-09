using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServicesAccessUbicar.cs.Contract
{
    public interface IServiceClientUbicar
    {
        T CastObjectResponse<T>();


        Response Save<T>(T Obj, bool hasModelResponse, String urlService, String language, String token);


        Task<Response> SaveAsync<T>(T Obj, bool hasModelResponse, String urlService, String language, String token);

        int? CodErrorRequest();

        String MessageError();

    }
}
