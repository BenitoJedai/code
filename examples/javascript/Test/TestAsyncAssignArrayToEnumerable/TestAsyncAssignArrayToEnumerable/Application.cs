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
using TestAsyncAssignArrayToEnumerable;
using TestAsyncAssignArrayToEnumerable.Design;
using TestAsyncAssignArrayToEnumerable.HTML.Pages;

namespace TestAsyncAssignArrayToEnumerable
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
            // X:\jsc.svn\examples\javascript\Test\TestAssignArrayToEnumerable\TestAssignArrayToEnumerable\Application.cs

            Invoke();
        }


        async void Invoke()
        {
            // X:\jsc.svn\examples\rewrite\Test\TestSwitchRewriteAsEnumerable\TestSwitchRewriteAsEnumerable\Class1.cs

            //f = new Array(1);
            //b.__stack0018__001d__0025 = f;
            //b.__stack0018__001d__0025[0] = { };
            //g = _6QUABoocDD2jQ9Bz7rBALA(b.__stack0018__001d__0025);

            IEnumerable<object> collection = new[] { new object() };


            // this causes a problem. why?
            // X:\jsc.svn\core\ScriptCoreLib\JavaScript\BCLImplementation\System\Threading\Tasks\Task\Task.Yield.cs
            // x:\jsc.svn\examples\javascript\async\asyncworkersourcesha1\asyncworkersourcesha1\application.cs
            await Task.Yield();

            //        f = new Array(1);
            //        b.__stack0019__001e__0026 = f;
            //        b.__stack0019__001e__0026[0] = { };
            //ref$c[0]._collection_5__1 = _6QUABoocDD2jQ9Bz7rBALA(b.__stack0019__001e__0026);


            ////  d = c.bwQABoBf2jWIHILvaqtMig();
            foreach (var item in collection)
            {
                Console.WriteLine(new { item });
                // 0:25ms {{ item = [object Object] }} 
            }
        }
    }
}
