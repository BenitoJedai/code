using Abstractatech.ConsoleFormPackage.Library;
using Abstractatech.JavaScript.FormAsPopup;
using chrome;
using FlashHeatZeeker.PlayerIOIntegrationBeta2.Design;
using FlashHeatZeeker.PlayerIOIntegrationBeta2.HTML.Images.FromAssets;
using FlashHeatZeeker.PlayerIOIntegrationBeta2.HTML.Pages;
using ScriptCoreLib;
using ScriptCoreLib.ActionScript.flash.display;
using ScriptCoreLib.Delegates;
using ScriptCoreLib.Extensions;
using ScriptCoreLib.JavaScript;
using ScriptCoreLib.JavaScript.BCLImplementation.System.Windows.Forms;
using ScriptCoreLib.JavaScript.Components;
using ScriptCoreLib.JavaScript.DOM;
using ScriptCoreLib.JavaScript.DOM.HTML;
using ScriptCoreLib.JavaScript.Extensions;
using ScriptCoreLib.JavaScript.Runtime;
using ScriptCoreLib.JavaScript.Windows.Forms;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;

namespace FlashHeatZeeker.PlayerIOIntegrationBeta2
{
    /// <summary>
    /// Your client side code running inside a web browser as JavaScript.
    /// </summary>
    public sealed class Application
    {

        //FlashHeatZeeker.PlayerIOIntegrationBeta2.HTML.Pages.TexturesImages
        //rewriting... done
        //2584:02:01 RewriteToAssembly error: System.ArgumentException: The unmanaged Version information is too large to persist.
        //   at System.Reflection.Emit.AssemblyBuilder.CreateVersionInfoResource(
        // String filename, String title, String iconFilename, String description, St
        // jsc how much description are you merging?


        // crx
        // https://chrome.google.com/webstore/detail/operation-heat-zeeker/iiabebggdceojiejhopnopmbkgandhha?hl=et&utm_source=chrome-ntp-launcher
        // use non chrome to download without install
        // http://clients2.google.com/service/update2/crx?response=redirect&x=id%3Diiabebggdceojiejhopnopmbkgandhha%26uc%26lang%3Den-US&prod=chrome
        // Download interrupted
        // means servers are not yet in sync, wait some
        // pbaaphpbkehboammnlcihpemkkimdfgo
        // http://clients2.google.com/service/update2/crx?response=redirect&x=id%3Dpbaaphpbkehboammnlcihpemkkimdfgo%26uc%26lang%3Den-US&prod=chrome
        // error-unknownApplication
        // Error 404

        //public readonly ApplicationWebService service = new ApplicationWebService();

        // https://groups.google.com/a/chromium.org/forum/#!msg/chromium-extensions/3sXvdfb5qk8/iMteKuIawcUJ
        // An error occurred: Failed to process your item.
        // background subsection of app section is not supported.

        // * New packaged apps are currently able to be searched and browsed in the Chrome Web Store by Windows and Chrome OS users on Chrome's developer channel. Users on other OSs and Chrome channels can view and install the app via a direct link.


