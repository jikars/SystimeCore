using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading.Tasks;

namespace SystimeSyncServiceWindows
{
    static class Program
    {
        /// <summary>
        /// Punto de entrada principal para la aplicación.
        /// </summary>
        static void Main()
        {
            ServiceBase[] ServicesToRun;
            ServicesToRun = new ServiceBase[]
            {
                //Se debe inicializar toda la logica para systime y despelgar logica como un servicio de windows 
                new ServiceSms()
            };
            ServiceBase.Run(ServicesToRun);
        }
    }
}
