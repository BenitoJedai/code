using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using ScriptCoreLib.Extensions;
using System.Reflection;
using System.Data;
using System.Threading.Tasks;
using System.Data.Common;

namespace ScriptCoreLib.Query.Experimental
{
    public static partial class QueryExpressionBuilder
    {
        // X:\jsc.svn\core\ScriptCoreLib.Async\ScriptCoreLib.Async\Query\Experimental\QueryExpressionBuilderAsync.IDbConnection.Insert.cs
        // X:\jsc.svn\core\ScriptCoreLib.Async\ScriptCoreLib.Async\Query\Experimental\QueryExpressionBuilderAsync.IDbConnection.Count.cs

        partial class SQLWriter<TElement>
        {
            public static readonly Func<IQueryStrategy<TElement>, long> CountReference = Count;
        }


        [Obsolete("xScalar")]
        class xCount : IQueryStrategy
        {
            public IQueryStrategy source;

            public override string ToString()
            {
                return "count";
            }
        }

        class xCount<TElement> : xCount, IQueryStrategy<TElement>
        {

        }

        public static IDbCommand GetCountCommand<TElement>(this IQueryStrategy<TElement> source, IDbConnection cc)
        {

            var nsource = new xCount { source = source };
            var Command = default(DbCommand);


            if (cc != null)
                Command = (DbCommand)cc.CreateCommand();
            var w = new SQLWriter<TElement>(nsource, new IQueryStrategy[] { nsource }, Command: Command);

            return Command;
        }





        public static long Average<TSource>(this IQueryStrategy<TSource> source, Expression<Func<TSource, long>> selector)
        {
            // X:\jsc.svn\examples\javascript\LINQ\test\auto\TestSelect\TestSelectAverage\Program.cs

            return source.Select(selector).Average();
        }

        public static long Average(this IQueryStrategy<long> source)
        {
            // X:\jsc.svn\examples\javascript\LINQ\test\auto\TestSelect\TestSelectAverage\Program.cs

            // first, lets apprach it in a similar way. lets copy count


            var xDbCommand = GetCountCommand(source, cc: null);

            return 0;
        }


        // first lets allow scalar subqueries
        public static long Count<TElement>(this IQueryStrategy<TElement> source)
        {
            // X:\jsc.svn\examples\javascript\LINQ\test\auto\TestSelect\TestXMySQL\Program.cs

            var value = default(long);

            WithConnection(
                cc =>
                {
                    value = Count(source, cc);
                }
            );

            return value;
        }

        // chrome needs CountAsync
        public static long Count<TElement>(this IQueryStrategy<TElement> source, IDbConnection cc)
        {
            // X:\jsc.svn\examples\javascript\LINQ\test\auto\TestSelect\TestDeleteAll\Program.cs
            // https://sites.google.com/a/jsc-solutions.net/backlog/knowledge-base/2014/20140705/20140705


            // how was it done before?
            // tested by?

            var xDbCommand = GetCountCommand(source, cc);

            // Additional information: Every derived table must have its own alias
            // what?
            var x = xDbCommand.ExecuteScalar();
            // cast?
            return (long)x;
        }

        // http://stackoverflow.com/questions/4471277/mysql-delete-from-with-subquery-as-condition
        // X:\jsc.svn\examples\javascript\LINQ\test\auto\TestSelect\TestSelectMath\Program.cs
        // X:\jsc.svn\examples\javascript\LINQ\GGearAlpha\GGearAlpha\Library\GoogleGearsAdvanced.cs

    }

}
