using ScriptCoreLib;
using ScriptCoreLib.Delegates;
using ScriptCoreLib.Extensions;
using ScriptCoreLib.JavaScript;
using ScriptCoreLib.JavaScript.Components;
using ScriptCoreLib.JavaScript.DOM;
using ScriptCoreLib.JavaScript.DOM.HTML;
using ScriptCoreLib.JavaScript.Extensions;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using TestBeforeUnloadWebservice;
using TestBeforeUnloadWebservice.Design;
using TestBeforeUnloadWebservice.HTML.Pages;

namespace TestBeforeUnloadWebservice
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

            var st = new Stopwatch();

            st.Start();


            Native.window.onbeforeunload +=
                delegate
                {
                    this.elapsed = st.ElapsedMilliseconds;
                    this.yield();
                };
        }

    }
}
