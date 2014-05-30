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
    public static partial class QueryStrategyExtensions
    {
        // X:\jsc.svn\examples\javascript\forms\Test\TestSQLJoin\TestSQLJoin\ApplicationWebService.cs

        // SQLite.Linq reference implementation
        // when can we have immutable version?

        // http://msdn.microsoft.com/en-us/library/bb310804.aspx


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
