using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TestSwitchWithLocalByRef
{
    interface IAsyncStateMachine { }

    public struct Class1 : IAsyncStateMachine
    {
        public int value;

        public void MoveNext()
        {
            // jsc will force switch rewrite

            var b = this;
            Console.WriteLine(new { b = new { b.value } });

            var a = new Class1 { value = 7 };
            Console.WriteLine(new { a = new { a.value } });

            if (a.value != b.value)
            {
                Console.WriteLine("try");

                try
                {
                    Invoke(ref a, ref b);
                }
                catch (Exception ex)
                {
                    Console.WriteLine("error");

                    //var ex0 = ex;
                }

                Console.WriteLine(new { b = new { b.value } });
                Console.WriteLine(new { a = new { a.value } });
            }
            //Invoke(ref x);

            //b = a;
        }

        public static void Invoke(ref Class1 x, ref Class1 y)
        {

            //ref Class1 z = x;


            y.value = 14;

            //var y = x;
            x = y;


            //Invoke(ref x);
        }


        //static void CSharpWay()
        //{
        //    var c = new Class1 { value = 3 };

        //    Action u = delegate
        //    {

        //        Invoke(ref c);
        //    };

        //    Invoke(ref c);

        //    u();
        //}
    }



    static class Program
    {
        public static void Main(string[] args)
        {
            var z = default(Class1);

            z.MoveNext();

            //{ b = { value = 0 } }
            //{ a = { value = 7 } }
            //{ b = { value = 14 } }
            //{ a = { value = 14 } }

            //<0000>
            //{ b = { value = 0 } }
            //{ a = { value = 7 } }
            //<005c>
            //{ b = { value = 14 } }
            //{ a = { value = 14 } }
            //<0096>

        }
    }
}
