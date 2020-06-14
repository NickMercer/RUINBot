using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace RUINBot
{
    public static class StringHelper
    {
        public static List<string> GetEmojis(this string text)
        {
            var checkedIndex = 0;
            var output = new List<string>();

            while(text.IndexOf("\n", checkedIndex) != -1)
            {
                checkedIndex = text.IndexOf("\n", checkedIndex) + 1;

                if(checkedIndex != -1)
                {
                    output.Add(text.Substring(checkedIndex, 2));
                }
            }

            return output;
        }

        public static bool Contains(this string source, string toCheck, StringComparison comp)
        {
            return source?.IndexOf(toCheck, comp) >= 0;
        }
    }
}
