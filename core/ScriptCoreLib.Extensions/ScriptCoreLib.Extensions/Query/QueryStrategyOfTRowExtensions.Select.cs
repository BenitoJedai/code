﻿using ScriptCoreLib.Shared.BCLImplementation.System.Data.Common;
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
using System.Xml.Linq;

namespace System.Data
{
    // move to a nuget?
    // shall reimplement IQueriable for jsc data layer gen
    //[Obsolete("the first generic extension method for all jsc data layer rows")]
    public static partial class QueryStrategyOfTRowExtensions
    {
        [ScriptCoreLib.ScriptAttribute.ExplicitInterface]
        public interface ISelectQueryStrategy : INestedQueryStrategy
        {
            // allow to inspect upper select . what if there are multiple upper selects?
            Expression selectorExpression { get; }

            IQueryStrategy source { get; }

            string scalarAggregateOperand { get; set; }
            // ? gDescendingByKeyReferenced
        }

        class SelectQueryStrategy<TSource, TResult> : XQueryStrategy<TResult>, ISelectQueryStrategy
        {
            public string scalarAggregateOperand { get; set; }

            public IQueryStrategy<TSource> source;
            public Expression<Func<TSource, TResult>> selector;




            public ISelectQueryStrategy upperSelect { get; set; }
            public IJoinQueryStrategy upperJoin { get; set; }
            public IGroupByQueryStrategy upperGroupBy { get; set; }


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

        // can we get diagnostics on every line we write to sql?
        static readonly Func<string> CommentLineNumber =
            delegate
        {
            // what would happen id we did this elsewhere?
            var f = new StackTrace(fNeedFileInfo: true).GetFrame(1);

            // http://dev.mysql.com/doc/refman/5.0/en/comments.html
            return " /* " + f.GetFileName().SkipUntilLastOrEmpty("\\") + ":" + f.GetFileLineNumber() + " */ ";
        };


        // https://sites.google.com/a/jsc-solutions.net/backlog/knowledge-base/2014/201406/20140601/let
        static MethodInfo refSelect = new Func<IQueryStrategy<object>, Expression<Func<object, object>>, IQueryStrategy<object>>(QueryStrategyOfTRowExtensions.Select).Method;
        // 
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



                     var asLambdaExpression = that.selector as LambdaExpression;
                     var xouter_Paramerer = asLambdaExpression.Parameters[0];

                     var asMemberInitExpression = (asLambdaExpression).Body as MemberInitExpression;

                     (that.source as INestedQueryStrategy).With(q => q.upperSelect = that);

                     // x:\jsc.svn\examples\javascript\linq\test\testjoinselectanonymoustype\testjoinselectanonymoustype\applicationwebservice.cs

                     var s = QueryStrategyExtensions.AsCommandBuilder(that.source);


                     // X:\jsc.svn\examples\javascript\LINQ\MinMaxAverageExperiment\MinMaxAverageExperiment\ApplicationWebService.cs


                     // ???
                     var gDescendingByKeyReferenced = false;
                     var GroupingKeyFieldExpressionName = "?";

                     //var asMemberInitExpression = default(MemberInitExpression);
                     var asMemberInitExpressionByParameter0 = default(ParameterExpression);
                     var asMemberInitExpressionByParameter1 = default(ParameterExpression);



                     var SelectCommand = default(string);
                     var s_SelectCommand =
                         CommentLineNumber() +
                         "select -- diagnostics ";

                     // are we using it or what?
                     #region AddToSelectCommand
                     Action<string> AddToSelectCommand =
                         x =>
                    {
                        if (SelectCommand == null)
                            SelectCommand = "select " + x;
                        else
                            SelectCommand += ",\n\t " + x;
                    };
                     #endregion


                     Action<int, Expression, MemberInfo, Tuple<int, MemberInfo>[], MethodInfo> WriteExpression = null;

