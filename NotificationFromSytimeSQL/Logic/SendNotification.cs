using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using NotificationFromSytimeSQL.Contract;
using NotificationFromSytimeSQL.DataAcces;
using Notifications;
using Notifications.Contract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NotificationFromSytimeSQL.Logic
{
    internal class SendNotification
    {
        readonly INotify Notify = null;
        readonly NoitificationEvent NotificatioEventDb = null;
        readonly NotificationLogs NotificationLogs = null;
        readonly NotificationQuerys NotificationQuerys = null;

        internal SendNotification(String conectionString)
        {
            NotificatioEventDb = new NoitificationEvent(conectionString);
            NotificationLogs = new NotificationLogs(conectionString);
            Notify = new ResolverNotify();
            NotificationQuerys = new NotificationQuerys(conectionString);
        }

        internal void SendNotificationEvent(String table, String nameEvent, ActionTable action, String jsonTableData)
        {

            Boolean eventValid = false;
            NotificationEvents notificationEvent = NotificatioEventDb.GetNotificationEvent(table, nameEvent);
            List<NotificationConditions> notificationConditions = null;

            eventValid = (action == ActionTable.I && notificationEvent.EventInsert);
            if (!eventValid)
                eventValid = (action == ActionTable.U && notificationEvent.EventUpdate);
            if (!eventValid)
                eventValid = (action == ActionTable.D && notificationEvent.EventDelete);


            if(eventValid && notificationEvent.Nortify)
            {
                notificationConditions = notificationEvent.NotificationConditions.ToList();
                String query = null;
                String codntionParams = null;
                foreach (NotificationConditions itemCondition in notificationConditions)
                {
                    itemCondition.NotificationQueryCondition.ToList().ForEach(t => { query = String.Concat(query, t.QueryCondition); codntionParams = String.Concat(codntionParams, t.ParameterDimical,";"); });
                    codntionParams = codntionParams.Remove(codntionParams.Length - 1);
                    if (itemCondition.Notify)
                    {
                        if (!String.IsNullOrEmpty(jsonTableData))
                            query =  QueryResolve(query, codntionParams, jsonTableData);

                        query = String.Concat("Select * From ", table, " Where ", query);
                        if (NotificationQuerys.ExecuteCondition(query))
                            SendDestination(itemCondition.NotificationDestination?.ToList());
                    }
                }
            }

        }

        /// <summary>
        /// Envia Notigicacion a la lista de destino segund el tipo 
        /// </summary>
        /// <param name="notificationDestination"></param>
        /// <returns></returns>
        private void SendDestination(List<NotificationDestination> notificationDestination)
        { 
            foreach (NotificationDestination notification in notificationDestination)
            {
                SendNotificationDestination(notification);
            }
        }




        private void SendNotificationDestination(NotificationDestination notification)
        {
            
                String jsonConfig = null;
                String provaider = null;
                String title = null;
                String message = null;
                String typeNotification = null;
                NotificationLog notificationLogSave = null;

                    if (notification.NotificationMessageBase != null)
                    {
                        title = ResolveParamsMessageDinamyc(notification.NotificationMessageBase.TitleMessage, notification.NotificationMessageBase.TitleParams, notification.ParamsTitleBase);
                        message = ResolveParamsMessageDinamyc(notification.NotificationMessageBase.MessageBase, notification.NotificationMessageBase.MessageParams, notification.ParamsMessageBase);
                    }

                    typeNotification = notification.CatalogNotificationType.NotificationType;

                    if (!String.IsNullOrEmpty(notification.TitleSpecifict))
                        title = notification.TitleSpecifict;
                    if (!String.IsNullOrEmpty(notification.MessageSpecifict))
                        message = notification.MessageSpecifict;

                    jsonConfig = notification.CatalogNotificationType.NotificationConfig.JsonConfig;
                    provaider = notification.CatalogNotificationType.NotificationConfig.ProvaiderNotification;
                    if (notification.Notify)
                    {
                        notificationLogSave = NotificationLogs.SaveNotificationLogs(new NotificationLog()
                        {
                            IdNotificationCondition = notification.NotificationConditions.IdNotificationCondition,
                            TaleName = notification.TableName,
                            TableKeys = notification.TableKeys,
                            IdTypeNotification = notification.IdNotificationType,
                            EventName = notification.NotificationConditions.NotificationEvents.EventName,
                            TableEvent = notification.NotificationConditions.NotificationEvents.TableName,
                            DateSend = DateTime.Now,
                            TitleMessage = title,
                            Message = message,
                            WasSend = false,
                            BaseSend = notification.BaseSend
                        });

                //TODO : Se ha quitado todo esto para poder compilar jajajajaja
                        //if (Enum.TryParse(typeNotification, out TypeNotification typeNotificationEnum) &&  Notify.Send(notification.BaseSend, title, message, jsonConfig, typeNotificationEnum, provaider,out String a))
                        //{
                        //    notificationLogSave.WasSend = true;
                        //    NotificationLogs.SaveNotificationLogs(notificationLogSave);
                        //}
                    }

               
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="messageBase"></param>
        /// <param name="paramsArray"></param>
        /// <param name="paramsMessage"></param>
        /// <returns></returns>
        private String ResolveParamsMessageDinamyc(String baseString, String paramsBase,String paramsReplace)
        {
            String[] parameters = paramsBase?.Split(';');
            String[] parametersMessage  = paramsReplace?.Split(';');
            int pos = 0;
            if(parameters != null && parametersMessage != null)
            {
                foreach (String param in parameters)
                {
                    if (pos <= parametersMessage?.Length - 1)
                    {
                        baseString = baseString.Replace(param, parametersMessage[pos]);
                    }
                }
            }
            
            return baseString;
        }

        private String QueryResolve(String query, String parametersQuery, String jsonObject)
        {
            JObject jsonObjec = (JObject)JsonConvert.DeserializeObject(jsonObject);
            if (String.IsNullOrEmpty(parametersQuery) || String.IsNullOrEmpty(query))
                return query;
            else if (jsonObjec == null)
                return null;

            String[] parameters = parametersQuery.Split(';');

            foreach (String param in parameters)
            {
                query = query.Replace(String.Concat(param,"Value"),  (String)jsonObjec[param]);
            }
            return query.Replace("\n"," ").Replace("\r"," ").Replace("  "," ");
        }
    }


   
}
