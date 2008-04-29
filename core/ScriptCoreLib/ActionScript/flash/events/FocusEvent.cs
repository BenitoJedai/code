using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ScriptCoreLib.ActionScript.flash.display;

namespace ScriptCoreLib.ActionScript.flash.events
{
    // http://livedocs.adobe.com/flash/9.0/ActionScriptLangRefV3/flash/events/FocusEvent.html
    [Script(IsNative = true)]
    public class FocusEvent : Event
    {
        #region Constants
        /// <summary>
        /// [static] Defines the value of the type property of a focusIn event object.
        /// </summary>
        public static readonly string FOCUS_IN = "focusIn";

        /// <summary>
        /// [static] Defines the value of the type property of a focusOut event object.
        /// </summary>
        public static readonly string FOCUS_OUT = "focusOut";

        /// <summary>
        /// [static] Defines the value of the type property of a keyFocusChange event object.
        /// </summary>
        public static readonly string KEY_FOCUS_CHANGE = "keyFocusChange";

        /// <summary>
        /// [static] Defines the value of the type property of a mouseFocusChange event object.
        /// </summary>
        public static readonly string MOUSE_FOCUS_CHANGE = "mouseFocusChange";

        #endregion


        #region Properties
        /// <summary>
        /// The key code value of the key pressed to trigger a keyFocusChange event.
        /// </summary>
        public uint keyCode { get; set; }

        /// <summary>
        /// A reference to the complementary InteractiveObject instance that is affected by the change in focus.
        /// </summary>
        public InteractiveObject relatedObject { get; set; }

        /// <summary>
        /// Indicates whether the Shift key modifier is activated, in which case the value is true.
        /// </summary>
        public bool shiftKey { get; set; }

        #endregion





    }
}
