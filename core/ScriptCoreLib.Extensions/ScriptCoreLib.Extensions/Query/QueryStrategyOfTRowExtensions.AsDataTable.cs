using ScriptCoreLib.Shared.BCLImplementation.System.Data.Common;
using ScriptCoreLib.Shared.Data.Diagnostics;
using ScriptCoreLib.Extensions;
using ScriptCoreLib.Shared.Lambda;
using ScriptCoreLib.Ultra.Library;

using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.SQLite;
using System.Diagnostics;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using System.Threading;

namespace System.Data
{
    // move to a nuget?
    // shall reimplement IQueriable for jsc data layer gen
    //[Obsolete("the first generic extension method for all jsc data layer rows")]
    public static partial class QueryStrategyOfTRowExtensions
    {
        static DataTable InternalAsDataTable(IQueryStrategy Strategy)
        {
            // X:\jsc.svn\examples\javascript\forms\Test\TestSQLiteGroupBy\TestSQLiteGroupBy\ApplicationWebService.cs

            // will it work for jvm?
            var st = new StackTrace(0, true);
            var sw = Stopwatch.StartNew();

            Console.WriteLine("AsDataTable " + new { Strategy });

            if (Strategy == null)
            {
                if (Debugger.IsAttached)
                    Debugger.Break();

                throw new ArgumentNullException("Strategy");
            }

            //System.Diagnostics.Contracts.Contract.Assume

            var value = ((Task<DataTable>)Strategy.GetDescriptor().GetWithConnection()(
                c =>
                {
                    var state = QueryStrategyExtensions.AsCommandBuilder(Strategy);

                    //var cmd = new SQLiteCommand(state.ToString(), c);

                    var cmd = (DbCommand)c.CreateCommand();


                    //ex = {"no such column: dealer.Key"}
                    cmd.CommandText = state.ToString();

                    // https://sites.google.com/a/jsc-solutions.net/backlog/knowledge-base/2014/201405/20140501

                    foreach (var item in state.ApplyParameter)
                    {
                        item(cmd);
                    }




                    // X:\jsc.svn\core\ScriptCoreLib\Shared\BCLImplementation\System\Data\Common\DbDataAdapter.cs
                    // will this work under CLR too?

                    // http://stackoverflow.com/questions/12608025/how-to-construct-a-sqlite-query-to-group-by-order
                    // http://www.devart.com/dotconnect/sqlite/docs/Devart.Data.SQLite~Devart.Data.SQLite.SQLiteDataReader~NextResult.html
                    // http://www.maplesoft.com/support/help/Maple/view.aspx?path=Database/Statement/NextResult
                    // To issue a multi-statement SQL string, the Execute command must be used.
                    //  Some databases may require that the processing of the current result be completed before the next result is returned by NextResult.
                    // http://www.java2s.com/Tutorial/CSharp/0560__ADO.Net/ExecutingaQueryThatReturnsMultipleResultSetswithSqlDataReader.htm
                    // http://amitchandnz.wordpress.com/2011/09/28/issues-with-idatareaderdatareader-multiple-results-sets-and-datatables/
                    // http://stuff.mit.edu/afs/athena/software/mono_v3.0/arch/i386_linux26/mono/mcs/class/Mono.Data.Sqlite/Mono.Data.Sqlite_2.0/SQLiteDataReader.cs
                    // http://zetcode.com/db/sqlitecsharp/read/
                    // http://stackoverflow.com/questions/18493169/sqlite-query-combining-two-result-sets-that-use-and
                    // http://www.sqlite.org/queryplanner.html
                    // One possible solution is to fetch all events, to a ToList() and do the grouping in-memory.
                    // http://blog.csainty.com/2008/01/linq-to-sql-groupby.html
                    // http://msdn.microsoft.com/en-us/library/vstudio/bb386922(v=vs.100).aspx

                    //var reader = cmd.ExecuteReader();
                    ////var reader = cmd.ExecuteReader();

                    ////Console.WriteLine(
                    ////    new
                    ////    {
                    ////        reader.Depth,
                    ////        reader.FieldCount
                    ////        //reader.NextResult

                    ////    }
                    ////);

                    //var a = new SQLiteDataAdapter(cmd);

                    // http://msdn.microsoft.com/en-us/library/bh8kx08z(v=vs.110).aspx

                    var a = new __DbDataAdapter { SelectCommand = cmd };
                    //var a = new SQLiteDataAdapter { SelectCommand = cmd };

                    //System.Data.XSQLite.dll!Community.CsharpSqlite.Sqlite3.fetchPayload(Community.CsharpSqlite.Sqlite3.BtCursor pCur, ref int pAmt, ref int outOffset, bool skipKey)	Unknown
                    //System.Data.XSQLite.dll!Community.CsharpSqlite.Sqlite3.sqlite3BtreeKeyFetch(Community.CsharpSqlite.Sqlite3.BtCursor pCur, ref int pAmt, ref int outOffset)	Unknown
                    //System.Data.XSQLite.dll!Community.CsharpSqlite.Sqlite3.sqlite3VdbeExec(Community.CsharpSqlite.Sqlite3.Vdbe p)	Unknown
                    //System.Data.XSQLite.dll!Community.CsharpSqlite.Sqlite3.sqlite3Step(Community.CsharpSqlite.Sqlite3.Vdbe p)	Unknown
                    //System.Data.XSQLite.dll!Community.CsharpSqlite.Sqlite3.sqlite3_step(Community.CsharpSqlite.Sqlite3.Vdbe pStmt)	Unknown
                    //System.Data.XSQLite.dll!System.Data.SQLite.SQLiteCommand.ExecuteStatement(Community.CsharpSqlite.Sqlite3.Vdbe pStmt, out int cols, out System.IntPtr pazValue, out System.IntPtr pazColName)	Unknown
                    //System.Data.XSQLite.dll!System.Data.SQLite.SQLiteDataReader.ReadpVm(Community.CsharpSqlite.Sqlite3.Vdbe pVm, int version, System.Data.SQLite.SQLiteCommand cmd)	Unknown
                    //System.Data.XSQLite.dll!System.Data.SQLite.SQLiteDataReader.SQLiteDataReader(System.Data.SQLite.SQLiteCommand cmd, Community.CsharpSqlite.Sqlite3.Vdbe pVm, int version)	Unknown
                    //System.Data.XSQLite.dll!System.Data.SQLite.SQLiteCommand.ExecuteReader(System.Data.CommandBehavior behavior, bool want_results, out int rows_affected)	Unknown
                    //System.Data.XSQLite.dll!System.Data.SQLite.SQLiteCommand.ExecuteReader(System.Data.CommandBehavior behavior)	Unknown
                    //System.Data.XSQLite.dll!System.Data.SQLite.SQLiteCommand.ExecuteDbDataReader(System.Data.CommandBehavior behavior)	Unknown
                    //System.Data.dll!System.Data.Common.DbCommand.ExecuteReader()	Unknown
                    //ScriptCoreLib.dll!ScriptCoreLib.Shared.BCLImplementation.System.Data.Common.__DbDataAdapter.Fill(System.Data.DataTable dataTable)	Unknown

                    var ss = Stopwatch.StartNew();

                    Console.WriteLine("before Fill");
                    var t = new DataTable();
                    //var ds = new DataSet();
                    a.Fill(t);
                    // is SQLIte Fill handicapped or what?

                    //a.Fill(ds);
                    //Console.WriteLine("after Fill " + new { ss.ElapsedMilliseconds, t.Rows.Count });
                    Console.WriteLine("after Fill " + new { ss.ElapsedMilliseconds });


                    var s = new TaskCompletionSource<DataTable>();
                    //s.SetResult(ds.Tables[0]);
                    s.SetResult(t);

                    return s.Task;
                }
            )).Result;

            var caller = st.GetFrame(1);
            Console.WriteLine(
                Process.GetCurrentProcess().Id.ToString("x4") + ":"
                + Thread.CurrentThread.ManagedThreadId.ToString("x4")
                + " AsDataTable " + new { sw.ElapsedMilliseconds, Debugger.IsAttached, caller = caller.ToString() });

            return value;
        }



        // referenced by the asset compiler
        public static DataTable AsDataTable<TElement>(this IQueryStrategy<TElement> Strategy)
        {
            return InternalAsDataTable(Strategy);
        }

    }
}

