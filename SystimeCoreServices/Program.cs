using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
using SystimeCore.Config;

namespace SystimeCoreServices
{
    static class Program
    {
        private static Config Config;

        /// <summary>
        /// Punto de entrada principal para la aplicación.
        /// </summary>
        static void Main()
        {

#if (!DEBUG)
            ServiceBase[] ServicesToRun;
            ServicesToRun = new ServiceBase[]
        {
            new SystimeCore()
        };
            ServiceBase.Run(ServicesToRun);

#else
            SystimeCore myServ = new SystimeCore();
            myServ.Ondebug();
            System.Threading.Thread.Sleep(System.Threading.Timeout.Infinite);
#endif
        }

        public static Config GetInstanceConfig()
        {
            if (Config == null)
                Config = new Config(false, false);

            return Config;
        }
    }
}
