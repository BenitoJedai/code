using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ScriptCoreLib.ActionScript.flash.system
{
    // http://livedocs.adobe.com/flash/9.0/ActionScriptLangRefV3/flash/system/LoaderContext.html
    [Script(IsNative = true)]
    public class LoaderContext
    {
        #region Constants

        #endregion

        #region Properties

        /// <summary>
        /// Specifies the application domain to use for the Loader.load() or Loader.loadBytes() method.
        /// </summary>
        public ApplicationDomain applicationDomain;

        /// <summary>
        /// Specifies whether Flash Player should attempt to download a cross-domain policy file from the loaded object's server before beginning to load the object itself.
        /// </summary>
        public bool checkPolicyFile;

        /// <summary>
        /// Specifies the security domain to use for a Loader.load() operation.
        /// </summary>
        public SecurityDomain securityDomain;

        #endregion

        #region Constructors

        /// <summary>
        /// Creates a new LoaderContext object, with the specified settings.
        /// </summary>
        public LoaderContext(bool checkPolicyFile, ApplicationDomain applicationDomain, SecurityDomain securityDomain)
        {
        }

        /// <summary>
        /// Creates a new LoaderContext object, with the specified settings.
        /// </summary>
        public LoaderContext(bool checkPolicyFile, ApplicationDomain applicationDomain)
        {
        }

        /// <summary>
        /// Creates a new LoaderContext object, with the specified settings.
        /// </summary>
        public LoaderContext(bool checkPolicyFile)
        {
        }

        /// <summary>
        /// Creates a new LoaderContext object, with the specified settings.
        /// </summary>
        public LoaderContext()
        {
        }

        #endregion

        #region Methods

        #endregion
    }
}
