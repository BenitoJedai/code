using ScriptCoreLib;
using ScriptCoreLib.Delegates;
using ScriptCoreLib.Extensions;
using ScriptCoreLib.Shared.Data.Diagnostics;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
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
        // under .net the IEnumerable is not buffered. the caller needs to enforce it ?
        public
            async
            Task<IEnumerable<Book1TheViewRow>> GetTheViewData()
        {

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

            //var DealerContact = new Book1.DealerContact();
            var DealerContact = new __Book1_DealerContact();


            //var Dealer = new Book1.Dealer();
            var Dealer = new __Book1_Dealer();

            var DealerOther = new Book1.DealerOther();

            var View = new Book1.TheView();

            //DealerOther.Where
            // public static Book1.DealerContact Where(this Book1.DealerContact value, Expression<Func<Book1DealerContactRow, bool>> value);
            //DealerContact.Where

            // rewrite into non LINQ keyowrd?

            //            return


            //            #region from contact in DealerContact
            // DealerContact
            //            #endregion

            //            #region join dealer in Dealer on contact.DealerId equals dealer.ID
            //.Join(
            //                    Dealer,
            //                    contact => contact.DealerId, dealer => dealer.ID,

            //            #endregion

            //            #region select new Book1TheViewRow;
            // (contact, dealer) =>
            //                        new Book1TheViewRow
            //                        {
            //                            DealerContactText = contact.DealerContactText,
            //                            DealerText = dealer.DealerText,
            //                            //DealerOtherText = other.DealerOtherText
            //                        }
            //                );
            //            #endregion

            var z =
                from contact in DealerContact

                //Error	24	Cannot implicitly convert type 'TestSQLJoin.Data.Book1.TheView' to 
                // 'System.Collections.Generic.IEnumerable<TestSQLJoin.Data.Book1TheViewRow>'. An explicit conversion exists (are you missing a cast?)	
                //X:\jsc.svn\examples\javascript\forms\Test\TestSQLJoin\TestSQLJoin\ApplicationWebService.cs	88	17	TestSQLJoin


                join dealer in Dealer on contact.DealerId equals dealer.ID
                //join other in DealerOther on contact.DealerId equals other.ID


                // how would this look like in generated sql?
                select new Book1TheViewRow
                {
                    DealerContactText = contact.DealerContactText,
                    DealerText = dealer.DealerText,
                    //DealerOtherText = other.DealerOtherText
                };


            //DataTable zz = z;

            // public static DataTable AsDataTable(this Book1.TheView value);

            //Error	31	The call is ambiguous between the following methods or properties: 
            // 'TestSQLJoin.Data.Book1Extensions.AsDataTable(TestSQLJoin.Data.Book1.TheView)' and
            // 'TestSQLJoin.Data.Book1Extensions.AsDataTable(System.Collections.Generic.IEnumerable<TestSQLJoin.Data.Book1TheViewRow>)'	
            // X:\jsc.svn\examples\javascript\forms\Test\TestSQLJoin\TestSQLJoin\ApplicationWebService.cs	146	28	TestSQLJoin


            // Error	2	The call is ambiguous between the following methods or properties: 'TestSQLJoin.Data.Book1Extensions.AsEnumerable(TestSQLJoin.Data.Book1.TheView)' and 'System.Linq.Enumerable.AsEnumerable<TestSQLJoin.Data.Book1TheViewRow>(System.Collections.Generic.IEnumerable<TestSQLJoin.Data.Book1TheViewRow>)'	X:\jsc.svn\examples\javascript\forms\Test\TestSQLJoin\TestSQLJoin\ApplicationWebService.cs	153	22	TestSQLJoin


            var zz = (Book1.TheView)z;
            DataTable zzz = zz.AsDataTable();

            //var zz = z.AsEnumerable();


            return z;
        }

    }


    public class __Book1_Dealer : Book1.Dealer, IQueryable<TestSQLJoin.Data.Book1DealerRow>
    {
        #region IEnumerable
        public IEnumerator<Book1DealerRow> GetEnumerator()
        {
            return Book1Extensions.AsEnumerable(this).GetEnumerator();
            //return this.AsEnumerable().GetEnumerator();
        }

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }
        #endregion

        #region IQueryable for LINQ join
        public Type ElementType
        {
            get { throw new NotImplementedException(); }
        }

        public Expression Expression
        {
            get { throw new NotImplementedException(); }
        }

        public IQueryProvider Provider
        {
            get { throw new NotImplementedException(); }
        }
        #endregion

    }


    public class __Book1_DealerContact : Book1.DealerContact, IQueryable<TestSQLJoin.Data.Book1DealerContactRow>
    {
        #region IEnumerable
        public IEnumerator<Book1DealerContactRow> GetEnumerator()
        {
            return Book1Extensions.AsEnumerable(this).GetEnumerator();
            //return this.AsEnumerable().GetEnumerator();
        }

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }
        #endregion


        #region IQueryable for LINQ join
        public Type ElementType
        {
            get { throw new NotImplementedException(); }
        }

        public Expression Expression
        {
            get { throw new NotImplementedException(); }
        }

        public IQueryProvider Provider
        {
            get { throw new NotImplementedException(); }
        }
        #endregion

    }

    public class __Book1_TheView : Book1.TheView, IQueryable<TestSQLJoin.Data.Book1TheViewRow>
    {
        //Error	1	Cannot implicitly convert type 'TestSQLJoin.__Book1_TheView' to 'System.Threading.Tasks.Task<System.Collections.Generic.IEnumerable<TestSQLJoin.Data.Book1TheViewRow>>'	X:\jsc.svn\examples\javascript\forms\Test\TestSQLJoin\TestSQLJoin\ApplicationWebService.cs	107	17	TestSQLJoin
        //             public static implicit operator Task<Book1TheViewRow>(Book1.TheView value);

        #region IEnumerable

        public IEnumerator<Book1TheViewRow> GetEnumerator()
        {
            return Book1Extensions.AsEnumerable(this).GetEnumerator();
            //return this.AsEnumerable().GetEnumerator();
        }

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }
        #endregion


        #region IQueryable for LINQ join
        public Type ElementType
        {
            get { throw new NotImplementedException(); }
        }

        public Expression Expression
        {
            get { throw new NotImplementedException(); }
        }

        public IQueryProvider Provider
        {
            get { throw new NotImplementedException(); }
        }
        #endregion
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


        public static __Book1_TheView Join<TOuter, TInner, TKey>(
            this IQueryable<TOuter> outer,
            IQueryable<TInner> inner,
            Expression<Func<TOuter, TKey>> outerKeySelector,
            Expression<Func<TInner, TKey>> innerKeySelector,
            Expression<Func<TOuter, TInner, Book1TheViewRow>> resultSelector
            )
        {

            // how do we get this barely functional?

            // can we manually convince this thing to include the join clause?
            var that = new __Book1_TheView();

            // this seems to be what we may want to use to do that
            //var x = n.GetDescriptor();

            //  X:\jsc.svn\core\ScriptCoreLib.Extensions\ScriptCoreLib.Extensions\Shared\Data\Diagnostics\QueryStrategyExtensions.cs
            //x.





            // or by looking at where implementation



            // will this help us?
            var xouter = outer as IQueryStrategy;
            var xinner = inner as IQueryStrategy;



            that.GetCommandBuilder().Add(
                state =>
                {
                    // xouter_SelectAll = "select `Key`, `DealerId`, `DealerContactText`, `Tag`, `Timestamp`"

                    // http://stackoverflow.com/questions/5090513/how-do-you-avoid-column-name-conflicts


                    var xouter_SelectAll = QueryStrategyExtensions.AsCommandBuilder(xouter);
                    //var xouter_SelectAll = xouter.GetDescriptor().GetSelectAllColumnsText();
                    var xinner_SelectAll = QueryStrategyExtensions.AsCommandBuilder(xinner);
                    //var xinner_SelectAll = xinner.GetDescriptor().GetSelectAllColumnsText();





                    //ex = {"A column named 'Key' already belongs to this DataTable."}
                    // !!! do we need to generate prefix for key fields to ease joins?
                    // !!! how much code will it break ???

                    state.SelectCommand =
                        // not sure what fields are we reading
                        "select DealerContactText, DealerText ";



                    // ex = {"no such column: Book1.DealerContact.ID"}
                    //ex = {"no such column: Book1.DealerContact.DealerId"}
                    // ex = {"no such column: Book1.DealerContact.DealerId"}
                    // wtf?

                    state.FromCommand =
                        " from ("
                            + xouter_SelectAll.ToString()
                            + ") as x inner join ("
                            + xinner_SelectAll.ToString()
                            + ") as y on "
                        //+ "`" + xouter_SelectAll.Strategy.GetDescriptor().GetQualifiedTableName() + "`.`DealerId`"
                            + "x.`DealerId`"
                            + " = "
                            + "y.`ID`";


                    //text = "ScriptCoreLib.Extensions::ScriptCoreLib.Shared.Data.Diagnostics.WithConnectionLambda.WithConnection\n\n error: { Message = no such column: Book1.DealerContact.DealerId, ex = System.Data.SQLite.SQLiteSyntaxException (0x80004005): no such column: Book1.Deale...


                    // http://www.tutorialspoint.com/sqlite/sqlite_using_joins.htm
                    // SELECT EMP_ID, NAME, DEPT FROM COMPANY INNER JOIN DEPARTMENT
                    //ON COMPANY.ID = DEPARTMENT.EMP_ID;

                    //-		state	{select `Key`, `DealerContactText`, `DealerText`, `DealerOtherText`, `Tag`, `Timestamp`
                    //from `Book1.TheView`



                    //}	ScriptCoreLib.Shared.Data.Diagnostics.QueryStrategyExtensions.CommandBuilderState
                    //+		ApplyParameter	Count = 0	System.Collections.Generic.List<System.Action<System.Data.IDbCommand>>
                    //        FromCommand	"from `Book1.TheView`"	string
                    //        LimitCommand	null	string
                    //        OrderByCommand	null	string
                    //        SelectCommand	"select `Key`, `DealerContactText`, `DealerText`, `DealerOtherText`, `Tag`, `Timestamp`"	string
                    //+		Strategy	{TestSQLJoin.__Book1_TheView}	ScriptCoreLib.Shared.Data.Diagnostics.IQueryStrategy {TestSQLJoin.__Book1_TheView}
                    //        WhereCommand	null	string


                    //MutableWhere { Method = Boolean op_Equality(System.String, System.String), Left = Goo, Right = Goo0 }


                    Debugger.Break();

                    //state.FromCommand
                    //state.OrderByCommand = "order by `" + ColumnName + "`";
                }
               );

            return that;
        }


        public static __Book1_TheView XJoin<TKey>(
            this Book1.DealerContact outer,
            Book1.Dealer inner,
            Expression<Func<Book1DealerContactRow, TKey>> outerKeySelector,
            Expression<Func<Book1DealerRow, TKey>> innerKeySelector,

            //Func<Book1DealerContactRow, Book1DealerRow, Book1TheViewRow> resultSelector


            Expression<Func<Book1DealerContactRow, Book1DealerRow, Book1TheViewRow>> resultSelector
            )
        {
            // Error	4	The type of one of the expressions in the join clause is incorrect.  
            // Type inference failed in the call to 'Join'.	X:\jsc.svn\examples\javascript\forms\Test\TestSQLJoin\TestSQLJoin\ApplicationWebService.cs	58	17	TestSQLJoin

            //Error	18	Cannot implicitly convert type 'object' to 'System.Collections.Generic.IEnumerable<TestSQLJoin.Data.Book1TheViewRow>'. An explicit conversion exists (are you missing a cast?)	X:\jsc.svn\examples\javascript\forms\Test\TestSQLJoin\TestSQLJoin\ApplicationWebService.cs	66	1	TestSQLJoin

            //default(IQueryable<object>).Join()
            return null;
        }
    }
}
