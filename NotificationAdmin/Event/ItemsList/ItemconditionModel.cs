using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NotificationAdmin.Event.ItemsList
{
    internal class ItemconditionModel
    {
        internal String Identifier { get; set; }
        internal ItemCondition Intance { get; set; }
        internal Boolean IsCheked { get; set; }

        internal String ValueQuery { get; set; }

        internal String Conector { get; set; }

        internal String LastValueQuery { get; set; }


    }
}
