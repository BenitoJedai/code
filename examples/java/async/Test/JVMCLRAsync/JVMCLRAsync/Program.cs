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

namespace JVMCLRAsync
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




            //0001 02000002 JVMCLRAsync__i__d.jvm::JVMCLRAsync.Program
            //0001 02000003 JVMCLRAsync__i__d.jvm::JVMCLRAsync.Program+<<Main>b__0>d__2
            //internal compiler error at method
            // type: JVMCLRAsync.Program+<<Main>b__0>d__2+<MoveNext>0600000e
            // method: <0000> ldc.i4.1.try
            // Object reference not set to an instance of an object.
            //    at jsc.Script.CompilerCLike.EmitIfBlock(Prestatement p, ILIfElseConstruct iif) in x:\jsc.internal.svn\compiler\jsc\Languages\CompilerCLike.cs:line 864


            // jsc java does not understand our async/switch rewriter?

            Action goo = async delegate
            {
                // will this break jvm build?
            };

            goo();

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
