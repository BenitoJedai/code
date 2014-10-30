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
using System.Net.Sockets;
using System.Net;
using System.Security.Cryptography.X509Certificates;
using System.Net.Security;
using System.Diagnostics;
using ScriptCoreLib.JavaScript.Extensions;

namespace JVMCLRRoslynConstructor
{
    // <LangVersion>Experimental</LangVersion>
    // http://blog.drorhelper.com/2014/09/visual-studio-2014-ctp-first-impressions.html
    // Error	1	Feature 'primary constructor' is only available in 'experimental' language version.	X:\jsc.svn\examples\java\hybrid\JVMCLRRoslynConstructor\JVMCLRRoslynConstructor\Program.cs	24	18	JVMCLRRoslynConstructor
    class MyClass(string goo)
    {
//        Implementation not found for type import :
// type: System.Threading.Interlocked
// method: T CompareExchange[T](T ByRef, T, T)
// Did you forget to add the[Script] attribute?
//Please double check the signature!

        public event Action yield;

        // https://roslyn.codeplex.com/wikipage?title=Samples%20and%20Walkthroughs&referringTitle=Home

        {
            Console.WriteLine(new { goo });


        }
    }

    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        public static void Main(string[] args)
        {
     

            System.Console.WriteLine(
               typeof(object).AssemblyQualifiedName
            );

            new MyClass(goo: "gooo")
            {
               //  yield += delegate { }

            };

           


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
