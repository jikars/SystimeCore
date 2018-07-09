using NotificationAdmin.Event.ItemsList;
using NotificationFromSytimeSQL.Contract;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace NotificationAdmin.Event
{
    public partial class AddDestinatarios : Form,INotificiationError, INotifyMessage
    {

        private INotificationSql NotificationSql { get; set; }
        private ISchemaTables SchemaTables { get; set; }

        private List<Tuple<int,String, String>> LisNotification = null;


        private List<Tuple<int, String, String>> ListNotificationEnenaled = null;

        private String JsonKeysTable { get; set; }
        private List<Tuple<String,String>> KeysValue { get; set; }

        private List<String> KeysTable { get; set; }

        public AddDestinatarios(INotificationSql notificationSql, ISchemaTables schemaTables)
        {
            InitializeComponent();
            NotificationSql = notificationSql;
            SchemaTables = schemaTables;
            CmbDestination.DataSource = NotificationSql?.GetAllTableNotification(Constants.CONECTION_STRING) ?? null;
            LisNotification = NotificationSql.GetProvaiderNotification(Constants.CONECTION_STRING);
            PnlNortificationType.VerticalScroll.Enabled = true;
            PnlNortificationType.VerticalScroll.Visible = true;
            BtnAddMessage.Visible = false;
        }

        private void BtnSearch_Click(object sender, EventArgs e)
        {
            PnlNortificationType.Controls.Clear();
            String query =String.Concat("SELECT * FROM ", CmbDestination.SelectedValue.ToString() , " WHERE ");
           
            List<String> listRowsTables =  SchemaTables.GetAllColumFromTable(Constants.CONECTION_STRING,Constants.DB_NAME, CmbDestination.SelectedValue.ToString());
            foreach(String item in listRowsTables)
            {
                query = String.Concat(query, "(",item, " LIKE '%", TxtSearch.Text, "%') ");
                if (listRowsTables.IndexOf(item) != listRowsTables.Count - 1)
                    query = String.Concat(query, " OR ");
            }
            GrdViewResult.DataSource = SchemaTables.ExeucteQuery(Constants.CONECTION_STRING,query,this);
        }

        public void NotificationError<T>(T exception) where T : Exception
        {
            MessageBox.Show(exception.Message);
        }

        private void GrdViewResult_CellMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            PnlNortificationType.Controls.Clear();

            List<String> itemsCells = new List<string>();
            KeysValue = new List<Tuple<string, string>>();
            foreach (DataGridViewCell item in GrdViewResult.Rows[e.RowIndex].Cells)
            {
                String columName = GrdViewResult.Columns[item.ColumnIndex].Name.ToString();
                if (KeysTable != null && KeysTable.Contains(columName))
                    KeysValue.Add(new Tuple<String,String>(columName,item.Value.ToString()));
                itemsCells.Add(item.Value.ToString());
            }

            ListNotificationEnenaled = new List<Tuple<int, string, string>>();
            foreach (Tuple<int,String,String> iten  in LisNotification)
            {
                itemsCells.ForEach(t=> {
                    if (Regex.IsMatch(t,iten.Item3.Replace("\r\n","")))
                        ListNotificationEnenaled.Add(new Tuple<int, string, string>(iten.Item1, iten.Item2, t));
                        
                });

            }

            int postitionControlPanl = 0;
            int position = 0;
            int sccrollverticalPostion = 0;

            ListNotificationEnenaled.ForEach(n =>
            {
                postitionControlPanl = PnlNortificationType.Controls.Count;
                position = RefreshPostition(postitionControlPanl);
                sccrollverticalPostion = PnlNortificationType.VerticalScroll.Value;
                PnlNortificationType.Controls.Add(new ItemNotificationType(n.Item1,n.Item2, n.Item3)
                {                 
                    Top =  position - sccrollverticalPostion,
                });
            });


        }


        private int RefreshPostition(int countItemsPnl)
        {
            int position = 0;

            if (PnlNortificationType.Controls.Count > 0)
                position = PnlNortificationType.Controls[0].Height * countItemsPnl;

            return position;
        }

        private void CmbDestination_SelectedValueChanged(object sender, EventArgs e)
        {
            PnlNortificationType.Controls.Clear();
            KeysTable = SchemaTables.GetColumKeys(Constants.CONECTION_STRING,Constants.DB_NAME,CmbDestination.SelectedValue.ToString());
        }

        private void ChckAddMessageParam_CheckedChanged(object sender, EventArgs e)
        {
            BtnAddMessage.Visible = ((CheckBox)sender).Checked;
            TxtMessagePreview.Enabled = !((CheckBox)sender).Checked;
        }

        private void BtnAddMessage_Click(object sender, EventArgs e)
        {
            new AddMessage(SchemaTables,this,NotificationSql,CmbDestination.SelectedValue.ToString()).Show();
        }

        public void NotifyChangeTitle(Dictionary<string, string> listParamterTitle)
        {
        }

        public void NotifyChangeMessage(Dictionary<string, string> ListParamterMessage)
        {
        }
    }
}
