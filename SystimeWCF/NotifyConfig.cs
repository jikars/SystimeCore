using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static SystimeWCF.Contract.Enums;

namespace SystimeWCF
{
    public class NotifyConfig
    {
        public String TableName { get; set; }
        public EventDataBase[] EnventArray { get; set; }
        public String[] PropiertyChange { get; set; }
        public String NameNotification { get; set; }
    }
}
