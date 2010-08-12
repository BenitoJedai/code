using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ScriptCoreLib.ActionScript.flash.display;

namespace ScriptCoreLib.ActionScript.flash.events
{
    // http://help.adobe.com/en_US/FlashPlatform/beta/reference/actionscript/3/flash/events/TouchEvent.html
    [Script(IsNative = true)]
    public class TouchEvent : Event
    {
        #region Constants
        /// <summary>
        /// [static] Defines the value of the type property of a TOUCH_BEGIN touch event object.
        /// </summary>
        public static readonly string TOUCH_BEGIN = "touchBegin";

        /// <summary>
        /// [static] Defines the value of the type property of a TOUCH_END touch event object.
        /// </summary>
        public static readonly string TOUCH_END = "touchEnd";

        /// <summary>
        /// [static] Defines the value of the type property of a TOUCH_MOVE touch event object.
        /// </summary>
        public static readonly string TOUCH_MOVE = "touchMove";

        /// <summary>
        /// [static] Defines the value of the type property of a TOUCH_OUT touch event object.
        /// </summary>
        public static readonly string TOUCH_OUT = "touchOut";

        /// <summary>
        /// [static] Defines the value of the type property of a TOUCH_OVER touch event object.
        /// </summary>
        public static readonly string TOUCH_OVER = "touchOver";

        /// <summary>
        /// [static] Defines the value of the type property of a TOUCH_ROLL_OUT touch event object.
        /// </summary>
        public static readonly string TOUCH_ROLL_OUT = "touchRollOut";

        /// <summary>
        /// [static] Defines the value of the type property of a TOUCH_ROLL_OVER touch event object.
        /// </summary>
        public static readonly string TOUCH_ROLL_OVER = "touchRollOver";

        /// <summary>
        /// [static] Defines the value of the type property of a TOUCH_TAP touch event object.
        /// </summary>
        public static readonly string TOUCH_TAP = "touchTap";

        #endregion


        #region Methods
        #endregion

        #region Constructors
        /// <summary>
        /// Creates an Event object that contains information about touch events.
        /// </summary>
        public TouchEvent(string type, bool bubbles, bool cancelable, int touchPointID, bool isPrimaryTouchPoint, double localX, double localY, double sizeX, double sizeY, double pressure, InteractiveObject relatedObject, bool ctrlKey, bool altKey, bool shiftKey, bool commandKey, bool controlKey)
        {
        }

        /// <summary>
        /// Creates an Event object that contains information about touch events.
        /// </summary>
        public TouchEvent(string type, bool bubbles, bool cancelable, int touchPointID, bool isPrimaryTouchPoint, double localX, double localY, double sizeX, double sizeY, double pressure, InteractiveObject relatedObject, bool ctrlKey, bool altKey, bool shiftKey, bool commandKey)
        {
        }

        /// <summary>
        /// Creates an Event object that contains information about touch events.
        /// </summary>
        public TouchEvent(string type, bool bubbles, bool cancelable, int touchPointID, bool isPrimaryTouchPoint, double localX, double localY, double sizeX, double sizeY, double pressure, InteractiveObject relatedObject, bool ctrlKey, bool altKey, bool shiftKey)
        {
        }

        /// <summary>
        /// Creates an Event object that contains information about touch events.
        /// </summary>
        public TouchEvent(string type, bool bubbles, bool cancelable, int touchPointID, bool isPrimaryTouchPoint, double localX, double localY, double sizeX, double sizeY, double pressure, InteractiveObject relatedObject, bool ctrlKey, bool altKey)
        {
        }

        /// <summary>
        /// Creates an Event object that contains information about touch events.
        /// </summary>
        public TouchEvent(string type, bool bubbles, bool cancelable, int touchPointID, bool isPrimaryTouchPoint, double localX, double localY, double sizeX, double sizeY, double pressure, InteractiveObject relatedObject, bool ctrlKey)
        {
        }

        /// <summary>
        /// Creates an Event object that contains information about touch events.
        /// </summary>
        public TouchEvent(string type, bool bubbles, bool cancelable, int touchPointID, bool isPrimaryTouchPoint, double localX, double localY, double sizeX, double sizeY, double pressure, InteractiveObject relatedObject)
        {
        }

        /// <summary>
        /// Creates an Event object that contains information about touch events.
        /// </summary>
        public TouchEvent(string type, bool bubbles, bool cancelable, int touchPointID, bool isPrimaryTouchPoint, double localX, double localY, double sizeX, double sizeY, double pressure)
        {
        }

