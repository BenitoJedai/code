using ScriptCoreLib;


namespace org.xml.sax
{
    [Script(IsNative = true)]
    public interface XMLReader
    {
        #region methods
        /// <summary>
        /// Return the current content handler.
        /// </summary>
         ContentHandler getContentHandler();

        /// <summary>
        /// Return the current DTD handler.
        /// </summary>
         DTDHandler getDTDHandler();

        /// <summary>
        /// Return the current entity resolver.
        /// </summary>
         EntityResolver getEntityResolver();

        /// <summary>
        /// Return the current error handler.
        /// </summary>
         ErrorHandler getErrorHandler();

        /// <summary>
        /// Look up the value of a feature flag.
        /// </summary>
         bool getFeature(string name);

        /// <summary>
        /// Look up the value of a property.
        /// </summary>
         object getProperty(string name);

        /// <summary>
        /// Parse an XML document.
        /// </summary>
         void parse(InputSource input);

        /// <summary>
        /// Parse an XML document from a system identifier (URI).
        /// </summary>
         void parse(string systemId);

        /// <summary>
        /// Allow an application to register a content event handler.
        /// </summary>
         void setContentHandler(ContentHandler handler);

        /// <summary>
        /// Allow an application to register a DTD event handler.
        /// </summary>
         void setDTDHandler(DTDHandler handler);

        /// <summary>
        /// Allow an application to register an entity resolver.
        /// </summary>
         void setEntityResolver(EntityResolver resolver);

        /// <summary>
        /// Allow an application to register an error event handler.
        /// </summary>
         void setErrorHandler(ErrorHandler handler);

        /// <summary>
        /// Set the value of a feature flag.
        /// </summary>
         void setFeature(string name, bool value);

        /// <summary>
        /// Set the value of a property.
        /// </summary>
         void setProperty(string name, object value);

        #endregion

    }
}
