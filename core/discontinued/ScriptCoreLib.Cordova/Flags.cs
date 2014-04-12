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
    /// This object is used to supply arguments to the DirectoryEntry getFile and getDirectory methods, which look up or create files and directories, respectively.
    /// http://docs.phonegap.com/en/1.7.0/cordova_file_file.md.html#Flags
    /// </summary>
    [Script(IsNative = true)]
    public class Flags
    {
        #region Constructor

        public Flags(bool create=false, bool exclusive=false)
        {

        }

        #endregion

        #region PROPERTIES

        /// <summary>
        ///  Used to indicate that the file or directory should be created, if it does not exist. (boolean)
        /// </summary>
        public bool create;

        /// <summary>
        ///  By itself, exclusive has no effect. Used with create, it causes the file or directory creation to fail if the target path already exists. (boolean)
        /// </summary>
        public bool exclusive;

        #endregion


    }
}
