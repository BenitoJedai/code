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
using VisualizedJoin;
using VisualizedJoin.Design;
using VisualizedJoin.HTML.Pages;
using System.Linq.Expressions;

namespace VisualizedJoin
{
    /// <summary>
    /// Your client side code running inside a web browser as JavaScript.
    /// </summary>
    public sealed class Application : ApplicationWebService
    {
        class xRow
        {

            public int
                TransactionID,
                ClientID,
                ClientAccountID,
                Key;
        }

        class xTable : IQueryStrategyForVisualization<xRow>
        {

        }

        /// <summary>
        /// This is a javascript application.
        /// </summary>
        /// <param name="page">HTML document rendered by the web server which can now be enhanced.</param>
        public Application(IApp page)
        {
            // X:\jsc.svn\examples\javascript\LINQ\visualized\VisualizeWhere\VisualizeWhere\Application.cs

            //Error   6   Could not find an implementation of the query pattern for source type 'VisualizedJoin.Application.xTable'.  'Join' not found.X:\jsc.svn\examples\javascript\linq\visualized\VisualizedJoin\VisualizedJoin\Application.cs 50  23  VisualizedJoin
            // Error	5	A query body must end with a select clause or a group clause	X:\jsc.svn\examples\javascript\linq\visualized\VisualizedJoin\VisualizedJoin\Application.cs	57	29	VisualizedJoin

            var x = from t in new xTable()
                    join cStatus in new xTable() on t.TransactionID equals cStatus.TransactionID
                    join cData in new xTable() on cStatus.TransactionID equals cData.TransactionID
                    join cClient in new xTable() on t.ClientID equals cClient.ClientID
                    join cAcc in new xTable() on t.ClientAccountID equals cAcc.Key
                    select new { t, cStatus, cData, cClient, cAcc };



        }

    }


    interface IQueryStrategyForVisualization<T>
    {



    }

    static class X
    {
        public static
                    IQueryStrategyForVisualization<TResult>

                    Join<TOuter, TInner, TKey, TResult>(
                    this IQueryStrategyForVisualization<TOuter> xouter,
                    IQueryStrategyForVisualization<TInner> xinner,
                    Expression<Func<TOuter, TKey>> outerKeySelector,
                    Expression<Func<TInner, TKey>> innerKeySelector,
                    Expression<Func<TOuter, TInner, TResult>> resultSelector
            )
        {
            new IHTMLElement(IHTMLElement.HTMLElementEnum.hr).AttachToDocument();

            // lets print out what we see.
            // jsc, do you already have backgroun compilation going?
            // for now i have to hit ctrl f5 to see current version?
            // and it will be blocked by the old executable?
            // should jsc spawn a copy of itself?

            new IHTMLPre {

                "Join" ,

                new IHTMLBreak(),

                new IHTMLElement(IHTMLElement.HTMLElementEnum.blockquote) {

                    new { xouter },
                    new IHTMLBreak(),
                    new { xinner },
                    new IHTMLBreak(),
                    new { outerKeySelector },
                    new IHTMLBreak(),
                    new { innerKeySelector },
                    new IHTMLBreak(),
                    new { resultSelector },

                }
            }.AttachToDocument();

            return null;
        }
    }

}
