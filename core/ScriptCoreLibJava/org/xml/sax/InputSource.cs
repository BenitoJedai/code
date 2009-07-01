using ScriptCoreLib;


namespace org.xml.sax
{
    [Script(IsNative = true)]
    public class InputSource
    {
		public InputSource(java.io.InputStream r)
		{
		}

        public InputSource(java.io.Reader r)
        {
        }

        #region methods
        /// <summary>
        /// Get the byte stream for this input source.
        /// </summary>
        public java.io.InputStream getByteStream()
        {
            return default(java.io.InputStream);
        }

        /// <summary>
        /// Get the character stream for this input source.
        /// </summary>
        public java.io.Reader getCharacterStream()
        {
            return default(java.io.Reader);
        }

        /// <summary>
        /// Get the character encoding for a byte stream or URI.
        /// </summary>
        public string getEncoding()
        {
            return default(string);
        }

        /// <summary>
        /// Get the public identifier for this input source.
        /// </summary>
        public string getPublicId()
        {
            return default(string);
        }

        /// <summary>
        /// Get the system identifier for this input source.
        /// </summary>
        public string getSystemId()
        {
            return default(string);
        }

        /// <summary>
        /// Set the byte stream for this input source.
        /// </summary>
        public void setByteStream(java.io.InputStream sbyteStream)
        {
        }

        /// <summary>
        /// Set the character stream for this input source.
        /// </summary>
        public void setCharacterStream(java.io.Reader characterStream)
        {
        }

        /// <summary>
        /// Set the character encoding, if known.
        /// </summary>
        public void setEncoding(string encoding)
        {
        }

        /// <summary>
        /// Set the public identifier for this input source.
        /// </summary>
        public void setPublicId(string publicId)
        {
        }

        /// <summary>
        /// Set the system identifier for this input source.
        /// </summary>
        public void setSystemId(string systemId)
        {
        }

        #endregion

    }
}
