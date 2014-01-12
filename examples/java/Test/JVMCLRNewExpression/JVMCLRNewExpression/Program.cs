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

namespace JVMCLRNewExpression
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

            System.Console.WriteLine(
               typeof(object).AssemblyQualifiedName
            );
            Test1();



            CLRProgram.CLRMain();
        }

        private static void Test1()
        {
            //- javac
            //"C:\Program Files (x86)\Java\jdk1.7.0_45\bin\javac.exe" -classpath "Y:\staging\web\java";release -d release java\JVMCLRNewExpression\Program.java
            //java\JVMCLRNewExpression\Program.java:53: error: cannot find symbol
            //        expression_10 = __Expression.<__Func_1<Object>>Lambda(__Expression.New(info3, __SZArrayEnumerator_1.<__Expression>Of(expressionArray4), infoArray5), new __ParameterExpression[] {});
            //                                                                                                                                                                 ^
            //  symbol:   class __ParameterExpression
            //  location: class Program

            ParameterExpression ref0;

            // X:\jsc.svn\core\ScriptCoreLibJava\BCLImplementation\System\Type.cs
            // https://sites.google.com/a/jsc-solutions.net/backlog/knowledge-base/2014/201401/20140111-iquery/new
            Expression<Func<object>> z = () => new { n = "1" };
        }


    }



    [SwitchToCLRContext]
    static class CLRProgram
    {

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
