using ErpDataAccessFromSystime.Contract.ErpDataAccessFromSystime.Contract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static ErpDataAccessFromSystime.TypesErpConfig;

namespace ErpDataAccessFromSystime.Contract
{
    public  interface IDataAccessSystimeToErp
    {
        bool UpdateAutorizateAtObservationsSystime(String idShpo,String workOrderNumber,String observations,DateTime? autorizatheAt, String nameErp, ParamsContract paramsContract);
    }
}
