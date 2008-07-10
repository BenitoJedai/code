using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ScriptCoreLib;
using ScriptCoreLib.ActionScript.flash.display;

namespace FlashTextScreenSaver.ActionScript
{
    /// <summary>
    /// This class defines the extension methods for this project
    /// </summary>
    [Script]
    internal static class Extensions
    {
        public static double GetOffsetRight(this DisplayObject e)
        {
            return e.x + e.width;
        }
    }
}
