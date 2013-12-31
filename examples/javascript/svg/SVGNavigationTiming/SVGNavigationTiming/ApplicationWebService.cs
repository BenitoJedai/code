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

namespace System.Linq
{
    public static class Average
    {
        public static long Median<T>(this IEnumerable<T> list, Func<T, long> s)
        {
            return (long)list.Select(s).Median();
        }

        public static long Median(this IEnumerable<long> list)
        {
            return (long)list.Select(x => (double)x).Median();
        }

        public static double Median(this IEnumerable<double> list)
        {
            List<double> orderedList = list
                .OrderBy(numbers => numbers)
                .ToList();

            int listSize = orderedList.Count;
            double result;

            if (listSize % 2 == 0) // even
            {
                int midIndex = listSize / 2;
                result = ((orderedList.ElementAt(midIndex - 1) +
                           orderedList.ElementAt(midIndex)) / 2);
            }
            else // odd
            {
                double element = (double)listSize / 2;

                // http://stackoverflow.com/questions/311696/why-does-net-use-bankers-rounding-as-default
                element = Math.Round(element);
                //element = Math.Round(element, MidpointRounding.AwayFromZero);

                result = orderedList.ElementAt((int)(element - 1));
            }

            return result;
        }

        public static IEnumerable<double> Modes(this IEnumerable<double> list)
        {
            var modesList = list
                .GroupBy(values => values)
                .Select(valueCluster =>
                        new
                        {
                            Value = valueCluster.Key,
                            Occurrence = valueCluster.Count(),
                        })
                .ToList();

            int maxOccurrence = modesList
                .Max(g => g.Occurrence);

            return modesList
                .Where(x => x.Occurrence == maxOccurrence && maxOccurrence > 1) // Thanks Rui!
                .Select(x => x.Value);
        }
    }
}

namespace SVGNavigationTiming
{
    // http://www.remondo.net/calculate-mean-median-mode-averages-csharp/

    /// <summary>
    /// Methods defined in this type can be used from JavaScript. The method calls will seamlessly be proxied to the server.
    /// </summary>
    public partial class ApplicationWebService
    {

        //[IL]: Error: [X:\jsc.svn\examples\javascript\svg\SVGNavigationTiming\SVGNavigationTiming\bin\staging.AssetsLibrary\SVGNavigationTiming.AssetsLibrary.dll : SVGNavigationTiming.Design.PerformanceResourceTimingData2::GetDataSet][offset 0x00000005] Unable to resolve token.
        //[IL]: Error: [X:\jsc.svn\examples\javascript\svg\SVGNavigationTiming\SVGNavigationTiming\bin\staging.AssetsLibrary\SVGNavigationTiming.AssetsLibrary.dll : SVGNavigationTiming.Design.PerformanceResourceTimingData2+ApplicationPerformance+Queries::Create]  [HRESULT 0x80070002] - The system cannot find the file specified.

        //[IL]: Error: [X:\jsc.svn\examples\javascript\svg\SVGNavigationTiming\SVGNavigationTiming\bin\staging.AssetsLibrary\SVGNavigationTiming.AssetsLibrary.dll : SVGNavigationTiming.Design.PerformanceResourceTimingData2ApplicationPerformanceRow::ToString][offset 0x0000011F][found value 'System.DateTime'][expected ref 'System.Object'] Unexpected type on the stack.
        //3 Error(s) Verifying X:\jsc.svn\examples\javascript\svg\SVGNavigationTiming\SVGNavigationTiming\bin\staging.AssetsLibrary\SVGNavigationTiming.AssetsLibrary.dll







        // wthflip?
        // http://stackoverflow.com/questions/1606867/how-to-prevent-a-net-application-to-use-an-assembly-from-the-gac

        //Cannot process request because the process (1044) has exited.

        //   at System.Diagnostics.Process.GetProcessHandle(Int32 access, Boolean throwIfExited)
        //   at System.Diagnostics.Process.Kill()
        //   at jsc.meta.Commands.Configuration.ConfigurationDisposeSubst.<>c__DisplayClass2.<Monitor>b__1()
        //   at jsc.meta.Library.VolumeFunctions.VolumeFunctionsExtensions.ToVirtualDriveToDirectory.Dispose()
        //   at jsc.meta.Commands.Rewrite.RewriteToUltraApplication.RewriteToUltraApplication.AsProgram.Launch()
        //   at jsc.meta.Commands.Rewrite.RewriteToUltraApplication.RewriteToUltraApplication.AsProgram.Launch(Type PrimaryApplication)
        //   at SVGNavigationTiming.Program.Main(String[] args) in x:\jsc.svn\examples\javascript\svg\SVGNavigationTiming\SVGNavigationTiming\Program.cs:line 13

