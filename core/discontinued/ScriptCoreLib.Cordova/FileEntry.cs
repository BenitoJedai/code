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
    /// This object represents a file on a file system. It is defined in the W3C Directories and Systems specification
    /// http://docs.phonegap.com/en/1.7.0/cordova_file_file.md.html#FileEntry
    /// </summary>
    [Script(IsNative = true)]
    public class FileEntry
    {
        #region Constructor

        public FileEntry()
        {

        }

        #endregion

        #region PROPERTIES

        /// <summary>
        ///  Always true. (boolean)
        /// </summary>
        public bool isFile;

        /// <summary>
        ///  Always false. (boolean)
        /// </summary>
        public bool isDirectory;

        /// <summary>
        ///  The name of the FileEntry, excluding the path leading to it. (DOMString)
        /// </summary>
        public string name;

        /// <summary>
        ///  The full absolute path from the root to the FileEntry. (DOMString)
        /// </summary>
        public string fullPath;

        /// <summary>
        ///  The file system on which the FileEntry resides. (FileSystem)
        /// </summary>
        public FileSystem filesystem;

        #endregion

        #region Methods

        /// <summary>
        ///  Look up metadata about a file.
        /// </summary>
        public void getMetadata(Action<Metadata> successCallback, Action<FileError> errorCallback) { }

        /// <summary>
        ///  Move a file to a different location on the file system.
        /// </summary>
        public void moveTo(DirectoryEntry parent, string newName, Action<FileEntry> successCallback, Action<FileError> errorCallback) { }

        /// <summary>
        ///  Copy a file to a different location on the file system.
        /// </summary>
        public void copyTo(DirectoryEntry parent, string newName, Action<FileEntry> successCallback, Action<FileError> errorCallback) { }

        /// <summary>
        ///  Return a URI that can be used to locate a file.
        /// </summary>
        public string toURI()
        {
            return default(string);
        }

        /// <summary>
        ///  Delete a file.
        /// </summary>
        public void remove(Action successCallback, Action<FileError> errorCallback) { }

        /// <summary>
        ///  Look up the parent directory.
        /// </summary>
        public void getParent(Action<DirectoryEntry> successCallback, Action<FileError> errorCallback) { }

        /// <summary>
        ///  Creates a FileWriter object that can be used to write to a file.
        /// </summary>
        public void createWriter(Action<FileWriter> successCallback, Action<FileError> errorCallback) { }

        /// <summary>
        ///  Creates a File object containing file properties.
        /// </summary>
        public void file(Action<File> successCallback, Action<FileError> errorCallback) { }

        #endregion




    }
}
