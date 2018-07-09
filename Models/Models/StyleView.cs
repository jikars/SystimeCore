using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Models
{
    public class StyleView
    {
        public ColorStyle PrimaryColor { get; set; }
        public ColorStyle SecondaryColor { get; set; }
        public ColorStyle ConstractColor { get; set; }
        public String Font1 { get; set; }
        public String Font2 { get; set; }
        public String Font3 { get; set; }

        public String InputTextColor { get; set; }

        public static implicit operator StyleView(int v)
        {
            throw new NotImplementedException();
        }
    }

    public class ColorStyle
    {
        public String Color { get; set; }
        public String TextColor1 { get; set; }
        public String TextColor2 { get; set; }
        public String TextColor3 { get; set; }
        public String ColorEvent { get; set; }

        public String ColorShadow2 { get; set; }

        public String ColorShadow1 { get; set; }

        public String ColorBorderShadow { get; set; }


    }
}