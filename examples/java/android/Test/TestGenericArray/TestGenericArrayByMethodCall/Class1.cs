using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

[assembly: Obfuscation(Feature = "script")]
namespace TestGenericArrayByMethodCall
{
    public class Class1
    {
        public static void Test(List<Class1> e, List<List<Class1>> z)
        {
            // https://sites.google.com/a/jsc-solutions.net/backlog/knowledge-base/2012/20120-1/20120817-uri
            // jsc shall do array casting here 
            var y = e.ToStringArray();
            var x = e.ToArray();

            var zy = z.ToStringArray();
            var zx = z.ToArray();
        }

        public static T[] Test<T>(List<T> e)
        {
            // https://sites.google.com/a/jsc-solutions.net/backlog/knowledge-base/2012/20120-1/20120817-uri
            // jsc shall do array casting here 
            var y = e.ToStringArray();
            var x = e.ToArray();

            return x;
        }

        public Class1[] __example(object[] e)
        {
            if (e is Class1[])
                return (Class1[])e;

            var a = new Class1[e.Length];

            a[0] = (Class1)e[0];

            return a;
        }
    }

    public class List<T>
    {
        public string[] ToStringArray()
        {
            return null;
        }

        public T[] ToArray()
        {
            return null;
        }
    }
}
