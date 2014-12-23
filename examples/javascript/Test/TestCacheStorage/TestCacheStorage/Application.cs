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
using TestCacheStorage;
using TestCacheStorage.Design;
using TestCacheStorage.HTML.Pages;

namespace TestCacheStorage
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

            // http://blog.chromium.org/2014/12/chrome-40-beta-powerful-offline-and.html

            new IHTMLButton { "window.caches.keys() " + new { Native.window.caches } }.AttachToDocument().onclick += delegate
            {
                Native.window.caches.keys().then(
                    x =>
                    {
                        new IHTMLPre { x }.AttachToDocument();
                    }
                );

            };


        }

    }
}
