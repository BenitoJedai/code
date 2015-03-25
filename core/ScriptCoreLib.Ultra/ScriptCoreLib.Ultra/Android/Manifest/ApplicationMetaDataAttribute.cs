using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ScriptCoreLib.Android.Manifest
{
    [AttributeUsage(AttributeTargets.All, Inherited = false, AllowMultiple = true)]
    public sealed class ApplicationMetaDataAttribute : Attribute
    {
        // X:\jsc.svn\core\ScriptCoreLibAndroid\ScriptCoreLibAndroid\Manifest\ApplicationAttribute.cs

        public string name;
        public string value;

        // http://stackoverflow.com/questions/16510140/android-nfc-intent-filter-to-show-my-application-when-nfc-discover-a-tag
        public string resource;   

    }
}
