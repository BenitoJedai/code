using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ScriptCoreLib.ActionScript
{
    // http://livedocs.adobe.com/flash/9.0/ActionScriptLangRefV3/Namespace.html
    // http://livedocs.adobe.com/flex/3/langref/Namespace.html
    [Script(IsNative=true)]
    public sealed class Namespace
    {
        #region Properties
        /// <summary>
        /// The prefix of the namespace.
        /// </summary>
        public string prefix { get; set; }

        /// <summary>
        /// The Uniform Resource Identifier (URI) of the namespace.
        /// </summary>
        public string uri { get; set; }

        #endregion

        #region Methods
        /// <summary>
        /// Returns the URI value of the specified object.
        /// </summary>
        public string valueOf()
        {
            return default(string);
        }

        #endregion

        #region Constructors
        /// <summary>
        /// Creates a Namespace object.
        /// </summary>
        public Namespace(object uriValue)
        {
        }

        /// <summary>
        /// Creates a Namespace object according to the values of the prefixValue and uriValue parameters.
        /// </summary>
        public Namespace(object prefixValue, object uriValue)
        {
        }

        #endregion


    }
}
