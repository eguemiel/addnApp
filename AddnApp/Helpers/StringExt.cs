using System.Linq;
using System.Text.RegularExpressions;

namespace System
{
    public static class StringExt
    {
        public static string RemoveSpecialCaracters(this string source)
        {            
            string[] replaceables = new[] { ":", "/", "|", "*", "?", "<", ">","*", "?", ":", "\\", "\"" };
            string rxString = string.Join("|", replaceables.Select(s => Regex.Escape(s)));
            string output = Regex.Replace(source, rxString, string.Empty);

            return output;
        }
    }
}