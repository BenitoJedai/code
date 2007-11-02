using ScriptCoreLib.JavaScript;
using ScriptCoreLib.JavaScript.DOM;
using ScriptCoreLib.JavaScript.DOM.HTML;
using ScriptCoreLib.JavaScript.Runtime;
using ScriptCoreLib;
using ScriptCoreLib.Shared;
using ScriptCoreLib.Shared.Drawing;

namespace ButterFly.source.js
{


    [Script]
    public class Butterfly
    {
        public static void Spawn(IHTMLElement e)
        {


            Native.Document.body.style.margin = "0px";
            Native.Document.body.style.padding = "0px";
            Native.Document.body.style.overflow = IStyle.OverflowEnum.hidden;

            e.style.width = "100%";
            e.style.height = "800px";

            e.style.backgroundColor = Color.FromRGB(209, 245, 245);

            IHTMLElement loading = new IHTMLElement(IHTMLElement.HTMLElementEnum.code, "loading...");

            loading.style.SetLocation(64, 64, 200, 64);

            e.appendChild(loading);
            new IHTMLImage("fx/gfx/buttryfly.gif").InvokeOnComplete(
                delegate
                {
                    loading.FadeOut();
                    IStyleSheet.Default.AddRule("*", "cursor: url('fx/gfx/nocursor.cur'), auto;", 0);

                    e.style.backgroundImage = "url(fx/gfx/buttryfly.gif)";
                    e.style.backgroundRepeat = "no-repeat";



                    e.DisableContextMenu();

                    e.onmousemove +=
                        delegate(IEvent i)
                        {

                            Console.WriteLine("pos: " + i.CursorPosition);

                            e.style.backgroundPosition = i.CursorX + "px " + i.CursorY + "px";


                        };
                });

        }
    }
}
