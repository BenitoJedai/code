using ScriptCoreLib;
using ScriptCoreLib.Shared.BCLImplementation.System.Data.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ScriptCoreLibJava.BCLImplementation.System.Data.SQLite
{
    //type: System.Data.MySQL.MySQLConnectionStringBuilder
    // X:\jsc.svn\examples\javascript\LINQ\test\auto\TestSelect\TestAppEngineOrderByThenGroupBy\ApplicationWebService.cs

    //[Script(Implements = typeof(global::System.Data.SQLite.SQLiteDataReader))]
    [Script(ImplementsViaAssemblyQualifiedName = "System.Data.MySQL.MySQLDataReader")]
    internal class __MySQLDataReader : __DbDataReader
    {
        // X:\jsc.svn\core\ScriptCoreLibAndroid\ScriptCoreLibAndroid\BCLImplementation\System\Data\SQLite\SQLiteDataReader.cs
        // X:\jsc.svn\core\ScriptCoreLib\PHP\BCLImplementation\System\Data\SQLite\SQLiteDataReader.cs

        public __MySQLCommand InternalCommand;
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

        public override object this[int i]
        {
            get
            {
                var t = this.GetFieldType(i);

                var ColumnType = InternalGetColumnType(i);

                //Console.WriteLine("__SQLiteDataReader get_Item " + new { name, i, t.FullName, ColumnType });

                if (t == typeof(int))
                    return this.GetInt32(i);

                if (t == typeof(long))
                    return this.GetInt64(i);


                return this.GetString(i);
            }
        }

        public override object this[string name]
        {
            get
            {

                var i = this.GetOrdinal(name);


                return this[i];
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
                // http://bugs.mysql.com/bug.php?id=43684
                //  JDBC-compliant way of getting the information you're asking for, i.e. the "alias" for the column is by calling ResultSetMetaData.getColumnLabel(),
                // X:\jsc.svn\examples\javascript\forms\Test\TestSQLiteGroupBy\TestSQLiteGroupBy\ApplicationWebService.cs

                //r = this.InternalResultSet.getMetaData().getColumnName(ordinal + 1);
                r = this.InternalResultSet.getMetaData().getColumnLabel(ordinal + 1);
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

            if (ColumnType == 3)
            {
                // X:\jsc.svn\examples\javascript\appengine\AppEngineWhereOperator\AppEngineWhereOperator\ApplicationWebService.cs
                return typeof(long);
            }

            if (ColumnType == -5)
            {
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
            if (ColumnType == 2005)
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

        public override long GetBytes(int ordinal, long fieldOffset, byte[] buffer, int bufferoffset, int length)
        {
            //Caused by: java.lang.RuntimeException
            //        at ScriptCoreLibJava.BCLImplementation.System.Data.SQLite.__SQLiteDataReader.GetBytes(__SQLiteDataReader.java:298)
            //        at ScriptCoreLibJava.BCLImplementation.System.Data.SQLite.__SQLiteDataReader.System_Data_IDataRecord_GetBytes(__SQLiteDataReader.java:325)
            //        at ScriptCoreLib.Shared.Data.DynamicDataReader.GetBytes(DynamicDataReader.java:114)
            //        at ScriptCoreLib.Shared.Data.DynamicDataReader.System_Data_IDataRecord_GetBytes(DynamicDataReader.java:438)
            //        at com.abstractatech.analytics.ApplicationWebService___c__DisplayClass4e._Handler_b__35(ApplicationWebService___c__DisplayClass4e.java:97)
            //        ... 79 more



            // X:\jsc.svn\core\ScriptCoreLibAndroid\ScriptCoreLibAndroid\BCLImplementation\System\Data\SQLite\SQLiteDataReader.cs
            // // Get size of image data–pass null as the byte array parameter
            var value = default(byte[]);

            try
            {
                value = (byte[])(object)this.InternalResultSet.getBytes(ordinal + 1);
            }
            catch { throw; }


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
