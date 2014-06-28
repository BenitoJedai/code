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
using ScriptCoreLib.JavaScript.Runtime;

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
            var scopeField2 = 7;
            var scopeField3 = new { foo = "bar" };

            Console.WriteLine("before delegate " + new { scopeField1, scopeField2, scopeField3 });

            Inspection(
                delegate
            {
                Console.WriteLine("inside the delegate  " + new { scopeField1, scopeField2, scopeField3 });

                return scopeField1;
            }
            );
        }

        static void Inspection(Func<string> y)
        {
            //Serializer
            // enter Inspection {{ y = [object Object], Method = { MethodToken = owAABhluGjmdbjAyX2PIQA }, Target = [object Object] }}
            Console.WriteLine("enter Inspection " + new { y, y.Method, y.Target });

            // can we recreate the target on the other side of the thread?
            // do we have enough information on what the object is?

            var xRowType = y.Target.GetType();

            Console.WriteLine(new { xRowType });
            // {{ xRowType = <Namespace>.__c__DisplayClass0 }}

            var nRow = Activator.CreateInstance(xRowType);

            //Activator.GetObject(


            //{ { MemberName = scopeField1 } }
            //{ { MemberName = sQAABnvguzW_b3wQRWrbnyg } }
            //{ { MemberName = sAAABnvguzW_b3wQRWrbnyg } }
            //{ { Name = scopeField1 } }

            //var tt = Expando.Of(y.Target);

            //foreach (var MemberName in tt.GetMemberNames())
            //{
            //    Console.WriteLine(new { MemberName });
            //}


            var xFields = xRowType.GetFields(System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.Public | System.Reflection.BindingFlags.NonPublic);

            foreach (var item in xFields)
            {
                // {{ Name = scopeField1, value = field1 }}

                var value = item.GetValue(y.Target);

                // {{ Name = scopeField1, value = field1, isString = false }}
                var isString = value is string;
                var isInt32 = value is int;

                //{ { Name = scopeField1, value = field1, isString = false, isInt32 = false } }
                //{ { Name = scopeField2, value = 7, isString = false, isInt32 = false } }
                //{ { Name = scopeField3, value = { { foo = bar } }, isString = false, isInt32 = false } }
                // is operator does not seem to be working for us!

                Console.WriteLine(new { item.Name, value, isString, isInt32 });

                // we are copying data! it musts be transferable
                item.SetValue(nRow, value);
            }


            Console.WriteLine(new { nRow });

            //xRowType.GetFields(

            // can we send the type across the threads?

            //var yy = new Func<string>(nRow, y.Method);
            // MulticastDelegate
            // protected MulticastDelegate(object target, string method); ???
            //var yy = default(MulticastDelegate);

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
