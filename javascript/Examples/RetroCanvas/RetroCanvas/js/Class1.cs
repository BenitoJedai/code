using ScriptCoreLib;
using ScriptCoreLib.JavaScript;
using ScriptCoreLib.JavaScript.Extensions;
using ScriptCoreLib.JavaScript.DOM.HTML;
using ScriptCoreLib.Shared.Drawing;
using ScriptCoreLib.Shared.Lambda;
using System;


namespace RetroCanvas.js
{
    [Script, ScriptApplicationEntryPoint(IsClickOnce = true)]
    public class Class1
    {

        public Class1()
        {
            var x = new IHTMLDiv();

            x.style.SetLocation(0, 0, Native.Window.Width, Native.Window.Height);

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



            Native.Document.onkeydown +=
                ev => Append("down: " + ev.KeyCode);


            Native.Document.onkeyup +=
                ev => Append("up: " + ev.KeyCode);

            Native.Document.onkeypress +=
                ev => Append("press: " + ev.KeyCode);



        }

        static Class1()
        {
            typeof(Class1).SpawnTo(i => new Class1());
        }


    }

}
