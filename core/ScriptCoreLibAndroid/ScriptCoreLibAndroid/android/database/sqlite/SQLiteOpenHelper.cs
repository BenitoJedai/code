using ScriptCoreLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace android.database.sqlite
{
    // https://github.com/android/platform_frameworks_base/blob/master/core/java/android/database/sqlite/SQLiteOpenHelper.java

    // http://developer.android.com/reference/android/database/sqlite/SQLiteOpenHelper.html
    [Script(IsNative = true)]
    public abstract class SQLiteOpenHelper
    {


        public SQLiteOpenHelper(content.Context context, string name, SQLiteDatabase.CursorFactory factory, int version)
        {
  
        }
        public void close()
        {
        }


        public SQLiteDatabase getReadableDatabase()
        {
            return default(SQLiteDatabase);
        }

        public SQLiteDatabase getWritableDatabase()
        {
            return default(SQLiteDatabase);
        }

        public abstract void onCreate(SQLiteDatabase db);
        public abstract void onUpgrade(SQLiteDatabase db, int oldVersion, int newVersion);
    }
}
