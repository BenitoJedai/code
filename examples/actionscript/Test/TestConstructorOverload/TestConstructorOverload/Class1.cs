using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;
using ScriptCoreLib;
using System.Windows;

[assembly: Obfuscation(Feature = "script")]

namespace TestConstructorOverload
{
    [Script(Implements = typeof(global::System.Collections.Generic.IEnumerable<>))]
    internal interface __IEnumerable<T> 
    {
    }

    [Script(Implements = typeof(global::System.Collections.Generic.List<>))]
    internal class __List<T>
    {
        public __List()
            : this(null)
        {

        }

        public __List(IEnumerable<T> collection)
        {
            
        }
    }

    public class __ValueCollection : List<object>
    {

    }

    [Script(Implements = typeof(global::System.Windows.Vector))]
    internal class __Vector
    {
        public double X { get; set; }
        public double Y { get; set; }

        public __Vector()
            : this(0, 0)
        {

        }

        public __Vector(double X, double Y)
        {
            this.X = X;
            this.Y = Y;
        }
    }

    public class Class1<T>
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

        readonly T nn;

        public Class1(string a, string e, string b, string x, string c, string y, string d, int i = 3, T n = default(T))
        {
            nn = n;
        }
    }

    class A : Class1<int> 
    {
        public A() : base("e")
        {

        }
    }

    class B : Class1<string>
    {
        public B()
            : base("x", "y")
        {

        }
    }


    class MyClass
    {
        void Foo()
        {
            var x = default(Vector);
        }

        void Bar()
        {
            var x = new Vector(1, 1);
        }

        public MyClass()
        {
            var u1 = new Class1<object>("e");
            var u2 = new Class1<object>("x", "y");

            var u3 = new Class1<object>(
                a: "a",    
                e: "e",    
                b: "b",    
                x: "x",    
                c: "c",    
                y: "y",    
                d: "d"    
            );

            var u4 = new Class1<object>(
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
