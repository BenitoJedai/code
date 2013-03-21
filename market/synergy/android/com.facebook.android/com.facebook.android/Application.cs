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
using com.facebook.android.Design;
using com.facebook.android.HTML.Pages;

namespace com.facebook.android
{
    /// <summary>
    /// Your client side code running inside a web browser as JavaScript.
    /// </summary>
    public sealed class Application
    {
        // !
        // "C:\util\android-sdk-windows\platform-tools\aapt.exe"  package -v -f -m -S X:\opensource\github\facebook-android-sdk\facebook\res -M "X:\opensource\github\facebook-android-sdk\facebook\AndroidManifest.xml" -I "C:\util\android-sdk-windows\platforms\android-16\android.jar" -J X:\jsc.svn\market\synergy\android\com.facebook.android\com.facebook.android


        public readonly ApplicationWebService service = new ApplicationWebService();

        /// <summary>
        /// This is a javascript application.
        /// </summary>
        /// <param name="page">HTML document rendered by the web server which can now be enhanced.</param>
        public Application(IApp page)
        {
            @"Hello world".ToDocumentTitle();
            // Send data from JavaScript to the server tier
            service.WebMethod2(
                @"A string from JavaScript.",
                value => value.ToDocumentTitle()
            );
        }

    }
}
