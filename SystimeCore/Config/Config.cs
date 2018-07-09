using IntegrateErpToSystime;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utils.Contract.Segurity;
using Utils.Segurity;

namespace SystimeCore.Config
{
    /// <summary>
    /// Clase de configuracion del dealer que se comporta como un singleton 
    /// </summary>
    public  class Config
    {
        private readonly String PATH_CONFIG;
        private const String NAME_FILE = "Config.txt";

        public DealerInfo DealerInfo { get; set; } = null;

        public const String KEY_Dll_SYSTIME = "0SFtkUb6QQE+ecgNqOm3ohrWlYYusP06VuQVQd5lu/q/KafgXGYuUx02o6tLnCZJ";
        public const String IV_DLL_SYSTIME = "IfV9GNXvdyIc8NDcR3cLXLbAX4QJUob6txzoH6xDsJY=";
        public  String IvDealer { get; set; }
        public  String KeyDealer { get; set; }
        public const String ID_REGISTER = "Dll_SYSTIME";
        public const String Id_USER_MODIFY = "DllSystime";
        public const string DATA_BASE_SYSTIME = "Systimedb" ;

        internal Boolean OperationContinue { get; set; } = true;

         private  IEncyptedSimetric EncyptedSymetric { get; set; }


        /// <summary>
        /// constructyro privado de la clase
        /// </summary>
        public Config(Boolean test1,Boolean test2)
        {
            PATH_CONFIG = ConfigInstall.GetPathConfigProject(NAME_FILE);
            EncyptedSymetric = new EncryptedSimetric();
            ChargueConfig();

            if (test1 && !test2 && !DealerInfo.SaveInTest)
                OperationContinue = false;
            else if (test2 && !test1 && !DealerInfo.SaveInTest2)
                OperationContinue = false;
            else if (test1 && test2)
                OperationContinue = false;


            if (test1 && !test2 && OperationContinue)
                ReconfigTestOperation();
            else if (test2 && !test1 && OperationContinue)
                ReconfigTest2Operation();
         

        }

        

