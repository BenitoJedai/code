using ScriptCoreLib.PHP.Data;
using ScriptCoreLib.PHP.Runtime;
using ScriptCoreLib.Shared.BCLImplementation.System.Data.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ScriptCoreLib.PHP.BCLImplementation.System.Data.SQLite
{
    //[Script(Implements = typeof(global::System.Data.SQLite.SQLiteDataReader))]
    internal class __SQLiteDataReader : __DbDataReader
    {
        // tested by ?


        // X:\jsc.svn\core\ScriptCoreLibJava\BCLImplementation\System\Data\SQLite\SQLiteDataReader.cs

        public mysqli_result InternalResultSet;

        // tested by X:\jsc.svn\examples\php\Test\mysqli_stmt_xget_result\mysqli_stmt_xget_result\ApplicationWebService.cs
        public mysqli_stmt InternalStatement;

        public override void Close()
        {
            if (this.InternalStatement != null)
            {
                this.InternalStatement.free_result();

                return;
            }

            this.InternalResultSet.close();
        }

        public override int FieldCount
        {
            get
            {
                if (this.InternalStatement != null)
                {
                    return this.InternalStatement.field_count;
                }

                return this.InternalResultSet.field_count;
            }
        }

        public int __rowid = -1;
        public object[] __row;

        public override bool Read()
        {
            if (this.InternalStatement != null)
            {
                __rowid++;

                if (__rowid < this.InternalStatement.num_rows)
                {
                    this.__row = this.InternalStatement.__fetch_array();
                    return true;
                }

                return false;
            }

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


        public override string GetName(int ordinal)
        {
            var r = this.InternalResultSet;

            if (this.InternalStatement != null)
            {
                r = this.InternalStatement.result_metadata();
            }

            var f = r.fetch_field_direct(ordinal);

            return f.name;
        }

        public override Type GetFieldType(int ordinal)
        {
            var r = this.InternalResultSet;

            if (this.InternalStatement != null)
            {
                r = this.InternalStatement.result_metadata();
            }


            var t = r.fetch_field_direct(ordinal);


            if (t.type == 3)
                return typeof(int);



            return typeof(string);
        }


        public override int GetOrdinal(string name)
        {
            var r = this.InternalResultSet;

            if (this.InternalStatement != null)
            {
                r = this.InternalStatement.result_metadata();
            }

            var i = -1;

            for (int j = 0; j < r.field_count; j++)
            {
                if (r.fetch_field_direct(j).name == name)
                {
                    i = j;
                    break;
                }
            }

            return i;
        }

        public override object this[int i]
        {
            get
            {
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
        public override object this[string name]
        {
            get
            {
                var i = GetOrdinal(name);
                return this[i];
            }
        }

        public override string GetString(int i)
        {
            return (string)this.__row[i];

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




        public override long GetBytes(int i, long fieldOffset, byte[] buffer, int bufferoffset, int length)
        {
            throw new Exception("GetBytes");
        }
    }

}