        /// <summary>
        /// This is a javascript application.
        /// </summary>
        /// <param name="page">HTML document rendered by the web server which can now be enhanced.</param>
        public Application(IApp page)
        {
            // https://code.google.com/p/chromium/issues/detail?can=2&start=0&num=100&q=&colspec=ID%20Pri%20M%20Iteration%20ReleaseBlock%20Cr%20Status%20Owner%20Summary%20OS%20Modified&groupby=&sort=&id=313673
            // https://code.google.com/p/chromium/issues/detail?can=2&start=0&num=100&q=&colspec=ID%20Pri%20M%20Iteration%20ReleaseBlock%20Cr%20Status%20Owner%20Summary%20OS%20Modified&groupby=&sort=&id=317352&thanks=317352&ts=1384094600

            // current way
            //FormStyler.AtFormCreated = s =>
            //{
            //    // X:\jsc.svn\examples\javascript\IsometricTycoonViewWithToolbar\IsometricTycoonViewWithToolbar\Application.cs
            //    // X:\jsc.internal.svn\core\com.abstractatech.web\com.abstractatech.web\Domains\discover.xavalon.net\discover_xavalon_net.cs

            //    // browser popup will use this color
            //    ((__Form)s.Context).HTMLTargetContainerRef.style.backgroundColor = JSColor.FromRGB(154, 108, 70);

            //    s.Caption.style.backgroundColor = JSColor.FromRGB(154, 108, 70);
            //    s.TargetOuterBorder.style.boxShadow = "rgba(154, 108, 70, 0.3) 0px 0px 6px 3px";
            //    s.TargetOuterBorder.style.borderColor = JSColor.FromRGB(154, 108, 70);

            //    s.TargetInnerBorder.style.borderWidth = "0px";

            //    s.CloseButton.style.color = JSColor.White;
            //    s.CloseButton.style.backgroundColor = JSColor.None;
            //    s.CloseButton.style.borderWidth = "0px";
            //    s.CloseButtonContent.style.borderWidth = "0px";

            //    s.TargetResizerPadding.style.left = "0px";
            //    s.TargetResizerPadding.style.top = "0px";
            //    s.TargetResizerPadding.style.right = "0px";
            //    s.TargetResizerPadding.style.bottom = "0px";

            //};


            // new way

            FormStyler.AtFormCreated =
                 s =>
                 {
                     s.Context.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;

                     //var x = new ChromeTCPServerWithFrameNone.HTML.Pages.AppWindowDrag().AttachTo(s.Context.GetHTMLTarget());
                     var x = new ChromeTCPServerWithFrameNone.HTML.Pages.AppWindowDragWithShadow().AttachTo(s.Context.GetHTMLTarget());
                 };



            #region ChromeTCPServer
            dynamic self = Native.self;
            dynamic self_chrome = self.chrome;
            object self_chrome_socket = self_chrome.socket;

            if (self_chrome_socket != null)
            {
                Console.WriteLine("FlashHeatZeeker shall run as a chrome app as server");

                chrome.Notification.DefaultTitle = "Operation «Heat Zeeker»";
                chrome.Notification.DefaultIconUrl = new HTML.Images.FromAssets.Preview().src;

                ChromeTCPServer.TheServerWithStyledForm.Invoke(
                    AppSource.Text,
                    AtFormCreated: FormStyler.AtFormCreated
                );

                return;
            }
            #endregion



            // Your item is in the process of being published and may take up to 60 minutes to appear in the Chrome Web Store. 
            // https://chrome.google.com/webstore/detail/cpcgbahhjcobocclaolehaffhpfonofe/publish-delayed



            var sprite = new ApplicationSprite();



            sprite.AttachSpriteToDocument().With(
                   embed =>
                   {

                       // show thumbnail
                       // or click to enable plugin?
                       embed.style.SetLocation(0, 0);
                       embed.style.SetSize(Native.window.Width, 96);

                       //embed.style.transition = "height 200ms linear";

                       sprite.WhenReady(
                           async delegate
                           {

                               embed.style.SetSize(Native.window.Width, Native.window.Height);


                               //embed.style.transition = "";


                               Native.window.onresize +=
                                   delegate
                                   {
                                       embed.style.SetSize(Native.window.Width, Native.window.Height);
                                   };

                               // this will not work if offline or lan only
                               // chrome webview needs to allow new window
                               // in order to log in. { loginstatus = unknown }

#if FFACEBOOK
                               var user = await FaceBook.API.Spy.GetCurrentUser();

                               new TitleElement { }.AttachToDocument().Container.innerText = user.name;
#endif


                               //new IHTMLDiv { innerText = user.name }.AttachToDocument().style.SetLocation(64, 8);


                           }
                       );
                   }
               );

            // https://sites.google.com/a/jsc-solutions.net/backlog/knowledge-base/2013/201310/20131029-nuget
            // wont work yet

            //IStyleSheet.all["*"].style.setProperty(
            //    //"cursor", "url('" + new _3dgarro().src + "'), auto", ""
            //    "cursor", "url('" + new cursor().src + "'), auto", ""
            //);


            "Operation «Heat Zeeker»".ToDocumentTitle();

            //try
            //{
            //    //Console.WriteLine(new { chrome.app.isInstalled });

            //}
            //catch
            //{
            //    Console.WriteLine("error, not in chrome?");
            //}

        }

    }


}
