using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestAsyncStringConcat
{
    class Program
    {
        public string Text { get; set; }

        static void Main(string[] args)
        {
            // https://sites.google.com/a/jsc-solutions.net/backlog/knowledge-base/2014/201401/20140120/avg

            var f = new Program();

            Action y = async delegate
            {
                var s = Stopwatch.StartNew();

                f.Text += " " + s.ElapsedMilliseconds + "ms";
            };

        }
    }
}
