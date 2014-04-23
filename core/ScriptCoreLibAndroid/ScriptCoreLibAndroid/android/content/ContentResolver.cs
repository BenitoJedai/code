using android.database;
using android.net;
using ScriptCoreLib;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace android.content
{
    // http://developer.android.com/reference/android/content/ContentResolver.html
    [Script(IsNative = true)]
    public class ContentResolver
    {
        public Cursor query(Uri uri, string[] projection, string selection, string[] selectionArgs, string sortOrder)
        {
            return null;
        }

    }
}
