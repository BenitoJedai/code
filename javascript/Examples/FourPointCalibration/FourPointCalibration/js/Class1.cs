using ScriptCoreLib;
using ScriptCoreLib.JavaScript;
using ScriptCoreLib.JavaScript.Extensions;
using ScriptCoreLib.JavaScript.DOM.HTML;
using ScriptCoreLib.Shared.Drawing;
using ScriptCoreLib.Shared.Lambda;


namespace FourPointCalibration.js
{
    [Script, ScriptApplicationEntryPoint(IsClickOnce = true)]
    public class Class1
    {

        public Class1()
        {

            var btn = new IHTMLButton("Hello World!").AttachToDocument();

            var counter = new IHTMLSpan().AttachTo(btn);

            counter.style.margin = "1em";

            var i = 0;

            btn.onclick += ev =>
                {
                    i++;

                    counter.innerText = "(" + i + ")";

                    counter.style.color = Color.FromRGB(
                        0xff.Random(),
                        0xff.Random(),
                        0xff.Random()
                    );
                };
        }

        static Class1()
        {
            typeof(Class1).SpawnTo(i => new Class1());
        }


    }

}
