using jDOSBoxAppletExperiment;
using jDOSBoxAppletExperiment.Design;
using jDOSBoxAppletExperiment.HTML.Pages;
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

namespace jDOSBoxAppletExperiment
{
    /// <summary>
    /// Your client side code running inside a web browser as JavaScript.
    /// </summary>
    public sealed class Application
    {
        public readonly ApplicationWebService service = new ApplicationWebService();

        /// <summary>
        /// This is a javascript application.
        /// </summary>
        /// <param name="page">HTML document rendered by the web server which can now be enhanced.</param>
        public Application(IApp page)
        {
            // Initialize ApplicationApplet
            var applet = new ApplicationApplet();

            applet.AttachAppletToDocument();

            page.Keyboard.onkeydown +=
                e =>
                {
                    e.preventDefault();
                    e.stopPropagation();


                    int KeyCode = e.KeyCode;
                    if (KeyCode == 13)
                        KeyCode = 10;
                    int KeyChar = KeyCode;

            

                    applet.__MainApplet_keyPressed("" + KeyCode, "" + KeyChar,
                        message => Native.window.alert(message)
                    );
                };

            page.Keyboard.onkeyup +=
                e =>
                {
                    e.preventDefault();
                    e.stopPropagation();

                    int KeyCode = e.KeyCode;
                    if (KeyCode == 13)
                        KeyCode = 10;
                    int KeyChar = KeyCode;



                    applet.__MainApplet_keyReleased("" + KeyCode, "" + KeyChar,
                        message => Native.window.alert(message)
                    );
                };

            page.Keyboard.onmousedown +=
                e =>
                {
                    page.Keyboard.requestPointerLock();

                };

            page.Keyboard.onmousemove +=
             e =>
             {
                 if (Native.Document.pointerLockElement == page.Keyboard)
                 {
                     var dx = e.movementX;
                     var dy = e.movementY;

                     applet.__MainApplet_mousemove("" + dx, "" + dy);

                 }
             };

            page.Keyboard.onmouseup +=
                e =>
                {
                    Native.Document.exitPointerLock();
                };

            @"Hello world".ToDocumentTitle();
            // Send data from JavaScript to the server tier
            service.WebMethod2(
                @"A string from JavaScript.",
                value => value.ToDocumentTitle()
            );
        }

    }
}
