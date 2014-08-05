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


        static MethodInfo refLast = new Func<IQueryStrategyGrouping<long, object>, object>(QueryStrategyOfTRowExtensions.Last).Method;




        // http://stackoverflow.com/questions/3179021/sha1-hashing-in-sqlite-how
        // while SQLite might allow iproc user funtions, appengine mysql likely will not.

        //[Obsolete("non grouping methods shall use FirstOrDefault")]

        // MYSQL and SQLITE seem to behave differently? in reverse actually!
        public static TElement Last<TKey, TElement>(this IQueryStrategyGrouping<TKey, TElement> source)
        {
            // http://stackoverflow.com/questions/5140785/mysql-order-before-group-by
            // http://www.tocker.ca/2013/10/21/heads-up-implicit-sorting-by-group-by-is-deprecated-in-mysql-5-6.html

            // reverse and take 1 ?
            // can we reverse yet? reorder by Key desc?
            // then we could also do ElementAt
            // but. would we be able to to reverse on nested orderbys?
            // would have to flip all selectors
            // in a way the group by already implements reverse ordering?

            throw new NotImplementedException();
        }
    }
}

