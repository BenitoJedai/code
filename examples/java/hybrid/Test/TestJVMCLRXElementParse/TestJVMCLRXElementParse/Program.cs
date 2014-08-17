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

namespace TestJVMCLRXElementParse
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

            try
            {
                var x = XElement.Parse("<xml>hello </xml>");
                Console.WriteLine(new { x });

                var xx = XElement.Parse("<xml>hello </");
                Console.WriteLine(new { xx });
            }
            catch (Exception ex)
            {
                //java.lang.Object, rt
                //{{ x = <xml>hello </xml> }}
                //[Fatal Error] :1:14: The element type "xml" must be terminated by the matching end-tag "</xml>".
                //XDocument Parse error: { text = <xml>hello </ }
                //{{ Message = The element type "xml" must be terminated by the matching end-tag "</xml>"., StackTrace = java.lang.RuntimeException: The element type "xml" must be terminated by the matching end-tag "</xml>".
                Console.WriteLine(new { ex.Message, ex.StackTrace });
            }

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
