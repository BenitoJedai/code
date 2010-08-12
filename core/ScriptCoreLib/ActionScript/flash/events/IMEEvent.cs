using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ScriptCoreLib.ActionScript.Extensions.flash.text.ime;

namespace ScriptCoreLib.ActionScript.flash.events
{
    // http://help.adobe.com/en_US/FlashPlatform/beta/reference/actionscript/3/flash/events/IMEEvent.html
    [Script(IsNative = true)]
    public class IMEEvent : TextEvent
    {
        #region Properties
        /// <summary>
        /// Specifies an object that implements the IMEClient interface.
        /// </summary>
        public IIMEClient imeClient { get; set; }

        #endregion

        #region Constants
        /// <summary>
        /// [static] Defines the value of the type property of an imeComposition event object.
        /// </summary>
        public static readonly string IME_COMPOSITION = "imeComposition";

        /// <summary>
        /// [static] To handle IME text input, the receiver must set the imeClient field of the event to an object that implements the IIMEClient interface.
        /// </summary>
        public static readonly string IME_START_COMPOSITION = "imeStartComposition";

        #endregion

        #region Methods
        /// <summary>
        /// [override] Creates a copy of the IMEEvent object and sets the value of each property to match that of the original.
        /// </summary>
        public Event clone()
        {
            return default(Event);
        }

        #endregion

        #region Constructors
        /// <summary>
        /// Creates an Event object with specific information relevant to IME events.
        /// </summary>
        public IMEEvent(string type, bool bubbles, bool cancelable, string text, IIMEClient imeClient)
            : base(type, bubbles, cancelable, text)
        {
        }

        /// <summary>
        /// Creates an Event object with specific information relevant to IME events.
        /// </summary>
        public IMEEvent(string type, bool bubbles, bool cancelable, string text)
            : base(type, bubbles, cancelable, text)
        {
        }

        /// <summary>
        /// Creates an Event object with specific information relevant to IME events.
        /// </summary>
        public IMEEvent(string type, bool bubbles, bool cancelable)
            : base(type, bubbles, cancelable)
        {
        }

        /// <summary>
        /// Creates an Event object with specific information relevant to IME events.
        /// </summary>
        public IMEEvent(string type, bool bubbles)
            : base(type, bubbles)
        {
        }

        /// <summary>
        /// Creates an Event object with specific information relevant to IME events.
        /// </summary>
        public IMEEvent(string type)
            : base(type)
        {
        }

        #endregion

    }
}
