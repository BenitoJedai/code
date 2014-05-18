#define SQLite

using Abstractatech.JavaScript.ApplicationPerformance;
using ScriptCoreLib;
using ScriptCoreLib.Delegates;
using ScriptCoreLib.Extensions;
using SVGNavigationTiming.Design;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Xml.Linq;
using SVGNavigationTiming;
using System.Data.SQLite;


namespace SVGNavigationTiming
{
    // http://www.remondo.net/calculate-mean-median-mode-averages-csharp/

    /// <summary>
    /// Methods defined in this type can be used from JavaScript. The method calls will seamlessly be proxied to the server.
    /// </summary>
    public partial class XApplicationPerformanceWebService
    {
        // wtf?

        // http://stackoverflow.com/questions/1606867/how-to-prevent-a-net-application-to-use-an-assembly-from-the-gac



        /// <summary>
        /// This Method is a javascript callable method.
        /// </summary>
        /// <param name="e">A parameter from javascript.</param>
        /// <param name="y">A callback to javascript.</param>
        public Task WebMethod2500()
        {

            Thread.Sleep(2500);

            return "ok".AsResult();
        }

        public Task WebMethod500()
        {

            Thread.Sleep(500);

            return "ok".AsResult();
        }

        public Task<string> WebMethod22()
        {

            Thread.Sleep(22);

            return "ok".AsResult();
        }


        public const long TicksPerMillisecond = 10000;
        public const long ticks_1970_1_1 = 621355968000000000;

        #region Reset
        public Task Reset()
        {


            return new Design.PerformanceResourceTimingData2.ApplicationPerformance.Queries().WithConnection(
                c =>
                {
                    #region drop
                    Action<string, string> drop =
                        (QualifiedTableName, sql) =>
                        {
                            Console.WriteLine("drop " + new { QualifiedTableName });
                            try
                            {

                                var xvalue = new System.Data.SQLite.SQLiteCommand(sql, (SQLiteConnection)c).ExecuteNonQuery();
                                Console.WriteLine(new { QualifiedTableName, xvalue });

                                Console.WriteLine("ok " + new { QualifiedTableName });
                            }
                            catch (Exception e)
                            {
                                Console.WriteLine("nok " + new { QualifiedTableName });

                                //Console.WriteLine(new { QualifiedTableName, e.Message, e.StackTrace });
                                Console.WriteLine(new { QualifiedTableName, e.Message });
                            }
                        };
                    #endregion

                    drop(Design.PerformanceResourceTimingData2.ApplicationPerformance.Queries.QualifiedTableName, Design.PerformanceResourceTimingData2.ApplicationPerformance.Queries.DropCommandText);
                    drop(Design.PerformanceResourceTimingData2.ApplicationResourcePerformance.Queries.QualifiedTableName, Design.PerformanceResourceTimingData2.ApplicationResourcePerformance.Queries.DropCommandText);

                    return "".AsResult();
                }
            );
        }
        #endregion

    }

    #region IExploreApplicationPerformance
    public partial class XApplicationPerformanceWebService : IExploreApplicationPerformance
    {
        public async Task<DataTable> GetApplicationPerformance()
        {
            // https://sites.google.com/a/jsc-solutions.net/backlog/knowledge-base/2014/201401/20140114

            var x = new Design.PerformanceResourceTimingData2.ApplicationPerformance();

            return x.AsDataTable();
        }

        [Obsolete("we could allow the client tell use, by which parameter to order by")]
        public

