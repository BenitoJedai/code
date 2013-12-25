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

        public static long GetInt64OrDefault(System.Data.DataRow e, string ColumnName, long defaultValue)
        {
            if (e.Table.Columns.Contains(ColumnName))
            {
                return Convert.ToInt64(e[ColumnName]);
            }

            return defaultValue;
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
            var csb = new SQLiteConnectionStringBuilder
            {
                DataSource = DataSource,

                // is there any other version?
                Version = 3
            };

            return y =>
            {
                var ret = default(Task);

                using (var c = new SQLiteConnection(csb.ConnectionString))
                {
                    c.Open();

                    try
                    {
                        ret = y(c);
                    }
                    catch (Exception ex)
                    {
                        // ex.Message = "SQL logic error or missing database\r\nno such function: concat"
                        // ex = {"Could not load file or assembly 'System.Data.SQLite, Version=1.0.86.0, Culture=neutral, PublicKeyToken=db937bc2d44ff139' or one of its dependencies. The located assembly's manifest definition does not match the assembly reference. (Exception from HRESULT:...
                        // ex.Message = "SQL logic error or missing database\r\nno such table: Sheet2"
                        // table Book1.Sheet1 has no column named Sheet2
                        //Console.WriteLine(new { ex.Message, ex.StackTrace });
                        Console.WriteLine("WithConnectionLambda.WithConnection error: " + new { ex.Message, ex });
                        Debugger.Break();
                    }
                }

                return ret;
            };
        }
    }
}
