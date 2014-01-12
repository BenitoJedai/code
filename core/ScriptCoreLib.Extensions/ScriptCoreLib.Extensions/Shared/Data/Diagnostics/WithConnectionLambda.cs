using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Diagnostics;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
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


    public interface IQueryDescriptor
    {
        // this type has the reset state and how to make a connection

        string GetQualifiedTableName();
        Func<Func<SQLiteConnection, Task>, Task> GetWithConnection();

        // here we could ask for table stats?
    }

    public interface IQueryStrategy
    {
        // this state knows about reset state 

        IQueryDescriptor GetDescriptor();

        List<Action<QueryStrategyExtensions.CommandBuilderState>> GetCommandBuilder();


        // Stack<Apply>
    }

    public static class QueryStrategyExtensions
    {
        // SQLite.Linq reference implementation
        // when can we have immutable version?


        // behave like StringBuilder where core data is mutable?
        public static void MutableWhere(IQueryStrategy that, LambdaExpression filter)
        {
            // https://sites.google.com/a/jsc-solutions.net/backlog/knowledge-base/2014/201401/20140112/count
            // X:\jsc.svn\examples\javascript\Test\TestIQueryable\TestIQueryable\ApplicationWebService.cs


            // for op_Equals
            var f_Body_as_BinaryExpression = ((BinaryExpression)filter.Body);

            // http://stackoverflow.com/questions/9241607/whats-wrong-with-system-linq-expressions-logicalbinaryexpression-class
            var f_Body_Left_as_MemberExpression = (MemberExpression)f_Body_as_BinaryExpression.Left;
            //var f_Body_Right_as_ConstantExpression = (ConstantExpression)f_Body_as_BinaryExpression.Right;
            var f_Body_Right = (MemberExpression)f_Body_as_BinaryExpression.Right;

            //+		(new System.Linq.Expressions.Expression.ConstantExpressionProxy((new System.Linq.Expressions.Expression.MemberExpressionProxy(f_Body_Right as System.Linq.Expressions.FieldExpression)).Expression as System.Linq.Expressions.ConstantExpression)).Value	{AppEngineWhereOperator.ApplicationWebService.}	object {AppEngineWhereOperator.ApplicationWebService.}

            var f_Body_Right_Expression = (ConstantExpression)f_Body_Right.Expression;

            var f_Body_Right_Expression_Value = f_Body_Right_Expression.Value;
            var r = ((FieldInfo)f_Body_Right.Member).GetValue(f_Body_Right_Expression_Value);



            // for non op_Equals
            //var f_Body_as_MethodCallExpression = ((MethodCallExpression)f.Body);
            ////Console.WriteLine("IBook1Sheet1Queryable.Where");

            //var f_Body_Left_as_MemberExpression = (MemberExpression)f_Body_as_MethodCallExpression.Arguments[0];
            //var f_Body_Right_as_ConstantExpression = (ConstantExpression)f_Body_as_MethodCallExpression.Arguments[1];

            //Console.WriteLine("IBook1Sheet1Queryable.Where " + new { f_Body_as_MethodCallExpression.Method, f_Body_Left_as_MemberExpression.Member.Name, f_Body_Right_as_ConstantExpression.Value });
            Console.WriteLine("MutableWhere " + new
            {
                f_Body_as_BinaryExpression.Method,
                Left = f_Body_Left_as_MemberExpression.Member.Name,
                Right = r
            });


            that.GetCommandBuilder().Add(
                state =>
                {
                    //MutableWhere { Method = Boolean op_Equality(System.String, System.String), Left = Goo, Right = Goo0 }

                    var n = "@arg" + state.ApplyParameter.Count;

                    state.WhereCommand = " where `" + f_Body_Left_as_MemberExpression.Member.Name + "` = " + n;


                    state.ApplyParameter.Add(
                        c =>
                        {
                            // either the actualt command or the explain command?

                            c.Parameters.AddWithValue(n, r);
                        }
                    );
                }
            );
        }

        public class CommandBuilderState
        {
            public IQueryStrategy Strategy;


            public string SelectCommand;
            public string FromCommand;
            public string WhereCommand;

            public List<Action<SQLiteCommand>> ApplyParameter = new List<Action<SQLiteCommand>>();

            public override string ToString()
            {
                var w = new StringBuilder();

                w.AppendLine(this.SelectCommand);
                w.AppendLine(this.FromCommand);
                w.AppendLine(this.WhereCommand);

                //w.AppendLine(c.OrderByCommand);
                //w.AppendLine(c.LimitCommand);

                return w.ToString();

            }
        }

        public static long Count(IQueryStrategy Strategy)
        {
            return ((Task<long>)Strategy.GetDescriptor().GetWithConnection()(
                c =>
                {
                    var state = AsCommandBuilder(Strategy);

                    var cmd = new SQLiteCommand(state.ToString(), c);

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

        public static CommandBuilderState AsCommandBuilder(IQueryStrategy Strategy)
        {
            // time to build the CommandText

            var state = new CommandBuilderState
            {
                Strategy = Strategy,

                SelectCommand = "select count(*)",

                FromCommand = "from `" + Strategy.GetDescriptor().GetQualifiedTableName() + "`"
            };

            foreach (var item in Strategy.GetCommandBuilder())
            {
                item(state);
            }

            return state;
        }
    }
}
