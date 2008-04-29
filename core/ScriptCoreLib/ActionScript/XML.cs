using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ScriptCoreLib.ActionScript
{
    
    // http://livedocs.adobe.com/flash/9.0/ActionScriptLangRefV3/XML.html

    /// <summary>
    /// The XML class contains methods and properties for working with XML objects. The XML class (along with the XMLList, Namespace, and QName classes) implements the powerful XML-handling standards defined in ECMAScript for XML (E4X) specification (ECMA-357 edition 2).
    /// </summary>
    [Script(IsNative=true)]
    public sealed class XML
    {
        #region Properties
        /// <summary>
        /// [static] Determines whether XML comments are ignored when XML objects parse the source XML data.
        /// </summary>
        public bool ignoreComments { get; set; }

        /// <summary>
        /// [static] Determines whether XML processing instructions are ignored when XML objects parse the source XML data.
        /// </summary>
        public bool ignoreProcessingInstructions { get; set; }

        /// <summary>
        /// [static] Determines whether white space characters at the beginning and end of text nodes are ignored during parsing.
        /// </summary>
        public bool ignoreWhitespace { get; set; }

        /// <summary>
        /// [static] Determines the amount of indentation applied by the toString() and toXMLString() methods when the XML.prettyPrinting property is set to true.
        /// </summary>
        public int prettyIndent { get; set; }

        /// <summary>
        /// [static] Determines whether the toString() and toXMLString() methods normalize white space characters between some tags.
        /// </summary>
        public bool prettyPrinting { get; set; }

        #endregion

    }
}
