using ScriptCoreLib;
using ScriptCoreLib.Delegates;
using ScriptCoreLib.Extensions;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using MinMaxAverageExperiment.Data;
using System.Diagnostics;

namespace MinMaxAverageExperiment
{
    /// <summary>
    /// Methods defined in this type can be used from JavaScript. The method calls will seamlessly be proxied to the server.
    /// </summary>
    public class ApplicationWebService
    {
        /// <summary>
        /// This Method is a javascript callable method.
        /// </summary>
        /// <param name="e">A parameter from javascript.</param>
        /// <param name="y">A callback to javascript.</param>
        public void WebMethod2()
        {
            // X:\jsc.svn\examples\javascript\svg\SVGNavigationTiming\SVGNavigationTiming\ApplicationWebService.cs

            var x = new PerformanceResourceTimingData2.ApplicationResourcePerformance();

            x.Insert(
                new PerformanceResourceTimingData2ApplicationResourcePerformanceRow { duration = 44, path = " /zfoo/BAR/ " }
            );


            //            select 0 as foo,
            //         g.`path` as `path`,
            //         g.`ToLower` as `ToLower`,
            //         g.`ToUpper` as `ToUpper`,
            //         g.`Length` as `Length`
            //from(
            //        select 0 as foo,
            //                 s.`path` as `path`,
            //                 lower(s.`path`) as `ToLower`,
            //                 upper(s.`path`) as `ToUpper`,
            //                 length(s.`path`) as `Length`
            //         from `PerformanceResourceTimingData2.ApplicationResourcePerformance` as s
            //) as g

            var uc = from k in new PerformanceResourceTimingData2.ApplicationResourcePerformance()

                     where !string.IsNullOrEmpty(k.path)
                     where k.path.Contains("zfoo")

                     select new
                     {
                         xxfoo = "bar",

                         k.Timestamp,

                         k.Key,


                         k.path,
                         Trim = k.path.Trim(),
                         TrimStart = k.path.TrimStart(),
                         TrimEnd = k.path.TrimEnd(),
                         ToLower = k.path.ToLower(),
                         ToUpper = k.path.ToUpper(),

                         // www.w3schools.com/sql/sql_func_len.asp
                         k.path.Length,

                     };

            // what about our own PrimaryConstructor for select?
            var uc0 = uc.AsDataTable();
            var uc1 = uc.AsGenericEnumerable();
            var uc2 = uc.FirstOrDefault();


            //        Select { selector = k => new <> f__AnonymousType0`2(path = k.path, Length = k.path.Length) }
            //        AsDataTable {
            //            Strategy = System.Data.QueryStrategyOfTRowExtensions + SelectQueryStrategy`2[MinMax
            //Select CommandBuilder
            //select `Key`, `name`, `path`, `entryType`, `duration`, `startTime`, `connectStart`, `connectEnd
            //from `PerformanceResourceTimingData2.ApplicationResourcePerformance`

            Debugger.Break();

            x.Insert(
                 new PerformanceResourceTimingData2ApplicationResourcePerformanceRow { duration = 999 }
            );


            // http://stackoverflow.com/questions/1107868/linq-min-max
            // http://csharpbasic.blogspot.com/2008/08/exploring-linq-functions-select-min-max.html
            // http://blogs.msdn.com/b/mattwar/archive/2008/07/08/linq-building-an-iqueryable-provider-part-x.aspx
            // http://oakleafblog.blogspot.com/2008/07/linq-and-entity-framework-posts-for_14.html


            var min = new PerformanceResourceTimingData2.ApplicationResourcePerformance().Min(k => k.duration);

            var e = from z in new PerformanceResourceTimingData2.ApplicationResourcePerformance()
                        // .AsEnumerable()
                    group z by 1 into g
                    select new
                    {
                        Min = g.Min(k => k.duration),
                        Max = g.Max(k => k.duration),
                        Average = g.Average(k => k.duration),

                        Count = g.Count(),
                        Sum = g.Sum(k => k.duration)

                        //max = z.duration.Max(),
                        //avg = z.duration.Average(),
                    };

            // [0] = { Min = 44, Max = 999, Average = 521.5, Count = 2 }


            //GroupBy { keySelector = z => 1 }
            //Select { selector = g => new <> f__AnonymousType0`1(Count = g.Count()) }


            //error:
            //    {
            //        Message = Object reference not set to an instance of an object., ex = System.NullReferenceException: Object reference not set to an instance of an object.
            // at System.Data.QueryStrategyOfTRowExtensions.<> c__DisplayClass12`3.< GroupBy > b__8(CommandBuilderState state) in x:\jsc.svn\core\ScriptCoreLib.Extensions\ScriptCoreLib.Extensions\Shared\Data\Diagnostics\QueryStrategyOfTRowExtensions.GroupBy.cs:line 298

            // https://sites.google.com/a/jsc-solutions.net/backlog/knowledge-base/2014/201405/20140518-1/roslyn
            // what about generic AsEnumerable ?

            var a = e.AsDataTable();

            //            select g.`Grouping.Key`,
            //         g.`Count` as `Count`
            //from(
            //        select 1 as `Grouping.Key`,
            //                 count(*) as `Count`
            //         from `PerformanceResourceTimingData2.ApplicationResourcePerformance` as s
            //         group by `Grouping.Key`
            //) as g

            //select g.`Grouping.Key`,
            //         g.`Count` as `Count`,
            //         g.`Sum` as `Sum`
            //from(
            //        select 1 as `Grouping.Key`,
            //                 count(*) as `Count`,
            //                 sum(s.`duration`) as `Sum`
            //         from `PerformanceResourceTimingData2.ApplicationResourcePerformance` as s
            //         group by `Grouping.Key`
            //) as g


            Debugger.Break();
        }

    }
}
