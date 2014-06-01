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
                     state.SelectCommand = "select g.`Grouping.Key`";

                     // this can be disabled/enabled by a upper select?
                     var gDescendingByKeyReferenced = false;

                     // move to equals comparer?
                     // s is the inner grouping




                     //AsDataTable { Strategy = System.Data.QueryStrategyOfTRowExtensions_JoinQueryStrategy_4@17f3214 }
                     //InternalGetConnectionString { key = { DataSource = file:Book1.xlsx.sqlite, ReadOnly = false, InternalUser = root, InternalHost = localhost, InternalInstanceName = instance_name } }
                     //Join CommandBuilder { that = System.Data.QueryStrategyOfTRowExtensions_JoinQueryStrategy_4@17f3214 }
                     //Join CommandBuilder  ...  { asLambdaExpression = { Body = MemberInitExpression { NewExpression = NewExpression { Constructor = .ctor(), Type = TestSQLJoin.Data.Book1TheViewRow } }, Parameter
                     //Join CommandBuilder building FromCommand...
                     //Join CommandBuilder { that = System.Data.QueryStrategyOfTRowExtensions_JoinQueryStrategy_4@5c6936 }
                     //Join CommandBuilder  ...  { asLambdaExpression = { Body = NewExpression { Constructor = .ctor(java.lang.Object, java.lang.Object), Type = __AnonymousTypes__TestSQLJoin_ApplicationWebService.
                     //Join CommandBuilder building FromCommand...
                     //Join CommandBuilder building SelectCommand...
                     //Join CommandBuilder  ...  { asMemberInitExpression =  }
                     //Join CommandBuilder building SelectCommand... upperJoin
                     //Join CommandBuilder building SelectCommand... ImplicitConstantFields { Type =  }


                     var asMemberInitExpression = default(MemberInitExpression);
                     var asMemberInitExpressionByParameter0 = default(ParameterExpression);
                     var asMemberInitExpressionByParameter1 = default(ParameterExpression);
                     var asMemberInitExpressionByParameter2 = default(ParameterExpression);

                     if (GroupBy.upperSelect != null)
                         asMemberInitExpression = ((LambdaExpression)GroupBy.upperSelect.selectorExpression).Body as MemberInitExpression;


                     #region upperJoin
                     if (GroupBy.upperJoin != null)
                     {
                         //var j = from iu in new Schema.MiddleSheetUpdates()
                         //        group iu by iu.MiddleSheet into g
                         //        join im in new Schema.MiddleSheet() on g.Key equals im.Key

                         // if we are part of a join. are we inner our outer?

                         if (GroupBy.upperJoin.xouter == GroupBy)
                         {
                             // we are outer?

                             //GroupBy.upperJoin.resultSelectorExpression as LambdaExpression)
                             asMemberInitExpression = ((LambdaExpression)GroupBy.upperJoin.selectorExpression).Body as MemberInitExpression;
                             asMemberInitExpressionByParameter0 = ((LambdaExpression)GroupBy.upperJoin.selectorExpression).Parameters[0];


                             if (asMemberInitExpression == null)
                             {
                                 // ???

                                 if (GroupBy.upperJoin.upperJoin.xouter == GroupBy.upperJoin)
                                 {
                                     asMemberInitExpression = ((LambdaExpression)GroupBy.upperJoin.upperJoin.selectorExpression).Body as MemberInitExpression;
                                     //asMemberInitExpressionByParameter0 = (GroupBy.upperJoin.upperJoin.resultSelectorExpression as LambdaExpression).Parameters[0];
                                     asMemberInitExpressionByParameter1 = ((LambdaExpression)GroupBy.upperJoin.upperJoin.selectorExpression).Parameters[0];



                                     if (asMemberInitExpression == null)
                                     {
                                         // ???

                                         if (GroupBy.upperJoin.upperJoin.upperJoin.xouter == GroupBy.upperJoin.upperJoin)
                                         {
                                             asMemberInitExpression = ((LambdaExpression)GroupBy.upperJoin.upperJoin.upperJoin.selectorExpression).Body as MemberInitExpression;
                                             //asMemberInitExpressionByParameter0 = (GroupBy.upperJoin.upperJoin.resultSelectorExpression as LambdaExpression).Parameters[0];
                                             asMemberInitExpressionByParameter2 = ((LambdaExpression)GroupBy.upperJoin.upperJoin.upperJoin.selectorExpression).Parameters[0];

                                         }



                                     }
                                 }



                             }

                             // [0x00000000] = {Content = g.Last().UpdatedContent}

                             //var parameter0 = GroupBy.upperJoin.


                             // (GroupBy.upperJoin.resultSelectorExpression as LambdaExpression).Body = {new <>f__AnonymousType0`2(UpdatesByMiddlesheet = UpdatesByMiddlesheet, MiddleSheetz = MiddleSheetz)}
                             ////((GroupBy.upperJoin.resultSelectorExpression as LambdaExpression).Body as NewExpression).With(
                             ////    __projection =>
                             ////    {
                             ////        // seems our upper join does not exactly know whats needed.
                             ////        // can we go up another level?

                             ////        var jj = GroupBy.upperJoin.upperJoin;

                             ////        state.SelectCommand += "???";

                             ////       // __projection.Arguments.WithEach(
                             ////       //     __projectionArgument =>
                             ////       //     {
                             ////       //         var __projectionParameterArgument = __projectionArgument as ParameterExpression;

                             ////       //         // are we supposed to flatten/ select for such upper projections?

                             ////       //     }
                             ////       //);
                             ////    }
                             ////);

                         }

                         if (GroupBy.upperJoin.xinner == GroupBy)
                         {
                             // we are inner?
                             Debugger.Break();
                         }
                     }
                     #endregion



                     // GroupBy_asMemberExpression = {<>h__TransparentIdentifier0.rJoin.ClientName}
                     // https://sites.google.com/a/jsc-solutions.net/backlog/knowledge-base/2014/201405/20140515

                     //+		GroupBy_asMemberExpression	{<>h__TransparentIdentifier0.rJoin.ClientName}	System.Linq.Expressions.MemberExpression {System.Linq.Expressions.FieldExpression}
                     // what if we are not grouping a join?


                     var GroupingKeyFieldExpressionName = default(string);

                     if (GroupBy_asMemberExpression != null)
                     {
                         var GroupingKeyFieldExpression = GroupBy_asMemberExpression.Expression as MemberExpression;

                         // X:\jsc.svn\examples\javascript\forms\Test\TestSQLGroupByAfterJoin\TestSQLGroupByAfterJoin\ApplicationWebService.cs


                         if (GroupingKeyFieldExpression != null)
                             GroupingKeyFieldExpressionName = "s.`" + GroupingKeyFieldExpression.Member.Name + "_" + GroupBy_asMemberExpression.Member.Name + "`";
                         else
                             GroupingKeyFieldExpressionName = "s.`" + GroupBy_asMemberExpression.Member.Name + "`";
                     }


                     var GroupBy_asC = GroupBy.keySelector.Body as ConstantExpression;
                     if (GroupBy_asC != null)
                     {
                         // X:\jsc.svn\examples\javascript\LINQ\MinMaxAverageExperiment\MinMaxAverageExperiment\ApplicationWebService.cs

                         if (GroupBy_asC.Value is int)
                             if ((int)GroupBy_asC.Value == 1)
                             {
                                 GroupingKeyFieldExpressionName = "1";
                             }
                     }


                     if (GroupingKeyFieldExpressionName == null)
                         Debugger.Break();

                     var s_SelectCommand = "select " +
                         GroupingKeyFieldExpressionName
                         + " as `Grouping.Key`";


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



                     #region WriteMemberExpression
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

                             #region let z <- Grouping.Key
                             var IsKey = asMemberExpression.Member.Name == "Key";

                             // if not a property we may still have the getter in JVM
                             IsKey |= asMemberExpression.Member.Name == "get_Key";

                             if (IsKey)
                             {
                                 // special!
                                 state.SelectCommand += ",\n\t g.`" + asMemberAssignment.Member.Name + "` as `" + asMemberAssignment.Member.Name + "`";

                                 s_SelectCommand += ",\n\t "
                                    + GroupingKeyFieldExpressionName + " as `" + asMemberAssignment.Member.Name + "`";
                                 return;
                             }
                             #endregion

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

                                         var refLast = new Func<IQueryStrategyGrouping<long, object>, object>(QueryStrategyOfTRowExtensions.Last);

                                         if (asMMMCall.Method.Name == refLast.Method.Name)
                                         {
                                             // group by is not supposed to rename.
                                             // select will do that.
                                             // x:\jsc.svn\examples\javascript\linq\test\testwherejointtgroupbyselectlast\testwherejointtgroupbyselectlast\applicationwebservice.cs
                                             // https://sites.google.com/a/jsc-solutions.net/backlog/knowledge-base/2014/201406/20140601

                                             state.SelectCommand += ",\n\t g.`" + asMMemberExpression.Member.Name + "_" + asMemberExpression.Member.Name + "`";
                                             s_SelectCommand += ",\n\t s.`" + asMMemberExpression.Member.Name + "_" + asMemberExpression.Member.Name + "`";
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
                             }
                             #endregion


                             //asMMemberExpression.Member
                             Debugger.Break();
                         };
                     #endregion

                     #region WriteExpression
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
                                     var asMMemberExpression = asMethodCallExpression.Object as MemberExpression;

                                     // X:\jsc.svn\examples\javascript\LINQ\test\TestGroupByThenOrderByThenOrderBy\TestGroupByThenOrderByThenOrderBy\ApplicationWebService.cs
                                     // does it matter when we do the to lower?
                                     // before group by
                                     // or after? or both?
                                     // whats the benefit?

                                     state.SelectCommand += ",\n\t g.`" + asMemberAssignment.Member.Name + "` as `" + asMemberAssignment.Member.Name + "`";
                                     s_SelectCommand += ",\n\t lower(s.`" + asMMemberExpression.Member.Name + "`) as `" + asMemberAssignment.Member.Name + "`";
                                     return;


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
                                     s_SelectCommand += ",\n\t "


                                         + GroupingKeyFieldExpressionName + " as `" + asMemberAssignment.Member.Name + "`";

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


                     #region asNewExpression
                     if (asMemberInitExpression == null)
                     {
                         var asNewExpression = (GroupBy.upperSelect.selectorExpression as LambdaExpression).Body as NewExpression;

                         asNewExpression.Arguments.WithEachIndex(
                             (SourceArgument, index) =>
                             {
                                 var TargetMember = asNewExpression.Members[index];
                                 var asMemberAssignment = new { Member = TargetMember };


                                 WriteExpression(index, SourceArgument, TargetMember);
                             }
                         );
                     }
                     #endregion


                     #region asMemberInitExpression



                     //var InitBinding = asMemberInitExpression.Bindings.Select

                     if (asMemberInitExpression != null)
                         asMemberInitExpression.Bindings.WithEachIndex(
                             (SourceBinding, index) =>
                             {
                                 var asMemberAssignment = SourceBinding as MemberAssignment;
                                 if (asMemberAssignment != null)
                                 {
                                     WriteExpression(index, asMemberAssignment.Expression, asMemberAssignment.Member);
                                     return;
                                 }
                                 Debugger.Break();
                             }
                         );
                     #endregion


                     var s = QueryStrategyExtensions.AsCommandBuilder(GroupBy.source);
                     // is it default?

                     #region  state.FromCommand

                     //                 from (select `Key`, `MiddleSheet`, `UpdatedContent`, `Tag`, `Timestamp`
                     //from `Schema.MiddleSheetUpdates`
                     //) as s  group by `Grouping.Key`


                     // https://sites.google.com/a/jsc-solutions.net/backlog/knowledge-base/2014/201405/20140515

                     var g = s_SelectCommand
                              + "\n from " + s.GetQualifiedTableNameOrToString().Replace("\n", "\n\t") + " as s "
                              + "\n group by `Grouping.Key`";

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

