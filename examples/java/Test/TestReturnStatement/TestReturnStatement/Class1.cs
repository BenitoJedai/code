using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
[assembly: Obfuscation(Feature = "script")]

namespace TestReturnStatement
{
    public class Class1
    {
        public string Text;

        public override string ToString()
        {
            return "hello: " + Text;
        }
    }
}
