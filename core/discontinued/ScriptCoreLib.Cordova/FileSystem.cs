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
    /// This object represents a file system.
    /// http://docs.phonegap.com/en/1.7.0/cordova_file_file.md.html#FileSystem
    /// </summary>
    [Script(IsNative = true)]
    public class FileSystem
    {
        #region Constructor

        public FileSystem()
        {

        }

        #endregion

        #region PROPERTIES

        /// <summary>
        ///  The name of the file system. (DOMString)
        /// </summary>
        public string name;

        /// <summary>
        ///  The root directory of the file system. (DirectoryEntry)
        /// </summary>
        public DirectoryEntry root;


        #endregion


    }
}
