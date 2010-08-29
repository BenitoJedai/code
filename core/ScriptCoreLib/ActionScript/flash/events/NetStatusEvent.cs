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
        [Script]
        public class dynamic
        {
            // to be replaced by C# dynamic keyword at a later point?
            // http://www.adobe.com/livedocs/flash/9.0/ActionScriptLangRefV3/flash/events/NetStatusEvent.html#info

            public string code;
            public string level;

            public object message;
        }

        #region Properties
        /// <summary>
        /// An object with properties that describe the object's status or error condition.
        /// </summary>
        public dynamic info { get; set; }

        #endregion

        #region Constants
        /// <summary>
        /// [static] Defines the value of the type property of a netStatus event object.
        /// </summary>
        public static readonly string NET_STATUS = "netStatus";

        #endregion

    }
}
