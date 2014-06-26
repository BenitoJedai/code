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
using WebSQLXElement;
using WebSQLXElement.Design;
using WebSQLXElement.HTML.Pages;
using ScriptCoreLib.Query.Experimental;
using System.Linq.Expressions;
using System.Data.SQLite;

namespace WebSQLXElement
{

    #region example generated data layer
    public class xApplicationPerformance : QueryExpressionBuilder.xSelect<xRow>
    {
        public xApplicationPerformance()
        {
            Expression<Func<xRow, xRow>> selector =
                (xRow) => new xRow
            {
                xmlString = xRow.xmlString,
                Key = xRow.Key,

                Tag = xRow.Tag,
                Timestamp = xRow.Timestamp
            };

            this.selector = selector;
        }
    }


    public enum xRowKey : long { }

    public class xRow
    {
        public xRowKey Key;

        [Obsolete("does jsc keep FieldType?")]
        public XElement xml;
        public string xmlString;

        public string Tag;
        public DateTime Timestamp;
    }
    #endregion



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
            new { }.With(
                async state =>
                {
                    // X:\jsc.svn\examples\javascript\Test\TestSQLiteConnection\TestSQLiteConnection\Application.cs

                    #region cc
                    var cc0 = new SQLiteConnection(
                        new SQLiteConnectionStringBuilder
                    {
                        DataSource = "file:PerformanceResourceTimingData2.xlsx.sqlite"
                    }.ToString()
                    );

                    cc0.Open();

                    var n = new xApplicationPerformance();

                    n.Create(cc0);
                    #endregion


                    await n.InsertAsync(cc0,
                         new xRow
                    {
                        Tag = "what about xml? ",

                        xmlString =
                            new XElement("div",
                                new XElement("h1", DateTime.Now.ToString()),
                                this.Header
                            ).ToString()
                    }
                     );


                    new IHTMLButton { "select all" }.AttachToDocument().onclick +=
                        async e =>
                    {

                        e.Element.Orphanize();

                        //e.Element;

                        var z = n.AsEnumerableAsync(cc0);

                        foreach (var item in await z)
                        {
                            new IHTMLDiv { innerHTML = item.xmlString }.AttachToDocument();

                        }
                    };
                }
            );

        }

    }
}
