using android.net;
using ScriptCoreLib;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace android.provider
{
    [Script(IsNative = true)]
    public static class ContactsContract
    {
        [Script(IsNative = true)]
        public static class Contacts
        {
            public static readonly Uri CONTENT_URI;

            public static readonly string _ID;
            public static readonly string DISPLAY_NAME;
        }
    }
}
