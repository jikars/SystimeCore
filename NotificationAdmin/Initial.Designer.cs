namespace NotificationAdmin
{
    partial class EvnetoGenerate
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
            this.BtnCreateEvent = new System.Windows.Forms.Button();
            this.listView1 = new System.Windows.Forms.ListView();
            this.SuspendLayout();
            // 
            // BtnCreateEvent
            // 
            this.BtnCreateEvent.Location = new System.Drawing.Point(749, 45);
            this.BtnCreateEvent.Name = "BtnCreateEvent";
            this.BtnCreateEvent.Size = new System.Drawing.Size(112, 37);
            this.BtnCreateEvent.TabIndex = 0;
            this.BtnCreateEvent.Text = "Create Notification";
            this.BtnCreateEvent.UseVisualStyleBackColor = true;
            this.BtnCreateEvent.Click += new System.EventHandler(this.BtnCreateEvent_Click);
            // 
            // listView1
            // 
            this.listView1.Location = new System.Drawing.Point(12, 116);
            this.listView1.Name = "listView1";
            this.listView1.Size = new System.Drawing.Size(849, 598);
            this.listView1.TabIndex = 1;
            this.listView1.UseCompatibleStateImageBehavior = false;
            // 
            // EvnetoGenerate
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(886, 744);
            this.Controls.Add(this.listView1);
            this.Controls.Add(this.BtnCreateEvent);
            this.Name = "EvnetoGenerate";
            this.Text = "Form1";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button BtnCreateEvent;
        private System.Windows.Forms.ListView listView1;
    }
}

