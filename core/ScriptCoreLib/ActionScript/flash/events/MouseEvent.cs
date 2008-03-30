﻿using System;
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
        public static readonly string CLICK = "click";
        public static readonly string MOUSE_WHEEL = "mouseWheel";
        public static readonly string MOUSE_MOVE = "mouseMove";
        public static readonly string MOUSE_OVER = "mouseOver";
        public static readonly string MOUSE_OUT = "mouseOut";
        public static readonly string MOUSE_DOWN = "mouseDown";
        public static readonly string MOUSE_UP = "mouseUp";

        /// <summary>
        /// The horizontal coordinate at which the event occurred in global Stage coordinates.
        /// </summary>
        public double stageX { get; private set; }

        /// <summary>
        /// The vertical coordinate at which the event occurred in global Stage coordinates.
        /// </summary>
        public double stageY { get; private set; }

        /// <summary>
        /// Indicates how many lines should be scrolled for each unit the user rotates the mouse wheel.
        /// </summary>
        public int delta { get; private set; }

        /// <summary>
        /// Indicates whether the Shift key is active (true) or inactive (false).
        /// </summary>
        public bool shiftKey { get; set; }
        	

        /// <summary>
        /// Indicates whether the Control key is active (true) or inactive (false).
        /// </summary>
        public bool ctrlKey { get; set; }

        /// <summary>
        /// Indicates whether the Alt key is active (true) or inactive (false).
        /// </summary>
        public bool altKey { get; set; }

        
        /// <summary>
        /// A reference to a display list object that is related to the event. For example, when a mouseOut event occurs, relatedObject represents the display list object to which the pointing device now points. This property applies only to the mouseOut and mouseOver events.
        /// </summary>
        public InteractiveObject relatedObject { get; set; }


        /// <summary>
        /// Instructs Flash Player to render after processing of this event completes, if the display list has been modified. 
        /// </summary>
        public void updateAfterEvent()
        {
        }


    }
}
