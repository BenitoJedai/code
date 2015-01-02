using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Test453If
{
    namespace fake { public interface IAsyncStateMachine { } }

    class Program 
        //: fake.IAsyncStateMachine
    {
        // https://sites.google.com/a/jsc-solutions.net/backlog/knowledge-base/2015/201501/20150102

        //static
            void MoveNext(string[] r)
        {
            if (r == null)
            {
                Console.WriteLine("InternalWebMethodRequest.Complete r is null. why?");
            }
        }

        static void Main(string[] r) => new Program { }. MoveNext(r);

       
    }
}
