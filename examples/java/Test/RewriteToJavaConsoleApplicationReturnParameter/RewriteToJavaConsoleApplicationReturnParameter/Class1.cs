using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ScriptCoreLib;
using System.Xml.Linq;

namespace RewriteToJavaConsoleApplicationReturnParameter
{
    public class Class1
    {
        public static void Main(string[] e)
        {
            var r = new XElement("request");
            var x = new XElement("subrequest");
            var data1 = new XElement("data1");

            x.Add(data1);
            r.Add(x);

            var value = Convert.ToBase64String(
                new byte[] { 0, 0x7f, 0xFF }
            ); ;

            Console.WriteLine("value: " + value);

            data1.Value = value;

            Console.WriteLine("data1.value: " + data1.Value);

            x.Element("data1").Value = data1.Value;

            Console.WriteLine(x);

            Console.WriteLine("subrequest.data1.value: " + x.Element("data1").Value);


            Console.WriteLine(r);

            Console.WriteLine(r.GetString1());
        }
    }

    [SwitchToCLRContext]
    static class ExtensionsToSwitchToCLRContext
    {
        public static XElement GetString1(this XElement data)
        {
            Console.WriteLine("enter GetString1");

            var data1 = data.Element("subrequest").Element("data1");
            var value = Convert.FromBase64String(data1.Value);

            foreach (var item in value)
            {
                Console.Write(item.ToString("x2"));
            }
            Console.WriteLine();


            
            var doc = new XElement("result");

            doc.Add(Environment.StackTrace);
            doc.Add(data);

            return doc;
        }
    }
}
