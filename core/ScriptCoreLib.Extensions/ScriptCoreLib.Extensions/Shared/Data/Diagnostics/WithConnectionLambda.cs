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

                        Console.WriteLine();

                        // script: error JSC1000: No implementation found for this native method, please implement [System.Exception.get_StackTrace()]
                        // https://sites.google.com/a/jsc-solutions.net/work/knowledge-base/04-monese/2014/201401/20140101
                        var text = "ScriptCoreLib.Extensions::ScriptCoreLib.Shared.Data.Diagnostics.WithConnectionLambda.WithConnection\n\n error: "
                            + new
                            {
                                ex.Message,
                                ex,
                                ex.StackTrace
                            };

                        Console.WriteLine(text);

                        Debugger.Break();

                        throw new InvalidOperationException(text);
                    }
                }

                return ret;
            };
        }
    }
}
