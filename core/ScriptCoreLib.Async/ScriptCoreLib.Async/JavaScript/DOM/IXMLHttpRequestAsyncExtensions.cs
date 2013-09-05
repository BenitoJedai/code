using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace ScriptCoreLib.JavaScript.DOM
{
    public static class IXMLHttpRequestAsyncExtensions
    {
        public static TaskAwaiter<IXMLHttpRequest> GetAwaiter(this IXMLHttpRequest r)
        {
            var s = new TaskCompletionSource<IXMLHttpRequest>();

            r.InvokeOnComplete(s.SetResult);

            return s.Task.GetAwaiter();
        }
    }
}
