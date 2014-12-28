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
                backgroundColor = "darkcyan",


                // need to glue it
                position = IStyle.PositionEnum.absolute,
                top = "0px",
                right = "0px",
                bottom = "0px",
                left = "0px",
            };

            //__Form.

            //Native.shadow = desktop;

            desktop.AttachTo(Native.shadow);
            //Native.shadow



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
                Text = new { data.window_screenLeft, data.window_screenTop }.ToString()



            };

            f.GetHTMLTarget().AttachTo(desktop);



            var c = new IHTMLContent { select = "body" };
            c.AttachTo(f.GetHTMLTargetContainer());


            f.Show();
        }

    }
}
