using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ScriptCoreLib.Android.Manifest
{
    // move to .Manifest
    #region IntentFilter
    [AttributeUsage(AttributeTargets.Class, Inherited = false, AllowMultiple = true)]
    //public sealed class IntentFilterAttribute : Attribute
    public sealed class ApplicationIntentFilterAttribute : Attribute
    {
        // jsc does not support properties yet? are they even allowed in java?

        // action instead?
        public string Action;



    }
    #endregion

    #region IntentFilter
    [AttributeUsage(AttributeTargets.Class, Inherited = false, AllowMultiple = true)]
    //public sealed class IntentFilterDataAttribute : Attribute
    public sealed class ApplicationIntentFilterDataAttribute : Attribute
    {
        // jsc does not support properties yet? are they even allowed in java?

        public string scheme;
    }
    #endregion
}
