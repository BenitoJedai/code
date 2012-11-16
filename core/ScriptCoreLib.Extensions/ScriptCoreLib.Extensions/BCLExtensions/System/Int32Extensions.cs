using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace System
{
    public static class Int32Extensions
    {
        public static int Min(this int e, int x)
        {
            return Math.Min(e, x);
        }

        public static int Max(this int e, int x)
        {
            return Math.Max(e, x);
        }
    }
}
