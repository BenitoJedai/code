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
        [ScriptCoreLib.ScriptAttribute.ExplicitInterface]
        interface ISelectQueryStrategy
        {
            // allow to inspect upper select . what if there are multiple upper selects?
            Expression selectorExpression { get; }

            IQueryStrategy source { get; }

            // ? gDescendingByKeyReferenced
        }

        class SelectQueryStrategy<TSource, TResult> : XQueryStrategy<TResult>, ISelectQueryStrategy
        {
            public IQueryStrategy<TSource> source;
            public Expression<Func<TSource, TResult>> selector;



            #region ISelectQueryStrategy
            Expression ISelectQueryStrategy.selectorExpression
            {
                get { return selector; }
            }

            IQueryStrategy ISelectQueryStrategy.source
            {
                get { return source; }
            }
            #endregion

        }

        public static IQueryStrategy<TResult>
            Select
            <TSource, TResult>
            (
             this IQueryStrategy<TSource> source,
             Expression<Func<TSource, TResult>> selector)
        {
            // https://sites.google.com/a/jsc-solutions.net/backlog/knowledge-base/2014/201405/20140513

            //GroupBy
            //Select
            //Select
            //Select
            //AsDataTable



            // keySelector = {g => new <>f__AnonymousType0`2(g = g, last = g.Last())}
            // keySelector = {<>h__TransparentIdentifier0 => new <>f__AnonymousType1`2(<>h__TransparentIdentifier0 = <>h__TransparentIdentifier0, x = "???")}
            // keySelector = {<>h__TransparentIdentifier1 => new SchemaViewsMiddleViewRow() {
            // Content = <>h__TransparentIdentifier1.<>h__TransparentIdentifier0.last.UpdatedContent, 
            // Tag = <>h__TransparentIdentifier1.x, 
            // LatestLeftContent = <>h__TransparentIdentifier1.x, LatestRightContent...

            Console.WriteLine("Select " + new { selector });

            var that = new SelectQueryStrategy<TSource, TResult>
            {
                // save for inspection
                source = source,
                selector = selector,


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
                     Console.WriteLine("Select CommandBuilder");

                     //select `Key`, `MiddleSheet`, `UpdatedContent`, `Tag`, `Timestamp`
                     //from `Schema.MiddleSheetUpdates`


                     // X:\jsc.svn\examples\javascript\forms\test\TestNestedSQLiteGrouping\TestNestedSQLiteGrouping\ApplicationWebService.cs

                     (that.source as IGroupByQueryStrategy).With(q => q.upperSelect = that);

                     var s = QueryStrategyExtensions.AsCommandBuilder(that.source);


                     state.SelectCommand = s.SelectCommand;
                     state.FromCommand = s.FromCommand;

                     //state.ApplyParameter.AddRange(s.ApplyParameter);

                     state.ApplyParameter.AddRange(s.ApplyParameter);


                     //asGroupByQueryStrategy.upperSelect = null;
                 }
            );


            return that;
        }

    }
}