                     #region WriteMemberExpression
                     Action<int, MemberExpression, MemberInfo, Tuple<int, MemberInfo>[], MethodInfo> WriteMemberExpression =
                         (index, asMemberExpression, asMemberAssignment_Member, prefixes, valueSelector) =>
                         {
                             #region GetPrefixedTargetName
                             Func<string> GetPrefixedTargetName = delegate
                             {
                                 var w = "";


                                 foreach (var item in prefixes)
                                 {
                                     if (item.Item2 == null)
                                         w += item.Item1 + ".";
                                     else
                                         w += item.Item2.Name + ".";
                                 }

                                 // for primary constructors we know position.
                                 if (asMemberAssignment_Member == null)
                                     w += index;
                                 else
                                     w += asMemberAssignment_Member.Name;

                                 return w;
                             };
                             #endregion

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

                             #region WriteMemberExpression:asMemberExpressionMethodCallExpression
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

                                 // special! do we have reverse yet?
                                 if (asMemberExpressionMethodCallExpression.Method.Name.TakeUntilIfAny("_") == "First")
                                 {
                                     gDescendingByKeyReferenced = true;
                                     state.SelectCommand += ",\n\t gDescendingByKey.`" + asMemberAssignment.Member.Name + "` as `" + asMemberAssignment.Member.Name + "`";
                                     s_SelectCommand += ",\n\t s.`" + asMemberExpression.Member.Name + "` as `" + asMemberAssignment.Member.Name + "`";
                                     return;
                                 }


                                 if (asMemberExpressionMethodCallExpression.Method.Name == refLast.Name)
                                 {
                                     if (asMemberInitExpressionByParameter0 != null)
                                     {
                                         // https://sites.google.com/a/jsc-solutions.net/backlog/knowledge-base/2014/201405/20140513

                                         // the upper join dictates what it expects to find. no need to alias too early

                                         state.SelectCommand += ",\n\t g.`" + asMemberExpression.Member.Name + "` as `" + asMemberExpression.Member.Name + "`";
                                         s_SelectCommand += ",\n\t s.`" + asMemberExpression.Member.Name + "` as `" + asMemberExpression.Member.Name + "`";
                                         return;
                                     }

                                     if (source is IGroupByQueryStrategy)
                                     {
                                         // X:\jsc.svn\examples\javascript\LINQ\test\TestGroupByMultipleFields\TestGroupByMultipleFields\ApplicationWebService.cs
                                         // that grouping thing already did the aliasing for us?

                                         s_SelectCommand += ",\n" + CommentLineNumber() + "\t"
                                           + that.selector.Parameters[0].Name.Replace("<>", "__")
                                           + ".`" + asMemberAssignment.Member.Name + "` as `" + asMemberAssignment.Member.Name + "`";
                                         return;
                                     }


                                     // https://sites.google.com/a/jsc-solutions.net/backlog/knowledge-base/2014/201406/20140604
                                     // X:\jsc.svn\examples\javascript\LINQ\test\TestWhereJoinTTGroupBySelectLast\TestWhereJoinTTGroupBySelectLast\ApplicationWebService.cs
                                     s_SelectCommand += ",\n" + CommentLineNumber() + "\t"
                                           + that.selector.Parameters[0].Name.Replace("<>", "__")
                                           + ".`" + asMemberExpression.Member.Name + "` as `" + asMemberAssignment.Member.Name + "`";

                                     //state.SelectCommand += ",\n\t g.`" + asMemberAssignment.Member.Name + "` as `" + asMemberAssignment.Member.Name + "`";
                                     //s_SelectCommand += ",\n\t s.`" + asMemberExpression.Member.Name + "` as `" + asMemberAssignment.Member.Name + "`";
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




                             #region WriteMemberExpression:asMConstantExpression
                             //         var SpecialConstant_u = "44";
                             var asMConstantExpression = asMemberExpression.Expression as ConstantExpression;
                             if (asMConstantExpression != null)
                             {
                                 var asMPropertyInfo = asMemberExpression.Member as FieldInfo;
                                 var rAddParameterValue0 = asMPropertyInfo.GetValue(asMConstantExpression.Value);

                                 // X:\jsc.svn\examples\javascript\forms\Test\TestSQLGroupByAfterJoin\TestSQLGroupByAfterJoin\ApplicationWebService.cs

                                 var n = "@arg" + state.ApplyParameter.Count;

                                 s_SelectCommand += ",\n\t " + n + " as `" + GetPrefixedTargetName() + "`";

                                 state.ApplyParameter.Add(
                                     c =>
                                     {
                                         // either the actualt command or the explain command?

                                         //c.Parameters.AddWithValue(n, r);
                                         c.AddParameter(n, rAddParameterValue0);
                                     }
                                 );

                                 return;
                             }
                             #endregion



                             #region WriteMemberExpression:asMMemberExpression
                             var asMMemberExpression = asMemberExpression.Expression as MemberExpression;
                             if (asMMemberExpression != null)
                             {
                                 // X:\jsc.svn\examples\javascript\linq\test\TestSelectAndSubSelect\TestSelectAndSubSelect\ApplicationWebService.cs
                                 // X:\jsc.svn\examples\javascript\LINQ\test\TestWhereJoinTTGroupBySelectLast\TestWhereJoinTTGroupBySelectLast\ApplicationWebService.cs

                                 #region asIGroupByQueryStrategy
                                 var asIGroupByQueryStrategy = that.source as IGroupByQueryStrategy;
                                 if (asIGroupByQueryStrategy != null)
                                 {
                                     // our grouping has to flatten last and possibly also last selections
                                     // what if we wanted something in the middle too?
                                     // like instead of last, or first we want 2nd from last?

                                     #region string::
                                     if (valueSelector != null)
                                     {
                                         // for all string::
                                         if (valueSelector.Name == "ToLower")
                                         {
                                             // we are being selected intou a data group?
                                             // X:\jsc.svn\examples\javascript\LINQ\test\SelectToUpperIntoNewExpression\SelectToUpperIntoNewExpression\ApplicationWebService.cs

                                             s_SelectCommand += ",\n\t lower("
                                                + that.selector.Parameters[0].Name.Replace("<>", "__")
                                                + ".`" + asMMemberExpression.Member.Name + "_" + asMemberExpression.Member.Name + "`) as `" + GetPrefixedTargetName() + "`";

                                             return;
                                         }
                                     }
                                     #endregion


                                     s_SelectCommand += ",\n" + CommentLineNumber() + "\t"
                                         + that.selector.Parameters[0].Name.Replace("<>", "__")
                                         + ".`" + asMMemberExpression.Member.Name + "_" + asMemberExpression.Member.Name + "` as `" + GetPrefixedTargetName() + "`";

                                     return;
                                 }
                                 #endregion




                                 if (index < 0)
                                 {
                                     // X:\jsc.svn\examples\javascript\LINQ\test\TestSelectMath\TestSelectMath\ApplicationWebService.cs

                                     s_SelectCommand +=
                                    that.selector.Parameters[0].Name.Replace("<>", "__")
                                   + ".`" + asMemberExpression.Member.Name + "`";

                                     return;
                                 }
                                 s_SelectCommand += ",\n" + CommentLineNumber() + "\t"
                                   + that.selector.Parameters[0].Name.Replace("<>", "__")
                                   + ".`" + asMemberExpression.Member.Name + "` as `" + GetPrefixedTargetName() + "`";

                                 return;
                             }
                             #endregion

                             #region WriteMemberExpression:asMMemberExpressionParameterExpression
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

                                 #region asSelectQueryStrategy
                                 var asSelectQueryStrategy = (that.source as ISelectQueryStrategy);
                                 if (asSelectQueryStrategy != null)
                                 {
                                     // +		selector	{<>h__TransparentIdentifier1 => new <>f__AnonymousType2`2(<>h__TransparentIdentifier1 = <>h__TransparentIdentifier1, 
                                     // qq = new <>f__AnonymousType3`1(u = "!!!"))}	
                                     // System.Linq.Expressions.Expression<System.Func<<>f__AnonymousType1<<>f__AnonymousType0<TestSelectAndSubSelect.Data.PerformanceResourceTimingData2ApplicationPerformanceRow,string>,string>,<>f__AnonymousType2<<>f__AnonymousType1<<>f__AnonymousType0<TestSelectAndSubSelect.Data.PerformanceResourceTimingData2ApplicationPerformanceRow,string>,string>,<>f__AnonymousType3<string>>>>


                                     var asSLambdaExpression = asSelectQueryStrategy.selectorExpression as LambdaExpression;
                                     if (asSLambdaExpression != null)
                                     {
                                         //Body = { new <> f__AnonymousType2`2(<> h__TransparentIdentifier1 = <> h__TransparentIdentifier1, qq = new <> f__AnonymousType3`1(u = "!!!"))}
                                         var asSLNewExpression = asSLambdaExpression.Body as NewExpression;
                                         if (asSLNewExpression != null)
                                         {

                                             if (asSLNewExpression.Members[1].Name == asMemberExpression.Member.Name)
                                             {
                                                 // i think we may have that value on stack!
                                                 // X:\jsc.svn\examples\javascript\linq\test\TestSelectAndSubSelect\TestSelectAndSubSelect\ApplicationWebService.cs

                                                 // [1] = {new <>f__AnonymousType3`1(u = "!!!")}


                                                 var asSLNMethodCallExpression = asSLNewExpression.Arguments[1] as MethodCallExpression;
                                                 if (asSLNMethodCallExpression != null)
                                                 {

                                                     // mark the inputs for our selector
                                                     // X:\jsc.svn\core\ScriptCoreLib.Extensions\ScriptCoreLib.Extensions\Query\QueryStrategyOfTRowExtensions.AsGenericEnumerable.cs
                                                     // 554

                                                     s_SelectCommand += ",\n\t "
                                                              + that.selector.Parameters[0].Name.Replace("<>", "__")
                                                              + ".`" + asMemberExpression.Member.Name + "` as `" + GetPrefixedTargetName() + "`";

                                                     return;
                                                 }


                                                 var asSLNNewExpression = asSLNewExpression.Arguments[1] as NewExpression;
                                                 if (asSLNNewExpression != null)
                                                 {
                                                     asSLNNewExpression.Arguments.WithEachIndex(
                                                         (xSourceArgument, xIndex) =>
                                                         {
                                                             if (asSLNNewExpression.Members == null)
                                                             {
                                                                 // are we reselecting optional args?

                                                                 s_SelectCommand += ",\n\t "
                                                                + that.selector.Parameters[0].Name.Replace("<>", "__")
                                                                + ".`" + asMemberExpression.Member.Name + "." + xIndex + "` as `" + GetPrefixedTargetName() + "." + xIndex + "`";

                                                                 return;
                                                             }

                                                             var m = asSLNNewExpression.Members[xIndex];

                                                             s_SelectCommand += ",\n\t "
                                                                + that.selector.Parameters[0].Name.Replace("<>", "__")
                                                                + ".`" + asMemberExpression.Member.Name + "." + m.Name + "` as `" + GetPrefixedTargetName() + "." + m.Name + "`";

                                                             //WriteExpression (xIndex, xSourceArgument, asSLNNewExpression.Members[xIndex], prefixes, null);
                                                         }
                                                     );
                                                     return;
                                                 }

                                             }


                                         }
                                     }
                                 }
                                 #endregion



                                 #region asIGroupByQueryStrategy
                                 var asIGroupByQueryStrategy = that.source as IGroupByQueryStrategy;
                                 if (asIGroupByQueryStrategy != null)
                                 {
                                     #region Key
                                     if (asMemberExpression.Member.Name == "Key")
                                     {
                                         // X:\jsc.svn\examples\javascript\LINQ\test\TestGroupByMultipleFields\TestGroupByMultipleFields\ApplicationWebService.cs
                                         // X:\jsc.svn\examples\javascript\LINQ\test\TestWhereJoinTTGroupBySelectLast\TestWhereJoinTTGroupBySelectLast\ApplicationWebService.cs

                                         var asSSLambdaExpression = asIGroupByQueryStrategy.keySelector as LambdaExpression;


                                         var asSSNNewExpression = asSSLambdaExpression.Body as NewExpression;
                                         if (asSSNNewExpression != null)
                                         {
                                             asSSNNewExpression.Arguments.WithEachIndex(
                                                (SourceArgument, i) =>
                                                {
                                                    // Constructor = {Void .ctor(System.Xml.Linq.XName, System.Object)}
                                                    var SourceMember = default(MemberInfo);
                                                    if (asSSNNewExpression.Members != null)
                                                        SourceMember = asSSNNewExpression.Members[i];
                                                    // c# extension operators for enumerables, thanks
                                                    WriteExpression(i, SourceArgument, SourceMember, prefixes.Concat(new[] { Tuple.Create(index, asMemberExpression.Member) }).ToArray(), null);
                                                }
                                            );
                                             return;
                                         }

                                         WriteExpression(
                                             index, asSSLambdaExpression.Body, asMemberExpression.Member, prefixes, null);
                                         return;
                                     }
                                     #endregion

                                     // asMemberExpression
                                     s_SelectCommand += ",\n" + CommentLineNumber() + "\t"
                                         + that.selector.Parameters[0].Name.Replace("<>", "__")
                                         + ".`" + asMMemberExpressionParameterExpression.Name + "_" + asMemberExpression.Member.Name + "` as `" + GetPrefixedTargetName() + "`";
                                     return;
                                 }
                                 #endregion




                                 if (valueSelector != null)
                                 {
                                     if (valueSelector.Name == "ToUpper")
                                     {
                                         // we are being selected intou a data group?
                                         // X:\jsc.svn\examples\javascript\LINQ\test\SelectToUpperIntoNewExpression\SelectToUpperIntoNewExpression\ApplicationWebService.cs

                                         s_SelectCommand += ",\n\t upper("
                                           + that.selector.Parameters[0].Name.Replace("<>", "__")
                                           + ".`" + asMemberExpression.Member.Name + "`) as `" + GetPrefixedTargetName() + "`";
                                         return;
                                     }
                                 }


                                 if (!string.IsNullOrEmpty(that.scalarAggregateOperand))
                                 {
                                     s_SelectCommand += ",\n\t " + that.scalarAggregateOperand + "("
                                        + that.selector.Parameters[0].Name.Replace("<>", "__")
                                        + ".`" + asMemberExpression.Member.Name + "`) as `" + GetPrefixedTargetName() + "`";

                                     return;
                                 }


                                 if (index < 0)
                                 {
                                     // are we part of binary expression?
                                     // X:\jsc.svn\examples\javascript\LINQ\test\TestSelectMath\TestSelectMath\ApplicationWebService.cs

                                     s_SelectCommand +=
                                     that.selector.Parameters[0].Name.Replace("<>", "__")
                                     + ".`" + asMemberExpression.Member.Name + "`";

                                     return;
                                 }

                                 s_SelectCommand += ",\n" + CommentLineNumber() + "\t"
                                 + that.selector.Parameters[0].Name.Replace("<>", "__")
                                 + ".`" + asMemberExpression.Member.Name + "` as `" + GetPrefixedTargetName() + "`";

                                 return;
                             }
                             #endregion


                             //asMMemberExpression.Member
                             Debugger.Break();
                         };
                     #endregion


