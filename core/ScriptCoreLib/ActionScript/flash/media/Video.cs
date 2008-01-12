using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ScriptCoreLib.ActionScript.flash.display;

namespace ScriptCoreLib.ActionScript.flash.media
{
    /// <summary>
    /// http://livedocs.adobe.com/flash/9.0/ActionScriptLangRefV3/flash/media/Video.html
    /// </summary>
    [Script(IsNative = true)]
    public class Video : DisplayObject
    {
        /// <summary>
        /// Specifies a video stream from a camera to be displayed within the boundaries of the Video object in the application.
        /// </summary>
        /// <param name="camera"></param>
        public void attachCamera(Camera camera)
        {
        }

        public Video()
        {

        }

        public Video(int width, int height)
        {

        }
    }
}
