using android.database;
using ScriptCoreLib.Android.BCLImplementation.System.Data.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ScriptCoreLib.Android.BCLImplementation.System.Data.SQLite
{
    [Script(ImplementsViaAssemblyQualifiedName = "System.Data.SQLite.SQLiteDataReader, System.Data.SQLite")]
    public class __SQLiteDataReader : __DbDataReaders
    {
        public Cursor cursor;

        int __state;

        public override void Close()
        {
            cursor.close();
        }

        public override bool Read()
        {
            if (__state == 0)
            {
                __state = 1;

                cursor.moveToFirst();
            }
            else
            {
                cursor.moveToNext();
            }

            return !(cursor.isAfterLast());
        }

        public override object this[string name]
        {
            get
            {
                int i = cursor.getColumnIndex(name);

                return cursor.getString(i);
            }
        }

        public override string GetString(int i)
        {
            return cursor.getString(i);
        }

        public override int GetInt32(int i)
        {
            return cursor.getInt(i);
        }
    }

}
