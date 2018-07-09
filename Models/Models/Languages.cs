using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Models
{
    public class Languages
    {
       public  List<Language> ListLanguages { get;  set; }
    }

    public class Language
    {
        public String Codigo {  get;  set; }
        public String Text { get;  set; }
    }
}