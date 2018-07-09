using IntegrateErpToSystime;
using IntegrateErpToSystime.IntegrateLogic;
using IntegrateErpToSystime.ModelIntegrate;
using ServicesAccessUbicar.cs.Contract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SystimeCore.Config;
using SystimeDataAcces.DataAccess;
using static SystimeCore.Config.Enums;

namespace SystimeCore.Managers
{
    public class InsuranceCompanyManager : IManager
    {

        private IIntegrate<InsuranceCompanyIntegrate> Integrate { get; set; }

        public InsuranceCompanyManager()
        {
            Integrate = new InsuranceCompany<InsuranceCompanyIntegrate>();
        }

        public object FillterAccionSql(Config.Config config,string jsonKeys, TableAction action)
        {
            if (!config.OperationContinue)
                return null;

            switch (action)
            {
                case TableAction.I:
                case TableAction.U:
                    return Integrate.Save(jsonKeys, config.GetConfigIntegrate(), null);
            }

            return null;
        }



        public bool MigrateAll(Config.Config config,DateTime? datetimeMin, int year)
        {
            throw new NotImplementedException();
        }

        public bool RefresgFromTime(Config.Config config, DateTime dateTime)
        {
            throw new NotImplementedException();
        }

        public bool? SuportItemData(Config.Config config, string jsonKeys, TableAction action)
        {
            throw new NotImplementedException();
        }
    }
}
