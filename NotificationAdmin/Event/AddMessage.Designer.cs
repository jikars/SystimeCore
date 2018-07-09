namespace NotificationAdmin.Event
{
    partial class AddMessage
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
            this.CmbMessage = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.TxtTitle = new System.Windows.Forms.TextBox();
            this.PnlParameterTitle = new System.Windows.Forms.Panel();
            this.TxtMessage = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.PnlParametersMessage = new System.Windows.Forms.Panel();
            this.BtnOk = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // CmbMessage
            // 
            this.CmbMessage.FormattingEnabled = true;
            this.CmbMessage.Location = new System.Drawing.Point(12, 25);
            this.CmbMessage.Name = "CmbMessage";
            this.CmbMessage.Size = new System.Drawing.Size(167, 21);
            this.CmbMessage.TabIndex = 0;
            this.CmbMessage.SelectedValueChanged += new System.EventHandler(this.CmbMessage_SelectedValueChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(85, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "Tipo de mensaje";
            // 
            // TxtTitle
            // 
            this.TxtTitle.Location = new System.Drawing.Point(12, 78);
            this.TxtTitle.Name = "TxtTitle";
            this.TxtTitle.Size = new System.Drawing.Size(604, 20);
            this.TxtTitle.TabIndex = 2;
            // 
            // PnlParameterTitle
            // 
            this.PnlParameterTitle.Location = new System.Drawing.Point(12, 131);
            this.PnlParameterTitle.Name = "PnlParameterTitle";
            this.PnlParameterTitle.Size = new System.Drawing.Size(604, 100);
            this.PnlParameterTitle.TabIndex = 3;
            // 
            // TxtMessage
            // 
            this.TxtMessage.Location = new System.Drawing.Point(12, 279);
            this.TxtMessage.Name = "TxtMessage";
            this.TxtMessage.Size = new System.Drawing.Size(604, 20);
            this.TxtMessage.TabIndex = 4;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 62);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(33, 13);
            this.label2.TabIndex = 5;
            this.label2.Text = "Titulo";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(12, 263);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(47, 13);
            this.label3.TabIndex = 6;
            this.label3.Text = "Mensaje";
            // 
            // PnlParametersMessage
            // 
            this.PnlParametersMessage.Location = new System.Drawing.Point(12, 398);
            this.PnlParametersMessage.Name = "PnlParametersMessage";
            this.PnlParametersMessage.Size = new System.Drawing.Size(604, 144);
            this.PnlParametersMessage.TabIndex = 4;
            // 
            // BtnOk
            // 
            this.BtnOk.Location = new System.Drawing.Point(541, 550);
            this.BtnOk.Name = "BtnOk";
            this.BtnOk.Size = new System.Drawing.Size(75, 23);
            this.BtnOk.TabIndex = 7;
            this.BtnOk.Text = "Aceptar";
            this.BtnOk.UseVisualStyleBackColor = true;
            // 
            // AddMessage
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(628, 585);
            this.Controls.Add(this.BtnOk);
            this.Controls.Add(this.PnlParametersMessage);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.TxtMessage);
            this.Controls.Add(this.PnlParameterTitle);
            this.Controls.Add(this.TxtTitle);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.CmbMessage);
            this.Name = "AddMessage";
            this.Text = "AddMessage";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ComboBox CmbMessage;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox TxtTitle;
        private System.Windows.Forms.Panel PnlParameterTitle;
        private System.Windows.Forms.TextBox TxtMessage;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Panel PnlParametersMessage;
        private System.Windows.Forms.Button BtnOk;
    }
}