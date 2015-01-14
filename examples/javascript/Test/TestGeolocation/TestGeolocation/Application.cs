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
using TestGeolocation;
using TestGeolocation.Design;
using TestGeolocation.HTML.Pages;

namespace TestGeolocation
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
            // http://w3c-webmob.github.io/wake-lock-use-cases/

            new { }.With(
                async delegate
                {



                    // {{ longitude = 24.6980294, latitude = 59.4060572 }}

                    var a = await Native.window.navigator.geolocation.getCurrentPosition();


                    new IHTMLPre
                    {
                        new { a.coords.longitude, a.coords.latitude }

                    }.AttachToDocument();

                }
                );
        }

    }
}
