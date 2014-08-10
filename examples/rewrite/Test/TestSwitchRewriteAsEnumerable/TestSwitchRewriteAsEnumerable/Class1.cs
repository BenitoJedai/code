using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestSwitchRewriteAsEnumerable
{
    public class Class1
    {
        // https://sites.google.com/a/jsc-solutions.net/backlog/knowledge-base/2014/201408/20140810/asenumerable

        // e.i {[0x0011] stfld      +0 -2{[0x0000] ldarg.0    +1 -0} {[0x0010] ldloc.0    +1 -0} }
        //IEnumerable<object> e = new[] { new object() };

        // X:\jsc.internal.git\compiler\jsc.meta\jsc.meta\Commands\Rewrite\RewriteToAssembly\RewriteToAssembly.CreateMethodBaseEmitToArguments.cs
        //public Class1()
        //{
        //    //IEnumerable<object> e = new[] { new object() };

        //}

        //static async void Invoke()
        async void Invoke()
        {
            // X:\jsc.svn\examples\javascript\Test\TestAsyncAssignArrayToEnumerable\TestAsyncAssignArrayToEnumerable\Application.cs

            IEnumerable<object> collection = new[] { new object() };

            await Task.Yield();

            //        new Array(1)[0] = { };
            //ref$c[0]._collection_5__1 = _6QUABoocDD2jQ9Bz7rBALA(new Array(1));


            ////  d = c.bwQABoBf2jWIHILvaqtMig();
            foreach (var item in collection)
            {
                Console.WriteLine(new { item });
                // 0:25ms {{ item = [object Object] }} 
            }
        }
    }
}
