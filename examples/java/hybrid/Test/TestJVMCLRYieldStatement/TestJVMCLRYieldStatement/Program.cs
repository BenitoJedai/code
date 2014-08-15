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
using System.Reflection;
using System.Data.Common;

namespace TestJVMCLRYieldStatement
{

    static class Program
    {


        public static IEnumerable<TElement> ReadToElements<TElement>(DbDataReader r, IEnumerable<TElement> source)
        {
            // X:\jsc.svn\examples\rewrite\Test\TestYieldStatement\TestYieldStatement\Class1.cs
            // x:\jsc.svn\examples\java\hybrid\test\testjvmclryieldstatement\testjvmclryieldstatement\program.cs

            Console.WriteLine("enter AsEnumerable ");

            //while (r.Read())
            //{
            // what the flip jsc java?
            // https://sites.google.com/a/jsc-solutions.net/backlog/knowledge-base/2014/201408/20140813

            // jsc resolves to BCL but forgets generic args. needs a fix.
            yield return ReadToElement<TElement>(r, source, new Tuple<MemberInfo, int>[0]);
            //}

            Console.WriteLine("exit AsEnumerable ");
        }

        public static TElement ReadToElement<TElement>(DbDataReader r, IEnumerable source, Tuple<MemberInfo, int>[] Target)
        {
            Console.WriteLine("enter ReadToElement ");
            return default(TElement);
        }

        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        public static void Main(string[] args)
        {
            // jsc needs to see args to make Main into main for javac..



            System.Console.WriteLine(
               typeof(object).AssemblyQualifiedName
            );

            var a = ReadToElements(
                 default(DbDataReader),
                 default(IEnumerable<object>)
             );

            foreach (var item in a)
            {

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
