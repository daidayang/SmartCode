using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using SmartCode.Model;

namespace Samples
{
    public class Helper
    {
        private static Regex cleanRegEx;
        static Helper()
        {
            cleanRegEx = new Regex(@"\s+|_|-|\.", RegexOptions.Compiled);
        }

        public static string CleanName(string name)
        {
            return cleanRegEx.Replace(name, "");
        }

        public static string CamelCase(string name)
        {
            string output = CleanName(name);
            return char.ToLower(output[0]) + output.Substring(1);
        }

        public static string PascalCase(string name)
        {
            string output = CleanName(name);
            return char.ToUpper(output[0]) + output.Substring(1);
        }

        public static string GetNamespace(Domain domain)
        {
            return domain.Code.Trim();
        }

        public static string MakePlural(string name)
        {
            Regex plural1 = new Regex("(?<keep>[^aeiou])y$");
            Regex plural2 = new Regex("(?<keep>[aeiou]y)$");
            Regex plural3 = new Regex("(?<keep>[sxzh])$");
            Regex plural4 = new Regex("(?<keep>[^sxzhy])$");

            if (plural1.IsMatch(name))
                return plural1.Replace(name, "${keep}ies");
            else if (plural2.IsMatch(name))
                return plural2.Replace(name, "${keep}s");
            else if (plural3.IsMatch(name))
                return plural3.Replace(name, "${keep}es");
            else if (plural4.IsMatch(name))
                return plural4.Replace(name, "${keep}s");

            return name;
        }


        public string MakeSingle(string name)
        {
            Regex plural1 = new Regex("(?<keep>[^aeiou])ies$");
            Regex plural2 = new Regex("(?<keep>[aeiou]y)s$");
            Regex plural3 = new Regex("(?<keep>[sxzh])es$");
            Regex plural4 = new Regex("(?<keep>[^sxzhyu])s$");

            if (plural1.IsMatch(name))
                return plural1.Replace(name, "${keep}y");
            else if (plural2.IsMatch(name))
                return plural2.Replace(name, "${keep}");
            else if (plural3.IsMatch(name))
                return plural3.Replace(name, "${keep}");
            else if (plural4.IsMatch(name))
                return plural4.Replace(name, "${keep}");

            return name;
        }
    }
}
