using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
[assembly: Obfuscation(Feature = "script")]
namespace TestFunctionCall
{
    public class Class1
    {
        public Class1(string e)
        {

        }

        public Class1(string x, params object[] e)
        {
            new Class1("", null);
        }
    }


}

