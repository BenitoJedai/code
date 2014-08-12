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
        public static Tuple<MemberInfo, int>[] Target = new Tuple<MemberInfo, int>[] { Tuple.Create(default(MemberInfo), 0) };

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

            //- javac
            //"C:\Program Files (x86)\Java\jdk1.7.0_45\bin\javac.exe" -classpath "W:\staging\web\java";release -d release java\TestJVMCLRTupleArrayLast\Program.java
            //java\TestJVMCLRTupleArrayLast\Program.java:27: error: cannot find symbol
            //        if ((__Enumerable.<__Tuple_2<__MemberInfo, Integer>>Last(__SZArrayEnumerator_1.<__Tuple_2<__MemberInfo, Integer>>Of(goo.Target)).get_Item2() > 0))
            //                                                                                        ^
            //  symbol:   class __Tuple_2
            //  location: class Program
            //java\TestJVMCLRTupleArrayLast\Program.java:27: error: cannot find symbol
            //        if ((__Enumerable.<__Tuple_2<__MemberInfo, Integer>>Last(__SZArrayEnumerator_1.<__Tuple_2<__MemberInfo, Integer>>Of(goo.Target)).get_Item2() > 0))
            //                                                                                                  ^
            //  symbol:   class __MemberInfo
            //  location: class Program
            //java\TestJVMCLRTupleArrayLast\Program.java:27: error: cannot find symbol
            //        if ((__Enumerable.<__Tuple_2<__MemberInfo, Integer>>Last(__SZArrayEnumerator_1.<__Tuple_2<__MemberInfo, Integer>>Of(goo.Target)).get_Item2() > 0))
            //                           ^
            //  symbol:   class __Tuple_2
            //  location: class Program
            //java\TestJVMCLRTupleArrayLast\Program.java:27: error: cannot find symbol
            //        if ((__Enumerable.<__Tuple_2<__MemberInfo, Integer>>Last(__SZArrayEnumerator_1.<__Tuple_2<__MemberInfo, Integer>>Of(goo.Target)).get_Item2() > 0))
            //                                     ^

            var a = new Tuple<MemberInfo, int>[0];

            //if (goo.Target.Last().Item2 > 0)
            //{
            //    Console.WriteLine("hi");
            //}

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
