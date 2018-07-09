namespace NotificationAdmin.Event
{
    partial class DeleteTable
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
            this.ListDelteTable = new System.Windows.Forms.ListBox();
            this.BtnDeleteTable = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // ListDelteTable
            // 
            this.ListDelteTable.FormattingEnabled = true;
            this.ListDelteTable.Location = new System.Drawing.Point(5, 4);
            this.ListDelteTable.Name = "ListDelteTable";
            this.ListDelteTable.Size = new System.Drawing.Size(261, 303);
            this.ListDelteTable.TabIndex = 4;
            // 
            // BtnDeleteTable
            // 
            this.BtnDeleteTable.Location = new System.Drawing.Point(191, 313);
            this.BtnDeleteTable.Name = "BtnDeleteTable";
            this.BtnDeleteTable.Size = new System.Drawing.Size(75, 23);
            this.BtnDeleteTable.TabIndex = 3;
            this.BtnDeleteTable.Text = "Eliminar";
            this.BtnDeleteTable.UseVisualStyleBackColor = true;
            this.BtnDeleteTable.Click += new System.EventHandler(this.BtnDeleteTable_Click);
            // 
            // DeleteTable
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(269, 342);
            this.Controls.Add(this.ListDelteTable);
            this.Controls.Add(this.BtnDeleteTable);
            this.Name = "DeleteTable";
            this.Text = "DeleteTable";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ListBox ListDelteTable;
        private System.Windows.Forms.Button BtnDeleteTable;
    }
}