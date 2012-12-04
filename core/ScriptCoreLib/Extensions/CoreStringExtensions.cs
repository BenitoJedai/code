using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ScriptCoreLib.Extensions
{
    [Script]
    public static class CoreStringExtensions
    {
        public static IEnumerable<int> GetIndecies(this string e, string f)
        {
            var a = new List<int>();

            var p = 0;
            var i = e.IndexOf(f, p);
            while (i >= 0)
            {
                p = i + 1;

                a.Add(i);

                i = e.IndexOf(f, p);
            }

            return a;
        }
    }
}
