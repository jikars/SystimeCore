using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SystimeCore.Config;
using SystimeWCF;
using Utils.Contract.Segurity;
using Utils.Segurity;
using static SystimeWCF.Contract.Enums;

namespace TestSystimeCore
{
    [TestClass]
    public class EncryptedConfig
    {

        [TestMethod]
        public void EncryptedFileConfig()
        {

            IEncyptedSimetric encripted = new EncryptedSimetric();
            String conectionSystime = @"metadata=res://*/DataAccess.SystimeDb.csdl|res://*/DataAccess.SystimeDb.ssdl|res://*/DataAccess.SystimeDb.msl;provider=System.Data.SqlClient;provider connection string='data source=200.119.44.53,49190;initial catalog=Systimedb;user id=sa;password=J1u5a0n6fxe;MultipleActiveResultSets=True;'";
            String conectionDelaerErp = @"metadata=res://*/Erps.DmsV1.DataAcces.DmsV1Db.csdl|res://*/Erps.DmsV1.DataAcces.DmsV1Db.ssdl|res://*/Erps.DmsV1.DataAcces.DmsV1Db.msl;provider=System.Data.SqlClient;provider connection string='data source=192.168.1.100,1433;initial catalog=Autostok;user id=systime;password=systime_as;MultipleActiveResultSets=True;'";
            //String conectionDelaerErp = @"metadata=res://*/Erps.DmsV1.DataAcces.DmsV1Db.csdl|res://*/Erps.DmsV1.DataAcces.DmsV1Db.ssdl|res://*/Erps.DmsV1.DataAcces.DmsV1Db.msl;provider=System.Data.SqlClient;provider connection string='data source=ASBTAPDC02;initial catalog=Autostoktempo;user id=esfera;password=esfera2015;MultipleActiveResultSets=True;'";

            String conectionSystimeTest = @"metadata=res://*/DataAccess.SystimeDb.csdl|res://*/DataAccess.SystimeDb.ssdl|res://*/DataAccess.SystimeDb.msl;provider=System.Data.SqlClient;provider connection string='data source=200.119.44.53,49190;initial catalog=Systimedb;user id=sa;password=J1u5a0n6fxe;MultipleActiveResultSets=True;'";
            String conectionNotification = @"metadata=res://*/DataAcces.NotificationDb.csdl|res://*/DataAcces.NotificationDb.ssdl|res://*/DataAcces.NotificationDb.msl;provider=System.Data.SqlClient;provider connection string='data source=200.119.44.53,49190;initial catalog=Systimedb;persist security info=True;user id=sa;password=J1u5a0n6fxe;MultipleActiveResultSets=True;'";
            String ConectionStringSystimeTest2 = @"metadata=res://*/DataAccess.SystimeDb.csdl|res://*/DataAccess.SystimeDb.ssdl|res://*/DataAccess.SystimeDb.msl;provider=System.Data.SqlClient;provider connection string='data source=200.119.44.53,49190;initial catalog=Systimedb;user id=sa;password=J1u5a0n6fxe;MultipleActiveResultSets=True;'";
            Config config = new Config(false,false);
            DealerInfo mDealerInfo1 = new DealerInfo()
            {
                IdCityDealer = encripted.EncryptString("CO", config.KeyDealer, config.IvDealer),
                IdDealer = encripted.EncryptString("3", config.KeyDealer, config.IvDealer),
                IdShopsErp = "11;9;7",
                PassWord = encripted.EncryptString("password", config.KeyDealer, config.IvDealer),
                TinDealer = encripted.EncryptString("860507710", config.KeyDealer, config.IvDealer),
                PathBaseFiles = encripted.EncryptString("C://SysTime//Files", config.KeyDealer, config.IvDealer),
                Token = encripted.EncryptString("myToken", config.KeyDealer, config.IvDealer),
                IdErp = encripted.EncryptString("DMS", config.KeyDealer, config.IvDealer),
                DllType = encripted.EncryptString("DMSV1", config.KeyDealer, config.IvDealer),
                ConectionStringToSystime = encripted.EncryptString(conectionSystime, config.KeyDealer, config.IvDealer),
                ConectionStringErp = encripted.EncryptString(conectionDelaerErp, config.KeyDealer, config.IvDealer),
                ConectionStringSystimeTest = encripted.EncryptString(conectionSystimeTest, config.KeyDealer, config.IvDealer),
                ConectionStringSystimeTest2 = encripted.EncryptString(ConectionStringSystimeTest2, config.KeyDealer, config.IvDealer),
                LanguageDb = encripted.EncryptString("es-co", config.KeyDealer, config.IvDealer),
                ConectionStringNotification = encripted.EncryptString(conectionNotification, config.KeyDealer, config.IvDealer),
                UrlWebServiceUbicar = encripted.EncryptString("http://ubicarservices.azurewebsites.net/", config.KeyDealer, config.IvDealer),
                SaveInAzure = true,
                SaveInTest = true,
                SaveInTest2 = true,
                UrlWcf = encripted.EncryptString("http://localhost:55289/api/SystimeDLLNotify/", config.KeyDealer, config.IvDealer),
                UrlWcfTest = encripted.EncryptString("http://localhost:55289/api/SystimeDLLNotify/", config.KeyDealer, config.IvDealer),
                NotifyWcfChangeDataBase = true,
                UrlWebServiceUbicarTest = encripted.EncryptString("Azure Web service Test", config.KeyDealer, config.IvDealer),
                NotifyConfig = new List<NotifyConfig>() {
                    { new NotifyConfig(){ NameNotification = "Notifcacion WorkOrders",TableName = "WorkOrders", EnventArray = new EventDataBase[]{ EventDataBase.Delete,EventDataBase.Insert, EventDataBase.Update} ,PropiertyChange =  null}},
                    { new NotifyConfig(){ NameNotification = "Notifcacion WorkOrders",TableName = "WorkOrders", EnventArray = new EventDataBase[]{ EventDataBase.Delete,EventDataBase.Insert, EventDataBase.Update} ,PropiertyChange =  null}}
                }
            };

            String json = JsonConvert.SerializeObject(mDealerInfo1);

            String encriptSTringObject = encripted.EncryptString(json, config.KeyDealer, config.IvDealer);          
        }
    }
}
