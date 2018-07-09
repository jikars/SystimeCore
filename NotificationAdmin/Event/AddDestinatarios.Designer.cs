﻿namespace NotificationAdmin.Event
{
    partial class AddDestinatarios
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.label1 = new System.Windows.Forms.Label();
            this.CmbDestination = new System.Windows.Forms.ComboBox();
            this.TxtSearch = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.BtnSearch = new System.Windows.Forms.Button();
            this.GrdViewResult = new System.Windows.Forms.DataGridView();
            this.label3 = new System.Windows.Forms.Label();
            this.PnlNortificationType = new System.Windows.Forms.Panel();
            this.listView1 = new System.Windows.Forms.ListView();
            this.BtnAdd = new System.Windows.Forms.Button();
            this.BtnOk = new System.Windows.Forms.Button();
            this.TxtMessagePreview = new System.Windows.Forms.TextBox();
            this.BtnAddMessage = new System.Windows.Forms.Button();
            this.ChckAddMessageParam = new System.Windows.Forms.CheckBox();
            ((System.ComponentModel.ISupportInitialize)(this.GrdViewResult)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(13, 13);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(145, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Selecione la tabla del destino";
            // 
            // CmbDestination
            // 
            this.CmbDestination.FormattingEnabled = true;
            this.CmbDestination.Location = new System.Drawing.Point(16, 30);
            this.CmbDestination.Name = "CmbDestination";
            this.CmbDestination.Size = new System.Drawing.Size(670, 21);
            this.CmbDestination.TabIndex = 1;
            this.CmbDestination.SelectedValueChanged += new System.EventHandler(this.CmbDestination_SelectedValueChanged);
            // 
            // TxtSearch
            // 
            this.TxtSearch.Location = new System.Drawing.Point(16, 93);
            this.TxtSearch.Name = "TxtSearch";
            this.TxtSearch.Size = new System.Drawing.Size(552, 20);
            this.TxtSearch.TabIndex = 2;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(13, 77);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(40, 13);
            this.label2.TabIndex = 3;
            this.label2.Text = "Buscar";
            // 
            // BtnSearch
            // 
            this.BtnSearch.Location = new System.Drawing.Point(585, 91);
            this.BtnSearch.Name = "BtnSearch";
            this.BtnSearch.Size = new System.Drawing.Size(101, 23);
            this.BtnSearch.TabIndex = 4;
            this.BtnSearch.Text = "Buscar";
            this.BtnSearch.UseVisualStyleBackColor = true;
            this.BtnSearch.Click += new System.EventHandler(this.BtnSearch_Click);
            // 
            // GrdViewResult
            // 
            this.GrdViewResult.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.GrdViewResult.Location = new System.Drawing.Point(16, 145);
            this.GrdViewResult.Name = "GrdViewResult";
            this.GrdViewResult.Size = new System.Drawing.Size(670, 136);
            this.GrdViewResult.TabIndex = 5;
            this.GrdViewResult.CellMouseClick += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.GrdViewResult_CellMouseClick);
            // 
            // label3
            // 
            this.label3.AccessibleRole = System.Windows.Forms.AccessibleRole.None;
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(13, 129);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(60, 13);
            this.label3.TabIndex = 6;
            this.label3.Text = "Resultados";
            // 
            // PnlNortificationType
            // 
            this.PnlNortificationType.Location = new System.Drawing.Point(16, 288);
            this.PnlNortificationType.Name = "PnlNortificationType";
            this.PnlNortificationType.Size = new System.Drawing.Size(670, 161);
            this.PnlNortificationType.TabIndex = 7;
            // 
            // listView1
            // 
            this.listView1.Location = new System.Drawing.Point(738, 30);
            this.listView1.Name = "listView1";
            this.listView1.Size = new System.Drawing.Size(554, 637);
            this.listView1.TabIndex = 8;
            this.listView1.UseCompatibleStateImageBehavior = false;
            // 
            // BtnAdd
            // 
            this.BtnAdd.Location = new System.Drawing.Point(585, 736);
            this.BtnAdd.Name = "BtnAdd";
            this.BtnAdd.Size = new System.Drawing.Size(101, 23);
            this.BtnAdd.TabIndex = 9;
            this.BtnAdd.Text = "Añadir";
            this.BtnAdd.UseVisualStyleBackColor = true;
            // 
            // BtnOk
            // 
            this.BtnOk.Location = new System.Drawing.Point(1178, 736);
            this.BtnOk.Name = "BtnOk";
            this.BtnOk.Size = new System.Drawing.Size(101, 23);
            this.BtnOk.TabIndex = 10;
            this.BtnOk.Text = "Aceptar";
            this.BtnOk.UseVisualStyleBackColor = true;
            // 
            // TxtMessagePreview
            // 
            this.TxtMessagePreview.Location = new System.Drawing.Point(16, 493);
            this.TxtMessagePreview.Name = "TxtMessagePreview";
            this.TxtMessagePreview.Size = new System.Drawing.Size(670, 20);
            this.TxtMessagePreview.TabIndex = 11;
            // 
            // BtnAddMessage
            // 
            this.BtnAddMessage.Location = new System.Drawing.Point(517, 464);
            this.BtnAddMessage.Name = "BtnAddMessage";
            this.BtnAddMessage.Size = new System.Drawing.Size(169, 23);
            this.BtnAddMessage.TabIndex = 12;
            this.BtnAddMessage.Text = "Añadir Mensaje";
            this.BtnAddMessage.UseVisualStyleBackColor = true;
            this.BtnAddMessage.Click += new System.EventHandler(this.BtnAddMessage_Click);
            // 
            // ChckAddMessageParam
            // 
            this.ChckAddMessageParam.AutoSize = true;
            this.ChckAddMessageParam.Location = new System.Drawing.Point(16, 470);
            this.ChckAddMessageParam.Name = "ChckAddMessageParam";
            this.ChckAddMessageParam.Size = new System.Drawing.Size(138, 17);
            this.ChckAddMessageParam.TabIndex = 13;
            this.ChckAddMessageParam.Text = "Mensaje Parametrizable";
            this.ChckAddMessageParam.UseVisualStyleBackColor = true;
            this.ChckAddMessageParam.CheckedChanged += new System.EventHandler(this.ChckAddMessageParam_CheckedChanged);
            // 
            // AsginaDestinatarios
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1325, 771);
            this.Controls.Add(this.ChckAddMessageParam);
            this.Controls.Add(this.BtnAddMessage);
            this.Controls.Add(this.TxtMessagePreview);
            this.Controls.Add(this.BtnOk);
            this.Controls.Add(this.BtnAdd);
            this.Controls.Add(this.listView1);
            this.Controls.Add(this.PnlNortificationType);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.GrdViewResult);
            this.Controls.Add(this.BtnSearch);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.TxtSearch);
            this.Controls.Add(this.CmbDestination);
            this.Controls.Add(this.label1);
            this.Name = "AsginaDestinatarios";
            this.Text = "ChargeDestination";
            ((System.ComponentModel.ISupportInitialize)(this.GrdViewResult)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox CmbDestination;
        private System.Windows.Forms.TextBox TxtSearch;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button BtnSearch;
        private System.Windows.Forms.DataGridView GrdViewResult;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Panel PnlNortificationType;
        private System.Windows.Forms.ListView listView1;
        private System.Windows.Forms.Button BtnAdd;
        private System.Windows.Forms.Button BtnOk;
        private System.Windows.Forms.TextBox TxtMessagePreview;
        private System.Windows.Forms.Button BtnAddMessage;
        private System.Windows.Forms.CheckBox ChckAddMessageParam;
    }
}