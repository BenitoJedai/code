using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ScriptCoreLib.ActionScript.flash.events
{
    // http://livedocs.adobe.com/flash/9.0/ActionScriptLangRefV3/flash/events/StatusEvent.html
    [Script(IsNative=true)]
    public class StatusEvent : Event
    {
        #region Properties
        /// <summary>
        /// A description of the object's status.
        /// </summary>
        public string code { get; set; }

        /// <summary>
        /// The category of the message, such as "status", "warning" or "error".
        /// </summary>
        public string level { get; set; }

        #endregion

        #region Constants
        /// <summary>
        /// [static] Defines the value of the type property of a status event object.
        /// </summary>
        public static readonly string STATUS = "status";

        #endregion

    }
}

