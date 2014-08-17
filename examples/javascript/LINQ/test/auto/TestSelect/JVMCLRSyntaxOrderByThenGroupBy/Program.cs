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
using ScriptCoreLib.Query.Experimental;

namespace JVMCLRSyntaxOrderByThenGroupBy
{

    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main(string[] args)
        {
            // once this syntax thingy compiles            // lets run it under appengine, and android too..
            try
            {
                TryMain();
            }
            catch (Exception ex) // if (ex.Message.Contains("goo"))
            {
                // X:\jsc.svn\core\ScriptCoreLibJava\BCLImplementation\System\Exception.cs

                Console.WriteLine(new { ex, ex.Message, ex.StackTrace });

            }

        }

        private static void TryMain()
        {
            var f = (
                   from x in new xTable()

                   orderby x.field1 ascending

                   group x by 1 into gg

                   select new
                   {
                       gg.Last().Tag
                   }

               ).FirstOrDefault();
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
