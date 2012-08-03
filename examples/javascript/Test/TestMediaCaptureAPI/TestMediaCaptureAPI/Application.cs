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
using TestMediaCaptureAPI.Design;
using TestMediaCaptureAPI.HTML.Pages;
using ScriptCoreLib.JavaScript.Media;

namespace TestMediaCaptureAPI
{
    sealed class MediaStreamConstraints
    {
        public bool video;
        public bool audio;
    }

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
        public Application(IDefaultPage page)
        {
            var navigator = (NavigatorUserMedia)(object)Native.Window.navigator;

            var successCallback =
                new Action<MediaStream>(
                    stream =>
                    {
                        Native.Window.alert("got video");
                    }
                );

            var errorCallback =
                new Action<NavigatorUserMediaError>(
                    e =>
                    {
                        // restart on error? :P
                        Native.Window.alert("no video: " + new { e.code });
                    }
                );

            var a = 1;

           var c= new MediaStreamConstraints  { video = a == 1, audio = a == 0  };

           var o = new IFunction("return {video: true};").apply(null);


            navigator.webkitGetUserMedia(
                o,
                successCallback: IFunction.OfDelegate(
                    successCallback
                ),
                errorCallback: IFunction.OfDelegate(
                    errorCallback
                )
            );

            @"Hello world".ToDocumentTitle();
            // Send data from JavaScript to the server tier
            service.WebMethod2(
                @"A string from JavaScript.",
                value => value.ToDocumentTitle()
            );
        }

        
    }
}
