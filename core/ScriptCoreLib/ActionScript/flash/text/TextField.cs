using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using ScriptCoreLib.ActionScript.flash.display;
using ScriptCoreLib.ActionScript.flash.events;

namespace ScriptCoreLib.ActionScript.flash.text
{
    // http://livedocs.adobe.com/flex/201/langref/flash/text/TextField.html
    [Script(IsNative = true)]
    public class TextField : InteractiveObject
    {
        [method: Script(NotImplementedHere = true)]
        public event Action<Event> change;

        /// <summary>
        /// Controls automatic sizing and alignment of text fields.
        /// </summary>
        public string autoSize { get; set; }



        /// <summary>
        /// Contains the HTML representation of the text field's contents.
        /// </summary>
        public string htmlText { get; set; }

        /// <summary>
        /// A string that is the current text in the text field.
        /// </summary>
        public string text { get; set; }

        /// <summary>
        /// A Boolean value that indicates whether the text field is selectable.
        /// </summary>
        public bool selectable { get; set; }

        /// <summary>
        /// The sharpness of the glyph edges in this text field.
        /// </summary>
        public double sharpness { get; set; }

        /// <summary>
        /// The color of the text in a text field, in hexadecimal format.
        /// </summary>
        public uint textColor { get; set; }

        /// <summary>
        /// Specifies whether the text field has a border.
        /// </summary>
        public bool border { get; set; }


        /// <summary>
        /// Specifies whether the text field has a background fill.
        /// </summary>
        public bool background { get; set; }

        /// <summary>
        /// The color of the text field background.
        /// </summary>
        public uint backgroundColor { get; set; }

        /// <summary>
        /// Indicates whether the text field is a multiline text field.
        /// </summary>
        public bool multiline { get; set; }


        /// <summary>
        /// The type of the text field.
        /// </summary>
        public string type { get; set; }

        /// <summary>
        /// A Boolean value that indicates whether the text field has word wrap.
        /// </summary>
        public bool wordWrap { get; set; }


    }
}
