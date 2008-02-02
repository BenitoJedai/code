using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ScriptCoreLib.ActionScript.flash.events
{
    // http://livedocs.adobe.com/flash/9.0/ActionScriptLangRefV3/flash/events/FocusEvent.html
    [Script(IsNative = true)]
    public class FocusEvent : Event
    {
        public static readonly string FOCUS_OUT = "focusOut";
        public static readonly string FOCUS_IN = "focusIn";

        	
        /// <summary>
        /// The key code value of the key pressed to trigger a keyFocusChange event.
        /// </summary>
        public uint keyCode { get; set; }
         	 	





    }
}
