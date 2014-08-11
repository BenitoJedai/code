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

namespace TestJVMCLRArrayInitAsEnumerable
{

    static class Program
    {
        public static void Delete<TElement>(this IEnumerable<TElement> source)
        {
            // https://sites.google.com/a/jsc-solutions.net/backlog/knowledge-base/2014/20140705/20140705


            // how was it done before?
            // tested by?

            var nsource = new xDelete { source = source };


            var c = new object();


            var w = new SQLWriter<TElement>(nsource, new IEnumerable[] { nsource }, Command: c);

        }

        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        public static void Main(string[] args)
        {
            // jsc needs to see args to make Main into main for javac..

            // https://sites.google.com/a/jsc-solutions.net/backlog/knowledge-base/2014/201408/20140811


            // see also>
            // X:\jsc.svn\examples\javascript\android\AndroidBroadcastLogger\AndroidBroadcastLogger\ApplicationWebService.cs

            System.Console.WriteLine(
               typeof(object).AssemblyQualifiedName
            );


            Delete(default(IEnumerable<object>));




            CLRProgram.CLRMain();
        }

        private class SQLWriter<TElement>
        {
            private object Command;
            private IEnumerable[] enumerable;
            private xDelete nsource;

            public SQLWriter(xDelete nsource, IEnumerable[] enumerable, object Command)
            {
                this.nsource = nsource;
                this.enumerable = enumerable;
                this.Command = Command;

                Console.WriteLine("in SQLWriter");
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
