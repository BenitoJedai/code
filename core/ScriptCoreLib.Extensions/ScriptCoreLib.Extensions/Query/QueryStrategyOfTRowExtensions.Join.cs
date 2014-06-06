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


        public interface INestedQueryStrategy
        {

            ISelectQueryStrategy upperSelect { get; set; }
            IJoinQueryStrategy upperJoin { get; set; }
            IGroupByQueryStrategy upperGroupBy { get; set; }
        }

        [ScriptCoreLib.ScriptAttribute.ExplicitInterface]
        public interface IJoinQueryStrategy : INestedQueryStrategy
        {
            // allow to inspect upper select . what if there are multiple upper selects?
            Expression selectorExpression { get; }

            IQueryStrategy xouter { get; }
            IQueryStrategy xinner { get; }
            Expression outerKeySelector { get; }

            // ? gDescendingByKeyReferenced

        }

        class JoinQueryStrategy<TOuter, TInner, TKey, TResult> : XQueryStrategy<TResult>, IJoinQueryStrategy
        {
            public IQueryStrategy<TOuter> xouter { get; set; }
            public IQueryStrategy<TInner> xinner { get; set; }
            public Expression<Func<TOuter, TKey>> outerKeySelector { get; set; }
            public Expression<Func<TInner, TKey>> innerKeySelector { get; set; }
            public Expression<Func<TOuter, TInner, TResult>> resultSelector;

            public ISelectQueryStrategy upperSelect { get; set; }
            public IJoinQueryStrategy upperJoin { get; set; }
            public IGroupByQueryStrategy upperGroupBy { get; set; }

            #region IJoinQueryStrategy
            Expression IJoinQueryStrategy.selectorExpression
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



            [Obsolete("javac wont like generic nested delegates")]
            public void Invoke(QueryStrategyExtensions.CommandBuilderState state)
            {
                var that = this;


                Console.WriteLine("Join CommandBuilder " + new { that });
                // Caused by: java.lang.RuntimeException: System.Diagnostics.Debugger.Break


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

                Console.WriteLine("Join CommandBuilder  ...  " + new { asLambdaExpression });

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

                Console.WriteLine("Join CommandBuilder building FromCommand...");

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
                        + " as " + xouter_Paramerer_Name.Replace("<>", "__")
                        + "\n inner join "
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

                Console.WriteLine("Join CommandBuilder building SelectCommand...");


                // ?? not used in here
                var asMemberInitExpressionByParameter0 = default(ParameterExpression);
                var asMemberInitExpressionByParameter1 = default(ParameterExpression);
                var asMemberInitExpressionByParameter2 = default(ParameterExpression);


                #region SelectCommand
                var SelectCommand = default(string);
                var s_SelectCommand = "select " + CommentLineNumber() + " -- ";

                // is it used?
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

                #region WriteMemberExpression COPY from GroupBy
                Action<int, MemberExpression, MemberInfo> WriteMemberExpression =
                    (index, asMemberExpression, asMemberAssignment_Member) =>
                         {
                             var asMemberAssignment = new { Member = asMemberAssignment_Member };

                             // +		Member	{TestSQLiteGroupBy.Data.GooStateEnum Key}	System.Reflection.MemberInfo {System.Reflection.RuntimePropertyInfo}

                             // X:\jsc.svn\core\ScriptCoreLib\Shared\BCLImplementation\System\Linq\Expressions\Expression.cs


                             //{ index = 7, asMemberAssignment = MemberAssignment { Expression = MemberExpression { expression = MethodCallExpression { Object = , Method = java.lang.Object Last(TestSQLiteGroupBy.IQueryStrategyGrouping_2) }, field = double x }
                             //{ index = 7, asMemberExpression = MemberExpression { expression = MethodCallExpression { Object = , Method = java.lang.Object Last(TestSQLiteGroupBy.IQueryStrategyGrouping_2) }, field = double x } }
                             //{ index = 7, Member = double x, Name = x }
                             //{ index = 7, asMemberExpressionMethodCallExpression = MethodCallExpression { Object = , Method = java.lang.Object Last(TestSQLiteGroupBy.IQueryStrategyGrouping_2) } }


                             Console.WriteLine(new { index, asMemberExpression.Member, asMemberExpression.Member.Name });

                             ////#region let z <- Grouping.Key
                             ////var IsKey = asMemberExpression.Member.Name == "Key";

                             ////// if not a property we may still have the getter in JVM
                             ////IsKey |= asMemberExpression.Member.Name == "get_Key";

                             ////if (IsKey)
                             ////{
                             ////    // special!
                             ////    state.SelectCommand += ",\n\t g.`" + asMemberAssignment.Member.Name + "` as `" + asMemberAssignment.Member.Name + "`";

                             ////    s_SelectCommand += ",\n\t s.`"
                             ////       + GroupingKeyFieldExpressionName + "` as `" + asMemberAssignment.Member.Name + "`";
                             ////    return;
                             ////}
                             ////#endregion

                             // Method = {TestSQLiteGroupBy.Data.Book1MiddleRow First[GooStateEnum,Book1MiddleRow](TestSQLiteGroupBy.IQueryStrategyGrouping`2[TestSQLiteGroupBy.Data.GooStateEnum,TestSQLiteGroupBy.Data.Book1MiddleRow])}

                             #region WriteMemberExpression:asMemberExpressionMethodCallExpression
                             var asMemberExpressionMethodCallExpression = asMemberExpression.Expression as MethodCallExpression;
                             Console.WriteLine(new { index, asMemberExpressionMethodCallExpression });
                             if (asMemberExpressionMethodCallExpression != null)
                             {
                                 if (asMemberInitExpressionByParameter1 != null)
                                 {

                                     // ?
                                 }
                                 else if (asMemberInitExpressionByParameter0 != null)
                                 {
                                     if (asMemberInitExpressionByParameter0 != asMemberExpressionMethodCallExpression.Arguments[0])
                                     {
                                         // group by within a join, where this select is not part of this outer source!

                                         return;
                                     }
                                 }
                                 Console.WriteLine(new { index, asMemberExpressionMethodCallExpression, asMemberExpressionMethodCallExpression.Method.Name });


                             }
                             #endregion


                             //                                                 -		asMemberAssignment.Expression	{value(SQLiteWithDataGridViewX.ApplicationWebService+<>c__DisplayClass1b).SpecialConstant.u}	System.Linq.Expressions.Expression {System.Linq.Expressions.PropertyExpression}


                             //                                 +		(new System.Linq.Expressions.Expression.MemberExpressionProxy(asMemberAssignment.Expression as System.Linq.Expressions.PropertyExpression)).Member	{System.String u}	System.Reflection.MemberInfo {System.Reflection.RuntimePropertyInfo}
                             //+		(new System.Linq.Expressions.Expression.ConstantExpressionProxy((new System.Linq.Expressions.Expression.MemberExpressionProxy((new System.Linq.Expressions.Expression.MemberExpressionProxy(asMemberAssignment.Expression as System.Linq.Expressions.PropertyExpression)).Expression as System.Linq.Expressions.FieldExpression)).Expression as System.Linq.Expressions.ConstantExpression)).Value	{SQLiteWithDataGridViewX.ApplicationWebService.}	object {SQLiteWithDataGridViewX.ApplicationWebService.}

                             //                                 -		Value	{SQLiteWithDataGridViewX.ApplicationWebService.}	object {SQLiteWithDataGridViewX.ApplicationWebService.}
                             //-		SpecialConstant	{ u = "44" }	<Anonymous Type>
                             //        u	"44"	string




                             #region WriteMemberExpression:asMConstantExpression
                             //         var SpecialConstant_u = "44";
                             var asMConstantExpression = asMemberExpression.Expression as ConstantExpression;
                             if (asMConstantExpression != null)
                             {
                                 var asMPropertyInfo = asMemberExpression.Member as FieldInfo;
                                 var rAddParameterValue0 = asMPropertyInfo.GetValue(asMConstantExpression.Value);

                                 // X:\jsc.svn\examples\javascript\forms\Test\TestSQLGroupByAfterJoin\TestSQLGroupByAfterJoin\ApplicationWebService.cs

                                 var n = "@arg" + state.ApplyParameter.Count;

                                 state.SelectCommand += ",\n\t g.`" + asMemberAssignment.Member.Name + "` as `" + asMemberAssignment.Member.Name + "`";
                                 s_SelectCommand += ",\n\t " + n + " as `" + asMemberAssignment.Member.Name + "`";

                                 state.ApplyParameter.Add(
                                     c =>
                                     {
                                         // either the actualt command or the explain command?

                                         //c.Parameters.AddWithValue(n, r);
                                         c.AddParameter(n, rAddParameterValue0);
                                     }
                                 );

                                 return;

                                 //if (rAddParameterValue0 is string)
                                 //{
                                 //    // the outer select might be optimized away!
                                 //    state.SelectCommand += ",\n\t g.`" + asMemberAssignment.Member.Name + "` as `" + asMemberAssignment.Member.Name + "`";
                                 //    s_SelectCommand += ",\n\t '" + rAddParameterValue0 + "' as `" + asMemberAssignment.Member.Name + "`";
                                 //}
                                 //else
                                 //{
                                 //    // long?
                                 //    state.SelectCommand += ",\n\t g.`" + asMemberAssignment.Member.Name + "` as `" + asMemberAssignment.Member.Name + "`";
                                 //    s_SelectCommand += ",\n\t " + rAddParameterValue0 + " as `" + asMemberAssignment.Member.Name + "`";
                                 //}

                                 //return;
                             }
                             #endregion



                             var xasMParameterExpression = asMemberExpression.Expression as ParameterExpression;
                             if (xasMParameterExpression != null)
                             {

                             }


                             #region WriteMemberExpression:asMMemberExpression
                             var asMMemberExpression = asMemberExpression.Expression as MemberExpression;
                             if (asMMemberExpression != null)
                             {
                                 // Member = {<>f__AnonymousType0`1[System.String] SpecialConstant}
                                 // X:\jsc.svn\examples\javascript\forms\SQLiteWithDataGridViewX\SQLiteWithDataGridViewX\ApplicationWebService.cs
                                 // var SpecialConstant = new { u = "44" };


                                 if (asMemberInitExpressionByParameter1 != null)
                                 {
                                     // ???
                                     // +		(new System.Linq.Expressions.Expression.MemberExpressionProxy(asMemberExpression as System.Linq.Expressions.FieldExpression)).Expression	
                                     // {<>h__TransparentIdentifier0.MiddleSheetz}	System.Linq.Expressions.Expression {System.Linq.Expressions.PropertyExpression}

                                     // https://sites.google.com/a/jsc-solutions.net/backlog/knowledge-base/2014/201405/20140513
                                     //Debugger.Break();
                                     return;

                                     //var asFieldInfo = asMemberExpression.Member as FieldInfo;
                                     //if (asFieldInfo != null)
                                     //{
                                     //    //asMemberExpressionMethodCallExpression = {<>h__TransparentIdentifier0.UpdatesByMiddlesheet.Last()}

                                     //    state.SelectCommand += ",\n\t g.`" + asFieldInfo.Name + "` as `" + asFieldInfo.Name + "`";
                                     //    s_SelectCommand += ",\n\t s.`" + asFieldInfo.Name + "` as `" + asFieldInfo.Name + "`";
                                     //    return;
                                     //}
                                 }

                                 var asMMFieldInfo = asMMemberExpression.Member as FieldInfo;

                                 #region asPropertyInfo
                                 var asPropertyInfo = asMemberExpression.Member as PropertyInfo;
                                 if (asPropertyInfo != null)
                                 {


                                     if (asPropertyInfo.Name == "Length")
                                     {
                                         // http://www.sqlite.org/lang_corefunc.html

                                         state.SelectCommand += ",\n\t g.`" + asMemberAssignment.Member.Name + "` as `" + asMemberAssignment.Member.Name + "`";
                                         //s_SelectCommand += ",\n\t len(s.`" + asMMFieldInfo.Name + "`) as `" + asMemberAssignment.Member.Name + "`";
                                         s_SelectCommand += ",\n\t length(s.`" + asMMFieldInfo.Name + "`) as `" + asMemberAssignment.Member.Name + "`";

                                         return;
                                     }


                                     // CLR

                                     var asC = asMMemberExpression.Expression as ConstantExpression;

                                     // Member = {<>f__AnonymousType0`1[System.String] SpecialConstant}

                                     var value0 = asMMFieldInfo.GetValue(asC.Value);
                                     var rAddParameterValue0 = asPropertyInfo.GetValue(value0, null);



                                     var n = "@arg" + state.ApplyParameter.Count;
                                     state.SelectCommand += ",\n\t g.`" + asMemberAssignment.Member.Name + "` as `" + asMemberAssignment.Member.Name + "`";
                                     s_SelectCommand += ",\n\t " + n + " as `" + asMemberAssignment.Member.Name + "`";

                                     state.ApplyParameter.Add(
                                         c =>
                                         {
                                             // either the actualt command or the explain command?

                                             //c.Parameters.AddWithValue(n, r);
                                             c.AddParameter(n, rAddParameterValue0);
                                         }
                                     );

                                     //if (rAddParameterValue0 is string)
                                     //{
                                     //    // NULL?
                                     //    state.SelectCommand += ",\n\t '" + rAddParameterValue0 + "' as `" + asMemberAssignment.Member.Name + "`";
                                     //}
                                     //else
                                     //{
                                     //    // long?
                                     //    state.SelectCommand += ",\n\t " + rAddParameterValue0 + " as `" + asMemberAssignment.Member.Name + "`";
                                     //}

                                     return;
                                 }
                                 #endregion



                                 // https://sites.google.com/a/jsc-solutions.net/backlog/knowledge-base/2014/201405/20140515
                                 // X:\jsc.svn\examples\javascript\forms\Test\TestSQLGroupByAfterJoin\TestSQLGroupByAfterJoin\ApplicationWebService.cs
                                 var asMMMemberInfo = asMMemberExpression.Member as MemberInfo;
                                 if (asMMMemberInfo != null)
                                 {
                                     // asMMemberExpression = {result.Last().l}
                                     // asMemberExpression = {result.Last().l.FirstName}

                                     var asMMMCall = asMMemberExpression.Expression as MethodCallExpression;
                                     if (asMMMCall != null)
                                     {
                                         //asMMMCall = {result.Last()}


                                         if (asMMMCall.Method.Name.TakeUntilIfAny("_") == "Last")
                                         {
                                             state.SelectCommand += ",\n\t g.`" + asMemberAssignment.Member.Name + "` as `" + asMemberAssignment.Member.Name + "`";
                                             s_SelectCommand += ",\n\t s.`" + asMMemberExpression.Member.Name + "_" + asMemberAssignment.Member.Name + "` as `" + asMemberAssignment.Member.Name + "`";
                                             return;
                                         }
                                     }

                                     // asMMemberExpression = {<>h__TransparentIdentifier3.<>h__TransparentIdentifier2.<>h__TransparentIdentifier1.<>h__TransparentIdentifier0.k}

                                     // X:\jsc.svn\examples\javascript\linq\test\TestJoinSelectAnonymousType\TestJoinSelectAnonymousType\ApplicationWebService.cs
                                     s_SelectCommand += ",\n\t s.`" + asMMemberExpression.Member.Name + "_" + asMemberAssignment.Member.Name + "` as `" + asMemberAssignment.Member.Name + "`";
                                     return;
                                 }

                             }
                             #endregion

                             #region WriteMemberExpression:asMMemberExpressionParameterExpression
                             var asMMemberExpressionParameterExpression = asMemberExpression.Expression as ParameterExpression;
                             if (asMMemberExpressionParameterExpression != null)
                             {
                                 if (asMemberInitExpressionByParameter0 != null)
                                 {
                                     if (asMemberInitExpressionByParameter0 != asMMemberExpressionParameterExpression)
                                     {
                                         // group by within a join, where this select is not part of this outer source!

                                         return;
                                     }
                                 }


                                 // X:\jsc.svn\examples\javascript\linq\test\TestJoinSelectAnonymousType\TestJoinSelectAnonymousType\ApplicationWebService.cs
                                 var asMParameterExpression = asMemberExpression.Expression as ParameterExpression;
                                 s_SelectCommand += ",\n" + CommentLineNumber() + "\t"
                                    + asMParameterExpression.Name + ".`" + asMemberExpression.Member.Name + "` as `" + asMemberAssignment.Member.Name + "`";


                                 return;
                             }
                             #endregion


                             //asMMemberExpression.Member
                             Debugger.Break();
                         };
                #endregion


                #region WriteExpression COPY from GroupBy
                Action<int, Expression, MemberInfo> WriteExpression =
                    (index, asExpression, TargetMember) =>
                         {
                             var asMemberAssignment = new { Expression = asExpression, Member = TargetMember };

                             // what about nesed joins?


                             #region WriteExpression:asEParameterExpression
                             var asEParameterExpression = asExpression as ParameterExpression;
                             if (asEParameterExpression != null)
                             {
                                 // does it mean we want all fields?
                                 // or were we supposed to figure out what needs to be selected?


                                 // resultSelectorExpression = {(<>h__TransparentIdentifier0, uu) => new <>f__AnonymousType1`4(
                                 // kkey = <>h__TransparentIdentifier0.k.Key, 
                                 // kpath = <>h__TransparentIdentifier0.k.path, 
                                 // upath = <>h__TransparentIdentifier0.u.requestStart, 
                                 // uupath = uu.requestStart)}

                                 // X:\jsc.svn\examples\javascript\linq\test\TestJoinSelectAnonymousType\TestJoinSelectAnonymousType\ApplicationWebService.cs



                                 // level2
                                 //Debugger.Break();

                                 //s.`u0_kpath` as `kpath`,
                                 //s.`u1_upath` as `upath`,



                                 var cc = new
                                 {
                                     xouter,
                                     xouter_Paramerer_Name,
                                     xouter_Paramerer
                                 };


                                 // for transparent identifiers we need to go in and then proxy the values.
                                 //  xasMemberExpression.Member.Name 

                                 // https://sites.google.com/a/jsc-solutions.net/backlog/knowledge-base/2014/201405/20140531
                                 // are we supposed to unpack our fields?
                                 if (xouter_Paramerer == asExpression)
                                 {

                                     #region projectionWalker
                                     Action<IJoinQueryStrategy> projectionWalker = null;

                                     projectionWalker =
                                         yy =>
                                         {
                                             if (yy == null)
                                                 return;

                                             // show the inherited fields
                                             //s_SelectCommand += ",\n\t-- 0   " + (yy.selectorExpression as LambdaExpression).Parameters[0].Name;
                                             #region  // go up
                                             {

                                                 INestedQueryStrategy uu = that;

                                                 while (uu != null)
                                                 {


                                                     #region asSelectQueryStrategy
                                                     var asSelectQueryStrategy = uu as ISelectQueryStrategy;
                                                     if (asSelectQueryStrategy != null)
                                                     {
                                                         var xasLambdaExpression = asSelectQueryStrategy.selectorExpression as LambdaExpression;


                                                         // +		xasLambdaExpression.Body	{new PerformanceResourceTimingData2ApplicationResourcePerformanceRow() {path = nt.Last().u.path, duration = Convert(nt.Last().u2.Key)}}	System.Linq.Expressions.Expression {System.Linq.Expressions.MemberInitExpression}




                                                         #region itemWriteMemberExpression
                                                         Action<Expression> itemWriteMemberExpression =
                                                             xasExpression =>
                                                                {
                                                                    var xasUnaryExpression = xasExpression as UnaryExpression;
                                                                    if (xasUnaryExpression != null)
                                                                    {
                                                                        // X:\jsc.svn\examples\javascript\LINQ\test\TestDoubleJoinGroupBy\TestDoubleJoinGroupBy\ApplicationWebService.cs
                                                                        // allow the cast
                                                                        xasExpression = xasUnaryExpression.Operand;
                                                                    }


                                                                    var xasMemberExpression = xasExpression as MemberExpression;

                                                                    // xasMemberAssignment.Expression = {Convert(nt.Last().u2.Key)}

                                                                    // is this select doing a group by?
                                                                    // we need to first find the select and then its source as group by
                                                                    // as otherwise just looking at groupby would not tell us which fields are of interest


                                                                    #region IGroupByQueryStrategy
                                                                    var xasGroupByQueryStrategy = asSelectQueryStrategy.source as IGroupByQueryStrategy;
                                                                    if (xasGroupByQueryStrategy != null)
                                                                    {
                                                                        // first or last?
                                                                        // or aggregate?

                                                                        var xasMethodCallExpression = xasMemberExpression.Expression as MethodCallExpression;
                                                                        if (xasMethodCallExpression != null)
                                                                        {
                                                                            Debugger.Break();

                                                                            // could we actually do a ref?

                                                                            // public static TElement Last<TKey, TElement>(this IQueryStrategyGrouping<TKey, TElement> source)
                                                                            // will it work for jvm? will they equal?

                                                                            // generic info mismatch?
                                                                            if (xasMethodCallExpression.Method.Name == refLast.Name)
                                                                            {
                                                                                s_SelectCommand += ",\n\t " + asMemberAssignment.Member.Name + "." + xasMemberExpression.Member.Name + " as `" + asMemberAssignment.Member.Name + "_" + xasMemberExpression.Member.Name + "`";

                                                                            }
                                                                        }
                                                                    }
                                                                    #endregion



                                                                    #region  MemberExpression
                                                                    var xasMMemberExpression = xasMemberExpression.Expression as MemberExpression;
                                                                    if (xasMMemberExpression != null)
                                                                    {
                                                                        Console.WriteLine(
                                                                            new
                                                                        {
                                                                            xasMMemberExpression.Member.Name,
                                                                            asEParameterExpression = asEParameterExpression.Name
                                                                        }
                                                                        );

                                                                        // why would we compare these?
                                                                        //if (xasMMemberExpression.Member.Name == asEParameterExpression.Name)
                                                                        {
                                                                            // (yy.selectorExpression as LambdaExpression).Parameters[0].Name = "u0"
                                                                            if (xasMMemberExpression.Member.Name == (yy.selectorExpression as LambdaExpression).Parameters[0].Name)
                                                                            {
                                                                                s_SelectCommand += ",\n" + CommentLineNumber() + "\t "
                                                                                + asMemberAssignment.Member.Name.Replace("<>", "__")
                                                                                + "."
                                                                                + xasMMemberExpression.Member.Name + "_" + xasMemberExpression.Member.Name
                                                                                + " as `"
                                                                                + xasMMemberExpression.Member.Name
                                                                                + "_" + xasMemberExpression.Member.Name + "`";

                                                                            }

                                                                            // ?
                                                                            if (xasMMemberExpression.Member.Name == (yy.selectorExpression as LambdaExpression).Parameters[1].Name)
                                                                            {
                                                                                s_SelectCommand += ",\n" + CommentLineNumber() + "\t "
                                                                                + asMemberAssignment.Member.Name.Replace("<>", "__")
                                                                                + "."
                                                                                + xasMMemberExpression.Member.Name + "_" + xasMemberExpression.Member.Name
                                                                                + " as `"
                                                                                + xasMMemberExpression.Member.Name
                                                                                + "_" + xasMemberExpression.Member.Name + "`";

                                                                            }
                                                                        }
                                                                    }
                                                                    #endregion

                                                         };
                                                         #endregion




                                                         #region MemberInitExpression
                                                         var xMemberInitExpression = xasLambdaExpression.Body as MemberInitExpression;
                                                         if (xMemberInitExpression != null)
                                                         {
                                                             var asEMNewExpression = xMemberInitExpression.NewExpression;


                                                             xMemberInitExpression.Bindings.WithEachIndex(
                                                                  (SourceBinding, i) =>
                                                                  {
                                                                      var xasMemberAssignment = SourceBinding as MemberAssignment;



                                                                      itemWriteMemberExpression(xasMemberAssignment.Expression);
                                                                  }
                                                             );

                                                         }
                                                         #endregion



                                                         #region xasNewExpression
                                                         var xasNewExpression = xasLambdaExpression.Body as NewExpression;
                                                         // xasNewExpression = {new <>f__AnonymousType5`3(Key = g.Key, last_u0_path = g.Last().u0.path, last_u1_path = g.Last().u1.path)}
                                                         if (xasNewExpression != null)
                                                         {
                                                             foreach (var item in xasNewExpression.Arguments)
                                                             {
                                                                 // Expression = {<> h__TransparentIdentifier5.<> h__TransparentIdentifier4.<> h__TransparentIdentifier3.<> h__TransparentIdentifier2.<> h__TransparentIdentifier1.<> h__TransparentIdentifier0
                                                                 // .u0}





                                                                 #region MethodCallExpression
                                                                 var yasMethodCallExpression = item as MethodCallExpression;
                                                                 if (yasMethodCallExpression != null)
                                                                 {
                                                                     if (yasMethodCallExpression.Method.Name == "ToLower")
                                                                     {
                                                                         var yitem = yasMethodCallExpression.Object as MemberExpression;

                                                                         itemWriteMemberExpression(yitem);

                                                                     }
                                                                 }
                                                                 #endregion

                                                                 #region MemberExpression
                                                                 {
                                                                     var xasMemberExpression = item as MemberExpression;
                                                                     if (xasMemberExpression != null)
                                                                     {
                                                                         itemWriteMemberExpression(xasMemberExpression);


                                                                     }
                                                                 }
                                                                 #endregion

                                                             }
                                                         }
                                                         #endregion
                                                     }
                                                     #endregion
                                                     if (uu.upperSelect != null)
                                                         uu = uu.upperSelect;
                                                     else if (uu.upperJoin != null)
                                                         uu = uu.upperJoin;
                                                     else if (uu.upperGroupBy != null)
                                                         uu = uu.upperGroupBy;
                                                     else
                                                         break;
                                                 }
                                             }
                                             #endregion


                                             //s_SelectCommand += ",\n\t--  1  " + (yy.selectorExpression as LambdaExpression).Parameters[1].Name;
                                             #region  // go up 2
                                             {

                                                 INestedQueryStrategy uu = that;

                                                 while (uu != null)
                                                 {
                                                     #region asSelectQueryStrategy
                                                     var asSelectQueryStrategy = uu as ISelectQueryStrategy;
                                                     if (asSelectQueryStrategy != null)
                                                     {
                                                         var xasLambdaExpression = asSelectQueryStrategy.selectorExpression as LambdaExpression;

                                                         var xasNewExpression = xasLambdaExpression.Body as NewExpression;
                                                         if (xasNewExpression != null)
                                                         {
                                                             foreach (var item in xasNewExpression.Arguments)
                                                             {
                                                                 // Expression = {<> h__TransparentIdentifier5.<> h__TransparentIdentifier4.<> h__TransparentIdentifier3.<> h__TransparentIdentifier2.<> h__TransparentIdentifier1.<> h__TransparentIdentifier0
                                                                 // .u0}

                                                                 #region xasMemberExpression
                                                                 var xasMemberExpression = item as MemberExpression;
                                                                 if (xasMemberExpression != null)
                                                                 {
                                                                     var xasMMemberExpression = xasMemberExpression.Expression as MemberExpression;
                                                                     if (xasMMemberExpression != null)
                                                                     {
                                                                         if (xasMMemberExpression.Member.Name == (yy.selectorExpression as LambdaExpression).Parameters[1].Name)
                                                                         {
                                                                             s_SelectCommand += ",\n\t " + asMemberAssignment.Member.Name.Replace("<>", "__") + "." + xasMMemberExpression.Member.Name + "_" + xasMemberExpression.Member.Name + " as `" + xasMMemberExpression.Member.Name + "_" + xasMemberExpression.Member.Name + "`";

                                                                         }
                                                                     }
                                                                 }
                                                                 #endregion
                                                             }
                                                         }
                                                         else
                                                         {
                                                             #region asLMemberInitExpression
                                                             var asLMemberInitExpression = xasLambdaExpression.Body as MemberInitExpression;
                                                             if (asLMemberInitExpression != null)
                                                             {
                                                                 /// X:\jsc.svn\examples\javascript\LINQ\test\TestDoubleJoinGroupBy\TestDoubleJoinGroupBy\ApplicationWebService.cs
                                                                 /// 
                                                                 asLMemberInitExpression.Bindings.WithEachIndex(
                                                                    (SourceBinding, i) =>
                                                                     {
                                                                         var xasMemberAssignment = SourceBinding as MemberAssignment;


                                                                         #region xasMemberExpression
                                                                         var xasMemberExpression = xasMemberAssignment.Expression as MemberExpression;
                                                                         if (xasMemberExpression != null)
                                                                         {
                                                                             var xasMMemberExpression = xasMemberExpression.Expression as MemberExpression;
                                                                             if (xasMMemberExpression != null)
                                                                             {
                                                                                 if (xasMMemberExpression.Member.Name == (yy.selectorExpression as LambdaExpression).Parameters[1].Name)
                                                                                 {
                                                                                     s_SelectCommand += ",\n\t " + asMemberAssignment.Member.Name.Replace("<>", "__") + "." + xasMMemberExpression.Member.Name + "_" + xasMemberExpression.Member.Name + " as `" + xasMMemberExpression.Member.Name + "_" + xasMemberExpression.Member.Name + "`";

                                                                                 }
                                                                             }
                                                                         }
                                                                         #endregion
                                                                 }
                                                                  );
                                                             }
                                                             else Debugger.Break();
                                                             #endregion
                                                         }

                                                     }
                                                     #endregion


                                                     if (uu.upperSelect != null)
                                                         uu = uu.upperSelect;
                                                     else if (uu.upperJoin != null)
                                                         uu = uu.upperJoin;
                                                     else if (uu.upperGroupBy != null)
                                                         uu = uu.upperGroupBy;
                                                     else
                                                         break;
                                                 }
                                             }
                                             #endregion


                                             projectionWalker(yy.xouter as IJoinQueryStrategy);
                                             projectionWalker(yy.xinner as IJoinQueryStrategy);
                                         };
                                     #endregion

                                     projectionWalker(that.xouter as IJoinQueryStrategy);


                                     // https://sites.google.com/a/jsc-solutions.net/backlog/knowledge-base/2014/201406/20140605
                                     {
                                         var asIGLambda = that.outerKeySelector as LambdaExpression;
                                         if (asIGLambda != null)
                                         {
                                             // um. leftside vs riight side?

                                             var asIGLMemberExpression = asIGLambda.Body as MemberExpression;
                                             if (asIGLMemberExpression != null)
                                             {
                                                 // is it available on this level?
                                                 // or are we supposed to proxy it?

                                                 var asIGLMMemberExpression = asIGLMemberExpression.Expression as MemberExpression;
                                                 if (asIGLMMemberExpression != null)
                                                 {
                                                     if (asIGLMMemberExpression.Member.Name == xouter_Paramerer_Name)
                                                     {
                                                         s_SelectCommand += ",\n" + CommentLineNumber() + "\t "
                                                            + xouter_Paramerer.Name.Replace("<>", "__")
                                                            + "." + asIGLMemberExpression.Member.Name.Replace("<>", "__")
                                                            + " as `" + asMemberAssignment.Member.Name.Replace("<>", "__")
                                                            + "_" + asIGLMemberExpression.Member.Name + "`";

                                                     }
                                                     else
                                                     {
                                                         // not sure. pass it forward?

                                                         s_SelectCommand += ",\n" + CommentLineNumber() + "\t "
                                                            + xouter_Paramerer.Name.Replace("<>", "__")
                                                            + ".`"
                                                             + asIGLMMemberExpression.Member.Name.Replace("<>", "__")
                                                             + "_" + asIGLMemberExpression.Member.Name.Replace("<>", "__")
                                                             + "` as `" + asIGLMMemberExpression.Member.Name.Replace("<>", "__")
                                                             + "_" + asIGLMemberExpression.Member.Name + "`";
                                                     }
                                                 }
                                                 else
                                                 {

                                                     s_SelectCommand += ",\n" + CommentLineNumber() + "\t "
                                                        + xouter_Paramerer.Name.Replace("<>", "__")
                                                        + "." + asIGLMemberExpression.Member.Name.Replace("<>", "__")
                                                        + " as `" + asMemberAssignment.Member.Name.Replace("<>", "__")
                                                        + "_" + asIGLMemberExpression.Member.Name + "`";
                                                 }
                                             }


                                         }
                                     }

                                     // yet what about upper join?
                                     // https://sites.google.com/a/jsc-solutions.net/backlog/knowledge-base/2014/201406/20140605
                                     // what if there is more than one?
                                     if (that.upperJoin != null)
                                     {
                                         var asIGLambda = that.upperJoin.outerKeySelector as LambdaExpression;
                                         if (asIGLambda != null)
                                         {
                                             // um. leftside vs riight side?

                                             var asIGLMemberExpression = asIGLambda.Body as MemberExpression;
                                             if (asIGLMemberExpression != null)
                                             {
                                                 // is it available on this level?
                                                 // or are we supposed to proxy it?
                                                 var asIGLMMemberExpression = asIGLMemberExpression.Expression as MemberExpression;
                                                 if (asIGLMMemberExpression != null)
                                                     if (asIGLMMemberExpression.Member.Name == xouter_Paramerer_Name)
                                                     {
                                                         s_SelectCommand += ",\n" + CommentLineNumber() + "\t "
                                                            + xouter_Paramerer.Name.Replace("<>", "__")
                                                            + "." + asIGLMemberExpression.Member.Name.Replace("<>", "__")
                                                            + " as `" + asMemberAssignment.Member.Name.Replace("<>", "__")
                                                            + "_" + asIGLMemberExpression.Member.Name + "`";

                                                     }
                                             }


                                         }
                                     }
                                 }


                                 // x:\jsc.svn\examples\javascript\linq\test\testwherejointtgroupbyselectlast\testwherejointtgroupbyselectlast\applicationwebservice.cs
                                 #region xinner_Paramerer
                                 if (xinner_Paramerer == asExpression)
                                 {
                                     var asIGLambda = that.innerKeySelector as LambdaExpression;
                                     if (asIGLambda != null)
                                     {
                                         // um. leftside vs riight side?

                                         var asIGLMemberExpression = asIGLambda.Body as MemberExpression;
                                         if (asIGLMemberExpression != null)
                                         {
                                             // is it available on this level?
                                             // or are we supposed to proxy it?

                                             s_SelectCommand += ",\n" + CommentLineNumber() + "\t "
                                             + xinner_Paramerer.Name + "." + asIGLMemberExpression.Member.Name + " as `" + asMemberAssignment.Member.Name + "_" + asIGLMemberExpression.Member.Name + "`";

                                         }


                                     }
                                 }
                                 #endregion




                                 #region  // go up
                                 {

                                     INestedQueryStrategy uu = that;

                                     while (uu != null)
                                     {


                                         #region asSelectQueryStrategy
                                         var asSelectQueryStrategy = uu as ISelectQueryStrategy;
                                         if (asSelectQueryStrategy != null)
                                         {
                                             // https://sites.google.com/a/jsc-solutions.net/backlog/knowledge-base/2014/201406/20140601

                                             var xasLambdaExpression = asSelectQueryStrategy.selectorExpression as LambdaExpression;





                                             #region itemWriteMemberExpression
                                             Action<Expression> itemWriteMemberExpression =
                                                xasExpression =>
                                                {
                                                    var xasUnaryExpression = xasExpression as UnaryExpression;
                                                    if (xasUnaryExpression != null)
                                                    {
                                                        // X:\jsc.svn\examples\javascript\LINQ\test\TestDoubleJoinGroupBy\TestDoubleJoinGroupBy\ApplicationWebService.cs
                                                        // allow the cast
                                                        xasExpression = xasUnaryExpression.Operand;
                                                    }


                                                    var yasMethodCallExpression = xasExpression as MethodCallExpression;
                                                    if (yasMethodCallExpression != null)
                                                    {
                                                        if (yasMethodCallExpression.Method.Name == "ToLower")
                                                        {
                                                            xasExpression = yasMethodCallExpression.Object as MemberExpression;


                                                        }
                                                    }

                                                    var xasMemberExpression = xasExpression as MemberExpression;
                                                    // is this select doing a group by?
                                                    // we need to first find the select and then its source as group by
                                                    // as otherwise just looking at groupby would not tell us which fields are of interest


                                                    #region IGroupByQueryStrategy
                                                    var xasGroupByQueryStrategy = asSelectQueryStrategy.source as IGroupByQueryStrategy;
                                                    if (xasGroupByQueryStrategy != null)
                                                    {
                                                        // first or last?
                                                        // or aggregate?

                                                        var xasMethodCallExpression = xasMemberExpression.Expression as MethodCallExpression;
                                                        if (xasMethodCallExpression != null)
                                                        {
                                                            // could we actually do a ref?

                                                            // public static TElement Last<TKey, TElement>(this IQueryStrategyGrouping<TKey, TElement> source)
                                                            // will it work for jvm? will they equal?

                                                            // generic info mismatch?
                                                            if (xasMethodCallExpression.Method.Name == refLast.Name)
                                                            {
                                                                s_SelectCommand += ",\n\t " + asMemberAssignment.Member.Name + "." + xasMemberExpression.Member.Name + " as `" + asMemberAssignment.Member.Name + "_" + xasMemberExpression.Member.Name + "`";

                                                            }
                                                        }
                                                    }
                                                    #endregion


                                                    #region xasMMemberExpression
                                                    var xasMMemberExpression = xasMemberExpression.Expression as MemberExpression;
                                                    if (xasMMemberExpression != null)
                                                    {
                                                        if (xasMMemberExpression.Member.Name == asEParameterExpression.Name)
                                                        {
                                                            s_SelectCommand += ",\n" + CommentLineNumber() + "\t " + asMemberAssignment.Member.Name + "." + xasMemberExpression.Member.Name + " as `" + asMemberAssignment.Member.Name + "_" + xasMemberExpression.Member.Name + "`";

                                                        }
                                                    }
                                                    #endregion
                                             };
                                             #endregion


                                             // X:\jsc.svn\examples\javascript\linq\test\TestSelectIntoViewRow\TestSelectIntoViewRow\ApplicationWebService.cs
                                             var xasNewExpression = xasLambdaExpression.Body as NewExpression;
                                             if (xasNewExpression != null)
                                             {

                                                 foreach (var item in xasNewExpression.Arguments)
                                                 {
                                                     // item = {g.Key}
                                                     // what if the outer select wants the key?
                                                     // we need to know what is being selected for the key?
                                                     // item = {g.Last().path}

                                                     // Expression = {<> h__TransparentIdentifier5.<> h__TransparentIdentifier4.<> h__TransparentIdentifier3.<> h__TransparentIdentifier2.<> h__TransparentIdentifier1.<> h__TransparentIdentifier0
                                                     // .u0}

                                                     itemWriteMemberExpression(item);
                                                 }
                                             }
                                             else
                                             {
                                                 var xasMemberInitExpression = xasLambdaExpression.Body as MemberInitExpression;
                                                 if (xasMemberInitExpression != null)
                                                 {
                                                     //xasLambdaExpression.Body = { new PerformanceResourceTimingData2ApplicationResourcePerformanceRow() { path = g.Last().path }}

                                                     xasMemberInitExpression.Bindings.WithEachIndex(
                                                         (xitem, i) =>
                                                         {
                                                             var xasMemberAssignment = xitem as MemberAssignment;
                                                             var item = xasMemberAssignment.Expression;

                                                             itemWriteMemberExpression(item);



                                                         }
                                                     );
                                                 }
                                                 else
                                                     Debugger.Break();

                                             }
                                         }
                                         #endregion

                                         if (uu.upperSelect != null)
                                             uu = uu.upperSelect;
                                         else if (uu.upperJoin != null)
                                             uu = uu.upperJoin;
                                         else if (uu.upperGroupBy != null)
                                             uu = uu.upperGroupBy;
                                         else
                                             break;
                                     }
                                 }
                                 #endregion



                                 return;
                             }
                             #endregion



                             #region WriteExpression:asMConstantExpression
                             {
                                 var asMConstantExpression = asMemberAssignment.Expression as ConstantExpression;
                                 if (asMConstantExpression != null)
                                 {
                                     var asMPropertyInfo = asMemberAssignment.Member as FieldInfo;
                                     //var value1 = asMPropertyInfo.GetValue(asMConstantExpression.Value);
                                     var rAddParameterValue0 = asMConstantExpression.Value;

                                     var n = "@arg" + state.ApplyParameter.Count;


                                     state.SelectCommand += ",\n\t g.`" + asMemberAssignment.Member.Name + "` as `" + asMemberAssignment.Member.Name + "`";
                                     s_SelectCommand += ",\n\t " + n + " as `" + asMemberAssignment.Member.Name + "`";

                                     state.ApplyParameter.Add(
                                         c =>
                                         {
                                             // either the actualt command or the explain command?

                                             //c.Parameters.AddWithValue(n, r);
                                             c.AddParameter(n, rAddParameterValue0);
                                         }
                                     );


                                     //if (rAddParameterValue0 is string)
                                     //{
                                     //    // NULL?
                                     //    state.SelectCommand += ",\n\t '" + rAddParameterValue0 + "' as `" + asMemberAssignment.Member.Name + "`";
                                     //}
                                     //else
                                     //{
                                     //    // long?
                                     //    state.SelectCommand += ",\n\t " + rAddParameterValue0 + " as `" + asMemberAssignment.Member.Name + "`";
                                     //}

                                     return;
                                 }
                             }
                             #endregion

                             //                                 -		asMemberAssignment.Expression	{GroupByGoo.Count()}	System.Linq.Expressions.Expression {System.Linq.Expressions.MethodCallExpressionN}
                             //+		Method	{Int64 Count(ScriptCoreLib.Shared.Data.Diagnostics.IQueryStrategy`1[TestSQLiteGroupBy.Data.Book1MiddleRow])}	System.Reflection.MethodInfo {System.Reflection.RuntimeMethodInfo}

                             #region WriteExpression:asMethodCallExpression
                             var asMethodCallExpression = asMemberAssignment.Expression as MethodCallExpression;
                             if (asMethodCallExpression != null)
                             {
                                 Console.WriteLine(new { index, asMethodCallExpression.Method });

                                 #region count(*) special!
                                 if (asMethodCallExpression.Method.Name.TakeUntilIfAny("_") == "Count")
                                 {

                                     state.SelectCommand += ",\n\t g.`" + asMemberAssignment.Member.Name + "` as `" + asMemberAssignment.Member.Name + "`";
                                     //s_SelectCommand += ",\n\t count(*) as `" + asMemberAssignment.Member.Name + "`";
                                     s_SelectCommand += ",\n\t s.`" + asMemberAssignment.Member.Name + "` as `" + asMemberAssignment.Member.Name + "`";

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

                                         state.SelectCommand += ",\n\t g.`" + asMemberAssignment.Member.Name + "` as `" + asMemberAssignment.Member.Name + "`";
                                         //s_SelectCommand += ",\n\t sum(s.`" + asMemberExpression.Member.Name + "`) as `" + asMemberAssignment.Member.Name + "`";
                                         s_SelectCommand += ",\n\t s.`" + asMemberAssignment.Member.Name + "` as `" + asMemberAssignment.Member.Name + "`";
                                         return;
                                     }
                                 }
                                 #endregion


                                 #region  min( special!!
                                 if (asMethodCallExpression.Method.Name.TakeUntilIfAny("_") == "Min")
                                 {
                                     // X:\jsc.svn\examples\javascript\LINQ\MinMaxAverageExperiment\MinMaxAverageExperiment\ApplicationWebService.cs

                                     var arg1 = (asMethodCallExpression.Arguments[1] as UnaryExpression).Operand as LambdaExpression;
                                     if (arg1 != null)
                                     {
                                         var asMemberExpression = arg1.Body as MemberExpression;

                                         state.SelectCommand += ",\n\t g.`" + asMemberAssignment.Member.Name + "` as `" + asMemberAssignment.Member.Name + "`";
                                         //s_SelectCommand += ",\n\t min(s.`" + asMemberExpression.Member.Name + "`) as `" + asMemberAssignment.Member.Name + "`";
                                         s_SelectCommand += ",\n\t s.`" + asMemberAssignment.Member.Name + "` as `" + asMemberAssignment.Member.Name + "`";
                                         return;
                                     }
                                 }
                                 #endregion

                                 #region  max( special!!
                                 if (asMethodCallExpression.Method.Name.TakeUntilIfAny("_") == "Max")
                                 {
                                     // X:\jsc.svn\examples\javascript\LINQ\MinMaxAverageExperiment\MinMaxAverageExperiment\ApplicationWebService.cs

                                     var arg1 = (asMethodCallExpression.Arguments[1] as UnaryExpression).Operand as LambdaExpression;
                                     if (arg1 != null)
                                     {
                                         var asMemberExpression = arg1.Body as MemberExpression;

                                         state.SelectCommand += ",\n\t g.`" + asMemberAssignment.Member.Name + "` as `" + asMemberAssignment.Member.Name + "`";
                                         //s_SelectCommand += ",\n\t max(s.`" + asMemberExpression.Member.Name + "`) as `" + asMemberAssignment.Member.Name + "`";
                                         s_SelectCommand += ",\n\t s.`" + asMemberAssignment.Member.Name + "` as `" + asMemberAssignment.Member.Name + "`";
                                         return;
                                     }
                                 }
                                 #endregion

                                 #region  avg( special!!
                                 if (asMethodCallExpression.Method.Name.TakeUntilIfAny("_") == "Average")
                                 {
                                     // X:\jsc.svn\examples\javascript\LINQ\MinMaxAverageExperiment\MinMaxAverageExperiment\ApplicationWebService.cs

                                     var arg1 = (asMethodCallExpression.Arguments[1] as UnaryExpression).Operand as LambdaExpression;
                                     if (arg1 != null)
                                     {
                                         var asMemberExpression = arg1.Body as MemberExpression;

                                         state.SelectCommand += ",\n\t g.`" + asMemberAssignment.Member.Name + "` as `" + asMemberAssignment.Member.Name + "`";
                                         //s_SelectCommand += ",\n\t avg(s.`" + asMemberExpression.Member.Name + "`) as `" + asMemberAssignment.Member.Name + "`";
                                         s_SelectCommand += ",\n\t s.`" + asMemberAssignment.Member.Name + "` as `" + asMemberAssignment.Member.Name + "`";
                                         return;
                                     }
                                 }
                                 #endregion


                                 #region  lower( special!!
                                 if (asMethodCallExpression.Method.Name.TakeUntilIfAny("_") == "ToLower")
                                 {
                                     // X:\jsc.svn\examples\javascript\LINQ\MinMaxAverageExperiment\MinMaxAverageExperiment\ApplicationWebService.cs

                                     var asMemberExpression = asMethodCallExpression.Object as MemberExpression;
                                     if (asMemberExpression != null)
                                     {

                                         state.SelectCommand += ",\n\t g.`" + asMemberAssignment.Member.Name + "` as `" + asMemberAssignment.Member.Name + "`";
                                         s_SelectCommand += ",\n\t lower(s.`" + asMemberExpression.Member.Name + "`) as `" + asMemberAssignment.Member.Name + "`";
                                         return;
                                     }
                                 }
                                 #endregion

                                 #region  upper( special!!
                                 if (asMethodCallExpression.Method.Name.TakeUntilIfAny("_") == "ToUpper")
                                 {
                                     // X:\jsc.svn\examples\javascript\LINQ\MinMaxAverageExperiment\MinMaxAverageExperiment\ApplicationWebService.cs

                                     var asMemberExpression = asMethodCallExpression.Object as MemberExpression;
                                     if (asMemberExpression != null)
                                     {

                                         state.SelectCommand += ",\n\t g.`" + asMemberAssignment.Member.Name + "` as `" + asMemberAssignment.Member.Name + "`";
                                         s_SelectCommand += ",\n\t upper(s.`" + asMemberExpression.Member.Name + "`) as `" + asMemberAssignment.Member.Name + "`";
                                         return;
                                     }
                                 }
                                 #endregion

                                 #region  ltrim( special!!
                                 if (asMethodCallExpression.Method.Name.TakeUntilIfAny("_") == "TrimStart")
                                 {
                                     // X:\jsc.svn\examples\javascript\LINQ\MinMaxAverageExperiment\MinMaxAverageExperiment\ApplicationWebService.cs

                                     var asMemberExpression = asMethodCallExpression.Object as MemberExpression;
                                     if (asMemberExpression != null)
                                     {

                                         state.SelectCommand += ",\n\t g.`" + asMemberAssignment.Member.Name + "` as `" + asMemberAssignment.Member.Name + "`";
                                         s_SelectCommand += ",\n\t ltrim(s.`" + asMemberExpression.Member.Name + "`) as `" + asMemberAssignment.Member.Name + "`";
                                         return;
                                     }
                                 }
                                 #endregion

                                 #region  rtrim( special!!
                                 if (asMethodCallExpression.Method.Name.TakeUntilIfAny("_") == "TrimEnd")
                                 {
                                     // X:\jsc.svn\examples\javascript\LINQ\MinMaxAverageExperiment\MinMaxAverageExperiment\ApplicationWebService.cs

                                     var asMemberExpression = asMethodCallExpression.Object as MemberExpression;
                                     if (asMemberExpression != null)
                                     {

                                         state.SelectCommand += ",\n\t g.`" + asMemberAssignment.Member.Name + "` as `" + asMemberAssignment.Member.Name + "`";
                                         s_SelectCommand += ",\n\t rtrim(s.`" + asMemberExpression.Member.Name + "`) as `" + asMemberAssignment.Member.Name + "`";
                                         return;
                                     }
                                 }
                                 #endregion

                                 #region  trim( special!!
                                 if (asMethodCallExpression.Method.Name.TakeUntilIfAny("_") == "Trim")
                                 {
                                     // X:\jsc.svn\examples\javascript\LINQ\MinMaxAverageExperiment\MinMaxAverageExperiment\ApplicationWebService.cs

                                     var asMemberExpression = asMethodCallExpression.Object as MemberExpression;
                                     if (asMemberExpression != null)
                                     {

                                         state.SelectCommand += ",\n\t g.`" + asMemberAssignment.Member.Name + "` as `" + asMemberAssignment.Member.Name + "`";
                                         s_SelectCommand += ",\n\t trim(s.`" + asMemberExpression.Member.Name + "`) as `" + asMemberAssignment.Member.Name + "`";
                                         return;
                                     }
                                 }
                                 #endregion


                                 #region FirstOrDefault
                                 // https://www.youtube.com/watch?v=pt8VYOfr8To
                                 if (asMethodCallExpression.Method.Name.TakeUntilIfAny("_") == "FirstOrDefault")
                                 {
                                     // X:\jsc.svn\examples\javascript\LINQ\test\TestSelectOfSelect\TestSelectOfSelect\ApplicationWebService.cs
                                     // can we ask for sql?


                                     var arg0 = asMethodCallExpression.Arguments[0] as MethodCallExpression;
                                     if (arg0 != null)
                                     {

                                         // we dont know yet how to get sql of that thing do we
                                         s_SelectCommand += ",\n\t 0 as `" + asMemberAssignment.Member.Name + "`";
                                         return;
                                     }
                                 }
                                 #endregion

                             }
                             #endregion



                             #region WriteExpression:asMemberExpression
                             {
                                 // m_getterMethod = {TestSQLiteGroupBy.Data.GooStateEnum get_Key()}

                                 var asMemberExpression = asMemberAssignment.Expression as MemberExpression;
                                 Console.WriteLine(new { index, asMemberExpression });
                                 if (asMemberExpression != null)
                                 {
                                     WriteMemberExpression(index, asMemberExpression, TargetMember);
                                     return;
                                 }
                             }
                             #endregion

                             #region  asMemberAssignment.Expression = {Convert(GroupByGoo.First())}
                             var asUnaryExpression = asMemberAssignment.Expression as UnaryExpression;

                             Console.WriteLine(new { index, asUnaryExpression });

                             if (asUnaryExpression != null)
                             {




                             }
                             #endregion



                             Debugger.Break();
                         };
                #endregion




                //Join CommandBuilder  ...  { asLambdaExpression = { Body = MemberInitExpression { NewExpression = NewExpression {
                //Join CommandBuilder building FromCommand...
                //Join CommandBuilder { that = System.Data.QueryStrategyOfTRowExtensions_JoinQueryStrategy_4@5c6936 }
                //Join CommandBuilder  ...  { asLambdaExpression = { Body = NewExpression { Constructor = .ctor(java.lang.Object,
                //Join CommandBuilder building FromCommand...
                //Join CommandBuilder building SelectCommand...
                //Join CommandBuilder  ...  { asMemberInitExpression =  }

                //Join CommandBuilder  ...  { asLambdaExpression = (contact, dealer) => new <>f__AnonymousType0`2(contact = contact, dealer = dealer) }
                //Join CommandBuilder building FromCommand...
                //Join CommandBuilder building SelectCommand...
                //Join CommandBuilder  ...  { asMemberInitExpression = , Body = new <>f__AnonymousType0`2(contact = contact, dealer = dealer) }
                //Join CommandBuilder building SelectCommand... upperJoin
                //Join CommandBuilder building SelectCommand... ImplicitConstantFields { Type = TestSQLJoin.Data.Book1TheViewRow }



                // X:\jsc.svn\examples\javascript\linq\test\TestSelectIntoNewExpression\TestSelectIntoNewExpression\ApplicationWebService.cs
                // X:\jsc.svn\examples\javascript\linq\test\TestJoinSelectAnonymousType\TestJoinSelectAnonymousType\ApplicationWebService.cs
                var asLMemberExpression = asLambdaExpression.Body as MemberExpression;
                var asLNewExpression = asLambdaExpression.Body as NewExpression;
                var asMemberInitExpression = asLambdaExpression.Body as MemberInitExpression;
                // asLambdaExpression.Body = {new <>f__AnonymousType1`3(Key = k.Key, path = k.path, pathToLower = kk.path)}


                //Join CommandBuilder  ...  { asMemberInitExpression =  }
                //Join CommandBuilder building SelectCommand... upperJoin
                //Join CommandBuilder building SelectCommand... ImplicitConstantFields { Type =  }

                Console.WriteLine("Join CommandBuilder  ...  " + new { asMemberInitExpression, asLambdaExpression.Body });




                if (asMemberInitExpression == null)
                {
                    if (asLMemberExpression != null)
                    {
                        WriteMemberExpression(0, asLMemberExpression, asLMemberExpression.Member);
                        SelectCommand = s_SelectCommand;
                    }
                    else if (asLNewExpression != null)
                    {
                        asLNewExpression.Arguments.WithEachIndex(
                                     (SourceArgument, index) =>
                                    {
                                        var TargetMember = asLNewExpression.Members[index];
                                        var asMemberAssignment = new { Member = TargetMember };


                                        WriteExpression(index, SourceArgument, TargetMember);
                                    }
                                 );

                        SelectCommand = s_SelectCommand;
                    }
                    else
                    {
                        #region guess the field?
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
                        #endregion
                    }
                }
                else
                {
                    //Caused by: java.lang.NullPointerException
                    //        at System.Data.QueryStrategyOfTRowExtensions_JoinQueryStrategy_4.Invoke(QueryStrategyOfTRowExtensions_JoinQueryStrategy_4.java:465)
                    //        ... 99 more

                    //Join CommandBuilder  ...  { asMemberInitExpression = , Body = NewExpression { Constructor = .ctor(java.lang.Object, java.lang.Object), Type = __AnonymousTypes__TestSQLJoin_ApplicationWebService
                    //Join CommandBuilder building SelectCommand... upperJoin
                    //Join CommandBuilder building SelectCommand... ImplicitConstantFields { Type =  }

                    Console.WriteLine("Join CommandBuilder building SelectCommand... ImplicitConstantFields " + new { asMemberInitExpression.Type });
                    // X:\jsc.svn\examples\javascript\LINQ\test\TestJoinGroupByIntoViewRow\TestJoinGroupByIntoViewRow\ApplicationWebService.cs

                    if (asMemberInitExpression.Type == null)
                        throw new InvalidOperationException("asMemberInitExpression.Type == null");

                    asMemberInitExpression.Bindings.WithEachIndex(
                         (SourceBinding, i) =>
                            {
                                var asMemberAssignment = SourceBinding as MemberAssignment;

                                Console.WriteLine(new { asMemberAssignment });
                                // SourceBinding = {Content = <>h__TransparentIdentifier1.<>h__TransparentIdentifier0.UpdatesByMiddlesheet.Last().UpdatedContent}

                                WriteExpression(i, asMemberAssignment.Expression, asMemberAssignment.Member
                                    //, new Tuple<int, MemberInfo>[0], null
                                    );

                            }
                     );
                    SelectCommand = s_SelectCommand;



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



                // X:\jsc.svn\examples\javascript\linq\test\TestJoinSelectAnonymousType\TestJoinSelectAnonymousType\ApplicationWebService.cs
                state.ApplyParameter.AddRange(xouter_SelectAll.ApplyParameter);
                state.ApplyParameter.AddRange(xinner_SelectAll.ApplyParameter);


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


        }


        internal class xBinding
        {
            public FieldInfo SourceField;
            public MemberBinding SourceBinding;
        }

        internal static IEnumerable<xBinding> GetBindings(MemberInitExpression asMemberInitExpression, IEnumerable<FieldInfo> ImplicitConstantFields)
        {
            Console.WriteLine("Join CommandBuilder GetBindings... " + new { asMemberInitExpression });


            var ImplicitConstantBindings = ImplicitConstantFields.Select(
                SourceField => new xBinding { SourceField = SourceField, SourceBinding = default(MemberBinding) }
            );


            var ImplicitConstantBindings0 = asMemberInitExpression.Bindings.Select(
                SourceBinding => new xBinding { SourceField = (FieldInfo)SourceBinding.Member, SourceBinding = SourceBinding }
            );

            var ImplicitConstantBindings1 = ImplicitConstantBindings.Concat(ImplicitConstantBindings0);
            return ImplicitConstantBindings1;
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
            //Join { resultSelector = { Body = NewExpression { Constructor = .ctor(), Type =  }, Parameters = ScriptCoreLib.Shared.BCLImplementation.System.Collections.ObjectModel.__ReadOnlyCollection_1@1beead4 } }


            // X:\jsc.svn\examples\javascript\forms\Test\TestSQLJoin\TestSQLJoin\ApplicationWebService.cs

            // how do we get this barely functional?

            // can we manually convince this thing to include the join clause?
            //var that = new __Book1_TheView();
            var that = new JoinQueryStrategy<TOuter, TInner, TKey, TResult>
            {
                xouter = xouter,
                xinner = xinner,

                outerKeySelector = outerKeySelector,
                innerKeySelector = innerKeySelector,

                resultSelector = resultSelector,


                InternalGetDescriptor =
                    () =>
                    {
                        // inherit the connection/context from above
                        var StrategyDescriptor = xouter.GetDescriptor();

                        return StrategyDescriptor;
                    }
            };





            that.GetCommandBuilder().Add(that.Invoke);



            //return (IQueryStrategy<TResult>)that;
            return that;
        }
        #endregion


    }
}

