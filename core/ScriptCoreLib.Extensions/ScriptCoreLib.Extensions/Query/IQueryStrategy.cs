using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

//namespace ScriptCoreLib.Query
//{
//}

namespace ScriptCoreLib.Shared.Data.Diagnostics
{

    // ?
    //[Obsolete("?", true)]
    public interface IQueryStrategyGroupingBuilder<TSource>
    {
        IQueryStrategy<TSource> source { get; set; }

    }

    [Obsolete("there is no good translation of such queries to SQL and Linq-to-SQL has to resort to doing multiple subqueries.")]
    public interface IQueryStrategyGroupingBuilder<TKey, TSource> : IQueryStrategyGroupingBuilder<TSource>
    {
        // GroupByBuilder

        Expression<Func<TSource, TKey>> keySelector { get; set; }
    }

    [Obsolete("group by . into .")]
    class XQueryStrategyGroupingBuilder<TKey, TSource> : IQueryStrategyGroupingBuilder<TKey, TSource>
    {
        public IQueryStrategy<TSource> source { get; set; }
        public Expression<Func<TSource, TKey>> keySelector { get; set; }
    }



    // used by order by GroupingKey detection
    public interface IQueryStrategyGrouping
    {
    }

    [Obsolete("to make intellisense happy, and dispay only supported methods")]
    //public interface IQueryStrategyGrouping<out TKey, out TElement> : IQueryStrategy<TElement>
    public interface IQueryStrategyGrouping<out TKey, TElement> : IQueryStrategy<TElement>, IQueryStrategyGrouping
    {
        TKey Key { get; }
    }

    //[Obsolete("?", true)]
    public interface IQueryDescriptor
    {
        // this type has the reset state and how to make a connection

        string GetSelectAllColumnsText();

        string GetQualifiedTableName();

        // used by?
        // https://sites.google.com/a/jsc-solutions.net/backlog/knowledge-base/2014/201403/20140322
        //Func<Func<SQLiteConnection, Task>, Task> GetWithConnection();
        Func<Func<IDbConnection, Task>, Task> GetWithConnection();

        // here we could ask for table stats?
    }


    //[Obsolete("?", true)]
    public interface IQueryStrategy
    {
        // this state knows about reset state 

        IQueryDescriptor GetDescriptor();

        List<Action<QueryStrategyExtensions.CommandBuilderState>> GetCommandBuilder();



        Type GetElementType();
        // Stack<Apply>
    }



    // X:\jsc.svn\examples\javascript\LINQ\MashableVelocityGraph\MashableVelocityGraph\ApplicationWebService.cs
    // can JVM support out params, or does typeerasure work around it anyhow?
    [ScriptCoreLib.ScriptAttribute.ExplicitInterface]
    //[Obsolete("?", true)]
    public interface IQueryStrategy<out TRow> : IQueryStrategy
    {
        // this class exists to make LINQ happy

        // X:\jsc.svn\examples\javascript\forms\Test\TestSQLJoin\TestSQLJoin\ApplicationWebService.cs
        // https://sites.google.com/a/jsc-solutions.net/backlog/knowledge-base/2014/201405/20140501

    }

}
