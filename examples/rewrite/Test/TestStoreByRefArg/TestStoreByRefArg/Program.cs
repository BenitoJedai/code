using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestStoreByRefArg
{
    struct foo { }
    class Program
    {
        static void Invoke(ref foo f, foo value)
        {
            f = value;
        }

        static void Main(string[] args)
        {
        }
    }
}
