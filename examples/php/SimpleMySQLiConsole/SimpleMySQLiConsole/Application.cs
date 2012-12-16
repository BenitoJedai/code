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
using SimpleMySQLiConsole.Design;
using SimpleMySQLiConsole.HTML.Pages;

namespace SimpleMySQLiConsole
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
            #region onscroll
            Native.Window.onscroll +=
                delegate
                {
                    Native.Document.getElementsByTagName("title").WithEach(
                        title =>
                        {


                            if (Native.Document.body.scrollTop != 0)
                            {

                                title.className = "onscroll";
                            }
                            else
                            {
                                title.className = "";
                            }
                        }
                    );
                };
            #endregion

            page.Go.onclick +=
                delegate
                {
                    service.__mysqli_query(page.sql.value,
                        value => value.ToDocumentTitle()
                    );

                    page.sql.value = "";
                };

            @"Hello world".ToDocumentTitle();
            // Send data from JavaScript to the server tier
            service.WebMethod2(
                @"A string from JavaScript.",
                value => value.ToDocumentTitle()
            );
        }

    }
}
