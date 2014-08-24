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
using TestWebSumLong;
using TestWebSumLong.Design;
using TestWebSumLong.HTML.Pages;
using ScriptCoreLib.Query.Experimental;
using System.Data.SQLite;

namespace TestWebSumLong
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
                //new PerformanceResourceTimingData2ApplicationPerformance().Delete();

                // does insert await for create to complete?
                var rid = await new PerformanceResourceTimingData2ApplicationPerformance().InsertAsync(
                    new PerformanceResourceTimingData2ApplicationPerformanceRow
                {
                    connectEnd = 9,
                    connectStart = 5,
                    Tag = "first insert"
                }

                );

                new IHTMLPre { "before sum..." + new { rid } }.AttachToDocument();


                var sumLongTask = (
                    from x in new PerformanceResourceTimingData2ApplicationPerformance()
                    where x.connectEnd == 13
                    select x.connectStart
                    ).SumAsync();


                var sumLong = await sumLongTask;

                // inserts not complete yet?
                new IHTMLPre { new { sumLong } }.AttachToDocument();
                // count: 3


            }
            );
        }

    }
}
