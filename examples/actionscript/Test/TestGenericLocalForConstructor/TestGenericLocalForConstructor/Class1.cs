using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
[assembly: Obfuscation(Feature = "script")]
namespace TestGenericLocalForConstructor
{
    public class Class1Base<T>
    {
        public Class1Base(T t = default(T))
        {

        }
    }

    public class Class1<T> : Class1Base<T>
    {
        public Class1(T arg)
            : base(default(T))
        {
            var loc = arg;

            new Class1<T>(loc);
        }
    }

    public class Class1
    {
        public Class1(object arg)
        {
            var loc = arg;

            new Class1(loc);
        }
    }
}
