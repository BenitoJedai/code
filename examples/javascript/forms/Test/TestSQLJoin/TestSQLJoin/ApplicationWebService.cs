using ScriptCoreLib;
using ScriptCoreLib.Delegates;
using ScriptCoreLib.Extensions;
using ScriptCoreLib.Shared.Data.Diagnostics;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Linq.Expressions;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using TestSQLJoin.Data;

namespace TestSQLJoin
{
    /// <summary>
    /// Methods defined in this type can be used from JavaScript. The method calls will seamlessly be proxied to the server.
    /// </summary>
    public partial class ApplicationWebService : Component
    {
        // https://sites.google.com/a/jsc-solutions.net/backlog/knowledge-base/2014/201404/20140429
        // X:\jsc.svn\examples\javascript\test\TestLINQJoin\TestLINQJoin\Application.cs

        [Obsolete("future jsc shall find references by deep inspection.")]
        void References()
        {

        }





        // Error	3	Could not find an implementation of the query pattern for source type 'TestSQLJoin.Data.Book1.DealerContact'.  'Join' not found.	X:\jsc.svn\examples\javascript\forms\Test\TestSQLJoin\TestSQLJoin\ApplicationWebService.cs	34	33	TestSQLJoin
        // X:\jsc.svn\core\ScriptCoreLib.Extensions\ScriptCoreLib.Extensions\Shared\Data\Diagnostics\QueryStrategyExtensions.cs



        // http://stackoverflow.com/questions/485398/how-to-create-a-join-in-an-expression-tree-for-linq
        // http://msdn.microsoft.com/en-us/library/bb399415(v=vs.110).aspx

        // 4.5
        public async Task<IEnumerable<Book1TheViewRow>> GetTheViewData()
        {

            var DealerContact = new Book1.DealerContact();
            var Dealer = new Book1.Dealer();
            var DealerOther = new Book1.DealerOther();

            // public static Book1.DealerContact Where(this Book1.DealerContact value, Expression<Func<Book1DealerContactRow, bool>> value);
            //DealerContact.Where

            return
                from contact in DealerContact
                join dealer in Dealer on contact.DealerId equals dealer.ID
                //join other in DealerOther on contact.DealerId equals other.ID


                // how would this look like in generated sql?
                let v = new Book1TheViewRow
                {
                    DealerContactText = contact.DealerContactText,
                    DealerText = dealer.DealerText,
                    //DealerOtherText = other.DealerOtherText
                }

                select v;

        }

    }

    static class X
    {
        //public static IEnumerable<TResult> Join<TOuter, TInner, TKey, TResult>(
        //    this IEnumerable<TOuter> outer,
        //    IEnumerable<TInner> inner,
        //    Func<TOuter, TKey> outerKeySelector,
        //    Func<TInner, TKey> innerKeySelector,
        //    Func<TOuter, TInner, TResult> resultSelector)


        // public static Book1.DealerContact Where(this Book1.DealerContact value, Expression<Func<Book1DealerContactRow, bool>> value);
        // https://sites.google.com/a/jsc-solutions.net/backlog/knowledge-base/2014/201404/20140429

        // Error	4	Could not find an implementation of the query pattern for source type 'TestSQLJoin.Data.Book1.DealerContact'.  'Join' not found.	X:\jsc.svn\examples\javascript\forms\Test\TestSQLJoin\TestSQLJoin\ApplicationWebService.cs	51	33	TestSQLJoin


        // http://social.msdn.microsoft.com/Forums/en-US/bf98ec7a-cb80-4901-8eb2-3aa6636a4fde/linq-join-error-the-type-of-one-of-the-expressions-in-the-join-clause-is-incorrect-type-inference?forum=linqprojectgeneral

        [Obsolete("whats the correct signature?")]
        public static object Join<TOuter, TInner, TKey, TResult>(
            this Book1.DealerContact outer,
            Book1.Dealer inner,
            Expression<Func<TOuter, TKey>> outerKeySelector,
            Expression<Func<TInner, TKey>> innerKeySelector,
            Func<TOuter, TInner, TResult> resultSelector
            )
        {
            // Error	4	The type of one of the expressions in the join clause is incorrect.  Type inference failed in the call to 'Join'.	X:\jsc.svn\examples\javascript\forms\Test\TestSQLJoin\TestSQLJoin\ApplicationWebService.cs	58	17	TestSQLJoin


            //default(IQueryable<object>).Join()
            return null;
        }
    }
}
