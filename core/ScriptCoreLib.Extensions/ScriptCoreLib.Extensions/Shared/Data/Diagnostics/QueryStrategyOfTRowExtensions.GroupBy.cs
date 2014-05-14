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
        interface IGroupByQueryStrategy
        {
            // allow to inspect upper select . what if there are multiple upper selects?
            ISelectQueryStrategy upperSelect { get; set; }

            // what if we are in a join?
            IJoinQueryStrategy upperJoin { get; set; }
        }

        class GroupByQueryStrategy<TSource, TKey> :
            XQueryStrategy<IQueryStrategyGrouping<TKey, TSource>>,
            IGroupByQueryStrategy
        {
            public IQueryStrategy<TSource> source;
            public Expression<Func<TSource, TKey>> keySelector;

            public ISelectQueryStrategy upperSelect { get; set; }
            public IJoinQueryStrategy upperJoin { get; set; }
        }


        //public static IQueryStrategyGroupingBuilder<TKey, TSource>
        public static IQueryStrategy<IQueryStrategyGrouping<TKey, TSource>>
                GroupBy
                <TSource, TKey>
                (
             this IQueryStrategy<TSource> source,
             Expression<Func<TSource, TKey>> keySelector
            )
        {
            // https://sites.google.com/a/jsc-solutions.net/backlog/knowledge-base/2014/201405/20140513

            Console.WriteLine("GroupBy " + new { keySelector });

            var GroupBy = new GroupByQueryStrategy<TSource, TKey>
            {
                source = source,
                keySelector = keySelector,

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
                     // do we even know what needs to be selected?

                     // GroupBy.upperSelect.selectorExpression = {g => new SchemaViewsMiddleViewRow() {Content = g.Last().UpdatedContent}}

                     //Console.WriteLine("GroupBy CommandBuilder " + new { GroupBy.upperSelect.selectorExpression });
                     Console.WriteLine("GroupBy CommandBuilder");



                     var GroupBy_asMemberExpression = GroupBy.keySelector.Body as MemberExpression;






                     // do we need to select it?
                     state.SelectCommand = "select g.`Grouping.Key`";

                     // this can be disabled/enabled by a upper select?
                     var gDescendingByKeyReferenced = false;

                     // move to equals comparer?
                     var s_SelectCommand = "select s.`" + GroupBy_asMemberExpression.Member.Name + "` as `Grouping.Key`";

                     #region asMethodCallExpression
                     if (GroupBy.upperSelect != null)
                     {
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
                     }
                     #endregion


                     #region asMemberInitExpression
                     var asMemberInitExpression = default(MemberInitExpression);
                     var asMemberInitExpressionByParameter0 = default(ParameterExpression);
                     var asMemberInitExpressionByParameter1 = default(ParameterExpression);
                     var asMemberInitExpressionByParameter2 = default(ParameterExpression);

                     if (GroupBy.upperSelect != null)
                         asMemberInitExpression = (GroupBy.upperSelect.selectorExpression as LambdaExpression).Body as MemberInitExpression;


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
                             asMemberInitExpression = (GroupBy.upperJoin.resultSelectorExpression as LambdaExpression).Body as MemberInitExpression;
                             asMemberInitExpressionByParameter0 = (GroupBy.upperJoin.resultSelectorExpression as LambdaExpression).Parameters[0];


                             if (asMemberInitExpression == null)
                             {
                                 // ???

                                 if (GroupBy.upperJoin.upperJoin.xouter == GroupBy.upperJoin)
                                 {
                                     asMemberInitExpression = (GroupBy.upperJoin.upperJoin.resultSelectorExpression as LambdaExpression).Body as MemberInitExpression;
                                     //asMemberInitExpressionByParameter0 = (GroupBy.upperJoin.upperJoin.resultSelectorExpression as LambdaExpression).Parameters[0];
                                     asMemberInitExpressionByParameter1 = (GroupBy.upperJoin.upperJoin.resultSelectorExpression as LambdaExpression).Parameters[0];



                                     if (asMemberInitExpression == null)
                                     {
                                         // ???

                                         if (GroupBy.upperJoin.upperJoin.upperJoin.xouter == GroupBy.upperJoin.upperJoin)
                                         {
                                             asMemberInitExpression = (GroupBy.upperJoin.upperJoin.upperJoin.resultSelectorExpression as LambdaExpression).Body as MemberInitExpression;
                                             //asMemberInitExpressionByParameter0 = (GroupBy.upperJoin.upperJoin.resultSelectorExpression as LambdaExpression).Parameters[0];
                                             asMemberInitExpressionByParameter2 = (GroupBy.upperJoin.upperJoin.upperJoin.resultSelectorExpression as LambdaExpression).Parameters[0];

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


                     if (asMemberInitExpression != null)
                         asMemberInitExpression.Bindings.WithEachIndex(
                             (SourceBinding, index) =>
                             {
                                 //{ index = 0, asMemberAssignment = MemberAssignment { Expression = MemberExpression { expression = ParameterExpression { type = TestSQLiteGroupBy.IQueryStrategyGrouping_2, name = GroupByGoo }, field =
                                 //{ index = 0, asMemberExpression = MemberExpression { expression = ParameterExpression { type = TestSQLiteGroupBy.IQueryStrategyGrouping_2, name = GroupByGoo }, field = java.lang.Object get_Key() } }
                                 //{ index = 0, Name = get_Key }
                                 //{ index = 0, asMemberExpressionMethodCallExpression =  }
                                 //{ index = 0, asUnaryExpression =  }

                                 //{ index = 0, asMemberAssignment = GooStateEnum = GroupByGoo.Key }
                                 //{ index = 0, asMemberExpression = GroupByGoo.Key }

                                 //{ index = 1, asMemberAssignment = Count = GroupByGoo.Count() }
                                 //{ index = 1, Method = Int64 Count(ScriptCoreLib.Shared.Data.Diagnostics.IQueryStrategy`1[TestSQLite

                                 //{ index = 1, asMemberAssignment = MemberAssignment { Expression = MethodCallExpression { Object = , Method = long Count_060000af(ScriptCoreLib.Shared.Data.Diagnostics.IQueryStrategy_1) } } }
                                 //{ index = 1, Method = long Count_060000af(ScriptCoreLib.Shared.Data.Diagnostics.IQueryStrategy_1) }
                                 //{ index = 1, asMemberExpression =  }
                                 //{ index = 1, asUnaryExpression =  }

                                 //{ index = 2, asMemberAssignment = FirstTitle = GroupByGoo.First().Title }
                                 //{ index = 2, asMemberExpression = GroupByGoo.First().Title }
                                 //{ index = 3, asMemberAssignment = FirstKey = Convert(GroupByGoo.First()) }
                                 //{ index = 3, asMemberExpression =  }
                                 //{ index = 3, asUnaryExpression = Convert(GroupByGoo.First()) }
                                 //{ index = 3, Method = TestSQLiteGroupBy.Data.Book1MiddleRow First[GooStateEnum,Book1MiddleRow](Test
                                 //{ index = 4, asMemberAssignment = Firstx = GroupByGoo.First().x }
                                 //{ index = 4, asMemberExpression = GroupByGoo.First().x }
                                 //{ index = 5, asMemberAssignment = LastKey = Convert(GroupByGoo.Last()) }
                                 //{ index = 5, asMemberExpression =  }
                                 //{ index = 5, asUnaryExpression = Convert(GroupByGoo.Last()) }
                                 //{ index = 5, Method = TestSQLiteGroupBy.Data.Book1MiddleRow Last[GooStateEnum,Book1MiddleRow](TestS
                                 //{ index = 6, asMemberAssignment = LastTitle = GroupByGoo.Last().Title }
                                 //{ index = 6, asMemberExpression = GroupByGoo.Last().Title }

                                 //{ index = 7, asMemberAssignment = Lastx = GroupByGoo.Last().x }
                                 //{ index = 7, asMemberExpression = GroupByGoo.Last().x }
                                 //{ index = 7, Member = Double x, Name = x }
                                 //{ index = 7, asMemberExpressionMethodCallExpression = GroupByGoo.Last() }
                                 //{ index = 7, asMemberExpressionMethodCallExpression = GroupByGoo.Last(), Name = Last }

                                 //{ index = 7, asMemberAssignment = MemberAssignment { Expression = MemberExpression { expression = MethodCallExpression { Object = , Method = java.lang.Object Last(TestSQLiteGroupBy.IQueryStrategyGrouping_2) }, field = double x }
                                 //{ index = 7, asMemberExpression = MemberExpression { expression = MethodCallExpression { Object = , Method = java.lang.Object Last(TestSQLiteGroupBy.IQueryStrategyGrouping_2) }, field = double x } }
                                 //{ index = 7, Member = double x, Name = x }
                                 //{ index = 7, asMemberExpressionMethodCallExpression = MethodCallExpression { Object = , Method = java.lang.Object Last(TestSQLiteGroupBy.IQueryStrategyGrouping_2) } }


                                 //{ index = 8, asMemberAssignment = SumOfx = GroupByGoo.Sum(u => u.x) }
                                 //{ index = 8, Method = Double Sum[GooStateEnum,Book1MiddleRow](TestSQLiteGroupBy.IQueryStrategyGroup

                                 //{ index = 8, asMemberAssignment = MemberAssignment { Expression = MethodCallExpression { Object = , Method = double Sum_06000128(TestSQLiteGroupBy.IQueryStrategyGrouping_2, ScriptCoreLi
                                 //{ index = 8, Method = double Sum_06000128(TestSQLiteGroupBy.IQueryStrategyGrouping_2, ScriptCoreLib.Shared.BCLImplementation.System.Linq.Expressions.__Expression_1) }
                                 //{ index = 8, asMemberExpression =  }
                                 //{ index = 8, asUnaryExpression =  }


                                 //{ index = 9, asMemberAssignment = Tag = GroupByGoo.Last().Tag }
                                 //{ index = 9, asMemberExpression = GroupByGoo.Last().Tag }
                                 //{ index = 10, asMemberAssignment = Timestamp = GroupByGoo.Last().Timestamp }
                                 //{ index = 10, asMemberExpression = GroupByGoo.Last().Timestamp }


                                 //                     Caused by: java.lang.RuntimeException: System.Diagnostics.Debugger.Break
                                 //at ScriptCoreLibJava.BCLImplementation.System.Diagnostics.__Debugger.Break(__Debugger.java:31)
                                 //at TestSQLiteGroupBy.X___c__DisplayClass4_3___c__DisplayClass6._Select_b__3(X___c__DisplayClass4_3___c__DisplayClass6.java:197)





                                 // count and key
                                 var asMemberAssignment = SourceBinding as MemberAssignment;
                                 Console.WriteLine(new { index, asMemberAssignment });
                                 if (asMemberAssignment != null)
                                 {


                                     #region asMConstantExpression
                                     {
                                         var asMConstantExpression = asMemberAssignment.Expression as ConstantExpression;
                                         if (asMConstantExpression != null)
                                         {
                                             var asMPropertyInfo = asMemberAssignment.Member as FieldInfo;
                                             //var value1 = asMPropertyInfo.GetValue(asMConstantExpression.Value);
                                             var value1 = asMConstantExpression.Value;

                                             if (value1 is string)
                                             {
                                                 // NULL?
                                                 state.SelectCommand += ",\n\t '" + value1 + "' as `" + asMemberAssignment.Member.Name + "`";
                                             }
                                             else
                                             {
                                                 // long?
                                                 state.SelectCommand += ",\n\t " + value1 + " as `" + asMemberAssignment.Member.Name + "`";
                                             }

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

                                     }
                                     #endregion



                                     #region asMemberExpression
                                     {
                                         // m_getterMethod = {TestSQLiteGroupBy.Data.GooStateEnum get_Key()}

                                         var asMemberExpression = asMemberAssignment.Expression as MemberExpression;
                                         Console.WriteLine(new { index, asMemberExpression });
                                         if (asMemberExpression != null)
                                         {
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
                                                 state.SelectCommand += ",\n\t g.`" + asMemberAssignment.Member.Name + "`";

                                                 s_SelectCommand += ",\n\t s.`" + GroupBy_asMemberExpression.Member.Name + "` as `" + asMemberAssignment.Member.Name + "`";
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
                                                 var value1 = asMPropertyInfo.GetValue(asMConstantExpression.Value);

                                                 if (value1 is string)
                                                 {
                                                     // NULL?
                                                     state.SelectCommand += ",\n\t '" + value1 + "' as `" + asMemberAssignment.Member.Name + "`";
                                                 }
                                                 else
                                                 {
                                                     // long?
                                                     state.SelectCommand += ",\n\t " + value1 + " as `" + asMemberAssignment.Member.Name + "`";
                                                 }

                                                 return;
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

                                                 var asMPropertyInfo = asMMemberExpression.Member as FieldInfo;

                                                 #region asPropertyInfo
                                                 var asPropertyInfo = asMemberExpression.Member as PropertyInfo;
                                                 if (asPropertyInfo != null)
                                                 {
                                                     // CLR

                                                     var asC = asMMemberExpression.Expression as ConstantExpression;

                                                     // Member = {<>f__AnonymousType0`1[System.String] SpecialConstant}

                                                     var value0 = asMPropertyInfo.GetValue(asC.Value);
                                                     var value1 = asPropertyInfo.GetValue(value0, null);


                                                     if (value1 is string)
                                                     {
                                                         // NULL?
                                                         state.SelectCommand += ",\n\t '" + value1 + "' as `" + asMemberAssignment.Member.Name + "`";
                                                     }
                                                     else
                                                     {
                                                         // long?
                                                         state.SelectCommand += ",\n\t " + value1 + " as `" + asMemberAssignment.Member.Name + "`";
                                                     }

                                                     return;
                                                 }
                                                 #endregion

                                             }
                                             #endregion

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

                                             //asMMemberExpression.Member
                                             Debugger.Break();
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
                                             s_SelectCommand += ",\n\t s.`" + GroupBy_asMemberExpression.Member.Name + "` as `" + asMemberAssignment.Member.Name + "`";

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

                     var g = s_SelectCommand
                              + "\n from " + s.GetQualifiedTableNameOrToString().Replace("\n", "\n\t") + " as s "
                              + "\n group by s.`" + GroupBy_asMemberExpression.Member.Name + "`";

                     state.FromCommand =
                          "from (\n\t"
                            + g.Replace("\n", "\n\t")
                            + "\n) as g";


                     #region gDescendingByKeyReferenced
                     if (gDescendingByKeyReferenced)
                     {
                         // omit if we aint using it

                         // ? this wont work on a join!!
                         var gDescendingByKey = s_SelectCommand
                             + "\n from (select * from (" + s.ToString().Replace("\n", "\n\t") + ") order by `Key` desc) as s "
                           + "\n group by `Grouping.Key`";

                         state.FromCommand +=
                                 "\n inner join (\n\t"
                                + gDescendingByKey.Replace("\n", "\n\t")
                                + "\n) as gDescendingByKey"
                                + "\n on g.`Grouping.Key` = gDescendingByKey.`Grouping.Key`";
                     }
                     #endregion

                     #endregion


                     state.ApplyParameter.AddRange(s.ApplyParameter);
                 }
            );


            return GroupBy;
            //return new XQueryStrategyGroupingBuilder<TKey, TSource> { source = source, keySelector = keySelector };
        }

    }
}

