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


            // keep it up to date

            Native.window.onframe +=
                delegate
                {



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


                    // what happens if we move to the other monitor?
                    screen0.style.SetSize(
                        data.screen_width,
                        data.screen_height
                    );

                    if (data.window_screenLeft < -(data.window_Width / 2))
                    {
                        // assume we are on the other monitor to the left?

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

                    f.Text = new { data.window_screenLeft, data.window_screenTop }.ToString();

                    f.Left = data.window_screenLeft;
                    f.Top = data.window_screenTop;

                    f.Width = data.window_Width;
                    f.Height = data.window_Height;


                };
        }

    }
}
