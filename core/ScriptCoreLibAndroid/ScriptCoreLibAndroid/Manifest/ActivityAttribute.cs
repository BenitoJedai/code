using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ScriptCoreLib.Android.Manifest
{
    // http://developer.android.com/guide/topics/manifest/activity-element.html
    [AttributeUsage(AttributeTargets.Class, Inherited = false, AllowMultiple = true)]
    internal sealed class ActivityAttribute : Attribute
    {
        // tested by ?

        // see also
        // X:\jsc.svn\core\ScriptCoreLib.Ultra\ScriptCoreLib.Ultra\Android\IntentFilterAttribute.cs
        // X:\jsc.svn\core\ScriptCoreLib.Ultra\ScriptCoreLib.Ultra\Android\Manifest\ApplicationMetaDataAttribute.cs

    }
}
