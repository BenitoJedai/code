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



            public ISelectManyQueryStrategy upperSelectMany { get; set; }
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
        public static readonly Func<string> CommentLineNumber =
            delegate
        {
            // what would happen id we did this elsewhere?
            var f = new StackTrace(fNeedFileInfo: true).GetFrame(1);

            // http://dev.mysql.com/doc/refman/5.0/en/comments.html
            return " /* " + f.GetFileName().SkipUntilLastOrEmpty("\\") + ":" + f.GetFileLineNumber().ToString().PadRight(5) + " */ ";
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
                     //"select /* diagnostics */ ";
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


                     Action<IQueryStrategy, int, Expression, MemberInfo, Tuple<int, MemberInfo>[], MethodInfo, Tuple<int, MemberInfo>[]> WriteExpression = null;

                     #region WriteMemberExpression
                     Action<IQueryStrategy, int, MemberExpression, MemberInfo, Tuple<int, MemberInfo>[], MethodInfo, Tuple<int, MemberInfo>[]> WriteMemberExpression =
                         (that_source, index, asMemberExpression, asMemberAssignment_Member, prefixes, valueSelector, sourceprefixes) =>
                         {
                             #region GetPrefixedSourceName
                             Func<string> GetPrefixedSourceName = delegate
                             {
                                 // x:\jsc.svn\examples\javascript\linq\test\testselectdatesthencountsimilars\testselectdatesthencountsimilars\applicationwebservice.cs

                                 var w = "";

                                 if (sourceprefixes != null)
                                     foreach (var item in sourceprefixes)
                                     {
                                         if (item.Item2 == null)
                                             w += item.Item1 + ".";
                                         else
                                             w += item.Item2.Name + ".";
                                     }

                                 // for primary constructors we know position.
                                 if (asMemberExpression == null)
                                     // ???
                                     w += index;
                                 else
                                     w += asMemberExpression.Member.Name;

                                 return w;
                             };
                             #endregion

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


                             #region [0] WriteMemberExpression: ParameterExpression
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
                                                    WriteExpression(that_source, i, SourceArgument, SourceMember, prefixes.Concat(new[] { Tuple.Create(index, asMemberExpression.Member) }).ToArray(), null, sourceprefixes);
                                                }
                                            );
                                             return;
                                         }

                                         WriteExpression(that_source,
                                             index, asSSLambdaExpression.Body, asMemberExpression.Member, prefixes, null, sourceprefixes);
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


                                 //s_SelectCommand += ",\n" + CommentLineNumber() + "\t"
                                 //+ that.selector.Parameters[0].Name.Replace("<>", "__")
                                 //+ ".`" + asMemberExpression.Member.Name + "` as `" + GetPrefixedTargetName() + "`";

                                 // x:\jsc.svn\examples\javascript\linq\test\testselectdatesthencountsimilars\testselectdatesthencountsimilars\applicationwebservice.cs
                                 s_SelectCommand += ",\n" + CommentLineNumber() + "\t"
                                 + that.selector.Parameters[0].Name.Replace("<>", "__")
                                 + ".`" + GetPrefixedSourceName() + "` as `" + GetPrefixedTargetName() + "`";

                                 return;
                             }
                             #endregion


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


                             #region WriteMemberExpression:asMConstantExpression
                             //         var SpecialConstant_u = "44";
                             var asMConstantExpression = asMemberExpression.Expression as ConstantExpression;
                             if (asMConstantExpression != null)
                             {
                                 s_SelectCommand += ",\n" + CommentLineNumber() + "\t ";

                                 state.WriteExpression(
                                     ref s_SelectCommand,
                                     asMemberExpression,
                                     that
                                 );

                                 s_SelectCommand += " as `" + GetPrefixedTargetName() + "`";

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

                                 #region DateTime::
                                 if (asMMemberExpression.Type == typeof(DateTime))
                                 {
                                     // https://sites.google.com/a/jsc-solutions.net/backlog/knowledge-base/2014/201406/20140608
                                     // are we at before, .Date or after?


                                     // X:\jsc.svn\examples\javascript\Test\TestDate\TestDate\Application.cs
                                     // X:\jsc.svn\examples\javascript\linq\test\TestSelectDate\TestSelectDate\ApplicationWebService.cs
                                     // how many milliseconds are there in a day?
                                     // dividide round and multiply to get the date ms?

                                     //var x = (1000 * 60 * 60 * 24);


                                     if (that.upperSelect == null)
                                     {
                                         // perform the Date Taking and realiasing at last step?

                                         if (index < 0)
                                         {
                                             // we are in a binary logic?


                                             s_SelectCommand +=
                                             "(" + that.selector.Parameters[0].Name.Replace("<>", "__")
                                                + ".`" + asMMemberExpression.Member.Name + "` / (1000 * 60 * 60 * 24) * (1000 * 60 * 60 * 24))";

                                         }
                                         else
                                         {

                                             s_SelectCommand += ",\n" + CommentLineNumber() + "\t("
                                                + that.selector.Parameters[0].Name.Replace("<>", "__")
                                                + ".`" + asMMemberExpression.Member.Name + "` / (1000 * 60 * 60 * 24) * (1000 * 60 * 60 * 24)) as `" + GetPrefixedTargetName() + "`";
                                         }
                                     }
                                     else
                                     {
                                         // you must be playing with let keywords!

                                         s_SelectCommand += ",\n" + CommentLineNumber() + "\t"
                                             + that.selector.Parameters[0].Name.Replace("<>", "__")
                                             + ".`" + asMMemberExpression.Member.Name + "`  as `" + asMMemberExpression.Member.Name + "`";
                                     }

                                     ////s_SelectCommand += ",\n" + CommentLineNumber() + "\t("
                                     ////   + that.selector.Parameters[0].Name.Replace("<>", "__")
                                     ////   + ".`" + GetPrefixedSourceName() + "` / (1000 * 60 * 60 * 24) * (1000 * 60 * 60 * 24)) as `" + GetPrefixedTargetName() + "`";


                                     return;
                                 }
                                 #endregion

                                 #region (index < 0)
                                 if (index < 0)
                                 {
                                     // X:\jsc.svn\examples\javascript\LINQ\test\TestSelectMath\TestSelectMath\ApplicationWebService.cs

                                     s_SelectCommand +=
                                    that.selector.Parameters[0].Name.Replace("<>", "__")
                                   + ".`" + asMemberExpression.Member.Name + "`";

                                     return;
                                 }
                                 #endregion

                                 // asMemberExpression = {<>h__TransparentIdentifier3.<>h__TransparentIdentifier2.<>h__TransparentIdentifier1.<>h__TransparentIdentifier0.g}
                                 // a g? 
                                 // need to look deeper to see where that g is coming from!




                                 // do we have a reason to walk up and see how the g was put together?

                                 // what if the expression is actually referencin another source?
                                 // we would not know how many times we need to go deeper then!

                                 // select the correct source reference to go down from. might be upper than current 
                                 // if we are hunting for proxy data groups


                                 #region flatten g
                                 var yy = that_source as ISelectQueryStrategy;
                                 var yym = asMemberExpression;

                                 while (yym != null)
                                 {
                                     // selectorExpression = {x => new <>f__AnonymousType0`2(x = x, g = new <>f__AnonymousType1`3(requestStart = x.requestStart, Tag = x.Tag, EventTime = x.EventTime))}

                                     // we are now looking one level deeper.
                                     // do we see our g being constructed yet?

                                     // shall we go any deeper or lok for our g in this level?
                                     if (yym.Expression is MemberExpression)
                                     {
                                         // we should only go deeper if it is referencing the deeper source
                                         // selectorExpression = {x => new <>f__AnonymousType0`2(x = x, g0 = new <>f__AnonymousType1`3(requestStart = x.requestStart, Tag = x.Tag, EventTime = x.EventTime))}
                                         // yym.Expression = {<>h__TransparentIdentifier0.x}

                                         yym = yym.Expression as MemberExpression;
                                         // go even deeper
                                         if (yy != null)
                                             yy = yy.source as ISelectQueryStrategy;
                                     }
                                     else
                                     {
                                         // we seem to be at the correct level.
                                         // the generated datasources might want to implcitly be ISelectQueryStrategy ?
                                         if (yy != null)
                                         {
                                             var yyLambdaExpression = yy.selectorExpression as LambdaExpression;

                                             // yyNewExpression = {new <>f__AnonymousType2`2(<>h__TransparentIdentifier0 = <>h__TransparentIdentifier0, xrequestStart = <>h__TransparentIdentifier0.x.requestStart)}
                                             // yyNewExpression = {new <>f__AnonymousType0`2(x = x, g = new <>f__AnonymousType1`3(requestStart = x.requestStart, Tag = x.Tag, EventTime = x.EventTime))}

                                             var yyNewExpression = yyLambdaExpression.Body as NewExpression;
                                             var yyi = yyNewExpression.Members.IndexOf(asMemberExpression.Member);

                                             if (yyi < 0)
                                             {
                                                 // not defined, skip it
                                             }
                                             else
                                             {

                                                 var yya = yyNewExpression.Arguments[yyi];

                                                 // if its not a group just write it?

                                                 WriteExpression(
                                                 // ?
                                                 that_source,

                                                     index, yya,
                                                     asMemberAssignment_Member,
                                                     prefixes,
                                                     null,
                                                     //new[] { Tuple.Create(index, asMemberAssignment_Member) }
                                                     null
                                                    );
                                                 return;
                                             }
                                         }

                                         // ?
                                         break;
                                     }


                                 }
                                 #endregion




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
                         (that_source, index, asExpression, TargetMember, prefixes, valueSelector, sourceprefixes) =>
                         {
                             #region [0] WriteExpression:asMemberExpression
                             {
                                 // m_getterMethod = {TestSQLiteGroupBy.Data.GooStateEnum get_Key()}

                                 var asMemberExpression = asExpression as MemberExpression;
                                 if (asMemberExpression != null)
                                 {
                                     // X:\jsc.svn\examples\javascript\LINQ\test\SelectToUpperIntoNewExpression\SelectToUpperIntoNewExpression\ApplicationWebService.cs
                                     WriteMemberExpression(that_source, index, asMemberExpression, TargetMember, prefixes, null, sourceprefixes);
                                     return;
                                 }
                             }
                             #endregion

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
                                     // X:\jsc.svn\examples\javascript\linq\test\TestSelectAndSubSelect\TestSelectAndSubSelect\ApplicationWebService.cs

                                     if (index >= 0)
                                         s_SelectCommand += ",\n" + CommentLineNumber() + "\t ";

                                     state.WriteExpression(
                                         ref s_SelectCommand,
                                         asMConstantExpression,
                                         that
                                     );

                                     if (index >= 0)
                                         s_SelectCommand += " as `" + GetPrefixedTargetName() + "`";


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

                                 #region DateTime::
                                 if (asMethodCallExpression.Method.DeclaringType == typeof(DateTime))
                                 {
                                     // x:\jsc.svn\examples\javascript\linq\test\testselectdatesthencountsimilars\testselectdatesthencountsimilars\applicationwebservice.cs


                                     // asMethodCallExpression.Method = {System.DateTime AddDays(Double)}

                                     s_SelectCommand += ",\n" + CommentLineNumber() + "\t (  ";

                                     // Object = {x.EventTime.Date}

                                     WriteExpression(
                                         that_source,
                                         -1,
                                         asMethodCallExpression.Object,
                                         asMemberAssignment.Member,
                                         prefixes,
                                         null,
                                         sourceprefixes
                                    );


                                     s_SelectCommand += " + (1000 * 60 * 60 * 24) * ";

                                     var arg0 = asMethodCallExpression.Arguments[0] as ConstantExpression;


                                     WriteExpression(
                                         that_source,
                                         -1,
                                         arg0,
                                         asMemberAssignment.Member,
                                         prefixes,
                                         null,
                                         sourceprefixes
                                     );



                                     s_SelectCommand += ") as `" + GetPrefixedTargetName() + "`";

                                     return;
                                 }
                                 #endregion


                                 #region XName::
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
                                 #endregion



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


                                             WriteMemberExpression(that_source, index, asMemberExpression, TargetMember, prefixes, asMethodCallExpression.Method, sourceprefixes);
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


                                 #region QueryStrategyOfTRowExtensions::
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

                                             // X:\jsc.svn\examples\javascript\linq\test\TestSelectDateGroups\TestSelectDateGroups\ApplicationWebService.cs

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

                                                         #region from new xTable
                                                         var __Where_source_NewExpression = __Select_source.Arguments[0] as NewExpression;
                                                         if (__Where_source_NewExpression != null)
                                                         {
                                                             // do we have enough information to perfrm sql rendering?
                                                             // Constructor = {Void .ctor(System.String)}

                                                             // is it really our own table, jsc data layer? :P are they in the same database as current source?

                                                             //var xTable_datasource = (string)(__Where_source.Arguments[0] as ConstantExpression).Value;
                                                             var xTable = (IQueryStrategy)__Where_source_NewExpression.Constructor.Invoke(
                                                                 parameters: null
                                                             );

                                                             var xTable_Where = doWhere(xTable);
                                                             //var xTable_Where_OrderByDescending = doOrderBy(xTable_Where);
                                                             var xTable_Where_Select = doSelect(xTable_Where);


                                                             return xTable_Where_Select;
                                                         }
                                                         else
                                                         {
                                                             var __Where_source_ParameterExpression = __Select_source.Arguments[0] as ParameterExpression;
                                                             if (__Where_source_ParameterExpression != null)
                                                             {
                                                                 var arg0 = that.selector.Parameters[0];
                                                                 if (arg0.Name == __Where_source_ParameterExpression.Name)
                                                                 {
                                                                     // looks like the sub query wants to select from our parameter.

                                                                     // what about mutability?
                                                                     var xTable_Where = doWhere(that.source);
                                                                     //var xTable_Where_OrderByDescending = doOrderBy(xTable_Where);
                                                                     var xTable_Where_Select = doSelect(xTable_Where);


                                                                     return xTable_Where_Select;
                                                                 }
                                                             }
                                                         }
                                                         #endregion
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
                                                     else
                                                     {
                                                         var __Where_source_ParameterExpression = arg0ElementsBySelect.Arguments[0] as ParameterExpression;
                                                         if (__Where_source_ParameterExpression != null)
                                                         {
                                                             var arg0 = that.selector.Parameters[0];
                                                             if (arg0.Name == __Where_source_ParameterExpression.Name)
                                                             {
                                                                 // looks like the sub query wants to select from our parameter.

                                                                 // what about mutability?
                                                                 //var xTable_Where_OrderByDescending = doOrderBy(xTable_Where);
                                                                 var xTable_Where_Select = doSelect(that.source);


                                                                 return xTable_Where_Select;
                                                             }
                                                         }
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

                                     //#region count(*) special!
                                     //if (asMethodCallExpression.Method.Name == refCount.Name)
                                     //{
                                     //    // X:\jsc.svn\examples\javascript\LINQ\test\TestGroupByThenOrderByThenOrderBy\TestGroupByThenOrderByThenOrderBy\ApplicationWebService.cs

                                     //    // arg0ElementsBySelect = {new ApplicationResourcePerformance().Where(kk => (Convert(kk.ApplicationPerformance) == Convert(k.Key)))}

                                     //    #region count: arg0Elements_MemberExpression
                                     //    var arg0Elements_MemberExpression = asMethodCallExpression.Arguments[0] as MemberExpression;
                                     //    if (arg0Elements_MemberExpression != null)
                                     //    {
                                     //        // X:\jsc.svn\examples\javascript\LINQ\test\TestGroupByMultipleFields\TestGroupByMultipleFields\ApplicationWebService.cs

                                     //        var arg0Elements_MParameterExpression = arg0Elements_MemberExpression.Expression as ParameterExpression;


                                     //        s_SelectCommand += ",\n" + CommentLineNumber() + "\t"
                                     //          + arg0Elements_MParameterExpression.Name.Replace("<>", "__")
                                     //           + ".`" + asMemberAssignment.Member.Name
                                     //           + "` as `" + asMemberAssignment.Member.Name + "`";

                                     //        return;
                                     //    }
                                     //    #endregion



                                     //    #region count: arg0Elements_ParameterExpression
                                     //    var arg0Elements_ParameterExpression = asMethodCallExpression.Arguments[0] as ParameterExpression;
                                     //    if (arg0Elements_ParameterExpression != null)
                                     //    {
                                     //        // X:\jsc.svn\examples\javascript\linq\test\TestGroupByCount\TestGroupByCount\ApplicationWebService.cs

                                     //        // does that count have a special where clause?
                                     //        //if (asMethodCallExpression.Arguments.Count > 1)
                                     //        //{
                                     //        //    // https://sites.google.com/a/jsc-solutions.net/backlog/knowledge-base/2014/201406/20140607
                                     //        //    // are we about to scalar select a count on source?
                                     //        //    if (that.selector.Parameters[0].Name == arg0Elements_ParameterExpression.Name)
                                     //        //    {
                                     //        //        //yes the its talking about source.
                                     //        //        // if where is mutable.
                                     //        //        //we need a copy of source, to add our where?


                                     //        //        // do we need to rewrite it as an inner join?
                                     //        //        // otherewise we will be selecting the first scalar only?

                                     //        //        var cstate = QueryStrategyExtensions.AsCommandBuilder(that.source);
                                     //        //        // override
                                     //        //        cstate.SelectCommand = "select " + CommentLineNumber() + "\t"
                                     //        //   + arg0Elements_ParameterExpression.Name + ". `" + asMemberAssignment.Member.Name + "` as `" + asMemberAssignment.Member.Name + "`";

                                     //        //        var csql = cstate.ToString();


                                     //        //        s_SelectCommand += ",\n" + CommentLineNumber() + "\t (" + csql.Replace("\n", "\n\t") + ") as `" + asMemberAssignment.Member.Name + "`";
                                     //        //        return;
                                     //        //    }
                                     //        //}

                                     //        s_SelectCommand += ",\n" + CommentLineNumber() + "\t"
                                     //           + arg0Elements_ParameterExpression.Name + ". `" + asMemberAssignment.Member.Name + "` as `" + asMemberAssignment.Member.Name + "`";

                                     //        return;
                                     //    }
                                     //    #endregion


                                     //    #region a0MethodCallExpression
                                     //    var a0MethodCallExpression = asMethodCallExpression.Arguments[0] as MethodCallExpression;
                                     //    if (a0MethodCallExpression != null)
                                     //    {
                                     //        if (source is IGroupByQueryStrategy)
                                     //        {
                                     //            // groups do theyr own counting

                                     //            s_SelectCommand += ",\n" + CommentLineNumber() + "\t"
                                     //                 //+ arg0Elements_ParameterExpression.Name
                                     //                 + that.selector.Parameters[0].Name.Replace("<>", "__")
                                     //                  + ". `" + asMemberAssignment.Member.Name + "` as `" + asMemberAssignment.Member.Name + "`";

                                     //            return;
                                     //        }

                                     //        // X:\jsc.svn\examples\javascript\LINQ\test\TestSelectDatesThenCountSimilars\TestSelectDatesThenCountSimilars\ApplicationWebService.cs



                                     //        //// X:\jsc.svn\examples\javascript\LINQ\test\TestSelectDateGroups\TestSelectDateGroups\ApplicationWebService.cs

                                     //        var xTable_Where_Select0 = subquery(a0MethodCallExpression);

                                     //        // xTable_Where_Select0 = {System.Data.QueryStrategyOfTRowExtensions.WhereQueryStrategy<TestSelectDatesThenCountSimilars.Data.PerformanceResourceTimingData2ApplicationPerformanceRow>}

                                     //        var xTable_Where_Select = xTable_Where_Select0 as ISelectQueryStrategy;

                                     //        xTable_Where_Select.scalarAggregateOperand = "count";

                                     //        #region s_SelectCommand
                                     //        var xSelectScalar = QueryStrategyExtensions.AsCommandBuilder(xTable_Where_Select0);
                                     //        var scalarsubquery = xSelectScalar.ToString();

                                     //        // http://blog.tanelpoder.com/2013/08/22/scalar-subqueries-in-oracle-sql-where-clauses-and-a-little-bit-of-exadata-stuff-too/

                                     //        // do we have to 
                                     //        // we dont know yet how to get sql of that thing do we
                                     //        s_SelectCommand += ",\n\t (\n\t" + scalarsubquery.Replace("\n", "\n\t") + ") as `" + asMemberAssignment.Member.Name + "`";


                                     //        state.ApplyParameter.AddRange(xSelectScalar.ApplyParameter);
                                     //        #endregion
                                     //        return;
                                     //    }
                                     //    #endregion

                                     //}
                                     //#endregion

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
                                 #endregion


                                 Console.WriteLine(new { index, asMethodCallExpression.Method });


                                 // asMethodCallExpression = {Range(0, 3).Select(u => new <>f__AnonymousType1`1(z = x.EventTime.Date.AddDays(Convert(-u))))}
                                 // not sure, are we supposed to do a serverside selectmany?

                                 Debugger.Break();
                             }
                             #endregion





                             #region  asMemberAssignment.Expression = {Convert(GroupByGoo.First())}
                             var asUnaryExpression = asMemberAssignment.Expression as UnaryExpression;

                             Console.WriteLine(new { index, asUnaryExpression });

                             if (asUnaryExpression != null)
                             {
                                 // x:\jsc.svn\examples\javascript\linq\test\vb\testselectintoxelementwithattribute\testselectintoxelementwithattribute\applicationwebservice.vb

                                 WriteExpression(that_source, index, asUnaryExpression.Operand, TargetMember, prefixes, valueSelector, sourceprefixes);
                                 return;

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


                                                            #region NewExpression
                                                            var yasNewExpression = item as NewExpression;
                                                            if (yasNewExpression != null)
                                                            {
                                                                yasNewExpression.Arguments.WithEachIndex(
                                                                    (ySourceArgument, yindex) =>
                                                                {
                                                                    var yasSMemberExpression = ySourceArgument as MemberExpression;
                                                                    if (yasSMemberExpression != null)
                                                                    {
                                                                        //var yParameterExpression = yasSMemberExpression.Expression as ParameterExpression;
                                                                        //if (yParameterExpression != null)
                                                                        //{
                                                                        //    s_SelectCommand += ",\n\t "
                                                                        //      + asMemberAssignment.Member.Name.Replace("<>", "__")
                                                                        //      + "."
                                                                        //            //+ xasMMemberExpression.Member.Name + "_" 
                                                                        //            + yasSMemberExpression.Member.Name + " as `"
                                                                        //            //+ xasMMemberExpression.Member.Name + "_" 
                                                                        //            + yasSMemberExpression.Member.Name + "`";
                                                                        //}

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
                                                            #endregion

                                                            #region BinaryExpression
                                                            // X:\jsc.svn\examples\javascript\LINQ\test\TestSelectMath\TestSelectMath\ApplicationWebService.cs
                                                            var xxBinaryExpression = item as BinaryExpression;
                                                            if (xxBinaryExpression != null)
                                                            {
                                                                #region Left
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
                                                                #endregion


                                                                #region Right
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
                                                                #endregion
                                                            }
                                                            #endregion


                                                            #region xasMemberExpression
                                                            var xasMemberExpression = item as MemberExpression;
                                                            if (xasMemberExpression != null)
                                                            {
                                                                var xasMMemberExpression = xasMemberExpression.Expression as MemberExpression;
                                                                if (xasMMemberExpression != null)
                                                                {
                                                                    if (xasMMemberExpression.Member.Name == (yy.selectorExpression as LambdaExpression).Parameters[0].Name)
                                                                    {
                                                                        // /* QueryStrategyOfTRowExtensions.Select.cs:1535 */      __h__TransparentIdentifier0.g as `g`,
                                                                        // yes we have figured ut we need to proxy data
                                                                        // but, its grouped in source?

                                                                        // xasMMemberExpression.Member.Name = "<>h__TransparentIdentifier0"
                                                                        // +		yy.selectorExpression as LambdaExpression	{<>h__TransparentIdentifier0 => new <>f__AnonymousType2`2(<>h__TransparentIdentifier0 = <>h__TransparentIdentifier0, xrequestStart = <>h__TransparentIdentifier0.x.requestStart)}	System.Linq.Expressions.LambdaExpression {System.Linq.Expressions.Expression<System.Func<<>f__AnonymousType0<TestSelectDatesThenCountSimilars.Data.PerformanceResourceTimingData2ApplicationPerformanceRow,<>f__AnonymousType1<long,string,System.DateTime>>,<>f__AnonymousType2<<>f__AnonymousType0<TestSelectDatesThenCountSimilars.Data.PerformanceResourceTimingData2ApplicationPerformanceRow,<>f__AnonymousType1<long,string,System.DateTime>>,long>>>}



                                                                        // we scanned to the dop and now understand this shall be passed, too


                                                                        WriteExpression(
                                                                            // allow it do go down the levels it needs to find the g
                                                                            asSelectQueryStrategy.source,
                                                                            // that_source,
                                                                            index,
                                                                            xasMemberExpression,
                                                                            xasMemberExpression.Member,
                                                                            prefixes,
                                                                            null,
                                                                            sourceprefixes
                                                                        );


                                                                        //s_SelectCommand += ",\n" + CommentLineNumber() + "\t "
                                                                        //    + asMemberAssignment.Member.Name.Replace("<>", "__")
                                                                        //    + "."
                                                                        //    //+ xasMMemberExpression.Member.Name + "_" 
                                                                        //    + xasMemberExpression.Member.Name + " as `"
                                                                        //    //+ xasMMemberExpression.Member.Name + "_" 
                                                                        //    + xasMemberExpression.Member.Name + "`";

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
                                            //s_SelectCommand += " -- call selectProjectionWalker source\n";
                                            selectProjectionWalker(yy.source as ISelectQueryStrategy);
                                        };


                                     //s_SelectCommand += " -- call selectProjectionWalker\n";
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
                                             #region  // go up 2
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
                                        WriteExpression(that_source, i, SourceArgument, SourceMember, prefixes.Concat(new[] { Tuple.Create(index, TargetMember) }).ToArray(), null, sourceprefixes);
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
                                        WriteExpression(that_source, i, SourceArgument, SourceMember, prefixes.Concat(new[] { Tuple.Create(index, TargetMember) }).ToArray(), null, sourceprefixes);
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
                                        WriteExpression(that_source, i, SourceArgument, SourceMember, prefixes.Concat(new[] { Tuple.Create(index, TargetMember) }).ToArray(), null, sourceprefixes);
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
                                        WriteExpression(that_source, i, SourceArgument, SourceMember, prefixes.Concat(new[] { Tuple.Create(index, TargetMember) }).ToArray(), null, sourceprefixes);
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


                             #region xBinaryExpression
                             var xBinaryExpression = asExpression as BinaryExpression;
                             if (xBinaryExpression != null)
                             {
                                 // X:\jsc.svn\examples\javascript\linq\test\TestSelectMath\TestSelectMath\ApplicationWebService.cs

                                 s_SelectCommand += ",\n" + CommentLineNumber() + "\t";
                                 s_SelectCommand += "( ";

                                 WriteExpression(that_source, -1, xBinaryExpression.Left, null, prefixes, null, sourceprefixes);

                                 if (xBinaryExpression.NodeType == ExpressionType.Add)
                                     s_SelectCommand += " + ";
                                 else if (xBinaryExpression.NodeType == ExpressionType.Multiply)
                                     s_SelectCommand += " * ";
                                 else if (xBinaryExpression.NodeType == ExpressionType.Divide)
                                     s_SelectCommand += " / ";

                                 else
                                     Debugger.Break();


                                 WriteExpression(that_source, -1, xBinaryExpression.Right, null, prefixes, null, sourceprefixes);

                                 s_SelectCommand += ")";
                                 s_SelectCommand += " as `" + TargetMember.Name + "`";
                                 return;
                             }
                             #endregion

                             // asExpression = {Range(0, 3).Select(u => new <>f__AnonymousType1`1(z = x.EventTime.Date.AddDays(Convert(-u))))}

                             Debugger.Break();
                         };
                     #endregion




                     // asMemberInitExpression should mean select into row specific values?
                     #region asMemberInitExpression
                     var asMemberInitExpression = asLambdaExpression.Body as MemberInitExpression;
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

                                WriteExpression(that.source, i, asMemberAssignment.Expression, asMemberAssignment.Member,
                                    new Tuple<int, MemberInfo>[0], null,
                                    new Tuple<int, MemberInfo>[0]
                                    );

                            }
                         );
                         SelectCommand = s_SelectCommand;

                         var FromCommand =
                             "from "
                                 + s.GetQualifiedTableNameOrToString().Replace("\n", "\n\t")
                                 + " as " + xouter_Paramerer.Name.Replace("<>", "__");

                         state.FromCommand = FromCommand;
                     }
                     #endregion

                     else
                     {
                         // when does this happen?

                         //SelectCommand = "select 0 as foo";


                         // NewExpression shuld mean new { x, y }


                         var asLUnaryExpression = asLambdaExpression.Body as UnaryExpression;
                         if (asLUnaryExpression != null)
                         {
                             // X:\jsc.svn\examples\javascript\linq\test\TestWhereIsNullOrEmpty\TestWhereIsNullOrEmpty\ApplicationWebService.cs

                             // ??

                             state.WriteExpression(ref s_SelectCommand, asLUnaryExpression, that);
                             SelectCommand = s_SelectCommand;

                             var FromCommand =
                                  "from "
                                      + s.GetQualifiedTableNameOrToString().Replace("\n", "\n\t")
                                  + " as " + xouter_Paramerer.Name.Replace("<>", "__");
                             state.FromCommand = FromCommand;
                         }

                         else
                         {

                             #region asLMethodCallExpression
                             var asLMethodCallExpression = asLambdaExpression.Body as MethodCallExpression;
                             if (asLMethodCallExpression != null)
                             {
                                 // X:\jsc.svn\examples\javascript\linq\test\TestSelectToUpper\TestSelectToUpper\ApplicationWebService.cs
                                 // X:\jsc.svn\examples\javascript\linq\test\TestWhereIsNullOrEmpty\TestWhereIsNullOrEmpty\ApplicationWebService.cs


                                 if (asLMethodCallExpression.Method.DeclaringType == typeof(string))
                                 {

                                     if (asLMethodCallExpression.Method.Name == "ToUpper")
                                     {
                                         var asLMMemberExpression = asLMethodCallExpression.Object as MemberExpression;
                                         // X:\jsc.svn\core\ScriptCoreLib.Extensions\ScriptCoreLib.Extensions\Query\QueryStrategyOfTRowExtensions.AsGenericEnumerable.cs
                                         if (asLMMemberExpression != null)
                                         {
                                             WriteMemberExpression(
                                                 that.source,
                                                 0, asLMMemberExpression, asLMMemberExpression.Member, new Tuple<int, MemberInfo>[0], asLMethodCallExpression.Method,
                                                 new Tuple<int, MemberInfo>[0]
                                                 );


                                         }
                                     }
                                     else
                                     {

                                         state.WriteExpression(ref s_SelectCommand, asLMethodCallExpression, that);

                                     }


                                     SelectCommand = s_SelectCommand;

                                     var FromCommand =
                                          "from "
                                              + s.GetQualifiedTableNameOrToString().Replace("\n", "\n\t")
                                          + " as " + xouter_Paramerer.Name.Replace("<>", "__");
                                     state.FromCommand = FromCommand;
                                 }
                                 else
                                     // what are you calling?
                                     Debugger.Break();
                             }
                             #endregion

                             else
                             {
                                 #region asLMemberExpression
                                 var asLMemberExpression = asLambdaExpression.Body as MemberExpression;
                                 if (asLMemberExpression != null)
                                 {
                                     // scalar?
                                     // X:\jsc.svn\examples\javascript\LINQ\test\TestSelectMember\TestSelectMember\ApplicationWebService.cs
                                     // Member = {System.String path}

                                     WriteMemberExpression(that.source, 0, asLMemberExpression, asLMemberExpression.Member, new Tuple<int, MemberInfo>[0], null, new Tuple<int, MemberInfo>[0]);
                                     SelectCommand = s_SelectCommand;

                                     var FromCommand =
                                          "from "
                                              + s.GetQualifiedTableNameOrToString().Replace("\n", "\n\t")
                                          + " as " + xouter_Paramerer.Name.Replace("<>", "__");
                                     state.FromCommand = FromCommand;
                                 }
                                 #endregion

                                 else
                                 {
                                     // X:\jsc.svn\examples\javascript\linq\test\TestSelectIntoNewExpression\TestSelectIntoNewExpression\ApplicationWebService.cs
                                     #region asLNewExpression
                                     var asLNewExpression = asLambdaExpression.Body as NewExpression;
                                     if (asLNewExpression != null)
                                     {
                                         // https://sites.google.com/a/jsc-solutions.net/backlog/knowledge-base/2014/201406/20140607
                                         // X:\jsc.svn\examples\javascript\linq\test\TestGroupByCount\TestGroupByCount\ApplicationWebService.cs

                                         // prep the from command before select, as select may want to add new sources!
                                         var FromCommand =
                                             "from "
                                                 + s.GetQualifiedTableNameOrToString().Replace("\n", "\n\t")
                                             + " as " + that.selector.Parameters[0].Name.Replace("<>", "__");

                                         state.FromCommand = FromCommand;

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


                                                WriteExpression(
                                                    that.source,
                                                    index, SourceArgument, TargetMember, new Tuple<int, MemberInfo>[0], null, new Tuple<int, MemberInfo>[0]);
                                            }
                                         );
                                         #endregion

                                         // https://sites.google.com/a/jsc-solutions.net/backlog/knowledge-base/2014/201406/20140601/let






                                         SelectCommand = s_SelectCommand;



                                     }
                                     #endregion

                                     else
                                     {
                                         // what if we do select x?
                                         // X:\jsc.svn\examples\javascript\LINQ\test\TestSelect\TestSelect\ApplicationWebService.cs

                                         s_SelectCommand += "\n" + CommentLineNumber() + "\t";
                                         state.WriteExpression(ref s_SelectCommand, asLambdaExpression.Body, that);
                                         SelectCommand = s_SelectCommand;

                                         //SelectCommand = s.SelectCommand;

                                         state.FromCommand = s.FromCommand;

                                         // um. what if we do a where on it?
                                     }
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

