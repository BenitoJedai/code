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



// change namespace?
namespace ScriptCoreLib.Shared.Data.Diagnostics
{


    [Obsolete("we need to refactor this into a jsc market nuget. can this nuget also embedd the asset compiler for jsc?")]
    public static class QueryStrategyExtensions
    {
        // X:\jsc.svn\examples\javascript\forms\Test\TestSQLJoin\TestSQLJoin\ApplicationWebService.cs

        // SQLite.Linq reference implementation
        // when can we have immutable version?

        // http://msdn.microsoft.com/en-us/library/bb310804.aspx

        #region where
        // behave like StringBuilder where core data is mutable?
        public static void MutableWhere(IQueryStrategy that, LambdaExpression filter)
        {


            // to make it immutable, we would need to have Clone method
            // https://sites.google.com/a/jsc-solutions.net/backlog/knowledge-base/2014/201401/20140112/count
            // https://sites.google.com/a/jsc-solutions.net/backlog/knowledge-base/2014/201405/20140515

            // X:\jsc.svn\examples\javascript\Test\TestIQueryable\TestIQueryable\ApplicationWebService.cs
            // X:\jsc.svn\examples\javascript\svg\SVGNavigationTiming\SVGNavigationTiming\ApplicationWebService.cs
            // X:\jsc.svn\examples\javascript\forms\Test\TestSQLiteEnumWhere\TestSQLiteEnumWhere\ApplicationWebService.cs





            //Additional information: Unable to cast object of type 'System.Linq.Expressions.UnaryExpression' to type 'System.Linq.Expressions.MemberExpression'.

            //Additional information: Unable to cast object of type 'System.Linq.Expressions.UnaryExpression' to type 'System.Linq.Expressions.MemberExpression'.

            // http://stackoverflow.com/questions/9241607/whats-wrong-with-system-linq-expressions-logicalbinaryexpression-class
            //var f_Body_Left_as_MemberExpression = (MemberExpression)body.Left;
            //var f_Body_Right_as_ConstantExpression = (ConstantExpression)f_Body_as_BinaryExpression.Right;




            // for non op_Equals
            //var f_Body_as_MethodCallExpression = ((MethodCallExpression)f.Body);
            ////Console.WriteLine("IBook1Sheet1Queryable.Where");

            //var f_Body_Left_as_MemberExpression = (MemberExpression)f_Body_as_MethodCallExpression.Arguments[0];
            //var f_Body_Right_as_ConstantExpression = (ConstantExpression)f_Body_as_MethodCallExpression.Arguments[1];

            //Console.WriteLine("IBook1Sheet1Queryable.Where " + new { f_Body_as_MethodCallExpression.Method, f_Body_Left_as_MemberExpression.Member.Name, f_Body_Right_as_ConstantExpression.Value });
            Console.WriteLine("MutableWhere "
            //    + new
            //{
            //    body.Method,

            //    //NodeType	Equal	System.Linq.Expressions.ExpressionType
            //    body.NodeType,


            //    ColumnName = lColumnName0,
            //    Right = rAddParameterValue0
            //}
            );


            that.GetCommandBuilder().Add(
                state =>
                {
                    //MutableWhere { Method = Boolean op_Equality(System.String, System.String), Left = Goo, Right = Goo0 }


                    // what about multple where clauses, what about sub queries?
                    // X:\jsc.svn\examples\javascript\forms\Test\TestSQLiteEnumWhere\TestSQLiteEnumWhere\ApplicationWebService.cs

                    // state.WhereCommand = " where `FooStateEnum` = @arg0"


                    if (string.IsNullOrEmpty(state.WhereCommand))
                    {
                        // this is the first where clause

                        state.WhereCommand = " where ";

                    }
                    else
                    {
                        // this wants to add to the where clause
                        // http://www.w3schools.com/sql/sql_and_or.asp

                        state.WhereCommand += " and ";
                    }





                    // x:\jsc.svn\examples\javascript\linq\minmaxaverageexperiment\minmaxaverageexperiment\applicationwebservice.cs

                    //-filter.Body { Not(IsNullOrEmpty(k.path))}
                    //System.Linq.Expressions.Expression { System.Linq.Expressions.UnaryExpression}

                    var asUnaryExpression = filter.Body as UnaryExpression;
                    if (asUnaryExpression != null)
                    {
                        // Operand = {IsNullOrEmpty(k.path)}

                        if (asUnaryExpression.NodeType == ExpressionType.Not)
                        {


                            var asMethodCallExpression = asUnaryExpression.Operand as MethodCallExpression;
                            if (asMethodCallExpression != null)
                            {
                                if (asMethodCallExpression.Method.Name == "IsNullOrEmpty")
                                {
                                    // http://stackoverflow.com/questions/8054942/string-isnullorempty-in-linq-to-sql-query
                                    // http://connect.microsoft.com/VisualStudio/feedback/details/367077/i-want-to-use-string-isnullorempty-in-linq-to-sql-statements
                                    // http://stackoverflow.com/questions/15663207/how-to-use-null-or-empty-string-in-sql
                                    // x:\jsc.svn\examples\javascript\linq\minmaxaverageexperiment\minmaxaverageexperiment\applicationwebservice.cs

                                    var arg1 = asMethodCallExpression.Arguments[0] as MemberExpression;

                                    var xColumnName0 = arg1.Member.Name;


                                    state.WhereCommand += "not(";
                                    state.WhereCommand += "`" + xColumnName0 + "` is null or length(`" + xColumnName0 + "`) = 0";
                                    state.WhereCommand += ")";
                                    return;
                                }
                            }
                        }
                    }


                    // for op_Equals
                    var body = ((BinaryExpression)filter.Body);

                    // do we need to check our db schema or is reflection the schema for us?



                    #region WriteExpression
                    Action<BinaryExpression> WriteExpression =
                        (xbody) =>
                        {
                            var xbody_left = xbody.Left as UnaryExpression;
                            var xColumnName0 = (xbody_left.Operand as MemberExpression).Member.Name;


                            state.WhereCommand += "`" + xColumnName0 + "` ";

                            if (xbody.NodeType == ExpressionType.Equal)
                                state.WhereCommand += "=";
                            else if (xbody.NodeType == ExpressionType.LessThan)
                                state.WhereCommand += "<";
                            else if (xbody.NodeType == ExpressionType.GreaterThan)
                                state.WhereCommand += ">";
                            else
                                Debugger.Break();

                            // -		(new System.Linq.Expressions.Expression.BinaryExpressionProxy(x_asLogicalBinaryExpression0 as System.Linq.Expressions.LogicalBinaryExpression)).Right	{0}	System.Linq.Expressions.Expression {System.Linq.Expressions.ConstantExpression}


                            var asConstantExpression = xbody.Right as ConstantExpression;
                            if (asConstantExpression != null)
                            {
                                var rAddParameterValue0 = asConstantExpression.Value;
                                var n = "@arg" + state.ApplyParameter.Count;

                                state.WhereCommand += " ";
                                state.WhereCommand += n;

                                Console.WriteLine("MutableWhere OrElse " + new { n, rAddParameterValue0 });

                                state.ApplyParameter.Add(
                                    c =>
                                    {
                                        // either the actualt command or the explain command?

                                        //c.Parameters.AddWithValue(n, r);
                                        c.AddParameter(n, rAddParameterValue0);
                                    }
                                );
                                return;
                            }

                            Debugger.Break();
                        };
                    #endregion

                    if (body.NodeType == ExpressionType.OrElse)
                    {
                        state.WhereCommand += "(";
                        WriteExpression(body.Left as BinaryExpression);
                        state.WhereCommand += " or ";
                        WriteExpression(body.Right as BinaryExpression);
                        state.WhereCommand += ")";

                        //Debugger.Break();
                    }
                    else
                    {

                        var lColumnName0 = "";

                        var rAddParameterValue0 = default(object);


                        #region ColumnName

                        if (body.Left is MemberExpression)
                        {
                            lColumnName0 = ((MemberExpression)body.Left).Member.Name;
                        }
                        else if (body.Left is UnaryExpression)
                        {
                            lColumnName0 = ((MemberExpression)((UnaryExpression)body.Left).Operand).Member.Name;
                        }
                        else
                        {
                            // +		filter	{z => ((Convert(z.FooStateEnum) == 0) OrElse (Convert(z.FooStateEnum) == 2))}	System.Linq.Expressions.LambdaExpression {System.Linq.Expressions.Expression<System.Func<TestSQLiteGroupBy.Data.Book1MiddleRow,bool>>}

                            //                +		body.Left	{(Convert(z.FooStateEnum) == 0)}	System.Linq.Expressions.Expression {System.Linq.Expressions.LogicalBinaryExpression}
                            //+		body.Right	{(Convert(z.FooStateEnum) == 2)}	System.Linq.Expressions.Expression {System.Linq.Expressions.LogicalBinaryExpression}
                            //        body.NodeType	OrElse	System.Linq.Expressions.ExpressionType



                            Debugger.Break();
                        }
                        #endregion

                        state.WhereCommand += "`" + lColumnName0 + "` ";


                        #region rAddParameterValue

                        if (body.Right is MemberExpression)
                        {
                            var f_Body_Right = (MemberExpression)body.Right;

                            //+		(new System.Linq.Expressions.Expression.ConstantExpressionProxy((new System.Linq.Expressions.Expression.MemberExpressionProxy(f_Body_Right as System.Linq.Expressions.FieldExpression)).Expression as System.Linq.Expressions.ConstantExpression)).Value	{AppEngineWhereOperator.ApplicationWebService.}	object {AppEngineWhereOperator.ApplicationWebService.}

                            // +		(new System.Linq.Expressions.Expression.MemberExpressionProxy(f_Body_Right.Expression as System.Linq.Expressions.FieldExpression)).Member	{SVGNavigationTiming.Design.PerformanceResourceTimingData2ApplicationResourcePerformanceRow k}	System.Reflection.MemberInfo {System.Reflection.RtFieldInfo}



                            var f_Body_Right_as_ConstantExpression = f_Body_Right.Expression as ConstantExpression;
                            var f_Body_Right_as_MemberExpression = f_Body_Right.Expression as MemberExpression;
                            if (f_Body_Right_as_ConstantExpression != null)
                            {

                                var f_Body_Right_Expression_Value = f_Body_Right_as_ConstantExpression.Value;
                                rAddParameterValue0 = ((FieldInfo)f_Body_Right.Member).GetValue(f_Body_Right_Expression_Value);
                            }
                            else if (f_Body_Right_as_MemberExpression != null)
                            {
                                // we are doing a where against object field passed method argument

                                var z = (FieldInfo)f_Body_Right_as_MemberExpression.Member;

                                var zE = f_Body_Right_as_MemberExpression.Expression as ConstantExpression;

                                var f_Body_Right_Expression_Value = z.GetValue(zE.Value);


                                rAddParameterValue0 = ((FieldInfo)f_Body_Right.Member).GetValue(f_Body_Right_Expression_Value);
                            }
                            else Debugger.Break();

                        }
                        else if (body.Right is UnaryExpression)
                        {
                            // casting enum to long?

                            var f_Body_Right = (MemberExpression)((UnaryExpression)body.Right).Operand;

                            var f_Body_Right_as_ConstantExpression = f_Body_Right.Expression as ConstantExpression;
                            var f_Body_Right_as_MemberExpression = f_Body_Right.Expression as MemberExpression;

                            //var f_Body_Right_Expression = (ConstantExpression)f_Body_Right.Expression;

                            //var f_Body_Right_Expression_Value = f_Body_Right_Expression.Value;
                            //r = ((FieldInfo)f_Body_Right.Member).GetValue(f_Body_Right_Expression_Value);

                            if (f_Body_Right_as_ConstantExpression != null)
                            {

                                var f_Body_Right_Expression_Value = f_Body_Right_as_ConstantExpression.Value;
                                rAddParameterValue0 = ((FieldInfo)f_Body_Right.Member).GetValue(f_Body_Right_Expression_Value);
                            }
                            else if (f_Body_Right_as_MemberExpression != null)
                            {
                                // we are doing a where against object field passed method argument

                                var z = (FieldInfo)f_Body_Right_as_MemberExpression.Member;

                                var zE = f_Body_Right_as_MemberExpression.Expression as ConstantExpression;

                                var f_Body_Right_Expression_Value = z.GetValue(zE.Value);


                                rAddParameterValue0 = ((FieldInfo)f_Body_Right.Member).GetValue(f_Body_Right_Expression_Value);
                            }
                            else Debugger.Break();
                        }
                        else
                        {
                            // https://sites.google.com/a/jsc-solutions.net/backlog/knowledge-base/2014/201405/201405

                            var asConstantExpression = body.Right as ConstantExpression;
                            if (asConstantExpression != null)
                            {
                                rAddParameterValue0 = asConstantExpression.Value;
                            }
                            else Debugger.Break();
                        }
                        #endregion

                        // like we do in jsc. this is the opcode
                        //OpCodes.Ceq
                        if (body.NodeType == ExpressionType.Equal)
                            state.WhereCommand += "=";
                        else if (body.NodeType == ExpressionType.LessThan)
                            state.WhereCommand += "<";
                        else if (body.NodeType == ExpressionType.GreaterThan)
                            state.WhereCommand += ">";
                        else if (body.NodeType == ExpressionType.NotEqual)
                            state.WhereCommand += "<>";
                        else
                            Debugger.Break();

                        var n = "@arg" + state.ApplyParameter.Count;

                        state.WhereCommand += " ";
                        state.WhereCommand += n;

                        Console.WriteLine("MutableWhere " + new { n, r = rAddParameterValue0 });

                        state.ApplyParameter.Add(
                            c =>
                            {
                                // either the actualt command or the explain command?

                                //c.Parameters.AddWithValue(n, r);
                                c.AddParameter(n, rAddParameterValue0);
                            }
                        );

                    }

                }
            );
        }

