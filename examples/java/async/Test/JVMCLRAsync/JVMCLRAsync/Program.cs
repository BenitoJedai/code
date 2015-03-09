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
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;

namespace JVMCLRAsync
{
    public class Class1
    {
        public Class1()
        {
        }
    }

    public class Class1<T>
    {
    }


    static class Program
    {
        // this still works! :D
        // https://sites.google.com/a/jsc-solutions.net/backlog/knowledge-base/2014/201405/20140524

        //java.lang.Object, rt
        //JVMCLRAsync.Program+<<Main>b__0>d__7 <0000> ldc.i4.1
        //JVMCLRAsync.Program+<<Main>b__0>d__7 <0011> br, to be optimized away
        //JVMCLRAsync.Program+<<Main>b__0>d__7 <001b> br, to be optimized away
        //JVMCLRAsync.Program+<<Main>b__0>d__7 <0020> nop
        //hi from goo
        //JVMCLRAsync.Program+<<Main>b__0>d__7 <002c> leave, to be optimized away
        //JVMCLRAsync.Program+<<Main>b__0>d__7 <0056> nop
        //JVMCLRAsync.Program+<<Main>b__0>d__7 <006b> nop
        //JVMCLRAsync.Program+<<Main>b__1>d__9 <0000> ldc.i4.1
        //JVMCLRAsync.Program+<<Main>b__1>d__9 <0011> br, to be optimized away
        //JVMCLRAsync.Program+<<Main>b__1>d__9 <001b> br, to be optimized away
        //JVMCLRAsync.Program+<<Main>b__1>d__9 <0020> nop
        //JVMCLRAsync.Program+<<Main>b__1>d__9 <0056> nop
        //JVMCLRAsync.Program+<<Main>b__1>d__9 <006c> nop
        //{ Result = hi from foo }
        //JVMCLRAsync.Program+<>c__DisplayClass5+<<Main>b__2>d__b <0000> ldc.i4.1
        //JVMCLRAsync.Program+<>c__DisplayClass5+<<Main>b__2>d__b <0011> ldloc.3
        //JVMCLRAsync.Program+<>c__DisplayClass5+<<Main>b__2>d__b <0018> br, to be optimized away
        //JVMCLRAsync.Program+<>c__DisplayClass5+<<Main>b__2>d__b <0027> br, to be optimized away
        //JVMCLRAsync.Program+<>c__DisplayClass5+<<Main>b__2>d__b <002c> nop
        //enter foo2
        //JVMCLRAsync.Program+<<Main>b__1>d__9 <0000> ldc.i4.1
        //JVMCLRAsync.Program+<<Main>b__1>d__9 <0011> br, to be optimized away
        //JVMCLRAsync.Program+<<Main>b__1>d__9 <001b> br, to be optimized away
        //JVMCLRAsync.Program+<<Main>b__1>d__9 <0020> nop
        //JVMCLRAsync.Program+<<Main>b__1>d__9 <0056> nop
        //JVMCLRAsync.Program+<<Main>b__1>d__9 <006c> nop
        //JVMCLRAsync.Program+<>c__DisplayClass5+<<Main>b__2>d__b <009f> ldloca.s
        //exit foo2
        //JVMCLRAsync.Program+<>c__DisplayClass5+<<Main>b__2>d__b <00f9> nop
        //JVMCLRAsync.Program+<>c__DisplayClass5+<<Main>b__2>d__b <010f> nop
        //{ Result = hi from foo }
        //System.Object, mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089

        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        public static void Main(string[] args)
        {
            // 2012desktop?
            // scriptcorelib to be rebuilt with 2012

            //internal compiler error at method
            // assembly: C:\util\jsc\bin\ScriptCoreLib.dll at
            // type: ScriptCoreLib.Shared.BCLImplementation.Syste
            // method: CreatePaddedBuffer
            // Java : Opcode not implemented: stind.i1 at ScriptC

            // we are on 4.6 compiler?
            // X:\jsc.svn\core\ScriptCoreLib.Ultra\ScriptCoreLib.Ultra\Ultra\Library\StringConversions.cs

            //- javac
            //"C:\Program Files (x86)\Java\jdk1.7.0_45\bin\javac.exe" -classpath "Y:\staging\web\java";release -d release java\JVMCLRAsync\Program.java
            //Y:\staging\web\java\JVMCLRAsync__i__d\Internal\Library\StringConversions.java:115: error: bad operand types for binary operator '>'
            //        if (((e > null)))
            //                ^
            //  first type:  String
            //  second type: <null>
            //Y:\staging\web\java\JVMCLRAsync__i__d\Internal\Library\StringConversions.java:207: error: incompatible types
            //        for (num5 = 0; num5; num5++)
            //                       ^
            //  required: boolean
            //  found:    int
            //Note: Y:\staging\web\java\ScriptCoreLibJava\BCLImplementation\System\Threading\__Thread.java uses or overrides a deprecated API.



            // X:\jsc.svn\core\ScriptCoreLib\Shared\BCLImplementation\System\Runtime\CompilerServices\AsyncVoidMethodBuilder.cs
            // jsc needs to see args to make Main into main for javac..


            // see also>
            // X:\jsc.svn\examples\javascript\android\AndroidBroadcastLogger\AndroidBroadcastLogger\ApplicationWebService.cs

            System.Console.WriteLine(
               typeof(object).AssemblyQualifiedName
            );

            //- javac
            //"C:\Program Files (x86)\Java\jdk1.7.0_45\bin\javac.exe" -classpath "Y:\staging\web\java";release -d release java\JVMCLRAsync\Program.java
            //java\JVMCLRAsync\Program.java:33: error: generic array creation
            //        class1_1Array0 = new JVMCLRAsync.Class1_1<Class1>[1];
            //                         ^

            var a = new Class1<Class1>[1];
            //var a = (Class1<Class1>[])Array.CreateInstance(typeof(Class1<Class1>), 1);



            // https://sites.google.com/a/jsc-solutions.net/backlog/knowledge-base/2014/201402/20140216
            // X:\jsc.internal.svn\compiler\jsc.meta\jsc.meta\Commands\Rewrite\RewriteToAssembly\RewriteToAssembly.WriteSwitchRewrite.cs
            // X:\jsc.svn\examples\java\Test\TestByRefAwaitUnsafeOnCompleted\TestByRefAwaitUnsafeOnCompleted\Class1.cs

            // jsc java does not understand our async/switch rewriter?



            // X:\jsc.svn\examples\java\Test\TestNestedTypeImport\TestNestedTypeImport\Class1.cs
            Action goo = async delegate
            {



                Console.WriteLine("hi from goo");
            };
            goo();



            Func<Task<string>> foo = async delegate
            {
                return "hi from foo";
            };

            Console.WriteLine(
                new { foo().Result }
                );

            Func<Task<string>> foo2 = async delegate
            {
                Console.WriteLine("enter foo2");
                var x = await foo();

                Console.WriteLine("exit foo2");
                return x;

            };



            Console.WriteLine(
                new { foo2().Result }
                );

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
