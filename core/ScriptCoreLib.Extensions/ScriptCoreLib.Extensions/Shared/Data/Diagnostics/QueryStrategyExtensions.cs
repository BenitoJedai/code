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


        #region Join
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
        #endregion




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


            var asMemberInitExpression = keySelector.Body as MemberInitExpression;

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
                         "select 0 as foo ";



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




                     s.FromCommand += " as s";

                     // http://www.w3schools.com/sql/sql_func_last.asp
                     s.SelectCommand = "select 0 as foo";

                     //+ "s.x,\n\t"
                     // // specialname
                     //+ "s.`Key`,\n\t"
                     //+ "s.Title,\n\t"
                     // //+ "s.GooStateEnum,\n\t"
                     // + "sum(s.x) as SumOfx,\n\t";
                     //+ "13 as SumOfx, "
                     //+ "count(*) as Count";

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

                                         state.SelectCommand += ",\n\t g.`" + asMemberAssignment.Member.Name + "`";
                                         s.SelectCommand += ",\n\t count(*) as `" + asMemberAssignment.Member.Name + "`";

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

                                             state.SelectCommand += ",\n\t g.`" + asMemberAssignment.Member.Name + "`";
                                             s.SelectCommand += ",\n\t sum(s.`" + asMemberExpression.Member.Name + "`) as `" + asMemberAssignment.Member.Name + "`";
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

                                             s.SelectCommand += ",\n\t s.`" + GroupBy_asMemberExpression.Member.Name + "` as `" + asMemberAssignment.Member.Name + "`";
                                             return;
                                         }
                                         #endregion

                                         // Method = {TestSQLiteGroupBy.Data.Book1MiddleRow First[GooStateEnum,Book1MiddleRow](TestSQLiteGroupBy.IQueryStrategyGrouping`2[TestSQLiteGroupBy.Data.GooStateEnum,TestSQLiteGroupBy.Data.Book1MiddleRow])}
                                         var asMemberExpressionMethodCallExpression = asMemberExpression.Expression as MethodCallExpression;
                                         Console.WriteLine(new { index, asMemberExpressionMethodCallExpression });
                                         if (asMemberExpressionMethodCallExpression != null)
                                         {
                                             Console.WriteLine(new { index, asMemberExpressionMethodCallExpression, asMemberExpressionMethodCallExpression.Method.Name });

                                             // special!
                                             if (asMemberExpressionMethodCallExpression.Method.Name.TakeUntilIfAny("_") == "First")
                                             {
                                                 state.SelectCommand += ",\n\t gDescendingByKey.`" + asMemberAssignment.Member.Name + "`";
                                                 s.SelectCommand += ",\n\t s.`" + asMemberExpression.Member.Name + "` as `" + asMemberAssignment.Member.Name + "`";
                                                 return;
                                             }

                                             if (asMemberExpressionMethodCallExpression.Method.Name.TakeUntilIfAny("_") == "Last")
                                             {
                                                 state.SelectCommand += ",\n\t g.`" + asMemberAssignment.Member.Name + "`";
                                                 s.SelectCommand += ",\n\t s.`" + asMemberExpression.Member.Name + "` as `" + asMemberAssignment.Member.Name + "`";
                                                 return;
                                             }
                                         }
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
                                             state.SelectCommand += ",\n\t gDescendingByKey.`" + asMemberAssignment.Member.Name + "`";
                                             s.SelectCommand += ",\n\t s.`Key` as `" + asMemberAssignment.Member.Name + "`";
                                             return;
                                         }

                                         if (asMemberExpressionMethodCallExpression.Method.Name.TakeUntilIfAny("_") == "Last")
                                         {
                                             state.SelectCommand += ",\n\t g.`" + asMemberAssignment.Member.Name + "`";
                                             s.SelectCommand += ",\n\t s.`Key` as `" + asMemberAssignment.Member.Name + "`";
                                             return;
                                         }
                                     }
                                 }
                                 #endregion



                             }

                             Debugger.Break();
                         }
                     );


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
                     s.GroupByCommand = "group by `" + GroupBy_asMemberExpression.Member.Name + "`";

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

                     state.FromCommand =
                          "from (\n\t"
                            + s.ToString().Replace("\n", "\n\t")
                            + "\n) as g ";

                     // http://msdn.microsoft.com/en-us/library/vstudio/bb386996(v=vs.100).aspx


                     // hack it. no longer useable later
                     // http://help.sap.com/abapdocu_702/en/abaporderby_clause.htm#!ABAP_ALTERNATIVE_1@1@
                     //  ORDER BY { {PRIMARY KEY}

                     s.FromCommand = "from (select * " + s.FromCommand + " order by `Key` desc) as s";
                     //s.FromCommand = "from (select * " + s.FromCommand + " order by PRIMARY KEY desc)";

                     state.FromCommand +=
                        "inner join (\n\t"
                           + s.ToString().Replace("\n", "\n\t")
                            + "\n) as gDescendingByKey";

                     //state.FromCommand += " on g.GooStateEnum = gDescendingByKey.GooStateEnum";
                     state.FromCommand += " on g.`" + GroupBy_asMemberExpression.Member.Name + "` = gDescendingByKey.`" + GroupBy_asMemberExpression.Member.Name + "`";


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


