using NotificationAdmin.Event;
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

namespace NotificationAdmin
{
    public partial class EvnetoGenerate : Form
    {
        public EvnetoGenerate()
        {
            InitializeComponent();
        }


        private void BtnCreateEvent_Click(object sender, EventArgs e)
        {
            new EvnetEC(DependencyFactory.Resolve<ISchemaTables>()).Show();
        }
    }
}
