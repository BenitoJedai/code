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
        // X:\jsc.svn\examples\javascript\LINQ\test\TestOrderBy\TestOrderBy\ApplicationWebService.cs


        #region where
        // behave like StringBuilder where core data is mutable?
        static void MutableWhere(IQueryStrategy that, LambdaExpression filter)
        {


            // to make it immutable, we would need to have Clone method
            // https://sites.google.com/a/jsc-solutions.net/backlog/knowledge-base/2014/201401/20140112/count
            // https://sites.google.com/a/jsc-solutions.net/backlog/knowledge-base/2014/201405/20140515

            // X:\jsc.svn\examples\javascript\Test\TestIQueryable\TestIQueryable\ApplicationWebService.cs
            // X:\jsc.svn\examples\javascript\svg\SVGNavigationTiming\SVGNavigationTiming\ApplicationWebService.cs
            // X:\jsc.svn\examples\javascript\forms\Test\TestSQLiteEnumWhere\TestSQLiteEnumWhere\ApplicationWebService.cs





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
            Console.WriteLine("MutableWhere "
                //    + new
                //{
                //    body.Method,

            //    //NodeType	Equal	System.Linq.Expressions.ExpressionType
                //    body.NodeType,


            //    ColumnName = lColumnName0,
                //    Right = rAddParameterValue0
                //}
            );


            that.GetCommandBuilder().Add(
                state =>
                {
                    //MutableWhere { Method = Boolean op_Equality(System.String, System.String), Left = Goo, Right = Goo0 }


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





                    // x:\jsc.svn\examples\javascript\linq\minmaxaverageexperiment\minmaxaverageexperiment\applicationwebservice.cs

                    //-filter.Body { Not(IsNullOrEmpty(k.path))}
                    //System.Linq.Expressions.Expression { System.Linq.Expressions.UnaryExpression}


                    // +		filter.Body	{k.path.Contains("foo")}	System.Linq.Expressions.Expression {System.Linq.Expressions.InstanceMethodCallExpressionN}

                    // x:\jsc.svn\examples\javascript\linq\minmaxaverageexperiment\minmaxaverageexperiment\applicationwebservice.cs

                    #region [0] IsNullOrEmpty
                    var asUnaryExpression = filter.Body as UnaryExpression;
                    if (asUnaryExpression != null)
                    {
                        // http://stackoverflow.com/questions/8054942/string-isnullorempty-in-linq-to-sql-query
                        // http://connect.microsoft.com/VisualStudio/feedback/details/367077/i-want-to-use-string-isnullorempty-in-linq-to-sql-statements
                        // http://stackoverflow.com/questions/15663207/how-to-use-null-or-empty-string-in-sql
                        var __WhereCommand = state.WhereCommand;
                        state.WriteExpression(ref __WhereCommand, filter.Body, that);
                        state.WhereCommand = __WhereCommand;
                        return;
                    }
                    #endregion

                    #region [1] MethodCallExpression
                    {
                        var asMethodCallExpression = filter.Body as MethodCallExpression;
                        if (asMethodCallExpression != null)
                        {

                            var __WhereCommand = state.WhereCommand;
                            state.WriteExpression(ref __WhereCommand, filter.Body, that);
                            state.WhereCommand = __WhereCommand;
                            return;
                        }
                    }
                    #endregion


                    #region [2] BinaryExpression
                    {
                        var xBinaryExpression = filter.Body as BinaryExpression;
                        if (xBinaryExpression != null)
                        {

                            var __WhereCommand = state.WhereCommand;
                            state.WriteExpression(ref __WhereCommand, filter.Body, that);
                            state.WhereCommand = __WhereCommand;
                            return;
                        }
                    }
                    #endregion



                    // for op_Equals
                    var body = ((BinaryExpression)filter.Body);

                    // do we need to check our db schema or is reflection the schema for us?



                    #region WriteBinaryExpression
                    Action<BinaryExpression> WriteBinaryExpression =
                        (xbody) =>
                        {
                            var xasMemberExpression = xbody.Left as MemberExpression;
                            if (xasMemberExpression != null)
                            {
                                var xColumnName0 = xasMemberExpression.Member.Name;
                                state.WhereCommand += "`" + xColumnName0 + "` ";

                            }
                            else
                            {
                                var xasUnaryExpression = xbody.Left as UnaryExpression;
                                var xColumnName0 = (xasUnaryExpression.Operand as MemberExpression).Member.Name;


                                state.WhereCommand += "`" + xColumnName0 + "` ";
                            }

                            if (xbody.NodeType == ExpressionType.Equal)
                                state.WhereCommand += "=";
                            else if (xbody.NodeType == ExpressionType.LessThan)
                                state.WhereCommand += "<";
                            else if (xbody.NodeType == ExpressionType.GreaterThan)
                                state.WhereCommand += ">";
                            else
                                Debugger.Break();

                            // -		(new System.Linq.Expressions.Expression.BinaryExpressionProxy(x_asLogicalBinaryExpression0 as System.Linq.Expressions.LogicalBinaryExpression)).Right	{0}	System.Linq.Expressions.Expression {System.Linq.Expressions.ConstantExpression}


                            #region asConstantExpression
                            var asConstantExpression = xbody.Right as ConstantExpression;
                            if (asConstantExpression != null)
                            {
                                var xrAddParameterValue0 = asConstantExpression.Value;
                                var xn = "@where" + state.ApplyParameter.Count;

                                state.WhereCommand += " ";
                                state.WhereCommand += xn;

                                Console.WriteLine("MutableWhere OrElse " + new { xn, xrAddParameterValue0 });

                                state.ApplyParameter.Add(
                                    c =>
                                    {
                                        // either the actualt command or the explain command?

                                        //c.Parameters.AddWithValue(n, r);
                                        c.AddParameter(xn, xrAddParameterValue0);
                                    }
                                );
                                return;
                            }
                            #endregion


                            #region yasMemberExpression
                            // X:\jsc.svn\examples\javascript\LINQ\test\TestSelectOrUnaryExpression\TestSelectOrUnaryExpression\ApplicationWebService.cs
                            var yasMemberExpression = xbody.Right as MemberExpression;
                            if (yasMemberExpression != null)
                            {
                                // 		test	0x0000000000000017	long

                                var yasMConstantExpression = yasMemberExpression.Expression as ConstantExpression;
                                if (yasMConstantExpression != null)
                                {
                                    //yasMemberExpression.Member 
                                    var yasMemberExpressionField = yasMemberExpression.Member as FieldInfo;

                                    var xrAddParameterValue0 = yasMemberExpressionField.GetValue(yasMConstantExpression.Value);
                                    var xn = "@where" + state.ApplyParameter.Count;

                                    state.WhereCommand += " ";
                                    state.WhereCommand += xn;

                                    Console.WriteLine("MutableWhere OrElse " + new { xn, xrAddParameterValue0 });

                                    state.ApplyParameter.Add(
                                        c =>
                                        {
                                            // either the actualt command or the explain command?

                                            //c.Parameters.AddWithValue(n, r);
                                            c.AddParameter(xn, xrAddParameterValue0);
                                        }
                                    );
                                    return;
                                }
                            }
                            #endregion


                            #region yUnaryExpression
                            // X:\jsc.svn\examples\javascript\LINQ\test\TestSelectOrUnaryExpression\TestSelectOrUnaryExpression\ApplicationWebService.cs
                            var yUnaryExpression = xbody.Right as UnaryExpression;
                            if (yUnaryExpression != null)
                            {
                                #region yasMemberExpression
                                // X:\jsc.svn\examples\javascript\LINQ\test\TestSelectOrUnaryExpression\TestSelectOrUnaryExpression\ApplicationWebService.cs
                                var yyasMemberExpression = yUnaryExpression.Operand as MemberExpression;
                                if (yyasMemberExpression != null)
                                {
                                    // 		test	0x0000000000000017	long

                                    var yyasMConstantExpression = yyasMemberExpression.Expression as ConstantExpression;
                                    if (yyasMConstantExpression != null)
                                    {
                                        //yasMemberExpression.Member 
                                        var yyasMemberExpressionField = yyasMemberExpression.Member as FieldInfo;

                                        var xrAddParameterValue0 = yyasMemberExpressionField.GetValue(yyasMConstantExpression.Value);
                                        var xn = "@where" + state.ApplyParameter.Count;

                                        state.WhereCommand += " ";
                                        state.WhereCommand += xn;

                                        Console.WriteLine("MutableWhere OrElse " + new { xn, xrAddParameterValue0 });

                                        state.ApplyParameter.Add(
                                            c =>
                                            {
                                                // either the actualt command or the explain command?

                                                //c.Parameters.AddWithValue(n, r);
                                                c.AddParameter(xn, xrAddParameterValue0);
                                            }
                                        );
                                        return;
                                    }
                                }
                                #endregion
                            }
                            #endregion


                            Debugger.Break();
                        };
                    #endregion

                    if (body.NodeType == ExpressionType.OrElse)
                    {
                        state.WhereCommand += "(";
                        WriteBinaryExpression(body.Left as BinaryExpression);
                        state.WhereCommand += " or ";
                        WriteBinaryExpression(body.Right as BinaryExpression);
                        state.WhereCommand += ")";

                        //Debugger.Break();
                        return;
                    }












                    // X:\jsc.svn\examples\javascript\LINQ\test\TestSelectDatesThenCountSimilars\TestSelectDatesThenCountSimilars\ApplicationWebService.cs

                    var lColumnName0 = "";

                    var rAddParameterValue0 = default(object);


                    #region ColumnName

                    if (body.Left is MemberExpression)
                    {
                        lColumnName0 = ((MemberExpression)body.Left).Member.Name;
                    }
                    else if (body.Left is UnaryExpression)
                    {
                        lColumnName0 = ((MemberExpression)((UnaryExpression)body.Left).Operand).Member.Name;
                    }
                    else
                    {
                        // +		filter	{z => ((Convert(z.FooStateEnum) == 0) OrElse (Convert(z.FooStateEnum) == 2))}	System.Linq.Expressions.LambdaExpression {System.Linq.Expressions.Expression<System.Func<TestSQLiteGroupBy.Data.Book1MiddleRow,bool>>}

                        //                +		body.Left	{(Convert(z.FooStateEnum) == 0)}	System.Linq.Expressions.Expression {System.Linq.Expressions.LogicalBinaryExpression}
                        //+		body.Right	{(Convert(z.FooStateEnum) == 2)}	System.Linq.Expressions.Expression {System.Linq.Expressions.LogicalBinaryExpression}
                        //        body.NodeType	OrElse	System.Linq.Expressions.ExpressionType



                        Debugger.Break();
                    }
                    #endregion

                    state.WhereCommand += "`" + lColumnName0 + "` ";


                    #region rAddParameterValue

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
                            rAddParameterValue0 = ((FieldInfo)f_Body_Right.Member).GetValue(f_Body_Right_Expression_Value);


                        }
                        else if (f_Body_Right_as_MemberExpression != null)
                        {
                            // we are doing a where against object field passed method argument

                            var xFieldInfo = f_Body_Right_as_MemberExpression.Member as FieldInfo;
                            if (xFieldInfo != null)
                            {
                                var aMConstantExpression = f_Body_Right_as_MemberExpression.Expression as ConstantExpression;

                                var value1 = xFieldInfo.GetValue(aMConstantExpression.Value);


                                var xxFieldInfo = f_Body_Right.Member as FieldInfo;
                                if (xxFieldInfo != null)
                                {
                                    rAddParameterValue0 = xxFieldInfo.GetValue(value1);
                                }
                                else
                                {
                                    // f_Body_Right.Member = {System.DateTime x24}

                                    var xxPropertyInfo = f_Body_Right.Member as PropertyInfo;
                                    var value2 = xxPropertyInfo.GetValue(value1, null);

                                    rAddParameterValue0 = value2;
                                }
                            }
                            else
                            {
                                // X:\jsc.svn\examples\javascript\LINQ\MashableVelocityGraph\MashableVelocityGraph\ApplicationWebService.cs
                                #region xPropertyInfo
                                var xPropertyInfo = f_Body_Right_as_MemberExpression.Member as PropertyInfo;
                                if (xPropertyInfo != null)
                                {
                                    var asMMemberExpression = f_Body_Right_as_MemberExpression.Expression as MemberExpression;
                                    if (asMMemberExpression != null)
                                    {
                                        var asMMConstantExpression = asMMemberExpression.Expression as ConstantExpression;
                                        if (asMMConstantExpression != null)
                                        {
                                            // Value = {MashableVelocityGraph.ApplicationWebService.}

                                            //var value1 = ((PropertyInfo)asMMemberExpression.Member).GetValue(asMMConstantExpression.Value, null);
                                            var value1 = ((FieldInfo)asMMemberExpression.Member).GetValue(asMMConstantExpression.Value);
                                            var value2 = xPropertyInfo.GetValue(value1, null);

                                            //rAddParameterValue0 = ((FieldInfo)f_Body_Right.Member).GetValue(value2);
                                            rAddParameterValue0 = ((PropertyInfo)f_Body_Right.Member).GetValue(value2, null);
                                        }

                                    }

                                    // body.Right = {g.Key.domComplete}

                                    var xParameterExpression = f_Body_Right_as_MemberExpression.Expression as ParameterExpression;
                                    if (xParameterExpression != null)
                                    {
                                        // ?
                                        // x:\jsc.svn\examples\javascript\linq\test\testgroupbycountviascalarwhere\testgroupbycountviascalarwhere\applicationwebservice.cs

                                        // we are in a nested scalar select, probably in count which is doing a where against an upper parameter.
                                        // how is it defined?


                                        // there may not be an upper where. as they may be collapsed?
                                        var upperScalarSelect = (that as INestedQueryStrategy).upperSelect;

                                        var upperGroupBy = upperScalarSelect.upperGroupBy;


                                        //if (xParameterExpression.Name == (upperGroupBy.elementSelector as LambdaExpression).Parameters[0].Name)
                                        if (xParameterExpression.Name == (upperGroupBy.upperSelect.selectorExpression as LambdaExpression).Parameters[0].Name)
                                        {
                                            // found it!
                                            // render sql and return!
                                            // we are comparing to the group key in result are we not?

                                            if (body.NodeType == ExpressionType.Equal)
                                                state.WhereCommand += "=";
                                            else if (body.NodeType == ExpressionType.LessThan)
                                                state.WhereCommand += "<";

                                            else if (body.NodeType == ExpressionType.LessThanOrEqual)
                                                state.WhereCommand += "<=";

                                            else if (body.NodeType == ExpressionType.GreaterThan)
                                                state.WhereCommand += ">";
                                            else if (body.NodeType == ExpressionType.GreaterThanOrEqual)
                                                state.WhereCommand += ">=";
                                            else if (body.NodeType == ExpressionType.NotEqual)
                                                state.WhereCommand += "<>";
                                            else
                                                Debugger.Break();

                                            state.WhereCommand += " s.`" + f_Body_Right.Member.Name + "`";

                                            return;
                                        }
                                    }

                                }
                                #endregion

                            }
                        }
                        else
                        {
                            // X:\jsc.svn\examples\javascript\linq\test\TestSelectScalarAverage\TestSelectScalarAverage\ApplicationWebService.cs
                            // x:\jsc.svn\examples\javascript\linq\test\testselectofselect\testselectofselect\applicationwebservice.cs

                            #region asRParameterExpression
                            var asRParameterExpression = f_Body_Right.Expression as ParameterExpression;
                            if (asRParameterExpression != null)
                            {

                                if (body.NodeType == ExpressionType.Equal)
                                    state.WhereCommand += "=";
                                else if (body.NodeType == ExpressionType.LessThan)
                                    state.WhereCommand += "<";
                                else if (body.NodeType == ExpressionType.GreaterThan)
                                    state.WhereCommand += ">";
                                else if (body.NodeType == ExpressionType.GreaterThanOrEqual)
                                    state.WhereCommand += ">=";
                                else if (body.NodeType == ExpressionType.NotEqual)
                                    state.WhereCommand += "<>";
                                else
                                    Debugger.Break();

                                var asISelectQueryStrategy = that as ISelectQueryStrategy;
                                var asISLambdaExpression = asISelectQueryStrategy.selectorExpression as LambdaExpression;

                                if (asISelectQueryStrategy != null)
                                {
                                    if (asISelectQueryStrategy.upperSelect != null)
                                    {
                                        var uL = asISelectQueryStrategy.upperSelect.selectorExpression as LambdaExpression;
                                        if (uL != null)
                                        {
                                            if (uL.Parameters[0].Name == asRParameterExpression.Name)
                                            {
                                                // are we in the wrong level?
                                                // X:\jsc.svn\examples\javascript\LINQ\test\TestSelectAboveAverage\TestSelectAboveAverage\ApplicationWebService.cs
                                                //state.WhereCommand += " " + asISLambdaExpression.Parameters[0].Name.Replace("<>", "__") + ".`" + f_Body_Right.Member.Name + "` ";
                                                state.WhereCommand += " `" + f_Body_Right.Member.Name + "` ";
                                                return;
                                            }
                                        }
                                    }
                                }

                                // X:\jsc.svn\examples\javascript\LINQ\test\TestSelectAboveAverage\TestSelectAboveAverage\ApplicationWebService.cs
                                state.WhereCommand += " " + asRParameterExpression.Name.Replace("<>", "__") + ".`" + f_Body_Right.Member.Name + "` ";
                                return;
                            }
                            #endregion

                            // f_Body_Right = {DateTime.Now}

                            Debugger.Break();
                        }

                    }
                    else if (body.Right is UnaryExpression)
                    {
                        // casting enum to long?
                        var xExpression = ((UnaryExpression)body.Right).Operand;

                        // https://sites.google.com/a/jsc-solutions.net/backlog/knowledge-base/2014/201406/20140606
                        // +		xExpression	{UNREAD}	System.Linq.Expressions.Expression {System.Linq.Expressions.ConstantExpression}

                        var asConstantExpression = xExpression as ConstantExpression;
                        if (asConstantExpression != null)
                        {
                            rAddParameterValue0 = asConstantExpression.Value;
                        }
                        else
                        {


                            var xUMemberExpression = xExpression as MemberExpression;

                            var xUMConstantExpression = xUMemberExpression.Expression as ConstantExpression;
                            if (xUMConstantExpression != null)
                            {

                                var f_Body_Right_Expression_Value = xUMConstantExpression.Value;
                                rAddParameterValue0 = ((FieldInfo)xUMemberExpression.Member).GetValue(f_Body_Right_Expression_Value);
                            }
                            else
                            {
                                var asRMemberExpression = xUMemberExpression.Expression as MemberExpression;
                                if (asRMemberExpression != null)
                                {
                                    // we are doing a where against object field passed method argument

                                    var zE = asRMemberExpression.Expression as ConstantExpression;
                                    if (zE != null)
                                    {
                                        var asRFieldInfo = asRMemberExpression.Member as FieldInfo;
                                        if (asRFieldInfo != null)
                                        {

                                            var f_Body_Right_Expression_Value = asRFieldInfo.GetValue(zE.Value);


                                            rAddParameterValue0 = ((FieldInfo)xUMemberExpression.Member).GetValue(f_Body_Right_Expression_Value);
                                        }
                                        Debugger.Break();
                                    }
                                    else
                                    {
                                        // filter.Body = {(Convert(kk.ApplicationPerformance) == Convert(<>h__TransparentIdentifier0.k.Key))}

                                        // filter.Body = {(Convert(kk.ApplicationPerformance) == Convert(<>h__TransparentIdentifier1.<>h__TransparentIdentifier0.k.Key))}

                                        var asRMMemberExpression = asRMemberExpression.Expression as MemberExpression;
                                        if (asRMMemberExpression != null)
                                        {
                                            // {(Convert(kk.ApplicationPerformance) == Convert(<>h__TransparentIdentifier2.<>h__TransparentIdentifier1.<>h__TransparentIdentifier0.k.Key))}

                                            #region asRMMMemberExpression
                                            var asRMMMemberExpression = asRMMemberExpression.Expression as MemberExpression;
                                            if (asRMMMemberExpression != null)
                                            {
                                                var asRMMMParameterExpression = asRMMMemberExpression.Expression as ParameterExpression;
                                                if (asRMMMParameterExpression != null)
                                                {
                                                    if (body.NodeType == ExpressionType.Equal)
                                                        state.WhereCommand += "=";
                                                    else if (body.NodeType == ExpressionType.LessThan)
                                                        state.WhereCommand += "<";
                                                    else if (body.NodeType == ExpressionType.GreaterThan)
                                                        state.WhereCommand += ">";
                                                    else if (body.NodeType == ExpressionType.NotEqual)
                                                        state.WhereCommand += "<>";
                                                    else
                                                        Debugger.Break();


                                                    state.WhereCommand += " " +
                                                    asRMMMParameterExpression.Name.Replace("<>", "__")
                                                    + ".`" + xUMemberExpression.Member.Name + "` ";

                                                    return;
                                                }
                                            }
                                            #endregion


                                            #region asRMMParameterExpression
                                            var asRMMParameterExpression = asRMMemberExpression.Expression as ParameterExpression;
                                            if (asRMMParameterExpression != null)
                                            {
                                                if (body.NodeType == ExpressionType.Equal)
                                                    state.WhereCommand += "=";
                                                else if (body.NodeType == ExpressionType.LessThan)
                                                    state.WhereCommand += "<";
                                                else if (body.NodeType == ExpressionType.GreaterThan)
                                                    state.WhereCommand += ">";
                                                else if (body.NodeType == ExpressionType.NotEqual)
                                                    state.WhereCommand += "<>";
                                                else
                                                    Debugger.Break();


                                                state.WhereCommand += " " +
                                                asRMMParameterExpression.Name.Replace("<>", "__")
                                                + ".`" + xUMemberExpression.Member.Name + "` ";

                                                return;
                                            }
                                            #endregion

                                        }

                                        #region asRMParameterExpression
                                        var asRMParameterExpression = asRMemberExpression.Expression as ParameterExpression;
                                        if (asRMParameterExpression != null)
                                        {
                                            if (body.NodeType == ExpressionType.Equal)
                                                state.WhereCommand += "=";
                                            else if (body.NodeType == ExpressionType.LessThan)
                                                state.WhereCommand += "<";
                                            else if (body.NodeType == ExpressionType.GreaterThan)
                                                state.WhereCommand += ">";
                                            else if (body.NodeType == ExpressionType.NotEqual)
                                                state.WhereCommand += "<>";
                                            else
                                                Debugger.Break();


                                            state.WhereCommand += " " +
                                            asRMParameterExpression.Name.Replace("<>", "__")
                                            + ".`" + xUMemberExpression.Member.Name + "` ";

                                            return;
                                        }
                                        #endregion

                                    }



                                }
                                else
                                {
                                    // X:\jsc.svn\examples\javascript\linq\test\TestSelectScalarAverage\TestSelectScalarAverage\ApplicationWebService.cs
                                    // x:\jsc.svn\examples\javascript\linq\test\testselectofselect\testselectofselect\applicationwebservice.cs

                                    #region asRParameterExpression
                                    var asRParameterExpression = xUMemberExpression.Expression as ParameterExpression;
                                    if (asRParameterExpression != null)
                                    {

                                        if (body.NodeType == ExpressionType.Equal)
                                            state.WhereCommand += "=";
                                        else if (body.NodeType == ExpressionType.LessThan)
                                            state.WhereCommand += "<";
                                        else if (body.NodeType == ExpressionType.GreaterThan)
                                            state.WhereCommand += ">";
                                        else if (body.NodeType == ExpressionType.NotEqual)
                                            state.WhereCommand += "<>";
                                        else
                                            Debugger.Break();


                                        state.WhereCommand += " " + asRParameterExpression.Name + ".`" + xUMemberExpression.Member.Name + "` ";
                                        return;
                                    }
                                    #endregion

                                    Debugger.Break();
                                }
                            }
                        }
                    }
                    else
                    {
                        // https://sites.google.com/a/jsc-solutions.net/backlog/knowledge-base/2014/201405/201405

                        var asConstantExpression = body.Right as ConstantExpression;
                        if (asConstantExpression != null)
                        {
                            rAddParameterValue0 = asConstantExpression.Value;
                        }
                        else Debugger.Break();
                    }
                    #endregion

                    // like we do in jsc. this is the opcode
                    //OpCodes.Ceq

                    #region ExpressionType
                    if (body.NodeType == ExpressionType.Equal)
                        state.WhereCommand += "=";
                    else if (body.NodeType == ExpressionType.LessThan)
                        state.WhereCommand += "<";

                    else if (body.NodeType == ExpressionType.LessThanOrEqual)
                        state.WhereCommand += "<=";

                    else if (body.NodeType == ExpressionType.GreaterThan)
                        state.WhereCommand += ">";
                    else if (body.NodeType == ExpressionType.GreaterThanOrEqual)
                        state.WhereCommand += ">=";
                    else if (body.NodeType == ExpressionType.NotEqual)
                        state.WhereCommand += "<>";
                    else
                        Debugger.Break();
                    #endregion




                    #region  arg name collision
                    var n = "";

                    var xINestedQueryStrategy = that as INestedQueryStrategy;
                    while (xINestedQueryStrategy != null)
                    {


                        if (xINestedQueryStrategy.upperGroupBy != null)
                        {
                            n = "GroupBy_" + n;
                            xINestedQueryStrategy = xINestedQueryStrategy.upperGroupBy;
                        }
                        else if (xINestedQueryStrategy.upperJoin != null)
                        {
                            n = "Join_" + n;

                            xINestedQueryStrategy = xINestedQueryStrategy.upperJoin;
                        }
                        else if (xINestedQueryStrategy.upperSelect != null)
                        {
                            n = "Select_" + n;

                            xINestedQueryStrategy = xINestedQueryStrategy.upperSelect;
                        }
                        else if (xINestedQueryStrategy.upperSelect != null)
                        {
                            n = "SelectMany_" + n;
                            xINestedQueryStrategy = xINestedQueryStrategy.upperSelectMany;
                        }
                        else break;
                    }


                    n = "@" + n + "Where" + state.ApplyParameter.Count;

                    state.WhereCommand += " ";
                    state.WhereCommand += n;
                    #endregion

                    Console.WriteLine("MutableWhere " + new { n, r = rAddParameterValue0 });

                    // X:\jsc.svn\examples\javascript\linq\test\TestJoinSelectAnonymousType\TestJoinSelectAnonymousType\ApplicationWebService.cs
                    state.ApplyParameter.Add(
                        c =>
                        {
                            // um. the upper scope isnt calling this here, in a join, why?

                            // either the actualt command or the explain command?

                            //c.Parameters.AddWithValue(n, r);
                            c.AddParameter(n, rAddParameterValue0);
                        }
                    );


                }
            );
        }

        #endregion



        // X:\jsc.svn\examples\javascript\LINQ\test\TestGroupByThenOrderByThenOrderBy\TestGroupByThenOrderByThenOrderBy\ApplicationWebService.cs


        [ScriptCoreLib.ScriptAttribute.ExplicitInterface]
        public interface IWhereQueryStrategy : INestedQueryStrategy
        {
            Expression filter { get; }
            IQueryStrategy source { get; }

        }

        class WhereQueryStrategy<TElement> : XQueryStrategy<TElement>, IWhereQueryStrategy
        {
            public Expression<Func<TElement, bool>> filter { get; set; }
            public IQueryStrategy source { get; set; }

            Expression IWhereQueryStrategy.filter
            {
                get
                {
                    return this.filter;
                }
            }

            IQueryStrategy IWhereQueryStrategy.source
            {
                get
                {
                    return this.source;
                }
            }

            public ISelectManyQueryStrategy upperSelectMany { get; set; }
            public ISelectQueryStrategy upperSelect { get; set; }
            public IJoinQueryStrategy upperJoin { get; set; }
            public IGroupByQueryStrategy upperGroupBy { get; set; }
        }


        static MethodInfo refWhere = new Func<IQueryStrategy<object>, Expression<Func<object, bool>>, IQueryStrategy<object>>(QueryStrategyOfTRowExtensions.Where).Method;
        public static IQueryStrategy<TElement> Where<TElement>(this IQueryStrategy<TElement> source, Expression<Func<TElement, bool>> filter)
        {
            // which thread are we running on?
            // X:\jsc.svn\examples\javascript\appengine\AppEngineUserAgentLoggerWithXSLXAsset\AppEngineUserAgentLoggerWithXSLXAsset\ApplicationWebService.cs
            Console.WriteLine("enter Where");


            var xINestedQueryStrategy = source is INestedQueryStrategy;
            if (!xINestedQueryStrategy)
            {
                // lets mark it as nestable, this allows where to know how to aviod name clashes.
                // X:\jsc.svn\examples\javascript\linq\test\TestGroupByCountViaScalarWhere\TestGroupByCountViaScalarWhere\ApplicationWebService.cs
                // https://sites.google.com/a/jsc-solutions.net/backlog/knowledge-base/2014/201406/20140607


                // first step being immputable for where clauses
                var that = new WhereQueryStrategy<TElement>
                {
                    source = source,
                    filter = filter,


                    InternalGetElementType =
                    delegate
                    {
                        return source.GetElementType();
                    },

                    InternalGetDescriptor =
                    () =>
                    {
                        // inherit the connection/context from above
                        var StrategyDescriptor = source.GetDescriptor();

                        return StrategyDescriptor;
                    }
                };

                MutableWhere(
                    that, filter
                );

                return that;
            }

            MutableWhere(
                source, filter
            );

            return source;
        }
    }
}

