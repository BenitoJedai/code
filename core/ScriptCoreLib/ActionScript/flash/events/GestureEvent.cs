using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ScriptCoreLib.ActionScript.flash.events
{
    // http://help.adobe.com/en_US/FlashPlatform/beta/reference/actionscript/3/flash/events/GestureEvent.html
    [Script(IsNative = true)]
    public class GestureEvent : Event
    {
        #region Properties
        /// <summary>
        /// Indicates whether the Alt key is active (true) or inactive (false).
        /// </summary>
        public bool altKey { get; set; }

        /// <summary>
        /// Indicates whether the Control key is activated on Mac and whether the Ctrl key is activated on Windows or Linux.
        /// </summary>
        public bool controlKey { get; set; }

        /// <summary>
        /// On Windows or Linux, indicates whether the Ctrl key is active (true) or inactive (false).
        /// </summary>
        public bool ctrlKey { get; set; }

        /// <summary>
        /// The horizontal coordinate at which the event occurred relative to the containing sprite.
        /// </summary>
        public double localX { get; set; }

        /// <summary>
        /// The vertical coordinate at which the event occurred relative to the containing sprite.
        /// </summary>
        public double localY { get; set; }

        /// <summary>
        /// A value from the GesturePhase class indicating the progress of the touch gesture.
        /// </summary>
        public string phase { get; set; }

        /// <summary>
        /// Indicates whether the Shift key is active (true) or inactive (false).
        /// </summary>
        public bool shiftKey { get; set; }

        /// <summary>
        /// [read-only] The horizontal coordinate at which the event occurred in global Stage coordinates.
        /// </summary>
        public double stageX { get; private set; }

        /// <summary>
        /// [read-only] The vertical coordinate at which the event occurred in global Stage coordinates.
        /// </summary>
        public double stageY { get; private set; }

        #endregion

        #region Constants
        /// <summary>
        /// [static] Defines the value of the type property of a GESTURE_TWO_FINGER_TAP gesture event object.
        /// </summary>
        public static readonly string GESTURE_TWO_FINGER_TAP = "gestureTwoFingerTap";

        #endregion

        #region Methods
        /// <summary>
        /// [override] Creates a copy of the GestureEvent object and sets the value of each property to match that of the original.
        /// </summary>
        public Event clone()
        {
            return default(Event);
        }

        /// <summary>
        /// Refreshes the Flash runtime display after processing the gesture event, in case the display list has been modified by the event handler.
        /// </summary>
        public void updateAfterEvent()
        {
        }

        #endregion

        #region Constructors
        /// <summary>
        /// Creates an Event object that contains information about multi-touch events (such as pressing two fingers on a touch screen at the same time).
        /// </summary>
        public GestureEvent(string type, bool bubbles, bool cancelable, string phase, double localX, double localY, bool ctrlKey, bool altKey, bool shiftKey, bool commandKey, bool controlKey)
        {
        }

        /// <summary>
        /// Creates an Event object that contains information about multi-touch events (such as pressing two fingers on a touch screen at the same time).
        /// </summary>
        public GestureEvent(string type, bool bubbles, bool cancelable, string phase, double localX, double localY, bool ctrlKey, bool altKey, bool shiftKey, bool commandKey)
        {
        }

        /// <summary>
        /// Creates an Event object that contains information about multi-touch events (such as pressing two fingers on a touch screen at the same time).
        /// </summary>
        public GestureEvent(string type, bool bubbles, bool cancelable, string phase, double localX, double localY, bool ctrlKey, bool altKey, bool shiftKey)
        {
        }

        /// <summary>
        /// Creates an Event object that contains information about multi-touch events (such as pressing two fingers on a touch screen at the same time).
        /// </summary>
        public GestureEvent(string type, bool bubbles, bool cancelable, string phase, double localX, double localY, bool ctrlKey, bool altKey)
        {
        }

        /// <summary>
        /// Creates an Event object that contains information about multi-touch events (such as pressing two fingers on a touch screen at the same time).
        /// </summary>
        public GestureEvent(string type, bool bubbles, bool cancelable, string phase, double localX, double localY, bool ctrlKey)
        {
        }

        /// <summary>
        /// Creates an Event object that contains information about multi-touch events (such as pressing two fingers on a touch screen at the same time).
        /// </summary>
        public GestureEvent(string type, bool bubbles, bool cancelable, string phase, double localX, double localY)
        {
        }

        /// <summary>
        /// Creates an Event object that contains information about multi-touch events (such as pressing two fingers on a touch screen at the same time).
        /// </summary>
        public GestureEvent(string type, bool bubbles, bool cancelable, string phase, double localX)
        {
        }

        /// <summary>
        /// Creates an Event object that contains information about multi-touch events (such as pressing two fingers on a touch screen at the same time).
        /// </summary>
        public GestureEvent(string type, bool bubbles, bool cancelable, string phase)
        {
        }

        /// <summary>
        /// Creates an Event object that contains information about multi-touch events (such as pressing two fingers on a touch screen at the same time).
        /// </summary>
        public GestureEvent(string type, bool bubbles, bool cancelable)
        {
        }

        /// <summary>
        /// Creates an Event object that contains information about multi-touch events (such as pressing two fingers on a touch screen at the same time).
        /// </summary>
        public GestureEvent(string type, bool bubbles)
        {
        }

        /// <summary>
        /// Creates an Event object that contains information about multi-touch events (such as pressing two fingers on a touch screen at the same time).
        /// </summary>
        public GestureEvent(string type)
        {
        }

        #endregion

    }
}
