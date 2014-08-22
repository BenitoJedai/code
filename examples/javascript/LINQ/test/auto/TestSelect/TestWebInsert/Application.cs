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
using TestWebInsert;
using TestWebInsert.Design;
using TestWebInsert.HTML.Pages;
using ScriptCoreLib.Query.Experimental;

namespace TestWebInsert
{
    /// <summary>
    /// Your client side code running inside a web browser as JavaScript.
    /// </summary>
    public sealed class Application : ApplicationWebService
    {
        static Application()
        {
            #region QueryExpressionBuilder.WithConnection
            QueryExpressionBuilder.WithConnection =
                y =>
                {
                    var cc = new SQLiteConnection();
                    cc.Open();
                    y(cc);
                    cc.Dispose();
                };
            #endregion
        }

        /// <summary>
        /// This is a javascript application.
        /// </summary>
        /// <param name="page">HTML document rendered by the web server which can now be enhanced.</param>
        public Application(IApp page)
        {

            new { }.With(
                async delegate
            {
                new PerformanceResourceTimingData2ApplicationPerformance().Delete();

                var rid = await new PerformanceResourceTimingData2ApplicationPerformance().InsertAsync(
                    new PerformanceResourceTimingData2ApplicationPerformanceRow
                {
                    connectEnd = 9,
                    connectStart = 5,
                    Tag = "first insert"
                }

                );



                // inserts not complete yet?
                new IHTMLPre { "count: ", new PerformanceResourceTimingData2ApplicationPerformance().CountAsync() }.AttachToDocument();
                // count: 3


                new IHTMLPre { new { rid } }.AttachToDocument();
            }
            );
        }

    }
}
