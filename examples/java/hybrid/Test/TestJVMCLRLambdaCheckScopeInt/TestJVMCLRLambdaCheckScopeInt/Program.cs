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

namespace TestJVMCLRLambdaCheckScopeInt
{

    static class Program
    {
        class SQLWriterWithoutLinefeeds : IDisposable
        {
            public Action yield;
            public void Dispose()
            {
                yield();
            }

        }

        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        public static void Main(string[] args)
        {
            // jsc needs to see args to make Main into main for javac..

            // X:\jsc.svn\examples\java\Test\TestLambdaCheckScopeInt\TestLambdaCheckScopeInt\Class1.cs


            Action<string> Write =
                text =>
                    {
                        //if (Command != null)
                        //    Command.CommandText += text;

                        Console.Write(text);
                    };



            //            -javac
            //"C:\Program Files (x86)\Java\jdk1.7.0_45\bin\javac.exe" - classpath "Y:\staging\web\java"; release - d release java\TestJVMCLRLambdaCheckScopeInt\Program.java
            //   Y:\staging\web\java\TestJVMCLRLambdaCheckScopeInt\Program___c__DisplayClass0.java:33: error: incomparable types: int and < null >
            //        if (this.WithoutLinefeedsCounter == null)
            //                                         ^
            //Y:\staging\web\java\TestJVMCLRLambdaCheckScopeInt\Program___c__DisplayClass0.java:61: error: incomparable types: int and < null >
            //        if (this.WithoutLinefeedsCounter == null)
            //                                         ^

            #region WithoutLinefeeds
            var WithoutLinefeedsCounter = 0;
            var WithoutLinefeedsDirty = false;
            Func<IDisposable> WithoutLinefeeds =
                delegate
            {
                if (WithoutLinefeedsCounter == 0)
                    WithoutLinefeedsDirty = false;

                WithoutLinefeedsCounter++;

                return new SQLWriterWithoutLinefeeds
                {
                    yield = delegate
                    {
                        WithoutLinefeedsCounter--;

                        if (WithoutLinefeedsCounter == 0)
                            Write("\r\n");
                    }
                };
            };
            #endregion


            using (WithoutLinefeeds())
            {
                //x:\jsc.svn\core\scriptcorelib.extensions\scriptcorelib.extensions\query\experimental\queryexpressionbuilder.cs
                Write("hello ");
            }
            Write("world ");

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