        // Could not load file or assembly 'System.Data.SQLite, Version=1.0.89.0, Culture=neutral, PublicKeyToken=db937bc2d44ff139' or one of its dependencies. The system cannot find the file specified.
        // can jsc security analyzer go one level deeper? atleast on [script] [merge] assemblies?
        public ApplicationWebService()
        {
#if SQLite
            //1a60:01:01 RewriteToAssembly error: System.IO.FileNotFoundException: Could not load file or assembly 'System.Data.SQLite, Version=1.0.90.0, Culture=neutral, PublicKeyToken=db937bc2d44ff139' or one of its dependencies. The system cannot find the file specified.
            //File name: 'System.Data.SQLite, Version=1.0.90.0, Culture=neutral, PublicKeyToken=db937bc2d44ff139' ---> System.IO.FileNotFoundException: Could not load file or assembly 'System.Data.SQLite, Version=1.0.89.0, Culture=neutral, PublicKeyToken=db937bc2d44ff139' or one of its dependencies. The system cannot find the file specified.
            //File name: 'System.Data.SQLite, Version=1.0.89.0, Culture=neutral, PublicKeyToken=db937bc2d44ff139'

            //=== Pre-bind state information ===
            //LOG: DisplayName = System.Data.SQLite, Version=1.0.89.0, Culture=neutral, PublicKeyToken=db937bc2d44ff139
            // (Fully-specified)
            //LOG: Appbase = file:///X:/jsc.svn/examples/javascript/svg/SVGNavigationTiming/SVGNavigationTiming/bin/Debug/
            //LOG: Initial PrivatePath = NULL
            //Calling assembly : jsc.meta, Version=0.86.0.518, Culture=neutral, PublicKeyToken=null.
            //===
            //LOG: This bind starts in default load context.
            //LOG: Using application configuration file: X:\jsc.svn\examples\javascript\svg\SVGNavigationTiming\SVGNavigationTiming\bin\Debug\SVGNavigationTiming.exe.Config
            //LOG: Using host configuration file:
            //LOG: Using machine configuration file from C:\Windows\Microsoft.NET\Framework\v4.0.30319\config\machine.config.
            //LOG: Redirect found in application configuration file: 1.0.89.0 redirected to 1.0.90.0.
            //LOG: Post-policy reference: System.Data.SQLite, Version=1.0.90.0, Culture=neutral, PublicKeyToken=db937bc2d44ff139
            //LOG: Attempting download of new URL file:///X:/jsc.svn/examples/javascript/svg/SVGNavigationTiming/SVGNavigationTiming/bin/Debug/System.Data.SQLite.DLL.
            //LOG: Attempting download of new URL file:///X:/jsc.svn/examples/javascript/svg/SVGNavigationTiming/SVGNavigationTiming/bin/Debug/System.Data.SQLite/System.Data.SQLite.DLL.
            //LOG: Attempting download of new URL file:///X:/jsc.svn/examples/javascript/svg/SVGNavigationTiming/SVGNavigationTiming/bin/Debug/System.Data.SQLite.EXE.
            //LOG: Attempting download of new URL file:///X:/jsc.svn/examples/javascript/svg/SVGNavigationTiming/SVGNavigationTiming/bin/Debug/System.Data.SQLite/System.Data.SQLite.EXE.

            { var r = typeof(global::System.Data.SQLite.SQLiteCommand); }
            { var r = typeof(global::ScriptCoreLib.Shared.Data.Diagnostics.WithConnectionLambda); }
#endif
        }

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

                                var xvalue = new System.Data.SQLite.SQLiteCommand(sql, c).ExecuteNonQuery();
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
    public partial class ApplicationWebService : IExploreApplicationPerformance
    {
        public Task<DataTable> GetApplicationPerformance()
        {
            //Task.FromResult
            return new Design.PerformanceResourceTimingData2.ApplicationPerformance().SelectAllAsDataTable().AsResult();
        }