        #endregion




        #region select sum
        // can this be used in a join?
        [Obsolete("this is somewhat like select foo and then sum, or like orderby. what about summing vec3"
            )]
        public static long Sum(IQueryStrategy Strategy, Expression selector)
        {
            // http://stackoverflow.com/questions/3785995/sqlite-accumulator-sum-column-in-a-select-statement
            // http://www.tutorialspoint.com/sqlite/sqlite_useful_functions.htm
            //throw new NotImplementedException("sqlite does not have it yet");
            // http://sqlite.1065341.n5.nabble.com/SUM-and-NULL-values-td2257.html

            var body = ((MemberExpression)((LambdaExpression)selector).Body);

            // do we need to check our db schema or is reflection the schema for us?
            #region ColumnName
            var ColumnName = "";

            ColumnName = body.Member.Name;
            #endregion



            return ((Task<long>)Strategy.GetDescriptor().GetWithConnection()(
                c =>
                {
                    var state = AsCommandBuilder(Strategy);

                    // override
                    state.SelectCommand = "select sum(`" + ColumnName + "`) ";

                    //var cmd = new SQLiteCommand(state.ToString(), c);
                    var cmd = c.CreateCommand(state.ToString());

                    foreach (var item in state.ApplyParameter)
                    {
                        item(cmd);
                    }

                    var s = new TaskCompletionSource<long>();


                    s.SetResult(
                    (long)cmd.ExecuteScalar()
                    );

                    //var r = cmd.ExecuteReader();

                    //if (r.NextResult())
                    //{
                    //    //ex = {"No current row"}
                    //    s.SetResult(
                    //        r.GetInt64(0)
                    //    );
                    //}

                    return s.Task;
                }
            )).Result;
        }
        #endregion