                     #region WriteExpression 

                     WriteExpression =
                         (index, asExpression, TargetMember, prefixes, valueSelector) =>
                         {
                             var asMemberAssignment = new { Expression = asExpression, Member = TargetMember };

                             #region GetPrefixedTargetName
                             Func<string> GetPrefixedTargetName = delegate
                             {
                                 var w = "";


                                 foreach (var item in prefixes)
                                 {
                                     if (item.Item2 == null)
                                         w += item.Item1 + ".";
                                     else
                                         w += item.Item2.Name + ".";
                                 }

                                 // for primary constructors we know position.
                                 if (TargetMember == null)
                                     w += index;
                                 else
                                     w += TargetMember.Name;

                                 return w;
                             };
                             #endregion

                             #region WriteExpression:asMConstantExpression
                             {
                                 var asMConstantExpression = asMemberAssignment.Expression as ConstantExpression;
                                 if (asMConstantExpression != null)
                                 {
                                     var asMPropertyInfo = asMemberAssignment.Member as FieldInfo;
                                     //var value1 = asMPropertyInfo.GetValue(asMConstantExpression.Value);
                                     var rAddParameterValue0 = asMConstantExpression.Value;


                                     // X:\jsc.svn\examples\javascript\linq\test\TestSelectAndSubSelect\TestSelectAndSubSelect\ApplicationWebService.cs

                                     var n = "@";

                                     INestedQueryStrategy u = that;
                                     while (u.upperSelect != null)
                                     {

                                         n += "_";
                                         u = u.upperSelect;

                                     }


                                     n += "arg" + state.ApplyParameter.Count;

                                     if (index < 0)
                                        s_SelectCommand += n ;
                                     else
                                         s_SelectCommand += ",\n\t " + n + " as `" + GetPrefixedTargetName() + "`";

                                     state.ApplyParameter.Add(
                                         c =>
                                         {
                                             // either the actualt command or the explain command?
                                             c.AddParameter(n, rAddParameterValue0);
                                         }
                                     );


                                     return;
                                 }
                             }
                             #endregion

                             //                                 -		asMemberAssignment.Expression	{GroupByGoo.Count()}	System.Linq.Expressions.Expression {System.Linq.Expressions.MethodCallExpressionN}
                             //+		Method	{Int64 Count(ScriptCoreLib.Shared.Data.Diagnostics.IQueryStrategy`1[TestSQLiteGroupBy.Data.Book1MiddleRow])}	System.Reflection.MethodInfo {System.Reflection.RuntimeMethodInfo}

                             #region WriteExpression:asMethodCallExpression
                             var asMethodCallExpression = asMemberAssignment.Expression as MethodCallExpression;
                             if (asMethodCallExpression != null)
                             {
                                 var refXNameGet = new Func<string, string, XName>(XName.Get);

                                 if (asMethodCallExpression.Method == refXNameGet.Method)
                                 {
                                     var asMConstantExpression = asMethodCallExpression.Arguments[0] as ConstantExpression;
                                     if (asMConstantExpression != null)
                                     {
                                         var asMPropertyInfo = asMemberAssignment.Member as FieldInfo;
                                         //var value1 = asMPropertyInfo.GetValue(asMConstantExpression.Value);
                                         var rAddParameterValue0 = asMConstantExpression.Value;

                                         var n = "@arg" + state.ApplyParameter.Count;

                                         s_SelectCommand += ",\n\t " + n + " as `" + GetPrefixedTargetName() + "`";

                                         state.ApplyParameter.Add(
                                             c =>
                                         {
                                             // either the actualt command or the explain command?
                                             c.AddParameter(n, rAddParameterValue0);
                                         }
                                         );


                                         return;
                                     }
                                 }


                                 //if (asMethodCallExpression.Method.DeclaringType != typeof(XName))

                                 #region string::
                                 if (asMethodCallExpression.Method.DeclaringType == typeof(string))
                                 {
                                     #region  lower( special!!
                                     if (asMethodCallExpression.Method.Name.TakeUntilIfAny("_") == "ToLower")
                                     {
                                         // X:\jsc.svn\examples\javascript\LINQ\MinMaxAverageExperiment\MinMaxAverageExperiment\ApplicationWebService.cs
                                         // X:\jsc.svn\examples\javascript\LINQ\test\SelectToUpperIntoNewExpression\SelectToUpperIntoNewExpression\ApplicationWebService.cs
                                         // X:\jsc.svn\examples\javascript\LINQ\test\TestWhereJoinTTGroupBySelectLast\TestWhereJoinTTGroupBySelectLast\ApplicationWebService.cs

                                         var asMemberExpression = asMethodCallExpression.Object as MemberExpression;
                                         if (asMemberExpression != null)
                                         {
                                             //var asMMemberExpression = asMemberExpression.Expression as MemberExpression;


                                             WriteMemberExpression(index, asMemberExpression, TargetMember, prefixes, asMethodCallExpression.Method);
                                             //s_SelectCommand += ",\n\t lower("
                                             //    + that.selector.Parameters[0].Name.Replace("<>", "__")
                                             //    + ".`" + asMemberExpression.Member.Name + "`) as `" + GetPrefixedTargetName() + "`";
                                             return;
                                         }
                                     }
                                     #endregion

                                     #region  upper( special!!
                                     if (asMethodCallExpression.Method.Name.TakeUntilIfAny("_") == "ToUpper")
                                     {
                                         // X:\jsc.svn\examples\javascript\LINQ\MinMaxAverageExperiment\MinMaxAverageExperiment\ApplicationWebService.cs
                                         // X:\jsc.svn\examples\javascript\LINQ\test\SelectToUpperIntoNewExpression\SelectToUpperIntoNewExpression\ApplicationWebService.cs

                                         var asMemberExpression = asMethodCallExpression.Object as MemberExpression;
                                         if (asMemberExpression != null)
                                         {

                                             //state.SelectCommand += ",\n\t g.`" + asMemberAssignment.Member.Name + "` as `" + asMemberAssignment.Member.Name + "`";
                                             s_SelectCommand += ",\n\t upper("
                                                 + that.selector.Parameters[0].Name.Replace("<>", "__")
                                                 + ".`" + asMemberExpression.Member.Name + "`) as `" + GetPrefixedTargetName() + "`";
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

                                             s_SelectCommand += ",\n\t ltrim("
                                                                    + that.selector.Parameters[0].Name.Replace("<>", "__")
                                                                    + ".`" + asMemberExpression.Member.Name + "`) as `" + GetPrefixedTargetName() + "`";
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

                                             s_SelectCommand += ",\n\t rtrim("
                                                                     + that.selector.Parameters[0].Name.Replace("<>", "__")
                                                                     + ".`" + asMemberExpression.Member.Name + "`) as `" + GetPrefixedTargetName() + "`";
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

                                             s_SelectCommand += ",\n\t trim("
                                                                        + that.selector.Parameters[0].Name.Replace("<>", "__")
                                                                        + ".`" + asMemberExpression.Member.Name + "`) as `" + GetPrefixedTargetName() + "`";
                                             return;
                                         }
                                     }
                                     #endregion

                                     Debugger.Break();
                                 }
                                 #endregion

                                 if (asMethodCallExpression.Method.DeclaringType == typeof(QueryStrategyOfTRowExtensions))
                                 {
                                     // user call on select?
                                     // x:\jsc.svn\examples\javascript\linq\test\selecttoupperintonewexpression\selecttoupperintonewexpression\applicationwebservice.cs

                                     // asMethodCallExpression.Method = {System.String StaticSpecial(System.String)}
                                     // asMethodCallExpression.Method = {System.Tuple`2[System.String,System.Int64] Create[String,Int64](System.String, Int64)}
                                     // asMethodCallExpression.Method = {System.Xml.Linq.XName Get(System.String, System.String)}

                                     #region subquery
                                     Func<MethodCallExpression, IQueryStrategy> subquery =
                                         arg0ElementsBySelect =>
                                         {
                                             // x:\jsc.svn\examples\javascript\linq\test\testselectandsubselect\testselectandsubselect\applicationwebservice.cs
                                             // X:\jsc.svn\examples\javascript\LINQ\test\TestSelectOfSelect\TestSelectOfSelect\ApplicationWebService.cs

                                             // arg0 = {new ApplicationResourcePerformance("file:PerformanceResourceTimingData2.xlsx.sqlite").Where(kk => (kk.duration == 46)).Select(kk => kk.path)}
                                             // asMethodCallExpression = {new ApplicationResourcePerformance("file:PerformanceResourceTimingData2.xlsx.sqlite").Where(kk => (kk.duration == 46)).Select(kk => kk.path).FirstOrDefault()}
                                             // https://sites.google.com/a/jsc-solutions.net/backlog/knowledge-base/2014/201406/20140601/let


                                             if (arg0ElementsBySelect.Method.Name == refSelect.Name)
                                             {
                                                 #region select

                                                 #region doSelect
                                                 Func<IQueryStrategy, IQueryStrategy> doSelect =
                                                     xTable_Where_OrderByDescending =>
                                                     {
                                                         var __Select_selector = arg0ElementsBySelect.Arguments[1] as UnaryExpression;

                                                         var xTable_Where_Select = (IQueryStrategy)arg0ElementsBySelect.Method.Invoke(null,
                                                             parameters: new object[] { xTable_Where_OrderByDescending, __Select_selector.Operand }
                                                         );

                                                         return xTable_Where_Select;
                                                     };
                                                 #endregion

                                                 // __source = {new ApplicationResourcePerformance("file:PerformanceResourceTimingData2.xlsx.sqlite").Where(kk => (kk.duration == 46))}
                                                 // orderby
                                                 var __Select_source = arg0ElementsBySelect.Arguments[0] as MethodCallExpression;
                                                 if (__Select_source != null)
                                                 {
                                                     if (__Select_source.Method.Name == refOrderByDescending.Name)
                                                     {
                                                         #region orderby
                                                         #region doOrderBy
                                                         Func<IQueryStrategy, IQueryStrategy> doOrderBy =
                                                            xTable_Where =>
                                                             {
                                                                 // X:\jsc.svn\core\ScriptCoreLib.Extensions\ScriptCoreLib.Extensions\Query\QueryStrategyOfTRowExtensions.OrderBy.cs
                                                                 var __OrderByDescending_keySelector = __Select_source.Arguments[1] as UnaryExpression;

                                                                 var xTable_Where_OrderByDescending = (IQueryStrategy)__Select_source.Method.Invoke(null,
                                                                    parameters: new object[] { xTable_Where, __OrderByDescending_keySelector.Operand }
                                                                );
                                                                 return xTable_Where_OrderByDescending;
                                                             };
                                                         #endregion

                                                         // where
                                                         var __OrderByDescending_source = __Select_source.Arguments[0] as MethodCallExpression;
                                                         if (__OrderByDescending_source.Method.Name == refWhere.Name)
                                                         {

                                                             #region doWhere
                                                             Func<IQueryStrategy, IQueryStrategy> doWhere =
                                                                xTable =>
                                                                {
                                                                    // Operand = {kk => (kk.duration == 46)}
                                                                    var __Where_filter = __OrderByDescending_source.Arguments[1] as UnaryExpression;

                                                                    // X:\jsc.svn\core\ScriptCoreLib.Extensions\ScriptCoreLib.Extensions\Query\QueryStrategyOfTRowExtensions.Where.cs
                                                                    var xTable_Where = (IQueryStrategy)__OrderByDescending_source.Method.Invoke(null,
                                                                         parameters: new object[] { xTable, __Where_filter.Operand }
                                                                     );

                                                                    return xTable_Where;
                                                                };
                                                             #endregion

                                                             // from
                                                             var __Where_source = __OrderByDescending_source.Arguments[0] as NewExpression;
                                                             if (__Where_source != null)
                                                             {
                                                                 // do we have enough information to perfrm sql rendering?
                                                                 // Constructor = {Void .ctor(System.String)}

                                                                 // is it really our own table, jsc data layer? :P are they in the same database as current source?

                                                                 //var xTable_datasource = (string)(__Where_source.Arguments[0] as ConstantExpression).Value;
                                                                 var xTable = (IQueryStrategy)__Where_source.Constructor.Invoke(
                                                                     parameters: null
                                                                 );

                                                                 var xTable_Where = doWhere(xTable);
                                                                 var xTable_Where_OrderByDescending = doOrderBy(xTable_Where);
                                                                 var xTable_Where_Select = doSelect(xTable_Where_OrderByDescending);



                                                                 return xTable_Where_Select;
                                                             }
                                                         }
                                                         #endregion
                                                     }
                                                     else if (__Select_source.Method.Name == refWhere.Name)
                                                     {
                                                         #region doWhere
                                                         Func<IQueryStrategy, IQueryStrategy> doWhere =
                                                            xTable =>
                                                                {
                                                                    // Operand = {kk => (kk.duration == 46)}
                                                                    var __Where_filter = __Select_source.Arguments[1] as UnaryExpression;

                                                                    // X:\jsc.svn\core\ScriptCoreLib.Extensions\ScriptCoreLib.Extensions\Query\QueryStrategyOfTRowExtensions.Where.cs
                                                                    var xTable_Where = (IQueryStrategy)__Select_source.Method.Invoke(null,
                                                                         parameters: new object[] { xTable, __Where_filter.Operand }
                                                                     );

                                                                    return xTable_Where;
                                                                };
                                                         #endregion

                                                         // from
                                                         var __Where_source = __Select_source.Arguments[0] as NewExpression;
                                                         if (__Where_source != null)
                                                         {
                                                             // do we have enough information to perfrm sql rendering?
                                                             // Constructor = {Void .ctor(System.String)}

                                                             // is it really our own table, jsc data layer? :P are they in the same database as current source?

                                                             //var xTable_datasource = (string)(__Where_source.Arguments[0] as ConstantExpression).Value;
                                                             var xTable = (IQueryStrategy)__Where_source.Constructor.Invoke(
                                                                 parameters: null
                                                             );

                                                             var xTable_Where = doWhere(xTable);
                                                             //var xTable_Where_OrderByDescending = doOrderBy(xTable_Where);
                                                             var xTable_Where_Select = doSelect(xTable_Where);


                                                             return xTable_Where_Select;
                                                         }
                                                     }
                                                 }
                                                 else
                                                 {
                                                     // X:\jsc.svn\examples\javascript\LINQ\test\TestSelectAboveAverage\TestSelectAboveAverage\ApplicationWebService.cs
                                                     var __Select_source_NewExpression = arg0ElementsBySelect.Arguments[0] as NewExpression;
                                                     if (__Select_source_NewExpression != null)
                                                     {
                                                         // do we have enough information to perfrm sql rendering?
                                                         // Constructor = {Void .ctor(System.String)}

                                                         // is it really our own table, jsc data layer? :P are they in the same database as current source?

                                                         //var xTable_datasource = (string)(__Where_source.Arguments[0] as ConstantExpression).Value;
                                                         var xTable = (IQueryStrategy)__Select_source_NewExpression.Constructor.Invoke(
                                                             parameters: null
                                                         );

                                                         //var xTable_Where = doWhere(xTable);
                                                         //var xTable_Where_OrderByDescending = doOrderBy(xTable_Where);
                                                         var xTable_Where_Select = doSelect(xTable);


                                                         return xTable_Where_Select;
                                                     }
                                                 }
                                                 #endregion
                                             }
                                             else if (arg0ElementsBySelect.Method.Name == refWhere.Name)
                                             {
                                                 #region doWhere
                                                 Func<IQueryStrategy, IQueryStrategy> doWhere =
                                                    xTable =>
                                                                {
                                                                    // Operand = {kk => (kk.duration == 46)}
                                                                    var __Where_filter = arg0ElementsBySelect.Arguments[1] as UnaryExpression;

                                                                    // X:\jsc.svn\core\ScriptCoreLib.Extensions\ScriptCoreLib.Extensions\Query\QueryStrategyOfTRowExtensions.Where.cs
                                                                    var xTable_Where = (IQueryStrategy)arg0ElementsBySelect.Method.Invoke(null,
                                                                         parameters: new object[] { xTable, __Where_filter.Operand }
                                                                     );

                                                                    return xTable_Where;
                                                                };
                                                 #endregion

                                                 // from
                                                 var __Where_source = arg0ElementsBySelect.Arguments[0] as NewExpression;
                                                 if (__Where_source != null)
                                                 {
                                                     // do we have enough information to perfrm sql rendering?
                                                     // Constructor = {Void .ctor(System.String)}

                                                     // is it really our own table, jsc data layer? :P are they in the same database as current source?

                                                     //var xTable_datasource = (string)(__Where_source.Arguments[0] as ConstantExpression).Value;
                                                     var xTable = (IQueryStrategy)__Where_source.Constructor.Invoke(
                                                         parameters: null
                                                     );

                                                     var xTable_Where = doWhere(xTable);
                                                     //var xTable_Where_OrderByDescending = doOrderBy(xTable_Where);
                                                     //var xTable_Where_Select = doSelect(xTable_Where);


                                                     return xTable_Where;
                                                 }
                                             }

                                             Debugger.Break();
                                             return null;
                                         };
                                     #endregion

                                     #region count(*) special!
                                     if (asMethodCallExpression.Method.Name == refCount.Name)
                                     {
                                         // X:\jsc.svn\examples\javascript\LINQ\test\TestGroupByThenOrderByThenOrderBy\TestGroupByThenOrderByThenOrderBy\ApplicationWebService.cs

                                         // arg0ElementsBySelect = {new ApplicationResourcePerformance().Where(kk => (Convert(kk.ApplicationPerformance) == Convert(k.Key)))}

                                         var arg0Elements_MemberExpression = asMethodCallExpression.Arguments[0] as MemberExpression;
                                         if (arg0Elements_MemberExpression != null)
                                         {
                                             // X:\jsc.svn\examples\javascript\LINQ\test\TestGroupByMultipleFields\TestGroupByMultipleFields\ApplicationWebService.cs

                                             var arg0Elements_MParameterExpression = arg0Elements_MemberExpression.Expression as ParameterExpression;


                                             s_SelectCommand += ",\n" + CommentLineNumber() + "\t"
                                               + arg0Elements_MParameterExpression.Name.Replace("<>", "__")
                                                + ".`" + asMemberAssignment.Member.Name
                                                + "` as `" + asMemberAssignment.Member.Name + "`";

                                             return;
                                         }

                                         var arg0Elements_ParameterExpression = asMethodCallExpression.Arguments[0] as ParameterExpression;
                                         if (arg0Elements_ParameterExpression != null)
                                         {
                                             s_SelectCommand += ",\n" + CommentLineNumber() + "\t"
                                                + arg0Elements_ParameterExpression.Name + ". `" + asMemberAssignment.Member.Name + "` as `" + asMemberAssignment.Member.Name + "`";

                                             return;
                                         }

                                         var arg0ElementsBySelect = asMethodCallExpression.Arguments[0] as MethodCallExpression;
                                         if (arg0ElementsBySelect != null)
                                         {
                                             var xTable_Where_Select0 = subquery(arg0ElementsBySelect);
                                             var xTable_Where_Select = xTable_Where_Select0 as ISelectQueryStrategy;

                                             xTable_Where_Select.scalarAggregateOperand = "count";

                                             #region s_SelectCommand
                                             var xSelectScalar = QueryStrategyExtensions.AsCommandBuilder(xTable_Where_Select0);
                                             var scalarsubquery = xSelectScalar.ToString();

                                             // http://blog.tanelpoder.com/2013/08/22/scalar-subqueries-in-oracle-sql-where-clauses-and-a-little-bit-of-exadata-stuff-too/

                                             // do we have to 
                                             // we dont know yet how to get sql of that thing do we
                                             s_SelectCommand += ",\n\t (\n\t" + scalarsubquery.Replace("\n", "\n\t") + ") as `" + asMemberAssignment.Member.Name + "`";


                                             state.ApplyParameter.AddRange(xSelectScalar.ApplyParameter);
                                             #endregion
                                             return;
                                         }
                                     }
                                     #endregion

                                     #region  sum( special!!
                                     if (asMethodCallExpression.Method.Name.TakeUntilIfAny("_") == "Sum")
                                     {
                                         var arg1 = (asMethodCallExpression.Arguments[1] as UnaryExpression).Operand as LambdaExpression;
                                         if (arg1 != null)
                                         {
                                             var asMemberExpression = arg1.Body as MemberExpression;
                                             s_SelectCommand += ",\n\t "
                                                + that.selector.Parameters[0].Name.Replace("<>", "__")
                                                + ".`" + asMemberAssignment.Member.Name + "` as `" + GetPrefixedTargetName() + "`";
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
                                             s_SelectCommand += ",\n\t "
                                                 + that.selector.Parameters[0].Name.Replace("<>", "__")
                                                 + ".`" + asMemberAssignment.Member.Name + "` as `" + GetPrefixedTargetName() + "`";
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
                                             s_SelectCommand += ",\n\t "
                                                 + that.selector.Parameters[0].Name.Replace("<>", "__")
                                                 + ".`" + asMemberAssignment.Member.Name + "` as `" + GetPrefixedTargetName() + "`";
                                             return;
                                         }
                                     }
                                     #endregion



                                     #region  avg( special!!
                                     if (asMethodCallExpression.Method.Name == refAverage.Name)
                                     {
                                         // X:\jsc.svn\examples\javascript\LINQ\MinMaxAverageExperiment\MinMaxAverageExperiment\ApplicationWebService.cs
                                         // X:\jsc.svn\examples\javascript\linq\test\TestSelectScalarAverage\TestSelectScalarAverage\ApplicationWebService.cs

                                         if (asMethodCallExpression.Arguments.Count == 2)
                                         {
                                             var arg1 = (asMethodCallExpression.Arguments[1] as UnaryExpression).Operand as LambdaExpression;
                                             if (arg1 != null)
                                             {
                                                 var asMemberExpression = arg1.Body as MemberExpression;
                                                 s_SelectCommand += ",\n\t "
                                                      + that.selector.Parameters[0].Name.Replace("<>", "__")
                                                      + ".`" + asMemberAssignment.Member.Name + "` as `" + GetPrefixedTargetName() + "`";
                                                 return;
                                             }
                                         }

                                         var arg0ElementsBySelect = asMethodCallExpression.Arguments[0] as MethodCallExpression;
                                         if (arg0ElementsBySelect != null)
                                         {
                                             var xTable_Where_Select0 = subquery(arg0ElementsBySelect);
                                             var xTable_Where_Select = xTable_Where_Select0 as ISelectQueryStrategy;

                                             xTable_Where_Select.scalarAggregateOperand = "avg";

                                             #region s_SelectCommand
                                             var xSelectScalar = QueryStrategyExtensions.AsCommandBuilder(xTable_Where_Select0);
                                             var scalarsubquery = xSelectScalar.ToString();

                                             // http://blog.tanelpoder.com/2013/08/22/scalar-subqueries-in-oracle-sql-where-clauses-and-a-little-bit-of-exadata-stuff-too/

                                             // do we have to 
                                             // we dont know yet how to get sql of that thing do we
                                             s_SelectCommand += ",\n\t (\n\t" + scalarsubquery.Replace("\n", "\n\t") + ") as `" + asMemberAssignment.Member.Name + "`";


                                             state.ApplyParameter.AddRange(xSelectScalar.ApplyParameter);
                                             #endregion
                                             return;
                                         }
                                     }
                                     #endregion


                                     #region FirstOrDefault
                                     // https://www.youtube.com/watch?v=pt8VYOfr8To
                                     if (asMethodCallExpression.Method.Name == refFirstOrDefault.Name)
                                     {
                                         // X:\jsc.svn\examples\javascript\LINQ\test\TestGroupByThenOrderByThenOrderBy\TestGroupByThenOrderByThenOrderBy\ApplicationWebService.cs

                                         // x:\jsc.svn\examples\javascript\linq\test\testselectandsubselect\testselectandsubselect\applicationwebservice.cs
                                         // X:\jsc.svn\examples\javascript\LINQ\test\TestSelectOfSelect\TestSelectOfSelect\ApplicationWebService.cs
                                         // can we ask for sql?


                                         // [0] = {Invoke(value(TestSelectAndSubSelect.ApplicationWebService+<>c__DisplayClass0).child1, Convert(<>h__TransparentIdentifier1.<>h__TransparentIdentifier0.x))}

                                         // +		(new System.Collections.Generic.Mscorlib_CollectionDebugView<System.Linq.Expressions.Expression>((new System.Linq.Expressions.Expression.MethodCallExpressionProxy(asMethodCallExpression as System.Linq.Expressions.MethodCallExpressionN)).Arguments as System.Runtime.CompilerServices.TrueReadOnlyCollection<System.Linq.Expressions.Expression>)).Items[0]	{Invoke(value(TestSelectAndSubSelect.ApplicationWebService+<>c__DisplayClass0).child1, <>h__TransparentIdentifier1.<>h__TransparentIdentifier0.x)}	System.Linq.Expressions.Expression {System.Linq.Expressions.InvocationExpression}
                                         var asMInvocationExpression = asMethodCallExpression.Arguments[0] as InvocationExpression;
                                         if (asMInvocationExpression != null)
                                         {
                                             // just mark the inputs to be used in select.
                                             // X:\jsc.svn\core\ScriptCoreLib.Extensions\ScriptCoreLib.Extensions\Query\QueryStrategyOfTRowExtensions.AsGenericEnumerable.cs
                                             // 554

                                             return;
                                         }


                                         var arg0ElementsBySelect = asMethodCallExpression.Arguments[0] as MethodCallExpression;
                                         if (arg0ElementsBySelect != null)
                                         {
                                             var xTable_Where_Select = subquery(arg0ElementsBySelect);

                                             #region s_SelectCommand
                                             var xSelectScalar = QueryStrategyExtensions.AsCommandBuilder(xTable_Where_Select);
                                             var scalarsubquery = xSelectScalar.ToString();

                                             // http://blog.tanelpoder.com/2013/08/22/scalar-subqueries-in-oracle-sql-where-clauses-and-a-little-bit-of-exadata-stuff-too/

                                             // do we have to 
                                             // we dont know yet how to get sql of that thing do we
                                             s_SelectCommand += ",\n\t (\n\t" + scalarsubquery.Replace("\n", "\n\t") + ") as `" + asMemberAssignment.Member.Name + "`";


                                             state.ApplyParameter.AddRange(xSelectScalar.ApplyParameter);
                                             #endregion

                                             return;

                                         }
                                     }
                                     #endregion

                                     Debugger.Break();
                                 }

                                 Console.WriteLine(new { index, asMethodCallExpression.Method });



                                 Debugger.Break();
                             }
                             #endregion



                             #region WriteExpression:asMemberExpression
                             {
                                 // m_getterMethod = {TestSQLiteGroupBy.Data.GooStateEnum get_Key()}

                                 var asMemberExpression = asMemberAssignment.Expression as MemberExpression;
                                 Console.WriteLine(new { index, asMemberExpression });
                                 if (asMemberExpression != null)
                                 {
                                     // X:\jsc.svn\examples\javascript\LINQ\test\SelectToUpperIntoNewExpression\SelectToUpperIntoNewExpression\ApplicationWebService.cs
                                     WriteMemberExpression(index, asMemberExpression, TargetMember, prefixes, null);
                                     return;
                                 }
                             }
                             #endregion

                             #region  asMemberAssignment.Expression = {Convert(GroupByGoo.First())}
                             var asUnaryExpression = asMemberAssignment.Expression as UnaryExpression;

                             Console.WriteLine(new { index, asUnaryExpression });

                             if (asUnaryExpression != null)
                             {
                                 // x:\jsc.svn\examples\javascript\linq\test\vb\testselectintoxelementwithattribute\testselectintoxelementwithattribute\applicationwebservice.vb

                                 WriteExpression(index, asUnaryExpression.Operand, TargetMember, prefixes, valueSelector);
                                 return
                                 ;

                             }
                             #endregion


                             #region WriteExpression:asEParameterExpression
                             var asEParameterExpression = asExpression as ParameterExpression;
                             if (asEParameterExpression != null)
                             {
                                 // used in select
                                 // using the let keyword?

                                 // x:\jsc.svn\examples\javascript\linq\test\testjoinselectanonymoustype\testjoinselectanonymoustype\applicationwebservice.cs

                                 if (asEParameterExpression == that.selector.Parameters[0])
                                 {
                                     Action<ISelectQueryStrategy> selectProjectionWalker = null;

                                     selectProjectionWalker =
                                        yy =>
                                        {
                                            // X:\jsc.svn\examples\javascript\linq\test\TestSelectAndSubSelect\TestSelectAndSubSelect\ApplicationWebService.cs
                                            if (yy == null)
                                                return;
                                            #region  // go up
                                            {

                                                INestedQueryStrategy uu = that;

                                                while (uu != null)
                                                {
                                                    #region asSelectQueryStrategy
                                                    var asSelectQueryStrategy = uu as ISelectQueryStrategy;
                                                    if (asSelectQueryStrategy != null)
                                                    {
                                                        var xasLambdaExpression = asSelectQueryStrategy.selectorExpression as LambdaExpression;
                                                        var xasNewExpression = xasLambdaExpression.Body as NewExpression;

                                                        foreach (var item in xasNewExpression.Arguments)
                                                        {
                                                            // Expression = {<> h__TransparentIdentifier5.<> h__TransparentIdentifier4.<> h__TransparentIdentifier3.<> h__TransparentIdentifier2.<> h__TransparentIdentifier1.<> h__TransparentIdentifier0
                                                            // .u0}

                                                            // item = {new <>f__AnonymousType4`2(connectStart = <>h__TransparentIdentifier2.<>h__TransparentIdentifier1.<>h__TransparentIdentifier0.x.connectStart, connectEnd = <>h__TransparentIdentifier2.<>h__TransparentIdentifier1.<>h__TransparentIdentifier0.x.connectEnd)}
                                                            var yasNewExpression = item as NewExpression;
                                                            if (yasNewExpression != null)
                                                            {
                                                                yasNewExpression.Arguments.WithEachIndex(
                                                                    (ySourceArgument, yindex) =>
                                                                {
                                                                    var yasSMemberExpression = ySourceArgument as MemberExpression;
                                                                    if (yasSMemberExpression != null)
                                                                    {
                                                                        var yasSMMemberExpression = yasSMemberExpression.Expression as MemberExpression;
                                                                        if (yasSMMemberExpression != null)
                                                                        {
                                                                            if (yasSMMemberExpression.Member.Name == (yy.selectorExpression as LambdaExpression).Parameters[0].Name)
                                                                            {

                                                                                s_SelectCommand += ",\n\t "
                                                                                + asMemberAssignment.Member.Name.Replace("<>", "__")
                                                                                + "."
                                                                                    //+ xasMMemberExpression.Member.Name + "_" 
                                                                                    + yasSMemberExpression.Member.Name + " as `"
                                                                                    //+ xasMMemberExpression.Member.Name + "_" 
                                                                                    + yasSMemberExpression.Member.Name + "`";
                                                                            }
                                                                        }
                                                                    }
                                                                }
                                                                );

                                                            }


                                                            // X:\jsc.svn\examples\javascript\LINQ\test\TestSelectMath\TestSelectMath\ApplicationWebService.cs
                                                            var xxBinaryExpression = item as BinaryExpression;
                                                            if (xxBinaryExpression != null)
                                                            {
                                                                {
                                                                    var zxasMemberExpression = xxBinaryExpression.Left as MemberExpression;
                                                                    if (zxasMemberExpression != null)
                                                                    {
                                                                        var xasMMemberExpression = zxasMemberExpression.Expression as ParameterExpression;
                                                                        if (xasMMemberExpression != null)
                                                                        {

                                                                            if (xasMMemberExpression.Name == (yy.selectorExpression as LambdaExpression).Parameters[0].Name)
                                                                            {
                                                                                s_SelectCommand += ",\n" + CommentLineNumber() + "\t "
                                                                                    + asMemberAssignment.Member.Name.Replace("<>", "__")
                                                                                    + ".`"
                                                                            //+ xasMMemberExpression.Member.Name + "_" 
                                                                            + zxasMemberExpression.Member.Name + "` as `"
                                                                            //+ xasMMemberExpression.Member.Name + "_" 
                                                                            + zxasMemberExpression.Member.Name + "`";

                                                                            }
                                                                        }
                                                                    }
                                                                }

                                                                {
                                                                    var zxasMemberExpression = xxBinaryExpression.Right as MemberExpression;
                                                                    if (zxasMemberExpression != null)
                                                                    {
                                                                        var xasMMemberExpression = zxasMemberExpression.Expression as ParameterExpression;
                                                                        if (xasMMemberExpression != null)
                                                                        {

                                                                            if (xasMMemberExpression.Name == (yy.selectorExpression as LambdaExpression).Parameters[0].Name)
                                                                            {
                                                                                s_SelectCommand += ",\n" + CommentLineNumber() + "\t "
                                                                                    + asMemberAssignment.Member.Name.Replace("<>", "__")
                                                                                    + ".`"
                                                                            //+ xasMMemberExpression.Member.Name + "_" 
                                                                            + zxasMemberExpression.Member.Name + "` as `"
                                                                            //+ xasMMemberExpression.Member.Name + "_" 
                                                                            + zxasMemberExpression.Member.Name + "`";

                                                                            }
                                                                        }
                                                                    }
                                                                }
                                                            }



                                                            #region xasMemberExpression
                                                            var xasMemberExpression = item as MemberExpression;
                                                            if (xasMemberExpression != null)
                                                            {
                                                                var xasMMemberExpression = xasMemberExpression.Expression as MemberExpression;
                                                                if (xasMMemberExpression != null)
                                                                {
                                                                    if (xasMMemberExpression.Member.Name == (yy.selectorExpression as LambdaExpression).Parameters[0].Name)
                                                                    {
                                                                        s_SelectCommand += ",\n\t "
                                                                            + asMemberAssignment.Member.Name.Replace("<>", "__")
                                                                            + "."
                                                                            //+ xasMMemberExpression.Member.Name + "_" 
                                                                            + xasMemberExpression.Member.Name + " as `"
                                                                            //+ xasMMemberExpression.Member.Name + "_" 
                                                                            + xasMemberExpression.Member.Name + "`";

                                                                    }
                                                                }
                                                            }
                                                            #endregion
                                                        }
                                                    }
                                                    #endregion

                                                    if (uu.upperSelect != null)
                                                        uu = uu.upperSelect;
                                                    else if (uu.upperJoin != null)
                                                        uu = uu.upperJoin;
                                                    else if (uu.upperGroupBy != null)
                                                        uu = uu.upperGroupBy;
                                                    else
                                                        break;
                                                }
                                            }
                                            #endregion

                                            // deeper?
                                            selectProjectionWalker(yy.source as ISelectQueryStrategy);
                                        };

                                     selectProjectionWalker(that);

                                     #region projectionWalker
                                     Action<IJoinQueryStrategy> projectionWalker = null;

                                     projectionWalker =
                                         yy =>
                                         {
                                             if (yy == null)
                                                 return;

                                             // show the inherited fields
                                             //s_SelectCommand += ",\n\t-- 0   " + (yy.selectorExpression as LambdaExpression).Parameters[0].Name;
                                             #region  // go up
                                             {

                                                 INestedQueryStrategy uu = that;

                                                 while (uu != null)
                                                 {
                                                     #region asSelectQueryStrategy
                                                     var asSelectQueryStrategy = uu as ISelectQueryStrategy;
                                                     if (asSelectQueryStrategy != null)
                                                     {
                                                         var xasLambdaExpression = asSelectQueryStrategy.selectorExpression as LambdaExpression;
                                                         var xasNewExpression = xasLambdaExpression.Body as NewExpression;

                                                         foreach (var item in xasNewExpression.Arguments)
                                                         {
                                                             // Expression = {<> h__TransparentIdentifier5.<> h__TransparentIdentifier4.<> h__TransparentIdentifier3.<> h__TransparentIdentifier2.<> h__TransparentIdentifier1.<> h__TransparentIdentifier0
                                                             // .u0}

                                                             var xasMemberExpression = item as MemberExpression;
                                                             if (xasMemberExpression != null)
                                                             {
                                                                 var xasMMemberExpression = xasMemberExpression.Expression as MemberExpression;
                                                                 if (xasMMemberExpression != null)
                                                                 {
                                                                     if (xasMMemberExpression.Member.Name == (yy.selectorExpression as LambdaExpression).Parameters[0].Name)
                                                                     {
                                                                         s_SelectCommand += ",\n\t " + asMemberAssignment.Member.Name.Replace("<>", "__") + "." + xasMMemberExpression.Member.Name + "_" + xasMemberExpression.Member.Name + " as `" + xasMMemberExpression.Member.Name + "_" + xasMemberExpression.Member.Name + "`";

                                                                     }
                                                                 }
                                                             }
                                                         }
                                                     }
                                                     #endregion

                                                     if (uu.upperSelect != null)
                                                         uu = uu.upperSelect;
                                                     else if (uu.upperJoin != null)
                                                         uu = uu.upperJoin;
                                                     else if (uu.upperGroupBy != null)
                                                         uu = uu.upperGroupBy;
                                                     else
                                                         break;
                                                 }
                                             }
                                             #endregion


                                             //s_SelectCommand += ",\n\t--  1  " + (yy.selectorExpression as LambdaExpression).Parameters[1].Name;
                                             #region  // go up
                                             {

                                                 INestedQueryStrategy uu = that;

                                                 while (uu != null)
                                                 {
                                                     #region asSelectQueryStrategy
                                                     var asSelectQueryStrategy = uu as ISelectQueryStrategy;
                                                     if (asSelectQueryStrategy != null)
                                                     {
                                                         var xasLambdaExpression = asSelectQueryStrategy.selectorExpression as LambdaExpression;
                                                         var xasNewExpression = xasLambdaExpression.Body as NewExpression;

                                                         foreach (var item in xasNewExpression.Arguments)
                                                         {
                                                             // Expression = {<> h__TransparentIdentifier5.<> h__TransparentIdentifier4.<> h__TransparentIdentifier3.<> h__TransparentIdentifier2.<> h__TransparentIdentifier1.<> h__TransparentIdentifier0
                                                             // .u0}

                                                             var xasMemberExpression = item as MemberExpression;
                                                             if (xasMemberExpression != null)
                                                             {
                                                                 var xasMMemberExpression = xasMemberExpression.Expression as MemberExpression;
                                                                 if (xasMMemberExpression != null)
                                                                 {
                                                                     if (xasMMemberExpression.Member.Name == (yy.selectorExpression as LambdaExpression).Parameters[1].Name)
                                                                     {
                                                                         s_SelectCommand += ",\n\t " + asMemberAssignment.Member.Name.Replace("<>", "__") + "." + xasMMemberExpression.Member.Name + "_" + xasMemberExpression.Member.Name + " as `" + xasMMemberExpression.Member.Name + "_" + xasMemberExpression.Member.Name + "`";

                                                                     }
                                                                 }
                                                             }
                                                         }
                                                     }
                                                     #endregion

                                                     if (uu.upperSelect != null)
                                                         uu = uu.upperSelect;
                                                     else if (uu.upperJoin != null)
                                                         uu = uu.upperJoin;
                                                     else if (uu.upperGroupBy != null)
                                                         uu = uu.upperGroupBy;
                                                     else
                                                         break;
                                                 }
                                             }
                                             #endregion


                                             projectionWalker(yy.xouter as IJoinQueryStrategy);
                                             projectionWalker(yy.xinner as IJoinQueryStrategy);
                                         };
                                     #endregion

                                     projectionWalker(that.source as IJoinQueryStrategy);

                                     return;

                                 }
                             }
                             #endregion

                             #region WriteExpression:asNewArrayExpression
                             var asNewArrayExpression = asExpression as NewArrayExpression;
                             if (asNewArrayExpression != null)
                             {
                                 asNewArrayExpression.Expressions.WithEachIndex(
                                (SourceArgument, i) =>
                                    {

                                        // Constructor = {Void .ctor(System.Xml.Linq.XName, System.Object)}
                                        var SourceMember = default(MemberInfo);



                                        // c# extension operators for enumerables, thanks
                                        WriteExpression(i, SourceArgument, SourceMember, prefixes.Concat(new[] { Tuple.Create(index, TargetMember) }).ToArray(), null);
                                    }
                             );
                                 return;
                             }
                             #endregion


                             // asExpression = {Invoke(value(SelectToUpperIntoNewExpression.ApplicationWebService+<>c__DisplayClass0).Special, ss.Tag)}

                             #region WriteExpression:asInvocationExpression
                             var asInvocationExpression = asExpression as InvocationExpression;
                             if (asInvocationExpression != null)
                             {
                                 asInvocationExpression.Arguments.WithEachIndex(
                                    (SourceArgument, i) =>
                                    {

                                        // Constructor = {Void .ctor(System.Xml.Linq.XName, System.Object)}
                                        var SourceMember = default(MemberInfo);


                                        // c# extension operators for enumerables, thanks
                                        WriteExpression(i, SourceArgument, SourceMember, prefixes.Concat(new[] { Tuple.Create(index, TargetMember) }).ToArray(), null);
                                    }
                                 );

                                 return;
                             }
                             #endregion

                             #region WriteExpression:asNewExpression
                             var asNewExpression = asExpression as NewExpression;
                             if (asNewExpression != null)
                             {
                                 // pre select level 1 fields?
                                 // X:\jsc.svn\examples\javascript\LINQ\test\SelectToUpperIntoNewExpression\SelectToUpperIntoNewExpression\ApplicationWebService.cs

                                 asNewExpression.Arguments.WithEachIndex(
                                    (SourceArgument, i) =>
                                    {
                                        // Constructor = {Void .ctor(System.Xml.Linq.XName, System.Object)}
                                        var SourceMember = default(MemberInfo);
                                        if (asNewExpression.Members != null)
                                            SourceMember = asNewExpression.Members[i];
                                        // c# extension operators for enumerables, thanks
                                        WriteExpression(i, SourceArgument, SourceMember, prefixes.Concat(new[] { Tuple.Create(index, TargetMember) }).ToArray(), null);
                                    }
                                 );
                                 return;
                             }
                             #endregion


                             // roslyn allows dictionary indexer intit
                             #region WriteExpression:asEMemberInitExpression
                             var asEMemberInitExpression = asExpression as MemberInitExpression;
                             if (asEMemberInitExpression != null)
                             {
                                 var asEMNewExpression = asEMemberInitExpression.NewExpression;

                                 asEMNewExpression.Arguments.WithEachIndex(
                                    (SourceArgument, i) =>
                                    {

                                        // Constructor = {Void .ctor(System.Xml.Linq.XName, System.Object)}
                                        var SourceMember = default(MemberInfo);

                                        if (asEMNewExpression.Members != null)
                                            SourceMember = asEMNewExpression.Members[i];

                                        // c# extension operators for enumerables, thanks
                                        WriteExpression(i, SourceArgument, SourceMember, prefixes.Concat(new[] { Tuple.Create(index, TargetMember) }).ToArray(), null);
                                    }
                                 );

                                 asEMemberInitExpression.Bindings.WithEachIndex(
                                    (SourceBinding, i) =>
                                    {
                                        // roslyn seems to be missing the indexer arguments!
                                        Debugger.Break();

                                        //WriteExpression(i, SourceArgument, SourceMember, prefixes.Concat(new[] { Tuple.Create(index, TargetMember) }).ToArray(), null);
                                    }
                                 );

                                 return;
                             }
                             #endregion


                             var xBinaryExpression = asExpression as BinaryExpression;
                             if (xBinaryExpression != null)
                             {
                                 // X:\jsc.svn\examples\javascript\linq\test\TestSelectMath\TestSelectMath\ApplicationWebService.cs

                                 s_SelectCommand += ",\n" + CommentLineNumber() + "\t";
                                 s_SelectCommand += "( ";

                                 WriteExpression(-1, xBinaryExpression.Left, null, prefixes, null);

                                 if (xBinaryExpression.NodeType == ExpressionType.Add)
                                     s_SelectCommand += " + ";
                                 else if (xBinaryExpression.NodeType == ExpressionType.Multiply)
                                     s_SelectCommand += " * ";
                                 else if (xBinaryExpression.NodeType == ExpressionType.Divide)
                                     s_SelectCommand += " / ";

                                 else
                                     Debugger.Break();


                                 WriteExpression(-1, xBinaryExpression.Right, null, prefixes, null);

                                 s_SelectCommand += ")";
                                 s_SelectCommand += " as `" + TargetMember.Name + "`";
                                 return;
                             }

                             Debugger.Break();
                         };
                     #endregion




