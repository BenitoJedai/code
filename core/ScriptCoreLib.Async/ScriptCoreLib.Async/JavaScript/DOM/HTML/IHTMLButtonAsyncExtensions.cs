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

        [Obsolete("use button.async.onclick instead?")]
        public static TaskAwaiter<IHTMLButton> GetAwaiter(this IHTMLButton button)
        {
            var y = new TaskCompletionSource<IHTMLButton>();

            button.async.onclick.ContinueWith(
                t =>
                {
                    y.SetResult(button);
                }
            );

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

        [Obsolete("experimental, what about reentry? signal the previous via scope?")]
        public static IHTMLAnchor Historic(this IHTMLAnchor e, Action<HistoryScope<object>> yield)
        {
            e.onclick +=
                delegate
                {
                    Native.window.history.pushState(
                        state: new object(),
                        url: "/#/" + e.innerText.Replace(" ", "+").ToLower(),
                        yield: yield
                    );
                };


            return e;
        }
    }
}
