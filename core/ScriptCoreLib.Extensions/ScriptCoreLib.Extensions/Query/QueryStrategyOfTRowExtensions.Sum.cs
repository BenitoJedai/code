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

namespace System.Data
{
    // move to a nuget?
    // shall reimplement IQueriable for jsc data layer gen
    //[Obsolete("the first generic extension method for all jsc data layer rows")]
    public static partial class QueryStrategyOfTRowExtensions
    {
        // X:\jsc.svn\examples\javascript\forms\Test\TestSQLiteGroupBy\TestSQLiteGroupBy\ApplicationWebService.cs

        // http://stackoverflow.com/questions/3785995/sqlite-accumulator-sum-column-in-a-select-statement
        // http://www.tutorialspoint.com/sqlite/sqlite_useful_functions.htm
        // http://sqlite.1065341.n5.nabble.com/SUM-and-NULL-values-td2257.html
        // X:\jsc.svn\examples\javascript\LINQ\MinMaxAverageExperiment\MinMaxAverageExperiment\ApplicationWebService.cs

        #region select sum double
        // can this be used in a join?
        [Obsolete("this is somewhat like select foo and then sum, or like orderby. what about summing vec3"
            )]
        public static double Sum<TElement>(this IQueryStrategy<TElement> Strategy, Expression<Func<TElement, double>> selector)
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



            return ((Task<double>)Strategy.GetDescriptor().GetWithConnection()(
                c =>
                {
                    var state = QueryStrategyExtensions.AsCommandBuilder(Strategy);

                    // override
                    state.SelectCommand = "select sum(`" + ColumnName + "`) ";

                    //var cmd = new SQLiteCommand(state.ToString(), c);
                    var cmd = c.CreateCommand(state.ToString());

                    foreach (var item in state.ApplyParameter)
                    {
                        item(cmd);
                    }

                    var s = new TaskCompletionSource<double>();


                    s.SetResult(
                    (double)cmd.ExecuteScalar()
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





        public static TResult Min<TElement, TResult>(this IQueryStrategy<TElement> Strategy, Expression<Func<TElement, TResult>> selector)
        {
            // X:\jsc.svn\examples\javascript\LINQ\MinMaxAverageExperiment\MinMaxAverageExperiment\ApplicationWebService.cs

            return ((Task<TResult>)Strategy.GetDescriptor().GetWithConnection()(
                c =>
                {

                    // http://stackoverflow.com/questions/3785995/sqlite-accumulator-sum-column-in-a-select-statement
                    // http://www.tutorialspoint.com/sqlite/sqlite_useful_functions.htm
                    // http://sqlite.1065341.n5.nabble.com/SUM-and-NULL-values-td2257.html

                    var body = ((MemberExpression)((LambdaExpression)selector).Body);

                    // do we need to check our db schema or is reflection the schema for us?
                    #region ColumnName
                    var ColumnName = "";

                    ColumnName = body.Member.Name;
                    #endregion

                    var state = QueryStrategyExtensions.AsCommandBuilder(Strategy);

                    // override
                    state.SelectCommand = "select min(`" + ColumnName + "`) ";

                    //var cmd = new SQLiteCommand(state.ToString(), c);
                    var cmd = c.CreateCommand(state.ToString());

                    foreach (var item in state.ApplyParameter)
                    {
                        item(cmd);
                    }

                    var s = new TaskCompletionSource<TResult>();

                    // will it compile to java???
                    s.SetResult(
                    (TResult)cmd.ExecuteScalar()
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

        public static TResult Max<TElement, TResult>(this IQueryStrategy<TElement> Strategy, Expression<Func<TElement, TResult>> selector)
        {
            // X:\jsc.svn\examples\javascript\LINQ\MinMaxAverageExperiment\MinMaxAverageExperiment\ApplicationWebService.cs

            return ((Task<TResult>)Strategy.GetDescriptor().GetWithConnection()(
                c =>
                {

                    // http://stackoverflow.com/questions/3785995/sqlite-accumulator-sum-column-in-a-select-statement
                    // http://www.tutorialspoint.com/sqlite/sqlite_useful_functions.htm
                    // http://sqlite.1065341.n5.nabble.com/SUM-and-NULL-values-td2257.html

                    var body = ((MemberExpression)((LambdaExpression)selector).Body);

                    // do we need to check our db schema or is reflection the schema for us?
                    #region ColumnName
                    var ColumnName = "";

                    ColumnName = body.Member.Name;
                    #endregion

                    var state = QueryStrategyExtensions.AsCommandBuilder(Strategy);

                    // override
                    state.SelectCommand = "select max(`" + ColumnName + "`) ";

                    //var cmd = new SQLiteCommand(state.ToString(), c);
                    var cmd = c.CreateCommand(state.ToString());

                    foreach (var item in state.ApplyParameter)
                    {
                        item(cmd);
                    }

                    var s = new TaskCompletionSource<TResult>();

                    // will it compile to java???
                    s.SetResult(
                    (TResult)cmd.ExecuteScalar()
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


    }
}

