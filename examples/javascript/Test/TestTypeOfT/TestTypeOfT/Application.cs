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
using TestTypeOfT;
using TestTypeOfT.Design;
using TestTypeOfT.HTML.Pages;
using System.Linq.Expressions;
using ScriptCoreLib.Query.Experimental;

namespace TestTypeOfT
{
    /// <summary>
    /// Your client side code running inside a web browser as JavaScript.
    /// </summary>
    public sealed class Application : ApplicationWebService
    {


        public static Type GetType<T>()
        {
            // e = qAUABi01lj2yX93nEIaSJw(zh4ABtC6ljmbrk8x5kK6iA(new ctor$Wh4ABhfpfj6IFLf_a4gLSZg(type$G6QsNaFnCzOv9wWy1Pr_aSQ)), 'e');

            //  b = zh4ABtC6ljmbrk8x5kK6iA(new ctor$Wh4ABhfpfj6IFLf_a4gLSZg(type$AAAAAAAAAAAAAAAAAAAAAA));
            return typeof(T);
        }

        public static IQueryStrategy<ScriptCoreLib.Query.Experimental.QueryExpressionBuilder.IQueryStrategyGrouping<TKey, TSource>>
             GroupBy<TSource, TKey>( IQueryStrategy<TSource> source,
            Expression<Func<TSource, TKey>> keySelector)
        {
            // e = qAUABi01lj2yX93nEIaSJw(zh4ABtC6ljmbrk8x5kK6iA(new ctor$Wh4ABhfpfj6IFLf_a4gLSZg(type$AAAAAAAAAAAAAAAAAAAAAA)), 'e');

            // ReferenceError: type$AAAAAAAAAAAAAAAAAAAAAA is not defined
            // X:\jsc.svn\examples\javascript\LINQ\test\auto\TestSelect\TestWebOrderByThenGroupBy\Application.cs
            // jsc. need to do MakeArray?

            Expression<Func<TSource, TSource>> elementSelector = e => e;

            return null;
        }




        /// <summary>
        /// This is a javascript application.
        /// </summary>
        /// <param name="page">HTML document rendered by the web server which can now be enhanced.</param>
        public Application(IApp page)
        {

        }

    }
}
