using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
[assembly: Obfuscation(Feature = "script")]
namespace TestFor
{
    struct foo
    {
        public int i;

        static void add(ref foo p)
        {
            //// TestFor.Program.add
            // this.AQAABvfjIDK5FE8y3QJ43A = function (ref$b)
            // {
            //   ref$b[0].i = (ref$b[0].i + 1);
            // };

            p.i++;
        }
    }

    class Program : ScriptCoreLib.Shared.IAssemblyReferenceToken
    {
    

        static void Main(string[] args)
        {
            Func<Task> y = async delegate
            {
                //                // TestFor.Program+<<Main>b__0>d__2+<>MoveNext.<0094> ldloca.s.try
                //this.IQAABmEeHDKi9yNAQtypBw = function (b, ref$c, ref$d, ref$e)
                //{
                //  UxoABo302jK_arjcePTx37w('<0094> ldloca.s');
                //  ref$d[0].ZQgABiCcyDCAZI5EVliZfg();
                //  ref$d;
                //  ref$c._i_5__3 = (ref$c._i_5__3 + 1);
                //  return 179;
                //};


                for (int i = 0; i < 4; i++)
                {
                    Console.WriteLine(new { i });

                    await Task.Delay(500);
                }
            };

            y().Wait();
        }
    }
}
