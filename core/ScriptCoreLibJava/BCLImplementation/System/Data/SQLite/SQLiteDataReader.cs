using ScriptCoreLib;
using ScriptCoreLib.Shared.BCLImplementation.System.Data.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ScriptCoreLibJava.BCLImplementation.System.Data.SQLite
{
    [Script(Implements = typeof(global::System.Data.SQLite.SQLiteDataReader))]
    internal class __SQLiteDataReader : __DbDataReader
    {
        // X:\jsc.svn\core\ScriptCoreLibAndroid\ScriptCoreLibAndroid\BCLImplementation\System\Data\SQLite\SQLiteDataReader.cs
        // X:\jsc.svn\core\ScriptCoreLib\PHP\BCLImplementation\System\Data\SQLite\SQLiteDataReader.cs

        public __SQLiteCommand InternalCommand;
        public java.sql.ResultSet InternalResultSet;
        // https://sites.google.com/a/jsc-solutions.net/backlog/knowledge-base/2012/20121001-solutionbuilderv1/20121014-gae-data
        // http://msdn.microsoft.com/en-us/library/ms379039.aspx


        public override void Close()
        {
            if (this.InternalResultSet == null)
                return;

            try
            {
                this.InternalResultSet.close();
            }
            catch
            {
                throw;
            }

        }

        public override bool Read()
        {
            if (this.InternalResultSet == null)
                return false;

            var value = default(bool);

            try
            {
                value = this.InternalResultSet.next();

            }
            catch
            {
                throw;
            }

            return value;
        }

        public override object this[string name]
        {
            get
            {

                var i = this.GetOrdinal(name);

                var t = this.GetFieldType(i);

                var ColumnType = InternalGetColumnType(i);

                Console.WriteLine("__SQLiteDataReader get_Item " + new { name, i, t.FullName, ColumnType });

                if (t == typeof(int))
                    return this.GetInt32(i);

                if (t == typeof(long))
                    return this.GetInt64(i);


                return this.GetString(i);
            }
        }

        public override int GetOrdinal(string name)
        {
            var value = default(int);
            try
            {
                value = this.InternalResultSet.findColumn(name) - 1;

            }
            catch
            {
                throw;
            }
            return value;
        }

        public override string GetString(int i)
        {
            var value = default(string);
            // the first column is 1
            try
            {
                value = this.InternalResultSet.getString(i + 1);


            }
            catch
            {
                throw;
            }
            return value;
        }

        public override int GetInt32(int i)
        {
            var value = default(int);
            try
            {
                value = this.InternalResultSet.getInt(i + 1);
            }
            catch
            {
                throw;
            }
            return value;
        }

        public override string GetName(int ordinal)
        {
            var r = default(string);

            try
            {
                r = this.InternalResultSet.getMetaData().getColumnName(ordinal + 1);
            }
            catch
            {
                throw;
            }

            return r;
        }

        public override long GetInt64(int ordinal)
        {
            var value = default(long);
            try
            {
                value = this.InternalResultSet.getLong(ordinal + 1);
            }
            catch
            {
                throw;
            }
            return value;
        }

        public override Type GetFieldType(int ordinal)
        {
            var ColumnType = InternalGetColumnType(ordinal);

            // GetFieldType unknown type: 3,
            // http://docs.oracle.com/javase/1.4.2/docs/api/constant-values.html#java.sql

            if (ColumnType == 4)
            {
                //int int32 = 0;
                //object int32box = int32;

                //// why are we using the boxed version of the type?
                //// jsc is giving us the primitive? 
                //return int32box.GetType();

                return typeof(int);
            }

            if (ColumnType == -5)
            {
                //long int64 = 0;
                //object int64box = int64;

                //return int64box.GetType();

                // jsc is giving us the primitive? 
                return typeof(long);
            }

            // In MySQL 4.1.x, the four TEXT types (TINYTEXT, TEXT, MEDIUMTEXT, and LONGTEXT) return 'blob" as field types, not "string".
            // how to fix that?

            // long varchar
            if (ColumnType == -1)
                return typeof(string);

            // CHAR
            if (ColumnType == 1)
                return typeof(string);

            if (ColumnType == 8)
                return typeof(double);

            if (ColumnType == 2004)
                return typeof(string);

            if (ColumnType == 91)
                return typeof(string);

            // timestamp
            if (ColumnType == 93)
                return typeof(string);

            if (ColumnType == 12)
                return typeof(string);

            // http://docs.oracle.com/javase/1.4.2/docs/api/constant-values.html#java.sql.Types.INTEGER
            var message = "GetFieldType fault: " + new { ColumnType, ordinal, InternalCommand.CommandText };

            throw new InvalidOperationException(message);
        }

        private int InternalGetColumnType(int ordinal)
        {
            var ColumnType = default(int);

            try
            {
                ColumnType = this.InternalResultSet.getMetaData().getColumnType(ordinal + 1);
            }
            catch
            {
                throw;
            }
            return ColumnType;
        }

        public override int FieldCount
        {
            get
            {
                var r = default(int);
                try
                {

                    r = this.InternalResultSet.getMetaData().getColumnCount();

                }
                catch
                {
                    throw;
                }
                return r;
            }
        }

        public override long GetBytes(int i, long fieldOffset, byte[] buffer, int bufferoffset, int length)
        {
            throw new NotImplementedException();
        }

        public override double GetDouble(int ordinal)
        {
            throw new NotImplementedException();
        }
    }
}
