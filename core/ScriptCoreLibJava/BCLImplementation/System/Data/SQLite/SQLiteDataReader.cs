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
        public java.sql.ResultSet InternalResultSet;
        // https://sites.google.com/a/jsc-solutions.net/backlog/knowledge-base/2012/20121001-solutionbuilderv1/20121014-gae-data
        // http://msdn.microsoft.com/en-us/library/ms379039.aspx


        public override void Close()
        {

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
    }
}
