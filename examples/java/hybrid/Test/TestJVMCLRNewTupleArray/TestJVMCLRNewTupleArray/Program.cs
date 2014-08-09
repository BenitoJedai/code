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
using System.Reflection;

namespace TestJVMCLRNewTupleArray
{

    static class Program
    {
        //     0001 0200000d TestJVMCLRNewTupleArray__i__d.jvm::<module>.SHA19fe34a04cc4c17b86ac2554d45fafe3411c1ae10@1418247224$00000018$0000004f
        //- javac
        //"C:\Program Files (x86)\Java\jdk1.7.0_45\bin\javac.exe" -classpath "Y:\staging\web\java";release -d release java\TestJVMCLRNewTupleArray\Program.java
        //java\TestJVMCLRNewTupleArray\Program.java:25: error: cannot find symbol
        //        __Tuple_2<__MemberInfo, Integer>[] tuple_2Array0;
        //                  ^
        //  symbol:   class __MemberInfo
        //  location: class Program
        //java\TestJVMCLRNewTupleArray\Program.java:27: error: cannot find symbol
        //        tuple_2Array0 = (__Tuple_2<__MemberInfo, Integer>[])__Array.CreateInstance(__Type.GetTypeFromHandle(__RuntimeTypeHandle.op_Explicit(__Tuple_2.class)), 0);
        //                                   ^

        // why cant jsc pick it up?
        static MemberInfo ref2;


        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        public static void Main(string[] args)
        {
            // https://sites.google.com/a/jsc-solutions.net/backlog/knowledge-base/2014/201408/20140809/linq-jvm
            // X:\jsc.svn\examples\java\Test\TestNewArrayGenericImport\TestNewArrayGenericImport\Class1.cs


            var z = new Tuple<MemberInfo, int>[] {
                // Tuple.Create(item.m, index)
            };

            Console.WriteLine(z.GetType().FullName);

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
