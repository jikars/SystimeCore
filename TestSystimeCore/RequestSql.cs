using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using static SystimeCore.Config.Enums;
using Newtonsoft.Json;
using ErpDataAccessFromSystime.Erps.DmsV1.DataAcces;
using SystimeDataAcces.DataAccess;

namespace TestSystimeCore
{
    [TestClass]
    public class RequestSqlTest
    {
        [TestMethod]
        public  void RequestSqlFromSpSysime()
        {
          String json  = JsonConvert.SerializeObject(new WorkOrderDmsV1Keys() { IdErpShop = "11", WorkOrderNumber = "9869" });
            RequestSql.FillterTestSql(TableName.WORK_ORDERS.ToString(), "U", json);
        }


        //[TestMethod]
        //public void NotificationSql()
        //{
        //    String json = JsonConvert.SerializeObject(new WorkOrderTracking() { IdWorkOrderTracking =1 });
        //    Boolean result = global::RequestSql.SendNotificationTest("NotificaAutorizacionCliente", "WorkOrderTracking", "I", json);
        //    Assert.AreEqual(true, result);
        //}



        [TestMethod]
        public  void ReqestMigrate()
        {
            global::RequestSql.MigrateErpToSystimeSqlTest(TableName.WORK_ORDERS.ToString(),"2016");
        }



        [TestMethod]
        public void CreateTriggerNotificationWCF()
        {
            global::RequestSql.CreateNotifySystimeWcfSqlTest("WorkOrders","D",1);
        }


        [TestMethod]
        public void NotifyWcfSystime()
        {
            global::RequestSql.NotifyWcfSqlTest("WorkOrderTrackingDetail", "I", "40455",0);
        }


        [TestMethod]
        public void Refresh()
        {
            global::RequestSql.RefreshSqlTest(TableName.WORK_ORDERS.ToString(),new DateTime(2018,1, 1));
        }
      

        [TestMethod]
        public void TestingTcp()
        {
            Assert.AreEqual(true, ContractSql.TestinTcp("192.168.99.1", 49190));
        }



        [TestMethod]
        public void TestUpdateInfo()
        {
            String json = JsonConvert.SerializeObject(new WorkOrderDmsV1Keys() { IdErpShop = "9", WorkOrderNumber = "34567" });
            ContractSql.FillTerSystime(TableName.WORK_ORDERS.ToString(), "I", json, "192.168.99.1", 49190);
        }


    }
}
