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

namespace JVMCLRIdentityExpression
{

    static class Program
    {
        // X:\jsc.svn\core\ScriptCoreLib\Shared\BCLImplementation\System\Linq\Expressions\Expression.cs


        //found 1159 types to be compiled
        //script: error JSC1000: Java : unable to emit ldtoken at 'JVMCLRIdentityExpression.Program.GetIdentity'#0001: typeof(T) not supported due to type erasure

        //- javac
        //"C:\Program Files (x86)\Java\jdk1.7.0_45\bin\javac.exe" -classpath "Y:\staging\web\java";release -d release java\JVMCLRIdentityExpression\Program.java
        //Y:\staging\web\java\ScriptCoreLib\Shared\BCLImplementation\System\Linq\Expressions\__Expression.java:278: error: constructor __ReadOnlyCollection_1 in cl
        //        expression_10.set_Parameters(new __ReadOnlyCollection_1<__ParameterExpression>(parameters));
        //                                     ^
        //  required: __IList_1<__ParameterExpression>
        //  found: __ParameterExpression[]
        //  reason: actual argument __ParameterExpression[] cannot be converted to __IList_1<__ParameterExpression> by method invocation conversion
        //  where T is a type-variable:
        //    T extends Object declared in class __ReadOnlyCollection_1

        static Type GetGenericType<T>()
        {
            return typeof(T);
        }

        static Expression<Func<TSource, TSource>> GetIdentity<TSource>(TSource source)
        {
            return x => x;
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

            var e = GetIdentity("");

            Console.WriteLine(new { e });


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
