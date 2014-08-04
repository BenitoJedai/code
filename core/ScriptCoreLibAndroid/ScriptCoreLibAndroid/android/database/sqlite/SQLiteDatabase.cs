using ScriptCoreLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace android.database.sqlite
{
    // https://github.com/android/platform_frameworks_base/blob/master/core/java/android/database/sqlite/SQLiteDatabase.java

    // http://developer.android.com/reference/android/database/sqlite/SQLiteDatabase.html
    [Script(IsNative = true)]
    public class SQLiteDatabase
    {
        // X:\jsc.svn\core\ScriptCoreLib\JavaScript\DOM\IWindow.webdatabase.cs

        // http://developer.android.com/reference/android/database/sqlite/SQLiteDatabase.CursorFactory.html
        [Script(IsNative = true)]
        public interface CursorFactory
        {

        }

        public void execSQL(string sql)
        {
            throw new NotImplementedException();
        }

        public void execSQL(string sql, object[] bindArgs)
        {
            throw new NotImplementedException();
        }

        public Cursor rawQuery(string sql, string[] selectionArgs)
        {
            throw new NotImplementedException();
        }
    }
}
