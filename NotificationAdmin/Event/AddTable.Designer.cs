namespace NotificationAdmin.Event
{
    partial class AddTable
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
            this.BtnAddTable = new System.Windows.Forms.Button();
            this.ListAddTable = new System.Windows.Forms.ListBox();
            this.SuspendLayout();
            // 
            // BtnAddTable
            // 
            this.BtnAddTable.Location = new System.Drawing.Point(181, 314);
            this.BtnAddTable.Name = "BtnAddTable";
            this.BtnAddTable.Size = new System.Drawing.Size(75, 23);
            this.BtnAddTable.TabIndex = 1;
            this.BtnAddTable.Text = "Agregar";
            this.BtnAddTable.UseVisualStyleBackColor = true;
            // 
            // ListAddTable
            // 
            this.ListAddTable.FormattingEnabled = true;
            this.ListAddTable.Location = new System.Drawing.Point(2, 3);
            this.ListAddTable.Name = "ListAddTable";
            this.ListAddTable.Size = new System.Drawing.Size(254, 303);
            this.ListAddTable.TabIndex = 2;
            // 
            // AddTable
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(261, 349);
            this.Controls.Add(this.ListAddTable);
            this.Controls.Add(this.BtnAddTable);
            this.Name = "AddTable";
            this.Text = "AddTable";
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Button BtnAddTable;
        private System.Windows.Forms.ListBox ListAddTable;
    }
}