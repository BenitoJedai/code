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
using VisualizeWhere;
using VisualizeWhere.Design;
using VisualizeWhere.HTML.Pages;
using System.Linq.Expressions;

namespace VisualizeWhere
{
    /// <summary>
    /// Your client side code running inside a web browser as JavaScript.
    /// </summary>
    public sealed class Application : ApplicationWebService
    {
        class ConstrainedApplicationPerformance : IQueryStrategtForVisualization<Data.PerformanceResourceTimingData2ApplicationPerformanceRow>
        {

        }

        public Application(IApp page)
        {
            //QueryStrategyOfTRowExtensions
            // script: error JSC1000: No implementation found for this native method, please implement [static System.Reflection.PropertyInfo.op_Inequality(System.Reflection.PropertyInfo, System.Reflection.PropertyInfo)]

            // lets viz the LINQ expressions
            var q =
                from x in new ConstrainedApplicationPerformance()

                let xg0 = new { x.connectEnd }

                let xc = x.connectEnd
                where xc > 0

                let xx = x.domComplete

                select new
                {
                    xx,
                    x.connectEnd,

                    xxx = new { xg0 }
                };
        }

    }

    interface IQueryStrategtForVisualization<T>
    {

    }

    static class VE
    {
        public static IQueryStrategtForVisualization<TResult>
            Select<TSource, TResult>
            (
             this IQueryStrategtForVisualization<TSource> source,
             Expression<Func<TSource, TResult>> selector
            )
        {
            new IHTMLPre { "select " + new { selector } }.AttachToDocument();

            var asLambdaExpression = selector as LambdaExpression;
            if (asLambdaExpression != null)
            {
                var asNewExpression = asLambdaExpression.Body as NewExpression;
                if (asNewExpression != null)
                {
                    asNewExpression.Arguments.WithEachIndex(
                        (SourceArgument, index) =>
                        {
                            new IHTMLPre { " new " + new { SourceArgument } }.AttachToDocument();
                        }
                    );
                }
            }

            return null;
        }

        public static IQueryStrategtForVisualization<TElement> Where<TElement>(this IQueryStrategtForVisualization<TElement> source, Expression<Func<TElement, bool>> filter)
        {
            new IHTMLPre { "where (filter)" }.AttachToDocument();

            return null;
        }
    }
}
