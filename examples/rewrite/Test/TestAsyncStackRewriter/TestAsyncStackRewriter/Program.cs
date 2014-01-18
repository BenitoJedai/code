using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace TestAsyncStackRewriter
{
    class Program
    {
        static void Main(string[] args)
        {
        }

        static async void XMain(Task x)
        {
            var button = new XElement("button");

            await x;

            var outdata = new XElement("FB-api", 
                new XAttribute("do", "login"),
                //new XAttribute("do2", "login")

                // using data from time before async is causing us issues
                // why?
                button
            );

        }
    }
}
