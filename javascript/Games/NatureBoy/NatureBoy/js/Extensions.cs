using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ScriptCoreLib.JavaScript.Runtime;
using ScriptCoreLib;

namespace NatureBoy.js
{
    [Script]
    static class Extensions
    {
        public static string ToCSSImage(this string url)
        {
            return "url(" + url + ")";
        }

        public static Timer AutoRotate(this Dude e, double multiplier)
        {
            return new Timer(
                t => e.Rotation = System.Convert.ToInt32( t.Counter * multiplier) , 0, 100
            );
        }

    }
}