        #region order by
        public static void MutableOrderBy(IQueryStrategy that, Expression selector)
        {
            #region ColumnName
            var ColumnName = "";

            // +		Member	{System.String path}	System.Reflection.MemberInfo {System.Reflection.RtFieldInfo}
            var body = ((LambdaExpression)selector).Body;

            // unpack the convert?
            var body_as_UnaryExpression = body as UnaryExpression;
            var body_as_MemberExpression = body as MemberExpression;
            if (body_as_UnaryExpression != null)
            {
                ColumnName = ((MemberExpression)(body_as_UnaryExpression).Operand).Member.Name;
            }
            else if (body_as_MemberExpression != null)
            {
                ColumnName = body_as_MemberExpression.Member.Name;
            }
            else Debugger.Break();
            #endregion

            Console.WriteLine("MutableOrderBy " + new { ColumnName });

            that.GetCommandBuilder().Add(
             state =>
             {
                 //MutableWhere { Method = Boolean op_Equality(System.String, System.String), Left = Goo, Right = Goo0 }


                 state.OrderByCommand = "order by `" + ColumnName + "`";
             }
            );
        }

        public static void MutableOrderByDescending(IQueryStrategy that, Expression selector)
        {


            // do we need to check our db schema or is reflection the schema for us?
            #region ColumnName
            var ColumnName = "";

            // +		Member	{System.String path}	System.Reflection.MemberInfo {System.Reflection.RtFieldInfo}
            var body = ((LambdaExpression)selector).Body;

            // unpack the convert?
            var body_as_UnaryExpression = body as UnaryExpression;
            var body_as_MemberExpression = body as MemberExpression;
            if (body_as_UnaryExpression != null)
            {
                ColumnName = ((MemberExpression)(body_as_UnaryExpression).Operand).Member.Name;
            }
            else if (body_as_MemberExpression != null)
            {
                ColumnName = body_as_MemberExpression.Member.Name;
            }
            else Debugger.Break();
            #endregion

            Console.WriteLine("MutableOrderByDescending " + new { ColumnName });

            that.GetCommandBuilder().Add(
             state =>
             {
                 //MutableWhere { Method = Boolean op_Equality(System.String, System.String), Left = Goo, Right = Goo0 }

                 var n = "@arg" + state.ApplyParameter.Count;

                 state.OrderByCommand = "order by `" + ColumnName + "` desc";
             }
            );
        }
        #endregion


