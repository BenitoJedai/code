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
using ScriptCoreLib.JavaScript.Runtime;

using SimpleMySQLiConsole.Library;

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
            #region titlestyle
            Action titlestyle =
                delegate
                {
                    Native.Document.getElementsByTagName("title").WithEach(
                       title =>
                       {



                           if (title.innerText.Contains("errno"))
                               if (!title.innerText.Contains("errno = 0"))
                               {
                                   title.className = "onerror";
                                   return;

                               }

                           if (Native.Document.body.scrollTop != 0)
                           {

                               title.className = "onscroll";
                           }
                           else
                           {
                               title.className = "";
                           }

                           Native.Document.body.style.paddingTop = title.clientHeight + "px";
                       }
                   );
                };

            Native.Window.onscroll +=
                delegate
                {
                    titlestyle();
                };

            new Timer(
                delegate
                {
                    titlestyle();
                }
            ).StartInterval(1000 / 15);
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
                    "...".ToDocumentTitle();

                    Action<string, string> yield_field =
                        (name, type) =>
                        {
                            page.sql.value += "\n" + new { name, type };

                        };

                    Action<XElement> yield_resultset =
                        resultset =>
                        {
                            page.output.Add(resultset);


                            resultset.ToForm();
                        };

                    var sql = "";

                    sql += page.sql0.value;

                    if (sql.Length > 0)
                        sql += ";";

                    sql += page.sql.value;

                    service.__mysqli_query(sql,
                        __y: value =>
                            {
                                Console.WriteLine(value);
                                value.ToDocumentTitle();
                            },

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
