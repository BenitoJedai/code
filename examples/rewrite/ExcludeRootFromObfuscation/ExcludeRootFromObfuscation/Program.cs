using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;

namespace ExcludeRootFromObfuscation
{
    enum Alpha { };

    [Obfuscation(Exclude = true)]
    abstract class Bar
    {
        public abstract Alpha GetData();
    }

    abstract class Foo : Bar
    {
        public abstract Alpha GetXData();

    }

    class Program : Foo
    {
        public override Alpha GetData()
        {
            return default(Alpha);
        }

        public override Alpha GetXData()
        {
            return default(Alpha);
        }

        static void Main(string[] args)
        {
        }
    }
}
