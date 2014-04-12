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
    /// An object that lists files and directories in a directory. Defined in the Directories and Systems specification
    /// http://docs.phonegap.com/en/1.7.0/cordova_file_file.md.html#DirectoryReader
    /// </summary>
    [Script(IsNative = true)]
    public class DirectoryReader
    {
        #region Constructor

        public DirectoryReader(string fullpath=null)
        {

        }

        #endregion

        #region METHODS

        /// <summary>
        /// 
        /// </summary>
        /// <param name="successCallback">
        ///     successCallback - A callback that is passed an array of FileEntry and DirectoryEntry objects. (Function)
        /// </param>
        /// <param name="errorCallback">
        ///     errorCallback - A callback that is called if an error occurs retrieving the directory listing. Invoked with a FileError object. (Function)
        /// </param>
        public void readEntries(Action<object[]> successCallback, Action<FileError> errorCallback){}


        #endregion


    }
}
