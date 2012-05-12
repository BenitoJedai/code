using ScriptCoreLib.Shared;

using ScriptCoreLib.JavaScript.DOM.HTML;
using ScriptCoreLib.JavaScript.Runtime;
using ScriptCoreLib.JavaScript.Extensions;
using ScriptCoreLib.JavaScript;

using ScriptCoreLib.Shared.Drawing;
using System;


namespace ScriptCoreLib.Cordova
{
    /// <summary>
    /// Encapsulates a set of media capture parameters that a device supports.
    /// http://docs.phonegap.com/en/1.7.0/cordova_media_capture_capture.md.html#ConfigurationData
    /// </summary>
    [Script(IsNative = true)]
    public class ConfigurationData
    {
        #region Constructor

        public ConfigurationData()
        {

        }

        #endregion

        #region PROPERTIES

        /// <summary>
        /// type: The ASCII-encoded string in lower case representing the media type. (DOMString)
        /// </summary>
        public string type;
                
        /// <summary>
        /// height: The height of the image or video in pixels. 
        /// In the case of a sound clip, this attribute has value 0. (Number)
        /// </summary>
        public int height;

        /// <summary>
        /// width: The width of the image or video in pixels. 
        /// In the case of a sound clip, this attribute has value 0. (Number)
        /// </summary>
        public int width;

        #endregion


    }
}
