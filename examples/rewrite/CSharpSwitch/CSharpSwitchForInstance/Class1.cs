using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CSharpSwitchForInstance
{
    public class Class1Base
    {
        public Class1Base(int caseSwitch2)
        {
            var k = default(string);

            switch (caseSwitch2)
            {
                case 1:
                    k = ("Case 1");
                    break;
                case 2:
                    k = ("Case 2");
                    break;
                default:
                    k = ("Default case");
                    break;
            }

            Console.WriteLine(k);
        }
    }

    public class Class1 : Class1Base
    {
        public Class1(int caseSwitch2)
            : base(Bar(caseSwitch2) + 2)
        {
            var k = default(string);

            switch (caseSwitch2)
            {
                case 1:
                    k = ("Case 1");
                    break;
                case 2:
                    k = ("Case 2");
                    break;
                default:
                    k = ("Default case");
                    break;
            }

            Console.WriteLine(k);
        }



        static int Bar(int i)
        {
            return i + 1;
        }

    }

    public class Class2
    {


        public string Bar(int caseSwitch2)
        {
            switch (caseSwitch2)
            {
                case 1:
                    return ("Case 1");
                case 2:
                    return ("Case 2");
                default:
                    return ("Default case");
            }
        }
    }

    public class Class3
    {

        class closure<T, B>
        {
            public int arg0;

            public T arg1;

            public int ret;
        }

        public static int Bar<T, B>(int caseSwitch2, T a)
        {
            return Bar_init<T, B>(caseSwitch2, a);
        }

        static int Bar_init<T, B>(int caseSwitch2, T a)
        {
            var cc = new closure<T, B>();

            cc.arg0 = caseSwitch2;
            cc.arg1 = a;

            Bar_workflow<T, B>(cc);

            return cc.ret;
        }

        static void Bar_workflow<T, B>(closure<T, B> caseSwitch2)
        {
            caseSwitch2.ret = -2;
        }
    }

    public class Class4
    {

        public static string Bar<T, B>(int caseSwitch2, T a)
        {
            T b = a;

            switch (caseSwitch2)
            {
                case 1:
                    return ("Case 1");
                case 2:
                    return ("Case 2");
                default:
                    return ("Default case");
            }
        }
    }

    public class XFoo
    {
    }

    public class Class5
    {

        public static string Bar<T, B>(int caseSwitch2, T a, XFoo f)
        {
            T b = a;

            switch (caseSwitch2)
            {
                case 1:
                    return ("Case 1");
                case 2:
                    return ("Case 2");
                default:
                    return ("Default case");
            }
        }
    }

    public class Class6<XFoo>
    {

        public static string Bar<T, B>(int caseSwitch2, T a, XFoo f)
        {
            T b = a;
            Console.WriteLine();
            var c = caseSwitch2;
            Console.WriteLine();
            var n = c == 7;
 
            if (n)
            {
                Console.WriteLine();
                caseSwitch2 = 1;
                Console.WriteLine();
            }
            Console.WriteLine();

            switch (caseSwitch2)
            {
                case 1:
                    return ("Case 1");
                case 2:
                    return ("Case 2");
                default:
                    return ("Default case");
            }
        }
    }

    public class Class7
    {
        public static IEnumerable<string> Foo()
        {
            yield return "hello";
            yield return "world";
        }
    }

    public class Class8
    {
        public static IEnumerable<Func<string>> Foo()
        {
            yield return () => "hello";
            yield return () => "world";
        }
    }
}
