using ScriptCoreLib;
using ScriptCoreLib.Delegates;
using ScriptCoreLib.Extensions;
using ScriptCoreLib.JavaScript;
using ScriptCoreLib.JavaScript.Components;
using ScriptCoreLib.JavaScript.DOM;
using ScriptCoreLib.JavaScript.DOM.HTML;
using ScriptCoreLib.JavaScript.Extensions;
using ScriptCoreLib.JavaScript.Windows.Forms;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using AsyncCustomElementExperiment;
using AsyncCustomElementExperiment.Design;
using AsyncCustomElementExperiment.HTML.Pages;

namespace AsyncCustomElementExperiment
{
    class future_work : IHTMLElement
    {
        public future_work()
        {
            // jsc rewriter needs to do some magic here.

            new IHTMLPre { "working..." }.AttachTo(this);
        }
    }

    // primary constructor with registerElement?
    class primaryconstructor_work() : IHTMLElement("work")
    {
        // jsc almost handles it correctly
        //  a[0]..ctor('work');
    }



    /// <summary>
    /// Your client side code running inside a web browser as JavaScript.
    /// </summary>
    public sealed class Application : ApplicationWebService
    {
        static Application()
        {
            Native.document.registerElement("x-work",
                async e =>
                {
                    var s = e.createShadowRoot();


                    new IHTMLPre { "working..." }.AttachTo(s);

                    for (int i = 0; i < 10; i++)
                    {
                        new IHTMLPre { "working... " + new { i } }.AttachTo(s);
                        await Task.Delay(500);
                    }

                    new IHTMLPre { "working... done" }.AttachTo(s);

                }
            );
        }

        /// <summary>
        /// This is a javascript application.
        /// </summary>
        /// <param name="page">HTML document rendered by the web server which can now be enhanced.</param>
        public Application(IApp page)
        {
            // The custom element type identifies a custom element interface 
            // and is a sequence of characters that must match the NCName production and contain a U+002D HYPHEN-MINUS character.
            // thats annoying

            // Uncaught SyntaxError: Failed to execute 'registerElement' on 'Document': Registration failed for type 'work'. The type name is invalid. 
            // Uncaught SyntaxError: Failed to execute 'registerElement' on 'Document': Registration failed for type '--work'. The type name is invalid. 
            new IHTMLElement("x-work").AttachToDocument();

        }

    }
}
