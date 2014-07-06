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
using TestNativeStaticDelegateCall;
using TestNativeStaticDelegateCall.Design;
using TestNativeStaticDelegateCall.HTML.Pages;

namespace TestNativeStaticDelegateCall
{
    [Script(ExternalTarget = "window", HasNoPrototype = true)]
    class xwindow
    {
        public static event Action xrequestAnimationFrame
        {
            add
            {
                // add xrequestAnimationFrame {{ value = [object Object] }}
                //in xrequestAnimationFrame

                var isDelegate = value is Delegate;


                new IHTMLPre { "add xrequestAnimationFrame " + new { isDelegate, value } }.AttachToDocument();

                Native.window.requestAnimationFrame +=
                    delegate
                {
                    // will it work???
                    value();
                };
            }

            remove { }
        }
    }

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
            // https://sites.google.com/a/jsc-solutions.net/backlog/knowledge-base/2014/20140705/20140706

            xwindow.xrequestAnimationFrame +=
                delegate
            {
                new IHTMLPre { "in xrequestAnimationFrame" }.AttachToDocument();
            };
        }

    }
}
