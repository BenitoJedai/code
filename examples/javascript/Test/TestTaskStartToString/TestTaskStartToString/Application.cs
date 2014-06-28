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
using TestTaskStartToString;
using TestTaskStartToString.Design;
using TestTaskStartToString.HTML.Pages;
using System.Threading;
using System.IO;

namespace TestTaskStartToString
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
            // X:\jsc.svn\examples\javascript\test\TestGetUninitializedObject\TestGetUninitializedObject\Application.cs

            Console.SetOut(new xConsole());


            new xFoo { }.With(
                async state =>
                {
                    //var x = await Task.Factory.StartNew(

                    var t = new Task<xFoo>(
                            state:
                            state,

                            function:
                            xstate =>
                            {

                                var isFoo = xstate is xFoo;

                                // inside task. does scope have tostring? {{ ManagedThreadId = 10, isFoo = false, xstate = [object Object] }}
                                // inside task. does scope have tostring? {{ ManagedThreadId = 10, isFoo = true, xstate = xFoo: {{ xField1 = field1 {{ Counter = 1 }} }} }}
                                Console.WriteLine("inside task. does scope have tostring? " + new { Thread.CurrentThread.ManagedThreadId, isFoo, xstate });

                                return new xFoo { Tag = "inside task" };
                            }
                    );

                    // __worker_onfirstmessage: { href = http://192.168.43.252:28806/view-source#worker, MethodToken = AgAABmejYjqz6lg77adLEQ, MethodType = FuncOfObjectToObject, IsIProgress = false, IsTuple2_Item1_IsIProgress = false, ManagedThreadId = 10, state = [object Object] }

                    t.Start();
                    

                    {
                        var x = await t;
                        var isFoo = x is xFoo;

                        // after task. does x have tostring? {{ ManagedThreadId = 1, isFoo = true, x = [object Object] }}
                        Console.WriteLine("after task. does x have tostring? " + new { Thread.CurrentThread.ManagedThreadId, isFoo, x });
                    }
                }
            );


        }

        public class xFoo
        {
            public string Tag = "Tag!";


            //0:32ms xFoo ctor {{ Counter = 2 }
            //0:32ms GetUninitializedObject
            //0:33ms xFoo ctor {{ Counter = 4 }}
            //0:33ms {{ oo = {{ xField1 = field1 {{ Counter = 3 }} }} }}

            static int Counter = 1;

            public string xField1 = "field1 " + new { Counter = Counter++ };

            public xFoo()
            {
                Console.WriteLine("xFoo ctor " + new { Counter = Counter++ });
            }

            public override string ToString()
            {
                return "xFoo: " + new { xField1 }.ToString();
            }
        }
    }


    #region xConsole
    //class xConsole : StringWriter
    [Obsolete("jsc:js does not allow to overrider an override? we need it for SpecialFieldInfo to work!")]
    class xConsole : TextWriter
    {
        // http://www.danielmiessler.com/study/encoding_encryption_hashing/
        [Obsolete("can we have encrypted encoding?")]
        public override Encoding Encoding
        {
            get
            {
                throw new NotImplementedException();
            }
        }

        public override void Write(string value)
        {
            var p = new IHTMLCode { innerText = value }.AttachToDocument();
            var s = p.style;

            // jsc, enum tostring?
            if (Console.ForegroundColor == ConsoleColor.Red)
                s.color = "red";

            if (Console.ForegroundColor == ConsoleColor.Blue)
                s.color = "blue";

            if (Console.ForegroundColor == ConsoleColor.Gray)
                s.color = "gray";

            if (Console.ForegroundColor == ConsoleColor.Yellow)
                s.color = "yellow";

            if (Console.ForegroundColor == ConsoleColor.Magenta)
                s.color = "magneta";
        }

        public override void WriteLine(object value)
        {
            WriteLine("" + value);
        }

        public override void WriteLine(string value)
        {
            //Console.WriteLine(new { value });


            Write(value);

            new IHTMLBreak { }.AttachToDocument();
        }
    }
    #endregion

}