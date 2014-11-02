using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SQLite;
using System.Diagnostics;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

// rename namespace?
namespace ScriptCoreLib.Shared.Data.Diagnostics
{
    [Obsolete("part of string conversions?")]
    public static class WithConnectionLambdaZ
    {
        public static double GetDoubleOrDefault(System.Data.DataRow e, string ColumnName)
        {
            //Console.WriteLine("GetInt64OrDefault " + new { ColumnName });

            // X:\jsc.svn\examples\javascript\p2p\SharedBrowserSessionExperiment\SharedBrowserSessionExperiment\TheBrowserTab.cs

            if (e.Table.Columns.Contains(ColumnName))
            {
                // e[ColumnName] ""

                // X:\jsc.svn\examples\java\Test\TestJVMCLRAsString\TestJVMCLRAsString\Program.cs

                var __value = e[ColumnName];

                //Console.WriteLine("GetInt64OrDefault " + new { __value });

                var __value_asString = __value as string;

                //Console.WriteLine("GetInt64OrDefault " + new { __value_asString });

                // new row added by the grid?
                if (!string.IsNullOrEmpty(__value_asString))
                {
                    var __value_asDouble = Convert.ToDouble(__value_asString);

                    //Console.WriteLine("GetInt64OrDefault " + new { __value_asInt64 });

                    return __value_asDouble;
                }
            }

            return default(double);
        }



        public static long GetInt64OrDefault(System.Data.DataRow e, string ColumnName)
        {
            //Console.WriteLine("GetInt64OrDefault " + new { ColumnName });

            // X:\jsc.svn\examples\javascript\p2p\SharedBrowserSessionExperiment\SharedBrowserSessionExperiment\TheBrowserTab.cs

            if (e.Table.Columns.Contains(ColumnName))
            {
                // e[ColumnName] ""

                // X:\jsc.svn\examples\java\Test\TestJVMCLRAsString\TestJVMCLRAsString\Program.cs

                var __value = e[ColumnName];

                //Console.WriteLine("GetInt64OrDefault " + new { __value });

                var __value_asString = __value as string;

                //Console.WriteLine("GetInt64OrDefault " + new { __value_asString });

                // new row added by the grid?
                if (!string.IsNullOrEmpty(__value_asString))
                {
                    var __value_asInt64 = Convert.ToInt64(__value_asString);

                    //Console.WriteLine("GetInt64OrDefault " + new { __value_asInt64 });

                    return __value_asInt64;
                }
            }

            return default(long);
        }

        public static object ConvertDBNullToNullIfAny(object e)
        {
            // https://sites.google.com/a/jsc-solutions.net/backlog/knowledge-base/2013/201312/20131208-dbnull

            if (e is DBNull)
                return null;

            return e;
        }
    }

    public static class WithConnectionLambda
    {
        // http://stackoverflow.com/questions/2427381/how-to-detect-that-c-sharp-windows-forms-code-is-executed-within-visual-studio
        // X:\jsc.svn\examples\javascript\p2p\SharedBrowserSessionExperiment\SharedBrowserSessionExperiment\ApplicationWebService.cs

        [Obsolete("Windows Forms designer?")]
        static bool DesignMode
        {
            get
            {
                return System.ComponentModel.LicenseManager.UsageMode == System.ComponentModel.LicenseUsageMode.Designtime;
            }
        }

        // https://sites.google.com/a/jsc-solutions.net/backlog/knowledge-base/2014/201403/20140322
        // called by assets compiler
        public static Func<Func<IDbConnection, Task>, Task> WithConnection(string DataSource)
        {
            if (DesignMode)
                return delegate { return default(Task); };

            // X:\jsc.svn\examples\javascript\LINQ\test\TestSelectGroupByAndConstant\TestSelectGroupByAndConstant\ApplicationWebService.cs
            if (VirtualWithConnection != null)
                return VirtualWithConnection(DataSource);


            return InternalWithConnectionLambda.WithConnection(DataSource);
        }

        // ThreadLocal ?
        public static Func<string, Func<Func<IDbConnection, Task>, Task>> VirtualWithConnection;
    }

    static class InternalWithConnectionLambda
    {
        // X:\jsc.svn\examples\javascript\LINQ\test\TestSelectGroupByAndConstant\TestSelectGroupByAndConstant\ApplicationWebService.cs

        public static Func<Func<IDbConnection, Task>, Task> WithConnection(string DataSource)
        {
            // https://sites.google.com/a/jsc-solutions.net/backlog/knowledge-base/2014/201411/20141102
//#if FSQLiteConnectionStringBuilder
            // ScriptCoreLib.Extensions
            var csb = new SQLiteConnectionStringBuilder
            {
                DataSource = DataSource,

                // is there any other version?
                Version = 3
            };


            var yy = csb.AsWithConnection(Initializer: null);

            return y =>
            {
                var ret = default(Task);

                yy(
                    c =>
                    {
                        ret = y(c);
                    }
                );

                return ret;
            };
//#endif
//            throw new NotImplementedException();
        }

    }



}
