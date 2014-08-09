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

namespace TestJVMCLRTypeOfTupleArray
{

    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        public static void Main(string[] args)
        {
            // jsc needs to see args to make Main into main for javac..
            // X:\jsc.svn\examples\java\hybrid\Test\TestJVMCLRNewTupleArray\TestJVMCLRNewTupleArray\Program.cs

            //- javac
            //"C:\Program Files (x86)\Java\jdk1.7.0_45\bin\javac.exe" -classpath "Y:\staging\web\java";release -d release java\TestJVMCLRTypeOfTupleArray\Program.java
            //java\TestJVMCLRTypeOfTupleArray\Program.java:25: error: illegal start of expression
            //        type0 = __Type.GetTypeFromHandle(__RuntimeTypeHandle.op_Explicit(__Tuple_2<__MemberInfo, Integer>[].class));
            //        tuple_2Array0 = (__Tuple_2<__MemberInfo, Integer>[])__Array.CreateInstance(__Type.GetTypeFromHandle(__RuntimeTypeHandle.op_Explicit(__Tuple_2.class)), 0);
            //                                                                                            ^

            var z = typeof(Tuple<MemberInfo, int>[]);

            Console.WriteLine(z.FullName);


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
