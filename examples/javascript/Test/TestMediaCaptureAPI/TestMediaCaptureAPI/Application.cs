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
        public Application(IDefault page)
        {
            // http://blog.teamtreehouse.com/accessing-the-device-camera-with-getusermedia
            // http://stackoverflow.com/questions/11539689/why-localstreams-contains-localmediastream-and-remotestreams-contains-mediastrea
            // https://developer.mozilla.org/en-US/docs/Web/API/Navigator.getUserMedia
            // http://neave.github.io/face-detection/


            var navigator = (NavigatorUserMedia)(object)Native.window.navigator;

            var successCallback =
                new Action<LocalMediaStream>(
                    localMediaStream =>
                    {
                        Console.WriteLine("got video");

                        //var src = (string)new IFunction("return window.URL.createObjectURL(this);").apply(localMediaStream);

                        var v = new IHTMLVideo { src = localMediaStream.ToObjectURL() }.AttachToDocument();

                        v.play();
                    }
                );

            var errorCallback =
                new Action<NavigatorUserMediaError>(
                    e =>
                    {
                        // restart on error? :P
                        Native.window.alert("no video: " + new { e.code });
                    }
                );

            //var a = 1;

            //var c = new MediaStreamConstraints { video = a == 1, audio = a == 0 };

            //var o = new IFunction("return {video: true};").apply(null);


            navigator.webkitGetUserMedia(
                new { video = true, audio = false },
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
