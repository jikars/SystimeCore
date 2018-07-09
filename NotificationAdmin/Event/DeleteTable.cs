using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace NotificationAdmin.Event
{
    public partial class DeleteTable : Form
    {
        private BindingList<String> ListDeleteTable; 
        public DeleteTable()
        {
            InitializeComponent();
            ListDeleteTable = new BindingList<String>();
            ListDelteTable.SelectionMode = SelectionMode.MultiExtended;
            ConditionEC.TablesNamesListBinding.Where(t => t != ConditionEC.TableName).ToList().ForEach(x => ListDeleteTable.Add(x));
            ListDelteTable.DataSource = ListDeleteTable;
        }

        private void BtnDeleteTable_Click(object sender, EventArgs e)
        {
            ConditionEC.TableDelete = new List<string>();
            foreach (var item in ListDelteTable.SelectedItems)
            {
                ConditionEC.TableDelete.Add(item.ToString());
                ConditionEC.TablesNamesListBinding.Remove(item.ToString());
            }
            Close();
        }
    }
}
