using NotificationAdmin.Event.ItemsList;
using NotificationFromSytimeSQL.Contract;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace NotificationAdmin.Event
{
    public partial class ConditionEC : Form, INotifyCondition
    {

        private List<ItemconditionModel> RowsCharge = null;

        public static List<String> TableDelete { get; set; }

        private String QueryStructure { get; set; }
            
        public static BindingList<String> TablesNamesListBinding { get; set; }
        public static String TableName { get; set; }
        public ConditionEC(String tableName,String nameCondition,String EventsSupport)
        {
            InitializeComponent();
            TableName = tableName;
            if (TablesNamesListBinding == null)
            {
                TablesNamesListBinding = new BindingList<string>
                {
                    tableName
                };
            }

            if (RowsCharge == null)
                RowsCharge = new List<ItemconditionModel>();



            QueryStructure = "{SelecCondition} ";
           
            TxtQueryResult.Multiline = true;
            TxtQueryResult.ScrollBars = ScrollBars.Vertical;
            TxtQueryResult.AcceptsTab = true;
            TxtQueryResult.AcceptsReturn = true;
            TxtQueryResult.WordWrap = true;
            TxtQueryResult.Height = 200;


            TxtQueryResult.Text = QueryStructure.Replace("{SelecCondition}","SELECT  * FROM "+TablesNamesListBinding[0] + " WHERE ");

            LblTableNAme.Text = tableName;
            LblNotificationName.Text = nameCondition;
            LblEventNotification.Text = EventsSupport;
        }

     

        private void BtnAddCondition_Click(object sender, EventArgs e)
        {
            String Identifier = DateTime.Now.ToString("yyyyMMddHHmmssffff");
            int postitionControlPanl = PnlConditions.Controls.Count;

            int position = RefreshPostition(postitionControlPanl);
            int sccrollverticalPostion = PnlConditions.VerticalScroll.Value;
            int scrollHorizontalPostion = PnlConditions.HorizontalScroll.Value;

            RowsCharge.Add(new ItemconditionModel()
            {
                Identifier = Identifier,
                Intance = new ItemCondition(DependencyFactory.Resolve<ISchemaTables>(), Identifier, this),
                LastValueQuery = " {" + Identifier + "}"

            });
            QueryStructure += " {" + Identifier + "}";


            RowsCharge[postitionControlPanl].Intance.Left -= scrollHorizontalPostion;
            RowsCharge[postitionControlPanl].Intance.Top = position - sccrollverticalPostion;
            
            PnlConditions.Controls.Add(RowsCharge[postitionControlPanl].Intance);

            if(postitionControlPanl == 0)
                RowsCharge[postitionControlPanl].Intance.ChargeInitialItem();
            else
                RowsCharge[postitionControlPanl].Intance.GetValueInitial();

           

            PnlConditions.HorizontalScroll.Enabled = true;
            PnlConditions.HorizontalScroll.Visible = false;
            PnlConditions.VerticalScroll.Enabled = true;
            PnlConditions.VerticalScroll.Visible = true;
            PnlConditions.Anchor = (AnchorStyles.Left | AnchorStyles.Right);
            PnlConditions.AutoScroll = true;
            TxtQueryResult.AutoSize = false;




        }



        private int RefreshPostition(int countItemsPnl)
        {
            int position = 0;
        
            if (PnlConditions.Controls.Count > 0)
                position = PnlConditions.Controls[0].Height * countItemsPnl;

            return position;
        }



        private void BtnAddTable_Click(object sender, EventArgs e)
        {
            new AddTable().Show();
        }

        private void ConditionEC_FormClosing(object sender, FormClosingEventArgs e)
        {
            TableDelete = null;
            TablesNamesListBinding = null;
            TableName = String.Empty;
        }

        private void BtnDeleteTable_Click(object sender, EventArgs e)
        {
            new DeleteTable().Show();
        }

        public void ChangeValueItem(String Identifier, String value,String conector)
        {
            Boolean hastLasRowtValue = true;
            int postition = 0;
            Boolean hastValueCurrentQuery = false;
            hastValueCurrentQuery = RowsCharge.FirstOrDefault(r => r.Identifier == Identifier) != null;
           
            if(hastValueCurrentQuery)
            {
                RowsCharge.FirstOrDefault(r => r.Identifier == Identifier).Conector = conector;
                postition = RowsCharge.IndexOf(RowsCharge.FirstOrDefault(r => r.Identifier == Identifier));
            }
             
            if (postition>0)
            {
                hastLasRowtValue = RowsCharge[postition-1].ValueQuery != null;
            }


            if (RowsCharge.FirstOrDefault(v=>v.LastValueQuery.Contains(value)) ==  null && hastLasRowtValue && hastValueCurrentQuery)
            {
                if (postition == 0)
                {
                    RowsCharge[postition].ValueQuery = value;
                    QueryStructure = QueryStructure.Replace(RowsCharge[postition].LastValueQuery, value);
                    RowsCharge[postition].LastValueQuery = value;
                }
                else
                {
                    RowsCharge[postition].ValueQuery = " " + RowsCharge[postition - 1].Conector + " " + value;
                    QueryStructure = QueryStructure.Replace(RowsCharge[postition].LastValueQuery, " " + RowsCharge[postition - 1].Conector + " " + value);
                    RowsCharge[postition].LastValueQuery = " "+RowsCharge[postition - 1].Conector +" "+ value;
                }          
            }
            else if(RowsCharge[postition].LastValueQuery != null && TableDelete != null)
            {
                Boolean hasContains = false;
                foreach(String item  in TableDelete)
                {
                    hasContains = RowsCharge[postition].LastValueQuery.Contains(item) || RowsCharge[postition].LastValueQuery.Contains(" {" + RowsCharge[postition].Identifier + "}");
                }
                if (hasContains)
                {
                    QueryStructure = QueryStructure.Replace(RowsCharge[postition].LastValueQuery, " {" + RowsCharge[postition].Identifier + "}");
                }
            }

            if (RowsCharge.Count - 1 > postition && RowsCharge[postition + 1].ValueQuery != null)
            {
                RowsCharge[postition + 1].ValueQuery = " " + RowsCharge[postition].Conector + RowsCharge[postition + 1].ValueQuery.Replace("OR ", "").Replace("AND ", "");
                QueryStructure = QueryStructure.Replace(RowsCharge[postition + 1].LastValueQuery, RowsCharge[postition + 1].ValueQuery);
                RowsCharge[postition + 1].LastValueQuery = RowsCharge[postition + 1].ValueQuery;
            }

            SetTextInterface();
        }

        public void SelectedItem(String Identifier, bool isCheked)
        {
            RowsCharge.FirstOrDefault(r=> r.Identifier == Identifier).IsCheked = isCheked;
        }

        private void BtnDeleteCondition_Click(object sender, EventArgs e)
        {
            int postition = 0;
            List<ItemconditionModel> listDelete = RowsCharge.Where(r => r.IsCheked).ToList();
            if(listDelete!= null)
            {
                foreach (ItemconditionModel item in listDelete)
                {
                    RowsCharge.Remove(item);
                    QueryStructure = QueryStructure.Replace(item.LastValueQuery, "");
                    postition =  PnlConditions.Controls.IndexOf(item.Intance);
                    PnlConditions.Controls.Remove(item.Intance);
                    if (postition == 0 && item.ValueQuery != null)
                        QueryStructure = QueryStructure.Replace(item.LastValueQuery, item.ValueQuery?.Replace(item.Conector,""));
                    ReajustAling();
                    SetTextInterface();
                }
                listDelete.Clear();
            }          
        }


        private void SetTextInterface()
        {
            String newValue = QueryStructure;
            String selectCondtion = "SELECT * FROM ";
            foreach (String itemtable in TablesNamesListBinding)
            {
                selectCondtion = String.Concat(selectCondtion, itemtable,",");
            }
            selectCondtion = selectCondtion.Remove(selectCondtion.Length - 1);
            selectCondtion += " WHERE ";
            newValue =  newValue.Replace("{SelecCondition}", selectCondtion);
            foreach (ItemconditionModel item in RowsCharge)
            {
                newValue = newValue.Replace(" {" + item.Identifier + "}", "");
            }

            TxtQueryResult.Text = newValue;
        }


        private void ReajustAling ()
        {
            int count = 0;
            foreach(Control item  in PnlConditions.Controls)
            {
                item.Top = PnlConditions.Controls[0].Height * count - PnlConditions.VerticalScroll.Value;
                item.Left -= PnlConditions.HorizontalScroll.Value;
                count++;
            }
        }

        private void BtnTest_Click(object sender, EventArgs e)
        {
            String querRefreh = TxtQueryResult.Text.ToUpper();
            String replaceString = "";
            String currentString = "";
            foreach(ItemconditionModel item in RowsCharge)
            {
                replaceString = item.Intance.GeParamTest()?.ToUpper() ?? "";
                currentString = item.Intance.GetValueColum() ?? "";
                if(!String.IsNullOrEmpty(replaceString) && !String.IsNullOrEmpty(currentString))
                     querRefreh = querRefreh.Replace(replaceString, currentString);
            }

            new TestQueryCondition(querRefreh, DependencyFactory.Resolve<ISchemaTables>()).Show();
        }

        private void BtnAceptar_Click(object sender, EventArgs e)
        {
            new AddDestinatarios(DependencyFactory.Resolve<INotificationSql>() , DependencyFactory.Resolve<ISchemaTables>()).Show();
        }
    }
}
