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

// roslyn
//using System.Console;

// Error	3	The type name 'Join' does not exist in the type 'string'	X:\jsc.svn\examples\java\test\JVMCLRStringJoin\JVMCLRStringJoin\Program.cs	15	21	JVMCLRStringJoin
// Error	3	A using directive can only be applied to static classes or namespaces; 'string' is a non-static class	X:\jsc.svn\examples\java\test\JVMCLRStringJoin\JVMCLRStringJoin\Program.cs	17	7	JVMCLRStringJoin
//using System.String;

namespace JVMCLRStringJoin
{
    // https://sites.google.com/a/jsc-solutions.net/backlog/knowledge-base/2014/201405/20140514


    class __Enumerable___c__DisplayClassb_4__
    {
        public static Func<object> CS___9__CachedAnonymousMethodDelegated;
    }

    class __Enumerable___c__DisplayClassb_4<TOuter, TInner, TKey, TResult> : __Enumerable___c__DisplayClassb_4__
    {

        public void _GroupJoin_b__5()
        {

            //Y:\staging\web\java\JVMCLRStringJoin\__Enumerable___c__DisplayClassb_4_4.java:24: error: ')' expected
            //if (!(__Enumerable___c__DisplayClassb_4_4<TOuter, TInner, TKey, TResult>.CS___9__CachedAnonymousMethodDelegated > null))
            //                                                                                                               ^

            //if (CS___9__CachedAnonymousMethodDelegated == null)
            //{
            //    WriteLine("!");
            //}
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


            //            -javac
            //"C:\Program Files (x86)\Java\jdk1.7.0_45\bin\javac.exe" - classpath "Y:\staging\web\java"; release - d release java\JVMCLRStringJoin\Program.java
            //   java\JVMCLRStringJoin\Program.java:38: error: bad operand types for binary operator '>'
            //           if (!(__Enumerable___c__DisplayClassb_4__.CS___9__CachedAnonymousMethodDelegated > null))
            //                                                                                         ^
            //  first type:  __Func_1<Object>
            //  second type: < null >

            if (__Enumerable___c__DisplayClassb_4<object, object, object, object>.CS___9__CachedAnonymousMethodDelegated == null)
            {
                Console.WriteLine("!");
            }





            //            java.lang.Object, rt
            //hello + world
            //System.Object, mscorlib, Version = 4.0.0.0, Culture = neutral, PublicKeyToken = b77a5c561934e089


            // jsc needs to see args to make Main into main for javac..


            // see also>
            // X:\jsc.svn\examples\javascript\android\AndroidBroadcastLogger\AndroidBroadcastLogger\ApplicationWebService.cs

            System.Console.WriteLine(
               typeof(object).AssemblyQualifiedName
            );


            //            0001 0200000d JVMCLRStringJoin__i__d.jvm::< module >.SHA14c879601b74adb3ae907d6f3a4ce80b8899dc5d5@138105774$00000018$0000004b
            //              - javac
            //"C:\Program Files (x86)\Java\jdk1.7.0_45\bin\javac.exe" - classpath "Y:\staging\web\java"; release - d release java\JVMCLRStringJoin\Program.java
            //   Y:\staging\web\java\ScriptCoreLib\Shared\BCLImplementation\System\Linq\__Enumerable___c__DisplayClassb_4.java:44: error: ')' expected
            //        if ((__Enumerable___c__DisplayClassb_4<TOuter, TInner, TKey, TResult>.CS___9__CachedAnonymousMethodDelegated == null))
            //                                                                                                                    ^

            var u = new[] { "hello", "world" };

            //http://www.java2s.com/Code/Java/Reflection/Getfieldtypeandgenerictypebyfieldname.htm
            new __Enumerable___c__DisplayClassb_4<object, object, object, object>()._GroupJoin_b__5();

            //            Implementation not found for type import :
            //type: System.String
            //            method: System.String Join(System.String, System.String[])
            //                Did you forget to add the[Script] attribute?
            //               Please double check the signature!


            var j = string.Join("+", u);


            Console.WriteLine(j);

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
