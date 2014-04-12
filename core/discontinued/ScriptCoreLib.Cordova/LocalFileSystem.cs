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
    /// This object provides a way to obtain root file systems
    /// http://docs.phonegap.com/en/1.7.0/cordova_file_file.md.html#LocalFileSystem
    /// </summary>
    [Script(IsNative = true)]
    public class LocalFileSystem
    {
        #region Constructor

        public LocalFileSystem()
        {

        }

        #endregion

        #region Constants

        /// <summary>
        ///  Used for storage with no guarantee of persistence.
        /// </summary>
        public static int TEMPORARY=0;

        /// <summary>
        ///  Used for storage that should not be removed by the user agent without application or user permission.
        /// </summary>
        public static int PERSISTENT= 1;

        public static int RESOURCE = 2;

        public static int APPLICATION = 3;

        #endregion

        #region Methods

        /// <summary>
        ///  Requests a filesystem. (Function)
        /// </summary>
        /// <param name="?"></param>
        /// <param name="?"></param>
        /// <param name="?"></param>
        /// <param name="?"></param>
        public void requestFileSystem(int type, int size, Action<FileSystem> successCallback, Action<FileError> errorCallback)
        {
        
        }

        /// <summary>
        /// resolveLocalFileSystemURI: Retrieve a DirectoryEntry or FileEntry using local URI.
        /// </summary>
        /// <param name="uri"></param>
        /// <param name="successCallback"></param>
        /// <param name="errorCallback"></param>
        public void resolveLocalFileSystemURI(string uri, Action<FileSystem> successCallback, Action<FileError> errorCallback)
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
