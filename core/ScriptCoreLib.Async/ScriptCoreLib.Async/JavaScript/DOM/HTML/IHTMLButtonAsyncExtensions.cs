using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using ScriptCoreLib.Extensions;

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

        //[Obsolete("experimental, what about reentry? signal the previous via scope?")]
        public static IHTMLAnchor Historic(
            this IHTMLAnchor e,
            Action<HistoryScope<object>> yield,
            bool replace = false
            )
        {
            // X:\jsc.svn\examples\javascript\async\AsyncHistoricActivities\AsyncHistoricActivities\Application.cs

            // https://sites.google.com/a/jsc-solutions.net/backlog/knowledge-base/2013/201312/20131222-form

            //var url = "/#/" + e.innerText.Replace(" ", "+").ToLower();

            var url = "#";


            if (string.IsNullOrEmpty(e.href))
            {
                var z = e.innerText;

                url += "/" + z.Replace(" ", "+").ToLower().Trim();

                // enable new tab click
                // start from root
                e.href = "/" + url;
            }

            // X:\jsc.svn\core\ScriptCoreLib.Ultra.Library\ScriptCoreLib.Ultra.Library\Ultra\WebService\InternalGlobalExtensions.cs
            else
            {
                // reusing jsc server redirector
                // Historic enter. activate? { url = #/http://192.168.43.252:19360, length = 1, hash = #/fake-right }
                //Console.WriteLine(
                //    new { e.href, location = Native.document.location.href }
                //);

                // will this support offline reload?
                // { href = http://192.168.43.252:22188/#/zTop, location = http://192.168.43.252:22188/ }
                url += "/" + e.href.SkipUntilLastOrEmpty("/");
            }

            Console.WriteLine("Historic enter. activate? " + new { url, Native.window.history.length, Native.document.location.hash });



            e.onclick +=
                ev =>
                {
                    if (ev.MouseButton == IEvent.MouseButtonEnum.Left)
                    {

                        ev.preventDefault();

                        var xreplace = replace;

                        // reentry shall reload?
                        if (Native.document.location.hash == url)
                            xreplace = true;


                        Console.WriteLine("Historic onclick " + new { url, Native.window.history.length, Native.document.location.hash });

                        if (xreplace)
                        {

                            Native.window.history.replaceState(
                                  state: new object(),
                                  url: e.href,
                                // exlusive replace means current state will be forgotten
                                  exclusive: true,
                                  yield: yield
                              );
                        }
                        else
                        {
                            Native.window.history.pushState(
                                state: new object(),
                                url: e.href,
                                exclusive: true,
                                yield: yield
                            );
                        }
                    }
                };

            if (Native.document.location.hash == url)
            {
                //Console.WriteLine("activate after onpopstate!");

                //HistoryExtensions.yield(
                //    delegate
                //    {
                Console.WriteLine("activate! " + new { Native.document.location.hash, url });

                e.click();
                //    }
                //);

            }



            return e;
        }
    }
}
