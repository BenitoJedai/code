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
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using TestCSSAttrExpression;
using TestCSSAttrExpression.Design;
using TestCSSAttrExpression.HTML.Pages;

namespace TestCSSAttrExpression
{
    /// <summary>
    /// Your client side code running inside a web browser as JavaScript.
    /// </summary>
    public sealed class Application : ApplicationWebService
    {
        static object by(Expression<Func<IHTMLElement, object>> f)
        {
            return null;
        }

        static void test()
        {
            var x = by(a => a.title);

        }

        /// <summary>
        /// This is a javascript application.
        /// </summary>
        /// <param name="page">HTML document rendered by the web server which can now be enhanced.</param>
        public Application(IApp page)
        {
            // https://sites.google.com/a/jsc-solutions.net/backlog/knowledge-base/2013/201312/20131208-expression

            // script: error JSC1000: No implementation found for this native method, please implement 
            // [static System.Linq.Expressions.Expression.Parameter(System.Type, System.String)]

            //            arg[0] is typeof System.RuntimeFieldHandle
            //script: error JSC1000: No implementation found for this native method, please implement [static System.Reflection.FieldInfo.GetFieldFromHandle(System.RuntimeFieldHandle)]

            test();

        }

    }
}
