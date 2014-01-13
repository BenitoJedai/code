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

                var insertwatch = Stopwatch.StartNew();

                //Book1Extensions
                var TotalCount = new Book1.Sheet1().Count();
                var Goo = "Goo" + i;

                //var GooCount = new Book1.Sheet1().XXCount(x => x.Goo == Goo);
                //  public static long Where(this Book1Sheet1Strategy value, Expression<Func<object, object>> value);

                // what about where x or y?
                var GooCountStrategy = new Book1.Sheet1().Where(x => x.Goo == Goo);

                // show me the sql damit
                ScriptCoreLib.Shared.Data.Diagnostics.QueryStrategyExtensions.AsCommandBuilder(GooCountStrategy);

                var GooCount = new Book1.Sheet1().Count(x => x.Goo == Goo);
                var GooCount1 = new Book1.Sheet1().Where(x => x.Goo == Goo).Count();


                var k = new Book1.Sheet1().Insert(
                    new Book1Sheet1Row
                    {
                        Goo = Goo,
                        Value = new { TotalCount, GooCount }.ToString()
                    }
                );

                //                { insertwatch = 12 }
                //{ slowwatch = 13, ElapsedTicks = 23492, slow = 96, Goo, Count:95, , 1/2/2014 2:01:36 PM }

                if (i % len != 0)
                    continue;

                Console.WriteLine(new { insertwatch = insertwatch.ElapsedMilliseconds, TotalCount, GooCount });



                //var slowwatch = Stopwatch.StartNew();
                //var slow = new Book1.Sheet1().SelectAllAsEnumerable(

                //).ToArray().FirstOrDefault(x => x.Key == k);

                //Console.WriteLine(new { slowwatch = slowwatch.ElapsedMilliseconds, slowwatch.ElapsedTicks, slow });

                var fastwatch = Stopwatch.StartNew();
                var fast = ((Task<DataTable>)new Book1.Sheet1.Queries().WithConnection(
                    c =>
                    {
                        var CommandText = Book1.Sheet1.Queries.SelectAllCommandText.TakeUntilIfAny("order") + " where Key = @Key";



                        {
                            var cmd0 = new SQLiteCommand("explain query plan " + CommandText, c);
                            cmd0.Parameters.AddWithValue("Key", (long)k);

                            // ex = {"unknown error\r\nInsufficient parameters supplied to the command"}

                            var t = new DataTable();
                            var a = new global::System.Data.SQLite.SQLiteDataAdapter(cmd0);
                            a.Fill(t);
                            //return t.AsResult();
                        }

                        {
                            var cmd0 = new SQLiteCommand(CommandText, c);
                            cmd0.Parameters.AddWithValue("Key", (long)k);


                            var t = new DataTable();
                            var a = new global::System.Data.SQLite.SQLiteDataAdapter(cmd0);
                            a.Fill(t);
                            return t.AsResult();
                        }
                    }
                )).Result;
                Console.WriteLine(new { fastwatch = fastwatch.ElapsedMilliseconds, fastwatch.ElapsedTicks, fast });

                var nice = from q in new Book1.Sheet1()
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


                var tenPrevious = (
                    from q in new Book1.Sheet1()
                    where q.Key < k

                    // Error	5	Could not find an implementation of the query pattern for source type 'AppEngineWhereOperator.Design.Book1Sheet1Strategy'.  'OrderByDescending' not found.	X:\jsc.svn\examples\javascript\appengine\AppEngineWhereOperator\AppEngineWhereOperator\ApplicationWebService.cs	159	31	AppEngineWhereOperator
                    orderby q.Key descending

                    select q
                ).Take(5);


                Debugger.Break();
                //new Book1.Sheet1().AsE
                //nice.AsDataTable();



            }


        }


    }

}

