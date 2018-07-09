namespace SystimeCoreServices
{
    partial class ProjectInstaller
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
            this.ProcessIntallerSystimeCore = new System.ServiceProcess.ServiceProcessInstaller();
            this.SystimeCore = new System.ServiceProcess.ServiceInstaller();
            // 
            // ProcessIntallerSystimeCore
            // 
            this.ProcessIntallerSystimeCore.Account = System.ServiceProcess.ServiceAccount.LocalService;
            this.ProcessIntallerSystimeCore.Password = null;
            this.ProcessIntallerSystimeCore.Username = null;
            this.ProcessIntallerSystimeCore.AfterInstall += new System.Configuration.Install.InstallEventHandler(this.SystimeCore_AfterUninstall);
            this.ProcessIntallerSystimeCore.BeforeInstall += new System.Configuration.Install.InstallEventHandler(this.ProcessIntallerSystimeCore_BeforeInstall);
            this.ProcessIntallerSystimeCore.BeforeUninstall += new System.Configuration.Install.InstallEventHandler(this.ProcessIntallerSystimeCore_BeforeUninstall);
            // 
            // SystimeCore
            // 
            this.SystimeCore.DelayedAutoStart = true;
            this.SystimeCore.DisplayName = "SystimeCore";
            this.SystimeCore.ServiceName = "SystimeCore";
            this.SystimeCore.StartType = System.ServiceProcess.ServiceStartMode.Automatic;
            this.SystimeCore.AfterInstall += new System.Configuration.Install.InstallEventHandler(this.SystimeCore_AfterInstall);
            this.SystimeCore.AfterUninstall += new System.Configuration.Install.InstallEventHandler(this.SystimeCore_AfterUninstall);
            this.SystimeCore.BeforeUninstall += new System.Configuration.Install.InstallEventHandler(this.SystimeCore_BeforeUninstall);
            // 
            // ProjectInstaller
            // 
            this.Installers.AddRange(new System.Configuration.Install.Installer[] {
            this.ProcessIntallerSystimeCore,
            this.SystimeCore});

        }

        #endregion
        public System.ServiceProcess.ServiceInstaller SystimeCore;
        public System.ServiceProcess.ServiceProcessInstaller ProcessIntallerSystimeCore;
    }
}