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
using TestDelegateObjectScopeInspection;
using TestDelegateObjectScopeInspection.Design;
using TestDelegateObjectScopeInspection.HTML.Pages;
using System.IO;

namespace TestDelegateObjectScopeInspection
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
            Console.SetOut(new xConsole());
            // X:\jsc.svn\examples\javascript\async\test\TestDelayInsideWorker\TestDelayInsideWorker\Application.cs


            Foo.Invoke();

        }
    }

    public static class Foo
    {
        public static void Invoke()
        {
//            before delegate { { scopeField1 = field1 } }
//enter Inspection { { y = [object Object], Method = { InternalMethodToken = sAAABnvguzW_b3wQRWrbnyg }, Target = [object Object] } }
//            { { xRowType = < Namespace >.__c__DisplayClass0 } }
//            { { nRow = [object Object] } }
//            { { yy = [object Object] } }
//            inside the delegate { { scopeField1 = null } }
//{ { z = null } }

            var scopeField1 = "field1";

            Console.WriteLine("before delegate " + new { scopeField1 });

            Inspection(
                delegate
            {
                Console.WriteLine("inside the delegate  " + new { scopeField1 });

                return scopeField1;
            }
            );
        }

        static void Inspection(Func<string> y)
        {
            // enter Inspection {{ y = [object Object], Method = { MethodToken = owAABhluGjmdbjAyX2PIQA }, Target = [object Object] }}
            Console.WriteLine("enter Inspection " + new { y, y.Method, y.Target });

            // can we recreate the target on the other side of the thread?
            // do we have enough information on what the object is?

            var xRowType = y.Target.GetType();

            Console.WriteLine(new { xRowType });
            // {{ xRowType = <Namespace>.__c__DisplayClass0 }}

            var nRow = Activator.CreateInstance(xRowType);

            Console.WriteLine(new { nRow });

            // can we send the type across the threads?

            //var yy = new Func<string>(nRow, y.Method);
            // MulticastDelegate
            // protected MulticastDelegate(object target, string method); ???
            //var yy = default(MulticastDelegate);

            // script: error JSC1000: No implementation found for this native method, please implement [static System.Delegate.CreateDelegate(System.Type, System.Object, System.Reflection.MethodInfo)]
            //var y_withChangedTarget = (Func<string>)Delegate.CreateDelegate(typeof(Func<string>), nRow, y.Method);

            // script: error JSC1000: No implementation found for this native method, please implement [System.Type.GetConstructors(System.Reflection.BindingFlags)]
            //var ctors = typeof(Func<string>).GetConstructors(System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Public);

            // [0] = {Void .ctor(System.Object, IntPtr)}
            //var yy = Activator.CreateInstance(typeof(Func<string>),
            //    nRow,
            //    y.Method
            //);

            var yy = (Func<string>)Delegate.CreateDelegate(typeof(Func<string>), nRow, y.Method);


            Console.WriteLine(new { yy });

            //var yy = new Func<string>(nRow, y.Method);


            var z = yy();
            // wont work yet

            // Uncaught TypeError: undefined is not a function 
            //var z = y_withChangedTarget();
            // we cannot call it can we? why event make a delegate?
            //var z = y_withChangedTarget;

            Console.WriteLine(new { z });
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
