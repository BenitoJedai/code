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
    public static partial class QueryStrategyOfTRowExtensions
    {
        // X:\jsc.svn\core\ScriptCoreLib\Shared\BCLImplementation\System\Linq\Enumerable\Enumerable.GroupBy.cs
        // would group by work as distinct too?

        [ScriptCoreLib.ScriptAttribute.ExplicitInterface]
        public interface IGroupByQueryStrategy : INestedQueryStrategy
        {
            // allow to inspect upper select . what if there are multiple upper selects?
            //ISelectQueryStrategy upperSelect { get; set; }

            // what if we are in a join?
            // IJoinQueryStrategy upperJoin { get; set; }


            IQueryStrategy source { get; }
            Expression keySelector { get; }

            Expression elementSelector { get; }
        }

        class GroupByQueryStrategy<TSource, TKey, TElement> :
            XQueryStrategy<IQueryStrategyGrouping<TKey, TElement>>,
            IGroupByQueryStrategy
        {
            public IQueryStrategy<TSource> source { get; set; }
            public Expression<Func<TSource, TKey>> keySelector { get; set; }
            public Expression<Func<TSource, TElement>> elementSelector { get; set; }


            public ISelectManyQueryStrategy upperSelectMany { get; set; }
            public ISelectQueryStrategy upperSelect { get; set; }
            public IJoinQueryStrategy upperJoin { get; set; }
            public IGroupByQueryStrategy upperGroupBy { get; set; }

            #region IGroupByQueryStrategy
            IQueryStrategy IGroupByQueryStrategy.source
            {
                get { return this.source; }
            }

            Expression IGroupByQueryStrategy.keySelector
            {
                get { return this.keySelector; }
            }

            Expression IGroupByQueryStrategy.elementSelector
            {
                get { return this.elementSelector; }
            }
            #endregion

        }




        // X:\jsc.svn\examples\javascript\forms\Test\TestSQLGroupByAfterJoin\TestSQLGroupByAfterJoin\ApplicationWebService.cs

        //public static IQueryStrategyGroupingBuilder<TKey, TSource>


        public static IQueryStrategy<IQueryStrategyGrouping<TKey, TSource>>
             GroupBy
             <TSource, TKey>(
                 this IQueryStrategy<TSource> source,
                 Expression<Func<TSource, TKey>> keySelector
            )
        {
            // script: error JSC1000: Java : unable to emit ldtoken at 'System.Data.QueryStrategyOfTRowExtensions.GroupBy'#0006: typeof(T) not supported due to type erasure
            // http://stackoverflow.com/questions/1466689/linq-identity-function
            // X:\jsc.svn\examples\java\test\JVMCLRIdentityExpression\JVMCLRIdentityExpression\Program.cs

            return GroupBy(
                source,
                keySelector,
                //Expression.Lambda<
                x => x
            );
        }

        public static IQueryStrategy<IQueryStrategyGrouping<TKey, TElement>>
             GroupBy
             <TSource, TKey, TElement>(
                 this IQueryStrategy<TSource> source,
                 Expression<Func<TSource, TKey>> keySelector,
                 Expression<Func<TSource, TElement>> elementSelector
            )
        {
            // https://sites.google.com/a/jsc-solutions.net/backlog/knowledge-base/2014/201405/20140513
            //var ff = new StackTrace(fNeedFileInfo: true).GetFrame(2);

            Console.WriteLine("GroupBy " + new { keySelector });


            // script: error JSC1000: Java : unable to emit ldtoken at 'System.Data.QueryStrategyOfTRowExtensions.GroupBy'#0006: 
            // typeof(T) not supported due to type erasure
            var GroupBy = new GroupByQueryStrategy<TSource, TKey, TElement>
            {
                source = source,
                keySelector = keySelector,
                elementSelector = elementSelector,


                InternalGetDescriptor =
                    () =>
                    {
                        // inherit the connection/context from above
                        var StrategyDescriptor = source.GetDescriptor();

                        return StrategyDescriptor;
                    }
            };
            var that = GroupBy;


            GroupBy.GetCommandBuilder().Add(
                 state =>
                 {
                     //0001 0200003f TestSQLJoin.ApplicationWebService::System.Data.QueryStrategyOfTRowExtensions+<>c__DisplayClass4`2
                     //script: error JSC1000:
                     //error:
                     //  statement cannot be a load instruction (or is it a bug?)

                     // assembly: W:\TestSQLJoin.ApplicationWebService.exe
                     // type: System.Data.QueryStrategyOfTRowExtensions+<>c__DisplayClass12`3, TestSQLJoin.ApplicationWebService, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
                     // offset: 0x02c5
                     //  method:Void <GroupBy>b__8(CommandBuilderState)



                     // do we even know what needs to be selected?

                     // GroupBy.upperSelect.selectorExpression = {g => new SchemaViewsMiddleViewRow() {Content = g.Last().UpdatedContent}}

                     //Console.WriteLine("GroupBy CommandBuilder " + new { GroupBy.upperSelect.selectorExpression });
                     Console.WriteLine("GroupBy CommandBuilder");

                     (GroupBy.source as IJoinQueryStrategy).With(q => q.upperGroupBy = GroupBy);

                     // GroupBy.keySelector.Body = {1}
                     // +		GroupBy.keySelector.Body	{1}	System.Linq.Expressions.Expression {System.Linq.Expressions.ConstantExpression}
                     var GroupBy_asMemberExpression = GroupBy.keySelector.Body as MemberExpression;






                     // do we need to select it?
                     //state.SelectCommand = "select g.`Grouping.Key`";
                     state.SelectCommand = "" + CommentLineNumber() + " select -- ";
                     var s_SelectCommand = "" + CommentLineNumber() + " select -- ";

                     // this can be disabled/enabled by a upper select?
                     var gDescendingByKeyReferenced = false;

                     // move to equals comparer?
                     // s is the inner grouping

                     var asMemberInitExpression = default(MemberInitExpression);
                     var asMemberInitExpressionByParameter0 = default(ParameterExpression);
                     var asMemberInitExpressionByParameter1 = default(ParameterExpression);
                     var asMemberInitExpressionByParameter2 = default(ParameterExpression);

                     if (GroupBy.upperSelect != null)
                         asMemberInitExpression = ((LambdaExpression)GroupBy.upperSelect.selectorExpression).Body as MemberInitExpression;



                     // GroupBy_asMemberExpression = {<>h__TransparentIdentifier0.rJoin.ClientName}
                     // https://sites.google.com/a/jsc-solutions.net/backlog/knowledge-base/2014/201405/20140515

                     //+		GroupBy_asMemberExpression	{<>h__TransparentIdentifier0.rJoin.ClientName}	System.Linq.Expressions.MemberExpression {System.Linq.Expressions.FieldExpression}
                     // what if we are not grouping a join?






                     // GroupBy.keySelector.Body = {new <>f__AnonymousType1`2(duration = y.duration, path = y.path)}
                     // X:\jsc.svn\examples\javascript\linq\test\TestGroupByMultipleFields\TestGroupByMultipleFields\ApplicationWebService.cs



                     //var s_SelectCommand = "select " + CommentLineNumber()
                     //   + GroupingKeyFieldExpressionName
                     //    + " as `Grouping.Key`";


                     // disable comma


                     #region asMethodCallExpression
                     if (GroupBy.upperSelect != null)
                     {
                         // (GroupBy.upperSelect.selectorExpression as LambdaExpression).Body = {new <>f__AnonymousType2`5(Tag = value(TestSQLGroupByAfterJoin.ApplicationWebService+<>c__DisplayClass4).x.tag, ClientName = result.Key, FirstName = result.Last().l.FirstName, Payment = result.Last().rJoin.Payment, Timestamp = result.Last().rJoin.Timestamp)}



                         #region asMethodCallExpression

                         var asMethodCallExpression = (GroupBy.upperSelect.selectorExpression as LambdaExpression).Body as MethodCallExpression;
                         if (asMethodCallExpression != null)
                         {
                             //+		(new System.Collections.Generic.Mscorlib_CollectionDebugView<System.Linq.Expressions.Expression>((new System.Linq.Expressions.Expression.MethodCallExpressionProxy(asMethodCallExpression as System.Linq.Expressions.MethodCallExpressionN)).Arguments as System.Runtime.CompilerServices.TrueReadOnlyCollection<System.Linq.Expressions.Expression>)).Items[0x00000000]	
                             // {ug}	System.Linq.Expressions.Expression {System.Linq.Expressions.TypedParameterExpression}
                             //+		(new System.Linq.Expressions.Expression.MethodCallExpressionProxy(asMethodCallExpression as System.Linq.Expressions.MethodCallExpressionN)).Type	
                             // {Name = "SchemaTheGridTableViewRow" FullName = "SQLiteWithDataGridViewX.Data.SchemaTheGridTableViewRow"}	System.Type {System.RuntimeType}
                             //+		(new System.Linq.Expressions.Expression.MethodCallExpressionProxy(asMethodCallExpression as System.Linq.Expressions.MethodCallExpressionN)).Method	{SQLiteWithDataGridViewX.Data.SchemaTheGridTableViewRow 
                             // Last[SchemaTheGridTableKey,SchemaTheGridTableViewRow](ScriptCoreLib.Shared.Data.Diagnostics.IQueryStrategyGrouping`2[SQLiteWithDataGridViewX.Data.SchemaTheGridTableKey,SQLiteWithDataGridViewX.Data.SchemaTheGridTableViewRow])}	System.Reflection.MethodInfo {System.Reflection.RuntimeMethodInfo}

                             Console.WriteLine(new { asMethodCallExpression });

                             // special!
                             if (asMethodCallExpression.Method.Name.TakeUntilIfAny("_") == "First")
                             {
                                 gDescendingByKeyReferenced = true;
                                 //state.SelectCommand += ",\n\t gDescendingByKey.`" + asMemberAssignment.Member.Name + "` as `" + asMemberAssignment.Member.Name + "`";
                                 //s_SelectCommand += ",\n\t s.`" + asMemberExpression.Member.Name + "` as `" + asMemberAssignment.Member.Name + "`";

                                 Debugger.Break();
                                 //return;
                             }

                             if (asMethodCallExpression.Method.Name.TakeUntilIfAny("_") == "Last")
                             {
                                 // do we have type info on what fields to select?
                                 // jsc data layer gen is using fields. we might end yp using fields in the future.

                                 asMethodCallExpression.Type.GetFields().WithEach(
                                     SourceMember =>
                                     {
                                         if (SourceMember.Name == "Key")
                                             return;

                                         // what if the underlying select does not have all the columns?

                                         state.SelectCommand += ",\n\t g.`" + SourceMember.Name + "` as `" + SourceMember.Name + "`";
                                         s_SelectCommand += ",\n\t s.`" + SourceMember.Name + "` as `" + SourceMember.Name + "`";
                                     }
                                 );



                                 // http://stackoverflow.com/questions/8341136/mysql-alias-for-select-columns
                                 // ? You can't use * with an alias.
                                 //return;
                             }
                         }
                         #endregion

                     }
                     #endregion


                     //Action<int, Expression, MemberInfo> WriteExpression = null;
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

                             Console.WriteLine(new { index, asMemberExpression.Member, asMemberExpression.Member.Name });

                             #region let z <- Grouping.Key
                             // tested by?
                             //if (asMemberExpression.Member.DeclaringType.IsAssignableFrom(typeof(IQueryStrategyGrouping)))

                             // X:\jsc.svn\examples\javascript\linq\test\TestJoinGroupSelectCastLong\TestJoinGroupSelectCastLong\ApplicationWebService.cs

                             var isGrouping = typeof(IQueryStrategyGrouping)
                             .IsAssignableFrom(
                                 asMemberExpression.Member.DeclaringType
                                 );

                             if (isGrouping)
                             {
                                 var IsKey = asMemberExpression.Member.Name == "Key";

                                 // if not a property we may still have the getter in JVM
                                 IsKey |= asMemberExpression.Member.Name == "get_Key";

                                 if (IsKey)
                                 {
                                     var asSSNNewExpression = keySelector.Body as NewExpression;
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
                                             //WriteExpression(i, SourceArgument, SourceMember, prefixes.Concat(new[] { Tuple.Create(index, asMemberExpression.Member) }).ToArray(), null);
                                             WriteExpression(i, SourceArgument, SourceMember, prefixes, null);
                                         }
                                        );
                                         return;
                                     }

                                     WriteExpression(index, keySelector.Body, asMemberExpression.Member, prefixes, null);

                                     return;
                                 }
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

                                     var asIJoinQueryStrategy = GroupBy.source as IJoinQueryStrategy;
                                     if (asIJoinQueryStrategy != null)
                                     {
                                         var asJLambdaExpression = (asIJoinQueryStrategy.selectorExpression as LambdaExpression);

                                         // X:\jsc.svn\examples\javascript\linq\test\TestSelectIntoViewRow\TestSelectIntoViewRow\ApplicationWebService.cs
                                         s_SelectCommand += ",\n\t s.`" + asJLambdaExpression.Parameters[0].Name + "_" + asMemberExpression.Member.Name + "` as `" + asMemberAssignment.Member.Name + "`";
                                         return;
                                     }

                                     s_SelectCommand += ",\n\t s.`" + asMemberExpression.Member.Name + "` as `" + asMemberAssignment.Member.Name + "`";
                                     return;
                                 }
                             }
                             #endregion

                             #region WriteMemberExpression:asMConstantExpression
                             //         var SpecialConstant_u = "44";
                             var asMConstantExpression = asMemberExpression.Expression as ConstantExpression;
                             if (asMConstantExpression != null)
                             {
                                 if (that.upperSelect != null)
                                 {
                                     // X:\jsc.svn\examples\javascript\linq\test\TestSelectGroupByAndConstant\TestSelectGroupByAndConstant\ApplicationWebService.cs

                                     // the upper select will be selecting their own constants
                                     // no reason to do it inside group by
                                     return;
                                 }

                                 // ?
                                 Debugger.Break();

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
                             }
                             #endregion

                             #region WriteMemberExpression:asMMemberExpression
                             var asMMemberExpression = asMemberExpression.Expression as MemberExpression;
                             if (asMMemberExpression != null)
                             {
                                 #region asMMFieldInfo
                                 var asMMFieldInfo = asMMemberExpression.Member as FieldInfo;
                                 if (asMMFieldInfo != null)
                                 {
                                     #region asPropertyInfo
                                     var asPropertyInfo = asMemberExpression.Member as PropertyInfo;
                                     if (asPropertyInfo != null)
                                     {
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

                                         return;
                                     }
                                     #endregion
                                 }
                                 #endregion


                                 // https://sites.google.com/a/jsc-solutions.net/backlog/knowledge-base/2014/201405/20140515
                                 // X:\jsc.svn\examples\javascript\forms\Test\TestSQLGroupByAfterJoin\TestSQLGroupByAfterJoin\ApplicationWebService.cs
                                 #region asMMMemberInfo
                                 var asMMMemberInfo = asMMemberExpression.Member as MemberInfo;
                                 if (asMMMemberInfo != null)
                                 {
                                     // asMMemberExpression = {result.Last().l}
                                     // asMemberExpression = {result.Last().l.FirstName}

                                     #region  asMMMCall
                                     var asMMMCall = asMMemberExpression.Expression as MethodCallExpression;
                                     if (asMMMCall != null)
                                     {
                                         //asMMMCall = {result.Last()}


                                         // Last is for grouping?
                                         var refLast = new Func<IQueryStrategyGrouping<long, object>, object>(QueryStrategyOfTRowExtensions.Last);

                                         if (asMMMCall.Method.Name == refLast.Method.Name)
                                         {
                                             // group by is not supposed to rename.
                                             // select will do that.
                                             // x:\jsc.svn\examples\javascript\linq\test\testwherejointtgroupbyselectlast\testwherejointtgroupbyselectlast\applicationwebservice.cs
                                             // https://sites.google.com/a/jsc-solutions.net/backlog/knowledge-base/2014/201406/20140601

                                             state.SelectCommand += ",\n" + CommentLineNumber() + "\t g.`" + asMMemberExpression.Member.Name + "_" + asMemberExpression.Member.Name + "`";

                                             // if its a normal select then it wont be flat, will it

                                             var xasIJoinQueryStrategy = (that.source as IJoinQueryStrategy);
                                             if (xasIJoinQueryStrategy != null)
                                             {
                                                 // https://sites.google.com/a/jsc-solutions.net/backlog/knowledge-base/2014/201406/20140604
                                                 //Debugger.Break();

                                                 s_SelectCommand += ",\n" + CommentLineNumber() + "\t"
                                                 + "s.`" + asMMemberExpression.Member.Name + "_" + asMemberExpression.Member.Name + "`";
                                             }
                                             else
                                             {
                                                 s_SelectCommand += ",\n" + CommentLineNumber() + "\t"
                                                   + "s.`" + asMemberExpression.Member.Name + "`"
                                                   + " as `" + asMMemberExpression.Member.Name + "_" + asMemberExpression.Member.Name + "`";
                                             }


                                             return;
                                         }
                                     }
                                     #endregion


                                     // X:\jsc.svn\examples\javascript\LINQ\test\TestSelectManyRange\TestSelectManyRange\ApplicationWebService.cs



                                     state.SelectCommand += ",\n" + CommentLineNumber() + "\t g.`" + asMMemberExpression.Member.Name + "_" + asMemberExpression.Member.Name + "`";

                                     // X:\jsc.svn\examples\javascript\linq\test\TestJoinGroupSelectCastLong\TestJoinGroupSelectCastLong\ApplicationWebService.cs
                                     var asIJoinQueryStrategy = (that.source as IJoinQueryStrategy);
                                     if (asIJoinQueryStrategy != null)
                                     {

                                         s_SelectCommand += ",\n" + CommentLineNumber() + "\t"
                                      + "s.`" + asMMemberExpression.Member.Name + "_" + asMemberExpression.Member.Name + "`"
                                      + " as `" + asMMemberExpression.Member.Name + "_" + asMemberExpression.Member.Name + "`";
                                     }
                                     else
                                     {

                                         s_SelectCommand += ",\n" + CommentLineNumber() + "\t"
                                      + "s.`" + asMemberExpression.Member.Name + "`"
                                      + " as `" + asMMemberExpression.Member.Name + "_" + asMemberExpression.Member.Name + "`";
                                     }

                                     return;
                                 }
                                 #endregion


                             }
                             #endregion

                             #region WriteMemberExpression ParameterExpression
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

                                 //s_SelectCommand += ",\n" + CommentLineNumber() + "\t"
                                 //     + that.selector.Parameters[0].Name.Replace("<>", "__")
                                 //     + ".`" + asMemberExpression.Member.Name + "` as `" + GetPrefixedTargetName() + "`";

                                 state.SelectCommand += ",\n" + CommentLineNumber() + "\t g.`"
                                    + asMMemberExpressionParameterExpression.Name + "_" + asMemberExpression.Member.Name + "`";

                                 s_SelectCommand += ",\n" + CommentLineNumber() + "\t"
                                    + "s.`" + asMemberExpression.Member.Name + "` as `" + asMMemberExpressionParameterExpression.Name + "_" + asMemberExpression.Member.Name + "`";

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


                             // X:\jsc.svn\examples\javascript\LINQ\test\TestGroupByMultipleFields\TestGroupByMultipleFields\ApplicationWebService.cs
                             // is triggering this code.
                             // what are we doing on the select implementation.

                             #region WriteExpression:asEParameterExpression
                             var asEParameterExpression = asExpression as ParameterExpression;
                             if (asEParameterExpression != null)
                             {
                                 // used in select
                                 // using the let keyword?

                                 // x:\jsc.svn\examples\javascript\linq\test\testjoinselectanonymoustype\testjoinselectanonymoustype\applicationwebservice.cs

                                 // that.elementSelector = {y => new <>f__AnonymousType0`1(y = y)}

                                 // selectorExpression = {g => new <>f__AnonymousType1`2(g = g, du = g.Last().y.duration)}


                                 #region selectProjectionWalker
                                 Action<INestedQueryStrategy, ParameterExpression> selectProjectionWalker = null;

                                 selectProjectionWalker =
                                    (yySelect, arg1) =>
                                        {
                                            // X:\jsc.svn\examples\javascript\linq\test\TestSelectAndSubSelect\TestSelectAndSubSelect\ApplicationWebService.cs
                                            if (yySelect == null)
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
                                                                                //if (yasSMMemberExpression.Member.Name == (yy.selectorExpression as LambdaExpression).Parameters[0].Name)
                                                                                if (yasSMMemberExpression.Member.Name == arg1.Name)
                                                                                {

                                                                                    s_SelectCommand += ",\n" + CommentLineNumber() + "\t "
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

                                                            #region xasMemberExpression
                                                            // item = {gg.Last().y.duration}
                                                            var xasMemberExpression = item as MemberExpression;
                                                            if (xasMemberExpression != null)
                                                            {
                                                                var xasMMemberExpression = xasMemberExpression.Expression as MemberExpression;
                                                                if (xasMMemberExpression != null)
                                                                {
                                                                    //if (xasMMemberExpression.Member.Name == (yy.selectorExpression as LambdaExpression).Parameters[0].Name)
                                                                    if (xasMMemberExpression.Member.Name == arg1.Name)
                                                                    {
                                                                        s_SelectCommand += ",\n" + CommentLineNumber() + "\t "
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
                                            var xISelectQueryStrategy = yySelect as ISelectQueryStrategy;
                                            if (xISelectQueryStrategy != null)
                                                selectProjectionWalker(xISelectQueryStrategy.source as ISelectQueryStrategy, arg1);

                                            var xISelectManyQueryStrategy = yySelect as ISelectManyQueryStrategy;
                                            if (xISelectManyQueryStrategy != null)
                                                selectProjectionWalker(xISelectManyQueryStrategy.source as ISelectQueryStrategy, arg1);

                                        };
                                 #endregion


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



                                 if (asEParameterExpression == that.elementSelector.Parameters[0])
                                 {


                                     projectionWalker(that.source as IJoinQueryStrategy);

                                     return;

                                 }
                                 else
                                 {
                                     if (that.upperSelect != null)
                                     {
                                         var arg1 = (that.upperSelect.selectorExpression as LambdaExpression).Parameters[0];

                                         if (asEParameterExpression == arg1)
                                         {
                                             // ding ding. walk that!

                                             selectProjectionWalker(that.upperSelect, arg1);

                                             projectionWalker(that.source as IJoinQueryStrategy);

                                             return;
                                         }
                                     }
                                     else if (that.upperSelectMany != null)
                                     {
                                         // X:\jsc.svn\examples\javascript\linq\test\TestJoinGroupSelectCastLong\TestJoinGroupSelectCastLong\ApplicationWebService.cs

                                         var arg1 = (that.upperSelectMany.resultSelector as LambdaExpression).Parameters[1];

                                         if (asEParameterExpression == arg1)
                                         {
                                             // ding ding. walk that!

                                             selectProjectionWalker(that.upperSelectMany, arg1);

                                             projectionWalker(that.source as IJoinQueryStrategy);

                                             return;
                                         }
                                     }
                                     Debugger.Break();

                                     return;
                                 }
                             }
                             #endregion

                             #region WriteExpression:asMConstantExpression
                             {
                                 var asMConstantExpression = asMemberAssignment.Expression as ConstantExpression;
                                 if (asMConstantExpression != null)
                                 {
                                     if (that.upperSelect != null)
                                     {
                                         // jsc, where else do we have the exact same check?
                                         // the upper select shall do the constant selection!


                                         return;
                                     }


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
                                 Console.WriteLine(new { index, asMethodCallExpression.Method });

                                 #region  WriteExpression:asMethodCallExpression:QueryStrategyOfTRowExtensions::
                                 if (asMethodCallExpression.Method.DeclaringType == typeof(QueryStrategyOfTRowExtensions))
                                 {

                                     #region subquery COPY from select
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
                                                         var doWhere_source = __Select_source;
                                                         Func<IQueryStrategy, IQueryStrategy> doWhere =
                                                            xTable =>
                                                                {
                                                                    // Operand = {kk => (kk.duration == 46)}
                                                                    var __Where_filter = doWhere_source.Arguments[1] as UnaryExpression;

                                                                    // X:\jsc.svn\core\ScriptCoreLib.Extensions\ScriptCoreLib.Extensions\Query\QueryStrategyOfTRowExtensions.Where.cs
                                                                    var xTable_Where = (IQueryStrategy)doWhere_source.Method.Invoke(null,
                                                                         parameters: new object[] { xTable, __Where_filter.Operand }
                                                                     );

                                                                    return xTable_Where;
                                                                };
                                                         #endregion

                                                         // while where?
                                                         var __Where_source_Where2 = __Select_source.Arguments[0] as MethodCallExpression;
                                                         if (__Where_source_Where2 != null)
                                                         {

                                                             if (__Where_source_Where2.Method.Name == refWhere.Name)
                                                             {
                                                                 // x:\jsc.svn\examples\javascript\linq\test\testgroupbycountviascalarwhere\testgroupbycountviascalarwhere\applicationwebservice.cs
                                                                 var doWhere1 = doWhere;
                                                                 #region doWhere2
                                                                 doWhere =
                                                                xTable =>
                                                                {

                                                                    // Operand = {kk => (kk.duration == 46)}
                                                                    var __Where_filter = __Where_source_Where2.Arguments[1] as UnaryExpression;

                                                                    // X:\jsc.svn\core\ScriptCoreLib.Extensions\ScriptCoreLib.Extensions\Query\QueryStrategyOfTRowExtensions.Where.cs
                                                                    var xTable_Where = (IQueryStrategy)__Where_source_Where2.Method.Invoke(null,
                                                                         parameters: new object[] { xTable, __Where_filter.Operand }
                                                                     );

                                                                    return doWhere1(xTable_Where);
                                                                };
                                                                 #endregion

                                                                 // can we chain where with where2 and resume?

                                                                 __Select_source = __Where_source_Where2;
                                                             }
                                                         }

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
                                                                 var arg0 = that.elementSelector.Parameters[0];
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
                                                             var arg0 = that.elementSelector.Parameters[0];
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
                                     //    // X:\jsc.svn\examples\javascript\LINQ\test\TestSelectDateGroups\TestSelectDateGroups\ApplicationWebService.cs

                                     //    #region arg0Elements_ParameterExpression
                                     //    var arg0Elements_ParameterExpression = asMethodCallExpression.Arguments[0] as ParameterExpression;
                                     //    if (arg0Elements_ParameterExpression != null)
                                     //    {
                                     //        var us = (that.upperSelect.selectorExpression as LambdaExpression);

                                     //        var arg0 = us.Parameters[0];
                                     //        if (arg0.Name == arg0Elements_ParameterExpression.Name)
                                     //        {
                                     //            // are we in a scalar mode?

                                     //            // https://sites.google.com/a/jsc-solutions.net/backlog/knowledge-base/2014/201406/20140607
                                     //            // !!! actually no, its the upper select that has to do the counting?

                                     //            // um we have to call ourselves, but without selecting anything and instead
                                     //            // have to add a where to it?

                                     //            // we are in a nested query
                                     //            #region s_SelectCommand
                                     //            ////var xSelectScalar = QueryStrategyExtensions.AsCommandBuilder(that);
                                     //            ////var scalarsubquery = xSelectScalar.ToString();

                                     //            ////// http://blog.tanelpoder.com/2013/08/22/scalar-subqueries-in-oracle-sql-where-clauses-and-a-little-bit-of-exadata-stuff-too/

                                     //            // do we have to 
                                     //            // we dont know yet how to get sql of that thing do we

                                     //            state.SelectCommand += ",\n" + CommentLineNumber() + "\t /* upper has to do that? */ g.`" + asMemberAssignment.Member.Name + "` as `" + asMemberAssignment.Member.Name + "`";

                                     //            s_SelectCommand += ",\n" + CommentLineNumber() + "\t ( /* upper has to do that? */ count(*) ) as `" + asMemberAssignment.Member.Name + "`";


                                     //            ////state.ApplyParameter.AddRange(xSelectScalar.ApplyParameter);
                                     //            #endregion
                                     //            return;
                                     //        }


                                     //        s_SelectCommand += ",\n" + CommentLineNumber() + "\t"
                                     //       + arg0Elements_ParameterExpression.Name + ". `" + asMemberAssignment.Member.Name + "` as `" + asMemberAssignment.Member.Name + "`";

                                     //        return;
                                     //    }
                                     //    #endregion


                                     //    #region a0MethodCallExpression
                                     //    var a0MethodCallExpression = asMethodCallExpression.Arguments[0] as MethodCallExpression;
                                     //    if (a0MethodCallExpression != null)
                                     //    {
                                     //        // X:\jsc.svn\examples\javascript\LINQ\test\TestSelectDateGroups\TestSelectDateGroups\ApplicationWebService.cs
                                     //        // X:\jsc.svn\examples\javascript\linq\test\TestGroupByCountViaScalarWhere\TestGroupByCountViaScalarWhere\ApplicationWebService.cs

                                     //        // a0MethodCallExpression = {new ApplicationPerformance().Where(xx => (xx.connectStart > 1))}

                                     //        var xTable_Where_Select0 = subquery(a0MethodCallExpression);
                                     //        var xTable_Where_Select = xTable_Where_Select0 as ISelectQueryStrategy;

                                     //        // ?
                                     //        xTable_Where_Select.upperGroupBy = that;

                                     //        xTable_Where_Select.scalarAggregateOperand = "count";

                                     //        #region s_SelectCommand
                                     //        var xSelectScalar = QueryStrategyExtensions.AsCommandBuilder(xTable_Where_Select0);
                                     //        var scalarsubquery = xSelectScalar.ToString();

                                     //        // http://blog.tanelpoder.com/2013/08/22/scalar-subqueries-in-oracle-sql-where-clauses-and-a-little-bit-of-exadata-stuff-too/



                                     //        // pass it forward
                                     //        state.SelectCommand += ",\n" + CommentLineNumber() + "\t  g.`" + asMemberAssignment.Member.Name + "` as `" + asMemberAssignment.Member.Name + "`";

                                     //        // do we have to 
                                     //        // we dont know yet how to get sql of that thing do we
                                     //        s_SelectCommand += ",\n\t (\n\t" + scalarsubquery.Replace("\n", "\n\t") + ") as `" + asMemberAssignment.Member.Name + "`";


                                     //        state.ApplyParameter.AddRange(xSelectScalar.ApplyParameter);
                                     //        #endregion
                                     //        return;
                                     //    }
                                     //    #endregion

                                     //    state.SelectCommand += ",\n" + CommentLineNumber() + "\t g.`" + asMemberAssignment.Member.Name + "` as `" + asMemberAssignment.Member.Name + "`";
                                     //    s_SelectCommand += ",\n" + CommentLineNumber() + "\t count(*) as `" + asMemberAssignment.Member.Name + "`";

                                     //    return;
                                     //}
                                     //#endregion

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


                                 }
                                 #endregion

                                 //var refToLower = new Func<string, Func<string>>(x => x.ToLower)().Method;

                                 #region string::
                                 if (asMethodCallExpression.Method.DeclaringType == typeof(string))
                                 {
                                     #region  lower( special!!
                                     if (asMethodCallExpression.Method.Name.TakeUntilIfAny("_") == "ToLower")
                                     {
                                         // X:\jsc.svn\examples\javascript\LINQ\test\TestGroupByThenOrderByThenOrderBy\TestGroupByThenOrderByThenOrderBy\ApplicationWebService.cs
                                         var asMMemberExpression = asMethodCallExpression.Object as MemberExpression;
                                         var asMMMemberExpression = asMMemberExpression.Expression as MemberExpression;

                                         // X:\jsc.svn\examples\javascript\LINQ\test\TestGroupByThenOrderByThenOrderBy\TestGroupByThenOrderByThenOrderBy\ApplicationWebService.cs
                                         // does it matter when we do the to lower?
                                         // before group by
                                         // or after? or both?
                                         // whats the benefit?

                                         //state.SelectCommand += ",\n\t g.`" + asMemberAssignment.Member.Name + "` as `" + asMemberAssignment.Member.Name + "`";
                                         state.SelectCommand += ",\n\t g.`"
                                         + asMMMemberExpression.Member.Name + "_"
                                         + asMMemberExpression.Member.Name + "`";

                                         //s_SelectCommand += ",\n\t lower(s.`" + asMMemberExpression.Member.Name + "`) as `" + asMemberAssignment.Member.Name + "`";
                                         // the select will do the lowering?
                                         s_SelectCommand += ",\n\t s.`"
                                         + asMMMemberExpression.Member.Name + "_"
                                         + asMMemberExpression.Member.Name + "`";

                                         return;


                                     }
                                     #endregion
                                 }
                                 #endregion 

                             }
                             #endregion

                             #region WriteExpression:asInvocationExpression -> WriteExpression SourceArgument
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

                             #region WriteExpression:asMemberExpression
                             {
                                 var asMemberExpression = asMemberAssignment.Expression as MemberExpression;
                                 if (asMemberExpression != null)
                                 {
                                     WriteMemberExpression(index, asMemberExpression, TargetMember, prefixes, null);
                                     return;
                                 }
                             }
                             #endregion

                             #region  WriteExpression:asUnaryExpression -> WriteExpression Operand
                             var asUnaryExpression = asMemberAssignment.Expression as UnaryExpression;
                             if (asUnaryExpression != null)
                             {
                                 WriteExpression(index, asUnaryExpression.Operand, TargetMember, prefixes, valueSelector);
                                 return;
                             }
                             #endregion

                             // asExpression = {g}

                             Debugger.Break();
                         };
                     #endregion



                     #region asMemberInitExpression
                     if (asMemberInitExpression != null)
                         asMemberInitExpression.Bindings.WithEachIndex(
                             (SourceBinding, index) =>
                             {
                                 var asMemberAssignment = SourceBinding as MemberAssignment;
                                 if (asMemberAssignment != null)
                                 {
                                     WriteExpression(index, asMemberAssignment.Expression, asMemberAssignment.Member, new Tuple<int, MemberInfo>[0], null);
                                     return;
                                 }
                                 Debugger.Break();
                             }
                         );
                     #endregion

                     #region asNewExpression
                     if (asMemberInitExpression == null)
                     {
                         // X:\jsc.svn\examples\javascript\linq\test\TestWhereThenGroup\TestWhereThenGroup\ApplicationWebService.cs
                         // X:\jsc.svn\examples\javascript\linq\test\TestJoinGroupSelectCastLong\TestJoinGroupSelectCastLong\ApplicationWebService.cs

                         #region upperSelectMany
                         if (GroupBy.upperSelectMany != null)
                         {
                             var asNewExpression = (GroupBy.upperSelectMany.resultSelector as LambdaExpression).Body as NewExpression;

                             asNewExpression.Arguments.WithEachIndex(
                                 (SourceArgument, index) =>
                             {
                                 var TargetMember = asNewExpression.Members[index];
                                 var asMemberAssignment = new { Member = TargetMember };


                                 // ?
                                 WriteExpression(index, SourceArgument, TargetMember, new Tuple<int, MemberInfo>[0], null);
                             }
                             );
                         }
                         #endregion


                         #region upperSelectMany
                         if (GroupBy.upperSelect != null)
                         {
                             var asNewExpression = (GroupBy.upperSelect.selectorExpression as LambdaExpression).Body as NewExpression;
                             if (asNewExpression != null)
                             {
                                 asNewExpression.Arguments.WithEachIndex(
                                     (SourceArgument, index) =>
                                     {
                                         var TargetMember = asNewExpression.Members[index];
                                         var asMemberAssignment = new { Member = TargetMember };


                                         // ?
                                         WriteExpression(index, SourceArgument, TargetMember, new Tuple<int, MemberInfo>[0], null);
                                     }
                                 );
                             }
                             else
                             {
                                 // group by into g select g.Last() ?
                                 // Debugger.Break();
                             }

                         }
                         #endregion

                     }
                     #endregion




                     var s = QueryStrategyExtensions.AsCommandBuilder(GroupBy.source);
                     // is it default?

                     #region  state.FromCommand

                     //                 from (select `Key`, `MiddleSheet`, `UpdatedContent`, `Tag`, `Timestamp`
                     //from `Schema.MiddleSheetUpdates`
                     //) as s  group by `Grouping.Key`


                     // https://sites.google.com/a/jsc-solutions.net/backlog/knowledge-base/2014/201405/20140515

                     var g = s_SelectCommand
                              + "\n"
                              + "from " + s.GetQualifiedTableNameOrToString().Replace("\n", "\n\t") + " as s " // s needs to go at some point
                              + "\n"
                              + "group by ";

                     // can we group by multiple fields?
                     // X:\jsc.svn\examples\javascript\LINQ\test\TestGroupByMultipleFields\TestGroupByMultipleFields\ApplicationWebService.cs

                     #region  keySelector
                     // https://sites.google.com/a/jsc-solutions.net/backlog/knowledge-base/2014/201406/20140607
                     // X:\jsc.svn\examples\javascript\linq\test\TestGroupByCountViaJoin\TestGroupByCountViaJoin\ApplicationWebService.cs

                     var zkasNewExpression = GroupBy.keySelector.Body as NewExpression;
                     if (zkasNewExpression != null)
                     {
                         zkasNewExpression.Arguments.WithEachIndex(
                             (SourceBinding, i) =>
                             {
                                 if (i > 0)
                                     g += ", ";

                                 var xm = SourceBinding as MemberExpression;
                                 if (xm != null)
                                 {
                                     // what if its a join?

                                     var xmm = xm.Expression as MemberExpression;
                                     if (xmm != null)
                                     {
                                         g += "\n" + CommentLineNumber() + "\t"
                                            + "s.`" + xmm.Member.Name + "_" + xm.Member.Name + "`";
                                     }
                                     else
                                     {
                                         g += "\n" + CommentLineNumber() + "\t"
                                     + "s.`" + xm.Member.Name + "`";
                                     }
                                 }
                             }
                         );
                     }
                     else
                     {

                         var GroupBy_asC = GroupBy.keySelector.Body as ConstantExpression;
                         if (GroupBy_asC != null)
                         {
                             // X:\jsc.svn\examples\javascript\LINQ\MinMaxAverageExperiment\MinMaxAverageExperiment\ApplicationWebService.cs

                             if (GroupBy_asC.Value is int)
                                 if ((int)GroupBy_asC.Value == 1)
                                 {
                                     g += "\n" + CommentLineNumber() + "\t"
                                      + "1";
                                 }
                         }
                         else
                         {
                             var xm = GroupBy.keySelector.Body as MemberExpression;
                             if (xm != null)
                             {
                                 var xmm = xm.Expression as MemberExpression;
                                 if (xmm != null)
                                 {
                                     g += "\n" + CommentLineNumber() + "\t"
                                        + "s.`" + xmm.Member.Name + "_" + xm.Member.Name + "`";
                                 }
                                 else
                                 {

                                     g += "\n" + CommentLineNumber() + "\t"
                                          + "s.`" + xm.Member.Name + "`";
                                 }
                             }
                             else Debugger.Break();
                         }
                     }
                     #endregion


                     state.FromCommand =
                          "from (\n\t"
                            + g.Replace("\n", "\n\t")
                            + "\n) as g";


                     if (!gDescendingByKeyReferenced)
                     {
                         // can we simplyfy?

                         // X:\jsc.svn\examples\javascript\forms\Test\TestSQLGroupByAfterJoin\TestSQLGroupByAfterJoin\ApplicationWebService.cs
                         // https://sites.google.com/a/jsc-solutions.net/backlog/knowledge-base/2014/201405/20140515

                         // what if the where clause is not yet attached?
                         // http://stackoverflow.com/questions/9253244/sql-having-vs-where


                         // !!! we can allow this optimzation once where functions on its own nesting level!


                         {

                             //if (string.IsNullOrEmpty(state.WhereCommand))
                             //    if (string.IsNullOrEmpty(state.OrderByCommand))
                             //        if (string.IsNullOrEmpty(state.LimitCommand))
                             //        {
                             //            // we might not need the outer select?


                             //            state.SelectCommand = s_SelectCommand;
                             //            state.FromCommand =
                             //                "from " + s.GetQualifiedTableNameOrToString().Replace("\n", "\n\t") + " as s "
                             //                + "\n group by `Grouping.Key`";
                             //        }

                         }

                     }
                     else
                     {
                         // omit if we aint using it

                         // ? this wont work on a join!!
                         #region gDescendingByKeyReferenced
                         var gDescendingByKey = s_SelectCommand
                             + "\n from (select * from (" + s.ToString().Replace("\n", "\n\t") + ") order by `Key` desc) as s "
                           + "\n group by `Grouping.Key`";

                         state.FromCommand +=
                                 "\n inner join (\n\t"
                                + gDescendingByKey.Replace("\n", "\n\t")
                                + "\n) as gDescendingByKey"
                                + "\n on g.`Grouping.Key` = gDescendingByKey.`Grouping.Key`";
                         #endregion
                     }

                     #endregion


                     state.ApplyParameter.AddRange(s.ApplyParameter);
                 }
            );


            return GroupBy;
            //return new XQueryStrategyGroupingBuilder<TKey, TSource> { source = source, keySelector = keySelector };
        }

    }
}

