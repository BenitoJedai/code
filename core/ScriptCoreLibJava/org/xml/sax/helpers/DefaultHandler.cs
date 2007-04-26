using ScriptCoreLib;


namespace org.xml.sax.helpers
{
    [Script(IsNative = true)]
    public class DefaultHandler : ContentHandler, ErrorHandler, EntityResolver, DTDHandler
    {
        #region methods
        /// <summary>
        /// Receive notification of character data inside an element.
        /// </summary>
        public virtual void characters(char[] ch, int start, int length)
        {
        }

        /// <summary>
        /// Receive notification of the end of the document.
        /// </summary>
        public virtual void endDocument()
        {
        }

        /// <summary>
        /// Receive notification of the end of an element.
        /// </summary>
        public virtual void endElement(string uri, string localName, string qName)
        {
        }

        /// <summary>
        /// Receive notification of the end of a Namespace mapping.
        /// </summary>
        public void endPrefixMapping(string prefix)
        {
        }

        /// <summary>
        /// Receive notification of a recoverable parser error.
        /// </summary>
        public void error(SAXParseException e)
        {
        }

        /// <summary>
        /// Report a fatal XML parsing error.
        /// </summary>
        public void fatalError(SAXParseException e)
        {
        }

        /// <summary>
        /// Receive notification of ignorable whitespace in element content.
        /// </summary>
        public void ignorableWhitespace(char[] ch, int start, int length)
        {
        }

        /// <summary>
        /// Receive notification of a notation declaration.
        /// </summary>
        public void notationDecl(string name, string publicId, string systemId)
        {
        }

        /// <summary>
        /// Receive notification of a processing instruction.
        /// </summary>
        public void processingInstruction(string target, string data)
        {
        }

        /// <summary>
        /// Resolve an external entity.
        /// </summary>
        public InputSource resolveEntity(string publicId, string systemId)
        {
            return default(InputSource);
        }

        /// <summary>
        /// Receive a Locator object for document events.
        /// </summary>
        public void setDocumentLocator(Locator locator)
        {
        }

        /// <summary>
        /// Receive notification of a skipped entity.
        /// </summary>
        public void skippedEntity(string name)
        {
        }

        /// <summary>
        /// Receive notification of the beginning of the document.
        /// </summary>
        public virtual void startDocument()
        {
        }

        /// <summary>
        /// Receive notification of the start of an element.
        /// </summary>
        public virtual void startElement(string uri, string localName, string qName, Attributes attributes)
        {
        }

        /// <summary>
        /// Receive notification of the start of a Namespace mapping.
        /// </summary>
        public void startPrefixMapping(string prefix, string uri)
        {
        }

        /// <summary>
        /// Receive notification of an unparsed entity declaration.
        /// </summary>
        public void unparsedEntityDecl(string name, string publicId, string systemId, string notationName)
        {
        }

        /// <summary>
        /// Receive notification of a parser warning.
        /// </summary>
        public void warning(SAXParseException e)
        {
        }

        #endregion

    }
}
