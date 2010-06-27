using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace InteractiveDAESceneB.Library
{
    public static class MyExtensions
    {
        public static int[] ParseToInt32Array(this string e)
        {
            return e.Split(' ').Select(kk => int.Parse(kk)).ToArray();
        }

        public static double[] ParseToDoubleArray(this string e)
        {
            return e.Split(' ').Select(kk => double.Parse(kk)).ToArray();
        }
    }
}
