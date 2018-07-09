using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    internal static class DataAnotationConst
    {
        public const String RegularExpresionPhone = @"^\(?([0-9]{7})+([,][0-9]+)?$";
        public const String RegularExpresionMobile = @"^\(?([0-9]{3})?([0-9]{3})?([0-9]{4})$";
        public const String RegularExpresionEmail = "^[a-zA-Z0-9_\\.-]+@([a-zA-Z0-9-]+\\.)+[a-zA-Z]{2,6}$";
        public const String RegularExpresionUrl = @"^(www.|[a-zA-Z].)[a-zA-Z0-9\-\.]+\.(com|edu|gov|mil|net|org|biz|info|name|museum|us|ca|uk|co|com.co)(\:[0-9]+)*(/($|[a-zA-Z0-9\.\,\;\?\'\\\+&amp;%\$#\=~_\-]+))*$";
    }
}
