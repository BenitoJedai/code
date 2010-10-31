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
using LoadExternalFlashComponent.Components;
using LoadExternalFlashComponent.HTML.Pages;

namespace LoadExternalFlashComponent
{
    /// <summary>
    /// This type will run as JavaScript.
    /// </summary>
    internal sealed class Application
    {
        public readonly ApplicationWebService service = new ApplicationWebService();

        /// <summary>
        /// This is a javascript application.
        /// </summary>
        /// <param name="page">HTML document rendered by the web server which can now be enhanced.</param>
        public Application(IDefaultPage page)
        {
            // Initialize MySprite1
            var s = new MySprite1();

            s.Ready +=
                delegate
                {
                    new IHTMLDiv { innerText = "Ready!" }.AttachTo(page.Content);
                };

            s.VideoPlayerReady +=
               delegate
               {
                   new IHTMLDiv { innerText = "VideoPlayerReady!" }.AttachTo(page.Content);
               };

            s.Inspecting +=
                doc =>
                {
                    new IHTMLPre { innerText = doc.ToString() }.AttachTo(page.Content);
                };

            var LoadOdoSketch = new IHTMLButton("LoadOdoSketch").AttachTo(page.Content);

            LoadOdoSketch.onclick +=
                delegate
                {
                    s.LoadTargetContent();
                };

            new IHTMLButton("Load Player").AttachTo(page.Content).With(btn =>
            {

                btn.onclick +=
                    delegate
                    {
                        s.LoadTargetContent(
                            "http://www.youtube.com/apiplayer?version=3"

                        );
                    };
            });

            new IHTMLButton("Load Video 1").AttachTo(page.Content).With(btn =>
            {

                btn.onclick +=
                    delegate
                    {
                        s.loadVideoById();
                    };
            });

            new IHTMLButton("LoadVideo1").AttachTo(page.Content).With(btn =>
            {

                btn.onclick +=
                    delegate
                    {
                        s.LoadTargetContent(
                            "http://www.youtube.com/v/eK9lNtxH8-E?version=3"

                        );
                    };
            });

            var LoadVideo2 = new IHTMLButton("LoadVideo2").AttachTo(page.Content);

            LoadVideo2.onclick +=
                delegate
                {
                    s.LoadTargetContent(
                        "http://www.youtube.com/p/642A6CB03EFE7893?hl=en_GB&fs=1"
                    );
                };

            var playVideo = new IHTMLButton("playVideo").AttachTo(page.Content);

            playVideo.onclick +=
                delegate
                {
                    s.playVideo();
                };

            var Inspect = new IHTMLButton("Inspect").AttachTo(page.Content);

            Inspect.onclick +=
                delegate
                {
                    s.Inspect();
                };

            var Clean = new IHTMLButton("Clean").AttachTo(page.Content);

            Clean.onclick +=
                delegate
                {
                    s.Clean();
                };


            s.AttachSpriteTo(page.Content);
            @"Hello world".ToDocumentTitle();
            // Send data from JavaScript to the server tier
            service.WebMethod2(
                @"A string from JavaScript.",
                value => value.ToDocumentTitle()
            );
        }

    }
}
