using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TestClosureThis
{
    public class Class1
    {
        class ClosureType
        {
            public Class1 __this;
        }

        public void Foo_forwardref()
        {
            var c = new ClosureType();
            c.__this = this;
            Foo_workflow(c);
        }
        public int foo;

        static void Foo_workflow(ClosureType e)
        {
            var foo = e.__this.foo;

            foo++;
            ; ;

            e.__this.foo = foo;

        }
    }

    public struct Class2
    {

        class ClosureType
        {
            public Class2 __this;
        }

        public void Foo_forwardref()
        {
            var c = new ClosureType();
            c.__this = this;

            Foo_workflow(c);
        }
        public int foo;

        static void Foo_workflow(ClosureType e)
        {
            var foo = e.__this.foo;

            foo++;
            ; ;

            e.__this.foo = foo;
        }

    }

    class Program
    {
        public static void Main(string[] e)
        {
            {
                var c = new Class1();
                c.Foo_forwardref();
                var cfoo = c.foo;
            }

            {
                var c = new Class2();
                c.Foo_forwardref();
                var cfoo = c.foo;
            }
        }
    }
}
