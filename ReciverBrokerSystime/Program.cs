using Newtonsoft.Json;
using Notifications.Notifications.SMS.Provaider;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Security.Permissions;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using SystimeCore.Managers;
using SystimeDataAcces.DataAccess;
using SystimeWCF.Contract;
using SystimeWCF.NotifyTableWcf;
using TableDependency.EventArgs;

using TableDependency.SqlClient;

namespace ReciverBrokerSystime
{
   static class Program
    {
        //private static NotificateSuscriber Notification { get; set; }

        static void Main()
        {
            SystimeCore.Config.Config config = new SystimeCore.Config.Config(true, false);

            List<NotificationDependecy.Models.Notification> notifications = new List<NotificationDependecy.Models.Notification>()
            {
                {
                    new NotificationDependecy.Models.Notification(){
                    TableName = "WorkOrderTracking",
                    ColumNotify = new List<string>()
                    {
                        { "IdWorkOrderTracking,IdSubOperationByDealer,IdWorkOrder,IdSubOperationByDealer,InitiatedAt,CompletedAt,CreatedById" }
                    },
                    NotificationName = "Notification Alistamiento",
                    EventDatabase = new List<NotificationDependecy.Models.EvnetFillterDataBase>()
                    {
                        {
                            new NotificationDependecy.Models.EvnetFillterDataBase()
                            {
                                EventDatabse = NotificationDependecy.Contract.Enums.EventDataBase.Insert,
                                FillterDatabsase = "IdSubOperationByDealer = 16",
                                EventName = "SubOperation alistamenito"
                            }

                        }
                    },
                    RecipientsType = new List<NotificationDependecy.Models.RecipientType>()
                    {
                        {
                            new NotificationDependecy.Models.RecipientType(){
                                FilttersRecipient = new List<NotificationDependecy.Models.RecipientSend>()
                                {
                                    {
                                        new NotificationDependecy.Models.RecipientSend()
                                        {
                                            Filtter = "Select Customers.Phone,Customers.FullName,Vehicles.Plate,Customers.Email,Customers.Mobile from Customers,WorkOrders,Vehicles,WorkOrderTracking" +
                                            " where IdWorkOrderTracking = @IdWorkOrderTracking AND WorkOrders.IdWorkOrder = WorkOrderTracking.IdWorkOrder" +
                                            " AND WorkOrders.IdVinNumber = Vehicles.IdVinNumber ANd WorkOrders.IdCustomer =Customers.IdCustomer",
                                           
                                            TypeNotification = new List<NotificationDependecy.Models.NotifycatioType>()
                                            {
                                                {
                                                    new NotificationDependecy.Models.NotifycatioType()
                                                    {
                                                        JsonNotificationConfig =  JsonConvert.SerializeObject(new ConfigMsMFromContacto() { Password = "3sF3R4c0", User = "SMSESFERAC", PthBase = "http://www.appcontacto.com.co", ResourceBase = "wsurl" }),
                                                        ExpressionRegularMach = @"\d{10}",
                                                        TypeNotification = "SMS",
                                                        Provaider = "Contacto"
                                                    }

                                                }
                                            }
                                        }
                                    }
                                },
                                Message = new NotificationDependecy.Models.MessageSend()
                                {
                                    Message = "Su vehiculo @plate ha entrado a preparacion de superficie",
                                    ConfigMessage = new List<NotificationDependecy.Models.MessageConfig>()
                                    {
                                        {
                                            new NotificationDependecy.Models.MessageConfig()
                                            {
                                                DinamycParam = "@plate",
                                                NameColum = new List<string>(){
                                                    "Plate"
                                                },
                                                ExpressionRegular = new List<String>(){
                                                       { @"\d{10}" }
                                                }
                                            }
                                        }
                                    },
                                    TitleMessage = "SysTime"

                                },
                                RecipientName = "customer"
                            }

                        }

                    }

                }
            }
            };


          




            NotificationDependecy.Contract.INotificationSuscriber notificationDependcy = new NotificationDependecy.NotificationSuscriber();
            notificationDependcy.StartSucriber(notifications, config.DealerInfo.ConectionStringToSystime);
            //INotificateSucriber notificateSucriber = new NotificateSuscriber();
            //notificateSucriber.StartNofity(config.DealerInfo.NotifyConfig, config.DealerInfo.UrlWcfTest, config.DealerInfo.ConectionStringToSystime);
            SyncDatabase();
            Console.WriteLine("Press a key to exit");
            Console.ReadKey();
            notificationDependcy.StopAllNotifcation();
            //notificationDependcy.StopAllNotifcation();


        }

        private static void SyncDatabase()
        {

//            Task.Factory.StartNew(() => {
//                INotificationErpToSystime systimeNotification = new NotificationErprSystime();
//                while (true)
//                {
//                    try
//                    {
//                        systimeNotification.ErpSyncToSystime();
//                        Task.Delay((int)TimeSpan.FromMinutes(1).TotalMilliseconds).Wait();
//                    }
//#pragma warning disable S2486 // Generic exceptions should not be ignored
//#pragma warning disable CS0168 // La variable está declarada pero nunca se usa
//                    catch (Exception ex)
//#pragma warning restore CS0168 // La variable está declarada pero nunca se usa
//                    {

//                    }
//#pragma warning restore S2486 // Generic exceptions should not be ignored

//                }
//            });
        }
    }
}
