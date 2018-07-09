using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static NotificationAdmin.Constants;

namespace NotificationAdmin.Event.ItemsList
{
    public partial class ItemParamMessage : UserControl
    {
        private TypeParameterMessage TypeParamterMessage { get; set; }

        private INotifyParameterMessage  NotifyParameterMessage { get; set; }
        private String Parameter { get; set; }
        List<String> Colums { get; set; }
        internal ItemParamMessage(String parameter,List<String> colums,TypeParameterMessage typeParameter, INotifyParameterMessage callObject)
        {
            NotifyParameterMessage = callObject;
            Parameter = parameter;
            TypeParamterMessage = typeParameter;
            Colums = colums;
            InitializeComponent();
            TxtParameter.Text = Parameter;
            CmbColum.DataSource = Colums;
            TxtParameter.Enabled = false;
        }

        private void CmbColum_SelectedValueChanged(object sender, EventArgs e)
        {
            NotifyParameterMessage?.NotifyParamterMessageChange(TypeParamterMessage, Parameter, CmbColum.SelectedValue.ToString());
        }
    }
}
