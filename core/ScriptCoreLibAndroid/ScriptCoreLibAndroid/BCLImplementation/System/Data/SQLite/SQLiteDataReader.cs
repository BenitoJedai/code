using android.database;
using ScriptCoreLib.Shared.BCLImplementation.System.Data.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace ScriptCoreLib.Android.BCLImplementation.System.Data.SQLite
{
    //[Script(Implements = typeof(global::System.Data.SQLite.SQLiteDataReader))]
    [Script(ImplementsViaAssemblyQualifiedName = "System.Data.SQLite.SQLiteDataReader")]
    internal class __SQLiteDataReader : __DbDataReader
    {
        // X:\jsc.svn\core\ScriptCoreLib\JavaScript\BCLImplementation\System\Data\SQLite\SQLiteDataReader.cs
        // X:\jsc.svn\core\ScriptCoreLib\Shared\BCLImplementation\System\Data\Common\DbDataReader.cs

        //I/System.Console( 2892): enter GetFieldType { ordinal = 0 }
        //I/dalvikvm( 2892): Could not find method android.database.Cursor.getType, referenced from method ScriptCoreLib.Android.BCLImplementation.System.Data.SQLite.__SQLiteDataReader_OptionalAPI.__cctor_b__0
        //W/dalvikvm( 2892): VFY: unable to resolve interface method 5157: Landroid/database/Cursor;.getType (I)I
        //D/dalvikvm( 2892): VFY: replacing opcode 0x72 at 0x0017
        //D/dalvikvm( 2892): VFY: dead code 0x001a-001b in LScriptCoreLib/Android/BCLImplementation/System/Data/SQLite/__SQLiteDataReader_OptionalAPI;.__cctor_b__0 (Landroid/database/Cursor;I)I
        //I/System.Console( 2892): getType is unavailable at API 8


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

        public override object this[int i]
        {
            get
            {
                var t = GetFieldType(i);

                if (t == typeof(long))
                    return this.GetInt64(i);

                return this.GetString(i);
            }
        }

        public override object this[string name]
        {
            get
            {
                int i = GetOrdinal(name);
                return this[i];
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








        const int FIELD_TYPE_INTEGER = 0x00000001;
        const int FIELD_TYPE_STRING = 0x00000003;


        // inspiration for
        // http://msdn.microsoft.com/en-us/library/aa383056(v=vs.85).aspx
        [Obsolete("Run-Time linking")]
        [Script]
        static class OptionalAPI
        {
            static MethodInfo refCursor_getType = typeof(Cursor).GetMethod(
                "getType", new Type[] { typeof(int) }
                );

            static bool refCursor_getType_NotifiedOf = false;

            // can this be an extension method?
            public static Func<Cursor, int, int> Cursor_getType = (cursor, ordinal) =>
            {
                // X:\jsc.svn\examples\javascript\p2p\SharedBrowserSessionExperiment\SharedBrowserSessionExperiment\ApplicationWebService.cs

                if (refCursor_getType == null)
                {
                    if (!refCursor_getType_NotifiedOf)
                    {
                        Console.WriteLine("getType is unavailable at API 8");

                        refCursor_getType_NotifiedOf = true;
                    }

                    return FIELD_TYPE_STRING;
                }


                // can we actually reference it or is VM going to stop the method?
                return cursor.getType(ordinal);
            };
        }


        public override Type GetFieldType(int ordinal)
        {
            Console.WriteLine("enter GetFieldType " + new { ordinal });

            var t = OptionalAPI.Cursor_getType(this.cursor, ordinal);




            // http://developer.android.com/reference/android/database/Cursor.html#FIELD_TYPE_INTEGER

            if (t == FIELD_TYPE_INTEGER)
            {
                return typeof(long);
            }

            if (t == FIELD_TYPE_STRING)
                return typeof(string);

            //            12-04 10:56:47.453: W/dalvikvm(18774): VFY: unable to resolve interface method 2121: Landroid/database/Cursor;.getType (I)I
            //12-04 10:56:47.453: D/dalvikvm(18774): VFY: replacing opcode 0x72 at 0x0002
            //12-04 10:56:47.453: D/dalvikvm(18774): VFY: dead code 0x0005-0029 in LScriptCoreLib/Android/BCLImplementation/System/Data/SQLite/__SQLiteDataReader;.GetFieldType (I)LScriptCoreLibJava/BCLImplementation/System/__Type;

            // TryGetMember error: { Message = GetFieldType fault { ordinal = 1, t = 0 },

            throw new NotImplementedException("GetFieldType fault " +
                new { ordinal, t, FIELD_TYPE_INTEGER, FIELD_TYPE_STRING }
            );
        }

        public override int FieldCount
        {
            get { return this.cursor.getColumnCount(); }
        }

        public override long GetBytes(int ordinal, long fieldOffset, byte[] buffer, int bufferoffset, int length)
        {

            // X:\jsc.svn\core\ScriptCoreLibAndroid\ScriptCoreLibAndroid\BCLImplementation\System\Data\SQLite\SQLiteDataReader.cs
            // // Get size of image data–pass null as the byte array parameter
            var value = (byte[])(object)this.cursor.getBlob(ordinal);

            if (length == 0)
                if (buffer == null)
                    return value.Length;



            // how much data we need to copy?

            // java.lang.ArrayIndexOutOfBoundsException: length=8022; index=8022

            var c = 0;
            for (int i = 0; i < length; i++)
            {
                if ((i + bufferoffset) < buffer.Length)
                    if ((i + fieldOffset) < value.Length)
                    {
                        c++;
                        buffer[i + bufferoffset] = value[i + fieldOffset];
                    }
            }

            return c;
        }

        public override double GetDouble(int ordinal)
        {
            throw new NotImplementedException();
        }
    }

}
