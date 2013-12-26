#define SQLite

using ScriptCoreLib;
using ScriptCoreLib.Delegates;
using ScriptCoreLib.Extensions;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace SVGNavigationTiming
{
    /// <summary>
    /// Methods defined in this type can be used from JavaScript. The method calls will seamlessly be proxied to the server.
    /// </summary>
    public class ApplicationWebService
    {
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

        public Task<DataTable> GetApplicationPerformance()
        {
            //Task.FromResult
            return new Design.PerformanceResourceTimingData2.ApplicationPerformance().SelectAllAsDataTable().AsResult();
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

        public Design.PerformanceResourceTimingData2ApplicationPerformanceKey CurrentApplicationPerformance;


        public const long TicksPerMillisecond = 10000;
        public const long ticks_1970_1_1 = 621355968000000000;

        public Task AtApplicationPerformance(Design.PerformanceResourceTimingData2ApplicationPerformanceRow value)
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

            CurrentApplicationPerformance = new Design.PerformanceResourceTimingData2.ApplicationPerformance().Insert(value);

            return "ok".AsResult();
        }

        public Task AtApplicationResourcePerformance(Design.PerformanceResourceTimingData2ApplicationResourcePerformanceRow value)
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
            value.ApplicationPerformance = this.CurrentApplicationPerformance;

            new Design.PerformanceResourceTimingData2.ApplicationResourcePerformance().Insert(value);

            return "ok".AsResult();
        }
    }

    public static class X
    {
        public static DataTable AsDataTable(this IEnumerable<Design.PerformanceResourceTimingData2ApplicationResourcePerformanceRow> source)
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
            x.Columns.Add("Timestamp");

            // The runtime has encountered a fatal error. The address of the error was at 0x715f4ba0, on thread 0x2290. The error code is 0xc0000005. This error may be a bug in the CLR or in the unsafe or non-verifiable portions of user code. 
            // Common sources of this bug include user marshaling errors for COM-interop or PInvoke, which may corrupt the stack.


            foreach (var item in source)
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
                n["Timestamp"] = item.Timestamp;

                x.Rows.Add(n);
            }

            return x;
        }
    }
}
