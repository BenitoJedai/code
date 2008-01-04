using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ScriptCoreLib;

namespace ImageReflection.js
{
    [Script]
    static class Extensions
    {
        public static int ToInt32(this double e)
        {
            return System.Convert.ToInt32(e);
        }

        public static int Random(this int i)
        {
            return new Random().Next(i);
        }
    }
}
