using ScriptCoreLib.Shared;

using ScriptCoreLib.JavaScript.DOM.HTML;
using ScriptCoreLib.JavaScript.Runtime;
using ScriptCoreLib.JavaScript.Extensions;
using ScriptCoreLib.JavaScript;

using ScriptCoreLib.Shared.Drawing;
using System;
using ScriptCoreLib.JavaScript.DOM;


namespace ScriptCoreLib.Cordova
{
    /// <summary>
    /// Encapsulates properties of a media capture file
    /// http://docs.phonegap.com/en/1.7.0/cordova_media_capture_capture.md.html#MediaFile
    /// </summary>
    [Script(IsNative = true)]
    public class MediaFile
    {
        #region Constructor

        public MediaFile(string name=null, string fullPath=null, string type=null, IDate lastModifiedDate=null, int size=0)
        {

        }

        #endregion

        #region PROPERTIES

        /// <summary>
        ///  The name of the file, without path information. (DOMString)
        /// </summary>
        public string name;

        /// <summary>
        ///  The full path of the file, including the name. (DOMString)
        /// </summary>
        public string fullPath;

        /// <summary>
        ///  The mime type (DOMString)
        /// </summary>
        public string type;

        /// <summary>
        ///  The date and time that the file was last modified. (Date)
        /// </summary>
        public IDate lastModifiedDate;

        /// <summary>
        ///  The size of the file, in bytes. (Number)
        /// </summary>
        public int size;

        #endregion

        #region Methods

        public void getFormatData(Action<Metadata> successCallback, Action errorCallback)
        { }

        #endregion

    }
}
