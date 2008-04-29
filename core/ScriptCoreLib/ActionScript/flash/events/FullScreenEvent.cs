using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ScriptCoreLib.ActionScript.flash.display;

namespace ScriptCoreLib.ActionScript.flash.events
{
    // http://livedocs.adobe.com/flash/9.0/ActionScriptLangRefV3/flash/events/FullScreenEvent.html
    [Script(IsNative = true)]
    public class FullScreenEvent : ActivityEvent
    {
        #region Constants
        /// <summary>
        /// [static] The FullScreenEvent.FULL_SCREEN constant defines the value of the type property of a fullScreen event object.
        /// </summary>
        public static readonly string FULL_SCREEN = "fullScreen";

        #endregion


        #region Properties
        /// <summary>
        /// [read-only] Indicates whether the Stage object is in full-screen mode (true) or not (false).
        /// </summary>
        public bool fullScreen { get; private set; }

        #endregion



    }
}
