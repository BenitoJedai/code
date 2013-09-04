using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace ScriptCoreLib.JavaScript.DOM.HTML
{
    public static class IHTMLImageAsyncExtensions
    {
        // tested by
        // X:\jsc.svn\examples\javascript\HulaGirl\HulaGirl\Library\HulaGirl.cs

        public static TaskAwaiter<IHTMLImage> GetAwaiter(this IHTMLImage i)
        {
            var y = new TaskCompletionSource<IHTMLImage>();
            i.InvokeOnComplete(y.SetResult);
            return y.Task.GetAwaiter();
        }

        public static TaskAwaiter<TResult[][]> GetAwaiter<TResult>(this IEnumerable<Task<TResult[]>> i)
        {
            return Task.WhenAll(i).GetAwaiter();
        }
    }
}
