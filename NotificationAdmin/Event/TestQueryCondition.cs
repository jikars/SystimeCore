using NotificationFromSytimeSQL.Contract;
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
    public partial class TestQueryCondition : Form, INotificiationError
    {
        private ISchemaTables SchemaTables { get; set; } = null;

        public TestQueryCondition(String query, ISchemaTables schemaTables)
        {
            InitializeComponent();
            SchemaTables = schemaTables;
            GridResultTest.DataSource = schemaTables.ExeucteQuery(Constants.CONECTION_STRING,query,this);
        }

        public void NotificationError<T>(T exception) where T : Exception
        {
            MessageBox.Show(exception.Message);
            Close();
        }
    }
}
