using ScriptCoreLib;
using ScriptCoreLib.Delegates;
using ScriptCoreLib.Extensions;
using ScriptCoreLib.Shared.Data.Diagnostics;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Linq.Expressions;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using TestSQLiteGroupBy.Data;

namespace TestSQLiteGroupBy
{
    /// <summary>
    /// Methods defined in this type can be used from JavaScript. The method calls will seamlessly be proxied to the server.
    /// </summary>
    public partial class ApplicationWebService : Component
    {
        /// <summary>
        /// This Method is a javascript callable method.
        /// </summary>
        /// <param name="e">A parameter from javascript.</param>
        /// <param name="y">A callback to javascript.</param>
        //public async Task<IEnumerable<IGrouping<GooStateEnum, Book1MiddleRow>>> WebMethod2()
        //public async Task<IEnumerable<XGrouping>> WebMethod2()
        public async Task<IEnumerable<Book1MiddleAsGroupByGooWithCountRow>> WebMethod2()
        {
            // https://sites.google.com/a/jsc-solutions.net/backlog/knowledge-base/2014/201405/201405

            // X:\jsc.svn\examples\javascript\forms\Test\TestSQLiteEnumWhere\TestSQLiteEnumWhere\ApplicationWebService.cs
            // can we do sql group by or even send it over to client if we do it in memory?

            // how can this type define it depends on another assembly so 
            // security can detect it fast?
            // like throws for java.
            // http://msdn.microsoft.com/en-us/library/system.runtime.compilerservices.dependencyattribute.aspx
            // we will need to reference a specific type on that third party.
            // we can initally do it only for assetslibrary genererated types?
            // http://stackoverflow.com/questions/473575/unity-framework-dependencyattribute-only-works-for-public-properties


            //[FileNotFoundException]: Could not load file or assembly &#39;System.Data.XSQLite, Version=3.7.7.1, Culture=neutral, PublicKeyToken=null&#39; or one of its dependencies. The system cannot find the file specified.
            //   at ScriptCoreLib.Shared.Data.Diagnostics.InternalWithConnectionLambda.WithConnection(String DataSource)
            //   at ScriptCoreLib.Shared.Data.Diagnostics.WithConnectionLambda.WithConnection(String DataSource)
            //   at TestSQLiteGroupBy.Data.Book1.Middle..ctor(String DataSource)

            var x = new Book1.Middle
            {
                // collection initializer?
                // .Add ?
            };

            var SpecialRatio = new Random().NextDouble();

            x.Insert(
                new Book1MiddleRow
                {
                    // enum name clash? 
                    FooStateEnum = FooStateEnum.Foo0,
                    GooStateEnum = GooStateEnum.Goo2,

                    Ratio = SpecialRatio,
                    x = 0.9,

                    Title = "first 0.9"
                }
            );

            x.Insert(
                new Book1MiddleRow
                {
                    // enum name clash? 
                    FooStateEnum = FooStateEnum.Foo0,
                    GooStateEnum = GooStateEnum.Goo2,

                    Ratio = SpecialRatio,
                    x = 0.8,

                    Title = "second 0.8"
                }
            );

            x.Insert(
                new Book1MiddleRow
                {
                    // enum name clash? 
                    FooStateEnum = FooStateEnum.Foo0,
                    GooStateEnum = GooStateEnum.Goo2,

                    Ratio = SpecialRatio,
                    x = 0.6,

                    Title = "third 0.6"
                }
            );



            x.Insert(
                new Book1MiddleRow
                {
                    FooStateEnum = FooStateEnum.Foo0,
                    GooStateEnum = GooStateEnum.Goo0,
                    Ratio = 0.5,

                    Title = "goo0 " + new { Count = new Book1.Middle().Count(k => k.GooStateEnum == GooStateEnum.Goo0) }
                }
            );



            // x:\jsc.svn\core\ScriptCoreLib.Extensions\ScriptCoreLib.Extensions\Shared\Data\Diagnostics\QueryStrategyExtensions.cs
            // http://msdn.microsoft.com/en-US/vstudio/ee908647.aspx#leftouterjoin
            // f you want a LEFT OUTER JOIN then you need to use "into":
            // http://stackoverflow.com/questions/1092562/left-join-in-linq


            //MutableWhere { Method = , NodeType = Equal, ColumnName = FooStateEnum, Right = 0 }
            //AsDataTable
            //MutableWhere { n = @arg0, r = 0 }
            //select `Key`, `Title`, `Ratio`, `FooStateEnum`, `GooStateEnum`, `Tag`, `Timestamp`
            //from `Book1.Middle`
            // where `FooStateEnum` = @arg0



            // http://stackoverflow.com/questions/710508/how-best-to-loop-over-a-batch-of-results-with-a-c-sharp-dbdatareader

            var g = from z in x
                    where z.FooStateEnum == FooStateEnum.Foo0
                    where z.Ratio == SpecialRatio

                    //where z.Ratio > 0.1
                    //where z.Ratio < 0.9
                    //where z.GooStateEnum == GooStateEnum.Goo2
                    //select z

                    group z by z.GooStateEnum into GroupByGoo

                    select new Book1MiddleAsGroupByGooWithCountRow
                    {
                        // GroupByGoo cannot have 0 members

                        GooStateEnum = GroupByGoo.Key,

                        // do we have to do multple from clauses for ordering first and last?

                        FirstTitle = GroupByGoo.First().Title,
                        FirstKey = GroupByGoo.First(),

                        LastKey = GroupByGoo.Last(),
                        LastTitle = GroupByGoo.Last().Title,

                        // count is easy what about
                        // getting the first or last items?

                        Count = GroupByGoo.Count()
                    };

            // http://friism.com/linq-to-sql-group-by-subqueries-and-performance
            // !! This is because there is no good translation of such queries to SQL and Linq-to-SQL has to resort to doing multiple subqueries. 

            ;

            // Error	5	Could not find an implementation of the query pattern for source type 'TestSQLiteGroupBy.Data.Book1.Middle'.  'GroupBy' not found.	X:\jsc.svn\examples\javascript\forms\Test\TestSQLiteGroupBy\TestSQLiteGroupBy\ApplicationWebService.cs	93	31	TestSQLiteGroupBy





            // http://www.viblend.com/Questions/WinForms/HowToGroupWinFormsDataGridViewByColumn.aspx
            // tooltips?

            //var d = g.Join
            var zzz = g.AsEnumerable();

            return zzz;

            //var u
            //var u = QueryStrategyExtensions.AsDataTable(g);


            //var g = from z in zzz
            //        group z by z.GooStateEnum;

            // 0380:01:01 RewriteToAssembly error: System.NotImplementedException: { SourceType = System.Linq.IGrouping`2[TestSQLiteGroupBy.Data.GooStateEnum,TestSQLiteGroupBy.Data.Book1MiddleRow] }

            //throw null;

            //return g.Select(gg => new XGrouping { Key = gg.Key, Items = gg.AsEnumerable() });
        }

    }

