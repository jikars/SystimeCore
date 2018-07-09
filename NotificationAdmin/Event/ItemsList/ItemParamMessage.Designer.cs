namespace NotificationAdmin.Event.ItemsList
{
    partial class ItemParamMessage
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
            this.CmbColum = new System.Windows.Forms.ComboBox();
            this.TxtParameter = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // CmbColum
            // 
            this.CmbColum.FormattingEnabled = true;
            this.CmbColum.Location = new System.Drawing.Point(322, 4);
            this.CmbColum.Name = "CmbColum";
            this.CmbColum.Size = new System.Drawing.Size(272, 21);
            this.CmbColum.TabIndex = 0;
            this.CmbColum.SelectedValueChanged += new System.EventHandler(this.CmbColum_SelectedValueChanged);
            // 
            // TxtParameter
            // 
            this.TxtParameter.Location = new System.Drawing.Point(19, 4);
            this.TxtParameter.Name = "TxtParameter";
            this.TxtParameter.Size = new System.Drawing.Size(278, 20);
            this.TxtParameter.TabIndex = 1;
            // 
            // ItemParamMessage
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.TxtParameter);
            this.Controls.Add(this.CmbColum);
            this.Name = "ItemParamMessage";
            this.Size = new System.Drawing.Size(609, 28);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ComboBox CmbColum;
        private System.Windows.Forms.TextBox TxtParameter;
    }
}
