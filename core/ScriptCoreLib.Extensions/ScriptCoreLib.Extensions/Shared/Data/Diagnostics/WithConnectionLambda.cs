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

namespace ScriptCoreLib.Shared.Data.Diagnostics
{
    [Obsolete("part of string conversions?")]
    public static class WithConnectionLambdaZ
    {
        public static long GetInt64OrDefault(System.Data.DataRow e, string ColumnName)
        {
            // X:\jsc.svn\examples\javascript\p2p\SharedBrowserSessionExperiment\SharedBrowserSessionExperiment\TheBrowserTab.cs

            if (e.Table.Columns.Contains(ColumnName))
            {
                // e[ColumnName] ""

                var v = e[ColumnName] as string;

                // new row added by the grid?
                if (!string.IsNullOrEmpty(v))
                    return Convert.ToInt64(v);
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
        public static Func<Func<IDbConnection, Task>, Task> WithConnection(string DataSource)
        {
            if (DesignMode)
                return delegate { return default(Task); };

            return InternalWithConnectionLambda.WithConnection(DataSource);
        }
    }

    static class InternalWithConnectionLambda
    {

        public static Func<Func<IDbConnection, Task>, Task> WithConnection(string DataSource)
        {
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
        }
    }



}
