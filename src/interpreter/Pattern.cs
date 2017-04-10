using System;
using System.Text.RegularExpressions;

namespace interpreter
{
    internal class Pattern : Attribute
    {
        private readonly Regex regex;

        internal Pattern(string str)
        {
            regex = new Regex("^" + str);
        }

        internal Match Match(string str)
        {
            return regex.Match(str);
        }
    }
}
