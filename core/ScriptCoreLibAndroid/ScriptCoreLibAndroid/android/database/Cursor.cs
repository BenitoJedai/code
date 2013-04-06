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
        byte[] getBlob(int columnIndex);


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
