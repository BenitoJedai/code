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
using TestServiceWorkerVisualizedScreens;
using TestServiceWorkerVisualizedScreens.Design;
using TestServiceWorkerVisualizedScreens.HTML.Pages;
using System.Windows.Forms;
using ScriptCoreLib.JavaScript.BCLImplementation.System.Windows.Forms;
using System.Diagnostics;

namespace TestServiceWorkerVisualizedScreens
{
    public class ClientData
    {
        // postMessage forgets type info
        // atleast now we can cast and get the fields.

        // each tab needs its own identity. shall we use random until .client or .source becomes available?
        public int identity;

        public int screen_width;
        public int screen_height;

        public int window_screenLeft;
        public int window_screenTop;

        public int window_Width;
        public int window_Height;


        public bool closed;
    }

    static class __reinventingthewheel
    {
        // X:\jsc.svn\core\ScriptCoreLib.Query\Shared\Lambda\Lambda.ForEach.cs

        public static ScriptCoreLib.Shared.Lambda.BindingListWithEvents<T> AsEmptyListWithEvents<T>(this T template)
        {
            return new ScriptCoreLib.Shared.Lambda.BindingListWithEvents<T>(
                new System.ComponentModel.BindingList<T>()
            );

        }
    }


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
            #region serviceworker
            if (Native.serviceworker != null)
            {
                // since chrome does not yet tell us who the clients
                //are we need to collect the intel in user code?

                var clients = new { e = default(MessageEvent), edata = default(ClientData) }.AsEmptyListWithEvents();

                Native.serviceworker.onmessage += e =>
                {
                    var edata = (ClientData)e.data;

                    // if the data identity is already know, then its an update
                    // we will ignore current ports and reuse previous ones.

                    var known = clients.Source.FirstOrDefault(x => x.edata.identity == edata.identity);
                    if (known != null)
                    {
                        if (known.edata.window_screenLeft == edata.window_screenLeft)
                            if (known.edata.window_screenTop == edata.window_screenTop)
                                if (known.edata.closed == edata.closed)
                                {
                                    // discard as nop
                                    return;
                                }

                        Console.WriteLine(
                            "serviceworker.onmessage update " + new
                            {
                                edata.window_screenLeft,
                                edata.window_screenTop

                            }
                        );

                        known.edata.closed = edata.closed;
                        known.edata.window_screenLeft = edata.window_screenLeft;
                        known.edata.window_screenTop = edata.window_screenTop;
                        known.edata.window_Width = edata.window_Width;
                        known.edata.window_Height = edata.window_Height;

                        // data updated
                        // let everybody know

                        clients.Source.WithEach(
                            x =>
                            {
                                // let each know of this specific data item, not about their own data
                                x.e.postMessage(known.edata);

                            }
                        );


                        return;
                    }


                    // how could the as operator know if object is of a type
                    //var edata = e.data as ClientData;

                    // 48686ms serviceworker.onmessage {{ source = null, data = [object Object] }}

                    // tab is telling us something.

                    Console.WriteLine(
                        "serviceworker.onmessage " + new
                        {
                            edata.screen_width,
                            edata.screen_height

                        }
                    );

                    //e.postMessage("got it! " + new
                    //{
                    //    edata.screen_width,
                    //    edata.screen_height
                    //}
                    //);

                    // echo back what the tab told us
                    e.postMessage(edata);

                    // late to the party?
                    // let the newby know about the others.

                    clients.Source.WithEach(
                        n =>
                        {
                            e.postMessage(n.edata);
                        }
                    );

                    clients.Source.Add(new { e, edata });

                    clients.Added +=
                        (n, nindex) =>
                        {
                            Console.WriteLine(
                                "serviceworker.onmessage  clients.Added " + new
                                {
                                    nindex
                                }
                            );


                            // report that somebody joined?
                            e.postMessage(n.edata);
                        };

                };

                return;
            }
            #endregion



            // this does not work within shadow root, due to css use?
            //FormStyler.AtFormCreated = FormStylerLikeChrome.LikeChrome;

            // wont see it on black background
            //FormStyler.AtFormCreated = FormStylerLikeFloat.LikeFloat;

            new IHTMLAnchor { href = "chrome://serviceworker-internals", innerText = "chrome://serviceworker-internals" }.AttachToDocument();
            new IHTMLPre { "Opens the DevTools window for ServiceWorker on start for debugging." }.AttachToDocument();

