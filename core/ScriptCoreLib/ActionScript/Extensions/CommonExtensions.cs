using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ScriptCoreLib.ActionScript.flash.display;

namespace ScriptCoreLib.ActionScript.Extensions
{
    [Script]
    public static class CommonExtensions
    {
        public static Function ToFunction(this Delegate e)
        {
            return null;
        }

        public static Stage SetFullscreen(this Stage s, bool value)
        {
            if (value)
                s.displayState = StageDisplayState.FULL_SCREEN;
            else
                s.displayState = StageDisplayState.NORMAL;

            return s;
        }
        
    }
}
