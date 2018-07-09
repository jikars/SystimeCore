namespace NotificationAdmin.Event
{
    partial class ConditionEC
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
            this.BtnDeleteTable = new System.Windows.Forms.Button();
            this.BtnAddCondition = new System.Windows.Forms.Button();
            this.BtnDeleteCondition = new System.Windows.Forms.Button();
            this.PnlConditions = new System.Windows.Forms.Panel();
            this.PnlTitle = new System.Windows.Forms.Panel();
            this.label10 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.TxtQueryResult = new System.Windows.Forms.TextBox();
            this.LblTableNAme = new System.Windows.Forms.Label();
            this.LblNotificationName = new System.Windows.Forms.Label();
            this.LblEventNotification = new System.Windows.Forms.Label();
            this.BtnAceptar = new System.Windows.Forms.Button();
            this.BtnTest = new System.Windows.Forms.Button();
            this.label11 = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.label13 = new System.Windows.Forms.Label();
            this.PnlTitle.SuspendLayout();
            this.SuspendLayout();
            // 
            // BtnAddTable
            // 
            this.BtnAddTable.Location = new System.Drawing.Point(1141, 4);
            this.BtnAddTable.Name = "BtnAddTable";
            this.BtnAddTable.Size = new System.Drawing.Size(119, 23);
            this.BtnAddTable.TabIndex = 1;
            this.BtnAddTable.Text = "Agregar Tabla";
            this.BtnAddTable.UseVisualStyleBackColor = true;
            this.BtnAddTable.Click += new System.EventHandler(this.BtnAddTable_Click);
            // 
            // BtnDeleteTable
            // 
            this.BtnDeleteTable.Location = new System.Drawing.Point(1141, 31);
            this.BtnDeleteTable.Name = "BtnDeleteTable";
            this.BtnDeleteTable.Size = new System.Drawing.Size(119, 23);
            this.BtnDeleteTable.TabIndex = 2;
            this.BtnDeleteTable.Text = "Eliminar Tabla";
            this.BtnDeleteTable.UseVisualStyleBackColor = true;
            this.BtnDeleteTable.Click += new System.EventHandler(this.BtnDeleteTable_Click);
            // 
            // BtnAddCondition
            // 
            this.BtnAddCondition.Location = new System.Drawing.Point(1144, 497);
            this.BtnAddCondition.Name = "BtnAddCondition";
            this.BtnAddCondition.Size = new System.Drawing.Size(117, 23);
            this.BtnAddCondition.TabIndex = 3;
            this.BtnAddCondition.Text = "Agregar Condicion";
            this.BtnAddCondition.UseVisualStyleBackColor = true;
            this.BtnAddCondition.Click += new System.EventHandler(this.BtnAddCondition_Click);
            // 
            // BtnDeleteCondition
            // 
            this.BtnDeleteCondition.Location = new System.Drawing.Point(12, 497);
            this.BtnDeleteCondition.Name = "BtnDeleteCondition";
            this.BtnDeleteCondition.Size = new System.Drawing.Size(119, 23);
            this.BtnDeleteCondition.TabIndex = 4;
            this.BtnDeleteCondition.Text = "Eliminar Condicion";
            this.BtnDeleteCondition.UseVisualStyleBackColor = true;
            this.BtnDeleteCondition.Click += new System.EventHandler(this.BtnDeleteCondition_Click);
            // 
            // PnlConditions
            // 
            this.PnlConditions.AccessibleRole = System.Windows.Forms.AccessibleRole.ButtonMenu;
            this.PnlConditions.Location = new System.Drawing.Point(12, 100);
            this.PnlConditions.Name = "PnlConditions";
            this.PnlConditions.Size = new System.Drawing.Size(1249, 391);
            this.PnlConditions.TabIndex = 5;
            // 
            // PnlTitle
            // 
            this.PnlTitle.Controls.Add(this.label10);
            this.PnlTitle.Controls.Add(this.label9);
            this.PnlTitle.Controls.Add(this.label8);
            this.PnlTitle.Controls.Add(this.label7);
            this.PnlTitle.Controls.Add(this.label6);
            this.PnlTitle.Controls.Add(this.label5);
            this.PnlTitle.Controls.Add(this.label4);
            this.PnlTitle.Controls.Add(this.label3);
            this.PnlTitle.Controls.Add(this.label2);
            this.PnlTitle.Controls.Add(this.label1);
            this.PnlTitle.Location = new System.Drawing.Point(12, 68);
            this.PnlTitle.Name = "PnlTitle";
            this.PnlTitle.Size = new System.Drawing.Size(1248, 26);
            this.PnlTitle.TabIndex = 16;
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(1142, 6);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(79, 13);
            this.label10.TabIndex = 9;
            this.label10.Text = "Valor para Test";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(1061, 6);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(30, 13);
            this.label9.TabIndex = 8;
            this.label9.Text = "AND";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(929, 6);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(63, 13);
            this.label8.TabIndex = 7;
            this.label8.Text = "Valor Estico";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(806, 6);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(40, 13);
            this.label7.TabIndex = 6;
            this.label7.Text = "Campo";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(654, 6);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(34, 13);
            this.label6.TabIndex = 5;
            this.label6.Text = "Tabla";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(501, 6);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(102, 13);
            this.label5.TabIndex = 4;
            this.label5.Text = "Tiene Valor Estatico";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(410, 6);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(54, 13);
            this.label4.TabIndex = 3;
            this.label4.Text = "Condicion";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(292, 6);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(40, 13);
            this.label3.TabIndex = 2;
            this.label3.Text = "Campo";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(120, 6);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(34, 13);
            this.label2.TabIndex = 1;
            this.label2.Text = "Tabla";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(3, 6);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(63, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Seleccionar";
            // 
            // TxtQueryResult
            // 
            this.TxtQueryResult.Location = new System.Drawing.Point(12, 527);
            this.TxtQueryResult.Name = "TxtQueryResult";
            this.TxtQueryResult.Size = new System.Drawing.Size(1248, 20);
            this.TxtQueryResult.TabIndex = 17;
            // 
            // LblTableNAme
            // 
            this.LblTableNAme.AutoSize = true;
            this.LblTableNAme.Location = new System.Drawing.Point(90, 17);
            this.LblTableNAme.Name = "LblTableNAme";
            this.LblTableNAme.Size = new System.Drawing.Size(41, 13);
            this.LblTableNAme.TabIndex = 0;
            this.LblTableNAme.Text = "label11";
            // 
            // LblNotificationName
            // 
            this.LblNotificationName.AutoSize = true;
            this.LblNotificationName.Location = new System.Drawing.Point(90, 4);
            this.LblNotificationName.Name = "LblNotificationName";
            this.LblNotificationName.Size = new System.Drawing.Size(41, 13);
            this.LblNotificationName.TabIndex = 18;
            this.LblNotificationName.Text = "label11";
            // 
            // LblEventNotification
            // 
            this.LblEventNotification.AutoSize = true;
            this.LblEventNotification.Location = new System.Drawing.Point(90, 30);
            this.LblEventNotification.Name = "LblEventNotification";
            this.LblEventNotification.Size = new System.Drawing.Size(41, 13);
            this.LblEventNotification.TabIndex = 19;
            this.LblEventNotification.Text = "label11";
            // 
            // BtnAceptar
            // 
            this.BtnAceptar.Location = new System.Drawing.Point(1144, 784);
            this.BtnAceptar.Name = "BtnAceptar";
            this.BtnAceptar.Size = new System.Drawing.Size(116, 23);
            this.BtnAceptar.TabIndex = 20;
            this.BtnAceptar.Text = "Aceptar";
            this.BtnAceptar.UseVisualStyleBackColor = true;
            this.BtnAceptar.Click += new System.EventHandler(this.BtnAceptar_Click);
            // 
            // BtnTest
            // 
            this.BtnTest.Location = new System.Drawing.Point(12, 784);
            this.BtnTest.Name = "BtnTest";
            this.BtnTest.Size = new System.Drawing.Size(116, 23);
            this.BtnTest.TabIndex = 21;
            this.BtnTest.Text = "Test";
            this.BtnTest.UseVisualStyleBackColor = true;
            this.BtnTest.Click += new System.EventHandler(this.BtnTest_Click);
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(12, 4);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(63, 13);
            this.label11.TabIndex = 22;
            this.label11.Text = "Notificacion";
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(12, 17);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(60, 13);
            this.label12.TabIndex = 23;
            this.label12.Text = "Tabla base";
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Location = new System.Drawing.Point(15, 31);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(46, 13);
            this.label13.TabIndex = 24;
            this.label13.Text = "Eventos";
            // 
            // ConditionEC
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1272, 819);
            this.Controls.Add(this.label13);
            this.Controls.Add(this.label12);
            this.Controls.Add(this.label11);
            this.Controls.Add(this.BtnTest);
            this.Controls.Add(this.BtnAceptar);
            this.Controls.Add(this.LblEventNotification);
            this.Controls.Add(this.LblNotificationName);
            this.Controls.Add(this.LblTableNAme);
            this.Controls.Add(this.TxtQueryResult);
            this.Controls.Add(this.PnlTitle);
            this.Controls.Add(this.PnlConditions);
            this.Controls.Add(this.BtnDeleteCondition);
            this.Controls.Add(this.BtnAddCondition);
            this.Controls.Add(this.BtnDeleteTable);
            this.Controls.Add(this.BtnAddTable);
            this.Name = "ConditionEC";
            this.Text = "ConditionEC";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.ConditionEC_FormClosing);
            this.PnlTitle.ResumeLayout(false);
            this.PnlTitle.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Button BtnAddTable;
        private System.Windows.Forms.Button BtnDeleteTable;
        private System.Windows.Forms.Button BtnAddCondition;
        private System.Windows.Forms.Button BtnDeleteCondition;
        private System.Windows.Forms.Panel PnlConditions;
        private System.Windows.Forms.Panel PnlTitle;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox TxtQueryResult;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label LblTableNAme;
        private System.Windows.Forms.Label LblNotificationName;
        private System.Windows.Forms.Label LblEventNotification;
        private System.Windows.Forms.Button BtnAceptar;
        public System.Windows.Forms.Button BtnTest;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Label label13;
    }
}