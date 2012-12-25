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
using ConsoleByCookie.Design;
using ConsoleByCookie.HTML.Pages;
using SQLiteWithDataGridView.Library;
using ScriptCoreLib.JavaScript.Runtime;

namespace ConsoleByCookie
{
    /// <summary>
    /// Your client side code running inside a web browser as JavaScript.
    /// </summary>
    public sealed class Application
    {
        // "X:\jsc.svn\examples\javascript\EventSourceForWebServiceYield\EventSourceForWebServiceYield.sln"

        public readonly ApplicationWebService service = new ApplicationWebService();

        /// <summary>
        /// This is a javascript application.
        /// </summary>
        /// <param name="page">HTML document rendered by the web server which can now be enhanced.</param>
        public Application(IApp page)
        {
            var f = new ConsoleForm();

            f.InitializeConsoleFormWriter();
            f.Show();


            var session = new Cookie("session").DefaultToRandomInt32();


            Console.WriteLine("\n Console has been redirected!");
            Console.WriteLine("\n " + new { session = session.IntegerValue.ToString("x8") });



            page.CheckServerForSession.onclick +=
                delegate
                {
                    service.CheckServerForSession("" + session.IntegerValue, Console.WriteLine);
                };

            page.DoLongOperation.onclick +=
                delegate
                {
                    page.DoLongOperation.disabled = true;

                    // can we send IEvent as argument directly?
                    // can we set a field on client side and have
                    // it updated on each call?
                    service.DoLongOperation("" + session.IntegerValue,
                        delegate
                        {
                            page.DoLongOperation.disabled = false;

                        }
                    );
                };
        }

    }

    public static class X
    {
        public static Cookie DefaultToRandomInt32(this Cookie c)
        {
            if (string.IsNullOrEmpty(c.Value))
            {
                var r = new Random();
                var id = r.Next();

                c.IntegerValue = id;
            }

            return c;
        }
    }
}
