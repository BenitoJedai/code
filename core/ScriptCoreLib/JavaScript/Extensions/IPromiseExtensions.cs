using ScriptCoreLib.JavaScript.DOM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScriptCoreLib.JavaScript.Extensions
{
    [Script]
    public static class IPromiseExtensions
    {
        // X:\jsc.svn\examples\javascript\Test\TestServiceWorker\TestServiceWorker\Application.cs

        // until jsc supports Task from native calls?
        public static Task<T> AsTask<T>(this IPromise<T> z)
        {
            // X:\jsc.svn\examples\javascript\test\TestServiceWorkerClient\TestServiceWorkerClient\Application.cs

            var x = new TaskCompletionSource<T>();

            z.then(x.SetResult);

            return x.Task;
        }

    }
}
