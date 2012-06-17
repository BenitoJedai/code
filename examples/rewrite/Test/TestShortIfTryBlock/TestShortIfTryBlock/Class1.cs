using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace TestShortIfTryBlock
{
    public static class Class1
    {
        public static void Foo(bool e)
        {
            if (e)
            {
                using (new StringWriter())
                {
                    Console.WriteLine();
                }
            }
        }
    }
}
