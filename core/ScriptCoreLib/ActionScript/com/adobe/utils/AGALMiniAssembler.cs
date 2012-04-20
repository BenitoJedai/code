using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using ScriptCoreLib.ActionScript.Extensions;
using ScriptCoreLib.ActionScript.flash.utils;

namespace ScriptCoreLib.ActionScript.com.adobe.utils
{
    // http://barliesque.com/easy_agal/docs/com/adobe/utils/AGALMiniAssembler.html
    /// <summary>
    /// </summary>
    [Script(IsNative = true)]
    public class AGALMiniAssembler
    {

        #region CTOR

        public AGALMiniAssembler(bool debugging = false)
        {

        }

        #endregion

        #region METHODS

        public ByteArray assemble(string mode, string source)
        {
            return default(ByteArray);
        }

        #endregion

        #region PROTECTED_CONSTS

        protected static object COMPONENTS = default(object);
        protected static RegExp REGEXP_OUTER_SPACES = default(RegExp);
        protected static bool USE_NEW_SYNTAX = false;

        #endregion

        #region PROPERTIES

        public ByteArray agalcode { get; private set; }

        public string error { get; private set; }

        public bool verbose = false;

        #endregion
    }
}

    


