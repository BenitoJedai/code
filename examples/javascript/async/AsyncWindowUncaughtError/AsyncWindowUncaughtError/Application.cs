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
using AsyncWindowUncaughtError;
using AsyncWindowUncaughtError.Design;
using AsyncWindowUncaughtError.HTML.Pages;

namespace AsyncWindowUncaughtError
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
            // https://developer.mozilla.org/en-US/docs/Web/API/GlobalEventHandlers.onerror
            // http://www.w3schools.com/jsref/event_onerror.asp

            // http://stackoverflow.com/questions/12746034/how-to-get-error-event-details-in-firefox-using-addeventlistener
            // https://mikewest.org/2013/08/debugging-runtime-errors-with-window-onerror
            // https://code.google.com/p/chromium/issues/detail?id=270005
            // http://www.w3schools.com/jsref/event_onerror.asp

            page.Header.css[Native.window.async.onerror].style.backgroundColor = "red";

            (this as IUncaughtErrorHandler).With(
                h =>
                {
                    Native.window.onerror += e =>
                    {
                        h.onerror(
                            new IUncaughtErrorHandlerArguments
                            {
                                message = e.message,
                                lineno = e.lineno,
                                filename = e.filename,
                                stack = "" + e.error.stack
                            }
                        );
                    };
                }
            );

            // { message = Uncaught TypeError: Cannot set property 'innerHTML' of null, lineno = 50549, filename = http://192.168.43.252:13969/view-source }
            Native.window.onerror +=
                e =>
                {
                    page.Header.innerText = new { e.message, e.lineno, e.filename, e.error }.ToString();
                };

            //AppDomain.CurrentDomain.u

            // error [object ErrorEvent]
            // https://developer.mozilla.org/en-US/docs/Web/API/ErrorEvent
            // window.onerror for syntax errors and using the error event for load errors.
            //Native.window.addEventListener("error",
            //    new Action<string>(
            //        e =>
            //        {
            //            Native.window.alert("error " + e);
            //        }
            //    )
            //);

            Native.document.documentElement.css.hover.style.backgroundColor = "cyan";

            Native.document.documentElement.onclick +=
                delegate
                {

                    IHTMLElement u = null;



                    u.innerHTML = "cause npe";
                };

        }

    }
}
