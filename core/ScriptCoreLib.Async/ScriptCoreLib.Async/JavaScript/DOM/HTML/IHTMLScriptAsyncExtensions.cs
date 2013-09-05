using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ScriptCoreLib.JavaScript.Extensions;
using System.Runtime.CompilerServices;

namespace ScriptCoreLib.JavaScript.DOM.HTML
{
    public static class IHTMLScriptAsyncExtensions
    {

        public static Task<IHTMLScript> ToTask(this IHTMLScript i)
        {
            var y = new TaskCompletionSource<IHTMLScript>();

            i.onload +=
                delegate
                {
                    y.SetResult(i);


                    // cleanup
                    i.Orphanize();
                };

            i.AttachToHead();

            return y.Task;
        }

        public static IEnumerable<IHTMLScript> ScriptElements(this IElement i)
        {
            return i.querySelectorAll(IHTMLElement.HTMLElementEnum.script).Select(k => (IHTMLScript)k);
        }

        public static TaskAwaiter<IHTMLScript> GetAwaiter(this IHTMLScript i)
        {
            return i.ToTask().GetAwaiter();
        }

        public static TaskAwaiter<IHTMLScript[]> GetAwaiter(this IEnumerable<IHTMLScript> i)
        {
            var script = i
                //.Where(x => x.nodeName.ToLower() == "script")
                .Select(x => ((IHTMLScript)x).ToTask());

            var y = Task.WhenAll(script);

            return y.GetAwaiter();
        }

        public static TaskAwaiter<TResult[]> GetAwaiter<TResult>(this IEnumerable<Task<TResult>> i)
        {
            return Task.WhenAll(i).GetAwaiter();
        }
    }
}
