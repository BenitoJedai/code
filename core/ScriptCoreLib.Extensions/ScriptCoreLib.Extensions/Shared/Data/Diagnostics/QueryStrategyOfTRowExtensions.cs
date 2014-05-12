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
        // X:\jsc.svn\examples\javascript\forms\Test\TestSQLJoin\TestSQLJoin\ApplicationWebService.cs
        // X:\jsc.svn\examples\javascript\forms\Test\TestSQLiteGroupBy\TestSQLiteGroupBy\ApplicationWebService.cs

        #region XQueryStrategy
        class XQueryStrategy<TRow> : IQueryStrategy<TRow>
        {

            List<Action<QueryStrategyExtensions.CommandBuilderState>> InternalCommandBuilder = new List<Action<QueryStrategyExtensions.CommandBuilderState>>();

            public List<Action<QueryStrategyExtensions.CommandBuilderState>> GetCommandBuilder()
            {
                return InternalCommandBuilder;
            }

            public Func<IQueryDescriptor> InternalGetDescriptor;

            public IQueryDescriptor GetDescriptor()
            {
                //  public static DataTable AsDataTable(IQueryStrategy Strategy)

                return InternalGetDescriptor();
            }
        }
        #endregion



        [Obsolete("non grouping methods shall use FirstOrDefault")]
        public static TElement First<TKey, TElement>(this IQueryStrategyGrouping<TKey, TElement> source)
        {
            throw new NotImplementedException();
        }

        [Obsolete("non grouping methods shall use FirstOrDefault")]
        public static TElement Last<TKey, TElement>(this IQueryStrategyGrouping<TKey, TElement> source)
        {
            throw new NotImplementedException();
        }

        #region Sum
        public static long Sum<TKey, TElement>(this IQueryStrategyGrouping<TKey, TElement> source, Expression<Func<TElement, long>> f)
        {
            throw new NotImplementedException();
        }

        public static double Sum<TKey, TElement>(this IQueryStrategyGrouping<TKey, TElement> source, Expression<Func<TElement, double>> f)
        {
            throw new NotImplementedException();
        }
        #endregion




        // X:\jsc.svn\core\ScriptCoreLib\Shared\BCLImplementation\System\Linq\Enumerable.Methods.cs

        //public static IEnumerable<TResult> Join<TOuter, TInner, TKey, TResult>(
        //    this IEnumerable<TOuter> outer,
        //    IEnumerable<TInner> inner,
        //    Func<TOuter, TKey> outerKeySelector,
        //    Func<TInner, TKey> innerKeySelector,
        //    Func<TOuter, TInner, TResult> resultSelector)


        // public static Book1.DealerContact Where(this Book1.DealerContact value, Expression<Func<Book1DealerContactRow, bool>> value);
        // https://sites.google.com/a/jsc-solutions.net/backlog/knowledge-base/2014/201404/20140429

        // Error	4	Could not find an implementation of the query pattern for source type 
        // 'TestSQLJoin.Data.Book1.DealerContact'.  'Join' not found.	X:\jsc.svn\examples\javascript\forms\Test\TestSQLJoin\TestSQLJoin\ApplicationWebService.cs	51	33	TestSQLJoin


        // http://social.msdn.microsoft.com/Forums/en-US/bf98ec7a-cb80-4901-8eb2-3aa6636a4fde/linq-join-error-the-type-of-one-of-the-expressions-in-the-join-clause-is-incorrect-type-inference?forum=linqprojectgeneral
        // http://weblogs.asp.net/rajbk/archive/2010/03/12/joins-in-linq-to-sql.aspx
        // http://msdn.microsoft.com/en-us/library/bb311040.aspx
        // http://thomashundley.com/post/2010/05/20/The-type-of-one-of-the-expressions-in-the-join-clause-is-incorrect-Type-inference-failed-in-the-call-to-Join.aspx
        // http://www.roelvanlisdonk.nl/?p=2904
        // is this it?
        // http://www.pcreview.co.uk/forums/linq-join-using-expression-tree-t3432559.html

        //[Obsolete("whats the correct signature?")]
        //public static IEnumerable<TestSQLJoin.Data.Book1TheViewRow> Join<TKey>(

        // do we need  IQueryable<> ?

        //[Obsolete("can we get rid of the return type too? how would that look like?")]





        #region GroupBy
        public static IQueryStrategy<TResult>
            Select
            <TSource, TKey, TResult>
            (
             this IQueryStrategyGroupingBuilder<TKey, TSource> GroupBy,
             Expression<Func<IQueryStrategyGrouping<TKey, TSource>, TResult>> keySelector)
        {
            // source = {TestSQLiteGroupBy.X.XQueryStrategy<System.Linq.IGrouping<TestSQLiteGroupBy.Data.GooStateEnum,TestSQLiteGroupBy.Data.Book1MiddleRow>>}
            // keySelector = {GroupByGoo => new Book1MiddleAsGroupByGooWithCountRow() {GooStateEnum = GroupByGoo.Key, Count = Convert(GroupByGoo.Count())}}

            // we are about to create a view just like we do in the join.
            // http://stackoverflow.com/questions/9287119/get-first-row-for-one-group


            //select GooStateEnum, count(*)
            //from `Book1.Middle`


            var GroupBy_asMemberExpression = GroupBy.keySelector.Body as MemberExpression;


            //-		Bindings	Count = 0x00000007	System.Collections.ObjectModel.ReadOnlyCollection<System.Linq.Expressions.MemberBinding> {System.Runtime.CompilerServices.TrueReadOnlyCollection<System.Linq.Expressions.MemberBinding>}

            //+		[0x00000000]	{GooStateEnum = GroupByGoo.Key}	System.Linq.Expressions.MemberBinding {System.Linq.Expressions.MemberAssignment}

            //+		[0x00000001]	{FirstTitle = GroupByGoo.First().Title}	System.Linq.Expressions.MemberBinding {System.Linq.Expressions.MemberAssignment}
            //+		[0x00000002]	{FirstKey = Convert(GroupByGoo.First())}	System.Linq.Expressions.MemberBinding {System.Linq.Expressions.MemberAssignment}
            //+		[0x00000003]	{LastKey = Convert(GroupByGoo.Last())}	System.Linq.Expressions.MemberBinding {System.Linq.Expressions.MemberAssignment}
            //+		[0x00000004]	{LastTitle = GroupByGoo.Last().Title}	System.Linq.Expressions.MemberBinding {System.Linq.Expressions.MemberAssignment}
            //+		[0x00000005]	{SumOfx = GroupByGoo.Sum(u => u.x)}	System.Linq.Expressions.MemberBinding {System.Linq.Expressions.MemberAssignment}
            //+		[0x00000006]	{Count = GroupByGoo.Count()}	System.Linq.Expressions.MemberBinding {System.Linq.Expressions.MemberAssignment}


            //        Y:\TestSQLiteGroupBy.ApplicationWebService\staging.java\web\java\TestSQLiteGroupBy\X___c__DisplayClass4_3___c__DisplayClass6.java:200: error: ';' expected
            //private static __MethodCallExpression _<Select>b__3_Isinst_001c(Object _001c)
            //                                       ^

            var that = new XQueryStrategy<TResult>
            {


                InternalGetDescriptor =
                    () =>
                    {
                        // inherit the connection/context from above
                        var StrategyDescriptor = GroupBy.source.GetDescriptor();

                        return StrategyDescriptor;
                    }
            };

            //    Caused by: java.lang.RuntimeException: { Message = Duplicate column name 'Key', StackTrace = java.sql.SQLException: Duplicate column name 'Key'
            //at com.google.cloud.sql.jdbc.internal.Exceptions.newSqlException(Exceptions.java:219)


            // X:\jsc.svn\core\ScriptCoreLib.Extensions\ScriptCoreLib.Extensions\Shared\Data\Diagnostics\QueryStrategyExtensions.cs
            that.GetCommandBuilder().Add(
                 state =>
                 {
                     var s = QueryStrategyExtensions.AsCommandBuilder(GroupBy.source);

                     // http://www.xaprb.com/blog/2006/12/07/how-to-select-the-firstleastmax-row-per-group-in-sql/


                     // for the new view
                     // count is easy. 
                     // views should not care about keys, tags and timestamps?

                     // well the last seems to work
                     // not the first.


                     // Caused by: java.lang.RuntimeException: { Message = Every derived table must have its own alias,

                     state.SelectCommand =
                         //"select g.GooStateEnum as GooStateEnum,\n\t"
                         "select g.`Grouping.Key`";



                     ////+ "g.Count as Count,\n\t"


                     // + "g.`Key` as LastKey,\n\t"

                     //+ "g.x as Lastx,\n\t"
                     //+ "g.Title as LastTitle,\n\t"

                     //// aint working
                     //+ "gDescendingByKey.Key as FirstKey,\n\t"
                     //+ "gDescendingByKey.x as Firstx,\n\t"
                     //+ "gDescendingByKey.Title as FirstTitle,\n\t"

                     //+ "g.SumOfx as SumOfx,\n\t"

                     //+ "'' as Tag, 0 as Timestamp\n\t";


                     var gDescendingByKeyReferenced = false;


                     // http://www.w3schools.com/sql/sql_func_last.asp
                     var s_SelectCommand = "select s.`" + GroupBy_asMemberExpression.Member.Name + "` as `Grouping.Key`";

                     //+ "s.x,\n\t"
                     // // specialname
                     //+ "s.`Key`,\n\t"
                     //+ "s.Title,\n\t"
                     // //+ "s.GooStateEnum,\n\t"
                     // + "sum(s.x) as SumOfx,\n\t";
                     //+ "13 as SumOfx, "
                     //+ "count(*) as Count";


                     // +		keySelector.Body	{ug.Last()}	System.Linq.Expressions.Expression {System.Linq.Expressions.MethodCallExpressionN}

                     {
                         var asMethodCallExpression = keySelector.Body as MethodCallExpression;
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

                     #region asMemberInitExpression
                     var asMemberInitExpression = keySelector.Body as MemberInitExpression;
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

                                                 var asMPropertyInfo = asMMemberExpression.Member as FieldInfo;
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
                                             }
                                             #endregion



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
                                     }
                                     #endregion





                                 }





                                 Debugger.Break();
                             }
                         );
                     #endregion


                     // how do we get the first and the last in the same query??


                     //+ "3 as Count";

                     // error: { Message = no such column: g.GooStateEnum, ex = System.Data.SQLite.SQLiteSyntaxException (0x80004005): no such column: g.GooStateEnum

                     // http://stackoverflow.com/questions/27983/sql-group-by-with-an-order-by
                     // MySQL prior to version 5 did not allow aggregate functions in ORDER BY clauses.


                     //s.OrderByCommand = "order by GooStateEnum desc";

                     // what about distinct? 
                     // we cannot reorder the table by the grouping item
                     // we would have to rely on PK Key? either assume Key was generated by AssetsLibrary
                     // or crash or inspect the table by explain

                     //s.GroupByCommand = "group by GooStateEnum";


                     // http://www.afterhoursprogramming.com/tutorial/SQL/ORDER-BY-and-GROUP-BY/
                     // CANNOT limit nor order if we are about to group.

                     //s.LimitCommand = "limit 1";


                     //select g.GooStateEnum as GooStateEnum, g.Count as Count
                     //from (
                     //        select GooStateEnum, count(*) as Count
                     //        from `Book1.Middle`
                     //         where `FooStateEnum` = @arg0 and `Ratio` > @arg1 and `Ratio` < @arg2


                     //        group by GooStateEnum
                     //        ) as g


                     // how can we pass arguments to the flattened where?\
                     // g seems to be last inserted?



                     //                 var FromCommand =
                     //"from (\n\t"
                     //    + xouter_SelectAll.ToString().Replace("\n", "\n\t")
                     //    + ") as " + xouter_Paramerer_Name.Replace("<>", "__") + " "

                     //    + "\ninner join (\n\t"
                     //    + xinner_SelectAll.ToString().Replace("\n", "\n\t")
                     //    + ") as " + xinner_Paramerer.Name.Replace("<>", "__");


                     var g = s_SelectCommand
                         + "\n from (" + s.ToString().Replace("\n", "\n\t") + ") as s "
                         + " group by `Grouping.Key`";

                     state.FromCommand =
                          "from (\n\t"
                            + g.Replace("\n", "\n\t")
                            + "\n) as g";

                     if (gDescendingByKeyReferenced)
                     {
                         // omit if we aint using it

                         // ? this wont work on a join!!
                         var gDescendingByKey = s_SelectCommand
                             + "\n from (select * from (" + s.ToString().Replace("\n", "\n\t") + ") order by `Key` desc) as s "
                           + " group by `Grouping.Key`";

                         state.FromCommand +=
                                 "\n inner join (\n\t"
                                + gDescendingByKey.Replace("\n", "\n\t")
                                + "\n) as gDescendingByKey"
                                + "\n on g.`Grouping.Key` = gDescendingByKey.`Grouping.Key`";
                     }

                     // http://msdn.microsoft.com/en-us/library/vstudio/bb386996(v=vs.100).aspx


                     // hack it. no longer useable later
                     // http://help.sap.com/abapdocu_702/en/abaporderby_clause.htm#!ABAP_ALTERNATIVE_1@1@
                     //  ORDER BY { {PRIMARY KEY}

                     //s.FromCommand = "from (select * " + s.FromCommand + " order by PRIMARY KEY desc)";



                     //state.FromCommand += " on g.GooStateEnum = gDescendingByKey.GooStateEnum";
                     //state.FromCommand += " on g.`" + GroupBy_asMemberExpression.Member.Name + "` = gDescendingByKey.`" + GroupBy_asMemberExpression.Member.Name + "`";
                     //state.FromCommand += " on g.`Grouping.Key` = gDescendingByKey.`Grouping.Key`";


                     //select g.GooStateEnum as GooStateEnum, g.Key as LastKey, g.x as Lastx, g.Title as LastTitle, gDescendingByKey.Key as FirstKey, gDescendingByKey.x as Firstx, gDescendingByKey.Title as FirstTitle, g.Count as Count, '' as Tag, 0 as Timestamp
                     //from (
                     //        select x,Key, Title, GooStateEnum, count(*) as Count
                     //        from `Book1.Middle`
                     //         where `FooStateEnum` = @arg0 and `Ratio` = @arg1


                     //        group by GooStateEnum

                     //) as g inner join (
                     //        select x,Key, Title, GooStateEnum, count(*) as Count
                     //        from (select * from `Book1.Middle` order by Key desc)
                     //         where `FooStateEnum` = @arg0 and `Ratio` = @arg1


                     //        group by GooStateEnum

                     //) as gDescendingByKey on g.GooStateEnum = gDescendingByKey.GooStateEnum




                     ////state.FromCommand += ", (\n\t"
                     ////       + s.ToString().Replace("\n", "\n\t")
                     ////       + "\n) as gFirst ";



                     // copy em?
                     state.ApplyParameter.AddRange(s.ApplyParameter);

                 }
             );


            return that;
        }

        public static IQueryStrategyGroupingBuilder<TKey, TSource>
            GroupBy
            <TSource, TKey>
            (
         this IQueryStrategy<TSource> source,
         Expression<Func<TSource, TKey>> keySelector)
        {
            return new XQueryStrategyGroupingBuilder<TKey, TSource> { source = source, keySelector = keySelector };
        }
        #endregion


    }
}

