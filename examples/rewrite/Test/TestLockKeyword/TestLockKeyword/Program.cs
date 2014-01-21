using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestLockKeyword
{
    class Program
    {
        public static object Sync = new object();


        static void Main(string[] args)
        {
            // https://sites.google.com/a/jsc-solutions.net/backlog/knowledge-base/2014/201401/20140121

            lock (Sync)
            {
                Console.WriteLine("hi");
            }
        }
    }
}
