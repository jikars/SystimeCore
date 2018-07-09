namespace NotificationAdmin.Event.ItemsList
{
    partial class ItemNotificationType
    {
        /// <summary> 
        /// Variable del diseñador necesaria.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Limpiar los recursos que se estén usando.
        /// </summary>
        /// <param name="disposing">true si los recursos administrados se deben desechar; false en caso contrario.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Código generado por el Diseñador de componentes

        /// <summary> 
        /// Método necesario para admitir el Diseñador. No se puede modificar
        /// el contenido de este método con el editor de código.
        /// </summary>
        private void InitializeComponent()
        {
            this.LblNotificationType = new System.Windows.Forms.Label();
            this.TxtValueNotification = new System.Windows.Forms.TextBox();
            this.ChckNotify = new System.Windows.Forms.CheckBox();
            this.SuspendLayout();
            // 
            // LblNotificationType
            // 
            this.LblNotificationType.AutoSize = true;
            this.LblNotificationType.Location = new System.Drawing.Point(21, 9);
            this.LblNotificationType.Name = "LblNotificationType";
            this.LblNotificationType.Size = new System.Drawing.Size(35, 13);
            this.LblNotificationType.TabIndex = 0;
            this.LblNotificationType.Text = "label1";
            // 
            // TxtValueNotification
            // 
            this.TxtValueNotification.Location = new System.Drawing.Point(136, 6);
            this.TxtValueNotification.Name = "TxtValueNotification";
            this.TxtValueNotification.Size = new System.Drawing.Size(283, 20);
            this.TxtValueNotification.TabIndex = 1;
            // 
            // ChckNotify
            // 
            this.ChckNotify.AutoSize = true;
            this.ChckNotify.CheckAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.ChckNotify.Location = new System.Drawing.Point(462, 9);
            this.ChckNotify.Name = "ChckNotify";
            this.ChckNotify.Size = new System.Drawing.Size(15, 14);
            this.ChckNotify.TabIndex = 2;
            this.ChckNotify.UseVisualStyleBackColor = true;
            // 
            // ItemNotificationType
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.ChckNotify);
            this.Controls.Add(this.TxtValueNotification);
            this.Controls.Add(this.LblNotificationType);
            this.Name = "ItemNotificationType";
            this.Size = new System.Drawing.Size(501, 33);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label LblNotificationType;
        private System.Windows.Forms.TextBox TxtValueNotification;
        private System.Windows.Forms.CheckBox ChckNotify;
    }
}
