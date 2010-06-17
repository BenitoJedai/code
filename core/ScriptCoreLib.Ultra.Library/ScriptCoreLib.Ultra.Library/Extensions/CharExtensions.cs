using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ScriptCoreLib.Extensions
{
    public static class CharExtensions
    {
        public static bool IsDigit(this string e)
        {
            var r = e.Length > 0;
            foreach (char item in e)
            {
                r = char.IsDigit(item);
                if (!r)
                    break;
            }

            return r;
        }
    }
}