        #region take
        [Obsolete("caller has the option to clone the state before calling this function. should jsc add static expression pooling/caching like c# compiler does for lambdas?")]
        public static void MutableTake(IQueryStrategy that, long count)
        {
            // should the caller take care of cloning the instance?
            // should we start using Trace for logging?
            Console.WriteLine("MutableTake " + new { count });

            that.GetCommandBuilder().Add(
             state =>
             {
                 //MutableWhere { Method = Boolean op_Equality(System.String, System.String), Left = Goo, Right = Goo0 }

                 var n = "@arg" + state.ApplyParameter.Count;

                 // https://sites.google.com/a/jsc-solutions.net/backlog/knowledge-base/2014/201401/20140119
                 // limit 0?
                 state.LimitCommand = "limit " + n;

                 Console.WriteLine("MutableTake " + new { n, count });


                 state.ApplyParameter.Add(
                     c =>
                     {
                         // either the actualt command or the explain command?

                         //c.Parameters.AddWithValue(n, count);
                         c.AddParameter(n, count);
                     }
                 );
             }
            );
        }
        #endregion


        #region select count
        public static long Count(IQueryStrategy Strategy)
        {
            return ((Task<long>)Strategy.GetDescriptor().GetWithConnection()(
                c =>
                {
                    var state = AsCommandBuilder(Strategy);

                    // override
                    state.SelectCommand = "select count(*)";

                    var cmd = c.CreateCommand(state.ToString());

                    foreach (var item in state.ApplyParameter)
                    {
                        item(cmd);
                    }

                    var s = new TaskCompletionSource<long>();
                    s.SetResult((long)cmd.ExecuteScalar());

                    return s.Task;
                }
            )).Result;
        }
        #endregion



