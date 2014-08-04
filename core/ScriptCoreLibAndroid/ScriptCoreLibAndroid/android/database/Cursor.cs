using ScriptCoreLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace android.database
{
    // https://github.com/android/platform_frameworks_base/blob/master/core/java/android/database/Cursor.java

    // http://developer.android.com/reference/android/database/Cursor.html
    [Script(IsNative = true)]
    public interface Cursor
    {
        //        0012:07aa ScriptCoreLibAndroid create android.test.mock.MockCursor
        //Method 'getBlob' in type 'android.test.mock.MockCursor' from assembly 'ScriptCoreLibAndroid, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null' does not have an implementation.
        //  fix compiler to wait for android.test.mock.MockCursor

        // jsc should warn if we misspelled return type
        sbyte[] getBlob(int columnIndex);


        int getCount();


        void close();

        bool moveToFirst();

        bool moveToNext();
        int getColumnCount();
        string getColumnName(int columnIndex);

        int getInt(int i);
        long getLong(int columnIndex);

        string getString(int i);

        int getColumnIndex(string name);
        int getType(int columnIndex);

        bool isAfterLast();
    }
}
