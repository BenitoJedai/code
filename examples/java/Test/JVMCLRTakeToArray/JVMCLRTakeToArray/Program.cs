using java.util.zip;
using ScriptCoreLib;
using ScriptCoreLib.Delegates;
using ScriptCoreLib.Extensions;
using ScriptCoreLibJava.Extensions;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using System.Xml.Linq;

namespace JVMCLRTakeToArray
{
    public class a { public int i; };

    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        public static void Main(string[] args)
        {
            try
            {
                //var data = Enumerable.Range(0, 66).Select(i => new { i });
                var data = Enumerable.Range(0, 66).Select(i => new a { i = i });

                Console.WriteLine("before Where");
                var w = data.Where(x => x.i % 2 == 0);

                Console.WriteLine("before Take");
                var t = w.Take(5);
                Console.WriteLine("before ToList");
                var l = t.ToList();
                Console.WriteLine("before ToArray");
                var a = l.ToArray();

                //{ Message = [Ljava.lang.Object; cannot be cast to [L__AnonymousTypes__JVMCLRTakeToArray__i__d_jvm.__f__AnonymousType_10__27__29_0_1;, StackTrace = java.lang.ClassCastException: [Ljava.lang.Object; cannot be cast to [L__AnonymousTypes__JVMCLRTakeToArray__i__d_jvm.__f__AnonymousType_10__27__29_0_1;
                //        at JVMCLRTakeToArray.Program.main(Program.java:85)
                // }
                //System.Object, mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089


                Console.WriteLine("a " + new { a.Length });
            }
            catch (Exception wtf)
            {
                Console.WriteLine(new { wtf.Message, wtf.StackTrace });
            }


            CLRProgram.CLRMain();
        }


    }


    public delegate XElement XElementFunc();

    [SwitchToCLRContext]
    static class CLRProgram
    {
        public static XElement XML { get; set; }

        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        public static void CLRMain()
        {
            System.Console.WriteLine(
                typeof(object).AssemblyQualifiedName
            );

            MessageBox.Show("click to close");

        }
    }


}
