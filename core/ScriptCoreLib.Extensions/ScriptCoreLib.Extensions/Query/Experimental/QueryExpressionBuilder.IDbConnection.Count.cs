﻿using System;
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

        static class xReferencesOfLong
        {
            public static readonly Func<IQueryStrategy<long>, long> SumOfLongReference = Sum;
            public static readonly Func<IQueryStrategy<long>, long> MinOfLongReference = Min;
            public static readonly Func<IQueryStrategy<long>, long> MaxOfLongReference = Max;

            // X:\jsc.svn\examples\javascript\LINQ\test\auto\TestSelect\TestSelectAverage\Program.cs
            public static readonly Func<IQueryStrategy<long>, double> AverageOfLongReference = Average;
        }

        partial class SQLWriter<TElement>
        {
            public static readonly Func<IQueryStrategy<TElement>, long> CountReference = Count;


        }

        class xScalar : IQueryStrategy
        {

            public IQueryStrategy source;



            public MethodInfo Operand;

            public override string ToString()
            {
                return "scalar(x)";
            }
        }

        class xScalar<TElement> : xScalar, IQueryStrategy<TElement>
        {

        }

        public static IDbCommand GetScalarCommand<TElement>(
            this IQueryStrategy<TElement> source,
            IDbConnection cc,

            MethodInfo Operand
            )
        {

            var nsource = new xScalar { source = source, Operand = Operand };
            var Command = default(DbCommand);


            if (cc != null)
                Command = (DbCommand)cc.CreateCommand();
            var w = new SQLWriter<TElement>(nsource, new IQueryStrategy[] { nsource }, Command: Command);

            return Command;
        }





  



      

        public static long Sum(this IQueryStrategy<long> source)
        {

            // first, lets apprach it in a similar way. lets copy count


            var xDbCommand = GetScalarCommand(source, cc: null, Operand: xReferencesOfLong.SumOfLongReference.Method);

            return 0;
        }


        // first lets allow scalar subqueries
        public static long Count<TElement>(this IQueryStrategy<TElement> source)
        {
            // X:\jsc.svn\examples\javascript\LINQ\test\auto\TestSelect\TestXMySQL\Program.cs

            var value = default(long);

            // what if there is no connection?
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

            var xDbCommand = GetScalarCommand(source, cc, Operand: SQLWriter<TElement>.CountReference.Method);

            // Additional information: Every derived table must have its own alias
            // what?
            var x = default(long);


            if (xDbCommand != null)
                x = (long)xDbCommand.ExecuteScalar();

            return x;
        }

        // http://stackoverflow.com/questions/4471277/mysql-delete-from-with-subquery-as-condition
        // X:\jsc.svn\examples\javascript\LINQ\test\auto\TestSelect\TestSelectMath\Program.cs
        // X:\jsc.svn\examples\javascript\LINQ\GGearAlpha\GGearAlpha\Library\GoogleGearsAdvanced.cs

    }

}
