using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace ScriptCoreLib.JavaScript.DOM.HTML
{
    public static class IHTMLButtonAsyncExtensions
    {
        // tested by
        // X:\jsc.svn\examples\javascript\AsyncButtonSequence\AsyncButtonSequence\Application.cs

        public static TaskAwaiter<IHTMLButton> GetAwaiter(this IHTMLButton i)
        {
            var y = new TaskCompletionSource<IHTMLButton>();
            //i.InvokeOnComplete(y.SetResult);

            i.onclick +=
                delegate
                {
                    y.SetResult(i);
                };

            return y.Task.GetAwaiter();
        }

        public static IHTMLButton WhenClicked(this IHTMLButton e, Func<IHTMLButton, Task> h)
        {
            e.onclick +=
                async delegate
                {

                    e.disabled = true;

                    await h(e);

                    e.disabled = false;

                };

            return e;
        }
    }
}
