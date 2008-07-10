using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ScriptCoreLib.ActionScript
{
    // http://livedocs.adobe.com/flash/9.0/ActionScriptLangRefV3/QName.html
    [Script(IsNative = true)]
    public class QName
    {
        #region Properties

        /// <summary>
        /// [read-only] The local name of the QName object.
        /// </summary>
        public string localName { get; private set; }

        /// <summary>
        /// [read-only] The Uniform Resource Identifier (URI) of the QName object.
        /// </summary>
        public string uri { get; private set; }

        #endregion

        #region Constructors

        /// <summary>
        /// Creates a QName object with a URI from a Namespace object and a localName from a QName object.
        /// </summary>
        public QName(Namespace uri, QName localName)
        {
        }

        /// <summary>
        /// Creates a QName object that is a copy of another QName object.
        /// </summary>
        public QName(QName qname)
        {
        }

        #endregion

        #region Methods

        /// <summary>
        /// Returns a string composed of the URI, and the local name for the QName object, separated by "::".
        /// </summary>
        public string toString()
        {
            return default(string);
        }

        /// <summary>
        /// Returns the QName object.
        /// </summary>
        public QName valueOf()
        {
            return default(QName);
        }

        #endregion
    }
}
