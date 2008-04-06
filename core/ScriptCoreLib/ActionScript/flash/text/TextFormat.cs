using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ScriptCoreLib.ActionScript.flash.text
{
    /// <summary>
    /// http://livedocs.adobe.com/flex/3/langref/flash/text/TextFormat.html
    /// </summary>
    [Script(IsNative=true)]
    public class TextFormat
    {
        /// <summary>
        /// Indicates the alignment of the paragraph.
        /// <summary>
        public string align { get; set; }

        /// <summary>
        /// Indicates the block indentation in pixels.
        /// <summary>
        public object blockIndent { get; set; }

        /// <summary>
        /// Specifies whether the text is boldface.
        /// <summary>
        public object bold { get; set; }

        /// <summary>
        /// Indicates that the text is part of a bulleted list.
        /// <summary>
        public object bullet { get; set; }

        /// <summary>
        /// Indicates the color of the text.
        /// <summary>
        public object color { get; set; }

        /// <summary>
        /// The name of the font for text in this text format, as a string.
        /// <summary>
        public string font { get; set; }

        /// <summary>
        /// Indicates the indentation from the left margin to the first character in the paragraph.
        /// <summary>
        public object indent { get; set; }

        /// <summary>
        /// Indicates whether text in this text format is italicized.
        /// <summary>
        public object italic { get; set; }

        /// <summary>
        /// A Boolean value that indicates whether kerning is enabled (true) or disabled (false).
        /// <summary>
        public object kerning { get; set; }

        /// <summary>
        /// An integer representing the amount of vertical space (called leading) between lines.
        /// <summary>
        public object leading { get; set; }

        /// <summary>
        /// The left margin of the paragraph, in pixels.
        /// <summary>
        public object leftMargin { get; set; }

        /// <summary>
        /// A number representing the amount of space that is uniformly distributed between all characters.
        /// <summary>
        public object letterSpacing { get; set; }

        /// <summary>
        /// The right margin of the paragraph, in pixels.
        /// <summary>
        public object rightMargin { get; set; }

        /// <summary>
        /// The point size of text in this text format.
        /// <summary>
        public object size { get; set; }

        /// <summary>
        /// Specifies custom tab stops as an array of non-negative integers.
        /// <summary>
        public int[] tabStops { get; set; }

        /// <summary>
        /// Indicates the target window where the hyperlink is displayed.
        /// <summary>
        public string target { get; set; }

        /// <summary>
        /// Indicates whether the text that uses this text format is underlined (true) or not (false).
        /// <summary>
        public object underline { get; set; }

        /// <summary>
        /// Indicates the target URL for the text in this text format.
        /// <summary>
        public string url { get; set; }


    }
}
