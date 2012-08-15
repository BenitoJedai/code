using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;

[assembly: Obfuscation(Feature = "script")]

namespace TestGenericList
{
    public class Class1
    {
        public static void Invoke()
        {
            var nongeneric = new java.util.ArrayList();

            nongeneric.add(new Class1());

            var generic = new java.util.ArrayList<Class1>();

            generic.add(new Class1());


        }
    }
}
