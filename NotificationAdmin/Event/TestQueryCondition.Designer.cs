namespace NotificationAdmin.Event
{
    partial class TestQueryCondition
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
            this.GridResultTest = new System.Windows.Forms.DataGridView();
            ((System.ComponentModel.ISupportInitialize)(this.GridResultTest)).BeginInit();
            this.SuspendLayout();
            // 
            // GridResultTest
            // 
            this.GridResultTest.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.GridResultTest.Location = new System.Drawing.Point(13, 13);
            this.GridResultTest.Name = "GridResultTest";
            this.GridResultTest.Size = new System.Drawing.Size(1156, 626);
            this.GridResultTest.TabIndex = 0;
            // 
            // TestQueryCondition
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1181, 651);
            this.Controls.Add(this.GridResultTest);
            this.Name = "TestQueryCondition";
            this.Text = "TestQueryCondition";
            ((System.ComponentModel.ISupportInitialize)(this.GridResultTest)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView GridResultTest;
    }
}