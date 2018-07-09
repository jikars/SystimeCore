using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SystimeCore.Config
{
    public static class ConfigInstall
    {
        private const string ROUTE_PATH = "\\EsferaColor\\SystimeCore\\";
        private const string ROUTE_PATH_NOTIFICATION = ROUTE_PATH + "Notification\\";

        public static string ProgramFilesx86()
        {
            //if (8 == IntPtr.Size
            //    || (!String.IsNullOrEmpty(Environment.GetEnvironmentVariable("PROCESSOR_ARCHITEW6432"))))
            //{
            //    return Environment.GetEnvironmentVariable("ProgramFiles(x86)");
            //}

            return Environment.GetEnvironmentVariable("ProgramFiles");
        }


        public static string GetPathNotification(string nameFile)
        {
            return ProgramFilesx86() + ROUTE_PATH_NOTIFICATION + nameFile ?? "";
        }

        public static string GetPathConfigProject(string nameFile)
        {
            return ProgramFilesx86() + ROUTE_PATH + nameFile ?? "";
        }
    }
}
