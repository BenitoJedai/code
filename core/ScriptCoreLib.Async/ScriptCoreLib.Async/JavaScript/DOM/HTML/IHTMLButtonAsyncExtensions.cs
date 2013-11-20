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

            var old = new { i.disabled };

            i.disabled = false;

            i.onclick +=
                delegate
                {
                    if (old == null)
                        return;

                    i.disabled = old.disabled;

                    old = null;

                    y.SetResult(i);
                };

            return y.Task.GetAwaiter();
        }



        public static IHTMLButton WhenClicked(this IHTMLButton e, Func<IHTMLButton, Task> h)
        {
            // tested by
            // X:\jsc.svn\examples\javascript\css\CSSFontFaceExperiment\CSSFontFaceExperiment\Application.cs

            var busy = false;

            e.onclick +=
                async delegate
                {
                    if (busy)
                        return;

                    busy = true;

                    e.disabled = true;

                    await h(e);

                    e.disabled = false;
                    busy = false;
                };

            return e;
        }
    }
}
