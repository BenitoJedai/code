using ScriptCoreLib;
using ScriptCoreLib.Delegates;
using ScriptCoreLib.Extensions;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace MultipleXLSXAssets
{
    using MultipleXLSXAssets.Data;
    using ScriptCoreLib.Shared.Data.Diagnostics;
    using System.Diagnostics;


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
        public void WebMethod2()
        {
            // X:\jsc.svn\core\ScriptCoreLib.Extensions\ScriptCoreLib.Extensions\Shared\Data\Diagnostics\QueryStrategyOfTRowExtensions.Join.cs

            // X:\jsc.svn\examples\javascript\forms\ThreeWay\ThreeWay\ApplicationControl.cs
            // X:\jsc.svn\examples\javascript\forms\Test\TestSQLJoin\TestSQLJoin\ApplicationWebService.cs



            // ? new SchemaViewsMiddleViewContext()

            //Error	1	Could not find an implementation of the query pattern for source type 'MultipleXLSXAssets.Data.Schema.MiddleSheet'.  'GroupJoin' not found.	X:\jsc.svn\examples\javascript\forms\MultipleXLSXAssets\MultipleXLSXAssets\ApplicationWebService.cs	41	32	MultipleXLSXAssets

            // X:\jsc.svn\examples\javascript\Test\TestGroupJoin\TestGroupJoin\ApplicationWebService.cs
            //Error	1	Could not find an implementation of the query pattern for source type 
            // 'ScriptCoreLib.Shared.Data.Diagnostics.IQueryStrategyGroupingBuilder<
            // MultipleXLSXAssets.Data.SchemaMiddleSheetUpdatesKey,
            // MultipleXLSXAssets.Data.SchemaMiddleSheetUpdatesRow>'.  
            // 'Join' not found.	
            // X:\jsc.svn\examples\javascript\forms\MultipleXLSXAssets\MultipleXLSXAssets\ApplicationWebService.cs	46	32	MultipleXLSXAssets



            //Error	3	The type arguments for method 
            // 'System.Data.QueryStrategyOfTRowExtensions.Join<TOuter,TInner,TKey,TResult>(
            // ScriptCoreLib.Shared.Data.Diagnostics.IQueryStrategyGroupingBuilder<TOuter>, 
            // ScriptCoreLib.Shared.Data.Diagnostics.IQueryStrategy<TInner>, 
            // System.Linq.Expressions.Expression<System.Func<TOuter,TKey>>, 
            // System.Linq.Expressions.Expression<System.Func<TInner,TKey>>, 
            // System.Linq.Expressions.Expression<System.Func<TOuter,TInner,TResult>>)' 
            // cannot be inferred from the usage. Try specifying the type arguments explicitly.	X:\jsc.svn\examples\javascript\forms\MultipleXLSXAssets\MultipleXLSXAssets\ApplicationWebService.cs	55	21	MultipleXLSXAssets

            //{
            //    var jj =

            //        QueryStrategyOfTRowExtensions.Join(
            //        //(IQueryStrategyGroupingBuilder<SchemaMiddleSheetRow>)
            //        QueryStrategyOfTRowExtensions.GroupBy(new Schema.MiddleSheetUpdates(), iu => iu.MiddleSheet),
            //            new Schema.MiddleSheet(),
            //            g => g.Key,
            //            im => im.Key,
            //        //(g, im) => Tuple.Create(g, im)
            //            (g, im) => new { g, im }
            //        )
            //        .Join(new Schema.LeftSheet(), s => s.im.Key, il => il.MiddleSheet, (s, il) => new { s.g, s.im, il })
            //        .Join(new Schema.RightSheet(), s => s.im.Key, ir => ir.MiddleSheet, (s, ir) => new { s.g, s.im, s.il, ir })
            //        .Select(
            //            s =>
            //                new SchemaViewsMiddleViewRow
            //                {
            //                    // updated
            //                    Content = s.g.Last().UpdatedContent,
            //                }
            //        );
            //}


            //{
            //    var jj = new Schema.MiddleSheetUpdates().AsEnumerable()
            //        .GroupBy(iu => iu.MiddleSheet)
            //        .Join(new Schema.MiddleSheet().AsEnumerable(), g => g.Key, im => im.Key, (g, im) => new { g, im })
            //        .Join(new Schema.LeftSheet().AsEnumerable(), s => s.im.Key, il => il.MiddleSheet, (s, il) => new { s.g, s.im, il })
            //        .Join(new Schema.RightSheet().AsEnumerable(), s => s.im.Key, ir => ir.MiddleSheet, (s, ir) => new { s.g, s.im, s.il, ir })
            //        .Select(
            //            s =>
            //                new SchemaViewsMiddleViewRow
            //                {
            //                    // updated
            //                    Content = s.g.Last().UpdatedContent,
            //                }
            //        );
            //


            // https://sites.google.com/a/jsc-solutions.net/backlog/knowledge-base/2014/201405/20140513


            var k = new Schema.MiddleSheet().Insert(
                new SchemaMiddleSheetRow { Content = "0+0" }
                );

            var c = new Schema.MiddleSheetUpdates();

            //c.Insert(new SchemaMiddleSheetUpdatesRow { UpdatedContent = "0+0" });
            //c.Insert(new SchemaMiddleSheetUpdatesRow { UpdatedContent = "0+1" });
            //c.Insert(new SchemaMiddleSheetUpdatesRow { UpdatedContent = "0+2" });

            c.Insert(new SchemaMiddleSheetUpdatesRow { UpdatedContent = ((long)k) + "+0", MiddleSheet = k });
            c.Insert(new SchemaMiddleSheetUpdatesRow { UpdatedContent = ((long)k) + "+1", MiddleSheet = k });
            c.Insert(new SchemaMiddleSheetUpdatesRow { UpdatedContent = ((long)k) + "+2", MiddleSheet = k });

            new Schema.LeftSheet().Insert(new SchemaLeftSheetRow { MiddleSheet = k, LeftContent = ((long)k) + "+left" });
            new Schema.RightSheet().Insert(new SchemaRightSheetRow { MiddleSheet = k, RightContent = ((long)k) + "+right" });

            //select g.`Grouping.Key`,
            //         g.`MiddleSheet` as `MiddleSheet`,
            //         g.`UpdatedContent` as `UpdatedContent`,
            //         g.`Tag` as `Tag`,
            //         g.`Timestamp` as `Timestamp`
            //from (
            //        select s.`MiddleSheet` as `Grouping.Key`,
            //                 s.`MiddleSheet` as `MiddleSheet`,
            //                 s.`UpdatedContent` as `UpdatedContent`,
            //                 s.`Tag` as `Tag`,
            //                 s.`Timestamp` as `Timestamp`
            //         from `Schema.MiddleSheetUpdates` as s
            //         group by `Grouping.Key`
            //) as g

            // X:\jsc.svn\examples\javascript\forms\Test\TestSQLiteGroupBy\TestSQLiteGroupBy\ApplicationWebService.cs

            //var jlast = from iu in new Schema.MiddleSheetUpdates()
            //            group iu by iu.MiddleSheet into g
            //            //where g.MiddleSheet == (SchemaMiddleSheetKey)7
            //            select g.Last();

            //var aa = jlast.AsDataTable();


            //var jlastjoin =
            //    from iu in new Schema.MiddleSheetUpdates()
            //    group iu by iu.MiddleSheet into g
            //    //where g.MiddleSheet == (SchemaMiddleSheetKey)7
            //    select g.Last() into jlastt
            //    join im in new Schema.MiddleSheet() on jlastt.MiddleSheet equals im.Key
            //    select new SchemaViewsMiddleViewRow
            //        {

            //            MiddleViewKey = (long)jlastt.MiddleSheet,

            //            Content = jlastt.UpdatedContent,
            //            Tag = im.Content
            //        };

            //var aaa = jlastjoin.AsDataTable();

            var j = from iu in new Schema.MiddleSheetUpdates()
                    group iu by iu.MiddleSheet into UpdatesByMiddlesheet
                    join MiddleSheetz in new Schema.MiddleSheet() on UpdatesByMiddlesheet.Key equals MiddleSheetz.Key
                    join LeftSheetz in new Schema.LeftSheet() on MiddleSheetz.Key equals LeftSheetz.MiddleSheet
                    join RightSheetz in new Schema.RightSheet() on MiddleSheetz.Key equals RightSheetz.MiddleSheet

                    //let last = g.Last()

                    //let x = "???"

                    //let s = new { g, im, il, ir }

                    select new SchemaViewsMiddleViewRow
                    {
                        Timestamp = MiddleSheetz.Timestamp,
                        //Key = im.Key,

                        // updated
                        Content = UpdatesByMiddlesheet.Last().UpdatedContent,
                        //Content = last.UpdatedContent,

                        // original
                        //Tag = x,
                        Tag = MiddleSheetz.Content,

                        MiddleViewKey = (long)MiddleSheetz.Key,

                        //LatestLeftContent = x,
                        //LatestRightContent = "goo",

                        LatestLeftContent = LeftSheetz.LeftContent,
                        LatestRightContent = RightSheetz.RightContent,
                    };


            var ac = j.Count();
            var a = j.AsDataTable();

            Debugger.Break();
        }

    }
}
