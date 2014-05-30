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
        // X:\jsc.svn\examples\javascript\LINQ\test\TestOrderBy\TestOrderBy\ApplicationWebService.cs

        public static void MutableOrderBy(IQueryStrategy that, Expression selector, bool desc = false)
        {


            Console.WriteLine("MutableOrderBy " + new { selector });

            that.GetCommandBuilder().Add(
             state =>
             {
                 //MutableWhere { Method = Boolean op_Equality(System.String, System.String), Left = Goo, Right = Goo0 }




                 #region ColumnName
                 var ColumnName = "";

                 // +		Member	{System.String path}	System.Reflection.MemberInfo {System.Reflection.RtFieldInfo}
                 var body = ((LambdaExpression)selector).Body;

                 // unpack the convert?
                 var body_as_UnaryExpression = body as UnaryExpression;
                 var body_as_MemberExpression = body as MemberExpression;
                 if (body_as_UnaryExpression != null)
                 {
                     ColumnName = ((MemberExpression)(body_as_UnaryExpression).Operand).Member.Name;
                 }
                 else if (body_as_MemberExpression != null)
                 {
                     ColumnName = body_as_MemberExpression.Member.Name;

                     if (ColumnName == "Key")
                     {
                         // cant check it like that
                         //var source_groupby = body_as_MemberExpression.Member.DeclaringType is IQueryStrategyGrouping;

                         // X:\jsc.svn\examples\javascript\LINQ\test\TestGroupByThenOrderByThenOrderBy\TestGroupByThenOrderByThenOrderBy\ApplicationWebService.cs

                         var source_groupby = that as QueryStrategyOfTRowExtensions.IGroupByQueryStrategy;
                         if (source_groupby != null)
                         {
                             // we are using a special name for that!
                             // X:\jsc.svn\core\ScriptCoreLib.Extensions\ScriptCoreLib.Extensions\Query\QueryStrategyOfTRowExtensions.Select.cs

                             ColumnName = "Grouping.Key";
                         }
                     }
                 }
                 else Debugger.Break();
                 #endregion



                 if (string.IsNullOrEmpty(state.OrderByCommand))
                 {
                     if (desc)
                         state.OrderByCommand = "order by `" + ColumnName + "` desc";
                     else
                         state.OrderByCommand = "order by `" + ColumnName + "`";
                 }
                 else
                 {
                     if (desc)
                         state.OrderByCommand += ", `" + ColumnName + "` desc";
                     else
                         state.OrderByCommand += ", `" + ColumnName + "`";
                 }
             }
            );
        }


        // Error	273	Could not find an implementation of the query pattern for source type 'ScriptCoreLib.Shared.Data.Diagnostics.IQueryStrategy<<anonymous type: long duration, long count, string path>>'. 
        // 'ThenBy' not found.	X:\jsc.svn\examples\javascript\LINQ\test\TestGroupByThenOrderByThenOrderBy\TestGroupByThenOrderByThenOrderBy\ApplicationWebService.cs	55	31	TestGroupByThenOrderByThenOrderBy

        //[Obsolete("mutable")]
        public static IQueryStrategy<TElement> ThenBy<TElement, TKey>(this IQueryStrategy<TElement> source, Expression<Func<TElement, TKey>> keySelector)
        {
            MutableOrderBy(
                source, keySelector
            );

            return source;
        }

        //[Obsolete("mutable")]
        public static IQueryStrategy<TElement> OrderBy<TElement, TKey>(this IQueryStrategy<TElement> source, Expression<Func<TElement, TKey>> keySelector)
        {
            MutableOrderBy(
                source, keySelector
            );

            return source;
        }

        //[Obsolete("mutable")]
        public static IQueryStrategy<TElement> OrderByDescending<TElement, TKey>(this IQueryStrategy<TElement> source, Expression<Func<TElement, TKey>> keySelector)
        {
            MutableOrderBy(
               source, keySelector, desc: true
           );

            return source;
        }

        public static IQueryStrategy<TElement> ThenByDescending<TElement, TKey>(this IQueryStrategy<TElement> source, Expression<Func<TElement, TKey>> keySelector)
        {
            MutableOrderBy(
               source, keySelector, desc: true
           );

            return source;
        }
    }
}

