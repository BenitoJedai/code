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
using CustomerPrimaryConstructorsWebApplication;
using CustomerPrimaryConstructorsWebApplication.Design;
using CustomerPrimaryConstructorsWebApplication.HTML.Pages;

namespace CustomerPrimaryConstructorsWebApplication
{
    /// <summary>
    /// Your client side code running inside a web browser as JavaScript.
    /// </summary>
    public sealed class Application : ApplicationWebService
    {
        // X:\jsc.svn\examples\javascript\RoslynEndUserPreviewExperiment\RoslynEndUserPreviewExperiment\Application.cs
        [Obsolete("how does this relate to the work jsc does for ActionScript?")]
        public class CustomerPrimaryConstructors(string first, string last = "goo")
        {
            public string First { get; } = first;
            public string Last { get; } = last;


            static void Invoke()
            {
                var x = new CustomerPrimaryConstructors("foo");

            }
        }

        /// <summary>
        /// This is a javascript application.
        /// </summary>
        /// <param name="page">HTML document rendered by the web server which can now be enhanced.</param>
        public Application(IApp page)
        {


            var x = new CustomerPrimaryConstructors("f");

            // {{ First = undefined, Last = undefined }}
            // in C# 6 ScriptCoreLib can remove explicit ICollection.Add to allow dictionary init?
            //var z = new { x.First, x.Last };

            // X:\jsc.svn\examples\javascript\test\TestRoslynAnonymousType\TestRoslynAnonymousType\Class1.cs

            new IHTMLPre { "First: " + x.First }.AttachToDocument();

            // anonymous type ctor was broken?
            new IHTMLPre { "Last: " + x.Last }.AttachToDocument();


        }

    }
}
