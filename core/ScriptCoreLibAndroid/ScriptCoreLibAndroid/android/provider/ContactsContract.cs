using android.net;
using ScriptCoreLib;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace android.provider
{
    // https://github.com/android/platform_frameworks_base/blob/master/core/java/android/provider/ContactsContract.java

    [Script(IsNative = true)]
    public static class ContactsContract
    {
        // how would we use it from
        // x:\jsc.svn\examples\javascript\chrome\apps\chromenexus7\chromenexus7\application.cs

        [Script(IsNative = true)]
        public static class Contacts
        {
            public static readonly Uri CONTENT_URI;

            public static readonly string _ID;
            public static readonly string DISPLAY_NAME;
        }
    }
}
