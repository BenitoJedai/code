using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ScriptCoreLib;

namespace FlashZIndex.ActionScript
{
    /// <summary>
    /// This class defines the extension methods for this project
    /// </summary>
    [Script]
    internal static class MyExtensions
    {
        public static double Random(this int e)
        {
            return (Math.Round(new Random().NextDouble() * e));
        }

        public static int ToInt32(this double e)
        {
            return Convert.ToInt32(e);
        }
    }
}
