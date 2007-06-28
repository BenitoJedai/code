using ScriptCoreLib;
using ScriptCoreLib.Shared;
using ScriptCoreLib.JavaScript;
using ScriptCoreLib.JavaScript.DOM;
using ScriptCoreLib.JavaScript.DOM.HTML;

using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GGearAlpha.js
{
    [Script]
    public static class Extensions
    {
        public static void Zoom(this IHTMLElement e, double z)
        {
            Zoom(e, z, e);
        }

        public static void Zoom(this IHTMLElement e, double z, IHTMLElement x)
        {

            e.style.width = System.Math.Floor(x.width * z) + "px";
            e.style.height = System.Math.Floor(x.height * z) + "px";
        }

        public static void SafeInvoke(this Action e)
        {
            if (e != null)
                e();
        }

        public static void ToImage(this string src, EventHandler<IHTMLImage> a)
        {
            var img = new IHTMLImage(src);

            img.InvokeOnComplete(a);
        }

        public static object Create(this System.Type e)
        {
            // we have a bug!
            // GGearAlpha.js.Extensions.Create
            // var UwAABpEQ0D6VJPcvXn_anDA = _5wMABsIroDG3IdF6ulsSww;

            if (e == null)
                throw new System.ArgumentNullException("e");

            return System.Activator.CreateInstance(e);
        }
    }
}
