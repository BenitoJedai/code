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

namespace JVMCLRLinqAverage
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

            System.Console.WriteLine("hi");

            try
            {

                // see also>
                // X:\jsc.svn\examples\javascript\android\AndroidBroadcastLogger\AndroidBroadcastLogger\ApplicationWebService.cs

                System.Console.WriteLine(
                   typeof(object).AssemblyQualifiedName
                );

                //- javac
                //"C:\Program Files (x86)\Java\jdk1.7.0_45\bin\javac.exe" -classpath "Y:\staging\web\java";release -d release java\JVMCLRLinqAverage\Program.java
                //java\JVMCLRLinqAverage\Program.java:35: error: generic array creation
                //        type_9__27__28_0_1Array2 = new __AnonymousTypes__JVMCLRLinqAverage__i__d_jvm.__f__AnonymousType_9__27__28_0_1<Integer>[] {
                //                                   ^
                //java\JVMCLRLinqAverage\Program.java:56: error: method Average_0600046d in class __Enumerable cannot be applied to given types;
                //        double1 = __Enumerable.<__AnonymousTypes__JVMCLRLinqAverage__i__d_jvm.__f__AnonymousType_9__27__28_0_1<Integer>>Average_0600046d(__SZArrayEnumerator_1.<__AnonymousTypes__JVMCLRLinqAverage__i__d_jvm.__f__AnonymousType_9__27__28_0_1<Integer>>Of(type_9__27__28_0_1Array3), func_24);
                //                              ^
                //  required: __IEnumerable_1<TSource>,__Func_2<TSource,Double>
                //  found: __SZArrayEnumerator_1<__f__AnonymousType_9__27__28_0_1<Integer>>,__Func_2<__f__AnonymousType_9__27__28_0_1<Integer>,Integer>
                //  reason: actual argument __Func_2<__f__AnonymousType_9__27__28_0_1<Integer>,Integer> cannot be converted to __Func_2<__f__AnonymousType_9__27__28_0_1<Integer>,Double> by method invocation conversion
                //  where TSource is a type-variable:
                //    TSource extends Object declared in method <TSource>Average_0600046d(__IEnumerable_1<TSource>,__Func_2<TSource,Double>)
                //Y:\staging\web\java\ScriptCoreLib\Shared\BCLImplementation\System\Linq\__Enumerable.java:1720: error: inconvertible types
                //                num2 = ((int)(enumerator_15.System_Collections_Generic_IEnumerator_1_get_Current()));
                //                             ^
                //  required: int
                //  found:    Double
                //Y:\staging\web\java\ScriptCoreLib\Shared\BCLImplementation\System\Linq\__Enumerable.java:1769: error: inconvertible types
                //                num2 = ((int)(enumerator_15.System_Collections_Generic_IEnumerator_1_get_Current()));
                //            //                             ^

                //- javac
                //"C:\Program Files (x86)\Java\jdk1.7.0_45\bin\javac.exe" -classpath "Y:\staging\web\java";release -d release java\JVMCLRLinqAverage\Program.java
                //java\JVMCLRLinqAverage\Program.java:37: error: generic array creation
                //        type_9__27__28_0_1Array3 = new __AnonymousTypes__JVMCLRLinqAverage__i__d_jvm.__f__AnonymousType_9__27__28_0_1<Integer>[] {
                //                                   ^
                //java\JVMCLRLinqAverage\Program.java:58: error: method Average_0600046d in class __Enumerable cannot be applied to given types;
                //        double2 = __Enumerable.<__AnonymousTypes__JVMCLRLinqAverage__i__d_jvm.__f__AnonymousType_9__27__28_0_1<Integer>>Average_0600046d(__SZArrayEnumerator_1.<__AnonymousTypes__JVMCLRLinqAverage__i__d_jvm.__f__AnonymousType_9__27__28_0_1<Integer>>Of(type_9__27__28_0_1Array4), func_25);
                //                              ^
                //  required: __IEnumerable_1<TSource>,__Func_2<TSource,Double>
                //  found: __SZArrayEnumerator_1<__f__AnonymousType_9__27__28_0_1<Integer>>,__Func_2<__f__AnonymousType_9__27__28_0_1<Integer>,Integer>
                //  reason: actual argument __Func_2<__f__AnonymousType_9__27__28_0_1<Integer>,Integer> cannot be converted to __Func_2<__f__AnonymousType_9__27__28_0_1<Integer>,Double> by method invocation conversion
                //  where TSource is a type-variable:
                //    TSource extends Object declared in method <TSource>Average_0600046d(__IEnumerable_1<TSource>,__Func_2<TSource,Double>)
                //Note: Some input files use unchecked or unsafe operations.


                //var z = new { Int32 = 0 };
                var z = new __item { Int32 = 0 };
                //var x = new[] { z };
                var x = new[] { z };
                var a = x.Average(xx => xx.Int32);

                Console.WriteLine(new { a });
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());

            }

            //String.Concat
            CLRProgram.CLRMain();
        }


    }

    class __item
    {
        public int Int32;
    }


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
