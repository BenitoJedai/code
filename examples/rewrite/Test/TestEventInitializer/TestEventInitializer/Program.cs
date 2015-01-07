using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestEventInitializer
{
    class Program
    {
        public event Action foo;

        static void Main(string[] args)
        {
            // http://grantwinney.com/what-do-these-new-c-6-features-do-stack-overflow/
            // C# 7 ??
            new Program { foo += delegate { } };
        }
    }
}
