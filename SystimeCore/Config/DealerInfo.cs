using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SystimeWCF;

namespace SystimeCore.Config
{
    public class DealerInfo
    {
        //[JsonProperty(PropertyName = "RehvNPKhvTSsmuBhfY+TOA==")]
        public String PathBaseFiles { get; set; }

        //[JsonProperty(PropertyName = "oyt1iAwPR2/2oelPQfeA6A==")]
        public String IdDealer { get; set; }


        //[JsonProperty(PropertyName = "2Lv3O2LTGoxV8EVh/j6GOQ==")]
        public String TinDealer { get; set; }


        //[JsonProperty(PropertyName = "+XSvP4bEyZkL4jydkcRNFg==")]
        public String IdCityDealer { get; set; }


        //[JsonProperty(PropertyName = "Token")]
        public String Token { get; set; }

        public String PassWord { get; set; }

        public String IdErp { get; set; }

        public String DllType { get; set; }

        public String ConectionStringToSystime { get; set; }

        public String ConectionStringErp { get; set; }

        public String ConectionStringSystimeTest { get; set; }

        public String ConectionStringSystimeTest2 { get; set; }

        public String ConectionStringNotification { get; set; }

        public String LanguageDb { get; set; }

        public String IdShopsErp { get; set; }

        public String UrlWebServiceUbicar { get; set; }

        public String UrlWebServiceUbicarTest { get; set; }

        public Boolean NotifyWcfChangeDataBase { get; set; }

        public String UrlWcf { get; set; }

        public String UrlWcfTest { get; set; }

        public String[] IdShopsErpArray { get; set; }

        public Boolean SaveInAzure { get; set; }

        public Boolean SaveInTest { get; set; }

        public Boolean SaveInTest2 { get; set; }


        public List<NotifyConfig> NotifyConfig { get; set; }



    }




}
