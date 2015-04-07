extern alias core;

using ScriptCoreLib.JavaScript.DOM.HTML;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScriptCoreLib.JavaScript.DOM
{
    public class NavigatorInfo
    {
		//public void webkitGetUserMedia(object constraints, IFunction successCallback, IFunction errorCallback);

        [Script(DefineAsStatic = true)]
        public void getUserMedia(
            Action<LocalMediaStream> successCallback,
            bool video = true,
            bool audio = false,
            Action<NavigatorUserMediaError> errorCallback = null)
        {
			// X:\jsc.svn\examples\javascript\async\test\TestGetUserMedia\TestGetUserMedia\Application.cs

			var f = (IFunction)new IFunction(
                "return navigator.getUserMedia || navigator.webkitGetUserMedia || navigator.mozGetUserMedia || navigator.msGetUserMedia;"
            ).apply(null);

            // Failed to execute 'webkitGetUserMedia' on 'Navigator': The callback provided as parameter 3 is not a function.


            if (errorCallback == null)
            {
                errorCallback =
                    err =>
                    {
                        // X:\jsc.svn\market\javascript\Abstractatech.JavaScript.Avatar\Abstractatech.JavaScript.Avatar\Application.cs
                        Console.WriteLine(new { err.code });
                    };
            }


            f.apply(Native.window.navigator,
              new { video, audio },
              IFunction.OfDelegate(
                  successCallback
              ),
              IFunction.OfDelegate(
                  errorCallback
              )
            );

            //navigator.getUserMedia_ = navigator.getUserMedia || navigator.webkitGetUserMedia || navigator.mozGetUserMedia || navigator.msGetUserMedia;
        }

        // X:\jsc.svn\market\javascript\Abstractatech.JavaScript.Avatar\Abstractatech.JavaScript.Avatar\Application.cs

        #region async
        [Script]
        public new class Tasks
        // <TElement> where TElement : IHTMLElement
        {
            internal NavigatorInfo that;

            [System.Obsolete("should jsc expose events as async tasks until C# chooses to allow that?")]
            public virtual Task<IHTMLVideo> onvideo
            {
                [Script(DefineAsStatic = true)]
                get
                {
                    var x = new TaskCompletionSource<IHTMLVideo>();

                    // tested by
                    // X:\jsc.svn\market\javascript\Abstractatech.JavaScript.Avatar\Abstractatech.JavaScript.Avatar\Application.cs


                    that.getUserMedia(
                        successCallback:
                         stream =>
                         {
                             var src = stream.ToObjectURL();

                             var v = new IHTMLVideo { src = src };
                             x.SetResult(v);

                             // http://stackoverflow.com/questions/11642926/stop-close-webcam-which-is-opened-by-navigator-getusermedia

                             var w = (core::ScriptCoreLib.JavaScript.DOM.IWindow)(object)Native.window;

                             w.onframe +=
                                 delegate
                                 {
                                     if (stream == null)
                                         return;

                                     if (v.src == src)
                                         return;

									 // how else could media steam be used, webrtc?
                                     stream.stop();
                                     stream = null;
                                 };
                         }
                       );

                    return x.Task;
                }
            }


        }

        [System.Obsolete("is this the best way to expose events as async?")]
        public new Tasks async
        {
            [Script(DefineAsStatic = true)]
            get
            {
                return new Tasks { that = this };
            }
        }
        #endregion
    }

}
