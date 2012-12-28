using ScriptCoreLib;
using ScriptCoreLib.Delegates;
using ScriptCoreLib.Extensions;
using ScriptCoreLib.JavaScript;
using ScriptCoreLib.JavaScript.Components;
using ScriptCoreLib.JavaScript.DOM;
using ScriptCoreLib.JavaScript.DOM.HTML;
using ScriptCoreLib.JavaScript.Extensions;
using System;
using System.Linq;
using System.Text;
using System.Xml.Linq;
using TestScriptLoadedEvent.Design;
using TestScriptLoadedEvent.Foo;
using TestScriptLoadedEvent.HTML.Pages;

namespace TestScriptLoadedEvent
{
    /// <summary>
    /// Your client side code running inside a web browser as JavaScript.
    /// </summary>
    public sealed class Application
    {
        public readonly ApplicationWebService service = new ApplicationWebService();

        /// <summary>
        /// This is a javascript application.
        /// </summary>
        /// <param name="page">HTML document rendered by the web server which can now be enhanced.</param>
        public Application(IApp page)
        {
            var bar_script = new bar().Content;


            bar_script.onload +=
                delegate
                {
                    Console.WriteLine("at new bar().Content.onload");

                    dynamic window = Native.Window;

                    Action bar = () =>
                    {
                        IFunction f = window.bar;
                        f.apply(Native.Window);
                    };


                    new IHTMLButton
                    {
                        innerText = "bar"
                    }.AttachToDocument().onclick +=
                        delegate
                        {
                            bar();
                        };
                };

            bar_script.AttachToDocument();

        }

    }
}
