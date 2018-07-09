using ErpDataAccessFromSystime.Contract;
using ErpDataAccessFromSystime.Contract.ErpDataAccessFromSystime.Contract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using SystimeDataAcces.DataAccess;

namespace IntegrateErpToSystime
{
    public interface IIntegrate<T>
    {
        T Save(String jsonKeys, ParamsIntegrateErp configErp, List<Object> parameterAditional);


        Boolean SaveAll(ParamsIntegrateErp configErp,DateTime dateTimeMin);

        Boolean SuportItem(String jsonKeys, ParamsIntegrateErp configErp);
    }

  
    public class ParamsIntegrateErp
    {

        public String DllType { get; set; }

        public String ConectionStringToSystime { get; set; }

        public String ConectionStringErp { get; set; }

        public String LanguageDb { get; set; }


        public String[] IdShopsErpArray { get; set; }

        public String UserModify { get; set; }

        public Boolean SaveInAzure { get; set; }

        public String UrlService { get; set; }

        public String Token { get; set; }

        internal Boolean ReturnModelService { get; set; } = true;

        public int IdDealerUbicarService { get; set; }
        public ParamsContract ConfigParamsToErp {
            get
            {
                return new ParamsContract()
                {
                    ConectionStringErp = ConectionStringErp,
                    ConectionStringSystime = ConectionStringToSystime,
                    Language = LanguageDb,
                    SupportShop = IdShopsErpArray
                };
            }
            set { ConfigParamsToErp = value; }
        }
            





    }
}