    [Obsolete("there is no good translation of such queries to SQL and Linq-to-SQL has to resort to doing multiple subqueries.")]
    public interface IQueryStrategyGrouping<TKey, TSource>
    {
        IQueryStrategy<TSource> source { get; set; }
        Expression<Func<TSource, TKey>> keySelector { get; set; }
    }

    public static class X
    {
        #region XQueryStrategy
        class XQueryStrategy<TRow> : IQueryStrategy<TRow>
        {

            List<Action<QueryStrategyExtensions.CommandBuilderState>> InternalCommandBuilder = new List<Action<QueryStrategyExtensions.CommandBuilderState>>();

            public List<Action<QueryStrategyExtensions.CommandBuilderState>> GetCommandBuilder()
            {
                return InternalCommandBuilder;
            }

            public Func<IQueryDescriptor> InternalGetDescriptor;

            public IQueryDescriptor GetDescriptor()
            {
                //  public static DataTable AsDataTable(IQueryStrategy Strategy)

                return InternalGetDescriptor();
            }
        }

        #endregion




        class XQueryStrategyGrouping<TKey, TSource> : IQueryStrategyGrouping<TKey, TSource>
        {
            public IQueryStrategy<TSource> source { get; set; }
            public Expression<Func<TSource, TKey>> keySelector { get; set; }
        }


