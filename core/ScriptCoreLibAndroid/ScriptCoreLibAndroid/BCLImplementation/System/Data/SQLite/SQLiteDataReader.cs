using android.database;
using ScriptCoreLib.Shared.BCLImplementation.System.Data.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ScriptCoreLib.Android.BCLImplementation.System.Data.SQLite
{
    [Script(Implements = typeof(global::System.Data.SQLite.SQLiteDataReader))]
    internal class __SQLiteDataReader : __DbDataReader
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

        public override int GetOrdinal(string name)
        {
            int i = cursor.getColumnIndex(name);

            return i;
        }

        public override object this[string name]
        {
            get
            {
                int i = GetOrdinal(name);

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

        public override string GetName(int ordinal)
        {
            return cursor.getColumnName(ordinal);
        }

        public override long GetInt64(int ordinal)
        {
            return cursor.getLong(ordinal);
        }

        public override Type GetFieldType(int ordinal)
        {
            var t = cursor.getType(ordinal);

            // http://developer.android.com/reference/android/database/Cursor.html#FIELD_TYPE_INTEGER

            var FIELD_TYPE_INTEGER = 0x00000001;
            if (t == FIELD_TYPE_INTEGER)
                return typeof(long);

            var FIELD_TYPE_STRING = 0x00000003;
            if (t == FIELD_TYPE_STRING)
                return typeof(string);


            return null;
        }

        public override int FieldCount
        {
            get { return this.cursor.getColumnCount(); }
        }

        public override long GetBytes(int i, long fieldOffset, byte[] buffer, int bufferoffset, int length)
        {
            throw new NotImplementedException();
        }
    }

}
