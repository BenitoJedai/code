﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace System
{
    public static class DoubleExtensions
    {
        public static double Min(this double e, double x)
        {
            return Math.Min(e, x);
        }

        public static double Max(this double e, double x)
        {
            return Math.Max(e, x);
        }
    }
}
