using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ScriptCoreLib;

namespace CakeCuttingProblemAppJet.Library
{
    [Script]
    public static class Extensions
    {
        public static string ToImage(this string src)
        {
            return "<img src='" + src + "' />";
        }
    }
}
