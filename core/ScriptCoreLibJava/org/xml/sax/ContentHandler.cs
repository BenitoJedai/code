using ScriptCoreLib;


namespace org.xml.sax
{
    [Script(IsNative = true)]
    public interface ContentHandler
    {
        #region methods
        /// <summary>
        /// Receive notification of character data.
        /// </summary>
        void characters(char[] ch, int start, int length);

        /// <summary>
        /// Receive notification of the end of a document.
        /// </summary>
        void endDocument();

        /// <summary>
        /// Receive notification of the end of an element.
        /// </summary>
        void endElement(string uri, string localName, string qName);

        /// <summary>
        /// End the scope of a prefix-URI mapping.
        /// </summary>
        void endPrefixMapping(string prefix);
        /// <summary>
        /// Receive notification of ignorable whitespace in element content.
        /// </summary>
        void ignorableWhitespace(char[] ch, int start, int length);

        /// <summary>
        /// Receive notification of a processing instruction.
        /// </summary>
        void processingInstruction(string target, string data);

        /// <summary>
        /// Receive an object for locating the origin of SAX document events.
        /// </summary>
        void setDocumentLocator(Locator locator);

        /// <summary>
        /// Receive notification of a skipped entity.
        /// </summary>
        void skippedEntity(string name);

        /// <summary>
        /// Receive notification of the beginning of a document.
        /// </summary>
        void startDocument();
        /// <summary>
        /// Receive notification of the beginning of an element.
        /// </summary>
        void startElement(string uri, string localName, string qName, Attributes atts);

        /// <summary>
        /// Begin the scope of a prefix-URI Namespace mapping.
        /// </summary>
        void startPrefixMapping(string prefix, string uri);

        #endregion

    }
}
