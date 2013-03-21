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
using com.abstractatech.adminshell.chrome.Design;
using com.abstractatech.adminshell.chrome.HTML.Pages;

namespace com.abstractatech.adminshell.chrome
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
            new IHTMLButton { innerText = "Fake discovery event" }.AttachToDocument().WhenClicked(
                btn =>
                {
                    btn.Orphanize();

                    // ask for credentials for new ui

                    // Refused to load the script 'http://192.168.1.100:26385/a' 
                    // because it violates the following Content Security Policy 
                    // directive: "script-src 'unsafe-eval' 'self' ".

                    //                    ---------------------------
                    //Extension error
                    //---------------------------
                    //Could not load extension from 'A:\'. Invalid value for 'content_security_policy': Both 'script-src' and 'object-src' 
                    // directives must be specified (either explicitly, or implicitly 
                    // via 'default-src'), and both must whitelist only secure resources. You may include any of the following sources: "'self'", "'unsafe-eval'", "http://127.0.0.1", "http://localhost", or any "https://" or "chrome-extension://" origin. For more information, see http://developer.chrome.com/extensions/contentSecurityPolicy.html
                    //---------------------------
                    //OK   
                    //---------------------------


                    //var s = new IHTMLScript { src = "http://192.168.1.100:26385/a" };

                    //// http://stackoverflow.com/questions/538745/how-to-tell-if-a-script-tag-failed-to-load
                    //s.onload +=
                    //    delegate
                    //    {
                    //    };

                    //s.AttachToDocument();

                    var i = new IHTMLIFrame { src = "http://192.168.1.100:20498" }.AttachToDocument();
                    i.onload += delegate
                    {
                        Native.Document.title = new { i.src }.ToString();
                    };
                    i.style.width = "100%";
                    i.style.height = "100%";
                }
            );
            @"Hello world".ToDocumentTitle();

        }

    }
}
