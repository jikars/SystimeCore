using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace NotificationAdmin.Event.ItemsList
{
    public partial class ItemNotificationType : UserControl
    {
        public ItemNotificationType(int idNotification,String name,String value)
        {
            InitializeComponent();
            LblNotificationType.Text = name;
            TxtValueNotification.Text = value;
            ChckNotify.Checked = true;
        }
    }
}
