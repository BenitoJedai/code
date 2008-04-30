using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ScriptCoreLib.ActionScript.flash.events
{
    // http://livedocs.adobe.com/flex/3/langref/flash/events/ProgressEvent.html
    [Script(IsNative=true)]
    public class ProgressEvent : Event
    {
        #region Properties
        /// <summary>
        /// The number of items or bytes loaded when the listener processes the event.
        /// </summary>
        public double bytesLoaded { get; set; }

        /// <summary>
        /// The total number of items or bytes that will be loaded if the loading process succeeds.
        /// </summary>
        public double bytesTotal { get; set; }

        #endregion

        #region Constants
        /// <summary>
        /// [static] Defines the value of the type property of a progress event object.
        /// </summary>
        public static readonly string PROGRESS = "progress";

        /// <summary>
        /// [static] Defines the value of the type property of a socketData event object.
        /// </summary>
        public static readonly string SOCKET_DATA = "socketData";

        #endregion

    }
}
