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
using TestHistoricWithBaseElement;
using TestHistoricWithBaseElement.Design;
using TestHistoricWithBaseElement.HTML.Pages;

namespace TestHistoricWithBaseElement
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
            // X:\jsc.svn\core\ScriptCoreLib.Async\ScriptCoreLib.Async\JavaScript\DOM\HTML\IHTMLButtonAsyncExtensions.cs

            //Uncaught SecurityError: Failed to execute 'pushState' on 'History': A history state object with URL 'http://www.xmonese.xavalon.net/#/sign+in' cannot be created in a document with origin 'http://my.monese.com'. 

            //Native.document.baseUri

            new IHTMLBase { href = "http://otherhost/" }.AttachToHead();

            Console.WriteLine("before Historic");
            page.ClickToEnterANewHistoricState.Historic(
                async scope =>
                {
                    Native.document.body.style.borderTop = "1em solid red";

                    await scope;
                    Native.document.body.style.borderTop = "0em solid red";
                }
            );

            page.ClickToEnterANewHistoricState2.Historic(
                 async scope =>
                 {
                     Native.document.body.style.borderLeft = "1em solid red";

                     await scope;
                     Native.document.body.style.borderLeft = "0em solid red";
                 }
             );
        }

    }
}
