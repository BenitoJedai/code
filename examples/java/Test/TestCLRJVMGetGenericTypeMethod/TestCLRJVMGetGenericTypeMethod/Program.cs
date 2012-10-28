using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using ScriptCoreLib;
using ScriptCoreLib.Delegates;
using ScriptCoreLib.Extensions;
using ScriptCoreLibJava.Extensions;
using System.Xml.Linq;
using java.io;
using java.net;
using java.util.zip;
using System.Collections;
using System.IO;

namespace TestCLRJVMGetGenericTypeMethod
{
    interface abar<T>
    {
    }


    interface bar
    {
    }

    interface cbar<T>
    {
    }

    class foo
    {

    }

    class __Extensions___c__DisplayClass2<T, Tfoo, Tbar, Tfoobar, Tabar>
        where Tfoo : foo
        where Tbar : bar
        where Tfoobar : foo, bar
        where Tabar : abar<foo>, bar, cbar<foo>
    {
        public IEnumerable<XElement> _Elements_b__1(T k, Tfoo tfoo, Tbar tbar, Tfoobar tfoobar, Tabar tabar)
        {
            return null;
        }
    }

    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        public static void Main(string[] args)
        {
            // jsc needs to see args to make Main into main for javac..

            // generic parameter needs to be moved..

            Console.WriteLine("hi!");


            var t = typeof(__Extensions___c__DisplayClass2<,,,,>);

            // { SourceMethod = ScriptCoreLib.Shared.BCLImplementation.System.Collections.Generic.__IEnumerable_1 _Elements_b__1(java.lang.Object) }
            // { SourceMethod = ScriptCoreLib.Shared.BCLImplementation.System.Collections.Generic.__IEnumerable_1 _Elements_b__1(
            // java.lang.Object, 
            // TestCLRJVMGetGenericTypeMethod.foo, 
            // TestCLRJVMGetGenericTypeMethod.bar, 
            // TestCLRJVMGetGenericTypeMethod.foo
            // ) }

            t.GetMethods().WithEach(
                SourceMethod =>
                {
                    Console.WriteLine(new { SourceMethod });
                }
            );

            var m = t.GetMethod("_Elements_b__1", new[] { 
                typeof(object), 
                typeof(foo), 
                typeof(bar), 
                typeof(foo), 
                typeof(cbar<>), 
            });


            System.Console.WriteLine(new { m });

            Func<object, foo, bar, cbar<foo>> 

            System.Console.WriteLine("done");





            System.Console.WriteLine("jvm");


            CLRProgram.XML = new XElement("hello", "world");
            CLRProgram.CLRMain(
            );

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
        public static void CLRMain(
             StringAction ListMethods = null
            )
        {
            System.Console.WriteLine(XML);

            MessageBox.Show("it works?!?");
        }
    }

}
