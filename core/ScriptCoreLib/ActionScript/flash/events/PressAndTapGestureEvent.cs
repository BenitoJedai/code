using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ScriptCoreLib.ActionScript.flash.events
{
    // http://help.adobe.com/en_US/FlashPlatform/beta/reference/actionscript/3/flash/events/PressAndTapGestureEvent.html
    [Script(IsNative = true)]
    public class PressAndTapGestureEvent : GestureEvent
    {
        #region Methods
        /// <summary>
        /// [override] Creates a copy of the PressAndTapGestureEvent object and sets the value of each property to match that of the original.
        /// </summary>
        public Event clone()
        {
            return default(Event);
        }

        #endregion

        #region Constructors
        /// <summary>
        /// Creates an Event object that contains information about complex multi-touch events, such as a user raising a context-sensitive popup menu.
        /// </summary>
        public PressAndTapGestureEvent(string type, bool bubbles, bool cancelable, string phase, double localX, double localY, double tapLocalX, double tapLocalY, bool ctrlKey, bool altKey, bool shiftKey, bool commandKey, bool controlKey)
            : base(type, bubbles, cancelable, phase, localX, localY)
        {
        }

        /// <summary>
        /// Creates an Event object that contains information about complex multi-touch events, such as a user raising a context-sensitive popup menu.
        /// </summary>
        public PressAndTapGestureEvent(string type, bool bubbles, bool cancelable, string phase, double localX, double localY, double tapLocalX, double tapLocalY, bool ctrlKey, bool altKey, bool shiftKey, bool commandKey)
            : base(type, bubbles, cancelable, phase, localX, localY)
        {
        }

        /// <summary>
        /// Creates an Event object that contains information about complex multi-touch events, such as a user raising a context-sensitive popup menu.
        /// </summary>
        public PressAndTapGestureEvent(string type, bool bubbles, bool cancelable, string phase, double localX, double localY, double tapLocalX, double tapLocalY, bool ctrlKey, bool altKey, bool shiftKey)
            : base(type, bubbles, cancelable, phase, localX, localY)
        {
        }

        /// <summary>
        /// Creates an Event object that contains information about complex multi-touch events, such as a user raising a context-sensitive popup menu.
        /// </summary>
        public PressAndTapGestureEvent(string type, bool bubbles, bool cancelable, string phase, double localX, double localY, double tapLocalX, double tapLocalY, bool ctrlKey, bool altKey)
            : base(type, bubbles, cancelable, phase, localX, localY)
        {
        }

        /// <summary>
        /// Creates an Event object that contains information about complex multi-touch events, such as a user raising a context-sensitive popup menu.
        /// </summary>
        public PressAndTapGestureEvent(string type, bool bubbles, bool cancelable, string phase, double localX, double localY, double tapLocalX, double tapLocalY, bool ctrlKey)
            : base(type, bubbles, cancelable, phase, localX, localY)
        {
        }

        /// <summary>
        /// Creates an Event object that contains information about complex multi-touch events, such as a user raising a context-sensitive popup menu.
        /// </summary>
        public PressAndTapGestureEvent(string type, bool bubbles, bool cancelable, string phase, double localX, double localY, double tapLocalX, double tapLocalY)
            : base(type, bubbles, cancelable, phase, localX, localY)
        {
        }

        /// <summary>
        /// Creates an Event object that contains information about complex multi-touch events, such as a user raising a context-sensitive popup menu.
        /// </summary>
        public PressAndTapGestureEvent(string type, bool bubbles, bool cancelable, string phase, double localX, double localY, double tapLocalX)
            : base(type, bubbles, cancelable, phase, localX, localY)
        {
        }

        /// <summary>
        /// Creates an Event object that contains information about complex multi-touch events, such as a user raising a context-sensitive popup menu.
        /// </summary>
        public PressAndTapGestureEvent(string type, bool bubbles, bool cancelable, string phase, double localX, double localY)
            : base(type, bubbles, cancelable, phase, localX, localY)
        {
        }

        /// <summary>
        /// Creates an Event object that contains information about complex multi-touch events, such as a user raising a context-sensitive popup menu.
        /// </summary>
        public PressAndTapGestureEvent(string type, bool bubbles, bool cancelable, string phase, double localX)
            : base(type, bubbles, cancelable, phase, localX)
        {
        }

        /// <summary>
        /// Creates an Event object that contains information about complex multi-touch events, such as a user raising a context-sensitive popup menu.
        /// </summary>
        public PressAndTapGestureEvent(string type, bool bubbles, bool cancelable, string phase)
            : base(type, bubbles, cancelable, phase)
        {
        }

        /// <summary>
        /// Creates an Event object that contains information about complex multi-touch events, such as a user raising a context-sensitive popup menu.
        /// </summary>
        public PressAndTapGestureEvent(string type, bool bubbles, bool cancelable)
            : base(type, bubbles, cancelable)
        {
        }

        /// <summary>
        /// Creates an Event object that contains information about complex multi-touch events, such as a user raising a context-sensitive popup menu.
        /// </summary>
        public PressAndTapGestureEvent(string type, bool bubbles)
            : base(type, bubbles)
        {
        }

        /// <summary>
        /// Creates an Event object that contains information about complex multi-touch events, such as a user raising a context-sensitive popup menu.
        /// </summary>
        public PressAndTapGestureEvent(string type)
            : base(type)
        {
        }

        #endregion


        #region Constants
        /// <summary>
        /// [static] Defines the value of the type property of a GESTURE_PRESS_AND_TAP touch event object.
        /// </summary>
        public static readonly string GESTURE_PRESS_AND_TAP = "gesturePressAndTap";

        #endregion


        #region Properties
        /// <summary>
        /// The horizontal coordinate at which the event occurred relative to the containing interactive object.
        /// </summary>
        public double tapLocalX { get; set; }

        /// <summary>
        /// The vertical coordinate at which the event occurred relative to the containing interactive object.
        /// </summary>
        public double tapLocalY { get; set; }

        /// <summary>
        /// [read-only] The horizontal coordinate at which the tap touch occurred in global Stage coordinates.
        /// </summary>
        public double tapStageX { get; private set; }

        /// <summary>
        /// [read-only] The vertical coordinate at which the tap touch occurred in global Stage coordinates.
        /// </summary>
        public double tapStageY { get; private set; }

        #endregion

    }
}
