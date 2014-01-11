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
using TestReflectionField;
using TestReflectionField.Design;
using TestReflectionField.HTML.Pages;

namespace TestReflectionField
{
    sealed class Foo
    {
        public string Field1;
    }

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
            var ref0 = new Foo();

            var t = typeof(Foo);

            Console.WriteLine(new { t });
            Console.WriteLine(new { t.Name });

            var fields = t.GetFields();

            Console.WriteLine(new { fields.Length });

            // https://sites.google.com/a/jsc-solutions.net/backlog/knowledge-base/2014/201401/20140111-iquery/new
            fields.WithEach(x => Console.WriteLine(x.Name));

            // Uncaught TypeError: Cannot call method '_2QAABvEJMzKFrGX3QxjvzA' of null 

            var f = t.GetField("Field1");


            Native.document.body.innerText = new { f.Name }.ToString();

        }

    }
}
