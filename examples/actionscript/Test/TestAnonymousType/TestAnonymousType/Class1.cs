using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

[assembly: Obfuscation(Feature = "script")]

namespace TestAnonymousType
{
    // https://sites.google.com/a/jsc-solutions.net/backlog/knowledge-base/2015/201501/20150110

    sealed class __AnonymousType<T>
    {
        public T zoo { get; set; }
    }

}

namespace TestAnonymousTypeOther
{
    public static class Class1
    {
        public static object foo = new { zoo = 33 };



    }
}
