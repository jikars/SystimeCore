using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Unity.Attributes;
using NotificationFromSytimeSQL.Contract;
using System.Collections.Specialized;

namespace NotificationAdmin.Event.ItemsList
{
    public partial class ItemCondition : UserControl
    {
        private ISchemaTables SchemaTables { get; set; } = null;
        private BindingSource SourceListBinding { get; set; }
        private BindingSource SourceListBinding2 { get; set; }


        private Boolean ChangeValue = false;

        //private BindingList<String> ColumsOneBinding { get; set; }
        //private BindingSource ColomsOneSource { get; set; }


        //private BindingList<String> ColumsTwoBinding { get; set; }
        //private BindingSource ColomsTwoSource { get; set; }


        private INotifyCondition NotifyConditon { get; set; }

        private String Identifier { get; set; }

        private String ValueTableOneSelect { get; set; }


        private String ValueTableTwoSelect { get; set; }

        private String ValueColumOnneSelect { get; set; }

        private String ValueColumTwoSelect { get; set; }

        private Boolean IsChargeForm { get; set; } = false;

        public ItemCondition(ISchemaTables schemaTables, String identifier, INotifyCondition notifyCondition)
        {
            InitializeComponent();
            SchemaTables = schemaTables;
            Identifier = identifier;
            NotifyConditon = notifyCondition;
            TxtParameterDinamico.Enabled = false;
            if (SourceListBinding == null)
            {
                SourceListBinding = new BindingSource()
                {
                    DataSource = ConditionEC.TablesNamesListBinding
                };
                SourceListBinding2 = new BindingSource()
                {
                    DataSource = ConditionEC.TablesNamesListBinding
                };
                CmbXTables.DataSource = SourceListBinding;
                CmBTables2.DataSource = SourceListBinding2;

                //ColumsOneBinding = new BindingList<string>();
                //(SchemaTables.GetAllColumFromTable(Constants.CONECTION_STRING, Constants.DB_NAME, CmbXTables?.SelectedValue?.ToString())).ForEach(x => ColumsOneBinding.Add(x));

                //ColomsOneSource = new BindingSource()
                //{
                //    DataSource = ColumsOneBinding
                //};

                //ColumsTwoBinding = new BindingList<string>();
                //(SchemaTables.GetAllColumFromTable(Constants.CONECTION_STRING, Constants.DB_NAME, CmBTables2?.SelectedValue?.ToString())).ForEach(x => ColumsTwoBinding.Add(x));

                //ColomsTwoSource = new BindingSource()
                //{
                //    DataSource = ColumsTwoBinding
                //};

                CmbCondition.DataSource = Constants.ConditionList;
            }
            //CmbXColumns.DataSource = ColomsOneSource;
            //CmbColums2.DataSource = ColomsTwoSource;

            //SourceListBinding.ListChanged += ListTableOneChange;

        }

        //private void ListTableOneChange(object sender, ListChangedEventArgs e)
        //{
        //    if (CmbXTables.Items.Contains(ValueTableOneSelect))
        //    {
        //        CmbXTables.SelectedIndex = CmbXTables.Items.IndexOf(ValueTableOneSelect) ;
        //        CmbXColumns.SelectedIndex = CmbXColumns.Items.IndexOf(ValueColumOnneSelect);
        //    }
        //    if (CmBTables2.Items.Contains(ValueTableTwoSelect))
        //    {
        //        CmBTables2.SelectedIndex = CmBTables2.Items.IndexOf(ValueTableTwoSelect);
        //        CmbColums2.SelectedIndex = CmbColums2.Items.IndexOf(ValueColumTwoSelect);
        //    }
        //}

        //private void CmbXTables_SelectedValueChanged(object sender, EventArgs e)
        //{
        //    if(IsChargeForm)
        //      NotifyConditon.ChangeValueItem(Identifier, GenerateQuery(), CheckAND.Checked ? "AND" : "OR");
        //}

        internal string GenerateValue()
        {
            return GenerateQuery();
        }

        //private void CmBTables2_SelectedValueChanged(object sender, EventArgs e)
        //{
        //    CmbColums2.DataSource = SchemaTables.GetAllColumFromTable(Constants.CONECTION_STRING, Constants.DB_NAME, CmBTables2?.SelectedValue?.ToString());
        //    if (IsChargeForm)
        //        NotifyConditon.ChangeValueItem(Identifier, GenerateQuery(), CheckAND.Checked ? "AND" : "OR");
        //}

        private void ChckBSelection_CheckedChanged(object sender, EventArgs e)
        {
            if (IsChargeForm)
                NotifyConditon.SelectedItem(Identifier, ((CheckBox)sender).Checked);
        }






        private void CmbXColumns_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (IsChargeForm)
                NotifyConditon.ChangeValueItem(Identifier, GenerateQuery(), CheckAND.Checked ? "AND" : "OR");
        }

