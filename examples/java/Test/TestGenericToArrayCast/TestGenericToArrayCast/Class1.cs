using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;
[assembly: Obfuscation(Feature = "script")]
namespace TestGenericToArrayCast
{
    public interface IEnumerable<T>
    {
    }

     public static class Enumerable
    {
        public static T[] ToArray<T>(this IEnumerable<T> e)
        {
            return null;
        }
    }

    public class List<T>
    {
        public T[] ToArray()
        {
            return null;
        }
    }

    public class Class1
    {
        static void Invoke(List<float> a, List<Class1> b, IEnumerable<string> f)
        {
            var x = a.ToArray();
            var y = b.ToArray();

            var z = f.ToArray();

        }

    }
}
