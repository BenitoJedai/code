using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

// whats the namespace for jsc data?
//namespace ScriptCoreLib.Query
// are we not part of the system?
// are we not using non clasing names and we are distinct of IQueriable
namespace System.Data
{
    // when can we insert into secured views? when can we have extension operators?
    // what about async insert?
    // used by the compiler
    //public interface IQueryStrategyInsert<TElement, TKey>
    //{
    //    TKey Insert(TElement value);
    //}


    //// experimenting with client side inserts
    //public interface IAsyncQueryStrategyInsert<TElement, TKey>
    //{
    //    // since jsc only allows implicit implementations and does not allow return of interfaces yet, only one table can be inserted to for nw
    //    Task<TKey> Insert(TElement value);
    //}

    //public static class QueryStrategyInsertExtensions
    //{
    //    // X:\jsc.svn\examples\javascript\LINQ\test\TestGroupByThenOrderByThenOrderBy\TestGroupByThenOrderByThenOrderBy\ApplicationWebService.cs

    //    // Error	262	The params parameter must be a single dimensional array	X:\jsc.svn\core\ScriptCoreLib.Extensions\ScriptCoreLib.Extensions\Query\IQueryStrategyInsert.cs	23	110	ScriptCoreLib.Extensions
    //    //public static IEnumerable<TKey> Insert<TElement, TKey>(IQueryStrategyInsert<TElement, TKey> source, params IEnumerable<TElement> values)
    //    public static IEnumerable<TKey> Insert<TElement, TKey>(this IQueryStrategyInsert<TElement, TKey> source, params TElement[] values)
    //    {
    //        return source.Insert(values.AsEnumerable());
    //    }

    //    public static IEnumerable<TKey> Insert<TElement, TKey>(this IQueryStrategyInsert<TElement, TKey> source, IEnumerable<TElement> values)
    //    {
    //        //source.Insert(
    //        return values.Select(source.Insert).ToArray();
    //    }

    //    //public static void AttachTo()
    //}

}
