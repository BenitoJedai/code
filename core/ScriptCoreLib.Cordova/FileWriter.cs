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
    /// FileWriter is an object that allows one to write a file
    /// http://docs.phonegap.com/en/1.7.0/cordova_file_file.md.html#FileWriter
    /// </summary>
    [Script(IsNative = true)]
    public class FileWriter
    {
        #region Constructor

        public FileWriter(FileEntry file)
        {

        }

        #endregion

        #region ENUMS

        public enum READY_STATES {
            INIT = 0,
            WRITING = 1,
            DONE = 2
        };

        #endregion

        #region PROPERTIES

        /// <summary>
        ///  One of the three states the reader can be in INIT, WRITING or DONE.
        /// </summary>
        READY_STATES readyState;

        /// <summary>
        ///  The name of the file to be written. (DOMString)
        /// </summary>
        public string fileName;

        /// <summary>
        ///  The length of the file to be written. (long)
        /// </summary>
        public long length;

        /// <summary>
        ///  The current position of the file pointer. (long)
        /// </summary>
        public long position;

        /// <summary>
        ///  An object containing errors. (FileError)
        /// </summary>
        public FileError error;

        /// <summary>
        ///  Called when the write starts. . (Function)
        /// </summary>
        public Action onwritestart;

        /// <summary>
        ///  Called while writing the file, reports progress (progess.loaded/progress.total). (Function) -NOT SUPPORTED
        /// </summary>
        public Action onprogress;

        /// <summary>
        ///  Called when the request has completed successfully. (Function)
        /// </summary>
        public Action onwrite;

        /// <summary>
        ///  Called when the write has been aborted. For instance, by invoking the abort() method. (Function)
        /// </summary>
        public Action onabort;

        /// <summary>
        ///  Called when the write has failed. (Function)
        /// </summary>
        public Action onerror;

        /// <summary>
        ///  Called when the request has completed (either in success or failure). (Function)
        /// </summary>
        public Action onwriteend;


        #endregion

        #region Methods 

        /// <summary>
        ///  Aborts writing file.
        /// </summary>
        public void abort(){}

        /// <summary>
        ///  Moves the file pointer to the byte specified.
        /// </summary>
        public void seek(int offset){}

        /// <summary>
        ///  Shortens the file to the length specified.
        /// </summary>
        public void truncate(){}

        /// <summary>
        ///  Writes data to the file with a UTF-8 encoding.
        /// </summary>
        public void write(string text){}

        #endregion
    }
}
