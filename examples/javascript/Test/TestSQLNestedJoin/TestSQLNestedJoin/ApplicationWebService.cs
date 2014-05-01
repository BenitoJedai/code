using ScriptCoreLib;
using ScriptCoreLib.Delegates;
using ScriptCoreLib.Extensions;
using ScriptCoreLib.Shared.Data.Diagnostics;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Linq.Expressions;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using TestSQLNestedJoin.Data;

namespace TestSQLNestedJoin
{
    /// <summary>
    /// Methods defined in this type can be used from JavaScript. The method calls will seamlessly be proxied to the server.
    /// </summary>
    public class ApplicationWebService
    {
        public
              async
              Task<IEnumerable<Book1TheViewRow>> GetTheViewData()
        {
            #region InsertDemoData
            Action InsertDemoData = delegate
            {
                new[] { 
                    new Book1DealerContactRow { DealerId = 2, DealerContactText = ""},
                    new Book1DealerContactRow { DealerId = 3, DealerContactText = "hello "},
                    new Book1DealerContactRow { DealerId = 4, DealerContactText = ""}
                }.Select(new __Book1_DealerContact().Insert).ToArray();



                new[] { 
                    new Book1DealerRow { ID = 1, DealerText = ""},
                    new Book1DealerRow { ID = 3, DealerText = "world"},
                    new Book1DealerRow { ID = 5, DealerText = ""}
                }.Select(new __Book1_Dealer().Insert).ToArray();


                new[] { 
                    new Book1DealerOtherRow { ID = 0, DealerOtherText = ""},
                    new Book1DealerOtherRow { ID = 3, DealerOtherText = "!!"},
                    new Book1DealerOtherRow { ID = 20, DealerOtherText = ""}
                }.Select(new Book1.DealerOther().Insert).ToArray();
            };


            InsertDemoData();
            #endregion


            //var DealerContact = new Book1.DealerContact();
            IQueryStrategy<Book1DealerContactRow> DealerContact = new __Book1_DealerContact();


            //var Dealer = new Book1.Dealer();
            IQueryStrategy<Book1DealerRow> Dealer = new __Book1_Dealer();

            IQueryStrategy<Book1DealerOtherRow> DealerOther = new __Book1_DealerOther();



            ////            #region from contact in DealerContact
            ////            DealerContact
            ////            #endregion

            ////            #region join dealer in Dealer on contact.DealerId equals dealer.ID
            ////.Join(
            ////                               Dealer,
            ////                               contact => contact.DealerId, dealer => dealer.ID,
            ////                    (contact, dealer) => new { contact, dealer }
            ////                        )
            ////            #endregion

            ////            #region select new Book1TheViewRow;

            ////                //Error	2	The type arguments for method 'TestSQLNestedJoin.X.Join<TOuter,TInner,TKey,TResult>(TestSQLNestedJoin.IQueryStrategy<TOuter>, TestSQLNestedJoin.IQueryStrategy<TInner>, System.Linq.Expressions.Expression<System.Func<TOuter,TKey>>, System.Linq.Expressions.Expression<System.Func<TInner,TKey>>, System.Linq.Expressions.Expression<System.Func<TOuter,TInner,TResult>>)' cannot be inferred from the usage. Try specifying the type arguments explicitly.	X:\jsc.svn\examples\javascript\Test\TestSQLNestedJoin\TestSQLNestedJoin\ApplicationWebService.cs	81	1	TestSQLNestedJoin


            ////.Join(
            ////                        DealerOther,
            ////                               x => x.contact.DealerId, (Book1DealerOtherRow other) => other.ID,

            ////                              (x, other) =>
            ////                                    new Book1TheViewRow
            ////                                   {
            ////                                       DealerContactText = x.contact.DealerContactText,
            ////                                       DealerText = x.dealer.DealerText,
            ////                                       DealerOtherText = other.DealerOtherText
            ////                                   }
            ////                        );
            ////            #endregion



            var z =
                from contact in DealerContact

                //Error	24	Cannot implicitly convert type 'TestSQLJoin.Data.Book1.TheView' to 
                // 'System.Collections.Generic.IEnumerable<TestSQLJoin.Data.Book1TheViewRow>'. An explicit conversion exists (are you missing a cast?)	
                //X:\jsc.svn\examples\javascript\forms\Test\TestSQLJoin\TestSQLJoin\ApplicationWebService.cs	88	17	TestSQLJoin


                //Error	24	Could not find an implementation of the query pattern for source type 'TestSQLJoin.__Book1_DealerContact'.  'Join' not found.	X:\jsc.svn\examples\javascript\forms\Test\TestSQLJoin\TestSQLJoin\ApplicationWebService.cs	123	33	TestSQLJoin
                join dealer in Dealer on contact.DealerId equals dealer.ID

                // Error	9	Cannot implicitly convert type 'AnonymousType#1' to 'TestSQLJoin.Data.Book1TheViewRow'	X:\jsc.svn\examples\javascript\forms\Test\TestSQLJoin\TestSQLJoin\ApplicationWebService.cs	138	17	TestSQLJoin
                // how are we supposed to add secondary join?

                // Error	25	The type of one of the expressions in the join clause is incorrect.  Type inference failed in the call to 'Join'.	X:\jsc.svn\examples\javascript\forms\Test\TestSQLJoin\TestSQLJoin\ApplicationWebService.cs	135	17	TestSQLJoin
                join other in DealerOther on contact.DealerId equals other.ID



                // Message = "Column 'DealerOtherText' does not belong to table ."

                // how would this look like in generated sql?
                select new Book1TheViewRow
                {
                    // Additional information: Column 'Timestamp' does not belong to table .
                    // we shuld make the op more tolerant on missing fields?
                    Timestamp = contact.Timestamp,

                    //Additional information: Column 'Tag' does not belong to table .   
                    Tag = "no tag",

                    //Timestamp = DateTime.Now,

                    //Additional information: Column 'DealerOther' does not belong to table .
                    DealerOther = 0,

                    // make row operator happy?
                    DealerOtherText = "hi",
                    //DealerOtherText = other.DealerOtherText


                    Dealer = dealer.Key,
                    DealerContact = contact.Key,

                    DealerContactText = contact.DealerContactText,
                    DealerText = dealer.DealerText,

                };


            //return z;
            return null;
        }

    }


