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

    //[Obsolete("?", true)]

    public static partial class QueryStrategyOfTRowExtensions
    {
        public static long Count<TElement>(this IQueryStrategy<TElement> source, Expression<Func<TElement, bool>> filter)
        {
            return source.Where(filter).Count();

        }

        static MethodInfo refCount = new Func<IQueryStrategy<object>, long>(QueryStrategyOfTRowExtensions.Count).Method;

        #region select count
        // X:\jsc.svn\examples\javascript\LINQ\MashableVelocityGraph\MashableVelocityGraph\ApplicationWebService.cs
        //[Obsolete("this is the first method we made generic. would it work for a group now?")]
        //public static long Count<TElement>(this IQueryStrategy<TElement> Strategy)
        public static long Count(this IQueryStrategy Strategy)
        {
            return ((Task<long>)Strategy.GetDescriptor().GetWithConnection()(
                c =>
                {
                    var state = QueryStrategyExtensions.AsCommandBuilder(Strategy);

                    // override
                    state.SelectCommand = "select count(*)";

                    var cmd = c.CreateCommand(state.ToString());

                    foreach (var item in state.ApplyParameter)
                    {
                        item(cmd);
                    }

                    var s = new TaskCompletionSource<long>();
                    s.SetResult((long)cmd.ExecuteScalar());

                    return s.Task;
                }
            )).Result;
        }
        #endregion


    }
}

