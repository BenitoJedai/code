using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ScriptCoreLib.Extensions
{
    public static class ListExtensions
    {
        public static void AddDistinct<T>(this List<T> source, T value)
        {
            if (source.Contains(value))
                return;

            source.Add(value);
        }


    }

    public class ListOfTuple3<A, B, C, T> : List<T>
    {
        readonly Func<A, B, C, T> f;
        public ListOfTuple3(Func<A, B, C, T> f)
        {
            this.f = f;
        }

        public void Add(A a, B b, C c)
        {
            this.Add(f(a, b, c));
        }
    }

    public static class ListOfTuple3Extensions
    {
        public static ListOfTuple3<A, B, C, T> ToEmptyList<A, B, C, T>(this T template, Func<A, B, C, T> f)
        {
            return new ListOfTuple3<A, B, C, T>(f);
        }
    }
}
