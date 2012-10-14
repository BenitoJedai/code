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
            this.InternalResultSet.close();
        }

        public override bool Read()
        {
            return this.InternalResultSet.next();
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
            return this.InternalResultSet.findColumn(name) - 1;
        }

        public override string GetString(int i)
        {
            // the first column is 1
            return this.InternalResultSet.getString(i + 1);
        }

        public override int GetInt32(int i)
        {
            return this.InternalResultSet.getInt(i + 1);
        }
    }
}
