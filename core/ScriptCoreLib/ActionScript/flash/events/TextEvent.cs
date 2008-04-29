using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ScriptCoreLib.ActionScript.flash.display;

namespace ScriptCoreLib.ActionScript.flash.events
{
    // http://livedocs.adobe.com/flash/9.0/ActionScriptLangRefV3/flash/events/TextEvent.html
    [Script(IsNative = true)]
    public class TextEvent : Event
    {
        #region Constants
        /// <summary>
        /// [static] Defines the value of the type property of a link event object.
        /// </summary>
        public static readonly string LINK = "link";

        /// <summary>
        /// [static] Defines the value of the type property of a textInput event object.
        /// </summary>
        public static readonly string TEXT_INPUT = "textInput";

        #endregion


        #region Properties
        /// <summary>
        /// For a textInput event, the character or sequence of characters entered by the user.
        /// </summary>
        public string text { get; set; }

        #endregion


        #region Methods
        /// <summary>
        /// Creates a copy of the TextEvent object and sets the value of each property to match that of the original.
        /// </summary>
        public Event clone()
        {
            return default(Event);
        }

        #endregion

        #region Constructors
        /// <summary>
        /// Creates an Event object that contains information about text events.
        /// </summary>
        public TextEvent(string type, bool bubbles, bool cancelable, string text)
        {
        }

        /// <summary>
        /// Creates an Event object that contains information about text events.
        /// </summary>
        public TextEvent(string type, bool bubbles, bool cancelable)
        {
        }

        /// <summary>
        /// Creates an Event object that contains information about text events.
        /// </summary>
        public TextEvent(string type, bool bubbles)
        {
        }

        /// <summary>
        /// Creates an Event object that contains information about text events.
        /// </summary>
        public TextEvent(string type)
        {
        }

        #endregion

    }
}
