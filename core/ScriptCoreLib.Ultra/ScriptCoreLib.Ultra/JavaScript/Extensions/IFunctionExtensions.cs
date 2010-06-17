using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ScriptCoreLib.JavaScript.DOM;
using ScriptCoreLib.Ultra.Library;

namespace ScriptCoreLib.JavaScript.Extensions
{
    public static class IFunctionExtensions
    {
        public static IDisposable AtInterval(this Action h, int time)
        {
            var f = IFunction.OfDelegate(h);
            var i = Native.Window.setInterval(f, time);

            return new Disposable
            {
                AtDispose =
                    () => Native.Window.clearInterval(i)
            };
        }

        public static int setInterval(this IWindow w, Action h, int time)
        {
            return w.setInterval(IFunction.OfDelegate(h), time);
        }

        public static int setTimeout(this IWindow w, Action h, int time)
        {
            return w.setTimeout(IFunction.OfDelegate(h), time);
        }
    }
}
