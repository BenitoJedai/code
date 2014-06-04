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
        // public static IEnumerable<TResult> SelectMany<TSource, TCollection, TResult>(this IEnumerable<TSource> source, Func<TSource, IEnumerable<TCollection>> collectionSelector, Func<TSource, TCollection, TResult> resultSelector);

        [ScriptCoreLib.ScriptAttribute.ExplicitInterface]
        public interface ISelectManyQueryStrategy : INestedQueryStrategy
        {
            Expression resultSelector { get; }
            Expression collectionSelector { get; }

            IQueryStrategy source { get; }

        }

        class SelectManyQueryStrategy<TSource, TCollection, TResult> : XQueryStrategy<TResult>, ISelectManyQueryStrategy
        {
            public IQueryStrategy<TSource> source;

            public Expression<Func<TSource, TCollection, TResult>> resultSelector;
            public Expression<Func<TSource, IEnumerable<TCollection>>> collectionSelector;



            public ISelectQueryStrategy upperSelect { get; set; }
            public IJoinQueryStrategy upperJoin { get; set; }
            public IGroupByQueryStrategy upperGroupBy { get; set; }


            #region ISelectQueryStrategy
            Expression ISelectManyQueryStrategy.collectionSelector
            {
                get { return collectionSelector; }
            }

            Expression ISelectManyQueryStrategy.resultSelector
            {
                get { return resultSelector; }
            }

            IQueryStrategy ISelectManyQueryStrategy.source
            {
                get { return source; }
            }
            #endregion

        }

        public static IQueryStrategy<TResult> SelectMany<TSource, TCollection, TResult>(this IQueryStrategy<TSource> source,
            Expression<Func<TSource, IEnumerable<TCollection>>> collectionSelector,
            Expression<Func<TSource, TCollection, TResult>> resultSelector
            )
        {
            // http://alitarhini.wordpress.com/2010/11/20/114/
            Console.WriteLine("SelectMany " + new { resultSelector });

            var that = new SelectManyQueryStrategy<TSource, TCollection, TResult>
            {
                // save for inspection
                source = source,
                resultSelector = resultSelector,
                collectionSelector = collectionSelector,

                InternalGetDescriptor =
                    () =>
                    {
                        // inherit the connection/context from above
                        var StrategyDescriptor = source.GetDescriptor();

                        return StrategyDescriptor;
                    }
            };


            that.GetCommandBuilder().Add(
                 state =>
                 {
                     // X:\jsc.svn\examples\javascript\linq\test\TestSelectManyRange\TestSelectManyRange\ApplicationWebService.cs


                     // whats the additional stream about?

                     var cLMethodCallExpression = collectionSelector.Body as MethodCallExpression;
                     if (cLMethodCallExpression != null)
                     {
                         if (cLMethodCallExpression.Method.Name == "Range")
                         {
                             // ah a virtual index stream

                             var cRangeFrom = cLMethodCallExpression.Arguments[0] as ConstantExpression;
                             var cRangeCount = cLMethodCallExpression.Arguments[1] as ConstantExpression;


                             state.SelectCommand += ", y.y as y";


                             //select x0.x, x1.x, (x0.x * 10 + x1.x) as z from
                             //(select 0 as x union select 1 union select 2 union select 3 union select 4 union select 5 union select 6 union select 7 union select 8 union select 9) as x0,  
                             //(select 0 as x union select 1 union select 2 union select 3 union select 4 union select 5 union select 6 union select 7 union select 8 union select 9) as x1
                             //limit 64

                             var vRangeFrom = cRangeFrom.Value;
                             var nRangeFrom = "@argRangeFrom" + state.ApplyParameter.Count;

                             var vRangeCount = cRangeCount.Value;
                             var nRangeCount = "@argRangeCount" + state.ApplyParameter.Count;

                             state.FromCommand +=
                                @", (
    select (" + nRangeFrom + @" + x0.x * 10 + x1.x) as y from
    (select 0 as x union select 1 union select 2 union select 3 union select 4 union select 5 union select 6 union select 7 union select 8 union select 9) as x0,  
    (select 0 as x union select 1 union select 2 union select 3 union select 4 union select 5 union select 6 union select 7 union select 8 union select 9) as x1
    limit " + nRangeCount + @"
) as y";


                             state.ApplyParameter.Add(
                                 c =>
                                     {
                                         // either the actualt command or the explain command?

                                         //c.Parameters.AddWithValue(n, r);
                                         c.AddParameter(nRangeFrom, vRangeFrom);
                                         c.AddParameter(nRangeCount, vRangeCount);
                                     }
                             );

                         }
                     }

                 }
            );


            return that;
        }


    }
}

