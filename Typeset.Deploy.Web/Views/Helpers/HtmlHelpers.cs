using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace System.Web.Mvc
{
    public static class HtmlHelpers
    {
        private static string _ApplicationVersion = System.Reflection.Assembly.GetExecutingAssembly().GetName().Version.ToString();

        public static string ApplicationVersion(this HtmlHelper helper)
        {
            return _ApplicationVersion;
        }
    }
}