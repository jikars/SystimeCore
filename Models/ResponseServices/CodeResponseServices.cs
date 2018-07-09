using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Models.ResponseServices.MessageError
{

    /// <summary>
    /// Calse que contiene los codigos de serpues
    /// del servicio
    /// </summary>
    public class CodeResponseServices
    {
        public enum TypeErrorWebServicess
        {
            OperationOk = 0, ErrorOperation = 1, Exception = 2, DataDuplicate = 3, ErrorFogerynKey = 4, DataIncomplate = 5, DataNoFount = 6, ErrorDatabase = 7
            , ErroInitiatedSession = 8, DataAnotationError = 9, NotDataFromUpdate = 10, NotOperation = 11, NotCreateData = 12
        }
    }
}
