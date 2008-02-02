using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ScriptCoreLib.ActionScript.flash.display;
using ScriptCoreLib.ActionScript.BCLImplementation.System;
using ScriptCoreLib.ActionScript.flash.events;

namespace ScriptCoreLib.ActionScript.Extensions
{
    [Script]
    public static class CommonExtensions
    {
        public static void CombineDelegate<T>(this EventDispatcher _this, Action<T> value, string name)
            where T : Event
        {
            _this.addEventListener(name, value.ToFunction(), false, 0, false);
        }

        public static void RemoveDelegate<T>(this EventDispatcher _this, Action<T> value, string name)
            where T : Event
        {
            _this.removeEventListener(name, value.ToFunction(), false);
        }


        public static Function ToFunction(this Delegate e)
        {
            return ((__Delegate)(object)e).FunctionPointer;
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
