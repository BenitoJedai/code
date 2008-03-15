using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GetSingleDigit
{
    static class X
    {
        public static int GetDigit(this int value, int offset)
        {
            var x0 = Math.Pow(10 , offset + 0);
            var x1 = Math.Pow(10 , offset + 1);

            var y0 = (int)Math.Floor(value / x0);
            var y1 = (int)Math.Floor(value / x1);

            return y0 - (y1 * 10);
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            var value = 9020567;
            var index = 1;
            var expected = 6;

            var result = value.GetDigit(index);
            
        }
    }
}
