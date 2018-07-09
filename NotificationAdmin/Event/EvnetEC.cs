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
using Unity.Attributes;

namespace NotificationAdmin.Event
{
    public partial class EvnetEC : Form
    {

        private ISchemaTables SchemaTables { get; set; } = null;

        public static BindingList<String> TableListBinding { get; set; }
        private BindingSource SourceListBinding { get; set; }
        public EvnetEC(ISchemaTables schemaTables)
        {
            InitializeComponent();
            SchemaTables = schemaTables;
            if(TableListBinding == null)
            {
                TableListBinding = new BindingList<string>();
                SourceListBinding = new BindingSource()
                {
                    DataSource = TableListBinding
                };
                ListBTableName.DataSource = TableListBinding;
            }
            ChargeControlsAsync();
        }




        private  void ChargeControlsAsync()
        {
            SchemaTables.GetAllTableFromDatabase(Constants.CONECTION_STRING, Constants.DB_NAME).ForEach(x => TableListBinding.Add(x));
        }

        private void BtnContinuar_Click(object sender, EventArgs e)
        {
            String eventsSupport = "";
            if(checInsert.Checked)
                eventsSupport += "Insertar;";
            if (checkDelete.Checked)
                eventsSupport += "Eliminar;";
            if (checkUpdate.Checked)
                eventsSupport += "Actualizar";
            new ConditionEC(ListBTableName.SelectedValue.ToString(),txtNameEvent.Text, eventsSupport).Show();
        }
    }
}
