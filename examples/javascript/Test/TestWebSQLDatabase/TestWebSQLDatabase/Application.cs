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
using TestWebSQLDatabase;
using TestWebSQLDatabase.Design;
using TestWebSQLDatabase.HTML.Pages;

namespace TestWebSQLDatabase
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
            // as per http://www.c-sharpcorner.com/UploadFile/75a48f/html-5-web-sql-database/
            // X:\jsc.svn\core\ScriptCoreLib.Extensions\ScriptCoreLib.Extensions\Query\Experimental\QueryExpressionBuilder.cs
            // A Web SQL database only works in the latest versions of Safari, Google Chrome and Opera browsers.
            // no ff, ie?
            // what if the IE logo on rebuild would indicate what browser can run the current app based on analysis?
            // X:\jsc.svn\examples\javascript\LINQ\test\auto\TestSelect\TestSelectMath\Program.cs

        }

    }

    #region example generated data layer
    public class xApplicationPerformance : QueryExpressionBuilder.xSelect<xPerformanceResourceTimingData2ApplicationPerformanceRow>
    {
        public xApplicationPerformance()
        {
            Expression<Func<xPerformanceResourceTimingData2ApplicationPerformanceRow, xPerformanceResourceTimingData2ApplicationPerformanceRow>> selector =
                (xApplicationPerformance) => new xPerformanceResourceTimingData2ApplicationPerformanceRow
            {
                // : Field 'connectEnd' defined on type 'Program+xPerformanceResourceTimingData2ApplicationPerformanceRow' is not a field on the target object 
                // which is of type 'Program+xApplicationPerformance'.

                connectEnd = xApplicationPerformance.connectEnd,
                connectStart = xApplicationPerformance.connectStart,
                domComplete = xApplicationPerformance.domComplete,
                domLoading = xApplicationPerformance.domLoading,
                EventTime = xApplicationPerformance.EventTime,
                Key = xApplicationPerformance.Key,
                loadEventEnd = xApplicationPerformance.loadEventEnd,
                loadEventStart = xApplicationPerformance.loadEventStart,
                requestStart = xApplicationPerformance.requestStart,
                responseEnd = xApplicationPerformance.responseEnd,
                responseStart = xApplicationPerformance.responseStart,
                Tag = xApplicationPerformance.Tag,
                Timestamp = xApplicationPerformance.Timestamp
            };

            this.selector = selector;
        }
    }


    public enum xPerformanceResourceTimingData2ApplicationPerformanceKey : long { }

    public class xPerformanceResourceTimingData2ApplicationPerformanceRow
    {
        public long connectEnd;
        public long connectStart;
        public long domComplete;
        public long domLoading;
        public DateTime EventTime;
        public xPerformanceResourceTimingData2ApplicationPerformanceKey Key;
        public long loadEventEnd;
        public long loadEventStart;
        public long requestStart;
        public long responseEnd;
        public long responseStart;
        public string Tag;
        public DateTime Timestamp;

    }
    #endregion

}
