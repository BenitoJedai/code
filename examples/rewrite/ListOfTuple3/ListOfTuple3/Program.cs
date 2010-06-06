using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ListOfTuple3
{
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

    class Program
    {
        static void Main(string[] args)
        {
            var AllPages = new
            {
                Preview = default(Func<object>),
                CanvasType = default(Func<Type>),
                Text = default(Func<string>)
            }.ToEmptyList(
                (Func<object> Preview, Func<Type> CanvasType, Func<string> Text) => new { Preview, CanvasType, Text }
            );
        }
    }
}
