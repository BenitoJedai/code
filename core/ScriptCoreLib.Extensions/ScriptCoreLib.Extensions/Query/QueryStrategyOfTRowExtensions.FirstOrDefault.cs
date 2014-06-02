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
using System.Threading;

namespace System.Data
{
    // move to a nuget?
    // shall reimplement IQueriable for jsc data layer gen
    //[Obsolete("the first generic extension method for all jsc data layer rows")]
    public static partial class QueryStrategyOfTRowExtensions
    {


        [Obsolete("non grouping methods shall use FirstOrDefault")]
        public static TElement First<TKey, TElement>(this IQueryStrategyGrouping<TKey, TElement> source)
        {
            // first to be used in groups

            throw new NotImplementedException();
        }


        static MethodInfo refFirstOrDefault = new Func<IQueryStrategy<object>, object>(QueryStrategyOfTRowExtensions.FirstOrDefault).Method;


        //[Obsolete("experimental")]
        public static TElement FirstOrDefault<TElement>(this IQueryStrategy<TElement> source)
        {
            // will it work for jvm?
            var t = new StackTrace(0, true);
            var s = Stopwatch.StartNew();

            Console.WriteLine();

            // X:\jsc.svn\examples\javascript\linq\test\TestSelectAndSubSelect\TestSelectAndSubSelect\ApplicationWebService.cs
            // X:\jsc.svn\examples\javascript\LINQ\test\TestSelectFirstOrDefault\TestSelectFirstOrDefault\ApplicationWebService.cs
            // X:\jsc.svn\examples\javascript\LINQ\test\TestSelectMember\TestSelectMember\ApplicationWebService.cs



            var value = source.Take(1).AsGenericEnumerable().FirstOrDefault();

            var caller = t.GetFrame(1);
            Console.WriteLine(
                Process.GetCurrentProcess().Id.ToString("x4") + ":"
                + Thread.CurrentThread.ManagedThreadId.ToString("x4")
                + " FirstOrDefault " + new { s.ElapsedMilliseconds, Debugger.IsAttached, caller = caller.ToString() });

            return value;
        }
    }
}

