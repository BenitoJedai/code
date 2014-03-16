using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WebGLTunnel.Library
{
    public static class MyExtensions
    {
        public static void Add<T>(this List<T> e, T a, T b)
        {
            e.Add(a);
            e.Add(b);
        }

        public static void Add<T>(this List<T> e, T a, T b, T c)
        {
            e.Add(a);
            e.Add(b);
            e.Add(c);
        }

        public static void Add<T>(this List<T> e, T a, T b, T c, T d)
        {
            e.Add(a);
            e.Add(b);
            e.Add(c);
            e.Add(d);
        }
    }
}
