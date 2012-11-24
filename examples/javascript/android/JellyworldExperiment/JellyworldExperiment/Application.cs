using ScriptCoreLib;
using ScriptCoreLib.Delegates;
using ScriptCoreLib.Extensions;
using ScriptCoreLib.JavaScript;
using ScriptCoreLib.JavaScript.Components;
using ScriptCoreLib.JavaScript.DOM;
using ScriptCoreLib.JavaScript.DOM.HTML;
using ScriptCoreLib.JavaScript.Extensions;
using System;
using System.Linq;
using System.Text;
using System.Xml.Linq;
using JellyworldExperiment.Design;
using JellyworldExperiment.HTML.Pages;

namespace JellyworldExperiment
{
    /// <summary>
    /// Your client side code running inside a web browser as JavaScript.
    /// </summary>
    public sealed class Application
    {
        /* 
         * for http://blog.sc5.fi/2012/11/announcing-finhtml5-a-day-jam-packed-with-inspiration-for-the-future/#comment-142
         * Lets create a new demo.
         * 01. First let's tell use the screen and window size.
         * 02. If the client is flash capable tell that we have a cam
         * 03. If the client is orientation capable tell that
         * 04. Commit to svn
         * 05. Wait anwsers from JellyworldExperiment.HardwareDetection
         */

        public readonly ApplicationWebService service = new ApplicationWebService();

        /// <summary>
        /// This is a javascript application.
        /// </summary>
        /// <param name="page">HTML document rendered by the web server which can now be enhanced.</param>
        public Application(IApp page)
        {
            new IHTMLDiv
            {
                innerText =
                    new
                    {
                        Native.Window.Width,
                        Native.Window.Height,
                    }.ToString()
            }.AttachToDocument();

            @"Hello world".ToDocumentTitle();
            // Send data from JavaScript to the server tier
            service.WebMethod2(
                @"A string from JavaScript.",
                value => value.ToDocumentTitle()
            );
        }

    }

    [Obsolete("Temporary workaround to enable multiple apps.")]
    public sealed class Application_HardwareDetection_Sprite : global::JellyworldExperiment.HardwareDetection.XApplicationSprite
    {

    }

    [Obsolete("Temporary workaround to enable multiple apps.")]
    public sealed class Application_HardwareDetection
    {
        //        CreateType:  JellyworldExperiment.HardwareDetection.ApplicationSprite
        //error: System.InvalidOperationException: Unable to change after type has been created.
        //   at System.Reflection.Emit.TypeBuilder.ThrowIfCreated()

        public readonly Application_HardwareDetection_Sprite sprite = new Application_HardwareDetection_Sprite();

        public Application_HardwareDetection(global::JellyworldExperiment.HardwareDetection.HTML.Pages.IApp page)
        {
            // Initialize ApplicationSprite
            sprite.AttachSpriteTo(page.Content);
        }
    }
}
