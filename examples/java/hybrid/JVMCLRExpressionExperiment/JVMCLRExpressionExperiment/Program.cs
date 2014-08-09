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

namespace JVMCLRExpressionExperiment
{
    class xRow
    {
        public string Title;
    }
    static class Program
    {
        // X:\jsc.svn\examples\javascript\Test\TestCSSAttrExpression\TestCSSAttrExpression\Application.cs

        static object by(Expression<Func<xRow, bool>> f)
        {
            Console.WriteLine(new { f });

            return null;
        }

        static void test()
        {
            var findme1_text = "findme1_text";
            var findme1_number = 1;
            var findme1 = findme1_text + findme1_number;


            //            java.lang.Object, rt
            //Parameter { type = JVMCLRExpressionExperiment.xRow, name = a }
            //Field { expression = ParameterExpression { type = JVMCLRExpressionExperiment.xRow, name = a }, field = java.lang.String Title }
            //Constant { value = JVMCLRExpressionExperiment.Program___c__DisplayClass0@1afe17b }
            //Field { expression = Constant { value = JVMCLRExpressionExperiment.Program___c__DisplayClass0@1afe17b, type =  }, field = java.lang.String findme1 }
            //Call { instance = , method = boolean Equals_060002fb(java.lang.String, java.lang.String), arguments = [LScriptCoreLib.Shared.BCLImplementation.System.Linq.Expressions.__Expression;@134eca }
            //Call { instance = , method = boolean Equals_060002fb(java.lang.String, java.lang.String), arguments = [LScriptCoreLib.Shared.BCLImplementation.System.Linq.Expressions.__Expression;@134eca }
            //Lambda { body = MethodCallExpression { Object = , Method = boolean Equals_060002fb(java.lang.String, java.lang.String), arguments = [LScriptCoreLib.Shared.BCLImplementation.System.Linq.Expressions.__Expression;@134eca }, parameters = [LScriptCoreLib.Shared.BCLImplementation.System.Linq.Expressions.__ParameterExpression;@1189cbb }
            //{ f = { Body = MethodCallExpression { Object = , Method = boolean Equals_060002fb(java.lang.String, java.lang.String), arguments = [LScriptCoreLib.Shared.BCLImplementation.System.Linq.Expressions.__Expression;@134eca }, parameters = [LScriptCoreLib.Shared.BCLImplementation.System.Linq.Expressions.__ParameterExpression;@1189cbb } }
            //--
            //System.Object, mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089




            // Caused by: java.lang.NoSuchMethodException: ScriptCoreLibJava.BCLImplementation.System.__String.op_Equality(ScriptCoreLibJava.BCLImplementation.System.__String, ScriptCoreLibJava.BCLImplementation.System.__String)


            //            Implementation not found for type import :
            //type: System.Linq.Expressions.Expression
            //method: System.Linq.Expressions.MethodCallExpression Call(System.Linq.Expressions.Expression, System.Reflection.MethodInfo, System.Linq.Expressions.Expression[])
            //Did you forget to add the [Script] attribute?
            //Please double check the signature!


            //var x = by(a => a.Title == findme1);
            var x = by(a => string.Equals(a.Title, findme1)

                );

        }

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


            try
            {
                test();
            }
            catch (Exception x)
            {
                Console.WriteLine(new { x.Message, x.StackTrace });
            }


            Console.WriteLine("--");

            CLRProgram.CLRMain();
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
