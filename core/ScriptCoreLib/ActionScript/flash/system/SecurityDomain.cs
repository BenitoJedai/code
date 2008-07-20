using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ScriptCoreLib.ActionScript.flash.system
{
    // http://livedocs.adobe.com/flex/201/langref/flash/system/SecurityDomain.html
    [Script(IsNative = true)]
    public class SecurityDomain
    {
        #region Constants
        /// <summary>
        /// [static][read-only] Gets the current security domain.
        /// </summary>
        public static readonly SecurityDomain currentDomain;

        #endregion

    }
}
