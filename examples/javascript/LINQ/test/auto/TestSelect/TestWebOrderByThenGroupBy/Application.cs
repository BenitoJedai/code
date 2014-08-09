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
using TestWebOrderByThenGroupBy;
using TestWebOrderByThenGroupBy.Design;
using TestWebOrderByThenGroupBy.HTML.Pages;
using ScriptCoreLib.Query.Experimental;
using System.Data.SQLite;

namespace TestWebOrderByThenGroupBy
{
    /// <summary>
    /// Your client side code running inside a web browser as JavaScript.
    /// </summary>
    public sealed class Application : ApplicationWebService
    {
        // X:\jsc.svn\examples\javascript\LINQ\LINQWebCamAvatars\LINQWebCamAvatars\Application.cs

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
            // X:\jsc.svn\examples\javascript\LINQ\ClickCounter\ClickCounter\Application.cs

            new PerformanceResourceTimingData2ApplicationPerformance().Delete();

            new PerformanceResourceTimingData2ApplicationPerformance().Insert(
                new PerformanceResourceTimingData2ApplicationPerformanceRow
            {
                connectEnd = 9,
                connectStart = 5,
                Tag = "first insert"
            },

                new PerformanceResourceTimingData2ApplicationPerformanceRow
            {
                connectStart = 5,
                connectEnd = 111,
                Tag = "middle insert"
            },

                new PerformanceResourceTimingData2ApplicationPerformanceRow
            {
                connectStart = 5,
                connectEnd = 11,
                Tag = "Last insert, selected by group by"
            }
            );




            new IHTMLPre { "count: ", new PerformanceResourceTimingData2ApplicationPerformance().CountAsync() }.AttachToDocument();
            // count: 3


            var fa = (
                from x in new PerformanceResourceTimingData2ApplicationPerformance()
                    // MYSQL and SQLITE seem to behave differently? in reverse actually!
                    //orderby x.Key ascending
                orderby x.connectEnd ascending
                // { f = { Tag = middle insert } }
                group x by x.connectStart into gg
                select new
                {
                    //c = gg.Count(),
                    gg.Last().Tag
                }


            // [IL]: Error: [X:\jsc.svn\examples\javascript\LINQ\test\auto\TestSelect\TestWebOrderByThenGroupBy\bin\Debug\staging\TestWebOrderByThenGroupBy.Application\TestWebOrderByThenGroupBy.Application.exe : ScriptCoreLib.Query.Experimental.QueryExpressionBuilder+SQLWriter`1+<>c__DisplayClass1::<.ctor>b__52[offset 0x0409] Error.

            // browser talks async. what happens if we forget it?
            //).FirstOrDefaultAsync();
            ).FirstOrDefault();


            new IHTMLPre { "fa: ", () => fa }.AttachToDocument();

        }

    }
}
