using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ScriptCoreLib.ActionScript.flash.events
{
    // http://livedocs.adobe.com/flex/3/langref/flash/events/NetStatusEvent.html
    [Script(IsNative=true)]
    public class NetStatusEvent : Event
    {
        #region Properties
        /// <summary>
        /// An object with properties that describe the object's status or error condition.
        /// </summary>
        public object info { get; set; }

        #endregion

        #region Constants
        /// <summary>
        /// [static] Defines the value of the type property of a netStatus event object.
        /// </summary>
        public static readonly string NET_STATUS = "netStatus";

        #endregion

    }
}
