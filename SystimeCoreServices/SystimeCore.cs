using Newtonsoft.Json;
using NotificationChangesErp.Contract;
using NotificationChangesErp.ResolveConection;
using NotificationDependecy.Models;
using Notifications.Notifications.Config;
using Notifications.Notifications.SMS.Provaider;
using Notifications.Notifications.Whatsapp.Provider;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.ServiceProcess;
using System.Threading.Tasks;
using SystimeCore.Managers;
using static SystimeCore.Config.Enums;

namespace SystimeCoreServices
{
    partial class SystimeCore : ServiceBase
    {
        Boolean StartServiceNoitificationDynamic = false;
        Boolean StartServiceSyncErpToSystime = false;
        Boolean StartServiceSyncSystimeToErp = false;
        private const int TIMEOUTINSTALL = 10000;
        private static NotificationDependecy.Contract.INotificationSuscriber NotificationDependcy = new NotificationDependecy.NotificationSuscriber();
        private static IWorkOrderNotificationErp NoficiationChangeErp = new WorkOrderNotificationChange();
        private static String ConnectionString = Program.GetInstanceConfig().DealerInfo.ConectionStringToSystime;

        internal void Ondebug()
        {
            this.OnStart(null);
        }



        public SystimeCore()
        {
            InitializeComponent();
            StartServiceNoitificationDynamic = ConfigurationManager.AppSettings["EnabledNoficiaionDynamic"] == "1";
           // StartServiceSyncErpToSystime = ConfigurationManager.AppSettings["EnabledSyncErpToSystime"] == "1";
           // StartServiceSyncSystimeToErp = ConfigurationManager.AppSettings["EnabledSyncSystimeToErp"] == "1";
        }

        protected override void OnStart(string[] args)
        {
            //debe hacer lecturas de la configuracion
            if (StartServiceSyncErpToSystime)
                SyncDatabase();
            if (StartServiceNoitificationDynamic)
                StarNotificationDynamic();
            if (StartServiceSyncSystimeToErp)
                StartNotificationErp();
        }

        private void SyncDatabase()
        {
            Task.Factory.StartNew(() =>
            {
                Task.Delay(TIMEOUTINSTALL).Wait();
                RequestSql.MigrateErpToSystimeSql(TableName.WORK_ORDERS.ToString(), "2016", Program.GetInstanceConfig());
            });

            Task.Factory.StartNew(() =>
            {
                Task.Delay(TIMEOUTINSTALL).Wait();
                INotificationErpToSystime systimeNotification = new NotificationErprSystime();
                while (true)
                {
                    try
                    {
                        systimeNotification.ErpSyncToSystime(Program.GetInstanceConfig());
                        Task.Delay((int)TimeSpan.FromMinutes(1).TotalMilliseconds).Wait();
                    }
#pragma warning disable S2486 // Generic exceptions should not be ignored
#pragma warning disable CS0168 // La variable está declarada pero nunca se usa
                    catch (Exception ex)
#pragma warning restore CS0168 // La variable está declarada pero nunca se usa
                    {

                    }
#pragma warning restore S2486 // Generic exceptions should not be ignored

                }
            });
        }


        private static void StartNotificationErp()
        {
            Task.Factory.StartNew(() =>
           {
               Task.Delay(TIMEOUTINSTALL).Wait();
               NoficiationChangeErp.UpdateWorkOrderAutorizateAtAndObservationsStart(ConnectionString, Program.GetInstanceConfig().DealerInfo.ConectionStringErp, Program.GetInstanceConfig().DealerInfo.DllType, Program.GetInstanceConfig().DealerInfo.IdShopsErpArray, Program.GetInstanceConfig().DealerInfo.LanguageDb);
           });
        }


