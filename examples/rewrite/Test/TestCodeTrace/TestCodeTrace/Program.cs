using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;

namespace TestCodeTrace
{
    public class IDocument<TConstraint1>
    {
        public IXMLDocument __Type1;
    }


    public class IXMLDocument : IDocument<object>
    {
        void Method2<TConstraint2>(TConstraint2 e) where TConstraint2 : ICloneable
        {

        }
    }


    class Program
    {
        static void Method2(IDocument<object> e)
        {
        }

        static void Main(string[] args)
        {

            //Console.WriteLine("typeof(Program)");
            //Console.WriteLine(typeof(Program).GetHashCode());
            //Console.WriteLine(typeof(Program).GetHashCode());

            //Console.WriteLine("typeof(Program)");
            //Console.WriteLine(typeof(Action<>).GetHashCode());
            //Console.WriteLine(typeof(Action<>).GetHashCode());

            //Console.WriteLine(typeof(Action<Program>).GetHashCode());
            //Console.WriteLine(typeof(Action<Program>).GetHashCode());

            //Console.WriteLine(typeof(Action<object>).GetHashCode());
            //Console.WriteLine(typeof(Action<object>).GetHashCode());

            //Console.WriteLine("hello world");
        }
    }
}
