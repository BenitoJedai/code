using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ScriptCoreLib.ActionScript.flash.display;

namespace ScriptCoreLib.ActionScript.flash.events
{
    // http://livedocs.adobe.com/flash/9.0/ActionScriptLangRefV3/flash/events/FullScreenEvent.html
    [Script(IsNative = true)]
    public class ActivityEvent : Event
    {
        #region Constants
        /// <summary>
        /// [static] The ActivityEvent.ACTIVITY constant defines the value of the type property of an activity event object.
        /// </summary>
        public static readonly string ACTIVITY = "activity";

        #endregion


        #region Properties
        /// <summary>
        /// Indicates whether the device is activating (true) or deactivating (false).
        /// </summary>
        public bool activating { get; set; }

        #endregion




    }
}
