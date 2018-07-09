using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration.Install;
using System.Linq;
using System.ServiceProcess;
using System.Threading.Tasks;
using static SystimeCore.Config.Enums;

namespace SystimeCoreServices
{
    [RunInstaller(true)]
    public partial class ProjectInstaller : Installer
    {
        public ProjectInstaller()
        {
            InitializeComponent();
        }

        private void ProcessIntallerSystimeCore_AfterInstall(object sender, InstallEventArgs e)
        {
            //Task.Factory.StartNew(async () => {
            //await Task.Delay(5000);

            //});
        }

        private void SystimeCore_AfterInstall(object sender, InstallEventArgs e)
        {

            using (ServiceController sc = new ServiceController(SystimeCore.ServiceName))
            {
                sc.Start();
            }
        }

        private void SystimeCore_AfterUninstall(object sender, InstallEventArgs e)
        {

        }

        private void ProcessIntallerSystimeCore_BeforeInstall(object sender, InstallEventArgs e)
        {

        }

        private void ProcessIntallerSystimeCore_BeforeUninstall(object sender, InstallEventArgs e)
        {

        }

        private void SystimeCore_BeforeUninstall(object sender, InstallEventArgs e)
        {
            using (ServiceController sc = new ServiceController(SystimeCore.ServiceName))
            {
                sc.Stop();


            }
        }
    }
}
