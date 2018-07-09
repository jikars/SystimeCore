using ErpDataAccessFromSystime.Contract;
using ErpDataAccessFromSystime.Contract.ErpDataAccessFromSystime.Contract;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ErpDataAccessFromSystime.Erps.DmsV1.DataAcces
{
    internal class NotificationSystimeDmsV1
    {
        private ParamsContract ParamsContract { get; set; }
        private String ConectionStringErp { get; set; }


        /// <summary>
        /// Constructor de la clase
        /// </summary>
        internal NotificationSystimeDmsV1(ParamsContract paramsContract)
        {
            ParamsContract = paramsContract;
            ConectionStringErp = paramsContract?.ConectionStringErp;
        }



        internal bool DeleteNotificationSystime(int idNotificationsystime)
        {
            NotificationSystime entityDelete = null;

            using (DmsV1Entities DataBase = new DmsV1Entities(ConectionStringErp))
            {
                DataBase.Database.CommandTimeout = 2000;
                entityDelete = DataBase.NotificationSystime.FirstOrDefault(n => n.IdNotificationSystime == idNotificationsystime);
                if (entityDelete == null)
                    return true;
                else if(entityDelete.IdNotificationSystime>0)
                {
                    DataBase.NotificationSystime.Remove(entityDelete);
                    try
                    {
                        if (DataBase.SaveChanges() > 0)
                            return true;
                    }
                    catch (DbUpdateConcurrencyException ex)
                    {
                        var entry = ex.Entries.Single();
                        return entry.State == EntityState.Deleted;
                    }
                   
                }
               
            }
            return false;
        }

        internal List<NotificationSystimeIntegrate> GetAllNotification()
        {
            List<NotificationSystimeIntegrate> listNotificationIntegrate = new List<NotificationSystimeIntegrate>();
            using (DmsV1Entities DataBase = new DmsV1Entities(ConectionStringErp))
            {
                DataBase.Database.CommandTimeout = 4000;
                DataBase.NotificationSystime.ToList().ForEach(n=> {
                    listNotificationIntegrate.Add(new NotificationSystimeIntegrate()
                    {
                        IdNotification = n.IdNotificationSystime.ToString(),
                        TableName = n.TableName,
                        JsonKeys = n.JsonKeys,
                        Event = n.EventTable,
                        CreatedAt = n.CreateAt
                    });
                });
                return listNotificationIntegrate;
            }
        }


        internal Boolean DeleteNotificationSystimeGrouup(String jsonKyes,String tableName,String eventTable,DateTime dateTimeLimit)
        {
            using (DmsV1Entities DataBase = new DmsV1Entities(ConectionStringErp))
            {
                DataBase.Database.CommandTimeout = 4000;
                if (DataBase.Database.ExecuteSqlCommand("DELETE FROM NotificationSystime WHERE TableName = {0} AND  JsonKeys = {1} AND EventTable={2} AND CreateAt<{3}", tableName, jsonKyes, eventTable, dateTimeLimit) > 0)
                    return true;
                else
                    return false;
            }
        }
    }
}
