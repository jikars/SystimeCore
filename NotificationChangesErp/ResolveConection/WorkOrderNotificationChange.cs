using ErpDataAccessFromSystime;
using ErpDataAccessFromSystime.Contract;
using NotificationChangesErp.Contract;
using NotificationChangesErp.ResolveConection.Entity;
using System;
using System.Threading.Tasks;
using TableDependency.SqlClient;
using TableDependency.EventArgs;
using System.Data.SqlClient;
using Dapper.Contrib.Extensions;
using Newtonsoft.Json;

namespace NotificationChangesErp.ResolveConection
{
    public class WorkOrderNotificationChange : IWorkOrderNotificationErp
    {
        private SqlTableDependency<WorkOrders> DependyTable { get; set; }
        private String Erp { get; set; }
        private String ConectionStrinSystime { get; set; }

        private String ConectionStrinSystimenotEntity { get; set; }

        private String ConectionStringErp { get; set; }
        private String[] SupportShop { get; set; }

        private String Language { get; set; }

        public WorkOrderNotificationChange()
        {
        }

        public void UpdateWorkOrderAutorizateAtAndObservationsStart(string conectionString,String conectionStringErp, string erp, String [] supportShop,string language)
        {
            Erp = erp;
            ConectionStringErp = conectionStringErp;
            ConectionStrinSystime = conectionString;
            SupportShop = supportShop;
            Language = language;
            conectionString = conectionString.Substring(conectionString.IndexOf(@"connection string='"));
            conectionString = conectionString.Replace("connection string='", "").Replace("'", "");
            ConectionStrinSystimenotEntity = conectionString;
            try
            {
                DependyTable = new SqlTableDependency<WorkOrders>(conectionString);

                DependyTable.OnChanged += NotifyChange;
                DependyTable.Start();
            }
            catch
            {
                DependyTable?.Stop();
                DependyTable = null;
            }
        }

        private void NotifyChange(object sender, RecordChangedEventArgs<WorkOrders> e)
        {
            if (e.ChangeType == TableDependency.Enums.ChangeType.Update) 
            {
                Task.Factory.StartNew(() => {
                    WorkOrders entity = e.Entity;
                    try
                    {
                        IDataAccessSystimeToErp DataAccessSystimeToErp = new DataAccesErpContract();
                        if (!DataAccessSystimeToErp.UpdateAutorizateAtObservationsSystime(entity.IdDealerShop, entity.WorkOrderNumber, entity.Note, entity.AuthorizedAt, Erp, new ErpDataAccessFromSystime.Contract.ErpDataAccessFromSystime.Contract.ParamsContract()
                        {
                            ConectionStringErp = ConectionStringErp,
                            ConectionStringSystime = ConectionStrinSystime,
                            SupportShop = SupportShop,
                            Language = Language
                        }
                        ))
                        {
                            using (SqlConnection con = new SqlConnection(ConectionStrinSystimenotEntity))
                            {
                                con.Insert(new NotificationErpError()
                                {
                                    ErrorGenerate = "not update data",
                                    EntityUpdateJson = JsonConvert.SerializeObject(entity),
                                    CreatedAt = DateTime.Now,
                                    CreatedById = "SynErp",
                                    TableName = "WorkOrders"
                                });
                            }
                        }
                    }
                    catch( Exception ex )
                    {

                        using (SqlConnection con = new SqlConnection(ConectionStrinSystimenotEntity))
                        {
                            con.Insert(new NotificationErpError()
                            {
                                ErrorGenerate = ex.Message,
                                EntityUpdateJson = JsonConvert.SerializeObject(entity),
                                CreatedAt = DateTime.Now,
                                CreatedById = "SynErp",
                                TableName = "WorkOrders"
                            });
                        }

                    }
                });
            }
        }

        public void StopUdateWorkOrder()
        {
            DependyTable?.Stop();
            DependyTable = null;
        }     
    }
}