        private void CmbCondition_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (IsChargeForm)
                NotifyConditon.ChangeValueItem(Identifier, GenerateQuery(), CheckAND.Checked ? "AND" : "OR");
        }

        private void CheckDinamycalValue_CheckedChanged(object sender, EventArgs e)
        {
            if (IsChargeForm)
                NotifyConditon.ChangeValueItem(Identifier, GenerateQuery(), CheckAND.Checked ? "AND" : "OR");
        }



        private void CmbColums2_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (IsChargeForm)
                NotifyConditon.ChangeValueItem(Identifier, GenerateQuery(), CheckAND.Checked ? "AND" : "OR");
        }

        private void ParameterDinamico_TextChanged(object sender, EventArgs e)
        {
            if (IsChargeForm)
                NotifyConditon.ChangeValueItem(Identifier, GenerateQuery(), CheckAND.Checked ? "AND" : "OR");
        }

        private void CheckAND_CheckedChanged(object sender, EventArgs e)
        {
            if (IsChargeForm)
                NotifyConditon.ChangeValueItem(Identifier, GenerateQuery(), CheckAND.Checked ? "AND" : "OR");
        }


        private String GenerateQuery()
        {
            String Selection = null;

            if (CmbCondition.SelectedValue.ToString().ToUpper().Contains("IS NOT NULL") || CmbCondition.SelectedValue.ToString().ToUpper().Contains("IS NULL"))
            {
                CmBTables2.Enabled = false;
                CmbColums2.Enabled = false;
                TxtParameterDinamico.Enabled = false;
                TxtParameterDinamico.Text = "";
                TxtValueTest.Enabled = false;
                TxtValueTest.Text = "";
                TxtValueTest.Enabled = false;
                ChangeValue = false;
                Selection = String.Concat(CmbXTables.SelectedValue.ToString(), ".", CmbXColumns.SelectedValue.ToString(), " ", CmbCondition.SelectedValue.ToString());
            }
            else if (CheckDinamycalValue.Checked)
            {
                CmBTables2.Enabled = false;
                CmbColums2.Enabled = false;
                TxtParameterDinamico.Enabled = true;
                if (ChangeValue)
                {
                    TxtValueTest.Enabled = false;
                    TxtValueTest.Text = "";
                    Selection = String.Concat(CmbXTables.SelectedValue.ToString(), ".", CmbXColumns.SelectedValue.ToString(), " ", CmbCondition.SelectedValue.ToString(), " ", TxtParameterDinamico.Text.ToString());
                }
                else
                {
                    TxtValueTest.Enabled = true;
                    TxtParameterDinamico.Text = String.Concat(CmbXColumns.SelectedValue.ToString(), "Value");
                    Selection = String.Concat(CmbXTables.SelectedValue.ToString(), ".", CmbXColumns.SelectedValue.ToString(), " ", CmbCondition.SelectedValue.ToString(), " ", CmbXColumns.SelectedValue.ToString(), "value");
                }

            }
            else
            {
                CmBTables2.Enabled = true;
                CmbColums2.Enabled = true;
                TxtValueTest.Enabled = true;
                TxtParameterDinamico.Enabled = false;
                TxtParameterDinamico.Text = "";
                TxtValueTest.Text = "";
                TxtValueTest.Enabled = false;
                ChangeValue = false;
                Selection = String.Concat(CmbXTables.SelectedValue.ToString(), ".", CmbXColumns.SelectedValue.ToString(), " ", CmbCondition.SelectedValue.ToString(), " ", CmBTables2.SelectedValue.ToString(), ".", CmbColums2.SelectedValue.ToString());
            }


            ValueTableOneSelect = CmbXTables.SelectedValue.ToString();
            ValueColumOnneSelect = CmbXColumns.SelectedValue.ToString();
            ValueColumTwoSelect = CmbColums2.SelectedValue.ToString();
            ValueTableTwoSelect = CmBTables2.SelectedValue.ToString();
            return Selection;
        }


        internal void ChargeInitialItem()
        {
            CmbXTables.SelectedIndex = CmbXTables.Items.IndexOf(ConditionEC.TableName);
            CmbXTables.Enabled = false;
            CmbXColumns.Enabled = false;
            ChckBSelection.Enabled = false;
            CheckDinamycalValue.Enabled = false;
            CheckDinamycalValue.Checked = true;
            CmBTables2.Enabled = false;
            CmbColums2.Enabled = false;
            TxtValueTest.Enabled = true;
            CmbCondition.SelectedIndex = CmbCondition.Items.IndexOf("=");
            CmbCondition.Enabled = false;
            TxtParameterDinamico.Enabled = false;
            IsChargeForm = true;
            NotifyConditon.ChangeValueItem(Identifier, GenerateQuery(), CheckAND.Checked ? "AND" : "OR");
        }

        internal String GetValueColum()
        {
            return TxtValueTest.Text.ToString();
        }

        internal String GeParamTest()
        {
            return TxtParameterDinamico.Text.ToString();
        }

        internal void GetValueInitial()
        {
            IsChargeForm = true;
            NotifyConditon.ChangeValueItem(Identifier, GenerateQuery(), CheckAND.Checked ? "AND" : "OR");
        }

        private void CmbXTables_SelectedValueChanged(object sender, EventArgs e)
        {
            CmbXColumns.DataSource = SchemaTables.GetAllColumFromTable(Constants.CONECTION_STRING, Constants.DB_NAME, CmbXTables?.SelectedValue?.ToString());
            if (IsChargeForm)
                NotifyConditon.ChangeValueItem(Identifier, GenerateQuery(), CheckAND.Checked ? "AND" : "OR");
        }

        private void CmBTables2_SelectedValueChanged(object sender, EventArgs e)
        {
            CmbColums2.DataSource = SchemaTables.GetAllColumFromTable(Constants.CONECTION_STRING, Constants.DB_NAME, CmBTables2?.SelectedValue?.ToString());
            if (IsChargeForm)
                NotifyConditon.ChangeValueItem(Identifier, GenerateQuery(), CheckAND.Checked ? "AND" : "OR");
        }

        private void TxtParameterDinamico_KeyDown(object sender, KeyEventArgs e)
        {
            ChangeValue = true;
        }
    }
}
