using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace library.Extension
{
    public static class StringExtension
    {
        public static int ToInt(this string str)
        {
            var d = 0;
            int.TryParse(str, out d);
            return d;
        }

        public static bool IsNullOrWhiteSpace(this string str)
        {
            return !string.IsNullOrWhiteSpace(str);
        }


        public static bool IsNullOrEmpty(this string str)
        {
            return !string.IsNullOrEmpty(str);
        }
    }
}