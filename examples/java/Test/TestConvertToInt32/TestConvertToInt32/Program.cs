using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TestConvertToInt32
{
    class Program
    {
        public static void Main(string[] args)
        {
            Console.WriteLine("http://blogs.infosupport.com/blogs/alexb/archive/2011/02/04/c-and-net-don-t-freeze-when-converting-2-2250738585072012e-308.aspx");

            Console.WriteLine("JVM will hang in the next statement (2011/02/02)");

            double d = Double.Parse("2.2250738585072012e-308");
            Console.WriteLine("" + d);
        }
    }
}