        public Task<DataTable> GetSimilarApplicationResourcePerformance(Design.PerformanceResourceTimingData2ApplicationResourcePerformanceRow k)
        {
            // http://msdn.microsoft.com/en-us/library/aa287599(v=vs.71).aspx

            var data = new Design.PerformanceResourceTimingData2.ApplicationResourcePerformance()
             .SelectAllAsEnumerable()

             .Where(
                // host:port will differ
                z => z.name.SkipUntilIfAny("//").SkipUntilIfAny("/") == k.name.SkipUntilIfAny("//").SkipUntilIfAny("/"))


             .ToList();

            //Y:\SVGNavigationTiming.ApplicationWebService\staging.java\web\java\SVGNavigationTiming\ApplicationWebService.java:114: error: no suitable method found for Average(__List_1<PerformanceResourceTimingData2ApplicationResourcePerformanceRow>,__Func_2<PerformanceResourceTimingData2ApplicationResourcePerformanceRow,Long>)
            //        row1.duration = ((long)(__Enumerable.<PerformanceResourceTimingData2ApplicationResourcePerformanceRow>Average(list_10, ApplicationWebService.CS___9__CachedAnonymousMethodDelegate7)));
            //                                            ^
            //    method __Enumerable.Average(__IEnumerable_1<Integer>) is not applicable
            //      (actual and formal argument lists differ in length)
            //    method __Enumerable.<TSource>Average(__IEnumerable_1<TSource>,__Func_2<TSource,Integer>) is not applicable
            //      (actual argument __Func_2<PerformanceResourceTimingData2ApplicationResourcePerformanceRow,Long> cannot be converted to __Func_2<PerformanceResourceTimingData2ApplicationResourcePerformanceRow,Integer> by method invocation conversion)
            //  where TSource is a type-variable:
            //    TSource extends Object declared in method <TSource>Average(__IEnumerable_1<TSource>,__Func_2<TSource,Integer>)

            //Y:\SVGNavigationTiming.ApplicationWebService\staging.java\web\java\ScriptCoreLib\Shared\BCLImplementation\System\Linq\__Enumerable.java:1720: error: inconvertible types
            //                num2 = ((int)(enumerator_15.System_Collections_Generic_IEnumerator_1_get_Current()));
            //                             ^
            //  required: int
            //  found:    Double
            //Y:\SVGNavigationTiming.ApplicationWebService\staging.java\web\java\ScriptCoreLib\Shared\BCLImplementation\System\Linq\__Enumerable.java:1769: error: inconvertible types
            //                num2 = ((int)(enumerator_15.System_Collections_Generic_IEnumerator_1_get_Current()));
            //                             ^
            //  required: int
            //  found:    Long
            //Y:\SVGNavigationTiming.ApplicationWebService\staging.java\web\java\SVGNavigationTiming\ApplicationWebService.java:114: error: method Average_0600046d in class __Enumerable cannot be applied to given types;
            //        row1.duration = ((long)(__Enumerable.<PerformanceResourceTimingData2ApplicationResourcePerformanceRow>Average_0600046d(list_10, ApplicationWebService.CS___9__CachedAnonymousMethodDelegate7)));
            //                                            ^

            data.AddRange(
                new[] {
                    new Design.PerformanceResourceTimingData2ApplicationResourcePerformanceRow
                    {
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

             .AsResult();
        }

        public Task<DataTable> GetApplicationResourcePerformance(Design.PerformanceResourceTimingData2ApplicationPerformanceKey k)
        {
            //Task.FromResult
            return new Design.PerformanceResourceTimingData2.ApplicationResourcePerformance()
                .SelectAllAsEnumerable()

                .Where(z => z.ApplicationPerformance == k)

                .AsDataTable()

                .AsResult();
        }


    }


    #endregion

    #region IUpstreamApplicationPerformance
    public partial class ApplicationWebService : IUpstreamApplicationPerformance
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

            return new Design.PerformanceResourceTimingData2.ApplicationResourcePerformance().Insert(value).AsResult();
        }
    }



    #endregion





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
        Task<DataTable> GetApplicationResourcePerformance(PerformanceResourceTimingData2ApplicationPerformanceKey k);
    }
}