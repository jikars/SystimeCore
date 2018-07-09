using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NotificationChangesErp.ResolveConection.Entity
{
    public  class NotificationErpError
    {
        
        public String TableName { get; set; }
        public String EntityUpdateJson { get; set; }
        public String ErrorGenerate { get; set; }

        public DateTime? CreatedAt { get; set; }

        public String CreatedById { get; set; }

    }
}
