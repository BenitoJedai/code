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


        // AssemblyDescription
        public string description;

        // AssemblyTitle
        public string label;
    }
}
