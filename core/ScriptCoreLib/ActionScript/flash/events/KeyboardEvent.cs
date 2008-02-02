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
        public static readonly string KEY_DOWN = "keyDown";
        public static readonly string KEY_UP = "keyUp";

        /// <summary>
        /// The key code value of the key pressed or released.
        /// </summary>
        public uint keyCode { get; set; }
         	 	



        /// <summary>
        /// Instructs Flash Player to render after processing of this event completes, if the display list has been modified. 
        /// </summary>
        public void updateAfterEvent()
        {
        }


    }
}
