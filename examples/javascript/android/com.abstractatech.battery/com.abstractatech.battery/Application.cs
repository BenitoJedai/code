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
            Action<double> set =
                a =>
                {
                    var deg = a * 80 - 80;

                    if (a <= 0.3)
                        page.gauge_layer3.Show();
                    else
                        page.gauge_layer3.Hide();

                    page.gauge_layer1.style.transform = "rotate(" + deg + "deg)";
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
            set(0);

        Action batteryStatus = delegate
        {
            service.batteryStatus(
                batteryPct =>
                {
                    set(Convert.ToDouble(batteryPct));
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
