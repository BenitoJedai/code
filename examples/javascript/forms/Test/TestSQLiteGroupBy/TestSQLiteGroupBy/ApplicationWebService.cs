using ScriptCoreLib;
using ScriptCoreLib.Delegates;
using ScriptCoreLib.Extensions;
using ScriptCoreLib.Shared.Data.Diagnostics;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
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
            // C:\Program Files (x86)\Java\jdk1.7.0_45\bin\javac.exe  -encoding UTF-8 -cp Y:\TestSQLiteGroupBy.ApplicationWebService\staging.java\web\release;C:\util\appengine-java-sdk-1.8.8\lib\impl\*;C:\util\appengine-java-sdk-1.8.8\lib\shared\* -d "Y:\TestSQLiteGroupBy.ApplicationWebService\staging.java\web\release" @"Y:\TestSQLiteGroupBy.ApplicationWebService\staging.java\web\files"


            // Error	4	Could not find an implementation of the query pattern for source type 'ScriptCoreLib.Shared.Data.Diagnostics.IQueryStrategy<AnonymousType#1>'.  'Select' not found.	X:\jsc.svn\examples\javascript\forms\Test\TestSQLiteGroupBy\TestSQLiteGroupBy\ApplicationWebService.cs	136	31	TestSQLiteGroupBy


            // X:\jsc.svn\examples\javascript\forms\Test\TestSQLGroupByAfterJoin\TestSQLGroupByAfterJoin\ApplicationWebService.cs

            var g = from z in x

                    // now what?
                    // does or work yet?
                    where z.FooStateEnum == FooStateEnum.Foo0 || z.FooStateEnum == FooStateEnum.Foo2
                    where z.Ratio == SpecialRatio

                    //where z.Ratio > 0.1
                    //where z.Ratio < 0.9
                    //where z.GooStateEnum == GooStateEnum.Goo2
                    //select z

                    group z by z.GooStateEnum into GroupByGoo


                    //let FirstTitle = GroupByGoo.First().Title

                    select new Book1MiddleAsGroupByGooWithCountRow
                    {
                        // GroupByGoo cannot have 0 members

                        GooStateEnum = GroupByGoo.Key,


                        Count = GroupByGoo.Count(),


                        // do we have to do multple from clauses for ordering first and last?

                        FirstTitle = GroupByGoo.First().Title,
                        //FirstTitle = FirstTitle,

                        FirstKey = GroupByGoo.First(),
                        Firstx = GroupByGoo.First().x,

                        LastKey = GroupByGoo.Last(),
                        LastTitle = GroupByGoo.Last().Title,
                        Lastx = GroupByGoo.Last().x,

                        SumOfx = GroupByGoo.Sum(u => u.x),


                        // X:\jsc.svn\examples\javascript\forms\Test\TestSQLJoin\TestSQLJoin\ApplicationWebService.cs
                        //Tag = "",
                        Tag = GroupByGoo.Last().Tag,
                        Timestamp = GroupByGoo.Last().Timestamp,

                        // count is easy what about
                        // getting the first or last items?

                    };

            // http://friism.com/linq-to-sql-group-by-subqueries-and-performance
            // !! This is because there is no good translation of such queries to SQL and Linq-to-SQL has to resort to doing multiple subqueries. 

            ;

            // Error	5	Could not find an implementation of the query pattern for source type 'TestSQLiteGroupBy.Data.Book1.Middle'.  'GroupBy' not found.	X:\jsc.svn\examples\javascript\forms\Test\TestSQLiteGroupBy\TestSQLiteGroupBy\ApplicationWebService.cs	93	31	TestSQLiteGroupBy





            // http://www.viblend.com/Questions/WinForms/HowToGroupWinFormsDataGridViewByColumn.aspx
            // tooltips?

            // X:\jsc.svn\examples\javascript\forms\MultipleXLSXAssets\MultipleXLSXAssets\ApplicationWebService.cs
            var z0 = g.AsDataTable();

            //var d = g.Join
            var zzz = g.AsEnumerable();

            // script: error JSC1000: Java : class import: no implementation for ScriptCoreLib.Shared.Data.Diagnostics.IQueryDescriptor at TestSQLiteGroupBy.Data.Book1+MiddleAsGroupByGooWithCount

            return zzz;

            //var u
            //var u = QueryStrategyExtensions.AsDataTable(g);


            //var g = from z in zzz
            //        group z by z.GooStateEnum;

            // 0380:01:01 RewriteToAssembly error: System.NotImplementedException: { SourceType = System.Linq.IGrouping`2[TestSQLiteGroupBy.Data.GooStateEnum,TestSQLiteGroupBy.Data.Book1MiddleRow] }

            //throw null;

            //return g.Select(gg => new XGrouping { Key = gg.Key, Items = gg.AsEnumerable() });
        }



        //        Implementation not found for type import :
        //type: System.Reflection.PropertyInfo
        //method: System.Object GetValue(System.Object, System.Object[])
        //Did you forget to add the [Script] attribute?
        //Please double check the signature!


    }









}
