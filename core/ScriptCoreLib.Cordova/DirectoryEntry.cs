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
    /// This object represents a directory on a file system. It is defined in the W3C Directories and Systems specification.
    /// http://docs.phonegap.com/en/1.7.0/cordova_file_file.md.html#DirectoryEntry
    /// </summary>
    [Script(IsNative = true)]
    public class DirectoryEntry
    {
        #region Constructor

        public DirectoryEntry()
        {

        }

        #endregion

        #region PROPERTIES

        /// <summary>
        ///  Always false. (boolean)
        /// </summary>
        public bool isFile;

        /// <summary>
        ///  Always true. (boolean)
        /// </summary>
        public bool isDirectory;

        /// <summary>
        ///  The name of the DirectoryEntry, excluding the path leading to it. (DOMString)
        /// </summary>
        public string name;

        /// <summary>
        ///  The full absolute path from the root to the DirectoryEntry. (DOMString)
        /// </summary>
        public string fullPath;

        /// <summary>
        ///  The file system on which the DirectoryEntry resides. (FileSystem)
        /// </summary>
        public FileSystem filesystem;


        #endregion

        #region Methods

        /// <summary>
        ///  Look up metadata about a directory.
        /// </summary>
        public void getMetadata(Action<Metadata> successCallback, Action<FileError> errorCallback) { }

        /// <summary>
        ///  Move a directory to a different location on the file system.
        /// </summary>
        public void moveTo(DirectoryEntry parent, string newName,Action<DirectoryEntry> successCallback, Action<FileError> errorCallback){}

        /// <summary>
        ///  Copy a directory to a different location on the file system.
        /// </summary>
        public void copyTo(DirectoryEntry parent, string newName, Action<DirectoryEntry> successCallback, Action<FileError> errorCallback) { }

        /// <summary>
        ///  Return a URI that can be used to locate a directory.
        /// </summary>
        public string toURI()
        {
            return default(string);
        }

        /// <summary>
        ///  Delete a directory. The directory must be empty.
        /// </summary>
        public void remove(Action successCallback, Action<FileError> errorCallback) { }

        /// <summary>
        ///  Look up the parent directory.
        /// </summary>
        public void getParent(Action<DirectoryEntry> successCallback, Action<FileError> errorCallback) { }

        /// <summary>
        ///  Create a new DirectoryReader that can read entries from a directory.
        /// </summary>
        public DirectoryReader createReader(Action<DirectoryEntry> successCallback, Action<FileError> errorCallback) 
        {
            return default(DirectoryReader);
        }

        /// <summary>
        ///  Create or look up a directory.
        /// </summary>
        public void getDirectory(string path, Flags options, Action<DirectoryEntry> successCallback, Action<FileError> errorCallback) { }

        /// <summary>
        ///  Create or look up a file.
        /// </summary>
        public void getFile(string path, Flags options, Action<FileEntry> successCallback, Action<FileError> errorCallback) { }

        /// <summary>
        ///  Delete a directory and all of its contents.
        /// </summary>
        public void removeRecursively(Action successCallback, Action<FileError> errorCallback){}

        #endregion
    }
}
