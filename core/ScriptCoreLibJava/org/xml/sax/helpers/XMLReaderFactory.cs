using ScriptCoreLib;


namespace org.xml.sax.helpers
{
    [Script(IsNative = true)]
    public class XMLReaderFactory
    {

        #region methods
        /// <summary>
        /// Attempt to create an XMLReader from system defaults.
        /// </summary>
        public static XMLReader createXMLReader()
        {
            return default(XMLReader);
        }

        /// <summary>
        /// Attempt to create an XML reader from a class name.
        /// </summary>
        public static XMLReader createXMLReader(string className)
        {
            return default(XMLReader);
        }

        #endregion

    }
}
