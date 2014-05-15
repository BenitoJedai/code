using ScriptCoreLib;
using ScriptCoreLib.Delegates;
using ScriptCoreLib.Extensions;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using TestSQLGroupByAfterJoin.Data;

namespace TestSQLGroupByAfterJoin
{
    /// <summary>
    /// Methods defined in this type can be used from JavaScript. The method calls will seamlessly be proxied to the server.
    /// </summary>
    public partial class ApplicationWebService : Component
    {
        /// <summary>
        /// This Method is a javascript callable method.
        /// </summary>
        /// <param name="e">A parameter from javascript.</param>
        /// <param name="y">A callback to javascript.</param>
        public async Task<IEnumerable<DatabaseJoinViewRow>> WebMethod2()
        {

            var SourceDataSet = Database.GetDataSet();

            var leftTable = new Database.LeftTable();
            SourceDataSet.Tables["LeftTable"].Rows.AsEnumerable().WithEach(r => leftTable.Insert(r));

            var RightTable = new Database.RightTable();
            SourceDataSet.Tables["RightTable"].Rows.AsEnumerable().WithEach(r => RightTable.Insert(r));


            //var result0 = 
            //    from l in new Database.LeftTable().AsEnumerable()
            //     join rJoin in new Database.RightTable().AsEnumerable() on l.Key equals rJoin.ClientName
            //    group rJoin by rJoin.ClientName 
            //       ;

            //var result1 = result0.asd







            // public static IQueryStrategy<TResult> Join<TOuter, TInner, TKey, TResult>(
            // this IQueryStrategy<TOuter> xouter, 
            // IQueryStrategy<TInner> xinner, 
            // Expression<Func<TOuter, TKey>> outerKeySelector, 
            // Expression<Func<TInner, TKey>> innerKeySelector, 
            // Expression<Func<TOuter, TInner, TResult>> resultSelector);


            //var qq_join = QueryStrategyOfTRowExtensions.Join(
            //    xouter: new Database.LeftTable() //.AsEnumerable(),
            //    ,
            //    xinner: new Database.RightTable() //.AsEnumerable()
            //    ,
            //    outerKeySelector: l => l.Key,
            //    innerKeySelector: rJoin => rJoin.ClientName,
            //    resultSelector: (l, rJoin) => Tuple.Create(l, rJoin)
            //);

            ////IEnumerable<object>
            ////IEnumerable<IGrouping<DatabaseLeftTableKey, Tuple<DatabaseLeftTableRow, DatabaseRightTableRow>>> 

            //var
            //    qq_group =

            //// public static IEnumerable<TResult> GroupBy<TSource, TKey, TElement, TResult>(
            //    //      this IEnumerable<TSource> source, 
            //    //      Func<TSource, TKey> keySelector, 
            //    //      Func<TSource, TElement> elementSelector, 
            //    //      Func<TKey, IEnumerable<TElement>, TResult> resultSelector
            //    // );


            //QueryStrategyOfTRowExtensions.GroupBy
            //    //<Tuple<DatabaseLeftTableRow, DatabaseRightTableRow>, DatabaseLeftTableKey, Tuple<DatabaseLeftTableRow, DatabaseRightTableRow>, object>
            //(
            //    source:
            //        qq_join,
            //    keySelector:
            //    //new Func<Tuple<DatabaseLeftTableRow, DatabaseRightTableRow>, DatabaseLeftTableKey>(
            //            j => j.Item2.ClientName
            //    //)
            //        ,
            //    elementSelector:
            //    //new Func<Tuple<DatabaseLeftTableRow, DatabaseRightTableRow>, Tuple<DatabaseLeftTableRow, DatabaseRightTableRow>>(
            //            x => x
            //    //)
            //    //, resultSelector:
            //    ////new Func<DatabaseLeftTableKey, IEnumerable<Tuple<DatabaseLeftTableRow, DatabaseRightTableRow>>, object>(
            //    //    (key, elements) =>
            //    //        new { key, elements }
            //    ////)
            //);



            //IEnumerable<IGrouping<DatabaseLeftTableKey, Tuple<DatabaseRightTableRow, DatabaseLeftTableRow>>> q = 
            //    from l in new Database.LeftTable()
            //    join rJoin in new Database.RightTable()
            //    on l.Key equals rJoin.ClientName
            //    group Tuple.Create(rJoin, l) by rJoin.ClientName;

            //IEnumerable<IGrouping<DatabaseLeftTableKey, Tuple<DatabaseLeftTableRow, DatabaseRightTableRow>>> q = (


            // http://stackoverflow.com/questions/7325278/group-by-in-linq

            var tag = "???";
            var x = new { tag };

            var q =
                from l in new Database.LeftTable()
                //let tag =  "???" 
                join rJoin in new Database.RightTable()
                on l.Key equals rJoin.ClientName
                group new { l, rJoin
                    //, tag = "???" 
                } by rJoin.ClientName into result
                select new DatabaseJoinViewRow
                {
                    Tag = x.tag,


                    ClientName = result.Key,
                    //ClientName = rj

                    FirstName = result.Last().l.FirstName,
                    Payment = result.Last().rJoin.Payment,
                    //Tag = result.Last().rJoin.Tag,

                    //Tag = result.Last().tag,

                    Timestamp = result.Last().rJoin.Timestamp
                };
            //var q0 = q.ToArray();
            var q0 = q.AsDataTable();



            // stackoverflow.com/questions/18259750/sql-group-by-before-join-sequence-when-querying
            // Error	1	Could not find an implementation of the query pattern for source type 'ScriptCoreLib.Shared.Data.Diagnostics.IQueryStrategy<AnonymousType#1>'.  'GroupBy' not found.	X:\jsc.svn\examples\javascript\forms\Test\TestSQLGroupByAfterJoin\TestSQLGroupByAfterJoin\ApplicationWebService.cs	47	31	TestSQLGroupByAfterJoin
            // the fk?
            var a = (from l in new Database.LeftTable()
                     //.AsEnumerable()
                     //join rJoin in new Database.RightTable() on l.Key equals rJoin.ClientName // into test
                     join rJoin in new Database.RightTable()
                         //.AsEnumerable()
                     on l.Key equals rJoin.ClientName
                     // into test

                     // can we do away this select?
                     // our group by likes explicit views!
                     select new DatabaseJoinViewRow
                     {
                         ClientName = l.Key,
                         FirstName = l.FirstName,

                         //ClientName = result.Last().FirstName,
                         //ClientName = ((DatabaseLeftTableRow)l).FirstName,
                         Payment = rJoin.Payment,
                         Tag = rJoin.Tag,
                         Timestamp = rJoin.Timestamp
                     } into rJoin

                     //from z in test

                     //group test by test.ClientName into result

                     group rJoin by rJoin.ClientName into result
                     select result.Last()

                    //select new DatabaseJoinViewRow
                //{
                //    //ClientName = result.Last().FirstName,
                //    //ClientName = ((DatabaseLeftTableRow)l).FirstName,
                //    Payment = result.Last().Payment,
                //    Tag = result.Last().Tag,
                //    Timestamp = result.Last().Timestamp
                //}
            );


            var a0 = a.AsDataTable();

            var a1 = a.AsEnumerable();

            return a1;
        }
    }
}
