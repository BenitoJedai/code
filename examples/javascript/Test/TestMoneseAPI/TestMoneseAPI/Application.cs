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
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using TestMoneseAPI;
using TestMoneseAPI.Design;
using TestMoneseAPI.HTML.Pages;

namespace TestMoneseAPI
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

            page.Go.onclick +=
                delegate
                {
                    var t = new Stopwatch();
                    t.Start();

                    var service = new monese.experimental.MoneseWebServices();

                    //{ value = a, ElapsedMilliseconds = 467, uid = 0 }
                    //{ value = air@, ElapsedMilliseconds = 305, uid = 12 }

                    service.GetUserIDAsync(
                        page.username.value,
                        "",
                        uid =>
                        {
                            // The calling thread cannot access this object because a different thread owns it.

                            new IHTMLPre {
                                 new
                                    {
                                        page.username.value,
                                        t.ElapsedMilliseconds,
                                        uid,
                                    }
                                 }.AttachToDocument();

                        }
                    );

                };

        }

        public static string UTF8ToBase64StringOrDefault(string e)
        {
            if (e == null)
                return null;

            var bytes = Encoding.UTF8.GetBytes(e);

            return Convert.ToBase64String(bytes);
        }
    }
}
