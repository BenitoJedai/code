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
using TestActivatorWithArgs;
using TestActivatorWithArgs.Design;
using TestActivatorWithArgs.HTML.Pages;

namespace TestActivatorWithArgs
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
            // X:\jsc.svn\examples\javascript\async\Test\TestDelegateObjectScopeInspection\TestDelegateObjectScopeInspection\Application.cs
            var dontOptimize = new Foo("dontOptimize");

            var type = typeof(Foo);



            var afterCreateInstance = (Foo)Activator.CreateInstance(
                type,
                "hello world"
            );

            //0:46ms { { Foo_ctor_arg0 = dontOptimize } }
            //0:47ms { { Foo_ctor_arg0 = hello world } }
            //0:48ms { { afterCreateInstance = [object Object] } }

            // 0:40ms {{ Foo_ctor_arg0 = hello world }}
            Console.WriteLine(new { afterCreateInstance.Foo_ctor_arg0 });
        }

    }

    class Foo(public string Foo_ctor_arg0 = "?")
    {
        //public Foo(string Foo_ctor_arg0)
        //{
        //    Console.WriteLine(new { Foo_ctor_arg0 });
        //}
    }
}
