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
    static class Foo<T>
    {
        public static string Text;
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
