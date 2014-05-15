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
    //[Obsolete("the first generic extension method for all jsc data layer rows")]
    public static partial class QueryStrategyOfTRowExtensions
    {
        // X:\jsc.svn\examples\javascript\forms\MultipleXLSXAssets\MultipleXLSXAssets\ApplicationWebService.cs
        // X:\jsc.svn\examples\javascript\forms\ThreeWay\ThreeWay\ApplicationControl.cs
        // X:\jsc.svn\examples\javascript\forms\Test\TestSQLJoin\TestSQLJoin\ApplicationWebService.cs


        [ScriptCoreLib.ScriptAttribute.ExplicitInterface]
        interface IJoinQueryStrategy
        {
            // allow to inspect upper select . what if there are multiple upper selects?
            Expression resultSelectorExpression { get; }

            IQueryStrategy xouter { get; }
            IQueryStrategy xinner { get; }
            Expression outerKeySelector { get; }

            // ? gDescendingByKeyReferenced

            IJoinQueryStrategy upperJoin { get; set; }
        }

        class JoinQueryStrategy<TOuter, TInner, TKey, TResult> : XQueryStrategy<TResult>, IJoinQueryStrategy
        {
            public IQueryStrategy<TOuter> xouter { get; set; }
            public IQueryStrategy<TInner> xinner { get; set; }
            public Expression<Func<TOuter, TKey>> outerKeySelector { get; set; }
            public Expression<Func<TOuter, TInner, TResult>> resultSelector;

            public IJoinQueryStrategy upperJoin { get; set; }

            #region IJoinQueryStrategy
            Expression IJoinQueryStrategy.resultSelectorExpression
            {
                get { return resultSelector; }
            }

            IQueryStrategy IJoinQueryStrategy.xouter
            {
                get { return xouter; }
            }

            IQueryStrategy IJoinQueryStrategy.xinner
            {
                get { return xinner; }
            }



            Expression IJoinQueryStrategy.outerKeySelector
            {
                get
                {
                    return this.outerKeySelector;
                }

            }
            #endregion


        }


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
            Console.WriteLine("Join " + new { resultSelector });


            // X:\jsc.svn\examples\javascript\forms\Test\TestSQLJoin\TestSQLJoin\ApplicationWebService.cs

            // how do we get this barely functional?

            // can we manually convince this thing to include the join clause?
            //var that = new __Book1_TheView();
            var that = new JoinQueryStrategy<TOuter, TInner, TKey, TResult>
            {
                xouter = xouter,
                xinner = xinner,

                outerKeySelector = outerKeySelector,

                resultSelector = resultSelector,


                InternalGetDescriptor =
                    () =>
                    {
                        // inherit the connection/context from above
                        var StrategyDescriptor = xouter.GetDescriptor();

                        return StrategyDescriptor;
                    }
            };





            that.GetCommandBuilder().Add(
                state =>
                {
                    Console.WriteLine("Join CommandBuilder " + new { that });


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

                    var asGroupByQueryStrategy = that.xouter as IGroupByQueryStrategy;
                    if (asGroupByQueryStrategy != null)
                    {
                        asGroupByQueryStrategy.upperJoin = that;
                    }


                    //var __ISelectQueryStrategy = that.xouter as ISelectQueryStrategy;



                    // https://sites.google.com/a/jsc-solutions.net/backlog/knowledge-base/2014/201405/20140513
                    (xouter as IJoinQueryStrategy).With(j => j.upperJoin = that);


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



                    // https://sites.google.com/a/jsc-solutions.net/backlog/knowledge-base/2014/201405/20140515

                    var FromCommand =
                        "from "
                            + xouter_SelectAll.GetQualifiedTableNameOrToString().Replace("\n", "\n\t")
                            + " as " + xouter_Paramerer_Name.Replace("<>", "__") + " inner join "
                            + xinner_SelectAll.GetQualifiedTableNameOrToString().Replace("\n", "\n\t")
                            + " as " + xinner_Paramerer.Name.Replace("<>", "__");
                    #endregion




                    //-		xouter_asMemberExpression.Expression as MemberExpression	{<>h__TransparentIdentifier0.contact}	System.Linq.Expressions.MemberExpression {System.Linq.Expressions.PropertyExpression}


                    #region on . equals .
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
                        if (xouter is IGroupByQueryStrategy)
                        {
                            // https://sites.google.com/a/jsc-solutions.net/backlog/knowledge-base/2014/201405/20140513

                            FromCommand += " \non "
                                + xouter_Paramerer_Name.Replace("<>", "__") + ".`Grouping.Key`"
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


                    }
                    #endregion




                    // xouter_SelectAll = "select `Key`, `DealerId`, `DealerContactText`, `Tag`, `Timestamp`"

                    // http://stackoverflow.com/questions/5090513/how-do-you-avoid-column-name-conflicts



                    #region SelectCommand
                    var SelectCommand = default(string);


                    #region AddToSelectCommand
                    Action<string> AddToSelectCommand =
                        x =>
                        {
                            if (SelectCommand == null)
                                SelectCommand = "select " + x;
                            else
                                SelectCommand += ",\n\t " + x;
                        };
                    #endregion


                    var asMemberInitExpression = asLambdaExpression.Body as MemberInitExpression;
                    var asMemberInitExpressionByParameter0 = default(ParameterExpression);
                    var asMemberInitExpressionByParameter1 = default(ParameterExpression);

                    if (asMemberInitExpression == null)
                    {

                        if (that.upperJoin != null)
                        {
                            if (that.upperJoin.xouter == that)
                            {
                                asMemberInitExpression = (that.upperJoin.resultSelectorExpression as LambdaExpression).Body as MemberInitExpression;
                                asMemberInitExpressionByParameter0 = (that.upperJoin.resultSelectorExpression as LambdaExpression).Parameters[0];


                                var asMemberExpression = (that.upperJoin.outerKeySelector as LambdaExpression).Body as MemberExpression;
                                var asMMemberExpression = asMemberExpression.Expression as MemberExpression;

                                //if (asMemberInitExpression != null)


                                if (asLambdaExpression.Parameters.Any(p => p.Name == asMMemberExpression.Member.Name))
                                {
                                    // we seem to have that table!
                                    AddToSelectCommand(
                                               asMMemberExpression.Member.Name + "." + asMemberExpression.Member.Name + " as "
                                               + asMMemberExpression.Member.Name + "_" + asMemberExpression.Member.Name
                                               );
                                }
                                else
                                {
                                    // we seem to get it by projection?

                                    AddToSelectCommand(
                                         asMMemberExpression.Member.Name + "_" + asMemberExpression.Member.Name + " as "
                                         + asMMemberExpression.Member.Name + "_" + asMemberExpression.Member.Name
                                         );
                                }


                                if (asMemberInitExpression == null)
                                {
                                    // ???

                                    if (that.upperJoin.upperJoin.xouter == that.upperJoin)
                                    {
                                        asMemberInitExpression = (that.upperJoin.upperJoin.resultSelectorExpression as LambdaExpression).Body as MemberInitExpression;
                                        //asMemberInitExpressionByParameter0 = (GroupBy.upperJoin.upperJoin.resultSelectorExpression as LambdaExpression).Parameters[0];
                                        asMemberInitExpressionByParameter1 = (that.upperJoin.upperJoin.resultSelectorExpression as LambdaExpression).Parameters[0];

                                        //if (asMemberInitExpression != null)
                                        //    AddToSelectCommand(
                                        //               asMMemberExpression.Member.Name + "." + asMemberExpression.Member.Name + " as "
                                        //               + asMMemberExpression.Member.Name + "_" + asMemberExpression.Member.Name
                                        //               );
                                    }



                                }

                            }
                        }
                    }



                    if (asMemberInitExpression == null)
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
                        // shall we generate any missing bindings as constants?
                        // so if we do a group by and Last on it
                        // we are atleast able to select the constants?

                        // remember this is supposed to work on JVM too
                        var ImplicitConstantFields = asMemberInitExpression.Type.GetFields().Where(
                            xx =>
                            {
                                if (asMemberInitExpression.Bindings.Any(y => y.Member.Name == xx.Name))
                                    return false;

                                return true;
                            }
                        );




                        var ImplicitConstantBindings = ImplicitConstantFields.Select(
                            (SourceField) =>
                            {
                                // X:\jsc.svn\core\ScriptCoreLib\Shared\BCLImplementation\System\Linq\Expressions\Expression.cs

                                var SourceBinding = default(MemberBinding);

                                return new { SourceField, SourceBinding };
                            }
                        );


                        if (that.upperJoin != null)
                        {
                            //that.upperJoin.xouter
                        }

                        #region asNewExpression.Bindings
                        ImplicitConstantBindings.Concat(
                            from SourceBinding in asMemberInitExpression.Bindings
                            let SourceField = SourceBinding.Member as FieldInfo
                            select new { SourceField, SourceBinding }
                        ).WithEach(
                            mm =>
                            {


                                var TargetMemberName = mm.SourceField.Name;

                                var SourceBinding = mm.SourceBinding;

                                #region ImplicitConstantBindings
                                if (SourceBinding == null)
                                {
                                    // seems to be a default?

                                    if (mm.SourceField.FieldType == typeof(string))
                                    {
                                        // NULL?
                                        AddToSelectCommand("'' as " + TargetMemberName);
                                    }
                                    else
                                    {
                                        // long?
                                        AddToSelectCommand("0 as " + TargetMemberName);
                                    }

                                    return;
                                }
                                #endregion

                                // the name we want it to appear at later




                                // this looks like CustomAttributeBuilder thing.


                                var asMemberAssignment = SourceBinding as MemberAssignment;

                                // SourceBinding = {Content = <>h__TransparentIdentifier1.<>h__TransparentIdentifier0.UpdatesByMiddlesheet.Last().UpdatedContent}

                                #region asConstantExpression
                                var asConstantExpression = asMemberAssignment.Expression as ConstantExpression;
                                if (asConstantExpression != null)
                                {
                                    if (asConstantExpression.Type == typeof(string))
                                    {
                                        // NULL?
                                        AddToSelectCommand("'" + asConstantExpression.Value + "' as " + TargetMemberName);
                                    }
                                    else
                                    {
                                        // long?
                                        AddToSelectCommand("" + asConstantExpression.Value + " as " + TargetMemberName);
                                    }

                                    return;
                                }
                                #endregion


                                // X:\jsc.svn\examples\javascript\forms\SQLiteWithDataGridViewX\SQLiteWithDataGridViewX\ApplicationWebService.cs


                                // +		asMemberAssignment.Expression	{Convert(g.ParentContentKey)}	System.Linq.Expressions.Expression {System.Linq.Expressions.UnaryExpression}


                                #region asUnaryExpression
                                var asUnaryExpression = asMemberAssignment.Expression as UnaryExpression;
                                if (asUnaryExpression != null)
                                {
                                    // +		(new System.Linq.Expressions.Expression.UnaryExpressionProxy(asUnaryExpression)).Operand	{g.ParentContentKey}	System.Linq.Expressions.Expression {System.Linq.Expressions.FieldExpression}
                                    var asUnaryExpression_Operand_asFieldExpression = asUnaryExpression.Operand as MemberExpression;
                                    if (asUnaryExpression_Operand_asFieldExpression != null)
                                    {



                                        var asUnaryExpression_Operand_asFMemberExpression = asUnaryExpression_Operand_asFieldExpression.Expression as MemberExpression;
                                        if (asUnaryExpression_Operand_asFMemberExpression != null)
                                        {
                                            // what level are we operating at?
                                            // 		(new System.Linq.Expressions.Expression.ParameterExpressionProxy((
                                            // new System.Linq.Expressions.Expression.MemberExpressionProxy(
                                            // asUnaryExpression_Operand_asFMemberExpression as System.Linq.Expressions.PropertyExpression)).Expression as System.Linq.Expressions.TypedParameterExpression)).Name	"<>h__TransparentIdentifier0"	string







                                            if (asLambdaExpression.Parameters.Any(p => p.Name == asUnaryExpression_Operand_asFMemberExpression.Member.Name))
                                            {
                                                AddToSelectCommand(
                                                     asUnaryExpression_Operand_asFMemberExpression.Member.Name + "." +
                                                     asUnaryExpression_Operand_asFieldExpression.Member.Name
                                                     + " as " + asUnaryExpression_Operand_asFMemberExpression.Member.Name + "_" + asUnaryExpression_Operand_asFieldExpression.Member.Name
                                                     );
                                                return;
                                            }


                                            var asUnaryExpression_Operand_asFMMemberExpression = asUnaryExpression_Operand_asFMemberExpression.Expression as MemberExpression;
                                            if (asUnaryExpression_Operand_asFMMemberExpression != null)
                                            {
                                                // asUnaryExpression_Operand_asFMemberExpression.Expression = {<>h__TransparentIdentifier1.<>h__TransparentIdentifier0}
                                                //var __projection = asUnaryExpression_Operand_asFMMemberExpression.Expression as ParameterExpression;
                                                //var __projection = asUnaryExpression_Operand_asFMemberExpression.Expression as ParameterExpression;

                                                AddToSelectCommand(

                                                    asLambdaExpression.Parameters[0].Name.Replace("<>", "__") + "."
                                                    + asUnaryExpression_Operand_asFMemberExpression.Member.Name + "_" +
                                                    asUnaryExpression_Operand_asFieldExpression.Member.Name
                                                    + " as " + TargetMemberName
                                                    );
                                                return;
                                            }


                                            {

                                                // asUnaryExpression_Operand_asFMemberExpression.Expression = {<>h__TransparentIdentifier1.<>h__TransparentIdentifier0}
                                                var __projection = asUnaryExpression_Operand_asFMemberExpression.Expression as ParameterExpression;

                                                AddToSelectCommand(
                                                    __projection.Name.Replace("<>", "__") + "."
                                                    + asUnaryExpression_Operand_asFMemberExpression.Member.Name + "_" +
                                                    asUnaryExpression_Operand_asFieldExpression.Member.Name
                                                    + " as " + TargetMemberName
                                                    );
                                            }
                                            return;
                                        }

                                        {
                                            // reduce? flatten?  nested join?
                                            //asFieldExpression = asFieldExpression_Expression_asFieldExpression;
                                            var __projection = asUnaryExpression_Operand_asFieldExpression.Expression as ParameterExpression;
                                            AddToSelectCommand(
                                                __projection.Name.Replace("<>", "__") + "." +
                                                asUnaryExpression_Operand_asFieldExpression.Member.Name
                                                + " as " + TargetMemberName
                                                );
                                        }

                                        return;
                                    }
                                }
                                #endregion


                                // asFieldExpression = {<>h__TransparentIdentifier0.contact.Timestamp}


                                // asFieldExpression {<>h__TransparentIdentifier1.<>h__TransparentIdentifier0.MiddleSheetz.Content}

                                #region asFieldExpression
                                var asFieldExpression = asMemberAssignment.Expression as MemberExpression;
                                if (asFieldExpression != null)
                                {
                                    #region asFMethodCallExpression
                                    var asFMethodCallExpression = asFieldExpression.Expression as MethodCallExpression;
                                    if (asFMethodCallExpression != null)
                                    {
                                        if (asFMethodCallExpression.Method.Name.TakeUntilIfAny("_") == "Last")
                                        {
                                            #region asFParameterExpression
                                            var asFParameterExpression = asFMethodCallExpression.Arguments[0] as ParameterExpression;
                                            if (asFParameterExpression != null)
                                            {
                                                AddToSelectCommand(asFParameterExpression.Name + "." + asFieldExpression.Member.Name + " as " + TargetMemberName);


                                                return;
                                            }
                                            #endregion



                                            var asFMemberExpression = asFMethodCallExpression.Arguments[0] as MemberExpression;
                                            if (asFMemberExpression != null)
                                            {
                                                // https://sites.google.com/a/jsc-solutions.net/backlog/knowledge-base/2014/201405/20140513


                                                var asFMMemberExpression = asFMemberExpression.Expression as MemberExpression;
                                                if (asFMMemberExpression != null)
                                                {
                                                    #region __projection1
                                                    var __projection1 = asFMMemberExpression.Expression as ParameterExpression;
                                                    if (__projection1 != null)
                                                    {
                                                        if (asMemberInitExpressionByParameter1 != null)
                                                            if (asMemberInitExpressionByParameter0 != null)
                                                            {
                                                                if (asMemberInitExpressionByParameter1 != __projection1)
                                                                    return;
                                                            }


                                                        // um . what level are we at?
                                                        var pp = asLambdaExpression.Parameters;
                                                        // https://sites.google.com/a/jsc-solutions.net/backlog/knowledge-base/2014/201405/20140513


                                                        if (asMemberInitExpressionByParameter1 != null)
                                                            if (asMemberInitExpressionByParameter0 != null)
                                                            {
                                                                AddToSelectCommand(
                                                                asFMemberExpression.Member.Name + "." + asFieldExpression.Member.Name + " as "
                                                                + asFMemberExpression.Member.Name + "_" + asFieldExpression.Member.Name
                                                                );

                                                                return;
                                                            }

                                                        if (asMemberInitExpressionByParameter0 != null)
                                                        {
                                                            //           __h__TransparentIdentifier0.UpdatesByMiddlesheet_UpdatedContent as UpdatedContent_UpdatedContent,

                                                            AddToSelectCommand(
                                                                asLambdaExpression.Parameters[0].Name.Replace("<>", "__") + "." +
                                                               asFMemberExpression.Member.Name + "_" + asFieldExpression.Member.Name + " as "
                                                               + asFMemberExpression.Member.Name + "_" + asFieldExpression.Member.Name
                                                               );
                                                            return;
                                                        }



                                                        AddToSelectCommand(
                                                              asLambdaExpression.Parameters[0].Name.Replace("<>", "__") + "." +
                                                             asFMemberExpression.Member.Name + "_" + asFieldExpression.Member.Name + " as "
                                                             + asFieldExpression.Member.Name
                                                             );
                                                        return;
                                                    }
                                                    #endregion

                                                }

                                                #region __projection0
                                                var __projection0 = asFMemberExpression.Expression as ParameterExpression;
                                                if (__projection0 != null)
                                                {
                                                    if (asMemberInitExpressionByParameter0 != null)
                                                    {
                                                        if (asMemberInitExpressionByParameter0 != __projection0)
                                                            return;

                                                        //__h__TransparentIdentifier0.UpdatesByMiddlesheet_UpdatedContent as Content,
                                                        //
                                                        //   UpdatesByMiddlesheet.`UpdatedContent` as  `UpdatesByMiddlesheet_UpdatedContent`,


                                                        // is it available for us?
                                                        if (asLambdaExpression.Parameters[0].Name != asFMemberExpression.Member.Name)
                                                            if (asLambdaExpression.Parameters[1].Name != asFMemberExpression.Member.Name)
                                                                return;

                                                        AddToSelectCommand(
                                                             asFMemberExpression.Member.Name + "." + asFieldExpression.Member.Name + " as "
                                                             + asFMemberExpression.Member.Name + "_" + asFieldExpression.Member.Name
                                                             );

                                                        return;

                                                    }
                                                    AddToSelectCommand(
                                                        __projection0.Name.Replace("<>", "__") + "." +
                                                        asFMemberExpression.Member.Name
                                                        + "_" + asFieldExpression.Member.Name + " as " + TargetMemberName
                                                        );
                                                    return;
                                                }
                                                #endregion

                                            }
                                        }
                                    }
                                    #endregion



                                    #region asFieldExpression_Expression_asFieldExpression
                                    var asFFieldExpression = asFieldExpression.Expression as MemberExpression;
                                    if (asFFieldExpression != null)
                                    {

                                        //// Member = {<>f__AnonymousType0`1[System.String] SpecialConstant}
                                        //// X:\jsc.svn\examples\javascript\forms\SQLiteWithDataGridViewX\SQLiteWithDataGridViewX\ApplicationWebService.cs
                                        //// var SpecialConstant = new { u = "44" };

                                        var asFPropertyInfo = asFFieldExpression.Member as FieldInfo;
                                        var asPropertyInfo = asFieldExpression.Member as PropertyInfo;
                                        //if (asPropertyInfo != null)
                                        //{
                                        //    // CLR

                                        #region asFFConstantExpression
                                        var asFFConstantExpression = asFFieldExpression.Expression as ConstantExpression;
                                        if (asFFConstantExpression != null)
                                        {
                                            // Member = {<>f__AnonymousType0`1[System.String] SpecialConstant}

                                            var value0 = asFPropertyInfo.GetValue(asFFConstantExpression.Value);
                                            var value1 = asPropertyInfo.GetValue(value0, null);


                                            if (value1 is string)
                                            {
                                                // NULL?
                                                AddToSelectCommand(
                                                    " '" + value1 + "' as `" + asMemberAssignment.Member.Name + "`");
                                            }
                                            else
                                            {
                                                // long?
                                                AddToSelectCommand(" " + value1 + " as `" + asMemberAssignment.Member.Name + "`");
                                            }

                                            return;
                                        }
                                        #endregion






                                        var asFMMemberExpression = asFFieldExpression.Expression as MemberExpression;
                                        if (asFMMemberExpression != null)
                                        {
                                            #region __projection1
                                            var __projection1 = asFMMemberExpression.Expression as ParameterExpression;
                                            if (__projection1 != null)
                                            {
                                                if (asMemberInitExpressionByParameter1 != null)
                                                    if (asMemberInitExpressionByParameter0 != null)
                                                    {
                                                        if (asMemberInitExpressionByParameter1 != __projection1)
                                                            return;
                                                    }


                                                // um . what level are we at?
                                                var pp = asLambdaExpression.Parameters;
                                                // https://sites.google.com/a/jsc-solutions.net/backlog/knowledge-base/2014/201405/20140513


                                                if (asMemberInitExpressionByParameter1 != null)
                                                    if (asMemberInitExpressionByParameter0 != null)
                                                    {
                                                        AddToSelectCommand(
                                                        asFFieldExpression.Member.Name + "." + asFieldExpression.Member.Name + " as "
                                                        + asFFieldExpression.Member.Name + "_" + asFieldExpression.Member.Name
                                                        );

                                                        return;
                                                    }

                                                if (asMemberInitExpressionByParameter0 != null)
                                                {
                                                    //           __h__TransparentIdentifier0.UpdatesByMiddlesheet_UpdatedContent as UpdatedContent_UpdatedContent,

                                                    AddToSelectCommand(
                                                        asLambdaExpression.Parameters[0].Name.Replace("<>", "__") + "." +
                                                       asFFieldExpression.Member.Name + "_" + asFieldExpression.Member.Name + " as "
                                                       + asFFieldExpression.Member.Name + "_" + asFieldExpression.Member.Name
                                                       );
                                                    return;
                                                }



                                                AddToSelectCommand(
                                                      asLambdaExpression.Parameters[0].Name.Replace("<>", "__") + "." +
                                                     asFFieldExpression.Member.Name + "_" + asFieldExpression.Member.Name + " as "
                                                     + asFieldExpression.Member.Name
                                                     );
                                                return;
                                            }
                                            #endregion


                                        }

                                        // reduce? flatten?  nested join?
                                        //asFieldExpression = asFieldExpression_Expression_asFieldExpression;
                                        var __projection0 = asFFieldExpression.Expression as ParameterExpression;
                                        if (__projection0 != null)
                                        {
                                            if (asMemberInitExpressionByParameter0 != null)
                                            {
                                                if (asMemberInitExpressionByParameter0 != __projection0)
                                                    return;

                                                //__h__TransparentIdentifier0.UpdatesByMiddlesheet_UpdatedContent as Content,
                                                //
                                                //   UpdatesByMiddlesheet.`UpdatedContent` as  `UpdatesByMiddlesheet_UpdatedContent`,

                                                // is it available for us?
                                                if (asLambdaExpression.Parameters[0].Name != asFFieldExpression.Member.Name)
                                                    if (asLambdaExpression.Parameters[1].Name != asFFieldExpression.Member.Name)
                                                        return;

                                                AddToSelectCommand(
                                                    asFFieldExpression.Member.Name + "." + asFieldExpression.Member.Name + " as "
                                                     + asFFieldExpression.Member.Name + "_" + asFieldExpression.Member.Name);

                                                return;

                                            }

                                            AddToSelectCommand(
                                                __projection0.Name.Replace("<>", "__") + "." +
                                                asFFieldExpression.Member.Name
                                                + "_" + asFieldExpression.Member.Name + " as " + TargetMemberName);
                                            return;
                                        }
                                    }
                                    #endregion



                                    #region asFConstantExpression
                                    {
                                        var asFConstantExpression = asFieldExpression.Expression as ConstantExpression;
                                        if (asFConstantExpression != null)
                                        {
                                            // X:\jsc.svn\examples\javascript\forms\SQLiteWithDataGridViewX\SQLiteWithDataGridViewX\ApplicationWebService.cs

                                            var asMPropertyInfo = asFieldExpression.Member as FieldInfo;
                                            var value1 = asMPropertyInfo.GetValue(asFConstantExpression.Value);

                                            if (value1 is string)
                                            {
                                                // NULL?
                                                AddToSelectCommand("'" + value1 + "' as `" + asMemberAssignment.Member.Name + "`");
                                            }
                                            else
                                            {
                                                // long?
                                                AddToSelectCommand("  " + value1 + " as `" + asMemberAssignment.Member.Name + "`");
                                            }

                                            return;
                                        }
                                    }
                                    #endregion


                                    #region asTypedParameterExpression
                                    // http://dotnetinside.com/cn/type/System.Core/TypedParameterExpression/4.0.0.0
                                    //var asTypedParameterExpression = asFieldExpression.Expression as TypedParameterExpression
                                    var asTypedParameterExpression = asFieldExpression.Expression as ParameterExpression;
                                    if (asTypedParameterExpression != null)
                                    {
                                        // is it available for us?
                                        if (asLambdaExpression.Parameters[0] != asTypedParameterExpression)
                                            if (asLambdaExpression.Parameters[1] != asTypedParameterExpression)
                                                return;


                                        var SourceContextName = asTypedParameterExpression.Name;
                                        var SourceMemberName = asFieldExpression.Member.Name;




                                        // magic happens here!
                                        AddToSelectCommand(SourceContextName + "." + SourceMemberName + " as " + TargetMemberName);
                                        return;
                                    }
                                    #endregion


                                }
                                #endregion

                                Debugger.Break();
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


    }
}

