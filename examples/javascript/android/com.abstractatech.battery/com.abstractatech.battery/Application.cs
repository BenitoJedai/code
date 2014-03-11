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
using System.Threading.Tasks;

namespace com.abstractatech.battery
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
            if (Native.window.parent != Native.window.self)
            {
                Native.document.body.style.backgroundColor = JSColor.Transparent;
            }


            Action<double, bool> set =
                (a, b) =>
                {
                    var deg = a * 80 - 80;
                    if (b)
                    {
                        (page.gauge_layer3.style as dynamic).webkitFilter = "hue-rotate(70deg)";
                        page.gauge_layer3.Show();
                    }
                    else if (a <= 0.3)
                        page.gauge_layer3.Show();
                    else
                        page.gauge_layer3.Hide();


                    var transform = "rotate(" + deg + "deg)";

                    Console.WriteLine(new { a, transform });

                    page.gauge_layer1.style.transform = transform;
                };


            // it can get stuck. the dom might not represent the value we are setting if it is the same?
            //set(1);
            set(0, false);

            // will this animate our popup?
            (page.gauge_layer1.style as dynamic).webkitTransition = "-webkit-transform 0.7s ease-in";
            //(page.gauge_layer1.style as dynamic).transition = "-webkit-transform 0.7s ease-in";


            ((Action)(
                 async delegate
                 {
                     while (true)
                     {
                         await batteryStatusCheck();


                         set(batteryStatus, isCharging);

                         await Task.Delay(500 + new Random().Next(5000));
                     }
                 }
             ))();

            "Battery".ToDocumentTitle();
        }

    }
}
