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

namespace JVMCLRConvertExpression
{
    enum xKey : long { }
    class xRow
    {
        public xKey Key;
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


            var u = new xKey();
            //var u = default(xKey);



            //Implementation not found for type import :
            //type: System.Linq.Expressions.Expression
            //method: System.Linq.Expressions.UnaryExpression Convert(System.Linq.Expressions.Expression, System.Type)
            //Did you forget to add the [Script] attribute?
            //Please double check the signature!

            // https://sites.google.com/a/jsc-solutions.net/backlog/knowledge-base/2014/201401/20140114


            //Implementation not found for type import :
            //type: System.Linq.Expressions.Expression
            //method: System.Linq.Expressions.BinaryExpression Equal(System.Linq.Expressions.Expression, System.Linq.Expressions.Expression)
            //Did you forget to add the [Script] attribute?
            //Please double check the signature!

            Expression<Func<xRow, bool>> f = z => z.Key == u;

            //java.lang.Object, rt
            //{ f = { Body = BinaryExpression { left = UnaryExpression { Operand = MemberExpression { expression = ParameterExpression { type = JVMCLRConvertExpression.xRow,
            // name = z },
            // field = long Key },
            // Type = java.lang.Long },
            // right = UnaryExpression { Operand = MemberExpression { expression = Constant { value = JVMCLRConvertExpression.Program___c__DisplayClass0@8edb84,
            // type =  },
            // field = long u },
            // Type = java.lang.Long },
            // liftToNull = false,
            // method =  }, parameters = [LScriptCoreLib.Shared.BCLImplementation.System.Linq.Expressions.__ParameterExpression;@edf1de } }

            Console.WriteLine(new { f });

            CLRProgram.CLRMain();
        }


    }



    [SwitchToCLRContext]
    static class CLRProgram
    {
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
