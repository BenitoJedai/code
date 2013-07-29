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
using com.abstractatech.battery.Design;
using com.abstractatech.battery.HTML.Pages;
using ScriptCoreLib.JavaScript.Runtime;

namespace com.abstractatech.battery
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
            if (Native.window.parent != Native.window.self)
            {
                Native.document.body.style.backgroundColor = JSColor.Transparent;
            }


            Action<double> set =
                a =>
                {
                    var deg = a * 80 - 80;

                    if (a <= 0.3)
                        page.gauge_layer3.Show();
                    else
                        page.gauge_layer3.Hide();


                    var transform = "rotate(" + deg + "deg)";

                    Console.WriteLine(new { a, transform });

                    page.gauge_layer1.style.transform = transform;
                };

#if DEBUG
            set(0.3);

            Native.Window.onfocus +=
                delegate
                {
                    set(1);

                };

            Native.Window.onblur +=
                delegate
                {
                    set(0);

                };
#else
            // it can get stuck. the dom might not represent the value we are setting if it is the same?
            //set(1);
            set(0);

            // will this animate our popup?
            (page.gauge_layer1.style as dynamic).webkitTransition = "-webkit-transform 0.7s ease-in";
            //(page.gauge_layer1.style as dynamic).transition = "-webkit-transform 0.7s ease-in";


            Action batteryStatus = delegate
            {
                service.batteryStatus(
                    batteryPct =>
                    {
                        set(System.Convert.ToDouble(batteryPct));
                    }
                );
            };


            new ScriptCoreLib.JavaScript.Runtime.Timer(
                delegate
                {
                    batteryStatus();
                }
            ).StartInterval(15000);

            batteryStatus();
#endif

            "Battery".ToDocumentTitle();
        }

    }
}
