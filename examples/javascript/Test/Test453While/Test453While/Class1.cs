using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
[assembly: Obfuscation(Feature = "script")]
namespace Test453While
{
    public static class Class1
    {
        // https://sites.google.com/a/jsc-solutions.net/backlog/knowledge-base/2015/201501/20150102/foreach
        public static void loopStart() { }
        public static void loopEnd() { }
        public static void join() { }

        public static void Invoke(IEnumerable<object> e)
        {
            // https://sites.google.com/a/jsc-solutions.net/backlog/knowledge-base/2015/201501/20150102/foreach

            //02000002 Test453ForEach.Class1
            //script: error JSC1000: unknown while condition at Void Invoke(System.Collections.Generic.IEnumerable`1[System.Object]).Maybe you did not turn off c# compiler 'optimize code' feature?

            // 2015 comples it the way jsc likes it. yet for foreach it optimizes.
            // lets turn on optimization then?
            var x = e.GetEnumerator();
            while (x.MoveNext())
            {
                loopStart();
                loopEnd();
            }

            join();

            //var a = [this], c, d;

            //c = b.IQIABnMeWzaNooAKOmFm5g();
            //while (c.BgIABu7N0xGI6ACQJ1TEOg())
            //{
            //}

        }
    }
}
