using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TestUltraBCLImplementation
{
    public class Class1
    {
        public Class1()
        {
            // step 1. make jsc to rewrite this as .NET 4

            // step 2. make use of a .NET 4 type like Tuple

            var t = Tuple.Create(1, 2);

            // step 3. check the rewrite

            // step 4. make sure a BCLImplementation also gets rewritten
        }
    }
}
