using AppEngineWhereOperator.Design;
using ScriptCoreLib;
using ScriptCoreLib.Delegates;
using ScriptCoreLib.Extensions;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SQLite;
using System.Diagnostics;
using System.Linq;
using System.Linq.Expressions;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace AppEngineWhereOperator
{
    /// <summary>
    /// Methods defined in this type can be used from JavaScript. The method calls will seamlessly be proxied to the server.
    /// </summary>
    public class ApplicationWebService
    {

        public void WebMethod2()
        {
            //{ insertwatch = 23010 }
            //{ slowwatch = 52, ElapsedTicks = 92360, slow = 1, Goo, Count:0, , 1/2/2014 1:55:28 PM }
            //{ insertwatch = 5 }
            //{ slowwatch = 1, ElapsedTicks = 2239, slow = 2, Goo, Count:1, , 1/2/2014 1:55:28 PM }
            //{ insertwatch = 18 }
            //{ slowwatch = 1, ElapsedTicks = 2270, slow = 3, Goo, Count:2, , 1/2/2014 1:55:28 PM }
            //{ insertwatch = 7 }
            //{ slowwatch = 2, ElapsedTicks = 3953, slow = 4, Goo, Count:3, , 1/2/2014 1:55:29 PM }
            //{ insertwatch = 7 }
            //{ slowwatch = 1, ElapsedTicks = 2215, slow = 5, Goo, Count:4, , 1/2/2014 1:55:29 PM }

            //{ insertwatch = 13 }
            //{ slowwatch = 3, ElapsedTicks = 6339, slow = 129, Goo, Count:128, , 1/2/2014 2:02:27 PM }
            //{ insertwatch = 8 }
            //{ slowwatch = 2, ElapsedTicks = 4827, slow = 161, Goo, Count:160, , 1/2/2014 2:02:27 PM }
            //{ insertwatch = 5 }
            //{ slowwatch = 3, ElapsedTicks = 5524, slow = 193, Goo, Count:192, , 1/2/2014 2:02:27 PM }
            //{ insertwatch = 5 }
            //{ slowwatch = 3, ElapsedTicks = 6746, slow = 225, Goo, Count:224, , 1/2/2014 2:02:28 PM }
            //{ insertwatch = 26 }
            //{ slowwatch = 5, ElapsedTicks = 8810, slow = 257, Goo, Count:256, , 1/2/2014 2:02:29 PM }
            //{ insertwatch = 31 }
            //{ slowwatch = 3, ElapsedTicks = 6327, slow = 289, Goo, Count:288, , 1/2/2014 2:02:29 PM }
            //{ insertwatch = 21 }
            //{ slowwatch = 4, ElapsedTicks = 7187, slow = 321, Goo, Count:320, , 1/2/2014 2:02:30 PM }

            //{ insertwatch = 2304 }
            //{ slowwatch = 21, ElapsedTicks = 37106, slow = 609, Goo, Count:608, , 1/2/2014 2:03:41 PM }
            //{ insertwatch = 9 }
            //{ slowwatch = 6, ElapsedTicks = 11672, slow = 641, Goo, Count:640, , 1/2/2014 2:03:43 PM }
            //{ insertwatch = 9 }
            //{ slowwatch = 11, ElapsedTicks = 20683, slow = 673, Goo, Count:672, , 1/2/2014 2:03:45 PM }
            //{ insertwatch = 9 }
            //{ slowwatch = 6, ElapsedTicks = 11830, slow = 705, Goo, Count:704, , 1/2/2014 2:03:46 PM }
            //{ insertwatch = 8 }
            //{ slowwatch = 7, ElapsedTicks = 12593, slow = 737, Goo, Count:736, , 1/2/2014 2:03:46 PM }
            //{ insertwatch = 19 }
            //{ slowwatch = 7, ElapsedTicks = 13704, slow = 769, Goo, Count:768, , 1/2/2014 2:03:46 PM }


            var len = 32;
            for (int i = 0; i < len * 8; i++)
            {
                WithinForLoopExpressionRewrite(len, i);



            }


        }

        private static void WithinForLoopExpressionRewrite(int len, int i)
        {
            #region within for loop
            var insertwatch = Stopwatch.StartNew();

            //Book1Extensions
            var TotalCount = new Book18.Sheet1().Count();
            var Goo = "Goo" + i;

            //var GooCount = new Book1.Sheet1().XXCount(x => x.Goo == Goo);
            //  public static long Where(this Book1Sheet1Strategy value, Expression<Func<object, object>> value);

            // what about where x or y?
            var GooCountStrategy = new Book18.Sheet1().Where(x => x.Goo == Goo);

            // show me the sql damit
            ScriptCoreLib.Shared.Data.Diagnostics.QueryStrategyExtensions.AsCommandBuilder(GooCountStrategy);

            var GooCount = new Book18.Sheet1().Count(x => x.Goo == Goo);
            var GooCount1 = new Book18.Sheet1().Where(x => x.Goo == Goo).Count();


            var k = new Book18.Sheet1().Insert(
                new Book18Sheet1Row
                {
                    Goo = Goo,
                    Value = new { TotalCount, GooCount }.ToString()
                }
            );

            //Book18.Sheet2.Insert(Deposit: 1);

            var z = new Book18.Sheet2().Insert(
                //Deposit: 1
                new Book18Sheet2Row { Deposit = 33 }
            );

            //                { insertwatch = 12 }
            //{ slowwatch = 13, ElapsedTicks = 23492, slow = 96, Goo, Count:95, , 1/2/2014 2:01:36 PM }

            if (i % len == 0)
            {

                Console.WriteLine(new { insertwatch = insertwatch.ElapsedMilliseconds, TotalCount, GooCount });



                //var slowwatch = Stopwatch.StartNew();
                //var slow = new Book18.Sheet1().SelectAllAsEnumerable(

                //).ToArray().FirstOrDefault(x => x.Key == k);

                //Console.WriteLine(new { slowwatch = slowwatch.ElapsedMilliseconds, slowwatch.ElapsedTicks, slow });

                //var fastwatch = Stopwatch.StartNew();
                //var fast = ((Task<DataTable>)new Book18.Sheet1.Queries().WithConnection(
                //    c =>
                //    {
                //        var CommandText = Book18.Sheet1.Queries.SelectAllCommandText.TakeUntilIfAny("order") + " where Key = @Key";



                //        {
                //        //    var cmd0 = new SQLiteCommand("explain query plan " + CommandText, c);
                //        //    cmd0.Parameters.AddWithValue("Key", (long)k);

                //        //    // ex = {"unknown error\r\nInsufficient parameters supplied to the command"}

                //        //    var t = new DataTable();
                //        //    var a = new global::System.Data.SQLite.SQLiteDataAdapter(cmd0);
                //        //    a.Fill(t);
                //        //    //return t.AsResult();
                //        //}

                //        {
                //            var cmd0 = new SQLiteCommand(CommandText, c);
                //            cmd0.Parameters.AddWithValue("Key", (long)k);


                //            var t = new DataTable();
                //            var a = new global::System.Data.SQLite.SQLiteDataAdapter(cmd0);
                //            a.Fill(t);
                //            return t.AsResult();
                //        }
                //    }
                //)).Result;
                //Console.WriteLine(new { fastwatch = fastwatch.ElapsedMilliseconds, fastwatch.ElapsedTicks, fast });

                var nice = from q in new Book18.Sheet1()
                           where q.Key == k
                           select q;

                var one = nice.Count();

                var onerow = nice.AsDataTable();
                var onerows = nice.AsEnumerable();

                // X:\jsc.svn\core\ScriptCoreLib.Extensions\ScriptCoreLib.Extensions\Shared\Data\Diagnostics\QueryStrategyExtensions.cs
                Console.WriteLine("tenPrevious");
                //                tenPrevious
                //MutableWhere { Method = , NodeType = LessThan, ColumnName = Key, Right = 2453 }
                //MutableOrderByDescending { selector = q => Convert(q.Key) }
                //MutableTake { count = 5 }

                //select `Key`, `Goo`, `Value`, `Tag`, `Timestamp`
                //from `Book18.Sheet1`
                // where `Key` = @arg0
                //order by `Key`
                //limit @arg1


                //Implementation not found for type import :
                //type: System.Linq.Expressions.Expression
                //method: System.Linq.Expressions.UnaryExpression Convert(System.Linq.Expressions.Expression, System.Type)
                //Did you forget to add the [Script] attribute?
                //Please double check the signature!

                //Task<Book18Sheet1Row> __implicit_FirstOrDefault =
                //(Task<Book18Sheet1Row>) 


                Console.WriteLine("\n__FirstOrDefault");

                Book18Sheet1Row __FirstOrDefault =
                    (
                    from q in new Book18.Sheet1()
                    where q.Key == k
                    select q
                    ).FirstOrDefault();

                Console.WriteLine(new { __FirstOrDefault });

                Task<Book18Sheet1Row> __implicit_FirstOrDefault =
                    from q in new Book18.Sheet1()
                    where q.Key == k
                    select q;


                Console.WriteLine(new { __implicit_FirstOrDefault = __implicit_FirstOrDefault.Result });

                Task<Book18Sheet1Key> __implicit_Key_FirstOrDefault =
                    from q in new Book18.Sheet1()
                    where q.Key == k
                    select q;

                Console.WriteLine(new { __implicit_Key_FirstOrDefault = __implicit_Key_FirstOrDefault.Result });


                var tenPrevious = (
                    // jsc itself needs to use this knowledge
                    // based on excel simulator
                    // can we start to deatch datamining like this?
                    // can we introduce new LINQ keywords in C# 6 as we did with extension methods?
                    from q in new Book18.Sheet1()
                    where q.Key < k

                    // Error	5	Could not find an implementation of the query pattern for source type 'AppEngineWhereOperator.Design.Book1Sheet1Strategy'.  'OrderByDescending' not found.	X:\jsc.svn\examples\javascript\appengine\AppEngineWhereOperator\AppEngineWhereOperator\ApplicationWebService.cs	159	31	AppEngineWhereOperator
                    //orderby q.Key descending

                    //+		[0]	{1, Goo, Count:0, , 1/2/2014 1:55:28 PM}	AppEngineWhereOperator.Design.Book1Sheet1Row
                    //+		[1]	{2, Goo, Count:1, , 1/2/2014 1:55:28 PM}	AppEngineWhereOperator.Design.Book1Sheet1Row
                    //+		[2]	{3, Goo, Count:2, , 1/2/2014 1:55:28 PM}	AppEngineWhereOperator.Design.Book1Sheet1Row
                    //+		[3]	{4, Goo, Count:3, , 1/2/2014 1:55:29 PM}	AppEngineWhereOperator.Design.Book1Sheet1Row
                    //+		[4]	{5, Goo, Count:4, , 1/2/2014 1:55:29 PM}	AppEngineWhereOperator.Design.Book1Sheet1Row

                    // first five
                    //orderby q.Key

                    // last five
                    orderby q.Key descending

                    select q

                )

                .Take(5);

                // what if we did a reverse here and want to run it on db layer?
                // the above query would become the subquery?
                // what about sharding?
                // a webservice is a huge stored proc.


                //+		[0]	{2461, Goo0, { TotalCount = 2460, GooCount = 26 }, , 1/14/2014 1:50:39 PM}	AppEngineWhereOperator.Design.Book1Sheet1Row
                //+		[1]	{2462, Goo0, { TotalCount = 2461, GooCount = 27 }, , 1/14/2014 1:52:03 PM}	AppEngineWhereOperator.Design.Book1Sheet1Row
                //+		[2]	{2463, Goo0, { TotalCount = 2462, GooCount = 28 }, , 1/14/2014 1:57:17 PM}	AppEngineWhereOperator.Design.Book1Sheet1Row
                //+		[3]	{2464, Goo0, { TotalCount = 2463, GooCount = 29 }, , 1/14/2014 2:01:18 PM}	AppEngineWhereOperator.Design.Book1Sheet1Row
                //+		[4]	{2465, Goo0, { TotalCount = 2464, GooCount = 30 }, , 1/14/2014 2:02:50 PM}	AppEngineWhereOperator.Design.Book1Sheet1Row

                // what about anonymous types and tuples, vec3. foo.xy = 4  
                // what about linq to css
                var tenPr = tenPrevious.AsEnumerable()


                    //.Reverse()
                    ;

                foreach (var item in tenPr)
                {
                    Console.WriteLine(item);
                }
                // can we also sum?


                //Y:\AppEngineWhereOperator.ApplicationWebService\staging.java\web\java\AppEngineWhereOperator\Design\Book1Extensions.java:82: error: incompatible types
                //        return QueryStrategyExtensions.Sum(_arg0, _arg1);
                //                                          ^
                //  required: Book1Sheet1Strategy
                //  found:    long
                //Y:\AppEngineWhereOperator.ApplicationWebService\staging.java\web\java\AppEngineWhereOperator\Design\Book18Extensions.java:167: error: incompatible types
                //        return QueryStrategyExtensions.Sum(_arg0, _arg1);
                //                                          ^
                //  required: Book18Sheet2Strategy
                //  found:    long

                var sum0 = new Book18.Sheet2().AsEnumerable().Sum(x => x.Deposit);
                var sum1 = new Book18.Sheet2().Sum(x => x.Deposit);

                // should we reuse ScriptCoreLib.Query namespace and make it a nuget?
                // we could move all code to ScriptCoreLib.Shared and mark the shared namespace as "semi merge within script"
                // would we also need be able to provide encrypted async key selector for the client side?
                // this would require us to serialize the Expression between Applcation and WebApplication
                // jsc needs to pay attention on the referenced typesystem and include missing types.


                //Caused by: java.lang.RuntimeException: { Message = Parameter index out of range (3 > number of parameters, which is 2)., StackTrace = java.sql.SQLException: Parameter index out of range (3 > number of parameters, which is 2).
                //        at com.google.cloud.sql.jdbc.internal.Exceptions.newSqlException(Exceptions.java:219)
                //        at com.google.cloud.sql.jdbc.internal.SqlProtoClient.check(SqlProtoClient.java:198)
                //        at com.google.cloud.sql.jdbc.internal.SqlProtoClient.executeSql(SqlProtoClient.java:87)
                //        at com.google.cloud.sql.jdbc.Connection.executeSql(Connection.java:561)
                //java.lang.RuntimeException
                //        at com.google.cloud.sql.jdbc.PreparedStatement.executeSqlImpl(PreparedStatement.java:143)
                //        at ScriptCoreLibJava.BCLImplementation.System.Reflection.__MethodInfo.InternalInvoke(__MethodInfo.java:93)
                //        at com.google.cloud.sql.jdbc.Statement.executeImpl(Statement.java:154)
                //        at ScriptCoreLibJava.BCLImplementation.System.Reflection.__MethodBase.Invoke(__MethodBase.java:69)
                //        at com.google.cloud.sql.jdbc.Statement.executeQuery(Statement.java:327)
                //        at ScriptCoreLib.Shared.BCLImplementation.System.__Func_2.Invoke(__Func_2.java:28)
                //        at com.google.cloud.sql.jdbc.PreparedStatement.executeQuery(PreparedStatement.java:128)
                //        at AppEngineWhereOperator.ApplicationWebService.WithinForLoopExpressionRewrite(ApplicationWebService.java:181)


                //Console.WriteLine()
                //Debugger.Break();
            }
            //new Book1.Sheet1().AsE
            //nice.AsDataTable();
            #endregion
        }


    }

}

