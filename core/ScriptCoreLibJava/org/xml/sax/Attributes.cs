using ScriptCoreLib;


namespace org.xml.sax
{
 
        /// <summary>
        /// Interface for a list of XML attributes. 
        /// </summary>
        [Script(IsNative = true)]
        public interface Attributes
        {
            // Method Summary
            /// <summary>
            /// Look up the index of an attribute by XML 1.0 qualified name.
            /// </summary>
            int getIndex(string qName);

            /// <summary>
            /// Look up the index of an attribute by Namespace name.
            /// </summary>
            int getIndex(string uri, string localName);

            /// <summary>
            /// Return the number of attributes in the list.
            /// </summary>
            int getLength();

            /// <summary>
            /// Look up an attribute's local name by index.
            /// </summary>
            string getLocalName(int index);

            /// <summary>
            /// Look up an attribute's XML 1.0 qualified name by index.
            /// </summary>
            string getQName(int index);

            /// <summary>
            /// Look up an attribute's type by index.
            /// </summary>
            string getType(int index);

            /// <summary>
            /// Look up an attribute's type by XML 1.0 qualified name.
            /// </summary>
            string getType(string qName);

            /// <summary>
            /// Look up an attribute's type by Namespace name.
            /// </summary>
            string getType(string uri, string localName);

            /// <summary>
            /// Look up an attribute's Namespace URI by index.
            /// </summary>
            string getURI(int index);

            /// <summary>
            /// Look up an attribute's value by index.
            /// </summary>
            string getValue(int index);

            /// <summary>
            /// Look up an attribute's value by XML 1.0 qualified name.
            /// </summary>
            string getValue(string qName);

            /// <summary>
            /// Look up an attribute's value by Namespace name.
            /// </summary>
            string getValue(string uri, string localName);

        }


}