        // group by . into .
        public static IQueryStrategy<TResult>
                Select<TSource, TKey, TResult>(
             this IQueryStrategyGrouping<TKey, TSource> source,
             Expression<Func<IGrouping<TKey, TSource>, TResult>> keySelector)
        {
            // source = {TestSQLiteGroupBy.X.XQueryStrategy<System.Linq.IGrouping<TestSQLiteGroupBy.Data.GooStateEnum,TestSQLiteGroupBy.Data.Book1MiddleRow>>}
            // keySelector = {GroupByGoo => new Book1MiddleAsGroupByGooWithCountRow() {GooStateEnum = GroupByGoo.Key, Count = Convert(GroupByGoo.Count())}}

            // we are about to create a view just like we do in the join.
            // http://stackoverflow.com/questions/9287119/get-first-row-for-one-group


            //select GooStateEnum, count(*)
            //from `Book1.Middle`


            var that = new XQueryStrategy<TResult>
            {


                InternalGetDescriptor =
                    () =>
                    {
                        // inherit the connection/context from above
                        var StrategyDescriptor = source.source.GetDescriptor();

                        return StrategyDescriptor;
                    }
            };

            // X:\jsc.svn\core\ScriptCoreLib.Extensions\ScriptCoreLib.Extensions\Shared\Data\Diagnostics\QueryStrategyExtensions.cs
            that.GetCommandBuilder().Add(
                 state =>
                 {
                     var s = QueryStrategyExtensions.AsCommandBuilder(source.source);

                     // http://www.xaprb.com/blog/2006/12/07/how-to-select-the-firstleastmax-row-per-group-in-sql/


                     // for the new view
                     // count is easy. 
                     // views should not care about keys, tags and timestamps?

                     // well the last seems to work
                     // not the first.

                     state.SelectCommand =
                         "select g.GooStateEnum as GooStateEnum"

                         + ", g.Key as LastKey"

                        + ", g.x as Lastx"
                        + ", g.Title as LastTitle"

                        + ", g.FirstKey as FirstKey"
                        + ", g.Firstx as Firstx"
                        + ", g.FirstTitle as FirstTitle"

                        + ", g.Count as Count"

                        + ", '' as Tag, 0 as Timestamp";

                     // how do we get the first and the last in the same query??

                     // http://www.w3schools.com/sql/sql_func_last.asp
                     s.SelectCommand = "select "
                        + "x, min(x) as Firstx, max(x) as Lastx, "
                        + "Key, min(Key) as FirstKey, max(Key) as LastKey, "
                        + "Title, min(Title) as FirstTitle, max(Title) as LastTitle, "
                        + "GooStateEnum, count(*) as Count";

                     s.GroupByCommand = "group by GooStateEnum";


                     //select g.GooStateEnum as GooStateEnum, g.Count as Count
                     //from (
                     //        select GooStateEnum, count(*) as Count
                     //        from `Book1.Middle`
                     //         where `FooStateEnum` = @arg0 and `Ratio` > @arg1 and `Ratio` < @arg2


                     //        group by GooStateEnum
                     //        ) as g


                     // how can we pass arguments to the flattened where?\
                     // g seems to be last inserted?
                     state.FromCommand =
                          "from (\n\t"
                            + s.ToString().Replace("\n", "\n\t")
                            + "\n) as g";

                     //s.OrderByCommand = "order by GooStateEnum desc";

                     ////s.FromCommand = "from (select * " + s.FromCommand + " order by GooStateEnum desc)";

                     ////state.FromCommand += ", (\n\t"
                     ////       + s.ToString().Replace("\n", "\n\t")
                     ////       + "\n) as gFirst ";



                     // copy em?
                     state.ApplyParameter.AddRange(s.ApplyParameter);

                 }
             );


            return that;
        }

        public static IQueryStrategyGrouping<TKey, TSource>
            GroupBy<TSource, TKey>(
         this IQueryStrategy<TSource> source,
         Expression<Func<TSource, TKey>> keySelector)
        {
            return new XQueryStrategyGrouping<TKey, TSource> { source = source, keySelector = keySelector };
        }
    }

    public class XGrouping
    {
        public GooStateEnum Key;
        public IEnumerable<Book1MiddleRow> Items;
    }
}
