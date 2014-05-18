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
        [ScriptCoreLib.ScriptAttribute.ExplicitInterface]
        interface ISelectQueryStrategy
        {
            // allow to inspect upper select . what if there are multiple upper selects?
            Expression selectorExpression { get; }

            IQueryStrategy source { get; }

            // ? gDescendingByKeyReferenced
        }

        class SelectQueryStrategy<TSource, TResult> : XQueryStrategy<TResult>, ISelectQueryStrategy
        {
            public IQueryStrategy<TSource> source;
            public Expression<Func<TSource, TResult>> selector;



            #region ISelectQueryStrategy
            Expression ISelectQueryStrategy.selectorExpression
            {
                get { return selector; }
            }

            IQueryStrategy ISelectQueryStrategy.source
            {
                get { return source; }
            }
            #endregion

        }

        public static IQueryStrategy<TResult>
            Select
            <TSource, TResult>
            (
             this IQueryStrategy<TSource> source,
             Expression<Func<TSource, TResult>> selector)
        {
            // https://sites.google.com/a/jsc-solutions.net/backlog/knowledge-base/2014/201405/20140513

            //GroupBy
            //Select
            //Select
            //Select
            //AsDataTable



            // keySelector = {g => new <>f__AnonymousType0`2(g = g, last = g.Last())}
            // keySelector = {<>h__TransparentIdentifier0 => new <>f__AnonymousType1`2(<>h__TransparentIdentifier0 = <>h__TransparentIdentifier0, x = "???")}
            // keySelector = {<>h__TransparentIdentifier1 => new SchemaViewsMiddleViewRow() {
            // Content = <>h__TransparentIdentifier1.<>h__TransparentIdentifier0.last.UpdatedContent, 
            // Tag = <>h__TransparentIdentifier1.x, 
            // LatestLeftContent = <>h__TransparentIdentifier1.x, LatestRightContent...

            Console.WriteLine("Select " + new { selector });

            var that = new SelectQueryStrategy<TSource, TResult>
            {
                // save for inspection
                source = source,
                selector = selector,


                InternalGetDescriptor =
                    () =>
                    {
                        // inherit the connection/context from above
                        var StrategyDescriptor = source.GetDescriptor();

                        return StrategyDescriptor;
                    }
            };


            that.GetCommandBuilder().Add(
                 state =>
                 {
                     Console.WriteLine("Select CommandBuilder");

                     //select `Key`, `MiddleSheet`, `UpdatedContent`, `Tag`, `Timestamp`
                     //from `Schema.MiddleSheetUpdates`


                     // X:\jsc.svn\examples\javascript\forms\test\TestNestedSQLiteGrouping\TestNestedSQLiteGrouping\ApplicationWebService.cs
                     // +		Body	{new <>f__AnonymousType0`2(path = k.path, Length = k.path.Length)}	System.Linq.Expressions.Expression {System.Linq.Expressions.NewExpression}

                     var asMemberInitExpression = ((LambdaExpression)that.selector).Body as MemberInitExpression;

                     (that.source as IGroupByQueryStrategy).With(q => q.upperSelect = that);

                     var s = QueryStrategyExtensions.AsCommandBuilder(that.source);


                     // X:\jsc.svn\examples\javascript\LINQ\MinMaxAverageExperiment\MinMaxAverageExperiment\ApplicationWebService.cs


                     // ???
                     var gDescendingByKeyReferenced = false;
                     var GroupingKeyFieldExpressionName = "?";

                     //var asMemberInitExpression = default(MemberInitExpression);
                     var asMemberInitExpressionByParameter0 = default(ParameterExpression);
                     var asMemberInitExpressionByParameter1 = default(ParameterExpression);
                     var asMemberInitExpressionByParameter2 = default(ParameterExpression);


                     var s_SelectCommand = "select 0 as foo";


                     #region WriteMemberExpression COPY from GroupBy
                     Action<int, MemberExpression, MemberInfo> WriteMemberExpression =
                         (index, asMemberExpression, asMemberAssignment_Member) =>
                         {
                             var asMemberAssignment = new { Member = asMemberAssignment_Member };

                             // +		Member	{TestSQLiteGroupBy.Data.GooStateEnum Key}	System.Reflection.MemberInfo {System.Reflection.RuntimePropertyInfo}

                             // X:\jsc.svn\core\ScriptCoreLib\Shared\BCLImplementation\System\Linq\Expressions\Expression.cs


                             //{ index = 7, asMemberAssignment = MemberAssignment { Expression = MemberExpression { expression = MethodCallExpression { Object = , Method = java.lang.Object Last(TestSQLiteGroupBy.IQueryStrategyGrouping_2) }, field = double x }
                             //{ index = 7, asMemberExpression = MemberExpression { expression = MethodCallExpression { Object = , Method = java.lang.Object Last(TestSQLiteGroupBy.IQueryStrategyGrouping_2) }, field = double x } }
                             //{ index = 7, Member = double x, Name = x }
                             //{ index = 7, asMemberExpressionMethodCallExpression = MethodCallExpression { Object = , Method = java.lang.Object Last(TestSQLiteGroupBy.IQueryStrategyGrouping_2) } }


                             Console.WriteLine(new { index, asMemberExpression.Member, asMemberExpression.Member.Name });

                             ////#region let z <- Grouping.Key
                             ////var IsKey = asMemberExpression.Member.Name == "Key";

                             ////// if not a property we may still have the getter in JVM
                             ////IsKey |= asMemberExpression.Member.Name == "get_Key";

                             ////if (IsKey)
                             ////{
                             ////    // special!
                             ////    state.SelectCommand += ",\n\t g.`" + asMemberAssignment.Member.Name + "` as `" + asMemberAssignment.Member.Name + "`";

                             ////    s_SelectCommand += ",\n\t s.`"
                             ////       + GroupingKeyFieldExpressionName + "` as `" + asMemberAssignment.Member.Name + "`";
                             ////    return;
                             ////}
                             ////#endregion

                             // Method = {TestSQLiteGroupBy.Data.Book1MiddleRow First[GooStateEnum,Book1MiddleRow](TestSQLiteGroupBy.IQueryStrategyGrouping`2[TestSQLiteGroupBy.Data.GooStateEnum,TestSQLiteGroupBy.Data.Book1MiddleRow])}

                             #region asMemberExpressionMethodCallExpression
                             var asMemberExpressionMethodCallExpression = asMemberExpression.Expression as MethodCallExpression;
                             Console.WriteLine(new { index, asMemberExpressionMethodCallExpression });
                             if (asMemberExpressionMethodCallExpression != null)
                             {
                                 if (asMemberInitExpressionByParameter1 != null)
                                 {

                                     // ?
                                 }
                                 else if (asMemberInitExpressionByParameter0 != null)
                                 {
                                     if (asMemberInitExpressionByParameter0 != asMemberExpressionMethodCallExpression.Arguments[0])
                                     {
                                         // group by within a join, where this select is not part of this outer source!

                                         return;
                                     }
                                 }
                                 Console.WriteLine(new { index, asMemberExpressionMethodCallExpression, asMemberExpressionMethodCallExpression.Method.Name });

                                 // special!
                                 if (asMemberExpressionMethodCallExpression.Method.Name.TakeUntilIfAny("_") == "First")
                                 {
                                     gDescendingByKeyReferenced = true;
                                     state.SelectCommand += ",\n\t gDescendingByKey.`" + asMemberAssignment.Member.Name + "` as `" + asMemberAssignment.Member.Name + "`";
                                     s_SelectCommand += ",\n\t s.`" + asMemberExpression.Member.Name + "` as `" + asMemberAssignment.Member.Name + "`";
                                     return;
                                 }

                                 if (asMemberExpressionMethodCallExpression.Method.Name.TakeUntilIfAny("_") == "Last")
                                 {
                                     if (asMemberInitExpressionByParameter0 != null)
                                     {
                                         // https://sites.google.com/a/jsc-solutions.net/backlog/knowledge-base/2014/201405/20140513

                                         // the upper join dictates what it expects to find. no need to alias too early

                                         state.SelectCommand += ",\n\t g.`" + asMemberExpression.Member.Name + "` as `" + asMemberExpression.Member.Name + "`";
                                         s_SelectCommand += ",\n\t s.`" + asMemberExpression.Member.Name + "` as `" + asMemberExpression.Member.Name + "`";
                                         return;
                                     }

                                     state.SelectCommand += ",\n\t g.`" + asMemberAssignment.Member.Name + "` as `" + asMemberAssignment.Member.Name + "`";
                                     s_SelectCommand += ",\n\t s.`" + asMemberExpression.Member.Name + "` as `" + asMemberAssignment.Member.Name + "`";
                                     return;
                                 }
                             }
                             #endregion


                             //                                                 -		asMemberAssignment.Expression	{value(SQLiteWithDataGridViewX.ApplicationWebService+<>c__DisplayClass1b).SpecialConstant.u}	System.Linq.Expressions.Expression {System.Linq.Expressions.PropertyExpression}


                             //                                 +		(new System.Linq.Expressions.Expression.MemberExpressionProxy(asMemberAssignment.Expression as System.Linq.Expressions.PropertyExpression)).Member	{System.String u}	System.Reflection.MemberInfo {System.Reflection.RuntimePropertyInfo}
                             //+		(new System.Linq.Expressions.Expression.ConstantExpressionProxy((new System.Linq.Expressions.Expression.MemberExpressionProxy((new System.Linq.Expressions.Expression.MemberExpressionProxy(asMemberAssignment.Expression as System.Linq.Expressions.PropertyExpression)).Expression as System.Linq.Expressions.FieldExpression)).Expression as System.Linq.Expressions.ConstantExpression)).Value	{SQLiteWithDataGridViewX.ApplicationWebService.}	object {SQLiteWithDataGridViewX.ApplicationWebService.}

                             //                                 -		Value	{SQLiteWithDataGridViewX.ApplicationWebService.}	object {SQLiteWithDataGridViewX.ApplicationWebService.}
                             //-		SpecialConstant	{ u = "44" }	<Anonymous Type>
                             //        u	"44"	string




                             #region asMConstantExpression
                             //         var SpecialConstant_u = "44";
                             var asMConstantExpression = asMemberExpression.Expression as ConstantExpression;
                             if (asMConstantExpression != null)
                             {
                                 var asMPropertyInfo = asMemberExpression.Member as FieldInfo;
                                 var rAddParameterValue0 = asMPropertyInfo.GetValue(asMConstantExpression.Value);

                                 // X:\jsc.svn\examples\javascript\forms\Test\TestSQLGroupByAfterJoin\TestSQLGroupByAfterJoin\ApplicationWebService.cs

                                 var n = "@arg" + state.ApplyParameter.Count;

                                 state.SelectCommand += ",\n\t g.`" + asMemberAssignment.Member.Name + "` as `" + asMemberAssignment.Member.Name + "`";
                                 s_SelectCommand += ",\n\t " + n + " as `" + asMemberAssignment.Member.Name + "`";

                                 state.ApplyParameter.Add(
                                     c =>
                                     {
                                         // either the actualt command or the explain command?

                                         //c.Parameters.AddWithValue(n, r);
                                         c.AddParameter(n, rAddParameterValue0);
                                     }
                                 );

                                 return;

                                 //if (rAddParameterValue0 is string)
                                 //{
                                 //    // the outer select might be optimized away!
                                 //    state.SelectCommand += ",\n\t g.`" + asMemberAssignment.Member.Name + "` as `" + asMemberAssignment.Member.Name + "`";
                                 //    s_SelectCommand += ",\n\t '" + rAddParameterValue0 + "' as `" + asMemberAssignment.Member.Name + "`";
                                 //}
                                 //else
                                 //{
                                 //    // long?
                                 //    state.SelectCommand += ",\n\t g.`" + asMemberAssignment.Member.Name + "` as `" + asMemberAssignment.Member.Name + "`";
                                 //    s_SelectCommand += ",\n\t " + rAddParameterValue0 + " as `" + asMemberAssignment.Member.Name + "`";
                                 //}

                                 //return;
                             }
                             #endregion



                             #region asMMemberExpression
                             var asMMemberExpression = asMemberExpression.Expression as MemberExpression;
                             if (asMMemberExpression != null)
                             {
                                 // Member = {<>f__AnonymousType0`1[System.String] SpecialConstant}
                                 // X:\jsc.svn\examples\javascript\forms\SQLiteWithDataGridViewX\SQLiteWithDataGridViewX\ApplicationWebService.cs
                                 // var SpecialConstant = new { u = "44" };


                                 if (asMemberInitExpressionByParameter1 != null)
                                 {
                                     // ???
                                     // +		(new System.Linq.Expressions.Expression.MemberExpressionProxy(asMemberExpression as System.Linq.Expressions.FieldExpression)).Expression	
                                     // {<>h__TransparentIdentifier0.MiddleSheetz}	System.Linq.Expressions.Expression {System.Linq.Expressions.PropertyExpression}

                                     // https://sites.google.com/a/jsc-solutions.net/backlog/knowledge-base/2014/201405/20140513
                                     //Debugger.Break();
                                     return;

                                     //var asFieldInfo = asMemberExpression.Member as FieldInfo;
                                     //if (asFieldInfo != null)
                                     //{
                                     //    //asMemberExpressionMethodCallExpression = {<>h__TransparentIdentifier0.UpdatesByMiddlesheet.Last()}

                                     //    state.SelectCommand += ",\n\t g.`" + asFieldInfo.Name + "` as `" + asFieldInfo.Name + "`";
                                     //    s_SelectCommand += ",\n\t s.`" + asFieldInfo.Name + "` as `" + asFieldInfo.Name + "`";
                                     //    return;
                                     //}
                                 }

                                 var asMMFieldInfo = asMMemberExpression.Member as FieldInfo;

                                 #region asPropertyInfo
                                 var asPropertyInfo = asMemberExpression.Member as PropertyInfo;
                                 if (asPropertyInfo != null)
                                 {


                                     if (asPropertyInfo.Name == "Length")
                                     {
                                         // http://www.sqlite.org/lang_corefunc.html

                                         state.SelectCommand += ",\n\t g.`" + asMemberAssignment.Member.Name + "` as `" + asMemberAssignment.Member.Name + "`";
                                         //s_SelectCommand += ",\n\t len(s.`" + asMMFieldInfo.Name + "`) as `" + asMemberAssignment.Member.Name + "`";
                                         s_SelectCommand += ",\n\t length(s.`" + asMMFieldInfo.Name + "`) as `" + asMemberAssignment.Member.Name + "`";

                                         return;
                                     }


                                     // CLR

                                     var asC = asMMemberExpression.Expression as ConstantExpression;

                                     // Member = {<>f__AnonymousType0`1[System.String] SpecialConstant}

                                     var value0 = asMMFieldInfo.GetValue(asC.Value);
                                     var rAddParameterValue0 = asPropertyInfo.GetValue(value0, null);



                                     var n = "@arg" + state.ApplyParameter.Count;
                                     state.SelectCommand += ",\n\t g.`" + asMemberAssignment.Member.Name + "` as `" + asMemberAssignment.Member.Name + "`";
                                     s_SelectCommand += ",\n\t " + n + " as `" + asMemberAssignment.Member.Name + "`";

                                     state.ApplyParameter.Add(
                                         c =>
                                         {
                                             // either the actualt command or the explain command?

                                             //c.Parameters.AddWithValue(n, r);
                                             c.AddParameter(n, rAddParameterValue0);
                                         }
                                     );

                                     //if (rAddParameterValue0 is string)
                                     //{
                                     //    // NULL?
                                     //    state.SelectCommand += ",\n\t '" + rAddParameterValue0 + "' as `" + asMemberAssignment.Member.Name + "`";
                                     //}
                                     //else
                                     //{
                                     //    // long?
                                     //    state.SelectCommand += ",\n\t " + rAddParameterValue0 + " as `" + asMemberAssignment.Member.Name + "`";
                                     //}

                                     return;
                                 }
                                 #endregion



                                 // https://sites.google.com/a/jsc-solutions.net/backlog/knowledge-base/2014/201405/20140515
                                 // X:\jsc.svn\examples\javascript\forms\Test\TestSQLGroupByAfterJoin\TestSQLGroupByAfterJoin\ApplicationWebService.cs
                                 var asMMMemberInfo = asMMemberExpression.Member as MemberInfo;
                                 if (asMMMemberInfo != null)
                                 {
                                     // asMMemberExpression = {result.Last().l}
                                     // asMemberExpression = {result.Last().l.FirstName}

                                     var asMMMCall = asMMemberExpression.Expression as MethodCallExpression;
                                     if (asMMMCall != null)
                                     {
                                         //asMMMCall = {result.Last()}


                                         if (asMMMCall.Method.Name.TakeUntilIfAny("_") == "Last")
                                         {
                                             state.SelectCommand += ",\n\t g.`" + asMemberAssignment.Member.Name + "` as `" + asMemberAssignment.Member.Name + "`";
                                             s_SelectCommand += ",\n\t s.`" + asMMemberExpression.Member.Name + "_" + asMemberAssignment.Member.Name + "` as `" + asMemberAssignment.Member.Name + "`";
                                             return;
                                         }
                                     }
                                 }

                             }
                             #endregion

                             #region asMMemberExpressionParameterExpression
                             var asMMemberExpressionParameterExpression = asMemberExpression.Expression as ParameterExpression;
                             if (asMMemberExpressionParameterExpression != null)
                             {
                                 if (asMemberInitExpressionByParameter0 != null)
                                 {
                                     if (asMemberInitExpressionByParameter0 != asMMemberExpressionParameterExpression)
                                     {
                                         // group by within a join, where this select is not part of this outer source!

                                         return;
                                     }
                                 }


                                 state.SelectCommand += ",\n\t g.`" + asMemberAssignment.Member.Name + "` as `" + asMemberAssignment.Member.Name + "`";
                                 s_SelectCommand += ",\n\t s.`" + asMemberExpression.Member.Name + "` as `" + asMemberAssignment.Member.Name + "`";

                                 return;
                             }
                             #endregion


                             //asMMemberExpression.Member
                             Debugger.Break();
                         };
                     #endregion


                     #region WriteExpression COPY from GroupBy
                     Action<int, Expression, MemberInfo> WriteExpression =
                         (index, asExpression, TargetMember) =>
                         {
                             var asMemberAssignment = new { Expression = asExpression, Member = TargetMember };

                             #region asMConstantExpression
                             {
                                 var asMConstantExpression = asMemberAssignment.Expression as ConstantExpression;
                                 if (asMConstantExpression != null)
                                 {
                                     var asMPropertyInfo = asMemberAssignment.Member as FieldInfo;
                                     //var value1 = asMPropertyInfo.GetValue(asMConstantExpression.Value);
                                     var rAddParameterValue0 = asMConstantExpression.Value;

                                     var n = "@arg" + state.ApplyParameter.Count;


                                     state.SelectCommand += ",\n\t g.`" + asMemberAssignment.Member.Name + "` as `" + asMemberAssignment.Member.Name + "`";
                                     s_SelectCommand += ",\n\t " + n + " as `" + asMemberAssignment.Member.Name + "`";

                                     state.ApplyParameter.Add(
                                         c =>
                                         {
                                             // either the actualt command or the explain command?

                                             //c.Parameters.AddWithValue(n, r);
                                             c.AddParameter(n, rAddParameterValue0);
                                         }
                                     );


                                     //if (rAddParameterValue0 is string)
                                     //{
                                     //    // NULL?
                                     //    state.SelectCommand += ",\n\t '" + rAddParameterValue0 + "' as `" + asMemberAssignment.Member.Name + "`";
                                     //}
                                     //else
                                     //{
                                     //    // long?
                                     //    state.SelectCommand += ",\n\t " + rAddParameterValue0 + " as `" + asMemberAssignment.Member.Name + "`";
                                     //}

                                     return;
                                 }
                             }
                             #endregion

                             //                                 -		asMemberAssignment.Expression	{GroupByGoo.Count()}	System.Linq.Expressions.Expression {System.Linq.Expressions.MethodCallExpressionN}
                             //+		Method	{Int64 Count(ScriptCoreLib.Shared.Data.Diagnostics.IQueryStrategy`1[TestSQLiteGroupBy.Data.Book1MiddleRow])}	System.Reflection.MethodInfo {System.Reflection.RuntimeMethodInfo}

                             #region asMethodCallExpression
                             var asMethodCallExpression = asMemberAssignment.Expression as MethodCallExpression;
                             if (asMethodCallExpression != null)
                             {
                                 Console.WriteLine(new { index, asMethodCallExpression.Method });

                                 #region count(*) special!
                                 if (asMethodCallExpression.Method.Name.TakeUntilIfAny("_") == "Count")
                                 {

                                     state.SelectCommand += ",\n\t g.`" + asMemberAssignment.Member.Name + "` as `" + asMemberAssignment.Member.Name + "`";
                                     s_SelectCommand += ",\n\t count(*) as `" + asMemberAssignment.Member.Name + "`";

                                     return;
                                 }
                                 #endregion

                                 #region  sum( special!!
                                 if (asMethodCallExpression.Method.Name.TakeUntilIfAny("_") == "Sum")
                                 {
                                     var arg1 = (asMethodCallExpression.Arguments[1] as UnaryExpression).Operand as LambdaExpression;
                                     if (arg1 != null)
                                     {
                                         var asMemberExpression = arg1.Body as MemberExpression;

                                         state.SelectCommand += ",\n\t g.`" + asMemberAssignment.Member.Name + "` as `" + asMemberAssignment.Member.Name + "`";
                                         s_SelectCommand += ",\n\t sum(s.`" + asMemberExpression.Member.Name + "`) as `" + asMemberAssignment.Member.Name + "`";
                                         return;
                                     }
                                 }
                                 #endregion


                                 #region  min( special!!
                                 if (asMethodCallExpression.Method.Name.TakeUntilIfAny("_") == "Min")
                                 {
                                     // X:\jsc.svn\examples\javascript\LINQ\MinMaxAverageExperiment\MinMaxAverageExperiment\ApplicationWebService.cs

                                     var arg1 = (asMethodCallExpression.Arguments[1] as UnaryExpression).Operand as LambdaExpression;
                                     if (arg1 != null)
                                     {
                                         var asMemberExpression = arg1.Body as MemberExpression;

                                         state.SelectCommand += ",\n\t g.`" + asMemberAssignment.Member.Name + "` as `" + asMemberAssignment.Member.Name + "`";
                                         s_SelectCommand += ",\n\t min(s.`" + asMemberExpression.Member.Name + "`) as `" + asMemberAssignment.Member.Name + "`";
                                         return;
                                     }
                                 }
                                 #endregion

                                 #region  max( special!!
                                 if (asMethodCallExpression.Method.Name.TakeUntilIfAny("_") == "Max")
                                 {
                                     // X:\jsc.svn\examples\javascript\LINQ\MinMaxAverageExperiment\MinMaxAverageExperiment\ApplicationWebService.cs

                                     var arg1 = (asMethodCallExpression.Arguments[1] as UnaryExpression).Operand as LambdaExpression;
                                     if (arg1 != null)
                                     {
                                         var asMemberExpression = arg1.Body as MemberExpression;

                                         state.SelectCommand += ",\n\t g.`" + asMemberAssignment.Member.Name + "` as `" + asMemberAssignment.Member.Name + "`";
                                         s_SelectCommand += ",\n\t max(s.`" + asMemberExpression.Member.Name + "`) as `" + asMemberAssignment.Member.Name + "`";
                                         return;
                                     }
                                 }
                                 #endregion

                                 #region  avg( special!!
                                 if (asMethodCallExpression.Method.Name.TakeUntilIfAny("_") == "Average")
                                 {
                                     // X:\jsc.svn\examples\javascript\LINQ\MinMaxAverageExperiment\MinMaxAverageExperiment\ApplicationWebService.cs

                                     var arg1 = (asMethodCallExpression.Arguments[1] as UnaryExpression).Operand as LambdaExpression;
                                     if (arg1 != null)
                                     {
                                         var asMemberExpression = arg1.Body as MemberExpression;

                                         state.SelectCommand += ",\n\t g.`" + asMemberAssignment.Member.Name + "` as `" + asMemberAssignment.Member.Name + "`";
                                         s_SelectCommand += ",\n\t avg(s.`" + asMemberExpression.Member.Name + "`) as `" + asMemberAssignment.Member.Name + "`";
                                         return;
                                     }
                                 }
                                 #endregion


                                 #region  lower( special!!
                                 if (asMethodCallExpression.Method.Name.TakeUntilIfAny("_") == "ToLower")
                                 {
                                     // X:\jsc.svn\examples\javascript\LINQ\MinMaxAverageExperiment\MinMaxAverageExperiment\ApplicationWebService.cs

                                     var asMemberExpression = asMethodCallExpression.Object as MemberExpression;
                                     if (asMemberExpression != null)
                                     {

                                         state.SelectCommand += ",\n\t g.`" + asMemberAssignment.Member.Name + "` as `" + asMemberAssignment.Member.Name + "`";
                                         s_SelectCommand += ",\n\t lower(s.`" + asMemberExpression.Member.Name + "`) as `" + asMemberAssignment.Member.Name + "`";
                                         return;
                                     }
                                 }
                                 #endregion

                                 #region  upper( special!!
                                 if (asMethodCallExpression.Method.Name.TakeUntilIfAny("_") == "ToUpper")
                                 {
                                     // X:\jsc.svn\examples\javascript\LINQ\MinMaxAverageExperiment\MinMaxAverageExperiment\ApplicationWebService.cs

                                     var asMemberExpression = asMethodCallExpression.Object as MemberExpression;
                                     if (asMemberExpression != null)
                                         {

                                         state.SelectCommand += ",\n\t g.`" + asMemberAssignment.Member.Name + "` as `" + asMemberAssignment.Member.Name + "`";
                                         s_SelectCommand += ",\n\t upper(s.`" + asMemberExpression.Member.Name + "`) as `" + asMemberAssignment.Member.Name + "`";
                                         return;
                                     }
                                 }
                                 #endregion

                                 #region  ltrim( special!!
                                 if (asMethodCallExpression.Method.Name.TakeUntilIfAny("_") == "TrimStart")
                                 {
                                     // X:\jsc.svn\examples\javascript\LINQ\MinMaxAverageExperiment\MinMaxAverageExperiment\ApplicationWebService.cs

                                     var asMemberExpression = asMethodCallExpression.Object as MemberExpression;
                                     if (asMemberExpression != null)
                                     {

                                         state.SelectCommand += ",\n\t g.`" + asMemberAssignment.Member.Name + "` as `" + asMemberAssignment.Member.Name + "`";
                                         s_SelectCommand += ",\n\t ltrim(s.`" + asMemberExpression.Member.Name + "`) as `" + asMemberAssignment.Member.Name + "`";
                                         return;
                                     }
                                 }
                                 #endregion

                                 #region  rtrim( special!!
                                 if (asMethodCallExpression.Method.Name.TakeUntilIfAny("_") == "TrimEnd")
                                 {
                                     // X:\jsc.svn\examples\javascript\LINQ\MinMaxAverageExperiment\MinMaxAverageExperiment\ApplicationWebService.cs

                                     var asMemberExpression = asMethodCallExpression.Object as MemberExpression;
                                     if (asMemberExpression != null)
                                     {

                                         state.SelectCommand += ",\n\t g.`" + asMemberAssignment.Member.Name + "` as `" + asMemberAssignment.Member.Name + "`";
                                         s_SelectCommand += ",\n\t rtrim(s.`" + asMemberExpression.Member.Name + "`) as `" + asMemberAssignment.Member.Name + "`";
                                         return;
                                     }
                                 }
                                 #endregion

                                 #region  trim( special!!
                                 if (asMethodCallExpression.Method.Name.TakeUntilIfAny("_") == "Trim")
                                 {
                                     // X:\jsc.svn\examples\javascript\LINQ\MinMaxAverageExperiment\MinMaxAverageExperiment\ApplicationWebService.cs

                                     var asMemberExpression = asMethodCallExpression.Object as MemberExpression;
                                     if (asMemberExpression != null)
                                     {

                                         state.SelectCommand += ",\n\t g.`" + asMemberAssignment.Member.Name + "` as `" + asMemberAssignment.Member.Name + "`";
                                         s_SelectCommand += ",\n\t trim(s.`" + asMemberExpression.Member.Name + "`) as `" + asMemberAssignment.Member.Name + "`";
                                         return;
                                     }
                                 }
                                 #endregion
                             }
                             #endregion



                             #region asMemberExpression
                             {
                                 // m_getterMethod = {TestSQLiteGroupBy.Data.GooStateEnum get_Key()}

                                 var asMemberExpression = asMemberAssignment.Expression as MemberExpression;
                                 Console.WriteLine(new { index, asMemberExpression });
                                 if (asMemberExpression != null)
                                 {
                                     WriteMemberExpression(index, asMemberExpression, TargetMember);
                                     return;
                                 }
                             }
                             #endregion

                             #region  asMemberAssignment.Expression = {Convert(GroupByGoo.First())}
                             var asUnaryExpression = asMemberAssignment.Expression as UnaryExpression;

                             Console.WriteLine(new { index, asUnaryExpression });

                             if (asUnaryExpression != null)
                             {
                                 #region asUnaryExpression_Operand_asFieldExpression
                                 var asUnaryExpression_Operand_asFieldExpression = asUnaryExpression.Operand as MemberExpression;
                                 if (asUnaryExpression_Operand_asFieldExpression != null)
                                 {
                                     // reduce? flatten?  nested join?
                                     //asFieldExpression = asFieldExpression_Expression_asFieldExpression;
                                     var __projection = asUnaryExpression_Operand_asFieldExpression.Expression as MemberExpression;

                                     state.SelectCommand += ",\n\t g.`" + asMemberAssignment.Member.Name + "` as `" + asMemberAssignment.Member.Name + "`";
                                     s_SelectCommand += ",\n\t s.`"


                                         + GroupingKeyFieldExpressionName + "` as `" + asMemberAssignment.Member.Name + "`";

                                     return;
                                 }
                                 #endregion

                                 #region asMemberExpressionMethodCallExpression
                                 var asMemberExpressionMethodCallExpression = asUnaryExpression.Operand as MethodCallExpression;
                                 if (asMemberExpressionMethodCallExpression != null)
                                 {
                                     Console.WriteLine(new { index, asMemberExpressionMethodCallExpression.Method });
                                     // special! op_Implicit
                                     if (asMemberExpressionMethodCallExpression.Method.Name.TakeUntilIfAny("_") == "First")
                                     {
                                         gDescendingByKeyReferenced = true;
                                         state.SelectCommand += ",\n\t gDescendingByKey.`" + asMemberAssignment.Member.Name + "` as `" + asMemberAssignment.Member.Name + "`";
                                         s_SelectCommand += ",\n\t s.`Key` as `" + asMemberAssignment.Member.Name + "`";
                                         return;
                                     }

                                     if (asMemberExpressionMethodCallExpression.Method.Name.TakeUntilIfAny("_") == "Last")
                                     {
                                         state.SelectCommand += ",\n\t g.`" + asMemberAssignment.Member.Name + "` as `" + asMemberAssignment.Member.Name + "`";
                                         s_SelectCommand += ",\n\t s.`Key` as `" + asMemberAssignment.Member.Name + "`";
                                         return;
                                     }
                                 }
                                 #endregion

                             }
                             #endregion



                             Debugger.Break();
                         };
                     #endregion


                     if (asMemberInitExpression == null)
                     {
                         state.SelectCommand = "select 0 as foo";

                         #region asNewExpression
                         var asNewExpression = (that.selector as LambdaExpression).Body as NewExpression;

                         asNewExpression.Arguments.WithEachIndex(
                             (SourceArgument, index) =>
                             {
                                 var TargetMember = asNewExpression.Members[index];
                                 var asMemberAssignment = new { Member = TargetMember };


                                 WriteExpression(index, SourceArgument, TargetMember);
                             }
                         );
                         #endregion

                         var g = s_SelectCommand
                             + "\n from " + s.GetQualifiedTableNameOrToString().Replace("\n", "\n\t") + " as s ";

                         state.FromCommand =
                              "from (\n\t"
                                + g.Replace("\n", "\n\t")
                                + "\n) as g";
                     }
                     else
                     {
                         // ??

                         state.SelectCommand = s.SelectCommand;
                         state.FromCommand = s.FromCommand;
                     }

                     //state.ApplyParameter.AddRange(s.ApplyParameter);

                     state.ApplyParameter.AddRange(s.ApplyParameter);


                     //asGroupByQueryStrategy.upperSelect = null;
                 }
            );


            return that;
        }

    }
}

