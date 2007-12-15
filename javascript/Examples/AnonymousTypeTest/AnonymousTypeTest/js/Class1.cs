using ScriptCoreLib;
using ScriptCoreLib.JavaScript;
using ScriptCoreLib.JavaScript.Extensions;
using ScriptCoreLib.JavaScript.DOM.HTML;
using ScriptCoreLib.Shared.Drawing;
using ScriptCoreLib.Shared.Lambda;


namespace AnonymousTypeTest.js
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

                    var color = new
                    {
                        r = 0xff.Random(),
                        g = 0xff.Random(),
                        b = 0xff.Random()
                    };

                    counter.style.color = Color.FromRGB(
                        color.r,
                        color.g,
                        color.b
                    );
                };
        }

        static Class1()
        {
            typeof(Class1).SpawnTo(i => new Class1());
        }


    }

}