        private void ReconfigTestOperation()
        {

            DealerInfo.ConectionStringToSystime = DealerInfo.ConectionStringSystimeTest;
            DealerInfo.UrlWcf = DealerInfo.UrlWcfTest;
            DealerInfo.UrlWebServiceUbicar = DealerInfo.UrlWebServiceUbicarTest;
        }
        private void ReconfigTest2Operation()
        {
            DealerInfo.ConectionStringToSystime = DealerInfo.ConectionStringSystimeTest2;
            DealerInfo.NotifyWcfChangeDataBase = false;
            DealerInfo.SaveInAzure = false;
        }

   
        /// <summary>
        /// MEtoto encargado de cargar la configuracion por dealer 
        /// </summary>
        private  void ChargueConfig()
        {
            DealerInfo = null;
            String json = "";
            if (File.Exists(PATH_CONFIG))
            {
                ChargeConfigDealer();
                string[] lines = File.ReadAllLines(PATH_CONFIG);
                if (lines.Length > 1)
                    json += lines[1];
                else
                    throw new JsonException("error format json");

                if (String.IsNullOrEmpty(json))
                    throw new JsonException("error format json");

                json = EncyptedSymetric.DecryptString(json, KeyDealer, IvDealer);
                DealerInfo = JsonConvert.DeserializeObject<DealerInfo>(json);
                if (DealerInfo == null)
                    throw new JsonException("error format json");
                else
                {
                    DealerInfo = new DealerInfo()
                    {
                        IdCityDealer = EncyptedSymetric.DecryptString(DealerInfo.IdCityDealer, KeyDealer, IvDealer),
                        IdDealer = EncyptedSymetric.DecryptString(DealerInfo.IdDealer, KeyDealer, IvDealer),
                        PassWord = EncyptedSymetric.DecryptString(DealerInfo.PassWord, KeyDealer, IvDealer),
                        TinDealer = EncyptedSymetric.DecryptString(DealerInfo.TinDealer, KeyDealer, IvDealer),
                        PathBaseFiles = EncyptedSymetric.DecryptString(DealerInfo.PathBaseFiles, KeyDealer, IvDealer),
                        Token = EncyptedSymetric.DecryptString(DealerInfo.Token, KeyDealer, IvDealer),
                        IdErp = EncyptedSymetric.DecryptString(DealerInfo.IdErp, KeyDealer, IvDealer),
                        DllType = EncyptedSymetric.DecryptString(DealerInfo.DllType, KeyDealer, IvDealer),
                        ConectionStringToSystime = EncyptedSymetric.DecryptString(DealerInfo?.ConectionStringToSystime, KeyDealer, IvDealer),
                        ConectionStringErp = EncyptedSymetric.DecryptString(DealerInfo?.ConectionStringErp, KeyDealer, IvDealer),
                        ConectionStringSystimeTest = EncyptedSymetric.DecryptString(DealerInfo?.ConectionStringSystimeTest, KeyDealer, IvDealer),
                        ConectionStringNotification = EncyptedSymetric.DecryptString(DealerInfo?.ConectionStringNotification, KeyDealer, IvDealer),
                        ConectionStringSystimeTest2 = EncyptedSymetric.DecryptString(DealerInfo?.ConectionStringSystimeTest2, KeyDealer, IvDealer),
                        LanguageDb = EncyptedSymetric.DecryptString(DealerInfo.LanguageDb, KeyDealer, IvDealer),
                        IdShopsErp = DealerInfo.IdShopsErp,
                        UrlWebServiceUbicar = EncyptedSymetric.DecryptString(DealerInfo.UrlWebServiceUbicar, KeyDealer, IvDealer),
                        SaveInAzure = DealerInfo.SaveInAzure,
                        SaveInTest = DealerInfo.SaveInTest,
                        UrlWcf = EncyptedSymetric.DecryptString(DealerInfo.UrlWcf, KeyDealer, IvDealer),
                        UrlWcfTest = EncyptedSymetric.DecryptString(DealerInfo.UrlWcfTest, KeyDealer, IvDealer),
                        NotifyWcfChangeDataBase = DealerInfo.NotifyWcfChangeDataBase,
                        UrlWebServiceUbicarTest = EncyptedSymetric.DecryptString(DealerInfo.UrlWebServiceUbicarTest, KeyDealer, IvDealer),
                        SaveInTest2 = DealerInfo.SaveInTest2,
                        NotifyConfig = DealerInfo.NotifyConfig
                    };

                    if (!String.IsNullOrEmpty(DealerInfo.IdShopsErp))
                        DealerInfo.IdShopsErpArray = DealerInfo.IdShopsErp.ToUpper().Trim().Split(';');
                }
            }
            else
                throw new FileLoadException("Path not Support");
        }




        /// <summary>
        /// MEtoto encargado de cargar las llaves del dealer
        /// </summary>
        private  void ChargeConfigDealer()
        {
            Keys keys = null;
            String json = "";
            if (File.Exists(PATH_CONFIG))
            {
                string[] lines = File.ReadAllLines(PATH_CONFIG);
                if (lines.Length > 0)
                    json += lines[0];
                else
                    throw new JsonException("error format json not lines txt");

                if (String.IsNullOrEmpty(json))
                    throw new JsonException("error format json empty");

                json = EncyptedSymetric.DecryptString(json, KEY_Dll_SYSTIME, IV_DLL_SYSTIME);
                try
                {
                    keys = JsonConvert.DeserializeObject<Keys>(json);
                }
                catch (JsonException ex)
                {
                    throw new JsonException("error format json", ex);
                }
                if (keys != null)
                {
                    KeyDealer = keys.KeyDealer;
                    IvDealer = keys.IVDealer;
                }
                else
                    throw new JsonException("error format json in keys");
            }
            else
                throw new FileNotFoundException("file configuration not exist in system");

        }




        /// <summary>
        /// Resuelve la instancia de configura para la integtracion 
        /// </summary>
        /// <returns></returns>
        public  ParamsIntegrateErp GetConfigIntegrate()
        {
            return new ParamsIntegrateErp()
            {
                IdShopsErpArray = DealerInfo.IdShopsErpArray,
                ConectionStringErp = DealerInfo.ConectionStringErp,
                ConectionStringToSystime = DealerInfo.ConectionStringToSystime,
                DllType = DealerInfo.DllType,
                LanguageDb = DealerInfo.LanguageDb,
                SaveInAzure = DealerInfo.SaveInAzure,
                Token = DealerInfo.Token,
                UrlService = DealerInfo.UrlWebServiceUbicar,
                UserModify = Id_USER_MODIFY,
                IdDealerUbicarService = int.Parse(DealerInfo.IdDealer)
            };
        }
    }
}
