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
        public static void MutableWhere(IQueryStrategy that, LambdaExpression filter)
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

                    {
                        var asMethodCallExpression = filter.Body as MethodCallExpression;
                        if (asMethodCallExpression != null)
                        {
                            if (asMethodCallExpression.Method.Name == "Contains")
                            {
                                // http://stackoverflow.com/questions/8054942/string-isnullorempty-in-linq-to-sql-query
                                // http://connect.microsoft.com/VisualStudio/feedback/details/367077/i-want-to-use-string-isnullorempty-in-linq-to-sql-statements
                                // http://stackoverflow.com/questions/15663207/how-to-use-null-or-empty-string-in-sql
                                // x:\jsc.svn\examples\javascript\linq\minmaxaverageexperiment\minmaxaverageexperiment\applicationwebservice.cs

                                var rAddParameterValue0 = default(object);

                                var arg0ConstantExpression = asMethodCallExpression.Arguments[0] as ConstantExpression;
                                if (arg0ConstantExpression != null)
                                {
                                    rAddParameterValue0 = arg0ConstantExpression.Value;
                                }
                                else
                                {
                                    var arg0MemberExpression = asMethodCallExpression.Arguments[0] as MemberExpression;
                                    if (arg0MemberExpression != null)
                                    {
                                        var arg0MConstantExpression = arg0MemberExpression.Expression as ConstantExpression;
                                        if (arg0MConstantExpression != null)
                                        {

                                            var f = arg0MemberExpression.Member as FieldInfo;
                                            rAddParameterValue0 = f.GetValue(arg0MConstantExpression.Value);
                                        }
                                        else
                                        {
                                            var arg0MMemberExpression = arg0MemberExpression.Expression as MemberExpression;
                                            var arg0MMConstantExpression = arg0MMemberExpression.Expression as ConstantExpression;
                                            if (arg0MMConstantExpression != null)
                                            {
                                                var ff = arg0MMemberExpression.Member as FieldInfo;

                                                var vv = ff.GetValue(arg0MMConstantExpression.Value);

                                                // +		arg0MemberExpression.Member	{System.String ff}	System.Reflection.MemberInfo {System.Reflection.RuntimePropertyInfo}

                                                var p = arg0MemberExpression.Member as PropertyInfo;

                                                //vv = { ff = "bar" }
                                                rAddParameterValue0 = p.GetValue(vv, null);
                                            }
                                            else
                                                Debugger.Break();
                                        }
                                    }
                                    else
                                        Debugger.Break();
                                }

                                #region x.Contains(?)

                                // asMethodCallExpression.Object = {k.path.ToLower()}
                                var xColumnName0 = asMethodCallExpression.Object as MemberExpression;
                                if (xColumnName0 != null)
                                {
                                    var n = "@where" + state.ApplyParameter.Count;

                                    // http://stackoverflow.com/questions/16180117/instr-function-sqlite-for-android

                                    state.WhereCommand += "(replace(";
                                    state.WhereCommand += "`" + xColumnName0.Member.Name + "`, " + n;
                                    state.WhereCommand += ", '')<>`" + xColumnName0.Member.Name + "`)";

                                    state.ApplyParameter.Add(
                                        c =>
                                        {
                                            // either the actualt command or the explain command?

                                            //c.Parameters.AddWithValue(n, r);
                                            c.AddParameter(n, rAddParameterValue0);
                                        }
                                    );

                                    return;
                                }
                                #endregion

                                #region x.ToLower().Contains(?)
                                var asMMethodCallExpression = asMethodCallExpression.Object as MethodCallExpression;
                                if (asMMethodCallExpression != null)
                                {
                                    // x:\jsc.svn\examples\javascript\linq\test\testselectwheretolowercontains\testselectwheretolowercontains\applicationwebservice.cs

                                    if (asMMethodCallExpression.Method.Name == "ToLower")
                                    {
                                        var xMColumnName0 = asMMethodCallExpression.Object as MemberExpression;

                                        var n = "@where" + state.ApplyParameter.Count;

                                        // http://stackoverflow.com/questions/16180117/instr-function-sqlite-for-android

                                        state.WhereCommand += "(replace(";
                                        state.WhereCommand += "lower(`" + xMColumnName0.Member.Name + "`), " + n;
                                        state.WhereCommand += ", '')<>lower(`" + xMColumnName0.Member.Name + "`))";

                                        state.ApplyParameter.Add(
                                            c =>
                                            {
                                                // either the actualt command or the explain command?

                                                //c.Parameters.AddWithValue(n, r);
                                                c.AddParameter(n, rAddParameterValue0);
                                            }
                                        );

                                        return;
                                    }

                                }
                                #endregion

                                Debugger.Break();
                                return;
                            }
                        }
                    }




                    #region IsNullOrEmpty
                    var asUnaryExpression = filter.Body as UnaryExpression;
                    if (asUnaryExpression != null)
                    {
                        // Operand = {IsNullOrEmpty(k.path)}

                        if (asUnaryExpression.NodeType == ExpressionType.Not)
                        {


                            var asMethodCallExpression = asUnaryExpression.Operand as MethodCallExpression;
                            if (asMethodCallExpression != null)
                            {
                                if (asMethodCallExpression.Method.Name == "IsNullOrEmpty")
                                {
                                    // http://stackoverflow.com/questions/8054942/string-isnullorempty-in-linq-to-sql-query
                                    // http://connect.microsoft.com/VisualStudio/feedback/details/367077/i-want-to-use-string-isnullorempty-in-linq-to-sql-statements
                                    // http://stackoverflow.com/questions/15663207/how-to-use-null-or-empty-string-in-sql
                                    // x:\jsc.svn\examples\javascript\linq\minmaxaverageexperiment\minmaxaverageexperiment\applicationwebservice.cs

                                    var arg1 = asMethodCallExpression.Arguments[0] as MemberExpression;

                                    var xColumnName0 = arg1.Member.Name;


                                    state.WhereCommand += "not(";
                                    state.WhereCommand += "`" + xColumnName0 + "` is null or length(`" + xColumnName0 + "`) = 0";
                                    state.WhereCommand += ")";
                                    return;
                                }
                            }
                        }
                    }
                    #endregion


                    // for op_Equals
                    var body = ((BinaryExpression)filter.Body);

                    // do we need to check our db schema or is reflection the schema for us?



                    #region WriteExpression
                    Action<BinaryExpression> WriteExpression =
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
                                var rAddParameterValue0 = asConstantExpression.Value;
                                var n = "@where" + state.ApplyParameter.Count;

                                state.WhereCommand += " ";
                                state.WhereCommand += n;

                                Console.WriteLine("MutableWhere OrElse " + new { n, rAddParameterValue0 });

                                state.ApplyParameter.Add(
                                    c =>
                                    {
                                        // either the actualt command or the explain command?

                                        //c.Parameters.AddWithValue(n, r);
                                        c.AddParameter(n, rAddParameterValue0);
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

                                    var rAddParameterValue0 = yasMemberExpressionField.GetValue(yasMConstantExpression.Value);
                                    var n = "@where" + state.ApplyParameter.Count;

                                    state.WhereCommand += " ";
                                    state.WhereCommand += n;

                                    Console.WriteLine("MutableWhere OrElse " + new { n, rAddParameterValue0 });

                                    state.ApplyParameter.Add(
                                        c =>
                                        {
                                            // either the actualt command or the explain command?

                                            //c.Parameters.AddWithValue(n, r);
                                            c.AddParameter(n, rAddParameterValue0);
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

                                        var rAddParameterValue0 = yyasMemberExpressionField.GetValue(yyasMConstantExpression.Value);
                                        var n = "@where" + state.ApplyParameter.Count;

                                        state.WhereCommand += " ";
                                        state.WhereCommand += n;

                                        Console.WriteLine("MutableWhere OrElse " + new { n, rAddParameterValue0 });

                                        state.ApplyParameter.Add(
                                            c =>
                                            {
                                                // either the actualt command or the explain command?

                                                //c.Parameters.AddWithValue(n, r);
                                                c.AddParameter(n, rAddParameterValue0);
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
                        WriteExpression(body.Left as BinaryExpression);
                        state.WhereCommand += " or ";
                        WriteExpression(body.Right as BinaryExpression);
                        state.WhereCommand += ")";

                        //Debugger.Break();
                    }
                    else
                    {

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

                                var z = (FieldInfo)f_Body_Right_as_MemberExpression.Member;

                                var zE = f_Body_Right_as_MemberExpression.Expression as ConstantExpression;

                                var f_Body_Right_Expression_Value = z.GetValue(zE.Value);


                                rAddParameterValue0 = ((FieldInfo)f_Body_Right.Member).GetValue(f_Body_Right_Expression_Value);
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
                                rAddParameterValue0 = ((FieldInfo)f_Body_Right.Member).GetValue(f_Body_Right_Expression_Value);
                            }
                            else if (f_Body_Right_as_MemberExpression != null)
                            {
                                // we are doing a where against object field passed method argument

                                var z = (FieldInfo)f_Body_Right_as_MemberExpression.Member;

                                var zE = f_Body_Right_as_MemberExpression.Expression as ConstantExpression;

                                var f_Body_Right_Expression_Value = z.GetValue(zE.Value);


                                rAddParameterValue0 = ((FieldInfo)f_Body_Right.Member).GetValue(f_Body_Right_Expression_Value);
                            }
                            else Debugger.Break();
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


                        // arg name collision
                        var n = "@where" + state.ApplyParameter.Count;

                        state.WhereCommand += " ";
                        state.WhereCommand += n;

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

                }
            );
        }

        #endregion



        // X:\jsc.svn\examples\javascript\LINQ\test\TestGroupByThenOrderByThenOrderBy\TestGroupByThenOrderByThenOrderBy\ApplicationWebService.cs

        static MethodInfo refWhere = new Func<IQueryStrategy<object>, Expression<Func<object, bool>>, IQueryStrategy<object>>(QueryStrategyOfTRowExtensions.Where).Method;
        public static IQueryStrategy<TElement> Where<TElement>(this IQueryStrategy<TElement> source, Expression<Func<TElement, bool>> filter)
        {
            MutableWhere(
                source, filter
            );

            return source;
        }
    }
}