                     // asMemberInitExpression should mean select into row specific values?
                     if (asMemberInitExpression != null)
                     {
                         // ??
                         // jsc, can you think of a test for this situation?
                         // X:\jsc.svn\examples\javascript\LINQ\test\TestSelectIntoMemberInitExpression\TestSelectIntoMemberInitExpression\ApplicationWebService.cs
                         // asMemberInitExpression = {new PerformanceResourceTimingData2ApplicationResourcePerformanceRow() {duration = k.duration, path = k.path}}
                         // X:\jsc.svn\examples\javascript\linq\test\TestSelectIntoViewRow\TestSelectIntoViewRow\ApplicationWebService.cs

                         if (asMemberInitExpression.Type == null)
                             throw new InvalidOperationException("asMemberInitExpression.Type == null");


                         asMemberInitExpression.Bindings.WithEachIndex(
                             (SourceBinding, i) =>
                            {
                                var asMemberAssignment = SourceBinding as MemberAssignment;

                                Console.WriteLine(new { asMemberAssignment });
                                // SourceBinding = {Content = <>h__TransparentIdentifier1.<>h__TransparentIdentifier0.UpdatesByMiddlesheet.Last().UpdatedContent}

                                WriteExpression(i, asMemberAssignment.Expression, asMemberAssignment.Member,
                                    new Tuple<int, MemberInfo>[0], null);

                            }
                         );
                         SelectCommand = s_SelectCommand;

                         var FromCommand =
                             "from "
                                 + s.GetQualifiedTableNameOrToString().Replace("\n", "\n\t")
                                 + " as " + xouter_Paramerer.Name.Replace("<>", "__");

                         state.FromCommand = FromCommand;
                     }
                     else
                     {
                         // when does this happen?

                         //SelectCommand = "select 0 as foo";


                         // NewExpression shuld mean new { x, y }


                         var asLMethodCallExpression = asLambdaExpression.Body as MethodCallExpression;
                         if (asLMethodCallExpression != null)
                         {
                             // X:\jsc.svn\examples\javascript\linq\test\TestSelectToUpper\TestSelectToUpper\ApplicationWebService.cs

                             if (asLMethodCallExpression.Method.Name == "ToUpper")
                             {
                                 var asLMMemberExpression = asLMethodCallExpression.Object as MemberExpression;
                                 // X:\jsc.svn\core\ScriptCoreLib.Extensions\ScriptCoreLib.Extensions\Query\QueryStrategyOfTRowExtensions.AsGenericEnumerable.cs
                                 if (asLMMemberExpression != null)
                                 {
                                     WriteMemberExpression(0, asLMMemberExpression, asLMMemberExpression.Member, new Tuple<int, MemberInfo>[0], asLMethodCallExpression.Method);
                                     SelectCommand = s_SelectCommand;

                                     var FromCommand =
                                          "from "
                                              + s.GetQualifiedTableNameOrToString().Replace("\n", "\n\t")
                                          + " as " + xouter_Paramerer.Name.Replace("<>", "__");
                                     state.FromCommand = FromCommand;

                                 }
                             }
                             else
                                 Debugger.Break();
                         }
                         else
                         {
                             var asLMemberExpression = asLambdaExpression.Body as MemberExpression;
                             if (asLMemberExpression != null)
                             {
                                 // scalar?
                                 // X:\jsc.svn\examples\javascript\LINQ\test\TestSelectMember\TestSelectMember\ApplicationWebService.cs
                                 // Member = {System.String path}

                                 WriteMemberExpression(0, asLMemberExpression, asLMemberExpression.Member, new Tuple<int, MemberInfo>[0], null);
                                 SelectCommand = s_SelectCommand;

                                 var FromCommand =
                                      "from "
                                          + s.GetQualifiedTableNameOrToString().Replace("\n", "\n\t")
                                      + " as " + xouter_Paramerer.Name.Replace("<>", "__");
                                 state.FromCommand = FromCommand;
                             }
                             else
                             {
                                 // X:\jsc.svn\examples\javascript\linq\test\TestSelectIntoNewExpression\TestSelectIntoNewExpression\ApplicationWebService.cs
                                 var asLNewExpression = asLambdaExpression.Body as NewExpression;
                                 if (asLNewExpression != null)
                                 {
                                     #region asNewExpression
                                     asLNewExpression.Arguments.WithEachIndex(
                                         (SourceArgument, index) =>
                                            {
                                                //s_SelectCommand += "\n\t-- " + new { index, SourceArgument };

                                                // X:\jsc.svn\examples\javascript\LINQ\test\vb\TestSelectIntoXElementWithAttribute\TestSelectIntoXElementWithAttribute\ApplicationWebService.vb

                                                var TargetMember = default(MemberInfo);

                                                if (asLNewExpression.Members != null)
                                                {
                                                    TargetMember = asLNewExpression.Members[index];
                                                }


                                                WriteExpression(index, SourceArgument, TargetMember, new Tuple<int, MemberInfo>[0], null);
                                            }
                                     );
                                     #endregion

                                     // https://sites.google.com/a/jsc-solutions.net/backlog/knowledge-base/2014/201406/20140601/let






                                     SelectCommand = s_SelectCommand;

                                     var FromCommand =
                                       "from "
                                           + s.GetQualifiedTableNameOrToString().Replace("\n", "\n\t")
                                       + " as " + that.selector.Parameters[0].Name.Replace("<>", "__");

                                     state.FromCommand = FromCommand;

                                 }
                                 else
                                 {
                                     // what if we do select x?
                                     // X:\jsc.svn\examples\javascript\LINQ\test\TestSelect\TestSelect\ApplicationWebService.cs

                                     SelectCommand = s.SelectCommand;
                                     state.FromCommand = s.FromCommand;

                                     // um. what if we do a where on it?
                                 }
                             }
                         }
                     }


                     state.SelectCommand = SelectCommand;


                     //state.ApplyParameter.AddRange(s.ApplyParameter);

                     state.ApplyParameter.AddRange(s.ApplyParameter);


                     //asGroupByQueryStrategy.upperSelect = null;
                 }
            );


            return that;
        }









    }
}

