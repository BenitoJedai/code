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

                 var body = ((LambdaExpression)selector).Body;




                 if (body is BinaryExpression || body is MethodCallExpression)
                 {
                     var s = state.OrderByCommand;

                     if (string.IsNullOrEmpty(state.OrderByCommand))
                     {
                         s = "order by ";
                         state.WriteExpression(ref s, body, that);

                         if (desc)
                         {
                             s += " desc";
                         }
                     }
                     else
                     {
                         s += ", ";
                         state.WriteExpression(ref s, body, that);

                         if (desc)
                         {
                             s += " desc";
                         }

                     }

                     state.OrderByCommand = s;

                     return;
                 }




                 // unpack the convert?
                 var body_as_UnaryExpression = body as UnaryExpression;
                 var body_as_MemberExpression = body as MemberExpression;


                 #region ColumnName
                 var ColumnName = "";

                 // +		Member	{System.String path}	System.Reflection.MemberInfo {System.Reflection.RtFieldInfo}

                 if (body_as_UnaryExpression != null)
                 {
                     ColumnName = ((MemberExpression)(body_as_UnaryExpression).Operand).Member.Name;
                 }
                 else if (body_as_MemberExpression != null)
                 {
                     ColumnName = body_as_MemberExpression.Member.Name;

                     // that = {System.Data.QueryStrategyOfTRowExtensions.JoinQueryStrategy<xmonese.core.Data.InternalDataSettingsRow,xmonese.core.Data.InternalDataEmployeesDataRow,xmonese.core.Data.InternalDataEmployeesKey,<>f__AnonymousType0<xmonese.core.Data.InternalDataSettingsRow,xm...
                     var xIJoinQueryStrategy = that as QueryStrategyOfTRowExtensions.IJoinQueryStrategy;
                     if (xIJoinQueryStrategy != null)
                     {
                         // cant we just use writeExpression just yet?

                         var xMemberExpression = body_as_MemberExpression.Expression as MemberExpression;
                         if (xMemberExpression != null)
                         {
                             ColumnName = xMemberExpression.Member.Name + "_" + body_as_MemberExpression.Member.Name;
                         }
                     }
                     else
                     {


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

                                 // no longer true.
                                 ColumnName = "Grouping.Key";
                                 Debugger.Break();
                             }
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

        static MethodInfo refOrderByDescending = new Func<IQueryStrategy<object>, Expression<Func<object, bool>>, IQueryStrategy<object>>(QueryStrategyOfTRowExtensions.OrderByDescending).Method;

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

