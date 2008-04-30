using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ScriptCoreLib.ActionScript.flash.events
{
    // http://livedocs.adobe.com/flex/3/langref/flash/events/IOErrorEvent.html
    [Script(IsNative=true)]
    public class IOErrorEvent : ErrorEvent
    {
        #region Constants
        /// <summary>
        /// [static] Defines the value of the type property of an ioError event object.
        /// </summary>
        public static readonly string IO_ERROR = "ioError";

        #endregion

        #region Methods
        /// <summary>
        /// Creates a copy of the IOErrorEvent object and sets the value of each property to match that of the original.
        /// </summary>
        public Event clone()
        {
            return default(Event);
        }

        #endregion

        #region Constructors
        /// <summary>
        /// Creates an Event object that contains specific information about ioError events.
        /// </summary>
        public IOErrorEvent(string type, bool bubbles, bool cancelable, string text, int id)
            : base(type, bubbles, cancelable, text)
        {
        }

        /// <summary>
        /// Creates an Event object that contains specific information about ioError events.
        /// </summary>
        public IOErrorEvent(string type, bool bubbles, bool cancelable, string text)
            : base(type, bubbles, cancelable, text)
        {
        }

        /// <summary>
        /// Creates an Event object that contains specific information about ioError events.
        /// </summary>
        public IOErrorEvent(string type, bool bubbles, bool cancelable)
            : base(type, bubbles, cancelable)
        {
        }

        /// <summary>
        /// Creates an Event object that contains specific information about ioError events.
        /// </summary>
        public IOErrorEvent(string type, bool bubbles)
            : base(type, bubbles)
        {
        }

        /// <summary>
        /// Creates an Event object that contains specific information about ioError events.
        /// </summary>
        public IOErrorEvent(string type)
            : base(type)
        {
        }

        #endregion


    }
}
