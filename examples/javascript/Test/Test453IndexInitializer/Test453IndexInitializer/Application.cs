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
using Test453IndexInitializer;
using Test453IndexInitializer.Design;
using Test453IndexInitializer.HTML.Pages;

namespace Test453IndexInitializer
{
    /// <summary>
    /// Your client side code running inside a web browser as JavaScript.
    /// </summary>
    public sealed class Application : ApplicationWebService
    {
        /// <summary>
        /// This is a javascript application.
        /// </summary>
        /// <param name="page">HTML document rendered by the web server which can now be enhanced.</param>
        public Application(IApp page)
        {
            // http://gigi.nullneuron.net/gigilabs/c-6-preview-index-initializers/
            var morse = new Dictionary<char, string>()
            {
                //  the original syntax is translated into dictionary .Add() calls, while the new one is translated into index assignments.

                ['A'] = ".-",
                ['B'] = "-...",
                ['C'] = "-.-.",
                ['D'] = "-..",
                ['E'] = ".",
                //...
            };

            // morse for C is -.-.
            // what about databinding? 
            new IHTMLPre { "morse for C is \{morse['C']}" }.AttachToDocument();

        }

    }
}
