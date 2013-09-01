using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ScriptCoreLib.JavaScript.DOM
{
    public class NavigatorInfo
    {
        //public void webkitGetUserMedia(object constraints, IFunction successCallback, IFunction errorCallback);

        [Script(DefineAsStatic = true)]
        public void getUserMedia(Action<LocalMediaStream> successCallback, bool video = true, bool audio = false, Action<NavigatorUserMediaError> errorCallback = null)
        {
            var f = (IFunction)new IFunction(
                "return navigator.getUserMedia || navigator.webkitGetUserMedia || navigator.mozGetUserMedia || navigator.msGetUserMedia;"
            ).apply(null);


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

    }

}
