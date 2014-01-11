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
using System.Linq.Expressions;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using System.Xml.Linq;
using TestIQueryable;

namespace JVMCLRIQueryable
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


            // see also>
            // X:\jsc.svn\examples\javascript\android\AndroidBroadcastLogger\AndroidBroadcastLogger\ApplicationWebService.cs
            // X:\jsc.svn\examples\javascript\Test\TestIQueryable\TestIQueryable\ApplicationWebService.cs

            System.Console.WriteLine(
               typeof(object).AssemblyQualifiedName
            );

            InternalTry();

            CLRProgram.CLRMain();
        }

        private static void InternalTry()
        {
            try
            {
                Expression<Func<Book1Sheet1Row, bool>> where =
                    //x => x.Foo == "xxx";
                    x => string.Equals(x.Foo, "xxx");


                var w = new ApplicationWebService();

                w.WebMethod2();

                // X:\jsc.svn\core\ScriptCoreLib\JavaScript\DOM\CSS\CSSStyleSheet.cs

                //X:\jsc.svn\examples\java\JVMCLRIQueryable\JVMCLRIQueryable\bin\Release>JVMCLRIQueryable.exe
                //java.lang.Object, rt

                //error: { Target = class ScriptCoreLibJava.BCLImplementation.System.__String, MethodName = op_Equality }

                //error: { Parameter = class ScriptCoreLibJava.BCLImplementation.System.__String }
                //error: { Parameter = class ScriptCoreLibJava.BCLImplementation.System.__String }
                //error: { Method = java.lang.String Concat([Ljava.lang.Object;) }
                //error: { Method = java.lang.String Concat(java.lang.Object, java.lang.Object) }
                //error: { Method = java.lang.String Concat(java.lang.String, java.lang.String) }
                //error: { Method = java.lang.String Concat(java.lang.Object) }
                //error: { Method = java.lang.String Concat([Ljava.lang.String;) }
                //error: { Method = java.lang.String Concat(java.lang.Object, java.lang.Object, java.lang.Object) }
                //error: { Method = java.lang.String Concat(java.lang.String, java.lang.String, java.lang.String, java.lang.String) }
                //error: { Method = java.lang.String Concat(java.lang.String, java.lang.String, java.lang.String) }
                //error: { Method = java.lang.String Replace(java.lang.String, java.lang.String, java.lang.String) }
                //error: { Method = boolean op_Inequality(java.lang.String, java.lang.String) }
                //error: { Method = java.lang.String InternalConstructor(char, int) }
                //error: { Method = java.lang.String InternalConstructor([C) }
                //error: { Method = [Ljava.lang.String; SplitStringByChar(java.lang.String, char) }
                //error: { Method = boolean Equals_060002fb(java.lang.String, java.lang.String) }
                //error: { Method = [Ljava.lang.String; Split(java.lang.String, [Ljava.lang.String;, int) }
                //error: { Method = [Ljava.lang.String; Split(java.lang.String, [C) }
                //error: { Method = java.lang.String Substring(java.lang.String, int, int) }
                //error: { Method = boolean IsNullOrEmpty(java.lang.String) }
                //error: { Method = java.lang.String PadLeft(java.lang.String, int) }
                //error: { Method = java.lang.String PadLeft(java.lang.String, int, char) }
                //error: { Method = boolean Contains(java.lang.String, java.lang.String) }
                //error: { Method = [C ToCharArray(java.lang.String) }


            }
            catch (Exception ex)
            {
                Console.WriteLine(
                    new { ex.Message, ex.StackTrace }
                    );
            }
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
