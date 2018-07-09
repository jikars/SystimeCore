namespace NotificationAdmin.Event
{
    partial class EvnetEC
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
            this.txtNameEvent = new System.Windows.Forms.TextBox();
            this.ListBTableName = new System.Windows.Forms.ListBox();
            this.label2 = new System.Windows.Forms.Label();
            this.chkNotify = new System.Windows.Forms.CheckBox();
            this.label3 = new System.Windows.Forms.Label();
            this.checInsert = new System.Windows.Forms.CheckBox();
            this.checkUpdate = new System.Windows.Forms.CheckBox();
            this.checkDelete = new System.Windows.Forms.CheckBox();
            this.BtnContinuar = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(40, 32);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(91, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Notification Name";
            // 
            // txtNameEvent
            // 
            this.txtNameEvent.Location = new System.Drawing.Point(43, 49);
            this.txtNameEvent.Name = "txtNameEvent";
            this.txtNameEvent.Size = new System.Drawing.Size(343, 20);
            this.txtNameEvent.TabIndex = 1;
            // 
            // ListBTableName
            // 
            this.ListBTableName.FormattingEnabled = true;
            this.ListBTableName.Location = new System.Drawing.Point(43, 111);
            this.ListBTableName.Name = "ListBTableName";
            this.ListBTableName.Size = new System.Drawing.Size(343, 173);
            this.ListBTableName.TabIndex = 2;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(40, 95);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(91, 13);
            this.label2.TabIndex = 3;
            this.label2.Text = "Notification Name";
            // 
            // chkNotify
            // 
            this.chkNotify.AutoSize = true;
            this.chkNotify.Location = new System.Drawing.Point(42, 315);
            this.chkNotify.Name = "chkNotify";
            this.chkNotify.Size = new System.Drawing.Size(115, 17);
            this.chkNotify.TabIndex = 4;
            this.chkNotify.Text = "Notificacion Activa";
            this.chkNotify.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.chkNotify.UseVisualStyleBackColor = true;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(40, 359);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(103, 13);
            this.label3.TabIndex = 5;
            this.label3.Text = "Eventos Soportados";
            // 
            // checInsert
            // 
            this.checInsert.AutoSize = true;
            this.checInsert.Location = new System.Drawing.Point(42, 386);
            this.checInsert.Name = "checInsert";
            this.checInsert.Size = new System.Drawing.Size(61, 17);
            this.checInsert.TabIndex = 6;
            this.checInsert.Text = "Insertar";
            this.checInsert.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.checInsert.UseVisualStyleBackColor = true;
            // 
            // checkUpdate
            // 
            this.checkUpdate.AutoSize = true;
            this.checkUpdate.Location = new System.Drawing.Point(154, 386);
            this.checkUpdate.Name = "checkUpdate";
            this.checkUpdate.Size = new System.Drawing.Size(72, 17);
            this.checkUpdate.TabIndex = 7;
            this.checkUpdate.Text = "Actualizar";
            this.checkUpdate.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.checkUpdate.UseVisualStyleBackColor = true;
            // 
            // checkDelete
            // 
            this.checkDelete.AutoSize = true;
            this.checkDelete.Location = new System.Drawing.Point(280, 386);
            this.checkDelete.Name = "checkDelete";
            this.checkDelete.Size = new System.Drawing.Size(62, 17);
            this.checkDelete.TabIndex = 8;
            this.checkDelete.Text = "Eliminar";
            this.checkDelete.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.checkDelete.UseVisualStyleBackColor = true;
            // 
            // BtnContinuar
            // 
            this.BtnContinuar.Location = new System.Drawing.Point(341, 499);
            this.BtnContinuar.Name = "BtnContinuar";
            this.BtnContinuar.Size = new System.Drawing.Size(75, 23);
            this.BtnContinuar.TabIndex = 9;
            this.BtnContinuar.Text = "Continuar";
            this.BtnContinuar.UseVisualStyleBackColor = true;
            this.BtnContinuar.Click += new System.EventHandler(this.BtnContinuar_Click);
            // 
            // EvnetEC
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(445, 547);
            this.Controls.Add(this.BtnContinuar);
            this.Controls.Add(this.checkDelete);
            this.Controls.Add(this.checkUpdate);
            this.Controls.Add(this.checInsert);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.chkNotify);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.ListBTableName);
            this.Controls.Add(this.txtNameEvent);
            this.Controls.Add(this.label1);
            this.Name = "EvnetEC";
            this.Text = "EvnetAdd";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtNameEvent;
        private System.Windows.Forms.ListBox ListBTableName;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.CheckBox chkNotify;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.CheckBox checInsert;
        private System.Windows.Forms.CheckBox checkUpdate;
        private System.Windows.Forms.CheckBox checkDelete;
        private System.Windows.Forms.Button BtnContinuar;
    }
}