using ScriptCoreLib;
using ScriptCoreLib.JavaScript;
using ScriptCoreLib.JavaScript.Extensions;
using ScriptCoreLib.JavaScript.DOM.HTML;
using ScriptCoreLib.Shared.Drawing;
using ScriptCoreLib.Shared.Lambda;
using System;
using ScriptCoreLib.JavaScript.Runtime;


namespace RetroCanvas.js
{
    [Script, ScriptApplicationEntryPoint(IsClickOnce = true)]
    public class Class1
    {

        public Class1()
        {

            var x = new IHTMLDiv();

            var size = new
            {
                w = Native.Window.Width,
                h = Native.Window.Height
            };

            x.style.SetLocation(0, 0, size.w, size.h);

            x.style.backgroundColor = Color.Blue;
            x.style.color = Color.Yellow;
            x.style.overflow = ScriptCoreLib.JavaScript.DOM.IStyle.OverflowEnum.hidden;

            x.AttachToDocument();

            Action<string> Append =
                e =>
                {
                    x.appendChild(new IHTMLDiv(e));

                    if (x.childNodes.Length > 20) x.removeChild(x.firstChild);

                };

            Append("size: " + size);

            Native.Document.onkeydown +=
                ev => Append("keydown: " + ev.KeyCode);


            Native.Document.onkeyup +=
                ev => Append("keyup: " + ev.KeyCode);

            Native.Document.onkeypress +=
                ev => Append("keypress: " + ev.KeyCode);


            Native.Document.onclick +=
                ev => Append("click: " + ev.OffsetPosition);

        }


        static Class1()
        {
            typeof(Class1).SpawnTo(
                i =>
                {
                    // hide IE scrollbar

                    Func<IHTMLElement, IHTMLElement> WithStyle =
                        e =>
                        {
                            if (e == null)
                                return e;

                            e.style.overflow = ScriptCoreLib.JavaScript.DOM.IStyle.OverflowEnum.hidden;
                            e.style.margin = "0px";
                            e.style.padding = "0px";
                            return e;
                        };

                    WithStyle((IHTMLElement)WithStyle(Native.Document.body).parentNode);




                    Timer.DoAsync(
                        () => new Class1()
                    );
                }
            );
        }


    }

}
