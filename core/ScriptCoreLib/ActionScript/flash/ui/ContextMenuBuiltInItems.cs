using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ScriptCoreLib.ActionScript.flash.ui
{
    // http://livedocs.adobe.com/flash/9.0/ActionScriptLangRefV3/flash/ui/ContextMenuBuiltInItems.html
    [Script(IsNative=true)]
    public sealed class ContextMenuBuiltInItems
    {
        #region Fields
        /// <summary>
        /// Lets the user move forward or backward one frame in a SWF file at run time (does not appear for a single-frame SWF file).
        /// </summary>
        public bool forwardAndBack = true;

        /// <summary>
        /// Lets the user set a SWF file to start over automatically when it reaches the final frame (does not appear for a single-frame SWF file).
        /// </summary>
        public bool loop = true;

        /// <summary>
        /// Lets the user start a paused SWF file (does not appear for a single-frame SWF file).
        /// </summary>
        public bool play = true;

        /// <summary>
        /// Lets the user send the displayed frame image to a printer.
        /// </summary>
        public bool print = true;

        /// <summary>
        /// Lets the user set the resolution of the SWF file at run time.
        /// </summary>
        public bool quality = true;

        /// <summary>
        /// Lets the user set a SWF file to play from the first frame when selected, at any time (does not appear for a single-frame SWF file).
        /// </summary>
        public bool rewind = true;

        /// <summary>
        /// Lets the user with Shockmachine installed save a SWF file.
        /// </summary>
        public bool save = true;

        /// <summary>
        /// Lets the user zoom in and out on a SWF file at run time.
        /// </summary>
        public bool zoom = true;

        #endregion

    }
}
