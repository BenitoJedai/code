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
using ScriptCoreLib.Lambda;

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
#if false
            #region ChromeTCPServer

            //<package id="Abstractatech.JavaScript.Forms.FloatStyler" version="1.0.0.0" targetFramework="net451" />
            //<package id="Chrome.Web.Server" version="1.0.0.0" targetFramework="net451" />
            //<package id="Chrome.Web.Server.StyledForm" version="1.0.0.0" targetFramework="net451" />
            //<package id="Chrome.Web.Store" version="1.0.0.0" targetFramework="net451" />

            dynamic self = Native.self;
            dynamic self_chrome = self.chrome;

            // E/Web Console(10478): Uncaught TypeError: Cannot read property 'socket' of undefined at http://212.53.104.124:9475/view-source:30869

            object self_chrome_socket = self_chrome.socket;

            if (self_chrome_socket != null)
            {
                //Console.WriteLine("FlashHeatZeeker shall run as a chrome app as server");

                chrome.Notification.DefaultTitle = "I9000 Battery";
                chrome.Notification.DefaultIconUrl = new HTML.Images.FromAssets.Preview512().src;

                ChromeTCPServer.TheServerWithStyledForm.Invoke(
                    AppSource.Text,
                    //AtFormCreated: FormStyler.AtFormCreated
                    AtFormCreated: System.Windows.Forms.FormStylerLikeFloat.LikeFloat,

                    transparentBackground: true,
                    resizable: false
                );

                return;
            }
            #endregion
#endif


            // works with chrome beta 38
            // canary 39 does not show up?
            // chrome web server not yet implementing web method calls.

            if (Native.window.parent != Native.window.self)
            {
                // app running in AppWindow?
                Native.document.body.style.backgroundColor = JSColor.Transparent;
            }


            Action<double, bool> set =
                (a, b) =>
                {
                    var deg = a * 80 - 80;
                    if (b)
                    {
                        // batteryStatusCheck { status = 3, chargePlug = 0, isCharging = false }
                        (page.gauge_layer3.style as dynamic).webkitFilter = "hue-rotate(-170deg)";
                        //(page.gauge_layer3.style as dynamic).webkitFilter = "hue-rotate(-170deg)";
                        page.gauge_layer3.Show();
                    }
                    else
                    {
                        // when can we do css[if b].filter = hue(170)?
                        // can we have a client side hue editor and record it for jsc store?
                        (page.gauge_layer3.style as dynamic).webkitFilter = "hue-rotate(0deg)";

                        if (a <= 0.3)
                            page.gauge_layer3.Show();
                        else
                            page.gauge_layer3.Hide();
                    }


                    var transform = "rotate(" + deg + "deg)";


                    //I/System.Console(10858): batteryStatusCheck { status = 2, chargePlug = 2, isCharging = true }
                    //I/System.Console(10858): { batteryStatus = Intent { act=android.intent.action.BATTERY_CHANGED flg=0x60000000 (has extras) }, isCharging = true }
                    //I/Web Console(10858): %c0:23994ms UploadValuesAsync { status = 204, responseType = arraybuffer } at http://212.53.104.124:19436/view-source:42435
                    //I/Web Console(10858): %c0:24002ms MemoryStream set Capacity { value = 12 } at http://212.53.104.124:19436/view-source:42435
                    //I/Web Console(10858): %c0:24006ms MemoryStream set Capacity { value = 30 } at http://212.53.104.124:19436/view-source:42435
                    //I/Web Console(10858): 0:24013ms { a = NaN, transform = rotate(NaNdeg) } at http://212.53.104.124:19436/view-source:42394

                    // why nAN?
                    Console.WriteLine(new { a, transform });

                    page.gauge_layer1.style.transform = transform;
                };


            // it can get stuck. the dom might not represent the value we are setting if it is the same?
            //set(1);
            set(0, false);

            // will this animate our popup?
            //(page.gauge_layer1.style as dynamic).webkitTransition = "-webkit-transform 0.7s ease-in";


            page.gauge_layer1.style.transition = "-webkit-transform 0.7s ease-in";
            //page.gauge_layer1.style.transition = "transform 0.7s ease-in";

            //(page.gauge_layer1.style as dynamic).transition = "-webkit-transform 0.7s ease-in";



            ((Action)(
                 async delegate
                 {
                     while (true)
                     {
                         await batteryStatusCheck();


                         set(batteryStatus, isCharging);

                         //await Task.Delay(500 + new Random().Next(5000));
                         await (500 + new Random().Next(5000));
                     }
                 }
             ))();

            "Battery".ToDocumentTitle();
        }

    }
}