            // wont work yet here, why?
            //async 
            Task<DataTable> GetSimilarApplicationResourcePerformance(Design.PerformanceResourceTimingData2ApplicationResourcePerformanceRow k)
        {
            // 02000051 SVGNavigationTiming.XApplicationPerformanceWebService::<>f__AnonymousType$371$3`2
            //pt: error JSC1000: *** stack is empty, invalid pop?
            //pt: error JSC1000: error at SVGNavigationTiming.XApplicationPerformanceWebService+<GetSimilarApplicationResourcePerformance>d__10+<MoveNext>0600004b.<0149> ldsfld.try,
            //embly: W:\SVGNavigationTiming.XApplicationPerformanceWebService.exe
            //e: SVGNavigationTiming.XApplicationPerformanceWebService+<GetSimilarApplicationResourcePerformance>d__10+<MoveNext>0600004b, SVGNavigationTiming.XApplicationPerformanceWebService, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null


            // https://sites.google.com/a/jsc-solutions.net/backlog/knowledge-base/2014/201401/20140114
            // http://msdn.microsoft.com/en-us/library/aa287599(v=vs.71).aspx

            // autoconvert?
            var data = new Design.PerformanceResourceTimingData2.ApplicationResourcePerformance()

                .Where(x => x.path == k.path)
                .OrderBy(x => x.duration)
                .AsEnumerable()
                .ToList();




            data.AddRange(
                new[] {
                    new Design.PerformanceResourceTimingData2ApplicationResourcePerformanceRow
                    {
                        // move to sql?
                        name = "Average", duration = (long)data.Average(x => x.duration)
                    },

                    new Design.PerformanceResourceTimingData2ApplicationResourcePerformanceRow
                    {
                        name = "Min", duration = (long)data.Min(x => x.duration)
                    },

                    new Design.PerformanceResourceTimingData2ApplicationResourcePerformanceRow
                    {
                        name = "Max", duration = (long)data.Max(x => x.duration)
                    },


                    //                    Implementation not found for type import :
                    //type: System.Linq.Enumerable
                    //method: System.Collections.Generic.IEnumerable`1[System.Linq.IGrouping`2[TKey,TSource]] GroupBy[TSource,TKey](System.Collections.Generic.IEnumerable`1[TSource], System.Func`2[TSource,TKey])
                    //Did you forget to add the [Script] attribute?
                    //Please double check the signature!

                    //    new PerformanceResourceTimingData2ApplicationResourcePerformanceRow
                    //{
                    //    // http://www.remondo.net/calculate-mean-median-mode-averages-csharp/
                    //    name = "Median", duration = (long)data.Median(x => x.duration)
                    //},

            }
            );



            return

                data
                .AsEnumerable()
                .Reverse()
                .AsDataTable()

                // when can we revert to async?
                .AsResult();
        }





        public async Task<IEnumerable<PerformanceResourceTimingData2ApplicationResourcePerformanceRow>> GetApplicationResourcePerformance(PerformanceResourceTimingData2ApplicationPerformanceKey key)
        {
            // X:\jsc.svn\core\ScriptCoreLib.Extensions\ScriptCoreLib.Extensions\Shared\Data\Diagnostics\QueryStrategyExtensions.cs
            // https://sites.google.com/a/jsc-solutions.net/backlog/knowledge-base/2014/201401/20140114

            return (
                from k in new PerformanceResourceTimingData2.ApplicationResourcePerformance()
                where k.ApplicationPerformance == key
                orderby k.path descending
                select k
            ).AsEnumerable();
        }


    }


    #endregion

    #region IUpstreamApplicationPerformance
    public partial class XApplicationPerformanceWebService : IUpstreamApplicationPerformance
    {
        public Task<Design.PerformanceResourceTimingData2ApplicationPerformanceKey> AtApplicationPerformance(Design.PerformanceResourceTimingData2ApplicationPerformanceRow value)
        {
            //var ticks = DateTime.Now.Ticks;
            //var ms = (ticks - ticks_1970_1_1) / TicksPerMillisecond;

            //value.Timestamp = DateTime.Now;
            //value.Timestamp = new DateTime(year: 2005, month: 2, day: 2);

            //xQueries_Insert
            //xQueries_Insert { ColumnName = connectStart }
            //xQueries_Insert { ColumnName = connectEnd }
            //xQueries_Insert { ColumnName = requestStart }
            //xQueries_Insert { ColumnName = responseStart }
            //xQueries_Insert { ColumnName = responseEnd }
            //xQueries_Insert { ColumnName = domLoading }
            //xQueries_Insert { ColumnName = domComplete }
            //xQueries_Insert { ColumnName = loadEventStart }
            //xQueries_Insert { ColumnName = loadEventEnd }
            //xQueries_Insert { ColumnName = Timestamp }

            //Caused by: java.lang.RuntimeException: Data truncation: Out of range value for column 'connectStart' at row 1
            //        at ScriptCoreLibJava.BCLImplementation.System.Data.SQLite.__SQLiteCommand.ExecuteNonQuery(__SQLiteCommand.java:277)

            // http://dev.mysql.com/doc/refman/5.0/en/integer-types.html

            return new Design.PerformanceResourceTimingData2.ApplicationPerformance().Insert(value).AsResult();
        }

