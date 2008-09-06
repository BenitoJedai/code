using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ScriptCoreLib.ActionScript.flash.events
{
    // http://livedocs.adobe.com/flash/9.0/ActionScriptLangRefV3/flash/events/KeyboardEvent.html
    [Script(IsNative = true)]
    public class KeyboardEvent : Event
    {
        #region Constants
        /// <summary>
        /// [static] Defines the value of the type property of a keyDown event object.
        /// </summary>
        public static readonly string KEY_DOWN = "keyDown";

        /// <summary>
        /// [static] Defines the value of the type property of a keyUp event object.
        /// </summary>
        public static readonly string KEY_UP = "keyUp";

        #endregion


        #region Properties
        /// <summary>
        /// Indicates whether the Alt key is active (true) or inactive (false) on Windows; indicates whether the Option key is active on Mac OS.
        /// </summary>
        public bool altKey { get; set; }

        /// <summary>
        /// Contains the character code value of the key pressed or released.
        /// </summary>
        public uint charCode { get; set; }

        /// <summary>
        /// On Windows, indicates whether the Ctrl key is active (true) or inactive (false); On Mac OS, indicates whether either the Ctrl key or the Command key is active.
        /// </summary>
        public bool ctrlKey { get; set; }

        /// <summary>
        /// The key code value of the key pressed or released.
        /// </summary>
        public uint keyCode { get; set; }

        /// <summary>
        /// Indicates the location of the key on the keyboard.
        /// </summary>
        public uint keyLocation { get; set; }

        /// <summary>
        /// Indicates whether the Shift key modifier is active (true) or inactive (false).
        /// </summary>
        public bool shiftKey { get; set; }

        #endregion


        /// <summary>
        /// Instructs Flash Player to render after processing of this event completes, if the display list has been modified. 
        /// </summary>
        public void updateAfterEvent()
        {
        }



    }
}
