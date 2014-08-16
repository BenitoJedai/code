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

namespace TestJVMCLRTupleArrayLast
{
    static class goo

    {
        // X:\jsc.svn\examples\java\Test\TestGenericArrayInit\TestGenericArrayInit\Class1.cs

        // https://sites.google.com/a/jsc-solutions.net/backlog/knowledge-base/2014/201408/20140815
        //tuple_2Array0 = (__Tuple_2<__MemberInfo, Integer>[])__Array.CreateInstance(__Type.GetTypeFromHandle(__RuntimeTypeHandle.op_Explicit(__Tuple_2.class)), 1);
        //tuple_2Array0[0] = __Tuple.<__MemberInfo, Integer>Create(null, 0);
        //goo.Target = tuple_2Array0;


        // ???
        public static Tuple<MemberInfo, int>[] Target = new[] { Tuple.Create(default(MemberInfo), 0) };
        // {{ Target =  }}

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


            // see also>
            // X:\jsc.svn\examples\javascript\android\AndroidBroadcastLogger\AndroidBroadcastLogger\ApplicationWebService.cs

            System.Console.WriteLine(
               typeof(object).AssemblyQualifiedName
            );

            Console.WriteLine(new { Target = goo.Target[0] });
            // {{ Target = ScriptCoreLib.Shared.BCLImplementation.System.__Tuple_2@65a77f }}


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

            // X:\jsc.svn\examples\javascript\forms\FormsNIC\FormsNIC\ApplicationWebService.cs



            MessageBox.Show("click to close");

        }
    }


}