        public Task<PerformanceResourceTimingData2ApplicationResourcePerformanceKey> AtApplicationResourcePerformance(Design.PerformanceResourceTimingData2ApplicationResourcePerformanceRow value)
        {
            //var ticks = TotalMilliseconds * __DateTime.TicksPerMillisecond + __DateTime.ticks_1970_1_1; 

            //var ticks = DateTime.Now.Ticks;
            //var ms = (ticks - ticks_1970_1_1) / TicksPerMillisecond;

            //            Implementation not found for type import :
            //type: System.DateTime
            //method: Void .ctor(Int32, Int32, Int32)
            //Did you forget to add the [Script] attribute?
            //Please double check the signature!


            //            value.Timestamp = new DateTime(year: 2005, month: 2, day: 2);

            // check sig to prevent client side tamper
            //value.ApplicationPerformance = this.CurrentApplicationPerformance;

            value.path = value.name.SkipUntilIfAny("//").SkipUntilIfAny("/");

            return new Design.PerformanceResourceTimingData2.ApplicationResourcePerformance().Insert(value).AsResult();
        }
    }



    #endregion




    [Obsolete]
    public static class X
    {
        //public static DataTable WithoutColumn(this DataTable x, string columnName)
        //{
        //    x.Columns.Remove(columnName);

        //    return x;
        //}

        public static DataTable XAsDataTable(this IEnumerable<Design.PerformanceResourceTimingData2ApplicationResourcePerformanceRow> source)
        {
            var x = new DataTable();

            // Column 'Key' does not belong to table .

            x.Columns.Add("Key");
            x.Columns.Add("connectEnd");
            x.Columns.Add("connectStart");
            x.Columns.Add("duration");
            x.Columns.Add("entryType");
            x.Columns.Add("name");
            x.Columns.Add("requestStart");
            x.Columns.Add("responseEnd");
            x.Columns.Add("responseStart");
            x.Columns.Add("startTime");
            x.Columns.Add("ApplicationPerformance");
            x.Columns.Add("Timestamp");

            // The runtime has encountered a fatal error. The address of the error was at 0x715f4ba0, on thread 0x2290. The error code is 0xc0000005. This error may be a bug in the CLR or in the unsafe or non-verifiable portions of user code. 
            // Common sources of this bug include user marshaling errors for COM-interop or PInvoke, which may corrupt the stack.

            source.WithEach(
                item =>
                {
                    var n = x.NewRow();

                    n["Key"] = item.Key;

                    n["connectEnd"] = item.connectEnd;
                    n["connectStart"] = item.connectStart;
                    n["duration"] = item.duration;
                    n["entryType"] = item.entryType;
                    n["name"] = item.name;
                    n["requestStart"] = item.requestStart;
                    n["responseEnd"] = item.responseEnd;
                    n["responseStart"] = item.responseStart;
                    n["startTime"] = item.startTime;
                    n["ApplicationPerformance"] = item.ApplicationPerformance;

                    // Uncaught Error: InvalidOperationException: parseInt failed for 1/1/1970 12:00:00 AM
                    n["Timestamp"] = ScriptCoreLib.Library.StringConversionsForStopwatch.DateTimeConvertToString(item.Timestamp);



                    x.Rows.Add(n);
                }
            );


            return x;
        }
    }
}






namespace Abstractatech.JavaScript.ApplicationPerformance
{
    public interface IUpstreamApplicationPerformance
    {
        Task<PerformanceResourceTimingData2ApplicationPerformanceKey> AtApplicationPerformance(PerformanceResourceTimingData2ApplicationPerformanceRow value);
        Task<PerformanceResourceTimingData2ApplicationResourcePerformanceKey> AtApplicationResourcePerformance(PerformanceResourceTimingData2ApplicationResourcePerformanceRow value);
    }

    public interface IExploreApplicationPerformance
    {
        Task<DataTable> GetApplicationPerformance();
        Task<DataTable> GetSimilarApplicationResourcePerformance(PerformanceResourceTimingData2ApplicationResourcePerformanceRow k);


        Task<IEnumerable<PerformanceResourceTimingData2ApplicationResourcePerformanceRow>> GetApplicationResourcePerformance(PerformanceResourceTimingData2ApplicationPerformanceKey key);
        //Task<DataTable> GetApplicationResourcePerformance(PerformanceResourceTimingData2ApplicationPerformanceKey k);
    }
}