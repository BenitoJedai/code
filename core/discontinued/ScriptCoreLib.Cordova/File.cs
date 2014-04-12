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
    /// This object contains attributes of a single file.
    /// http://docs.phonegap.com/en/1.7.0/cordova_file_file.md.html#File
    /// </summary>
    [Script(IsNative = true)]
    public class File
    {
        #region Constructor

        public File(string name=null, string fullPath=null, string type=null, IDate lastModifiedDate=null, int size=0)
        {

        }

        #endregion

        #region PROPERTIES

        /// <summary>
        ///  The name of the file. (DOMString)
        /// </summary>
        public string name; 

        /// <summary>
        ///  The full path of the file including the file name. (DOMString)
        /// </summary>
        public string fullPath;

        /// The mime type of the file. (DOMString)
        public string type;

        /// <summary>
        ///  The last time the file was modified. (Date)
        /// </summary>
        public IDate lastModifiedDate;

        /// <summary>
        ///  The size of the file in bytes. (long)
        /// </summary>
        public long size;

        #endregion

       
    }
}
