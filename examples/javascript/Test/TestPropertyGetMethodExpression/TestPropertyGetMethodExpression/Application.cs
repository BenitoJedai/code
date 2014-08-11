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
using TestPropertyGetMethodExpression;
using TestPropertyGetMethodExpression.Design;
using TestPropertyGetMethodExpression.HTML.Pages;
using System.Linq.Expressions;

namespace TestPropertyGetMethodExpression
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
            new Foo();
            // 0:57ms {{ Name = ngAABrqNnzancVCmzUtOPQ }} 


        }

    }

    class Foo
    {
        public Foo()
        {
            var x = new { a = "hello world" };

            Expression<Func<string>> e = () => x.a;

            // e.Body = { value(TestPropertyGetMethodExpression.Foo +<> c__DisplayClass0).x.a}
            //var z = e.Body as PropertyExpression;
            var z = e.Body;
            var zMemberExpression = (MemberExpression)e.Body;

            Console.WriteLine(new { zMemberExpression.Member.Name });
        }
    }
}
