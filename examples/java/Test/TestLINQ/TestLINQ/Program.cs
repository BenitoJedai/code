using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using ScriptCoreLib;
using ScriptCoreLib.Delegates;
using ScriptCoreLib.Extensions;
using ScriptCoreLibJava.Extensions;
using System.Xml.Linq;
using java.net;
using java.util.zip;
using System.Collections;
using System.IO;

namespace TestLINQ
{
    class x
    {
        public string Name;
        public string Value;
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

            // generic parameter needs to be moved..
            //enumerable_10 = __Enumerable.AsEnumerable(__SZArrayEnumerator_1<String>.Of(stringArray3));

            Console.WriteLine("hi! vm:" + typeof(object).FullName);

            var a = new[] { "x", "foo1", "bar", "foo2" }.AsEnumerable();
            //var a = new List<string> { "foo", "bar" };

            var q = from i in a
                    where i.StartsWith("f")
                    select i;


            q.WithEach(
                x =>
                {
                    Console.WriteLine("x: " + x);
                    Console.WriteLine(new { x });
                }
            );

            System.Console.WriteLine("done");


            var query = @"insert into @bar Table1 (@ContentValue)  @foo values (@ContentValue) @bar";

            Console.WriteLine(query);

            var parameters = new[]
            {
                new x { Name = "@ContentValue", Value = "Foo" },
                new x { Name = "@foo", Value = "Foo2" },
                new x { Name = "@bar", Value = "Foo3" }
            };

            foreach (var item in
                from p in parameters
                from i in query.GetIndecies(p.Name)
                orderby i
                select new { p.Name, p.Value, i }

                )
            {
                Console.WriteLine(new { item }.ToString());
            }

            parameters.WithEach(
                p =>
                {
                    query = query.Replace(p.Name, "?");
                }
            );

            Console.WriteLine(query);

            System.Console.WriteLine("jvm");


            CLRProgram.XML = new XElement("hello", "world");
            CLRProgram.CLRMain(
            );

        }


    }

    static class XX
    {
        public static IEnumerable<int> GetIndecies(this string e, string f)
        {
            var a = new List<int>();

            var p = 0;
            var i = e.IndexOf(f, p);
            while (i >= 0)
            {
                p = i + 1;

                a.Add(i);

                i = e.IndexOf(f, p);
            }

            return a;
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
        public static void CLRMain(
             StringAction ListMethods = null
            )
        {
            System.Console.WriteLine(XML);

            MessageBox.Show("it works?!?");
        }
    }

}
