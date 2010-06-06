using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TypeNameCollision
{
    class Program
    {
        static void Main(string[] args)
        {
            var a = new[]
            {
                ClassLibrary1.Class1.Get(),
                ClassLibrary2.Class1.Get(),
            };
        }
    }
}
