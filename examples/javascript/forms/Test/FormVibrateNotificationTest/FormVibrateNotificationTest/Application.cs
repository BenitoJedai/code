using FormVibrateNotificationTest;
using FormVibrateNotificationTest.Design;
using FormVibrateNotificationTest.HTML.Pages;
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
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;

namespace FormVibrateNotificationTest
{
    /// <summary>
    /// Your client side code running inside a web browser as JavaScript.
    /// </summary>
    public sealed class Application : ApplicationWebService
    {
        public readonly ApplicationControl content = new ApplicationControl();

        /// <summary>
        /// This is a javascript application.
        /// </summary>
        /// <param name="page">HTML document rendered by the web server which can now be enhanced.</param>
        public Application(IApp page)
        {
            content.AttachControlToDocument();

            Action flash = async delegate
            {
                var v = new Stopwatch();
                v.Start();

                Console.WriteLine("Vibration start");

                while (v.ElapsedMilliseconds < 1500)
                {
                    content.panel1.GetHTMLTargetContainer().style.marginLeft = ((v.ElapsedMilliseconds % 3) - 1) + "px";

                    await Native.window.requestAnimationFrameAsync;
                }
                Console.WriteLine("Vibration end");
            };

            content.button1.Click += delegate
            {
                Console.WriteLine("Button press");
                flash();
            };
            new IStyle()
            {
                
            };
        }

    }
}
