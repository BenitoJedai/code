using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace TestShortIfElseTryBlock
{
    public static class Class1
    {
        public static void Foo(bool e)
        {
            if (e)
            {
                Console.WriteLine();
            }
            else
            {
                using (new StringWriter())
                {
                    Console.WriteLine();
                }
            }
        }
    }
}