    interface IQueryStrategy<TRow> : IQueryStrategy
    {

        //void Join(Book1.DealerOther DealerOther, object p1, Func<TInner, TKey> func, object p2);
    }

    // Error	1	The type of one of the expressions in the join clause is incorrect.  Type inference failed in the call to 'Join'.	X:\jsc.svn\examples\javascript\forms\Test\TestSQLJoin\TestSQLJoin\ApplicationWebService.cs	130	17	TestSQLJoin
    public class __Book1_Dealer : Book1.Dealer, IQueryStrategy<Book1DealerRow>
    {


    }


    public class __Book1_DealerContact : Book1.DealerContact, IQueryStrategy<Book1DealerContactRow>
    {


    }

    public class __Book1_DealerOther : Book1.DealerOther, IQueryStrategy<Book1DealerOtherRow>
    {


    }

    public class __Book1_TheView : Book1.TheView, IQueryStrategy<Book1TheViewRow>
    {

    }


    [Obsolete("what does it take to have nested join?")]
    static class X
    {
        // X:\jsc.svn\examples\javascript\forms\Test\TestSQLJoin\TestSQLJoin\ApplicationWebService.cs

        public static
            IQueryStrategy<TResult>

            Join<TOuter, TInner, TKey, TResult>(
                this IQueryStrategy<TOuter> xouter,

                IQueryStrategy<TInner> xinner,

                Expression<Func<TOuter, TKey>> outerKeySelector,
                Expression<Func<TInner, TKey>> innerKeySelector,

                Expression<Func<TOuter, TInner, TResult>> resultSelector
            )
        {
            return null;

        }
    }
}