        public static DataTable AsDataTable(IQueryStrategy Strategy)
        {
            // X:\jsc.svn\examples\javascript\forms\Test\TestSQLiteGroupBy\TestSQLiteGroupBy\ApplicationWebService.cs

            Console.WriteLine("AsDataTable " + new { Strategy });

            if (Strategy == null)
            {
                if (Debugger.IsAttached)
                    Debugger.Break();

                throw new ArgumentNullException("Strategy");
            }

            //System.Diagnostics.Contracts.Contract.Assume

            return ((Task<DataTable>)Strategy.GetDescriptor().GetWithConnection()(
                c =>
                {
                    var state = AsCommandBuilder(Strategy);

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
        }


        public class CommandBuilderState
        {
            public IQueryStrategy Strategy;


            public string SelectCommand;
            public string FromCommand;
            public string WhereCommand;
            public string OrderByCommand;
            public string LimitCommand;

            // is it before or after other clauses or both?
            // X:\jsc.svn\examples\javascript\forms\Test\TestSQLiteGroupBy\TestSQLiteGroupBy\ApplicationWebService.cs
            public string GroupByCommand;

            public List<Action<IDbCommand>> ApplyParameter = new List<Action<IDbCommand>>();

            public override string ToString()
            {
                var w = new StringBuilder();

                if (!string.IsNullOrEmpty(this.SelectCommand)) w.AppendLine(this.SelectCommand);
                if (!string.IsNullOrEmpty(this.FromCommand)) w.AppendLine(this.FromCommand);
                if (!string.IsNullOrEmpty(this.WhereCommand)) w.AppendLine(this.WhereCommand);
                if (!string.IsNullOrEmpty(this.OrderByCommand)) w.AppendLine(this.OrderByCommand);
                if (!string.IsNullOrEmpty(this.LimitCommand)) w.AppendLine(this.LimitCommand);
                if (!string.IsNullOrEmpty(this.GroupByCommand)) w.AppendLine(this.GroupByCommand);

                var x = w.ToString();

                Console.WriteLine(x);

                return x;

            }

            public string GetQualifiedTableNameOrToString()
            {
                if (string.IsNullOrEmpty(this.WhereCommand))
                    if (string.IsNullOrEmpty(this.OrderByCommand))
                        if (string.IsNullOrEmpty(this.LimitCommand))
                            if (string.IsNullOrEmpty(this.GroupByCommand))
                                if (this.FromCommand == "from `" + Strategy.GetDescriptor().GetQualifiedTableName() + "`")
                                    if (this.SelectCommand == Strategy.GetDescriptor().GetSelectAllColumnsText())
                                    {
                                        return "`" + Strategy.GetDescriptor().GetQualifiedTableName() + "`";
                                    }


                return "(" + this.ToString() + ")";
            }
        }







        public static CommandBuilderState AsCommandBuilder(CommandBuilderState state)
        {
            // time to build the CommandText

            var StrategyDescriptor = state.Strategy.GetDescriptor();



            // X:\jsc.svn\examples\javascript\forms\Test\TestSQLJoin\TestSQLJoin\ApplicationWebService.cs
            if (StrategyDescriptor != null)
            {
                // http://www.linkedin.com/groups/select-vs-selecting-all-columns-66097.S.206400776
                state.SelectCommand = StrategyDescriptor.GetSelectAllColumnsText();

                state.FromCommand = "from `" + StrategyDescriptor.GetQualifiedTableName() + "`";
            }

            foreach (var item in state.Strategy.GetCommandBuilder())
            {
                item(state);
            }

            return state;
        }

        public static CommandBuilderState AsCommandBuilder(IQueryStrategy Strategy)
        {
            // time to build the CommandText
            return AsCommandBuilder(
                new CommandBuilderState
            {
                Strategy = Strategy,
            }
            );
        }
    }
}