            new IHTMLBreak { }.AttachToDocument();

            #region register
            if (Native.window.navigator.serviceWorker.controller == null)
            {
                Native.css.style.borderTop = "1em solid red";

                // we need to register!

                new { }.With(async delegate
                {
                    var sw = Stopwatch.StartNew();

                    new IHTMLPre { "service register!" }.AttachToDocument();

                    // should jsc do this automatically?
                    // how many test cases should be made to understand it?
                    var activated = await Native.window.navigator.serviceWorker.activate();


                    new IHTMLPre { "service activated! " + new { sw.ElapsedMilliseconds } }.AttachToDocument();

                    Native.css.style.borderTop = "1em solid yellow";

                    new IHTMLButton { "reload to become controlled client " }.AttachToDocument().onclick +=
                         delegate
                         {
                             // is this something jsc app should do automatically?
                             Native.document.location.reload();

                         };
                }
                );



                return;
            }
            else
            {
                Native.css.style.borderTop = "1em solid green";

                new IHTMLPre { "service as controller!" }.AttachToDocument();
            }
            #endregion





            // we need to get roslyn compiler to work for scriptcorelib windows forms.

            // dual 4k is to be the max for visualization?
            // shall we use templates to bring the point across?


            // based on 
            // X:\jsc.svn\examples\javascript\Test\TestServiceWorkerFetchHTML\TestServiceWorkerFetchHTML\Application.cs
            // X:\jsc.svn\examples\javascript\Test\TestIScreen\TestIScreen\Application.cs
            // X:\jsc.svn\examples\javascript\test\TestServiceWorkerVisualizedScreens\TestServiceWorkerVisualizedScreens\Application.cs
            // X:\jsc.svn\examples\javascript\Test\TestServiceWorkerScreens\TestServiceWorkerScreens\Application.cs

            // jsc, how do the templates work again, i forgot?

            //Native.tem

            //Native.shadow

            var desktop = new IHTMLDiv();

            new IStyle(desktop)
            {
                backgroundColor = "black",


                // need to glue it
                position = IStyle.PositionEnum.absolute,
                top = "0px",
                right = "0px",
                bottom = "0px",
                left = "0px",

                // no scrollbars. thanks
                // how will this work for android multiscreeners?
                overflow = IStyle.OverflowEnum.hidden
            };

            //__Form.

            //Native.shadow = desktop;

            desktop.AttachTo(Native.shadow);
            //Native.shadow

            // actully the offset and scale.
            // screen0 as background should be there as another element.


            var offsetandscale = new IHTMLDiv();

            new IStyle(offsetandscale)
            {
                // the viewport info?

                //backgroundColor = "darkcyan",
                backgroundColor = "gray",


                // need to glue it
                position = IStyle.PositionEnum.absolute,


                // both screens should be able to fit here
                top = "100px",
                left = "100px",

                width = "600px",
                height = "600px",


                transformOrigin = "0% 0%",
                transform = "scale(0.3)"
            };

            offsetandscale.AttachTo(desktop);

            var screen0 = new IHTMLDiv();

            new IStyle(screen0)
            {
                // the viewport info?

                backgroundColor = "darkcyan",


                // need to glue it
                position = IStyle.PositionEnum.absolute,


                // both screens should be able to fit here
                top = "0px",
                left = "0px",

                width = "600px",
                height = "600px",
            };

            screen0.AttachTo(offsetandscale);


            // why would it be a good idea to maximize?
            //new Form().AttachFormTo(desktop);


            //namespace ScriptCoreLib.JavaScript.Extensions
            // 
            // where are they defined?
            //new Form().AttachControlTo(desktop);




            var data = new ClientData
            {
                identity = new Random().Next(),


                screen_width = Native.screen.width,
                screen_height = Native.screen.height,

                //Native.window.aspect,

                window_Width = Native.window.Width,
                window_Height = Native.window.Height,

                // where is this window on current screen?
                //(Native.window as dynamic).offsetLeft,
                //(Native.window as dynamic).offsetTop,

                window_screenLeft = (Native.window as dynamic).screenLeft,
                window_screenTop = (Native.window as dynamic).screenTop,

                // if we were to update, mutate this object,
                // how would we distribute the knowledge?
                // with sync events?
            };


