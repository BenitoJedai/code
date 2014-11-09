using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ScriptCoreLib.Android.Manifest
{
    // http://developer.android.com/guide/topics/manifest/application-element.html
    [AttributeUsage(AttributeTargets.Class, Inherited = false, AllowMultiple = true)]
    internal sealed class ApplicationAttribute : Attribute
    {
        // tested by ?
        // we seem to use the metadata also.
        //  EIDNFC_Android_App\EIDNFC_Android_App\ApplicationActivity.cs

        // AssemblyDescription
        public string description;

        // AssemblyTitle
        public string label;
    }
}
