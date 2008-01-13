﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using ScriptCoreLib.ActionScript.flash.display;

namespace ScriptCoreLib.ActionScript.flash.text
{
    // http://livedocs.adobe.com/flex/2/langref/flash/text/TextField.html
    [Script(IsNative = true)]
    public class TextField : InteractiveObject
    {
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
        /// Specifies whether the text field has a background fill.
        /// </summary>
        public bool background { get; set; }

        /// <summary>
        /// The color of the text field background.
        /// </summary>
        public uint backgroundColor { get; set; }
    }
}
