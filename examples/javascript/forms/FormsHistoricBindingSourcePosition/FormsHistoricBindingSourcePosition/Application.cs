using FormsHistoricBindingSourcePosition;
using FormsHistoricBindingSourcePosition.Design;
using FormsHistoricBindingSourcePosition.HTML.Pages;
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
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;

namespace FormsHistoricBindingSourcePosition
{
    /// <summary>
    /// Your client side code running inside a web browser as JavaScript.
    /// </summary>
    public sealed class Application : ApplicationWebService
    {
        public readonly ApplicationControl content = new ApplicationControl();

        /// <summary>
        /// This is a javascript application.
        /// </summary>
        /// <param name="page">HTML document rendered by the web server which can now be enhanced.</param>
        public Application(IApp page)
        {
            content.AttachControlToDocument();


            #region CurrentChanged
            content.navigationOrdersNavigateBindingSourceBindingSource.CurrentChanged +=
                delegate
                {
                    Console.WriteLine(
                        new
                        {
                            Native.document.location.pathname,
                            Native.document.location.search,
                            Native.document.location.hash,
                            Native.document.location.href,
                        }
                    );


                    // replace current url
                    Native.window.history.replaceState(
                        null,
                        null,
                        url: content.CurrentZeDocumentTextzNavigateRow.hash
                    );


                    Console.WriteLine(
                        new
                        {
                            Native.document.location.pathname,
                            Native.document.location.search,
                            Native.document.location.hash,
                            Native.document.location.href,
                        }
                    );

                };
            #endregion


            Console.WriteLine(
                new
                {
                    Native.document.location.pathname,
                    Native.document.location.search,
                    Native.document.location.hash,
                    Native.document.location.href,
                }
            );


            var url =
                Native.document.location.pathname
                + Native.document.location.search
                + Native.document.location.hash;

            // fill the blank.


            Console.WriteLine(
                new { url }
                );

            if (url.StartsWith("/#/"))
            {
                // redirect service.

                var nurl = url.SkipUntilOrEmpty("/#/");

                Console.WriteLine(
                    new { url, nurl }
                    );

                // replace current url
                Native.window.history.replaceState(
                    null,
                    null,
                    url: nurl
                );

            }

        }

    }
}
