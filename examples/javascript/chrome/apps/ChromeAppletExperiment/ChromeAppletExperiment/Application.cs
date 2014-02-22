using chrome;
using ChromeAppletExperiment;
using ChromeAppletExperiment.Design;
using ChromeAppletExperiment.HTML.Pages;
using java.applet;
using ScriptCoreLib;
using ScriptCoreLib.Delegates;
using ScriptCoreLib.Extensions;
using ScriptCoreLib.Java.Extensions;
using ScriptCoreLib.JavaScript;
using ScriptCoreLib.JavaScript.Components;
using ScriptCoreLib.JavaScript.DOM;
using ScriptCoreLib.JavaScript.DOM.HTML;
using ScriptCoreLib.JavaScript.Extensions;
using ScriptCoreLib.JavaScript.Windows.Forms;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;

namespace ChromeAppletExperiment
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
            // http://stackoverflow.com/questions/19625828/java-web-start-running-applications-by-unknown-publishers-will-be-blocked-fi

            // X:\jsc.svn\examples\java\synergy\jDOSBoxAppletWithWarcraft\jDOSBoxAppletWithWarcraft\Application.cs
            // http://download.java.net/jdk8/docs/technotes/guides/jweb/security/mixed_code.html
            // https://groups.google.com/forum/#!topic/jenkinsci-users/dLr_1LRucGA
            // http://stackoverflow.com/questions/18914650/can-you-sign-a-java-applet-but-keep-it-in-the-sandbox-not-give-it-full-access-t
            // https://www.duckware.com/tech/java-security-sandbox-tricked-into-granting-full-computer-access-to-applet.html
            // http://www.boardspace.net/BB/viewtopic.php?t=621

            #region ChromeTCPServer
            dynamic self = Native.self;
            dynamic self_chrome = self.chrome;
            object self_chrome_socket = self_chrome.socket;

            if (self_chrome_socket != null)
            {
                Console.WriteLine("FlashHeatZeeker shall run as a chrome app as server");

                //chrome.Notification.DefaultTitle = "Operation «Heat Zeeker»";
                //chrome.Notification.DefaultIconUrl = new HTML.Images.FromAssets.Preview().src;



                // https://sites.google.com/a/jsc-solutions.net/backlog/knowledge-base/2014/201402/20140221-war
                ChromeTCPServer.TheServer.Invoke(
                    AppSource.Text,

                    // what does this param do?
                    open:
                       async u =>
                       {

                           // this is messy. 
                           //await (Task)new Notification
                           //{
                           //    Message = new { u }.ToString()
                           //};

                           //new IWindow (

                           // make us a new chrome.tabs.Tab
                           //chrome.tabs.create(

                           Console.WriteLine("open window");
                           var w = Native.window.open(u);
                           Console.WriteLine("await window onload");
                           //await w;
                           //await w.async.onload;

                           //w.onload +=
                           //    async delegate
                           //    {

                           //        //w.document



                           //        Native.window.onmessage += e =>
                           //        {
                           //            new Notification
                           //            {
                           //                Message = "window.onmessage " + new { e.data }.ToString()
                           //            };
                           //        };

                           //        w.onmessage += e =>
                           //         {
                           //             new Notification
                           //             {
                           //                 Message = "w.onmessage " + new { e.data }.ToString()
                           //             };
                           //         };

                           //    //await Task.Delay()
                           //    Console.WriteLine("before postXElementAsync");
                           //    //var response = await w.postXElementAsync(new XElement("hello", "app to tab"));
                           //    var response = await w.postMessageAsync("app to tab as string");

                           //    Console.WriteLine("after postXElementAsync");

                           //    await (Task)new Notification
                           //    {
                           //        Message = "w.postXElementAsync " + new { response }.ToString()
                           //    };
                           //};

                       }
                );

                return;
            }
            #endregion

         
            // https://sites.google.com/a/jsc-solutions.net/backlog/knowledge-base/2014/201402/20140221-war

         
            //public readonly 
            ApplicationApplet applet = new ApplicationApplet();
            applet.AutoSizeAppletTo(page.ContentSize);
            applet.AttachAppletTo(page.Content);

            // http://stackoverflow.com/questions/19399944/warning-on-permissions-attribute-when-running-an-applet-with-jre-7u45

            new IStyle(
                new IHTMLAnchor { href = "view-source", target = "_blank", title = "save at a: for crx", innerText = "view-source" }.AttachToDocument()
            )
            {
                position = IStyle.PositionEnum.@fixed,
                zIndex = 1000,
                left = "1em",
                bottom = "1em"
            };


        }

    }
}
