using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ScriptCoreLib.ActionScript.flash.events;

namespace ScriptCoreLib.ActionScript.flash.text
{
    /// <summary>
    /// http://livedocs.adobe.com/flex/3/langref/flash/text/StyleSheet.html
    /// </summary>
    [Script(IsNative = true)]
    public class StyleSheet : EventDispatcher
    {

        /// <summary>
        /// Removes all styles from the style sheet object.
        /// </summary>
        public void clear() { }

        /// <summary>
        /// Returns a copy of the style object associated with the style named styleName.
        /// </summary>
        public object getStyle(string styleName) { return default(object); }


        /// <summary>
        /// Parses the CSS in CSSText and loads the style sheet with it.
        /// </summary>
        public void parseCSS(string CSSText)
        {
        }


        /// <summary>
        /// Adds a new style with the specified name to the style sheet object.
        /// </summary>
        public void setStyle(string styleName, object styleObject) { }

        /// <summary>
        /// Extends the CSS parsing capability.
        /// </summary>
        public TextFormat transform(object formatObject)
        {
            return default(TextFormat);
        }


    }
}