        private static void StarNotificationDynamic()
        {

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
                                            " where IdWorkOrderTracking = @IdWorkOrderTracking  AND WorkOrders.IdWorkOrder = WorkOrderTracking.IdWorkOrder" +
                                            " AND WorkOrders.IdVinNumber = Vehicles.IdVinNumber ANd WorkOrders.IdCustomer =Customers.IdCustomer"
                                        }


                                    }
                                },
                                Messages = new List<MessageSend>(){
                                    new NotificationDependecy.Models.MessageSend()
                                    {
                                        Message = new SmsConfig() { Message ="SMS AUTOSTOK : Apreciado Cliente, le informamos que su vehiculo @plate se encuentra en proceso de alistamiento de pintura" , Title = "En Alistamiento" },
                                        ConfigMessage =  new NotificationDependecy.Models.MessageConfig()
                                               {
                                                               DinamycParam = "@plate",
                                                                NameColum = new List<string>(){
                                                                    "Plate"
                                                                },
                                                                ExpressionRegular = new List<String>(){
                                                                       { @"\d{10}" }
                                             }
                                         },
                                                    TitleNotify = "En Alistamiento",
                                                    JsonConfig = JsonConvert.SerializeObject(new ConfigMsMFromContacto() { Password = "3sF3R4c0", User = "SMSESFERAC", PthBase = "http://www.appcontacto.com.co", ResourceBase = "wsurl" }),
                                                    Type = "SMS",
                                                    Provider = "Contacto"
                                                },
                                    new NotificationDependecy.Models.MessageSend()
                                    {
                                        Message = new WhatsAppConfig() { Message ="SMS AUTOSTOK : Apreciado Cliente, le informamos que su vehiculo @plate se encuentra en proceso de alistamiento de pintura" , TypeSend = TypeSend.Text },
                                        ConfigMessage =  new NotificationDependecy.Models.MessageConfig()
                                               {
                                                               DinamycParam = "@plate",
                                                                NameColum = new List<string>(){
                                                                    "Plate"
                                                                },
                                                                ExpressionRegular = new List<String>(){
                                                                       { @"\d{10}" }
                                             }
                                         },
                                                    TitleNotify = "En Alistamiento",
                                                    JsonConfig = JsonConvert.SerializeObject(new ConfigWahtsAppFromWabBox() { Key = "598da6b0579da2b1624d024cb3e0595c5b9147c19af76", Phone = "573174354217", PathBase = "https://www.waboxapp.com" }),
                                                    Type = "WHATSAPP",
                                                    Provider = "Waboxapp"
                                                }
                                },
                                RecipientName = "Customer"
                            }
                        },

                        //nuevo envio pero para los asesores 
                        {
                            new NotificationDependecy.Models.RecipientType(){
                                FilttersRecipient = new List<NotificationDependecy.Models.RecipientSend>()
                                {
                                    {
                                        new NotificationDependecy.Models.RecipientSend()
                                        {
                                            Filtter = "Select Workers.Phone,Workers.FullName,Vehicles.Plate,Workers.Email,Workers.Mobile from Workers,WorkOrders,Vehicles,WorkOrderTracking" +
                                            " where IdWorkOrderTracking =  @IdWorkOrderTracking AND WorkOrders.IdWorkOrder = WorkOrderTracking.IdWorkOrder " +
                                            "AND WorkOrders.IdVinNumber = Vehicles.IdVinNumber ANd WorkOrders.IdSalesRepresentative =Workers.IdWorker"
                                        }


                                    }
                                },
                                Messages  = new List<MessageSend>(){

                                    new NotificationDependecy.Models.MessageSend(){
                                    Message = new SmsConfig() { Message ="SMS AUTOSTOK : Senor Asesor, se notifico al cliente que el vehiculo @plate se encuentra en proceso de alistamiento." , Title = "En Alistamiento" },
                                    ConfigMessage  =  new NotificationDependecy.Models.MessageConfig()
                                            {
                                                DinamycParam = "@plate",
                                                NameColum = new List<string>(){
                                                    "Plate"
                                                },
                                                ExpressionRegular = new List<String>(){
                                                       { @"\d{10}" }
                                                }
                                            },
                                    TitleNotify = "En Alistamiento",
                                    JsonConfig = JsonConvert.SerializeObject(new ConfigMsMFromContacto() { Password = "3sF3R4c0", User = "SMSESFERAC", PthBase = "http://www.appcontacto.com.co", ResourceBase = "wsurl" }),
                                    Type = "SMS",
                                    Provider = "Contacto"

                                    },
                                    new NotificationDependecy.Models.MessageSend(){
                                    Message = new WhatsAppConfig() { Message ="SMS AUTOSTOK : Senor Asesor, se notifico al cliente que el vehiculo @plate se encuentra en proceso de alistamiento." , TypeSend = TypeSend.Text},
                                    ConfigMessage  =  new NotificationDependecy.Models.MessageConfig()
                                            {
                                                DinamycParam = "@plate",
                                                NameColum = new List<string>(){
                                                    "Plate"
                                                },
                                                ExpressionRegular = new List<String>(){
                                                       { @"\d{12}" }
                                                }
                                            },
                                    TitleNotify = "En Alistamiento",
                                    JsonConfig = JsonConvert.SerializeObject(new ConfigWahtsAppFromWabBox() { Key = "598da6b0579da2b1624d024cb3e0595c5b9147c19af76", Phone = "573174354217", PathBase = "https://www.waboxapp.com" }),
                                    Type = "WHATSAPP",
                                    Provider = "Waboxapp"
                                }
                                },
                                RecipientName = "Workers AS"
                            }

                        }

                    }

                }
                },
                {
                    new NotificationDependecy.Models.Notification(){
                    TableName = "OperationByWorkOrder",
                    DelayTimeOut = 5,
                    ColumNotify = new List<string>()
                    {
                        { "IdOperationByWorkOrder,IdWorkOrder,IdOperationByDealer" }
                    },
                    NotificationName = "Notificacion comienza el trabajo",
                    EventDatabase = new List<NotificationDependecy.Models.EvnetFillterDataBase>()
                    {
                        {
                            new NotificationDependecy.Models.EvnetFillterDataBase()
                            {
                                EventDatabse = NotificationDependecy.Contract.Enums.EventDataBase.Insert,
                                FillterDatabsase = "",
                                EventName = "Primera vez que se asigne una operacion a la ot a la orden de trabajo"
                            }

                        }
                    },
                    NotificationModuleTable = true,
                    //esta zona solo funciona con las llaves
                    FillterValidatePostScript = "SELECT  * from OperationByWorkOrder WHERE   IdOperationByDealer = @IdOperationByDealer  and " +
                    "IdOperationByWorkOrder IN (SELECT  top(1) IdOperationByWorkOrder FROM OperationByWorkOrder where IdWorkOrder =  @IdWorkOrder)",
                    QueryPostEvent = "exec InsertNewTracking @IdWorkOrder,9,'SystimeCore','SystimeCore',1,0,'seguimiento automatico','SystimeCore'",


                    RecipientsType = new List<NotificationDependecy.Models.RecipientType>()
                    {
                        {
                            new NotificationDependecy.Models.RecipientType(){
                                FilttersRecipient = new List<NotificationDependecy.Models.RecipientSend>()
                                {
                                    {
                                        new NotificationDependecy.Models.RecipientSend()
                                        {
                                            Filtter = "select Customers.FullName,Customers.Mobile,Customers.Email,Vehicles.Plate from Customers,OperationByWorkOrder,WorkOrders,Vehicles where OperationByWorkOrder.IdOperationByWorkOrder = @IdOperationByWorkOrder AND OperationByWorkOrder.IdWorkOrder = WorkOrders.IdWorkOrder " +
                                            " and Vehicles.IdVinNumber =  WorkOrders.IdVinNumber and Customers.IdCustomer =WorkOrders.IdCustomer"
                                        }


                                    }
                                },
                                Messages =  new List<MessageSend>(){
                                    new NotificationDependecy.Models.MessageSend()
                                {

                                    Message = new SmsConfig() { Message ="SMS AUTOSTOK : Apreciado Cliente, le informamos que que la reparacion de su vehiculo @plate ha sido autorizada. Iniciaremos el proceso de desarme." , Title = "Comienzo reparacion" },
                                    ConfigMessage = new NotificationDependecy.Models.MessageConfig()
                                            {
                                                DinamycParam = "@plate",
                                                NameColum = new List<string>(){
                                                    "Plate"
                                                },
                                                ExpressionRegular = new List<String>(){
                                                       { @"\d{10}" }
                                                }
                                            },
                                    TitleNotify = "Comienzo reparacion",
                                    JsonConfig = JsonConvert.SerializeObject(new ConfigMsMFromContacto() { Password = "3sF3R4c0", User = "SMSESFERAC", PthBase = "http://www.appcontacto.com.co", ResourceBase = "wsurl" }),
                                    Type = "SMS",
                                    Provider = "Contacto"
                                },
                                    new NotificationDependecy.Models.MessageSend()
                                {

                                    Message = new WhatsAppConfig() { Message ="SMS AUTOSTOK : Apreciado Cliente, le informamos que que la reparacion de su vehiculo @plate ha sido autorizada. Iniciaremos el proceso de desarme." , TypeSend = TypeSend.Text},
                                    ConfigMessage = new NotificationDependecy.Models.MessageConfig()
                                            {
                                                DinamycParam = "@plate",
                                                NameColum = new List<string>(){
                                                    "Plate"
                                                },
                                                ExpressionRegular = new List<String>(){
                                                       { @"\d{10}" }
                                                }
                                            },
                                    TitleNotify = "Comienzo reparacion",
                                    JsonConfig = JsonConvert.SerializeObject(new ConfigWahtsAppFromWabBox() { Key = "598da6b0579da2b1624d024cb3e0595c5b9147c19af76", Phone = "573174354217", PathBase = "https://www.waboxapp.com" }),
                                    Type = "WHATSAPP",
                                    Provider = "Waboxapp"
                                }

                                },
                                RecipientName = "Customers"
                            }

                        }
                    }

                }

                },
                {
                    new NotificationDependecy.Models.Notification(){
                    TableName = "JobsByWorkOrder",
                    ColumNotify = new List<string>()
                    {
                        { "IdJobByWorkOrder" }
                    },
                    NotificationName = "Notificacion para TOT",
                    EventDatabase = new List<NotificationDependecy.Models.EvnetFillterDataBase>()
                    {
                        {
                            new NotificationDependecy.Models.EvnetFillterDataBase()
                            {
                                EventDatabse = NotificationDependecy.Contract.Enums.EventDataBase.Insert,
                                FillterDatabsase = "",
                                EventName = "Cada vez que se asinge una tot "
                            }

                        }
                    },
                    //esta zona solo funciona con las llaves


                    RecipientsType = new List<NotificationDependecy.Models.RecipientType>()
                    {
                        {
                            new NotificationDependecy.Models.RecipientType(){FilttersRecipient = new List<NotificationDependecy.Models.RecipientSend>()
                                {
                                    {
                                        new NotificationDependecy.Models.RecipientSend()
                                        {
                                            Filtter = "select CatalogProviders.Mobile, CatalogProviders.Phone, CatalogProviders.Email,Vehicles.Plate from CatalogProviders,JobsByWorkOrder,Vehicles,WorkOrders " +
                                            "where CatalogProviders.IdProvider = JobsByWorkOrder.IdProvaider and WorkOrders.IdWorkOrder = JobsByWorkOrder.IdWorkOrder " +
                                            "and WorkOrders.IdVinNumber = Vehicles.IdVinNumber and JobsByWorkOrder.IdJobByWorkOrder = @IdJobByWorkOrder ",
                                        }

                                    }
                                },
                                Messages  = new List<MessageSend> {
                                 new NotificationDependecy.Models.MessageSend()
                                {
                                    Message = new SmsConfig() { Message ="SMS AUTOSTOK: Sr. Proveedor, el vehiculo @plate tiene un TOT asignado en el taller de Morato. recojalo en 2 dias." , Title = "Asingacion TOT" },
                                    ConfigMessage =  new NotificationDependecy.Models.MessageConfig()
                                            {
                                                DinamycParam = "@plate",
                                                NameColum = new List<string>(){
                                                    "Plate"
                                                },
                                                ExpressionRegular = new List<String>(){
                                                       { @"\d{10}" }
                                                }

                                            },
                                    Type = "SMS",
                                    Provider = "Contacto",
                                    JsonConfig = JsonConvert.SerializeObject(new ConfigMsMFromContacto() { Password = "3sF3R4c0", User = "SMSESFERAC", PthBase = "http://www.appcontacto.com.co", ResourceBase = "wsurl" }),
                                    TitleNotify = "Asingacion TOT"

                                },
                                new NotificationDependecy.Models.MessageSend()
                                {
                                    Message = new WhatsAppConfig() { Message ="SMS AUTOSTOK: Sr. Proveedor, el vehiculo @plate tiene un TOT asignado en el taller de Morato. recojalo en 2 dias." , TypeSend = TypeSend.Text },
                                    ConfigMessage =  new NotificationDependecy.Models.MessageConfig()
                                            {
                                                DinamycParam = "@plate",
                                                NameColum = new List<string>(){
                                                    "Plate"
                                                },
                                                ExpressionRegular = new List<String>(){
                                                       { @"\d{10}" }
                                                }

                                            },
                                    JsonConfig = JsonConvert.SerializeObject(new ConfigWahtsAppFromWabBox() { Key = "598da6b0579da2b1624d024cb3e0595c5b9147c19af76", Phone = "573174354217", PathBase = "https://www.waboxapp.com" }),
                                    Type = "WHATSAPP",
                                    Provider = "Waboxapp",
                                    TitleNotify = "Asingacion TOT"
                                }
                                },

                                RecipientName = "Provaider TOT"
                            }

                        }
                    }

                }

                }
            };


            String a = JsonConvert.SerializeObject(notifications);
            NotificationDependcy.StartSucriber(ConnectionString);
        }



        protected override void OnStop()
        {
            NotificationDependcy?.StopAllNotifcation();
            NoficiationChangeErp?.StopUdateWorkOrder();
        }
    }
}
