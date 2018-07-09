using NotificationAdmin.Event.ItemsList;
using NotificationFromSytimeSQL.Contract;
using NotificationFromSytimeSQL.DataAcces;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static NotificationAdmin.Constants;

namespace NotificationAdmin.Event
{
    public partial class AddMessage : Form, INotifyParameterMessage

    {

        private ISchemaTables  SchemaTables { get; set; }
        private INotificationSql NotificationSql { get; set; }

        private List<NotificationMessageBase> NofiticationMessageBase { get; set; }

        private NotificationMessageBase  SelectNNotificationBase { get; set; }

        private INotifyMessage NotifyMessage { get; set; }

        private List<String> ColumsTable { get; set; }

        private Dictionary<String, String> ParameterTitle = null;

        private Dictionary<String, String> ParameterMeesage = null;

        internal AddMessage(ISchemaTables schemaTables, INotifyMessage notifyMessage,INotificationSql notificationSql,String table)
        {
            NotifyMessage = notifyMessage;
            SchemaTables = schemaTables;
            NotificationSql = notificationSql;
            ColumsTable = SchemaTables.GetAllColumFromTable(CONECTION_STRING, DB_NAME, table);
            InitializeComponent();
            NofiticationMessageBase = NotificationSql.GetAllNotificationMessage(CONECTION_STRING);
            CmbMessage.DataSource =  (from N in NofiticationMessageBase select N.Title).ToList();
        }


        private void CmbMessage_SelectedValueChanged(object sender, EventArgs e)
        {
            PnlParameterTitle.Controls.Clear();
            ParameterTitle = new Dictionary<string, string>();
            ParameterMeesage = new Dictionary<string, string>();
            SelectNNotificationBase = NofiticationMessageBase.FirstOrDefault(n => n.Title == CmbMessage.SelectedValue.ToString());
            TxtTitle.Text = SelectNNotificationBase.TitleMessage;
            List<String> paramsTitle = SelectNNotificationBase?.TitleParams?.Split(';').ToList();
            paramsTitle?.ForEach(p =>
            {
                PnlParameterTitle.Controls.Add(new ItemParamMessage(p, ColumsTable, TypeParameterMessage.Title, this));
            });
            TxtMessage.Text = SelectNNotificationBase.MessageBase;
           
            List<String> paramsMessage = SelectNNotificationBase?.MessageParams?.Split(';').ToList();
            paramsMessage?.ForEach(p =>
            {
                PnlParametersMessage.Controls.Add(new ItemParamMessage(p, ColumsTable, TypeParameterMessage.Message, this));
            });
        }



        public void NotifyParamterMessageChange(TypeParameterMessage parameterMessage, string parameter, string columParameter)
        {
            if(parameterMessage == TypeParameterMessage.Title)
            {
                ParameterTitle?.Add(columParameter,parameter);
                NotifyMessage?.NotifyChangeTitle(ParameterTitle);
            }
            else if(parameterMessage == TypeParameterMessage.Message)
            {
                ParameterMeesage?.Add(columParameter, parameter);
                NotifyMessage?.NotifyChangeMessage(ParameterMeesage);
            }
        }
    }
}