// change namespace?
namespace ScriptCoreLib.Shared.Data.Diagnostics
{


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



            #region rAddParameterValue
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
            #endregion


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

        #region select count
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
        #endregion



        public static DataTable AsDataTable(IQueryStrategy Strategy)
        {
            // X:\jsc.svn\examples\javascript\forms\Test\TestSQLiteGroupBy\TestSQLiteGroupBy\ApplicationWebService.cs

            Console.WriteLine("AsDataTable");

            if (Strategy == null)
            {
                if (Debugger.IsAttached)
                    Debugger.Break();

                throw new ArgumentNullException("Strategy");
            }

            //System.Diagnostics.Contracts.Contract.Assume

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




                    // X:\jsc.svn\core\ScriptCoreLib\Shared\BCLImplementation\System\Data\Common\DbDataAdapter.cs
                    // will this work under CLR too?

                    // http://stackoverflow.com/questions/12608025/how-to-construct-a-sqlite-query-to-group-by-order
                    // http://www.devart.com/dotconnect/sqlite/docs/Devart.Data.SQLite~Devart.Data.SQLite.SQLiteDataReader~NextResult.html
                    // http://www.maplesoft.com/support/help/Maple/view.aspx?path=Database/Statement/NextResult
                    // To issue a multi-statement SQL string, the Execute command must be used.
                    //  Some databases may require that the processing of the current result be completed before the next result is returned by NextResult.
                    // http://www.java2s.com/Tutorial/CSharp/0560__ADO.Net/ExecutingaQueryThatReturnsMultipleResultSetswithSqlDataReader.htm
                    // http://amitchandnz.wordpress.com/2011/09/28/issues-with-idatareaderdatareader-multiple-results-sets-and-datatables/
                    // http://stuff.mit.edu/afs/athena/software/mono_v3.0/arch/i386_linux26/mono/mcs/class/Mono.Data.Sqlite/Mono.Data.Sqlite_2.0/SQLiteDataReader.cs
                    // http://zetcode.com/db/sqlitecsharp/read/
                    // http://stackoverflow.com/questions/18493169/sqlite-query-combining-two-result-sets-that-use-and
                    // http://www.sqlite.org/queryplanner.html
                    // One possible solution is to fetch all events, to a ToList() and do the grouping in-memory.
                    // http://blog.csainty.com/2008/01/linq-to-sql-groupby.html
                    // http://msdn.microsoft.com/en-us/library/vstudio/bb386922(v=vs.100).aspx

                    //var reader = cmd.ExecuteReader();
                    ////var reader = cmd.ExecuteReader();

                    ////Console.WriteLine(
                    ////    new
                    ////    {
                    ////        reader.Depth,
                    ////        reader.FieldCount
                    ////        //reader.NextResult

                    ////    }
                    ////);

                    //var a = new SQLiteDataAdapter(cmd);

                    // http://msdn.microsoft.com/en-us/library/bh8kx08z(v=vs.110).aspx

                    var a = new __DbDataAdapter { SelectCommand = cmd };
                    //var a = new SQLiteDataAdapter { SelectCommand = cmd };

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
                    var t = new DataTable();
                    //var ds = new DataSet();
                    a.Fill(t);
                    // is SQLIte Fill handicapped or what?

                    //a.Fill(ds);
                    //Console.WriteLine("after Fill " + new { ss.ElapsedMilliseconds, t.Rows.Count });
                    Console.WriteLine("after Fill " + new { ss.ElapsedMilliseconds });


                    var s = new TaskCompletionSource<DataTable>();
                    //s.SetResult(ds.Tables[0]);
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

            // is it before or after other clauses or both?
            // X:\jsc.svn\examples\javascript\forms\Test\TestSQLiteGroupBy\TestSQLiteGroupBy\ApplicationWebService.cs
            public string GroupByCommand;

            public List<Action<IDbCommand>> ApplyParameter = new List<Action<IDbCommand>>();

            public override string ToString()
            {
                var w = new StringBuilder();

                w.AppendLine(this.SelectCommand);
                w.AppendLine(this.FromCommand);
                w.AppendLine(this.WhereCommand);

                w.AppendLine(this.OrderByCommand);
                w.AppendLine(this.LimitCommand);
                w.AppendLine(this.GroupByCommand);

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
