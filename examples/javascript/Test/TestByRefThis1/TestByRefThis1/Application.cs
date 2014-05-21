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
using TestByRefThis1;
using TestByRefThis1.Design;
using TestByRefThis1.HTML.Pages;

namespace TestByRefThis
{
    struct __Invoke
    {
        public int state;

        public static void __forwardref(ref __Invoke that)
        {
            that.state++;

            Console.WriteLine("exit __forwardref " + new { that.state });
        }


        public void MoveNext()
        {
            Console.WriteLine("enter MoveNext " + new { state });

            var loc0 = this;

            // does JVM support it?
            //__forwardref(ref this);
            __forwardref(ref loc0);

            Console.WriteLine("exit MoveNext " + new { state });

            //0:14ms enter MoveNext { state = 5 }
            //0:14ms exit __forwardref { state = 6 }
            //0:14ms exit MoveNext { state = 6 }

            // CLR:
            //enter MoveNext { state = 5 }
            //exit __forwardref { state = 6 }
            //exit MoveNext { state = 5 }

        }
    }

    /// <summary>
    /// Your client side code running inside a web browser as JavaScript.
    /// </summary>
    public sealed class Application : ApplicationWebService
    {
        // X:\jsc.svn\examples\rewrite\Test\TestAsyncFinally\TestAsync\Program.cs
        // X:\jsc.svn\examples\javascript\test\TestByRefThis\TestByRefThis\Class1.cs
        // X:\jsc.svn\examples\java\test\JVMCLRByRefThis\JVMCLRByRefThis\Program.cs

        /// <summary>
        /// This is a javascript application.
        /// </summary>
        /// <param name="page">HTML document rendered by the web server which can now be enhanced.</param>
        public Application(IApp page)
        {
            __Invoke loc0;

            loc0.state = 5;
            loc0.MoveNext();
        }

    }
}
