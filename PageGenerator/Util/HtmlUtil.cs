using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PageGenerator.Util
{
    public class HtmlUtil
    {
        public static string GetHtmlPath(string htmlName)
        {
            return Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Templates/" + htmlName);
        }

        public static string GetHtmlContent(string htmlName)
        {
            return File.ReadAllText(GetHtmlPath(htmlName));
            //return File.ReadAllText(GetHtmlPath(htmlName)).Replace("@JQueryPath", Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Templates/js/jquery1.9.1.js"));
        }
    }
}
