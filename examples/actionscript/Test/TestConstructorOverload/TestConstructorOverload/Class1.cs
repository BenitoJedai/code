using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;

[assembly: Obfuscation(Feature = "script")]

namespace TestConstructorOverload
{
    public class Class1
    {
        #region to be inlined into the caller
        public Class1(string e)
            : this("a", e, null, null, null, null, null, 1)
        {

        }

        public Class1(string x, string y)
            : this(null, null, "b", x, null, y, null, 2)
        {

        }
        #endregion

        public Class1(string a, string e, string b, string x, string c, string y, string d, int i = 3)
        {

        }
    }

    class A : Class1 
    {
        public A() : base("e")
        {

        }
    }

    class B : Class1
    {
        public B()
            : base("x", "y")
        {

        }
    }


    class MyClass
    {
        public MyClass()
        {
            var u1 = new Class1("e");
            var u2 = new Class1("x", "y");

            var u3 = new Class1(
                a: "a",    
                e: "e",    
                b: "b",    
                x: "x",    
                c: "c",    
                y: "y",    
                d: "d"    
            );

            var u4 = new Class1(
                a: "a",    
                e: "e",    
                b: "b",    
                x: "x",    
                c: "c",    
                y: "y",    
                d: "d",   
                i: 4
            );
        }
    }
}