        /// <summary>
        /// Creates an Event object that contains information about touch events.
        /// </summary>
        public TouchEvent(string type, bool bubbles, bool cancelable, int touchPointID, bool isPrimaryTouchPoint, double localX, double localY, double sizeX, double sizeY)
        {
        }

        /// <summary>
        /// Creates an Event object that contains information about touch events.
        /// </summary>
        public TouchEvent(string type, bool bubbles, bool cancelable, int touchPointID, bool isPrimaryTouchPoint, double localX, double localY, double sizeX)
        {
        }

        /// <summary>
        /// Creates an Event object that contains information about touch events.
        /// </summary>
        public TouchEvent(string type, bool bubbles, bool cancelable, int touchPointID, bool isPrimaryTouchPoint, double localX, double localY)
        {
        }

        /// <summary>
        /// Creates an Event object that contains information about touch events.
        /// </summary>
        public TouchEvent(string type, bool bubbles, bool cancelable, int touchPointID, bool isPrimaryTouchPoint, double localX)
        {
        }

        /// <summary>
        /// Creates an Event object that contains information about touch events.
        /// </summary>
        public TouchEvent(string type, bool bubbles, bool cancelable, int touchPointID, bool isPrimaryTouchPoint)
        {
        }

        /// <summary>
        /// Creates an Event object that contains information about touch events.
        /// </summary>
        public TouchEvent(string type, bool bubbles, bool cancelable, int touchPointID)
        {
        }

        /// <summary>
        /// Creates an Event object that contains information about touch events.
        /// </summary>
        public TouchEvent(string type, bool bubbles, bool cancelable)
        {
        }

        /// <summary>
        /// Creates an Event object that contains information about touch events.
        /// </summary>
        public TouchEvent(string type, bool bubbles)
        {
        }

        /// <summary>
        /// Creates an Event object that contains information about touch events.
        /// </summary>
        public TouchEvent(string type)
        {
        }

        #endregion


        #region Properties
        /// <summary>
        /// Indicates whether the Alt key is active (true) or inactive (false).
        /// </summary>
        public bool altKey { get; set; }

        /// <summary>
        /// Indicates whether the command key is activated (Mac only).
        /// </summary>
        public bool commandKey { get; set; }

        /// <summary>
        /// Indicates whether the Control key is activated on Mac and whether the Ctrl key is activated on Windows or Linux.
        /// </summary>
        public bool controlKey { get; set; }

        /// <summary>
        /// On Windows or Linux, indicates whether the Ctrl key is active (true) or inactive (false).
        /// </summary>
        public bool ctrlKey { get; set; }

        /// <summary>
        /// Indicates whether the first point of contact is mapped to mouse events.
        /// </summary>
        public bool isPrimaryTouchPoint { get; set; }

        /// <summary>
        /// If true, the relatedObject property is set to null for reasons related to security sandboxes.
        /// </summary>
        public bool isRelatedObjectInaccessible { get; set; }

        /// <summary>
        /// The horizontal coordinate at which the event occurred relative to the containing sprite.
        /// </summary>
        public double localX { get; set; }

        /// <summary>
        /// The vertical coordinate at which the event occurred relative to the containing sprite.
        /// </summary>
        public double localY { get; set; }

        /// <summary>
        /// A value between 0.0 and 1.0 indicating force of the contact with the device.
        /// </summary>
        public double pressure { get; set; }

        /// <summary>
        /// A reference to a display list object that is related to the event.
        /// </summary>
        public InteractiveObject relatedObject { get; set; }

        /// <summary>
        /// Indicates whether the Shift key is active (true) or inactive (false).
        /// </summary>
        public bool shiftKey { get; set; }

        /// <summary>
        /// Width of the contact area.
        /// </summary>
        public double sizeX { get; set; }

        /// <summary>
        /// Height of the contact area.
        /// </summary>
        public double sizeY { get; set; }

        /// <summary>
        /// [read-only] The horizontal coordinate at which the event occurred in global Stage coordinates.
        /// </summary>
        public double stageX { get; private set; }

        /// <summary>
        /// [read-only] The vertical coordinate at which the event occurred in global Stage coordinates.
        /// </summary>
        public double stageY { get; private set; }

        /// <summary>
        /// A unique identification number (as an int) assigned to the touch point.
        /// </summary>
        public int touchPointID { get; set; }

        #endregion

    }
}
