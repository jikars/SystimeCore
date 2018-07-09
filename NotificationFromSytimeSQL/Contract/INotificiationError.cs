using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NotificationFromSytimeSQL.Contract
{
    public  interface INotificiationError
    {
       void  NotificationError<T>(T exception) where T:Exception;
        
    }
}
