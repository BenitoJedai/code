using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ScriptCoreLib.ActionScript.flash.events
{
    // http://livedocs.adobe.com/flex/3/langref/flash/events/SecurityErrorEvent.html
    [Script(IsNative=true)]
    public class SecurityErrorEvent : ErrorEvent
    {
        #region Constants
        /// <summary>
        /// [static] The SecurityErrorEvent.SECURITY_ERROR constant defines the value of the type property of a securityError event object.
        /// </summary>
        public static readonly string SECURITY_ERROR = "securityError";

        #endregion

        #region Methods
        /// <summary>
        /// Creates a copy of the SecurityErrorEvent object and sets the value of each property to match that of the original.
        /// </summary>
        public Event clone()
        {
            return default(Event);
        }

        #endregion

        #region Constructors
        /// <summary>
        /// Creates an Event object that contains information about security error events.
        /// </summary>
        public SecurityErrorEvent(string type, bool bubbles, bool cancelable, string text, int id)
            : base(type, bubbles, cancelable, text, id)
        {
        }

        /// <summary>
        /// Creates an Event object that contains information about security error events.
        /// </summary>
        public SecurityErrorEvent(string type, bool bubbles, bool cancelable, string text)
            : base(type, bubbles, cancelable, text)
        {
        }

        /// <summary>
        /// Creates an Event object that contains information about security error events.
        /// </summary>
        public SecurityErrorEvent(string type, bool bubbles, bool cancelable)
            : base(type, bubbles, cancelable)
        {
        }

        /// <summary>
        /// Creates an Event object that contains information about security error events.
        /// </summary>
        public SecurityErrorEvent(string type, bool bubbles)
            : base(type, bubbles)
        {
        }

        /// <summary>
        /// Creates an Event object that contains information about security error events.
        /// </summary>
        public SecurityErrorEvent(string type)
            : base(type)
        {
        }

        #endregion

    }
}
