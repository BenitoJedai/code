using ScriptCoreLib;
using ScriptCoreLib.Delegates;
using ScriptCoreLib.Extensions;
using ScriptCoreLib.JavaScript;
using ScriptCoreLib.JavaScript.Components;
using ScriptCoreLib.JavaScript.DOM;
using ScriptCoreLib.JavaScript.DOM.HTML;
using ScriptCoreLib.JavaScript.Extensions;
using ScriptCoreLib.JavaScript.Windows.Forms;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using CSSSearchUserFeedback;
using CSSSearchUserFeedback.Design;
using CSSSearchUserFeedback.HTML.Pages;
using ScriptCoreLib.Lambda;
using System.Diagnostics;

namespace ScriptCoreLib.JavaScript.Extensions
{
    public static class XCSSSearchUserFeedback
    {
        public static void WhenReadyToSearch(this IHTMLInput that, Func<Task> yield, Action<string> status = null)
        {
            // parameter rename.
            var page = new { searchbox = that, output = status };

            var hasFocus = false;

            page.searchbox.onfocus +=
                delegate
                {
                    hasFocus = true;
                };


            page.searchbox.onblur +=
                delegate
                {
                    hasFocus = false;
                    page.searchbox.value = "";
                };

            page.searchbox.With(
                async searchbox =>
                {
                    var onkeyup = searchbox.async.onkeyup;
                    var i = 0;
                    var v = searchbox.value;

                retry:
                    await onkeyup;

                    // listen for a reason to come back even if server is still busy
                    onkeyup = searchbox.async.onkeyup;

                    if (v == searchbox.value)
                        goto retry;

                    v = searchbox.value;

                    if (!hasFocus)
                    {
                        if (v == "")
                            goto retry;


                        var typer = Stopwatch.StartNew();
                        while (typer.ElapsedMilliseconds < 150)
                        {
                            status("keep typing... " + new { typer.ElapsedMilliseconds, v });
                            //await Native.window.requestAnimationFrameAsync;
                            await Native.window.async.onframe;
                            //await 100;

                            if (v != searchbox.value)
                                goto retry;
                        }
                    }

                    status("working... " + new { i, v });

                    i++;
                    // implementation code starts here
                    var ystopwatch = Stopwatch.StartNew();
                    var y = yield();

                    while (!y.IsCompleted)
                    {
                        status("working... " + new { ystopwatch.ElapsedMilliseconds, i, v });
                        //await Native.window.requestAnimationFrameAsync;
                        await Native.window.async.onframe;
                    }

                    await y;

                    status("");
                    goto retry;

                }
             );
        }
    }
}

namespace CSSSearchUserFeedback
{
    /// <summary>
    /// Your client side code running inside a web browser as JavaScript.
    /// </summary>
    public sealed class Application : ApplicationWebService
    {
        /// <summary>
        /// This is a javascript application.
        /// </summary>
        /// <param name="page">HTML document rendered by the web server which can now be enhanced.</param>
        public Application(IApp page)
        {
            new IStyle(page.searchbox.css | page.output.css).transition = "background-color 300ms linear, color 500ms linear";


            Action yield = delegate { };

            page.searchbox.WhenReadyToSearch(
                delegate
                {
                    yield();

                    //page.searchbox.style.color = "blue";

                    var o = this.SearchUsersByName(page.searchbox);

                    //var 
                    (page.searchbox.css - o < 300).style.color = "blue";

                    //page.searchbox.css[o].incomplete.style.color = "blue";

                    page.output = o;


                    // um conditional, yet only until next time around.
                    var css = (Native.document.documentElement.css + o)[page.searchbox, page.output];

                    yield +=
                        delegate
                        {
                            css.OrphanizeRule();
                        };


                    css.style.backgroundColor = "yellow";

                    // show we have a slow down
                    (page.searchbox.css - o > 300).style.color = "red";

                    // hide details if the event has not happened within 2000ms
                    (page.output.css - o > 600).style.color = "transparent";


                    //page.output[

                    return o;
                }
                , status: x => page.output.css.emptyText = x
            );


        }

    }
}
