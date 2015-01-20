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
        // X:\jsc.svn\core\ScriptCoreLib.Ultra\ScriptCoreLib.Ultra\Android\Manifest\ApplicationMetaDataAttribute.cs

        //[ScriptCoreLib.Android.Manifest.ApplicationMetaData(name = "android:targetSdkVersion", value = "21")]
        //[ScriptCoreLib.Android.Manifest.ApplicationMetaData(name = "android:theme", value = "@android:style/Theme.Holo.Dialog")]

        // tested by ?
        // we seem to use the metadata also.
        //  EIDNFC_Android_App\EIDNFC_Android_App\ApplicationActivity.cs

        // X:\jsc.svn\examples\java\android\forms\FormsMessageBox\FormsMessageBox\ApplicationActivity.cs

        // 
        // AssemblyDescription
        public string description;

        // AssemblyTitle
        public string label;
    }
}
