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

            page.Badhost.onclick +=
                delegate
                {
                    service.__connect_badhost("", value => value.ToDocumentTitle());

                };

            page.Baduser.onclick +=
            delegate
            {
                service.__connect_baduser("", value => value.ToDocumentTitle());

            };

            page.Clear.onclick +=
                delegate
                {
                    page.sql.value = "";
                    page.output.Clear();
                };

            page.Go.onclick +=
                delegate
                {
                    Action<string, string> yield_field =
                        (name, type) =>
                        {
                            page.sql.value += "\n" + new { name, type };

                        };

                    Action<XElement> yield_resultset = resultset =>
                            {
                                page.output.Add(resultset);

                            };

                    service.__mysqli_query(page.sql.value,
                        y: value => value.ToDocumentTitle(),
                        yield_field: yield_field,
                       yield_resultset: yield_resultset

                    );

                    page.sql.value = "";
                };

            // Send data from JavaScript to the server tier
            service.WebMethod2(
                @"A string from JavaScript.",
                value => value.ToDocumentTitle()
            );
        }

    }
}
