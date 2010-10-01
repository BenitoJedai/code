using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TestResourceUsage
{
    class Program
    {
        static void Main(string[] args)
        {
            var s = "TestResourceUsage.Folder1.TextFile1.txt";

            var u = typeof(Program).Assembly.GetManifestResourceStream(s);

            Console.WriteLine(s);
        }
    }
}
