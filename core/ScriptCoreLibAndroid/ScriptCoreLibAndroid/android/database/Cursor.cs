using ScriptCoreLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace android.database
{
    // http://developer.android.com/reference/android/database/Cursor.html
    [Script(IsNative = true)]
    public interface Cursor
    {

        void close();

        bool moveToFirst();

        bool moveToNext();

        int getInt(int i);

        string getString(int i);

        int getColumnIndex(string name);

        bool isAfterLast();
    }
}
