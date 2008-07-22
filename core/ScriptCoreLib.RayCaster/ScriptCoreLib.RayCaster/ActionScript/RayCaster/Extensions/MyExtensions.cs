using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ScriptCoreLib.ActionScript.RayCaster.Extensions
{
    [Script]
    internal static class MyExtensions
    {
        public static int Floor(this double e)
        {
            return (int)e;
        }
    }
}
