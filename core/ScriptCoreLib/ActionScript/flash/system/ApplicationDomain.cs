using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ScriptCoreLib.ActionScript.flash.utils;

namespace ScriptCoreLib.ActionScript.flash.system
{
    // http://livedocs.adobe.com/flash/9.0/ActionScriptLangRefV3/flash/system/ApplicationDomain.html
    [Script(IsNative=true)]
    public sealed class ApplicationDomain
    {
        #region Constants

        #endregion

        #region Properties

        /// <summary>
        /// [static] [read-only] Gets the current application domain in which your code is executing.
        /// </summary>
        public static ApplicationDomain currentDomain { get; private set; }

        /// <summary>
        /// Gets and sets the object on which domain-global memory operations will operate within this ApplicationDomain
        /// </summary>
        public ByteArray domainMemory { get; set; }

        /// <summary>
        /// [static] [read-only] Gets the minimum memory object length required to be used as ApplicationDomain.domainMemory
        /// </summary>
        public static uint MIN_DOMAIN_MEMORY_LENGTH { get; private set; }

        /// <summary>
        /// [read-only] Gets the parent domain of this application domain.
        /// </summary>
        public ApplicationDomain parentDomain { get; private set; }

        #endregion

        #region Constructors

        /// <summary>
        /// Creates a new application domain.
        /// </summary>
        public ApplicationDomain(ApplicationDomain parentDomain)
        {
        }

        /// <summary>
        /// Creates a new application domain.
        /// </summary>
        public ApplicationDomain()
        {
        }

        #endregion

        #region Methods

        /// <summary>
        /// Gets a public definition from the specified application domain.
        /// </summary>
        public object getDefinition(string name)
        {
            return default(object);
        }

        /// <summary>
        /// Checks to see if a public definition exists within the specified application domain.
        /// </summary>
        public bool hasDefinition(string name)
        {
            return default(bool);
        }

        #endregion
    }
}
