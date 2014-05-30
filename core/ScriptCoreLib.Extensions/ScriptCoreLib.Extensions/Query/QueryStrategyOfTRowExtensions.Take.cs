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

namespace System.Data
{
    // move to a nuget?
    // shall reimplement IQueriable for jsc data layer gen
    //[Obsolete("the first generic extension method for all jsc data layer rows")]
    public static partial class QueryStrategyOfTRowExtensions
    {


        #region take
        [Obsolete("caller has the option to clone the state before calling this function. should jsc add static expression pooling/caching like c# compiler does for lambdas?")]
        public static void MutableTake(IQueryStrategy that, long count)
        {
            // should the caller take care of cloning the instance?
            // should we start using Trace for logging?
            Console.WriteLine("MutableTake " + new { count });

            that.GetCommandBuilder().Add(
             state =>
             {
                 //MutableWhere { Method = Boolean op_Equality(System.String, System.String), Left = Goo, Right = Goo0 }

                 var n = "@arg" + state.ApplyParameter.Count;

                 // https://sites.google.com/a/jsc-solutions.net/backlog/knowledge-base/2014/201401/20140119
                 // limit 0?
                 state.LimitCommand = "limit " + n;

                 Console.WriteLine("MutableTake " + new { n, count });


                 state.ApplyParameter.Add(
                     c =>
                     {
                         // either the actualt command or the explain command?

                         //c.Parameters.AddWithValue(n, count);
                         c.AddParameter(n, count);
                     }
                 );
             }
            );
        }
        #endregion


        [Obsolete("mutable")]
        public static IQueryStrategy<TElement> Take<TElement>(this IQueryStrategy<TElement> source, long count)
        {
            MutableTake(
                source, count
            );

            return source;
        }


    }
}

