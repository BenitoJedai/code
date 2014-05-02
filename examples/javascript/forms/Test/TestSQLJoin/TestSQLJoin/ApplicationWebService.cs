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
            // {"Could not load file or assembly 'System.Data.XSQLite, Version=3.7.7.1, Culture=neutral, PublicKeyToken=null' or one of its dependencies. The system cannot find the file specified.":"System.Data.XSQLite, Version=3.7.7.1, Culture=neutral, PublicKeyToken=null"}
            { var ref0 = typeof(IQueryStrategy); }
            { var ref0 = typeof(System.Data.SQLite.SQLiteConnection); }
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
            // http://stackoverflow.com/questions/38549/difference-between-inner-and-outer-join
            // X:\jsc.svn\examples\javascript\Test\TestSQLNestedJoin\TestSQLNestedJoin\ApplicationWebService.cs

            #region InsertDemoData
            Action InsertDemoData = delegate
            {
                new[] { 
                    new Book1DealerContactRow { DealerId = 2, DealerContactText = ""},
                    new Book1DealerContactRow { DealerId = 3, DealerContactText = "hello "},
                    new Book1DealerContactRow { DealerId = 4, DealerContactText = ""}
                }.Select(new Book1.DealerContact().Insert).ToArray();



                new[] { 
                    new Book1DealerRow { ID = 1, DealerText = ""},
                    new Book1DealerRow { ID = 3, DealerText = "world"},
                    new Book1DealerRow { ID = 5, DealerText = ""}
                }.Select(new Book1.Dealer().Insert).ToArray();


                new[] { 
                    new Book1DealerOtherRow { ID = 0, DealerOtherText = ""},
                    new Book1DealerOtherRow { ID = 3, DealerOtherText = "!!"},
                    new Book1DealerOtherRow { ID = 20, DealerOtherText = ""}
                }.Select(new Book1.DealerOther().Insert).ToArray();
            };


            InsertDemoData();
            #endregion


            var DealerContact = new Book1.DealerContact();
            var Dealer = new Book1.Dealer();
            var DealerOther = new Book1.DealerOther();
            var View = new Book1.TheView();



            var z =
                from contact in DealerContact
                join dealer in Dealer on contact.DealerId equals dealer.ID
                join other in DealerOther on contact.DealerId equals other.ID
                select new Book1TheViewRow
                {
                    Timestamp = contact.Timestamp,
                    Tag = "no tag",
                    DealerOther = 0,
                    DealerOtherText = other.DealerOtherText,
                    Dealer = dealer.Key,
                    DealerContact = contact.Key,
                    DealerContactText = contact.DealerContactText,
                    DealerText = dealer.DealerText,
                };


            //DataTable zz = z;

            // public static DataTable AsDataTable(this Book1.TheView value);

            //Error	31	The call is ambiguous between the following methods or properties: 
            // 'TestSQLJoin.Data.Book1Extensions.AsDataTable(TestSQLJoin.Data.Book1.TheView)' and
            // 'TestSQLJoin.Data.Book1Extensions.AsDataTable(System.Collections.Generic.IEnumerable<TestSQLJoin.Data.Book1TheViewRow>)'	
            // X:\jsc.svn\examples\javascript\forms\Test\TestSQLJoin\TestSQLJoin\ApplicationWebService.cs	146	28	TestSQLJoin


            // Error	2	The call is ambiguous between the following methods or properties: 'TestSQLJoin.Data.Book1Extensions.AsEnumerable(TestSQLJoin.Data.Book1.TheView)' and 'System.Linq.Enumerable.AsEnumerable<TestSQLJoin.Data.Book1TheViewRow>(System.Collections.Generic.IEnumerable<TestSQLJoin.Data.Book1TheViewRow>)'	X:\jsc.svn\examples\javascript\forms\Test\TestSQLJoin\TestSQLJoin\ApplicationWebService.cs	153	22	TestSQLJoin


            //var zz = (Book1.TheView)z;
            //DataTable zzz = zz.AsDataTable();

            //var zz = z.AsEnumerable();

            // Error	26	Instance argument: cannot convert from 'System.Linq.ParallelQuery<TResult>' to 'TestSQLJoin.Data.Book1.DealerContact'	X:\jsc.svn\examples\javascript\forms\Test\TestSQLJoin\TestSQLJoin\ApplicationWebService.cs	189	22	TestSQLJoin
            //var zz = z.AsEnumerable();


            //var zz0 = zz.First();

            //            at System.Data.DataRow.GetDataColumn(String columnName)
            //at System.Data.DataRow.get_Item(String columnName)
            //at TestSQLJoin.Data.Book1TheViewRow.op_Implicit(DataRow )




            //var e0 = z.AsEnumerable();
            //var count0 = z.Count();
            //var data0 = z.AsDataTable();

            //var data = QueryStrategyExtensions.AsDataTable(z);

            //     public static IEnumerable<Book1TheViewRow> AsEnumerable(this Book1.TheView value);
            //new Book1.TheView().AsEnumerable();

            //var zz = data.Rows.AsEnumerable().Select(x => (Book1TheViewRow)x).ToArray();


            //Book1Extensions.





            // client cannot handle too much data! yet.
            return z.Take(11).AsEnumerable();
        }

    }


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

        //[Obsolete("can we get rid of the return type too? how would that look like?")]
        public static

            //__Book1_TheView 
            IQueryStrategy<TResult>

            Join<TOuter, TInner, TKey, TResult>(
            this IQueryStrategy<TOuter> xouter,
            IQueryStrategy<TInner> xinner,

            // outerKeySelector = {<>h__TransparentIdentifier0 => <>h__TransparentIdentifier0.contact.DealerId}
            Expression<Func<TOuter, TKey>> outerKeySelector,
            Expression<Func<TInner, TKey>> innerKeySelector,

            // resultSelector = {(contact, dealer) => new <>f__AnonymousType0`2(contact = contact, dealer = dealer)}
            Expression<Func<TOuter, TInner, TResult>> resultSelector
            )
        {
            // X:\jsc.svn\examples\javascript\forms\Test\TestSQLJoin\TestSQLJoin\ApplicationWebService.cs

            // how do we get this barely functional?

            // can we manually convince this thing to include the join clause?
            //var that = new __Book1_TheView();
            var that = new XQueryStrategy<TResult>
            {


                InternalGetDescriptor =
                    () =>
                    {
                        // inherit the connection/context from above
                        var StrategyDescriptor = xouter.GetDescriptor();

                        return StrategyDescriptor;
                    }
            };


            //Additional information: Unable to cast object of type 'TestSQLJoin.__Book1_TheView' to type 'TestSQLJoin.IQueryStrategy`1[<>f__AnonymousType0`2[TestSQLJoin.Data.Book1DealerContactRow,TestSQLJoin.Data.Book1DealerRow]]'.

            // this seems to be what we may want to use to do that
            //var x = n.GetDescriptor();

            //  X:\jsc.svn\core\ScriptCoreLib.Extensions\ScriptCoreLib.Extensions\Shared\Data\Diagnostics\QueryStrategyExtensions.cs
            //x.





            // or by looking at where implementation


            // is this a new expression?
            // what else could it be?


            //-		resultSelector	{(contact, dealer) => new Book1TheViewRow() {Dealer = dealer.Key, DealerContact = contact.Key, DealerContactText = contact.DealerContactText, DealerText = dealer.DealerText}}	System.Linq.Expressions.Expression<System.Func<TestSQLJoin.Data.Book1DealerContactRow,TestSQLJoin.Data.Book1DealerRow,TestSQLJoin.Data.Book1TheViewRow>>
            //+		Body	{new Book1TheViewRow() {Dealer = dealer.Key, DealerContact = contact.Key, DealerContactText = contact.DealerContactText, DealerText = dealer.DealerText}}	System.Linq.Expressions.Expression {System.Linq.Expressions.MemberInitExpression}
            //        CanReduce	false	bool
            //        DebugView	".Lambda #Lambda1<System.Func`3[TestSQLJoin.Data.Book1DealerContactRow,TestSQLJoin.Data.Book1DealerRow,TestSQLJoin.Data.Book1TheViewRow]>(\r\n    TestSQLJoin.Data.Book1DealerContactRow $contact,\r\n    TestSQLJoin.Data.Book1DealerRow $dealer) {\r\n    .New TestSQLJoin.Data.Book1TheViewRow(){\r\n        Dealer = $dealer.Key,\r\n        DealerContact = $contact.Key,\r\n        DealerContactText = $contact.DealerContactText,\r\n        DealerText = $dealer.DealerText\r\n    }\r\n}"	string
            //        Name	null	string
            //        NodeType	Lambda	System.Linq.Expressions.ExpressionType
            //+		Parameters	Count = 2	System.Collections.ObjectModel.ReadOnlyCollection<System.Linq.Expressions.ParameterExpression> {System.Runtime.CompilerServices.TrueReadOnlyCollection<System.Linq.Expressions.ParameterExpression>}
            //+		ReturnType	{Name = "Book1TheViewRow" FullName = "TestSQLJoin.Data.Book1TheViewRow"}	System.Type {System.RuntimeType}
            //        TailCall	false	bool
            //+		Type	{Name = "Func`3" FullName = "System.Func`3[[TestSQLJoin.Data.Book1DealerContactRow, TestSQLJoin.AssetsLibrary, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null],[TestSQLJoin.Data.Book1DealerRow, TestSQLJoin.AssetsLibrary, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null],[TestSQLJoin.Data.Book1TheViewRow, TestSQLJoin.AssetsLibrary, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null]]"}	System.Type {System.RuntimeType}
            //-		Raw View		
            //-		base	{(contact, dealer) => new Book1TheViewRow() {Dealer = dealer.Key, DealerContact = contact.Key, DealerContactText = contact.DealerContactText, DealerText = dealer.DealerText}}	System.Linq.Expressions.LambdaExpression {System.Linq.Expressions.Expression<System.Func<TestSQLJoin.Data.Book1DealerContactRow,TestSQLJoin.Data.Book1DealerRow,TestSQLJoin.Data.Book1TheViewRow>>}


            //var asNewExpression = resultSelector as NewExpression;
            var asLambdaExpression = resultSelector as LambdaExpression;

            // can we assume 
            //+		(new System.Linq.Expressions.Expression.ParameterExpressionProxy(xouter_Paramerer as System.Linq.Expressions.TypedParameterExpression)).Type	{Name = "Book1DealerContactRow" FullName = "TestSQLJoin.Data.Book1DealerContactRow"}	System.Type {System.RuntimeType}
            //+		xouter	{TestSQLJoin.__Book1_DealerContact}	TestSQLJoin.IQueryStrategy<TestSQLJoin.Data.Book1DealerContactRow> {TestSQLJoin.__Book1_DealerContact}
            // yes we can 0 is outer 1 is inner?

            var xouter_Paramerer = asLambdaExpression.Parameters[0];
            var xinner_Paramerer = asLambdaExpression.Parameters[1];


            //-		asLambdaExpression	{(contact, dealer) => new Book1TheViewRow() {Dealer = dealer.Key, DealerContact = contact.Key, DealerContactText = contact.DealerContactText, DealerText = dealer.DealerText}}	System.Linq.Expressions.LambdaExpression {System.Linq.Expressions.Expression<System.Func<TestSQLJoin.Data.Book1DealerContactRow,TestSQLJoin.Data.Book1DealerRow,TestSQLJoin.Data.Book1TheViewRow>>}
            //+		Body	{new Book1TheViewRow() {Dealer = dealer.Key, DealerContact = contact.Key, DealerContactText = contact.DealerContactText, DealerText = dealer.DealerText}}	System.Linq.Expressions.Expression {System.Linq.Expressions.MemberInitExpression}
            //        CanReduce	false	bool
            //        DebugView	".Lambda #Lambda1<System.Func`3[TestSQLJoin.Data.Book1DealerContactRow,TestSQLJoin.Data.Book1DealerRow,TestSQLJoin.Data.Book1TheViewRow]>(\r\n    TestSQLJoin.Data.Book1DealerContactRow $contact,\r\n    TestSQLJoin.Data.Book1DealerRow $dealer) {\r\n    .New TestSQLJoin.Data.Book1TheViewRow(){\r\n        Dealer = $dealer.Key,\r\n        DealerContact = $contact.Key,\r\n        DealerContactText = $contact.DealerContactText,\r\n        DealerText = $dealer.DealerText\r\n    }\r\n}"	string
            //        Name	null	string
            //        NodeType	Lambda	System.Linq.Expressions.ExpressionType
            //-		Parameters	Count = 2	System.Collections.ObjectModel.ReadOnlyCollection<System.Linq.Expressions.ParameterExpression> {System.Runtime.CompilerServices.TrueReadOnlyCollection<System.Linq.Expressions.ParameterExpression>}
            //+		[0]	{contact}	System.Linq.Expressions.ParameterExpression {System.Linq.Expressions.TypedParameterExpression}
            //+		[1]	{dealer}	System.Linq.Expressions.ParameterExpression {System.Linq.Expressions.TypedParameterExpression}



            // will this help us?
            //var xouter = outer as IQueryStrategy;
            //var xinner = inner as IQueryStrategy;


            //-		asLambdaExpression.Body	{new Book1TheViewRow() {Dealer = dealer.Key, DealerContact = contact.Key, DealerContactText = contact.DealerContactText, DealerText = dealer.DealerText}}	System.Linq.Expressions.Expression {System.Linq.Expressions.MemberInitExpression}
            //-		Bindings	Count = 4	System.Collections.ObjectModel.ReadOnlyCollection<System.Linq.Expressions.MemberBinding> {System.Runtime.CompilerServices.TrueReadOnlyCollection<System.Linq.Expressions.MemberBinding>}
            //+		[0]	{Dealer = dealer.Key}	System.Linq.Expressions.MemberBinding {System.Linq.Expressions.MemberAssignment}
            //+		[1]	{DealerContact = contact.Key}	System.Linq.Expressions.MemberBinding {System.Linq.Expressions.MemberAssignment}
            //+		[2]	{DealerContactText = contact.DealerContactText}	System.Linq.Expressions.MemberBinding {System.Linq.Expressions.MemberAssignment}
            //+		[3]	{DealerText = dealer.DealerText}	System.Linq.Expressions.MemberBinding {System.Linq.Expressions.MemberAssignment}



            // ex = {"near \"<>\": syntax error"}

            //            ((new System.Linq.Expressions.Expression.MemberExpressionProxy((new System.Linq.Expressions.Expression.LambdaExpressionProxy(outerKeySelector as System.Linq.Expressions.Expression<System.Func<TestSQLJoin.Data.Book1DealerContactRow,long>>)).Body as System.Linq.Expressions.FieldExpression)).Member
            // ).Name	"DealerId"	string
            //(new System.Linq.Expressions.Expression.ParameterExpressionProxy((new System.Collections.Generic.Mscorlib_CollectionDebugView<System.Linq.Expressions.ParameterExpression>((
            // new System.Linq.Expressions.Expression.LambdaExpressionProxy(outerKeySelector as System.Linq.Expressions.Expression<System.Func<TestSQLJoin.Data.Book1DealerContactRow,long>>))
            // .Parameters as System.Runtime.CompilerServices.TrueReadOnlyCollection<System.Linq.Expressions.ParameterExpression>)).Items[0] as System.Linq.Expressions.TypedParameterExpression))
            // .Name	"contact"	string


            //xouter = {TestSQLJoin.XQueryStrategy<<>f__AnonymousType0<TestSQLJoin.Data.Book1DealerContactRow,TestSQLJoin.Data.Book1DealerRow>>}
            #region FromCommand
            var xouter_SelectAll = QueryStrategyExtensions.AsCommandBuilder(xouter);
            var xinner_SelectAll = QueryStrategyExtensions.AsCommandBuilder(xinner);


            // outerKeySelector = {<>h__TransparentIdentifier0 => <>h__TransparentIdentifier0.contact.DealerId}
            var xouter_asMemberExpression = outerKeySelector.Body as MemberExpression;
            var xinner_asMemberExpression = innerKeySelector.Body as MemberExpression;


            var xouter_Paramerer_Name = xouter_Paramerer.Name;

            //if (xouter_Paramerer)

            var u = xouter_asMemberExpression.Expression as MemberExpression;
            if (u != null)
            {
                // nested join?

                // https://sites.google.com/a/jsc-solutions.net/backlog/knowledge-base/2014/201405/20140501

                //xouter_Paramerer_Name = u.Member.Name;
            }

            var FromCommand =
                "from (\n\t"
                    + xouter_SelectAll.ToString().Replace("\n", "\n\t")
                    + ") as " + xouter_Paramerer_Name.Replace("<>", "__") + " "

                    + "\ninner join (\n\t"
                    + xinner_SelectAll.ToString().Replace("\n", "\n\t")
                    + ") as " + xinner_Paramerer.Name.Replace("<>", "__");
            #endregion




            //-		xouter_asMemberExpression.Expression as MemberExpression	{<>h__TransparentIdentifier0.contact}	System.Linq.Expressions.MemberExpression {System.Linq.Expressions.PropertyExpression}

            var xouter_asMemberExpression_Expression_asMemberExpression = xouter_asMemberExpression.Expression as MemberExpression;
            if (xouter_asMemberExpression_Expression_asMemberExpression != null)
            {

                FromCommand += " \non "
                     + xouter_Paramerer_Name.Replace("<>", "__") + "."

                            //xouter_asMemberExpression_Expression_asMemberExpression.Member.Name	"contact"	string
                            + "`" + xouter_asMemberExpression_Expression_asMemberExpression.Member.Name + "_"

                     + "" + xouter_asMemberExpression.Member.Name + "`"
                     + " = "
                     + xinner_Paramerer.Name + ".`" + xinner_asMemberExpression.Member.Name + "`";


            }
            else
            {

                FromCommand += " \non "
                        + xouter_Paramerer_Name.Replace("<>", "__") + ".`" + xouter_asMemberExpression.Member.Name + "`"
                        + " = "
                        + xinner_Paramerer.Name + ".`" + xinner_asMemberExpression.Member.Name + "`";


            }



            that.GetCommandBuilder().Add(
                state =>
                {
                    // xouter_SelectAll = "select `Key`, `DealerId`, `DealerContactText`, `Tag`, `Timestamp`"

                    // http://stackoverflow.com/questions/5090513/how-do-you-avoid-column-name-conflicts



                    #region SelectCommand
                    var SelectCommand = default(string);

                    var asNewExpression = asLambdaExpression.Body as MemberInitExpression;

                    if (asNewExpression == null)
                    {
                        // ex = {"near \"?\": syntax error"}

                        // should we select every column available for us?
                        // or should we only select the ones being selected down the road?

                        // asLambdaExpression = {(contact, dealer) => new <>f__AnonymousType0`2(contact = contact, dealer = dealer)}
                        //<>h__TransparentIdentifier0.contact_DealerContactText as DealerContactText,
                        // <>h__TransparentIdentifier0.dealer_DealerText as DealerText


                        // xouter_c "select `Key`, `DealerId`, `DealerContactText`, `Tag`, `Timestamp`"
                        // xinner_c "select `Key`, `ID`, `DealerText`, `Tag`, `Timestamp`"
                        var xouter_c = xouter.GetDescriptor().GetSelectAllColumnsText();
                        var xinner_c = xinner.GetDescriptor().GetSelectAllColumnsText();

                        //xouter_SelectAll.Strategy.GetDescriptor().GetSelectAllColumnsText();

                        var f0 = xouter_c.SkipUntilOrEmpty("select").Split(
                            new[] { " `" }, StringSplitOptions.RemoveEmptyEntries
                        ).Select(x => xouter_Paramerer_Name + ".`" + x.TakeUntilIfAny("`") + "` as  `" + xouter_Paramerer_Name + "_" + x.TakeUntilIfAny("`") + "`").ToArray();

                        var f1 = xinner_c.SkipUntilOrEmpty("select").Split(
                              new[] { " `" }, StringSplitOptions.RemoveEmptyEntries
                          ).Select(x => xinner_Paramerer.Name + ".`" + x.TakeUntilOrEmpty("`") + "` as  `" + xinner_Paramerer.Name + "_" + x.TakeUntilIfAny("`") + "`").ToArray();

                        var ff = f0.Concat(f1).ToArray();

                        // hack it, since we cannot ask for column data yet
                        SelectCommand =
                            "select " +
                            string.Join(", \n\t", ff);





                        //select `contact_Key`, `contact_DealerId`, `contact_DealerContactText`, `contact_Tag`, `contact_Timestamp` `dealer_Key`, `dealer_ID`, `dealer_DealerText`, `dealer_Tag`, `dealer_Timestamp`

                    }
                    else
                    {
                        #region asNewExpression.Bindings
                        asNewExpression.Bindings.WithEach(
                            m =>
                            {

                                if (SelectCommand == null)
                                    SelectCommand = "select ";
                                else
                                    SelectCommand += ",\n\t ";




                                // the name we want it to appear at later
                                var TargetMemberName = m.Member.Name;




                                // this looks like CustomAttributeBuilder thing.


                                var asMemberAssignment = m as MemberAssignment;


                                var asConstantExpression = asMemberAssignment.Expression as ConstantExpression;
                                if (asConstantExpression != null)
                                {
                                    if (asConstantExpression.Type == typeof(string))
                                    {
                                        SelectCommand += "'" + asConstantExpression.Value + "' as " + TargetMemberName;
                                    }
                                    else
                                    {
                                        // long?
                                        SelectCommand += "" + asConstantExpression.Value + " as " + TargetMemberName;
                                    }

                                    return;
                                }

                                // asFieldExpression = {<>h__TransparentIdentifier0.contact.Timestamp}
                                var asFieldExpression = asMemberAssignment.Expression as MemberExpression;

                                var asFieldExpression_Expression_asFieldExpression = asFieldExpression.Expression as MemberExpression;

                                if (asFieldExpression_Expression_asFieldExpression != null)
                                {
                                    // reduce? flatten?  nested join?

                                    //asFieldExpression = asFieldExpression_Expression_asFieldExpression;

                                    var __projection = asFieldExpression_Expression_asFieldExpression.Expression as ParameterExpression;

                                    SelectCommand +=

                                        __projection.Name.Replace("<>", "__") + "." +

                                        asFieldExpression_Expression_asFieldExpression.Member.Name
                                        + "_"
                                        + asFieldExpression.Member.Name + " as " + TargetMemberName;
                                    return;
                                }

                                // http://dotnetinside.com/cn/type/System.Core/TypedParameterExpression/4.0.0.0
                                //var asTypedParameterExpression = asFieldExpression.Expression as TypedParameterExpression
                                var asTypedParameterExpression = asFieldExpression.Expression as ParameterExpression;


                                var SourceContextName = asTypedParameterExpression.Name;
                                var SourceMemberName = asFieldExpression.Member.Name;




                                // magic happens here!
                                SelectCommand += SourceContextName + "." + SourceMemberName + " as " + TargetMemberName;

                            }

                        );
                        #endregion

                    }
                    #endregion



                    //var xinner_SelectAll = xinner.GetDescriptor().GetSelectAllColumnsText();





                    //ex = {"A column named 'Key' already belongs to this DataTable."}
                    // !!! do we need to generate prefix for key fields to ease joins?
                    // !!! how much code will it break ???

                    // viewColumns = "select `Key`, `DealerContactText`, `DealerText`, `DealerOtherText`, `Tag`, `Timestamp`"
                    // GetDescriptor().GetSelectAllColumnsText needs to return the fields.

                    //var viewColumns = that.GetDescriptor().GetSelectAllColumnsText();

                    //Dealer = dealer.Key,
                    //DealerContact = contact.Key,

                    //ex = {"no such column: Dealer"}

                    // ex = {"no such column: Dealer"}


                    // SelectCommand = "select dealer.Key as Dealer, contact.Key as DealerContact, contact.DealerContactText as DealerContactText, dealer.DealerText as DealerText"
                    state.SelectCommand = SelectCommand;
                    state.FromCommand = FromCommand;
                    // not sure what fields are we reading
                    //"select DealerContactText, DealerText, x.Key as Dealer, y.Key as DealerContact";



                    // ex = {"no such column: Book1.DealerContact.ID"}
                    //ex = {"no such column: Book1.DealerContact.DealerId"}
                    // ex = {"no such column: Book1.DealerContact.DealerId"}
                    // wtf?



                    // from (select `Key`, `DealerId`, `DealerContactText`, `Tag`, `Timestamp`
                    //from `Book1.DealerContact`



                    //) as contact inner join (select `Key`, `ID`, `DealerText`, `Tag`, `Timestamp`
                    //from `Book1.Dealer`



                    //) as dealer on contact.`DealerId` = dealer.`ID`



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


                    //Debugger.Break();

                    //state.FromCommand
                    //state.OrderByCommand = "order by `" + ColumnName + "`";
                }
               );

            //return (IQueryStrategy<TResult>)that;
            return that;
        }




    }
}
