using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace TestNestedAsyncDelegate
{
    class Program
    {
        public event Action<Task> handler;

        static void Main(string[] args)
        {
            // https://sites.google.com/a/jsc-solutions.net/backlog/knowledge-base/2014/201401/20140118/testnestedasyncdelegate

            new Program().handler +=
                async z =>
                {
                    var x = args.Select(a => a.StartsWith("/"));

                    await z;

                    Expression<Func<object, bool>> f = o => o.ToString().StartsWith("o");

                };
        }
    }
}