            var f = new Form
            {
                // frame0
                Text = new { data.window_screenLeft, data.window_screenTop }.ToString(),

                Left = data.window_screenLeft,
                Top = data.window_screenTop

            };

            f.GetHTMLTarget().AttachTo(offsetandscale);

            var fcontent = new IHTMLContent { select = "body" };
            fcontent.AttachTo(f.GetHTMLTargetContainer());

            f.Show();


            #region Toggle
            Action Toggle =
                delegate
                {
                    if (desktop.parentNode == null)
                    {
                        // show setup mode again
                        Native.shadow.replaceChild(
                            desktop, Native.shadow.firstChild
                        );
                    }
                    else
                    {
                        // remove the screen setup mode
                        desktop.Orphanize();
                        new IHTMLContent { }.AttachTo(Native.shadow);
                    }
                };

            Native.document.onkeyup +=
               e =>
               {
                   // US
                   if (e.KeyCode == 222)
                   {
                       Toggle();
                   }
                   // EE
                   if (e.KeyCode == 192)
                   {
                       Toggle();
                   }
               };
            #endregion


            var lookup = new Dictionary<int, Form> { { data.identity, f } };



            new IHTMLPre { "lets tell the service, we have opened a new tab. " }.AttachToDocument();

            #region postMessage
            Native.window.navigator.serviceWorker.controller.postMessage(
                data,

                // data updated o the other side.
                // lets decode.
                m =>
                {
                    var mdata = (ClientData)m.data;


                    if (!lookup.ContainsKey(mdata.identity))
                    {
                        var ff = new Form { Text = new { mdata.identity }.ToString() };

                        ff.GetHTMLTarget().AttachTo(offsetandscale);
                        ff.Show();
                        ff.Opacity = 0.5;

                        lookup[mdata.identity] = ff;
                    }

                    {
                        var ff = lookup[mdata.identity];

                        ff.Left = mdata.window_screenLeft;
                        ff.Top = mdata.window_screenTop;

                        ff.Width = mdata.window_Width;
                        ff.Height = mdata.window_Height;

                        if (mdata.closed)
                        {
                            ff.Close();
                        }
                    }


                }
           );
            #endregion



            // keep it up to date

            #region onframe
            Native.window.onframe +=
                delegate
                {
                    if (data.closed)
                        return;




                    data.screen_width = Native.screen.width;
                    data.screen_height = Native.screen.height;

                    //Native.window.aspect,

                    data.window_Width = Native.window.Width;
                    data.window_Height = Native.window.Height;

                    // where is this window on current screen?
                    //(Native.window as dynamic).offsetLeft,
                    //(Native.window as dynamic).offsetTop,

                    data.window_screenLeft = (Native.window as dynamic).screenLeft;
                    data.window_screenTop = (Native.window as dynamic).screenTop;



                    // keep it in center

                    offsetandscale.style.transform = "scale(" + ((data.window_Width * 0.5) / (data.screen_width + 200)) + ")";



                    offsetandscale.style.left = (data.window_Width / 2) + "px";
                    //offsetandscale.style.top = (Native.window.Height / 2) + "px";

                    // assume our monitors are side by side?
                    offsetandscale.style.top = (data.window_Height / 4) + "px";

                    #region screen0
                    // what happens if we move to the other monitor?
                    screen0.style.SetSize(
                        data.screen_width,
                        data.screen_height
                    );

                    if (data.window_screenLeft < -(data.window_Width / 2))
                    {
                        // assume we are on the other monitor to the left?
                        // we do not know the actual offset until we go fullscreen.

                        screen0.style.SetLocation(
                           -data.screen_width,
                           0
                       );
                    }
                    else
                    {
                        screen0.style.SetLocation(
                           0,
                           0
                       );
                    }
                    #endregion


                    //f.Text = new { data.window_screenLeft, data.window_screenTop }.ToString();

                    //f.Left = data.window_screenLeft;
                    //f.Top = data.window_screenTop;

                    //f.Width = data.window_Width;
                    //f.Height = data.window_Height;



                    // resend data
                    Native.window.navigator.serviceWorker.controller.postMessage(data);
                };
            #endregion


            Native.window.onbeforeunload +=
                //Native.window.onunload +=
                delegate
                {

                    // move out of view to signify being closed?
                    data.closed = true;


                    // resend data
                    Native.window.navigator.serviceWorker.controller.postMessage(data);
                };

        }

    }
}
