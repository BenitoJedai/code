using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestControlsSetVisible
{
    class Program
    {
        public bool Visible { get; set; }

        static void Main(string[] args)
        {
            var a = new[] { new Program() };

            // https://sites.google.com/a/jsc-solutions.net/backlog/knowledge-base/2014/201401/20140120/avg

            var w = a.AsEnumerable().Where(x => x.Visible);

        }
    }
}
