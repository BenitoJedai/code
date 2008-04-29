using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ScriptCoreLib.ActionScript.flash.display;

namespace ScriptCoreLib.ActionScript.flash.events
{
    // http://livedocs.adobe.com/flex/201/langref/flash/events/MouseEvent.html
    [Script(IsNative = true)]
    public class MouseEvent : Event
    {



        #region Constants
        /// <summary>
        /// [static] Defines the value of the type property of a click event object.
        /// </summary>
        public static readonly string CLICK = "click";

        /// <summary>
        /// [static] Defines the value of the type property of a doubleClick event object.
        /// </summary>
        public static readonly string DOUBLE_CLICK = "doubleClick";

        /// <summary>
        /// [static] Defines the value of the type property of a mouseDown event object.
        /// </summary>
        public static readonly string MOUSE_DOWN = "mouseDown";

        /// <summary>
        /// [static] Defines the value of the type property of a mouseMove event object.
        /// </summary>
        public static readonly string MOUSE_MOVE = "mouseMove";

        /// <summary>
        /// [static] Defines the value of the type property of a mouseOut event object.
        /// </summary>
        public static readonly string MOUSE_OUT = "mouseOut";

        /// <summary>
        /// [static] Defines the value of the type property of a mouseOver event object.
        /// </summary>
        public static readonly string MOUSE_OVER = "mouseOver";

        /// <summary>
        /// [static] Defines the value of the type property of a mouseUp event object.
        /// </summary>
        public static readonly string MOUSE_UP = "mouseUp";

        /// <summary>
        /// [static] Defines the value of the type property of a mouseWheel event object.
        /// </summary>
        public static readonly string MOUSE_WHEEL = "mouseWheel";

        /// <summary>
        /// [static] Defines the value of the type property of a rollOut event object.
        /// </summary>
        public static readonly string ROLL_OUT = "rollOut";

        /// <summary>
        /// [static] Defines the value of the type property of a rollOver event object.
        /// </summary>
        public static readonly string ROLL_OVER = "rollOver";

        #endregion


        #region Properties
        /// <summary>
        /// Indicates whether the Alt key is active (true) or inactive (false).
        /// </summary>
        public bool altKey { get; set; }

        /// <summary>
        /// Indicates whether the primary mouse button is pressed (true) or not (false).
        /// </summary>
        public bool buttonDown { get; set; }

        /// <summary>
        /// On Windows, indicates whether the Ctrl key is active (true) or inactive (false).
        /// </summary>
        public bool ctrlKey { get; set; }

        /// <summary>
        /// Indicates how many lines should be scrolled for each unit the user rotates the mouse wheel.
        /// </summary>
        public int delta { get; set; }

        /// <summary>
        /// The horizontal coordinate at which the event occurred relative to the containing sprite.
        /// </summary>
        public double localX { get; set; }

        /// <summary>
        /// The vertical coordinate at which the event occurred relative to the containing sprite.
        /// </summary>
        public double localY { get; set; }

        /// <summary>
        /// A reference to a display list object that is related to the event.
        /// </summary>
        public InteractiveObject relatedObject { get; set; }

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



        /// <summary>
        /// Instructs Flash Player to render after processing of this event completes, if the display list has been modified. 
        /// </summary>
        public void updateAfterEvent()
        {
        }


    }
}
