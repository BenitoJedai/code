using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ScriptCoreLib;

namespace RayCaster6.ActionScript
{
    /// <summary>
    /// This class defines the extension methods for this project
    /// </summary>
    [Script]
    internal static class Extensions
    {


        public static int Floor(this int e)
        {
            return e;
        }

        //[Script(OptimizedCode = "return int(e);")]
        [Script(IsDebugCode = true)]
        public static int Floor(this double e)
        {
            return (int)e;
        }

    }
}
