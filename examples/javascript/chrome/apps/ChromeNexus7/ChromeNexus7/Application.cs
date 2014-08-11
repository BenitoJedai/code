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
using ChromeNexus7;
using ChromeNexus7.Design;
using ChromeNexus7.HTML.Pages;
using System.Windows.Forms;
using ChromeNexus7.HTML.Images.FromAssets;

namespace ChromeNexus7
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
        public Application(global::WebGLNexus7.HTML.Pages.IApp page)
        {
            // https://sites.google.com/a/jsc-solutions.net/backlog/knowledge-base/2014/201408/20140811

            // https://chrome.google.com/webstore/developer/edit/hdebmjbiddbadmkcnjepnefgmlolgjdd
            // https://chrome.google.com/webstore/detail/hdebmjbiddbadmkcnjepnefgmlolgjdd/publish-delayed

            // X:\jsc.svn\examples\javascript\chrome\apps\ChromeGalaxyS\ChromeGalaxyS\Application.cs

            #region ChromeTCPServer
            dynamic self = Native.self;
            dynamic self_chrome = self.chrome;
            object self_chrome_socket = self_chrome.socket;

            if (self_chrome_socket != null)
            {
                //Console.WriteLine("FlashHeatZeeker shall run as a chrome app as server");

                chrome.Notification.DefaultTitle = "Nexus7";
                chrome.Notification.DefaultIconUrl = new x128().src;

                ChromeTCPServer.TheServerWithStyledForm.Invoke(
                    AppSource.Text,
                    //AtFormCreated: FormStyler.AtFormCreated
                    AtFormCreated: styler =>
                    {
                        // X:\jsc.svn\examples\javascript\Test\TestShadowFlipForForm\TestShadowFlipForForm\Application.cs

                        FormStylerLikeFloat.LikeFloat(styler);

                        #region TestShadowFlipForForm

                        // maybe thats why the forms animation dont kick in.
                        // we are on the wrong context without onframe 

                        //styler.Context.GetHTMLTarget().style.perspective = "900px";

                        //styler.Context.GetHTMLTarget().css.last[IHTMLElement.HTMLElementEnum.div].style.transform = "rotate3d(0, 1.0, 0, 33deg) scale(0.6);";


                        //var f = styler.Context;

                        //f.GetHTMLTarget().shadow.With(
                        //    async shadow =>
                        //                {
                        //                    var s = new TestShadowFlipForForm.HTML.Pages.ShadowLayoutManual().AttachTo(shadow);

                        //                    s.content.setAttribute("state", "animateout");

                        //                    //var s = new ShadowLayout().AttachTo(shadow);

                        //                    // shadow content needs to be boxed to the same size the element thinks
                        //                    // it has!
                        //                    s.content.style.SetSize(f.Width, f.Height);

                        //                    f.SizeChanged +=
                        //                        delegate
                        //                    {
                        //                        s.content.style.SetSize(f.Width, f.Height);
                        //                    };

                        //                    //f.Load +=
                        //                    //delegate
                        //                    //{
                        //                    //    Console.WriteLine(" f.Load ");
                        //                    //};

                        //                    // this wont work for chrome app ?
                        //                    //await Native.window.async.onframe;

                        //                    //await Task.Delay(1000 / 15);
                        //                    Console.WriteLine(" before delay animatein");

                        //                    // when shall we animate?
                        //                    //await Task.Delay(300);
                        //                    // when loaded?
                        //                    await Task.Delay(2300);

                        //                    Console.WriteLine(" before  animatein");
                        //                    s.content.setAttribute("state", "animatein");

                        //                    // Error creating WebGL context

                        //                    // window close seems to benefit from hide animation.
                        //                }
                        //);
                        #endregion
                },

                    transparentBackground: true,
                    resizable: false
                );

                return;
            }
            #endregion


            // 
            new WebGLNexus7.Application(page);
        }

    }
}
