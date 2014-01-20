using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestInlineIncrement
{
    class Program
    {
        public static long Counter = 0;

        static void Main(string[] args)
        {
            var ccc = 0L;

            Action y = delegate
            {
                ccc = Counter++;
            };
        }
    }
}
