using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ScriptCoreLib.ActionScript.flash.events
{
    // http://livedocs.adobe.com/flex/3/langref/flash/events/HTTPStatusEvent.html
    [Script(IsNative=true)]
    public class HTTPStatusEvent : Event
    {
        #region Properties
        /// <summary>
        /// [read-only] The HTTP status code returned by the server.
        /// </summary>
        public int status { get; private set; }

        #endregion

        #region Constants
        /// <summary>
        /// [static] The HTTPStatusEvent.HTTP_STATUS constant defines the value of the type property of a httpStatus event object.
        /// </summary>
        public static readonly string HTTP_STATUS = "httpStatus";

        #endregion

    }
}
