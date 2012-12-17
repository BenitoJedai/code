using ScriptCoreLib.PHP.Data;
using ScriptCoreLib.PHP.Runtime;
using ScriptCoreLib.Shared.BCLImplementation.System.Data.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ScriptCoreLib.PHP.BCLImplementation.System.Data.SQLite
{
    [Script(Implements = typeof(global::System.Data.SQLite.SQLiteDataReader))]
    internal class __SQLiteDataReader : __DbDataReader
    {
        // X:\jsc.svn\core\ScriptCoreLibJava\BCLImplementation\System\Data\SQLite\SQLiteDataReader.cs

        public mysqli_result InternalResultSet;

        public override void Close()
        {
            this.InternalResultSet.close();
        }

        public int __rowid = -1;
        public object[] __row;

        public override bool Read()
        {
            if (this.InternalResultSet == null)
                return false;

            __rowid++;

            if (__rowid < this.InternalResultSet.num_rows)
            {
                if (this.InternalResultSet.data_seek(__rowid))
                {
                    this.__row = this.InternalResultSet.fetch_row();
                    return true;
                }
            }

            return false;
        }

        public override int GetOrdinal(string name)
        {
            var i = -1;

            for (int j = 0; j < this.InternalResultSet.field_count; j++)
            {
                if (this.InternalResultSet.fetch_field_direct(j).name == name)
                {
                    i = j;
                    break;
                }
            }

            return i;
        }

        public override object this[string name]
        {
            get
            {
                var i = GetOrdinal(name);
                var t = GetFieldType(i);

                if (t == typeof(double))
                {
                    return GetDouble(i);
                }

                if (t == typeof(int))
                {
                    return GetInt32(i);
                }

                return GetString(i);
            }
        }

        public override string GetString(int i)
        {
            return (string)this.__row[i];

        }



        public override string GetName(int ordinal)
        {
            var f = this.InternalResultSet.fetch_field_direct(ordinal);
            return f.name;
        }

        public override int GetInt32(int i)
        {
            return (int)this.__row[i];

        }


        public override long GetInt64(int i)
        {
            return (long)this.__row[i];

        }

        public override double GetDouble(int i)
        {
            return (double)this.__row[i];
        }

        public override Type GetFieldType(int ordinal)
        {
            var t = this.InternalResultSet.fetch_field_direct(ordinal);


            if (t.type == 3)
                return typeof(int);



            return typeof(string);
        }

        public override int FieldCount
        {
            get
            {
                return this.InternalResultSet.field_count;
            }
        }

        public override long GetBytes(int i, long fieldOffset, byte[] buffer, int bufferoffset, int length)
        {
            throw new Exception("GetBytes");
        }
    }

}
