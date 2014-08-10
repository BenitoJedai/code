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

namespace TestJVMCLRGenericField
{
    class Foo<T>
    {
        public static string Text;

        //    Y:\staging\web\java\JVMCLRSyntaxOrderByThenGroupBy__i__d\Internal\Query\Experimental\QueryExpressionBuilder_SQLWriter_1.java:68: error: non-static type variable TElement cannot be referenced from a static context
        //public final static __Func_2<JVMCLRSyntaxOrderByThenGroupBy__i__d.Internal.Query.Experimental.IQueryStrategy_1<TElement>, Long> CountReference = new __Func_2<JVMCLRSyntaxOrderByThenGroupBy__i__d.Internal.Query.Experimental.IQueryStrategy_1<TElement>, Long>(null,
        //                                                                                                               ^

        //public static Func<Foo<T>, Foo<T>> Identity = e => e;
        public Func<Foo<T>, Foo<T>> Identity = e => e;

        //- javac
        //"C:\Program Files (x86)\Java\jdk1.7.0_45\bin\javac.exe" -classpath "Y:\staging\web\java";release -d release java\TestJVMCLRGenericField\Program.java
        //Y:\staging\web\java\TestJVMCLRGenericField\Foo_1.java:14: error: non-static type variable T cannot be referenced from a static context
        //    public static __Func_2<Foo_1<T>, Foo_1<T>> Identity;
        //                                 ^
        //Y:\staging\web\java\TestJVMCLRGenericField\Foo_1.java:14: error: non-static type variable T cannot be referenced from a static context
        //    public static __Func_2<Foo_1<T>, Foo_1<T>> Identity;
        //                                           ^
        //Y:\staging\web\java\TestJVMCLRGenericField\Foo_1.java:19: error: non-static type variable T cannot be referenced from a static context
        //        Foo_1.Identity = new __Func_2<Foo_1<T>, Foo_1<T>>(null,
        //                                            ^
        //Y:\staging\web\java\TestJVMCLRGenericField\Foo_1.java:19: error: non-static type variable T cannot be referenced from a static context
        //        Foo_1.Identity = new __Func_2<Foo_1<T>, Foo_1<T>>(null,
        //                                                      ^
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

            //            java\TestJVMCLRGenericField\Program.java:22: error: not a statement
            //        TestJVMCLRGenericField.Foo_1<Object>.Text = "1";
            //                                            ^
            //java\TestJVMCLRGenericField\Program.java:22: error: ';' expected
            //        TestJVMCLRGenericField.Foo_1<Object>.Text = "1";
            //                                                 ^
            //java\TestJVMCLRGenericField\Program.java:23: error: not a statement
            //        TestJVMCLRGenericField.Foo_1<String>.Text = "2";
            //                                            ^
            //java\TestJVMCLRGenericField\Program.java:23: error: ';' expected
            //        TestJVMCLRGenericField.Foo_1<String>.Text = "2";
            //                                                 ^

            // while CLR may have alternate generic fields, java wont

            Foo<object>.Text = "1";
            Foo<string>.Text = "2";

            // Foo<object>.Text = "1"
            // Foo<string>.Text = "2"

            Console.WriteLine(
                new { Foo<object>.Text }
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
