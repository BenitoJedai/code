using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ScriptCoreLib.ActionScript.flash.events
{
    // http://livedocs.adobe.com/flex/3/langref/flash/events/ErrorEvent.html
    [Script(IsNative=true)]
    public class ErrorEvent : TextEvent
    {
        #region Methods
        /// <summary>
        /// Creates a copy of the ErrorEvent object and sets the value of each property to match that of the original.
        /// </summary>
        public Event clone()
        {
            return default(Event);
        }

        #endregion

        #region Constructors
        /// <summary>
        /// Creates an Event object that contains information about error events.
        /// </summary>
        public ErrorEvent(string type, bool bubbles, bool cancelable, string text, int id)
            : base(type, bubbles, cancelable, text)
        {
        }

        /// <summary>
        /// Creates an Event object that contains information about error events.
        /// </summary>
        public ErrorEvent(string type, bool bubbles, bool cancelable, string text)
            : base(type, bubbles, cancelable, text)
        {
        }

        /// <summary>
        /// Creates an Event object that contains information about error events.
        /// </summary>
        public ErrorEvent(string type, bool bubbles, bool cancelable)
            : base(type, bubbles, cancelable)
        {
        }

        /// <summary>
        /// Creates an Event object that contains information about error events.
        /// </summary>
        public ErrorEvent(string type, bool bubbles)
            : base(type, bubbles)
        {
        }

        /// <summary>
        /// Creates an Event object that contains information about error events.
        /// </summary>
        public ErrorEvent(string type)
            : base(type)
        {
        }

        #endregion



        #region Properties
        /// <summary>
        /// [read-only] Contains the reference number associated with the specific error.
        /// </summary>
        public int errorID { get; private set; }

        #endregion

        #region Constants
        /// <summary>
        /// [static] Defines the value of the type property of an error event object.
        /// </summary>
        public static readonly string ERROR = "error";

        #endregion

    }
}
