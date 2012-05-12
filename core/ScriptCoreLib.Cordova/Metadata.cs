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
    /// This interface supplies information about the state of a file or directory.
    /// http://docs.phonegap.com/en/1.7.0/cordova_file_file.md.html#Metadata
    /// </summary>
    [Script(IsNative = true)]
    public class Metadata
    {
        #region Constructor

        public Metadata()
        {

        }

        #endregion

        #region PROPERTIES

        /// <summary>
        ///  This is the time at which the file or directory was last modified. (Date)
        /// </summary>
        IDate modificationTime;

        #endregion

    }
}
