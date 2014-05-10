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
    [Obsolete("the first generic extension method for all jsc data layer rows")]
    public static class QueryStrategyOfTRowExtensions
    {
        // X:\jsc.svn\examples\javascript\forms\Test\TestSQLJoin\TestSQLJoin\ApplicationWebService.cs

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


                        // where is SkipUntilOrEmpty
                        // Error	22	The type 'System.Windows.UIElement' is defined in an assembly that is not referenced. You must add a reference to assembly 'PresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35'.	X:\jsc.svn\core\ScriptCoreLib.Extensions\ScriptCoreLib.Extensions\Shared\Data\Diagnostics\QueryStrategyExtensions.cs	307	25	ScriptCoreLib.Extensions
                        // Error	22	The type 'System.Windows.DependencyObject' is defined in an assembly that is not referenced. You must add a reference to assembly 'WindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35'.	X:\jsc.svn\core\ScriptCoreLib.Extensions\ScriptCoreLib.Extensions\Shared\Data\Diagnostics\QueryStrategyExtensions.cs	309	25	ScriptCoreLib.Extensions


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


// change namespace?
namespace ScriptCoreLib.Shared.Data.Diagnostics
{


    public interface IQueryDescriptor
    {
        // this type has the reset state and how to make a connection

        string GetSelectAllColumnsText();

        string GetQualifiedTableName();

        // used by?
        // https://sites.google.com/a/jsc-solutions.net/backlog/knowledge-base/2014/201403/20140322
        //Func<Func<SQLiteConnection, Task>, Task> GetWithConnection();
        Func<Func<IDbConnection, Task>, Task> GetWithConnection();

        // here we could ask for table stats?
    }

    public interface IQueryStrategy
    {
        // this state knows about reset state 

        IQueryDescriptor GetDescriptor();

        List<Action<QueryStrategyExtensions.CommandBuilderState>> GetCommandBuilder();


        // Stack<Apply>
    }


    public interface IQueryStrategy<TRow> : IQueryStrategy
    {
        // this class exists to make LINQ happy

        // X:\jsc.svn\examples\javascript\forms\Test\TestSQLJoin\TestSQLJoin\ApplicationWebService.cs
        // https://sites.google.com/a/jsc-solutions.net/backlog/knowledge-base/2014/201405/20140501

    }


    [Obsolete("we need to refactor this into a jsc market nuget. can this nuget also embedd the asset compiler for jsc?")]
    public static class QueryStrategyExtensions
    {
        // X:\jsc.svn\examples\javascript\forms\Test\TestSQLJoin\TestSQLJoin\ApplicationWebService.cs

        // SQLite.Linq reference implementation
        // when can we have immutable version?

        // http://msdn.microsoft.com/en-us/library/bb310804.aspx

        #region where
        // behave like StringBuilder where core data is mutable?
        public static void MutableWhere(IQueryStrategy that, LambdaExpression filter)
        {
            // to make it immutable, we would need to have Clone method
            // https://sites.google.com/a/jsc-solutions.net/backlog/knowledge-base/2014/201401/20140112/count
            // X:\jsc.svn\examples\javascript\Test\TestIQueryable\TestIQueryable\ApplicationWebService.cs
            // X:\jsc.svn\examples\javascript\svg\SVGNavigationTiming\SVGNavigationTiming\ApplicationWebService.cs
            // X:\jsc.svn\examples\javascript\forms\Test\TestSQLiteEnumWhere\TestSQLiteEnumWhere\ApplicationWebService.cs


            // for op_Equals
            var body = ((BinaryExpression)filter.Body);

            // do we need to check our db schema or is reflection the schema for us?
            #region ColumnName
            var ColumnName = "";

            if (body.Left is MemberExpression)
            {
                ColumnName = ((MemberExpression)body.Left).Member.Name;
            }
            else if (body.Left is UnaryExpression)
            {
                ColumnName = ((MemberExpression)((UnaryExpression)body.Left).Operand).Member.Name;
            }
            else
            {
                Debugger.Break();
            }
            #endregion

            var rAddParameterValue = default(object);

            if (body.Right is MemberExpression)
            {
                var f_Body_Right = (MemberExpression)body.Right;

                //+		(new System.Linq.Expressions.Expression.ConstantExpressionProxy((new System.Linq.Expressions.Expression.MemberExpressionProxy(f_Body_Right as System.Linq.Expressions.FieldExpression)).Expression as System.Linq.Expressions.ConstantExpression)).Value	{AppEngineWhereOperator.ApplicationWebService.}	object {AppEngineWhereOperator.ApplicationWebService.}

                // +		(new System.Linq.Expressions.Expression.MemberExpressionProxy(f_Body_Right.Expression as System.Linq.Expressions.FieldExpression)).Member	{SVGNavigationTiming.Design.PerformanceResourceTimingData2ApplicationResourcePerformanceRow k}	System.Reflection.MemberInfo {System.Reflection.RtFieldInfo}



                var f_Body_Right_as_ConstantExpression = f_Body_Right.Expression as ConstantExpression;
                var f_Body_Right_as_MemberExpression = f_Body_Right.Expression as MemberExpression;
                if (f_Body_Right_as_ConstantExpression != null)
                {

                    var f_Body_Right_Expression_Value = f_Body_Right_as_ConstantExpression.Value;
                    rAddParameterValue = ((FieldInfo)f_Body_Right.Member).GetValue(f_Body_Right_Expression_Value);
                }
                else if (f_Body_Right_as_MemberExpression != null)
                {
                    // we are doing a where against object field passed method argument

                    var z = (FieldInfo)f_Body_Right_as_MemberExpression.Member;

                    var zE = f_Body_Right_as_MemberExpression.Expression as ConstantExpression;

                    var f_Body_Right_Expression_Value = z.GetValue(zE.Value);


                    rAddParameterValue = ((FieldInfo)f_Body_Right.Member).GetValue(f_Body_Right_Expression_Value);
                }
                else Debugger.Break();

            }
            else if (body.Right is UnaryExpression)
            {
                // casting enum to long?

                var f_Body_Right = (MemberExpression)((UnaryExpression)body.Right).Operand;

                var f_Body_Right_as_ConstantExpression = f_Body_Right.Expression as ConstantExpression;
                var f_Body_Right_as_MemberExpression = f_Body_Right.Expression as MemberExpression;

                //var f_Body_Right_Expression = (ConstantExpression)f_Body_Right.Expression;

                //var f_Body_Right_Expression_Value = f_Body_Right_Expression.Value;
                //r = ((FieldInfo)f_Body_Right.Member).GetValue(f_Body_Right_Expression_Value);

                if (f_Body_Right_as_ConstantExpression != null)
                {

                    var f_Body_Right_Expression_Value = f_Body_Right_as_ConstantExpression.Value;
                    rAddParameterValue = ((FieldInfo)f_Body_Right.Member).GetValue(f_Body_Right_Expression_Value);
                }
                else if (f_Body_Right_as_MemberExpression != null)
                {
                    // we are doing a where against object field passed method argument

                    var z = (FieldInfo)f_Body_Right_as_MemberExpression.Member;

                    var zE = f_Body_Right_as_MemberExpression.Expression as ConstantExpression;

                    var f_Body_Right_Expression_Value = z.GetValue(zE.Value);


                    rAddParameterValue = ((FieldInfo)f_Body_Right.Member).GetValue(f_Body_Right_Expression_Value);
                }
                else Debugger.Break();
            }
            else
            {
                // https://sites.google.com/a/jsc-solutions.net/backlog/knowledge-base/2014/201405/201405

                var asConstantExpression = body.Right as ConstantExpression;
                if (asConstantExpression != null)
                {
                    rAddParameterValue = asConstantExpression.Value;
                }
                else Debugger.Break();
            }

            //Additional information: Unable to cast object of type 'System.Linq.Expressions.UnaryExpression' to type 'System.Linq.Expressions.MemberExpression'.

            //Additional information: Unable to cast object of type 'System.Linq.Expressions.UnaryExpression' to type 'System.Linq.Expressions.MemberExpression'.

            // http://stackoverflow.com/questions/9241607/whats-wrong-with-system-linq-expressions-logicalbinaryexpression-class
            //var f_Body_Left_as_MemberExpression = (MemberExpression)body.Left;
            //var f_Body_Right_as_ConstantExpression = (ConstantExpression)f_Body_as_BinaryExpression.Right;




            // for non op_Equals
            //var f_Body_as_MethodCallExpression = ((MethodCallExpression)f.Body);
            ////Console.WriteLine("IBook1Sheet1Queryable.Where");

            //var f_Body_Left_as_MemberExpression = (MemberExpression)f_Body_as_MethodCallExpression.Arguments[0];
            //var f_Body_Right_as_ConstantExpression = (ConstantExpression)f_Body_as_MethodCallExpression.Arguments[1];

            //Console.WriteLine("IBook1Sheet1Queryable.Where " + new { f_Body_as_MethodCallExpression.Method, f_Body_Left_as_MemberExpression.Member.Name, f_Body_Right_as_ConstantExpression.Value });
            Console.WriteLine("MutableWhere " + new
            {
                body.Method,

                //NodeType	Equal	System.Linq.Expressions.ExpressionType
                body.NodeType,


                ColumnName,
                Right = rAddParameterValue
            });


            that.GetCommandBuilder().Add(
                state =>
                {
                    //MutableWhere { Method = Boolean op_Equality(System.String, System.String), Left = Goo, Right = Goo0 }

                    var n = "@arg" + state.ApplyParameter.Count;

                    // what about multple where clauses, what about sub queries?
                    // X:\jsc.svn\examples\javascript\forms\Test\TestSQLiteEnumWhere\TestSQLiteEnumWhere\ApplicationWebService.cs

                    // state.WhereCommand = " where `FooStateEnum` = @arg0"

                    if (string.IsNullOrEmpty(state.WhereCommand))
                    {
                        // this is the first where clause

                        state.WhereCommand = " where ";

                    }
                    else
                    {
                        // this wants to add to the where clause
                        // http://www.w3schools.com/sql/sql_and_or.asp

                        state.WhereCommand += " and ";
                    }


                    state.WhereCommand += "`" + ColumnName + "` ";

                    // like we do in jsc. this is the opcode
                    //OpCodes.Ceq
                    if (body.NodeType == ExpressionType.Equal)
                        state.WhereCommand += "=";
                    else if (body.NodeType == ExpressionType.LessThan)
                        state.WhereCommand += "<";
                    else if (body.NodeType == ExpressionType.GreaterThan)
                        state.WhereCommand += ">";
                    else
                        Debugger.Break();


                    state.WhereCommand += " ";
                    state.WhereCommand += n;

                    Console.WriteLine("MutableWhere " + new { n, r = rAddParameterValue });

                    state.ApplyParameter.Add(
                        c =>
                        {
                            // either the actualt command or the explain command?

                            //c.Parameters.AddWithValue(n, r);
                            c.AddParameter(n, rAddParameterValue);
                        }
                    );
                }
            );
        }

        #endregion




        #region select sum
        // can this be used in a join?
        [Obsolete("this is somewhat like select foo and then sum, or like orderby. what about summing vec3"
            )]
        public static long Sum(IQueryStrategy Strategy, Expression selector)
        {
            // http://stackoverflow.com/questions/3785995/sqlite-accumulator-sum-column-in-a-select-statement
            // http://www.tutorialspoint.com/sqlite/sqlite_useful_functions.htm
            //throw new NotImplementedException("sqlite does not have it yet");
            // http://sqlite.1065341.n5.nabble.com/SUM-and-NULL-values-td2257.html

            var body = ((MemberExpression)((LambdaExpression)selector).Body);

            // do we need to check our db schema or is reflection the schema for us?
            #region ColumnName
            var ColumnName = "";

            ColumnName = body.Member.Name;
            #endregion



            return ((Task<long>)Strategy.GetDescriptor().GetWithConnection()(
                c =>
                {
                    var state = AsCommandBuilder(Strategy);

                    // override
                    state.SelectCommand = "select sum(`" + ColumnName + "`) ";

                    //var cmd = new SQLiteCommand(state.ToString(), c);
                    var cmd = c.CreateCommand(state.ToString());

                    foreach (var item in state.ApplyParameter)
                    {
                        item(cmd);
                    }

                    var s = new TaskCompletionSource<long>();


                    s.SetResult(
                    (long)cmd.ExecuteScalar()
                    );

                    //var r = cmd.ExecuteReader();

                    //if (r.NextResult())
                    //{
                    //    //ex = {"No current row"}
                    //    s.SetResult(
                    //        r.GetInt64(0)
                    //    );
                    //}

                    return s.Task;
                }
            )).Result;
        }
        #endregion





        #region order by
        public static void MutableOrderBy(IQueryStrategy that, Expression selector)
        {
            #region ColumnName
            var ColumnName = "";

            // +		Member	{System.String path}	System.Reflection.MemberInfo {System.Reflection.RtFieldInfo}
            var body = ((LambdaExpression)selector).Body;

            // unpack the convert?
            var body_as_UnaryExpression = body as UnaryExpression;
            var body_as_MemberExpression = body as MemberExpression;
            if (body_as_UnaryExpression != null)
            {
                ColumnName = ((MemberExpression)(body_as_UnaryExpression).Operand).Member.Name;
            }
            else if (body_as_MemberExpression != null)
            {
                ColumnName = body_as_MemberExpression.Member.Name;
            }
            else Debugger.Break();
            #endregion

            Console.WriteLine("MutableOrderBy " + new { ColumnName });

            that.GetCommandBuilder().Add(
             state =>
             {
                 //MutableWhere { Method = Boolean op_Equality(System.String, System.String), Left = Goo, Right = Goo0 }


                 state.OrderByCommand = "order by `" + ColumnName + "`";
             }
            );
        }

        public static void MutableOrderByDescending(IQueryStrategy that, Expression selector)
        {


            // do we need to check our db schema or is reflection the schema for us?
            #region ColumnName
            var ColumnName = "";

            // +		Member	{System.String path}	System.Reflection.MemberInfo {System.Reflection.RtFieldInfo}
            var body = ((LambdaExpression)selector).Body;

            // unpack the convert?
            var body_as_UnaryExpression = body as UnaryExpression;
            var body_as_MemberExpression = body as MemberExpression;
            if (body_as_UnaryExpression != null)
            {
                ColumnName = ((MemberExpression)(body_as_UnaryExpression).Operand).Member.Name;
            }
            else if (body_as_MemberExpression != null)
            {
                ColumnName = body_as_MemberExpression.Member.Name;
            }
            else Debugger.Break();
            #endregion

            Console.WriteLine("MutableOrderByDescending " + new { ColumnName });

            that.GetCommandBuilder().Add(
             state =>
             {
                 //MutableWhere { Method = Boolean op_Equality(System.String, System.String), Left = Goo, Right = Goo0 }

                 var n = "@arg" + state.ApplyParameter.Count;

                 state.OrderByCommand = "order by `" + ColumnName + "` desc";
             }
            );
        }
        #endregion


        [Obsolete("caller has the option to clone the state before calling this function. should jsc add static expression pooling/caching like c# compiler does for lambdas?")]
        public static void MutableTake(IQueryStrategy that, long count)
        {
            // should the caller take care of cloning the instance?
            // should we start using Trace for logging?
            Console.WriteLine("MutableTake " + new { count });

            that.GetCommandBuilder().Add(
             state =>
             {
                 //MutableWhere { Method = Boolean op_Equality(System.String, System.String), Left = Goo, Right = Goo0 }

                 var n = "@arg" + state.ApplyParameter.Count;

                 // https://sites.google.com/a/jsc-solutions.net/backlog/knowledge-base/2014/201401/20140119
                 // limit 0?
                 state.LimitCommand = "limit " + n;

                 Console.WriteLine("MutableTake " + new { n, count });


                 state.ApplyParameter.Add(
                     c =>
                     {
                         // either the actualt command or the explain command?

                         //c.Parameters.AddWithValue(n, count);
                         c.AddParameter(n, count);
                     }
                 );
             }
            );
        }


        public static long Count(IQueryStrategy Strategy)
        {
            return ((Task<long>)Strategy.GetDescriptor().GetWithConnection()(
                c =>
                {
                    var state = AsCommandBuilder(Strategy);

                    // override
                    state.SelectCommand = "select count(*)";

                    var cmd = c.CreateCommand(state.ToString());

                    foreach (var item in state.ApplyParameter)
                    {
                        item(cmd);
                    }

                    var s = new TaskCompletionSource<long>();
                    s.SetResult((long)cmd.ExecuteScalar());

                    return s.Task;
                }
            )).Result;
        }


        public static DataTable AsDataTable(IQueryStrategy Strategy)
        {
            Console.WriteLine("AsDataTable");

            return ((Task<DataTable>)Strategy.GetDescriptor().GetWithConnection()(
                c =>
                {
                    var state = AsCommandBuilder(Strategy);

                    //var cmd = new SQLiteCommand(state.ToString(), c);

                    var cmd = (DbCommand)c.CreateCommand();


                    //ex = {"no such column: dealer.Key"}
                    cmd.CommandText = state.ToString();

                    // https://sites.google.com/a/jsc-solutions.net/backlog/knowledge-base/2014/201405/20140501

                    foreach (var item in state.ApplyParameter)
                    {
                        item(cmd);
                    }

                    var t = new DataTable();



                    // X:\jsc.svn\core\ScriptCoreLib\Shared\BCLImplementation\System\Data\Common\DbDataAdapter.cs
                    // will this work under CLR too?

                    //var a = new SQLiteDataAdapter(cmd);
                    var a = new __DbDataAdapter { SelectCommand = cmd };

                    //System.Data.XSQLite.dll!Community.CsharpSqlite.Sqlite3.fetchPayload(Community.CsharpSqlite.Sqlite3.BtCursor pCur, ref int pAmt, ref int outOffset, bool skipKey)	Unknown
                    //System.Data.XSQLite.dll!Community.CsharpSqlite.Sqlite3.sqlite3BtreeKeyFetch(Community.CsharpSqlite.Sqlite3.BtCursor pCur, ref int pAmt, ref int outOffset)	Unknown
                    //System.Data.XSQLite.dll!Community.CsharpSqlite.Sqlite3.sqlite3VdbeExec(Community.CsharpSqlite.Sqlite3.Vdbe p)	Unknown
                    //System.Data.XSQLite.dll!Community.CsharpSqlite.Sqlite3.sqlite3Step(Community.CsharpSqlite.Sqlite3.Vdbe p)	Unknown
                    //System.Data.XSQLite.dll!Community.CsharpSqlite.Sqlite3.sqlite3_step(Community.CsharpSqlite.Sqlite3.Vdbe pStmt)	Unknown
                    //System.Data.XSQLite.dll!System.Data.SQLite.SQLiteCommand.ExecuteStatement(Community.CsharpSqlite.Sqlite3.Vdbe pStmt, out int cols, out System.IntPtr pazValue, out System.IntPtr pazColName)	Unknown
                    //System.Data.XSQLite.dll!System.Data.SQLite.SQLiteDataReader.ReadpVm(Community.CsharpSqlite.Sqlite3.Vdbe pVm, int version, System.Data.SQLite.SQLiteCommand cmd)	Unknown
                    //System.Data.XSQLite.dll!System.Data.SQLite.SQLiteDataReader.SQLiteDataReader(System.Data.SQLite.SQLiteCommand cmd, Community.CsharpSqlite.Sqlite3.Vdbe pVm, int version)	Unknown
                    //System.Data.XSQLite.dll!System.Data.SQLite.SQLiteCommand.ExecuteReader(System.Data.CommandBehavior behavior, bool want_results, out int rows_affected)	Unknown
                    //System.Data.XSQLite.dll!System.Data.SQLite.SQLiteCommand.ExecuteReader(System.Data.CommandBehavior behavior)	Unknown
                    //System.Data.XSQLite.dll!System.Data.SQLite.SQLiteCommand.ExecuteDbDataReader(System.Data.CommandBehavior behavior)	Unknown
                    //System.Data.dll!System.Data.Common.DbCommand.ExecuteReader()	Unknown
                    //ScriptCoreLib.dll!ScriptCoreLib.Shared.BCLImplementation.System.Data.Common.__DbDataAdapter.Fill(System.Data.DataTable dataTable)	Unknown

                    var ss = Stopwatch.StartNew();

                    Console.WriteLine("before Fill");
                    a.Fill(t);
                    Console.WriteLine("after Fill " + new { ss.ElapsedMilliseconds, t.Rows.Count });


                    var s = new TaskCompletionSource<DataTable>();
                    s.SetResult(t);

                    return s.Task;
                }
            )).Result;
        }


        public class CommandBuilderState
        {
            public IQueryStrategy Strategy;


            public string SelectCommand;
            public string FromCommand;
            public string WhereCommand;
            public string OrderByCommand;
            public string LimitCommand;

            public List<Action<IDbCommand>> ApplyParameter = new List<Action<IDbCommand>>();

            public override string ToString()
            {
                var w = new StringBuilder();

                w.AppendLine(this.SelectCommand);
                w.AppendLine(this.FromCommand);
                w.AppendLine(this.WhereCommand);

                w.AppendLine(this.OrderByCommand);
                w.AppendLine(this.LimitCommand);

                var x = w.ToString();

                Console.WriteLine(x);

                return x;

            }
        }

        public static CommandBuilderState AsCommandBuilder(IQueryStrategy Strategy)
        {
            // time to build the CommandText

            var StrategyDescriptor = Strategy.GetDescriptor();

            var state = new CommandBuilderState
            {
                Strategy = Strategy,


            };

            // X:\jsc.svn\examples\javascript\forms\Test\TestSQLJoin\TestSQLJoin\ApplicationWebService.cs
            if (StrategyDescriptor != null)
            {
                // http://www.linkedin.com/groups/select-vs-selecting-all-columns-66097.S.206400776
                state.SelectCommand = StrategyDescriptor.GetSelectAllColumnsText();

                state.FromCommand = "from `" + StrategyDescriptor.GetQualifiedTableName() + "`";
            }

            foreach (var item in Strategy.GetCommandBuilder())
            {
                item(state);
            }

            return state;
        }
    }
}
