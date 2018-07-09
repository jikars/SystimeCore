using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NotificationAdmin
{
    //TODO : resolver como se va a realizar la cconfiguracion del proyecto 
    public static class Constants
    {
        internal const String DB_NAME = "Systimedb";
#pragma warning disable S2068 // Credentials should not be hard-coded
        //internal const String CONECTION_STRING = @"metadata=res://*/DataAccess.SystimeDb.csdl|res://*/DataAccess.SystimeDb.ssdl|res://*/DataAccess.SystimeDb.msl;provider=System.Data.SqlClient;provider connection string='data source=190.147.5.19,49199;initial catalog=Systimedb;user id=sa;password=J1u5a0n6fxe;MultipleActiveResultSets=True;'";
        internal const String CONECTION_STRING = @"metadata=res://*/DataAcces.NotificationDb.csdl|res://*/DataAcces.NotificationDb.ssdl|res://*/DataAcces.NotificationDb.msl;provider=System.Data.SqlClient;provider connection string='data source=192.168.99.3,49199;initial catalog=Systimedb;persist security info=True;user id=sa;password=J1u5a0n6fxe;MultipleActiveResultSets=True;App=EntityFramework;'";
#pragma warning restore S2068 // Credentials should not be hard-coded

        internal readonly static List<String> ConditionList = new List<string>()
        {
            ">",
            "<",
            ">=",
            "<=",
            "=",
            "!=",
            "IS NULL",
            "IS NOT NULL"
        };


        public  enum TypeParameterMessage
        {
            Title,
            Message
        }

    }
}
