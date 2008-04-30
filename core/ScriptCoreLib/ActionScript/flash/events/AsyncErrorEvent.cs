using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ScriptCoreLib.ActionScript.flash.events
{
    // http://livedocs.adobe.com/flex/3/langref/flash/events/AsyncErrorEvent.html
    [Script(IsNative=true)]
    public class AsyncErrorEvent : ErrorEvent
    {
        #region Methods
        #endregion

        #region Constructors
        /// <summary>
        /// Creates an AsyncErrorEvent object that contains information about asyncError events.
        /// </summary>
        public AsyncErrorEvent(string type, bool bubbles, bool cancelable, string text, Error error)
            : base(type, bubbles, cancelable, text)
        {
        }

        /// <summary>
        /// Creates an AsyncErrorEvent object that contains information about asyncError events.
        /// </summary>
        public AsyncErrorEvent(string type, bool bubbles, bool cancelable, string text)
            : base(type, bubbles, cancelable, text)
        {
        }

        /// <summary>
        /// Creates an AsyncErrorEvent object that contains information about asyncError events.
        /// </summary>
        public AsyncErrorEvent(string type, bool bubbles, bool cancelable)
            : base(type, bubbles, cancelable)
        {
        }

        /// <summary>
        /// Creates an AsyncErrorEvent object that contains information about asyncError events.
        /// </summary>
        public AsyncErrorEvent(string type, bool bubbles)
            : base(type, bubbles)
        {
        }

        /// <summary>
        /// Creates an AsyncErrorEvent object that contains information about asyncError events.
        /// </summary>
        public AsyncErrorEvent(string type)
            : base(type)
        {
        }

        #endregion


        #region Properties
        /// <summary>
        /// The exception that was thrown.
        /// </summary>
        public Error error { get; set; }

        #endregion

        #region Constants
        /// <summary>
        /// [static] The AsyncErrorEvent.ASYNC_ERROR constant defines the value of the type property of an asyncError event object.
        /// </summary>
        public static readonly string ASYNC_ERROR = "asyncError";

        #endregion

    }
}
