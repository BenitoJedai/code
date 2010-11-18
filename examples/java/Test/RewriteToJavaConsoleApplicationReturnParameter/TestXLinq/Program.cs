using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;

namespace TestXLinq
{
    class Program
    {
        static void Main(string[] args)
        {
            {
                Console.WriteLine("\n*** TEST 1\n");
                var r = new XElement("request");
                r.Value = "hello";
                Console.WriteLine("r.Value: " + r.Value);
            }
            {
                Console.WriteLine("\n*** TEST 2\n");
                var r = new XElement("request");
                r.Add("hello");
                Console.WriteLine("r.Value: " + r.Value);
            }
            {
                Console.WriteLine("\n*** TEST 3\n");
                var r = new XElement("r");
                r.Add("hello");
                var doc = new XElement("doc", r);

                Console.WriteLine("doc.r.Value: " + doc.Element("r").Value);
            }

            {
                Console.WriteLine("\n*** TEST 4\n");
                var r = new XElement("r");
                r.Value = "hello";
                var doc = new XElement("doc", r);

                Console.WriteLine("doc.r.Value: " + doc.Element("r").Value);
            }

            {
                Console.WriteLine("\n*** TEST 5\n");
                var r = new XElement("r");
                var doc = new XElement("doc", r);
                r.Value = "hello";

                Console.WriteLine("doc.r.Value: " + doc.Element("r").Value);
            }

            {
                Console.WriteLine("\n*** TEST 6\n");
                var r = new XElement("r");
                var x = new XElement("x", r);
                r.Value = "hello";
                var doc = new XElement("doc", x);

                Console.WriteLine("doc.x.r.Value: " + doc.Element("x").Element("r").Value);
            }

            {
                Console.WriteLine("\n*** TEST 7\n");
                var r = new XElement("r");
                var x = new XElement("x", r);
                var doc = new XElement("doc", x);
                //r.Value = "hello";
                doc.Element("x").Element("r").Value = "hello";
                Console.WriteLine(doc);
                //Console.WriteLine("doc.x.r.Value: " + doc.Element("x").Element("r").Value);
            }

            {
                Console.WriteLine("\n*** TEST 8\n");
                var r = new XElement("r");
                var x = new XElement("x", r);
                var doc = new XElement("doc", x);
                r.Value = "hello";
                //doc.Element("x").Element("r").Value = "hello";
                //Console.WriteLine(doc);
                Console.WriteLine("doc.x.r.Value: " + doc.Element("x").Element("r").Value);
            }
        }

    }
}
