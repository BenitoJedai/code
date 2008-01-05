using ScriptCoreLib;

using ScriptCoreLib.JavaScript.Controls;
using ScriptCoreLib.JavaScript;
using ScriptCoreLib.JavaScript.Extensions;
using ScriptCoreLib.JavaScript.Runtime;
using ScriptCoreLib.JavaScript.Serialized;
using ScriptCoreLib.JavaScript.DOM.HTML;
//using ScriptCoreLib.JavaScript.DOM.XML;
using ScriptCoreLib.JavaScript.DOM;

using ScriptCoreLib.Shared;
using ScriptCoreLib.Shared.Drawing;

namespace ColorPicker.source.js.Controls
{
    [Script, ScriptApplicationEntryPoint]
    public class ColorPicker
    {
        // http://cpaint.net/examples/colorpicker/index.html
        // http://acko.net/dev/farbtastic

        //public const string Alias = "fx.DemoControl";

        IHTMLDiv Control = new IHTMLDiv();

        public IStyle Style { get { return Control.style; } }

        public ColorPicker()
        {
            Control.AttachToDocument();

            System.Console.WriteLine("colors...");

            IHTMLImage palette = shared.Description.colors.palette;


            Control.style.position = IStyle.PositionEnum.relative;
            Control.style.backgroundColor = Color.Gray;
            Control.style.SetSize(277, 262);


            var layer = new IHTMLDiv();


            layer.style.SetLocation(1, 1, 275, 260);

            palette.ToBackground(layer);

            Control.appendChild(layer);


            layer.onmousemove +=
                delegate(IEvent ev)
                {

                    System.Console.WriteLine("" + ev.OffsetPosition + ", x " + layer.Bounds);

                    // jscolor will be merged with color sometime in the future

                    if (ev.OffsetX > 260)
                    {

                        var lum = (byte)System.Math.Round((double)(240 - ev.OffsetY / 260 * 240));

                        System.Console.WriteLine("lum: " + lum);

                        Native.Document.body.style.backgroundColor = JSColor.FromHLS(0, lum, 0);
                    }
                    else
                    {
                        var hue = (byte)System.Math.Round((double)(ev.OffsetX / 260 * 240));
                        var lum = (byte)System.Math.Round((double)(240 - ev.OffsetY / 260 * 240));

                        Native.Document.body.style.backgroundColor = JSColor.FromHLS(hue, lum, 240);
                    }

                };
        }


        static ColorPicker()
        {
            typeof(ColorPicker).Spawn();

        }
    }


}
