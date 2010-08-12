using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ScriptCoreLib.ActionScript.flash.events
{
    // http://help.adobe.com/en_US/FlashPlatform/beta/reference/actionscript/3/flash/events/TransformGestureEvent.html
    [Script(IsNative = true)]
    public class TransformGestureEvent : GestureEvent
    {
        #region Properties
        /// <summary>
        /// The horizontal translation of the display object, since the previous gesture event.
        /// </summary>
        public double offsetX { get; set; }

        /// <summary>
        /// The vertical translation of the display object, since the previous gesture event.
        /// </summary>
        public double offsetY { get; set; }

        /// <summary>
        /// The current rotation angle, in degrees, of the display object along the z-axis, since the previous gesture event.
        /// </summary>
        public double rotation { get; set; }

        /// <summary>
        /// The horizontal scale of the display object, since the previous gesture event.
        /// </summary>
        public double scaleX { get; set; }

        /// <summary>
        /// The vertical scale of the display object, since the previous gesture event.
        /// </summary>
        public double scaleY { get; set; }

        #endregion


        #region Constants
        /// <summary>
        /// [static] Defines the value of the type property of a GESTURE_PAN touch event object.
        /// </summary>
        public static readonly string GESTURE_PAN = "gesturePan";

        /// <summary>
        /// [static] Defines the value of the type property of a GESTURE_ROTATE touch event object.
        /// </summary>
        public static readonly string GESTURE_ROTATE = "gestureRotate";

        /// <summary>
        /// [static] Defines the value of the type property of a GESTURE_SWIPE touch event object.
        /// </summary>
        public static readonly string GESTURE_SWIPE = "gestureSwipe";

        /// <summary>
        /// [static] Defines the value of the type property of a GESTURE_ZOOM touch event object.
        /// </summary>
        public static readonly string GESTURE_ZOOM = "gestureZoom";

        #endregion

        #region Methods
        /// <summary>
        /// [override] Creates a copy of the TransformGestureEvent object and sets the value of each property to match that of the original.
        /// </summary>
        public Event clone()
        {
            return default(Event);
        }

        #endregion

        #region Constructors
        /// <summary>
        /// Creates an Event object that contains information about complex multi-touch events, such as a user sliding his or her finger across a screen.
        /// </summary>
        public TransformGestureEvent(string type, bool bubbles, bool cancelable, string phase, double localX, double localY, double scaleX, double scaleY, double rotation, double offsetX, double offsetY, bool ctrlKey, bool altKey, bool shiftKey, bool commandKey, bool controlKey)
            : base(type, bubbles, cancelable, phase, localX, localY)
        {
        }

        /// <summary>
        /// Creates an Event object that contains information about complex multi-touch events, such as a user sliding his or her finger across a screen.
        /// </summary>
        public TransformGestureEvent(string type, bool bubbles, bool cancelable, string phase, double localX, double localY, double scaleX, double scaleY, double rotation, double offsetX, double offsetY, bool ctrlKey, bool altKey, bool shiftKey, bool commandKey)
            : base(type, bubbles, cancelable, phase, localX, localY)
        {
        }

        /// <summary>
        /// Creates an Event object that contains information about complex multi-touch events, such as a user sliding his or her finger across a screen.
        /// </summary>
        public TransformGestureEvent(string type, bool bubbles, bool cancelable, string phase, double localX, double localY, double scaleX, double scaleY, double rotation, double offsetX, double offsetY, bool ctrlKey, bool altKey, bool shiftKey)
            : base(type, bubbles, cancelable, phase, localX, localY)
        {
        }

        /// <summary>
        /// Creates an Event object that contains information about complex multi-touch events, such as a user sliding his or her finger across a screen.
        /// </summary>
        public TransformGestureEvent(string type, bool bubbles, bool cancelable, string phase, double localX, double localY, double scaleX, double scaleY, double rotation, double offsetX, double offsetY, bool ctrlKey, bool altKey)
            : base(type, bubbles, cancelable, phase, localX, localY)
        {
        }

        /// <summary>
        /// Creates an Event object that contains information about complex multi-touch events, such as a user sliding his or her finger across a screen.
        /// </summary>
        public TransformGestureEvent(string type, bool bubbles, bool cancelable, string phase, double localX, double localY, double scaleX, double scaleY, double rotation, double offsetX, double offsetY, bool ctrlKey)
            : base(type, bubbles, cancelable, phase, localX, localY)
        {
        }

        /// <summary>
        /// Creates an Event object that contains information about complex multi-touch events, such as a user sliding his or her finger across a screen.
        /// </summary>
        public TransformGestureEvent(string type, bool bubbles, bool cancelable, string phase, double localX, double localY, double scaleX, double scaleY, double rotation, double offsetX, double offsetY)
            : base(type, bubbles, cancelable, phase, localX, localY)
        {
        }

        /// <summary>
        /// Creates an Event object that contains information about complex multi-touch events, such as a user sliding his or her finger across a screen.
        /// </summary>
        public TransformGestureEvent(string type, bool bubbles, bool cancelable, string phase, double localX, double localY, double scaleX, double scaleY, double rotation, double offsetX)
            : base(type, bubbles, cancelable, phase, localX, localY)
        {
        }

        /// <summary>
        /// Creates an Event object that contains information about complex multi-touch events, such as a user sliding his or her finger across a screen.
        /// </summary>
        public TransformGestureEvent(string type, bool bubbles, bool cancelable, string phase, double localX, double localY, double scaleX, double scaleY, double rotation)
            : base(type, bubbles, cancelable, phase, localX, localY)
        {
        }

        /// <summary>
        /// Creates an Event object that contains information about complex multi-touch events, such as a user sliding his or her finger across a screen.
        /// </summary>
        public TransformGestureEvent(string type, bool bubbles, bool cancelable, string phase, double localX, double localY, double scaleX, double scaleY)
            : base(type, bubbles, cancelable, phase, localX, localY)
        {
        }

        /// <summary>
        /// Creates an Event object that contains information about complex multi-touch events, such as a user sliding his or her finger across a screen.
        /// </summary>
        public TransformGestureEvent(string type, bool bubbles, bool cancelable, string phase, double localX, double localY, double scaleX)
            : base(type, bubbles, cancelable, phase, localX, localY)
        {
        }

        /// <summary>
        /// Creates an Event object that contains information about complex multi-touch events, such as a user sliding his or her finger across a screen.
        /// </summary>
        public TransformGestureEvent(string type, bool bubbles, bool cancelable, string phase, double localX, double localY)
            : base(type, bubbles, cancelable, phase, localX, localY)
        {
        }

        /// <summary>
        /// Creates an Event object that contains information about complex multi-touch events, such as a user sliding his or her finger across a screen.
        /// </summary>
        public TransformGestureEvent(string type, bool bubbles, bool cancelable, string phase, double localX)
            : base(type, bubbles, cancelable, phase, localX)
        {
        }

        /// <summary>
        /// Creates an Event object that contains information about complex multi-touch events, such as a user sliding his or her finger across a screen.
        /// </summary>
        public TransformGestureEvent(string type, bool bubbles, bool cancelable, string phase)
            : base(type, bubbles, cancelable, phase)
        {
        }

        /// <summary>
        /// Creates an Event object that contains information about complex multi-touch events, such as a user sliding his or her finger across a screen.
        /// </summary>
        public TransformGestureEvent(string type, bool bubbles, bool cancelable)
            : base(type, bubbles, cancelable)
        {
        }

        /// <summary>
        /// Creates an Event object that contains information about complex multi-touch events, such as a user sliding his or her finger across a screen.
        /// </summary>
        public TransformGestureEvent(string type, bool bubbles)
            : base(type, bubbles)
        {
        }

        /// <summary>
        /// Creates an Event object that contains information about complex multi-touch events, such as a user sliding his or her finger across a screen.
        /// </summary>
        public TransformGestureEvent(string type)
            : base(type)
        {
        }

        #endregion

    }
}
