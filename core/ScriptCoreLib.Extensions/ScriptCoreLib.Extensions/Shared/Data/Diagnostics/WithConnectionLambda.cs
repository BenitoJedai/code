using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScriptCoreLib.Shared.Data.Diagnostics
{
    public static class WithConnectionLambda
    {

        public static long GetInt64OrDefault(System.Data.DataRow e, string ColumnName)
        {
            if (e.Table.Columns.Contains(ColumnName))
            {
                return Convert.ToInt64(e[ColumnName]);
            }

            return default(long);
        }

        public static object ConvertDBNullToNullIfAny(object e)
        {
            // https://sites.google.com/a/jsc-solutions.net/backlog/knowledge-base/2013/201312/20131208-dbnull

            if (e is DBNull)
                return null;

            return e;
        }


        public static Func<Func<SQLiteConnection, Task>, Task> WithConnection(string DataSource)
        {
            // ScriptCoreLib.Extensions
            var csb = new SQLiteConnectionStringBuilder
            {
                DataSource = DataSource,

                // is there any other version?
                Version = 3
            };


            var yy = csb.AsWithConnection(Initializer: null);

            return y =>
            {
                var ret = default(Task);

                yy(
                    c =>
                    {
                        ret = y(c);
                    }
                );

                return ret;
            };
        }
    }
}
