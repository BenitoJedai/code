using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ScriptCoreLib.ActionScript.flash.events;

namespace ScriptCoreLib.ActionScript.flash.media
{
    // http://livedocs.adobe.com/flash/9.0/ActionScriptLangRefV3/flash/media/Camera.html#getCamera()
    [Script(IsNative = true)]
    public class Camera : EventDispatcher
    {
        /// <summary>
        /// Returns a reference to a Camera object for capturing video.
        /// </summary>
        /// <returns></returns>
        public static Camera getCamera()
        {
            return default(Camera);
        }


        /// <summary>
        /// Sets the maximum amount of bandwidth per second or the required picture quality of the current outgoing video feed.
        /// </summary>
        /// <param name="bandwidth">Specifies the maximum amount of bandwidth that the current outgoing video feed can use, in bytes per second. To specify that Flash Player video can use as much bandwidth as needed to maintain the value of quality, pass 0 for bandwidth. The default value is 16384.</param>
        /// <param name="quality">An integer that specifies the required level of picture quality, as determined by the amount of compression being applied to each video frame. Acceptable values range from 1 (lowest quality, maximum compression) to 100 (highest quality, no compression). To specify that picture quality can vary as needed to avoid exceeding bandwidth, pass 0 for quality.</param>
        public void setQuality(int bandwidth, int quality)
        {
        }


    }
}
