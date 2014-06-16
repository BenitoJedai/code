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
using System.Threading;

//namespace System.Data
namespace ScriptCoreLib.Shared.Data.Diagnostics
{
    // move to a nuget?
    // shall reimplement IQueriable for jsc data layer gen
    //[Obsolete("the first generic extension method for all jsc data layer rows")]
    public static partial class QueryStrategyExtensions
    {



        static int WriteExpressionConstantCounter = 100;

        [Obsolete("refactoring... Where gets to use it first.")]
        // are the extension methods the new instance methods?
        // shall we  start by string::IsNullOrEmpty ?
        // X:\jsc.svn\examples\javascript\LINQ\test\TestSelectDatesThenCountSimilars\TestSelectDatesThenCountSimilars\ApplicationWebService.cs
        public static void WriteExpression(
            this CommandBuilderState state,
            // either WhereCommand or SelectCommand
            // byref against locals will likely work better than fields 
            ref string s,
            Expression asExpression,

            IQueryStrategy that
            )
        {
            // X:\jsc.svn\examples\javascript\linq\test\TestWhereIsNullOrEmpty\TestWhereIsNullOrEmpty\ApplicationWebService.cs
            // X:\jsc.svn\core\ScriptCoreLib.Extensions\ScriptCoreLib.Extensions\Query\QueryStrategyOfTRowExtensions.Where.cs

            // where not(`path` is null or length(`path`) = 0)
            // asExpression = {Not(IsNullOrEmpty(x.path))}

            #region asBinaryExpression
            var asBinaryExpression = asExpression as BinaryExpression;
            if (asBinaryExpression != null)
            {
                s += "(";
                state.WriteExpression(ref s, asBinaryExpression.Left, that);


                #region ExpressionType
                if (asBinaryExpression.NodeType == ExpressionType.Equal)
                    s += "=";
                else if (asBinaryExpression.NodeType == ExpressionType.LessThan)
                    s += "<";
                else if (asBinaryExpression.NodeType == ExpressionType.LessThanOrEqual)
                    s += "<=";
                else if (asBinaryExpression.NodeType == ExpressionType.GreaterThan)
                    s += ">";
                else if (asBinaryExpression.NodeType == ExpressionType.GreaterThanOrEqual)
                    s += ">=";
                else if (asBinaryExpression.NodeType == ExpressionType.NotEqual)
                    s += "<>";
                else if (asBinaryExpression.NodeType == ExpressionType.OrElse)
                    s += " or ";
                else if (asBinaryExpression.NodeType == ExpressionType.AndAlso)
                    s += " and ";

                // X:\jsc.svn\examples\javascript\linq\test\TestWhereMathAdd\TestWhereMathAdd\ApplicationWebService.cs
                else if (asBinaryExpression.NodeType == ExpressionType.Add)
                    s += " + ";
                else if (asBinaryExpression.NodeType == ExpressionType.Subtract)
                    s += " - ";
                else if (asBinaryExpression.NodeType == ExpressionType.Multiply)
                    s += " * ";
                else if (asBinaryExpression.NodeType == ExpressionType.Divide)
                    s += " / ";

                else
                    Debugger.Break();
                #endregion

                state.WriteExpression(ref s, asBinaryExpression.Right, that);
                s += ")";
                return;
            }
            #endregion


            #region asConstantExpression
            var asConstantExpression = asExpression as ConstantExpression;
            if (asConstantExpression != null)
            {
                var __value = asConstantExpression.Value;

                WriteExpressionConstantCounter++;
                var n = "@arg" + WriteExpressionConstantCounter;
                s += n;

                state.ApplyParameter.Add(c => c.AddParameter(n, __value));
                return;
            }
            #endregion


            #region WriteExpression:asMemberExpression
            var asMemberExpression = asExpression as MemberExpression;
            if (asMemberExpression != null)
            {
                // arg1 = {value(TestWhereContains.ApplicationWebService+<>c__DisplayClass0).z.u.n}
                // are we supposed to find that constant?

                #region GetValue
                var GetValue = default(Func<object>);

                #region xConstantExpression
                Action<MemberExpression, Action<object>> yy = null;

                yy = (x, yield) =>
                {
                    #region atyield
                    Action<object> atyield =
                        __value =>
                        {
                            var xPropertyInfo = x.Member as PropertyInfo;
                            if (xPropertyInfo != null)
                            {
                                yield(
                                    xPropertyInfo.GetValue(__value, null)
                                );
                                return;
                            }


                            var xFieldInfo = x.Member as FieldInfo;
                            if (xFieldInfo != null)
                            {
                                yield(
                                    xFieldInfo.GetValue(__value)
                                );
                                // __value2 = { u = { n = C } }
                                return;
                            }
                        };
                    #endregion


                    var xConstantExpression = x.Expression as ConstantExpression;
                    if (xConstantExpression != null)
                    {
                        // z = { u = { n = C } }
                        atyield(xConstantExpression.Value);
                    }


                    if (x.Expression is MemberExpression)
                        yy(x.Expression as MemberExpression, atyield);
                };
                #endregion

                yy(asMemberExpression,
                     __value =>
                    {
                        GetValue = () => __value;
                    }
                );

                if (GetValue != null)
                {
                    var __value = GetValue();

                    WriteExpressionConstantCounter++;
                    var n = "@arg" + WriteExpressionConstantCounter;
                    s += n;

                    state.ApplyParameter.Add(c => c.AddParameter(n, __value));
                    return;
                }
                #endregion


                // are we looking at a group?
                // asMemberExpression = {gg.Key.domComplete}


                // where (`domComplete`=(`domComplete` - @arg3))


                var currentSelect = (that as QueryStrategyOfTRowExtensions.INestedQueryStrategy).upperSelect;
                if (currentSelect != null)
                {
                    var currentGroup = (that as QueryStrategyOfTRowExtensions.INestedQueryStrategy).upperSelect.upperGroupBy;
                    if (currentGroup != null)
                    {
                        // keySelector = {x => new <>f__AnonymousType1`1(domComplete = x.domComplete)}

                        var gguser = currentGroup.upperSelect.selectorExpression as LambdaExpression;
                        // [0] = {gg}

                        var xMemberExpression = asMemberExpression.Expression as MemberExpression;
                        if (xMemberExpression != null)
                        {
                            // xMemberExpression = {gg.Key}
                            var xMParameterExpression = xMemberExpression.Expression as ParameterExpression;

                            if (xMParameterExpression.Name == gguser.Parameters[0].Name)
                            {
                                // are we talking about the group key member?

                                // thats not available inside here yet

                                var xNewExpression = (currentGroup.keySelector as LambdaExpression).Body as NewExpression;

                                // xNewExpression = {new <>f__AnonymousType1`1(goo = x.domComplete)}
                                // asMemberExpression = {gg.Key.goo}

                                var i = xNewExpression.Members.IndexOf(z => z.Name == asMemberExpression.Member.Name);


                                // group by, the inner source
                                s += "s";
                                s += ".";


                                s += "`";
                                s += "" + (xNewExpression.Arguments[i] as MemberExpression).Member.Name;
                                s += "`";
                                return;
                            }
                        }
                    }


                    var xParameterExpression = asMemberExpression.Expression as ParameterExpression;
                    if (xParameterExpression != null)
                    {

                        var xLambdaExpression = ((that as QueryStrategyOfTRowExtensions.INestedQueryStrategy).upperSelect.selectorExpression as LambdaExpression);
                        if (xLambdaExpression.Parameters[0].Name == xParameterExpression.Name)
                        {
                            // local?
                        }
                        else
                        {

                            // selecting it as we speak..?
                            s += "" + xParameterExpression.Name;
                            s += ".";
                        }
                    }
                }
                else
                {
                    // ?
                }

                s += "`";
                s += "" + asMemberExpression.Member.Name;
                s += "`";
                return;
            }
            #endregion


            #region WriteExpression::UnaryExpression
            var asUnaryExpression = asExpression as UnaryExpression;
            if (asUnaryExpression != null)
            {
                if (asUnaryExpression.NodeType == ExpressionType.Convert)
                {
                    // X:\jsc.svn\examples\javascript\LINQ\test\TestSelectOrUnaryExpression\TestSelectOrUnaryExpression\ApplicationWebService.cs
                    state.WriteExpression(ref s, asUnaryExpression.Operand, that);
                }
                else if (asUnaryExpression.NodeType == ExpressionType.Not)
                {

                    s += "not(";

                    state.WriteExpression(ref s, asUnaryExpression.Operand, that);

                    s += ")";
                }
                else Debugger.Break();

                return;
            }
            #endregion



            // asExpression = {IsNullOrEmpty(x.path)}


            #region WriteExpression:asMethodCallExpression
            var asMethodCallExpression = asExpression as MethodCallExpression;
            if (asMethodCallExpression != null)
            {
                // now what?

                if (asMethodCallExpression.Method.DeclaringType == typeof(string))
                {
                    // asMethodCallExpression.Method = {Boolean Contains(System.String)}

                    #region refToLower
                    var refToLower = new Func<string>("".ToLower).Method;
                    if (refToLower.Name == asMethodCallExpression.Method.Name)
                    {
                        s += "lower(";

                        var arg0 = asMethodCallExpression.Object;
                        state.WriteExpression(ref s, arg0, that);

                        s += ")";
                        return;
                    }
                    #endregion


                    #region refToUpper
                    var refToUpper = new Func<string>("".ToUpper).Method;
                    if (refToUpper.Name == asMethodCallExpression.Method.Name)
                    {
                        s += "upper(";

                        var arg0 = asMethodCallExpression.Object;
                        state.WriteExpression(ref s, arg0, that);

                        s += ")";
                        return;
                    }
                    #endregion


                    #region refContains
                    var refContains = new Func<string, bool>("".Contains).Method;
                    if (refContains.Name == asMethodCallExpression.Method.Name)
                    {
                        var arg1 = asMethodCallExpression.Arguments[0];
                        // arg1 = {"C"}
                        var arg0 = asMethodCallExpression.Object;
                        // arg0 = {x.path}


                        s += "(replace(";

                        state.WriteExpression(ref s, arg0, that);
                        s += ", ";

                        state.WriteExpression(ref s, arg1, that);

                        s += ", '')<>";

                        state.WriteExpression(ref s, arg0, that);
                        s += ")";

                        return;
                    }
                    #endregion


                    #region refIsNullOrEmpty
                    var refIsNullOrEmpty = new Func<string, bool>(string.IsNullOrEmpty).Method;
                    if (refIsNullOrEmpty.Name == asMethodCallExpression.Method.Name)
                    {
                        // the first method

                        // [0] = {x.path}
                        var arg1 = asMethodCallExpression.Arguments[0] as MemberExpression;

                        var xColumnName0 = arg1.Member.Name;

                        // ?
                        s += "`" + xColumnName0 + "` is null or length(`" + xColumnName0 + "`) = 0";

                        return;
                    }
                    #endregion

                }

                Debugger.Break();
            }
            #endregion



            Debugger.Break();
        }





        // upper select needs us to proxy some fields
        public static void WriteExpressionAlias(
            this CommandBuilderState state,
            // either WhereCommand or SelectCommand
            // byref against locals will likely work better than fields 
            ref string s,
            Expression asExpression,

            IQueryStrategy that
        )
        {
            // asExpression = {nt => new <>f__AnonymousType5`1(AccountToID = nt.Last().t.OtherPartyIBAN)}
            // asExpression = {nt => nt.Last().t.OtherPartyIBAN}
            // asExpression = {nt => 2}

            #region LambdaExpression - upper selector
            var xLambdaExpression = asExpression as LambdaExpression;
            if (xLambdaExpression != null)
            {
                // look the join needs to proxy fields and the upper select isnt selecting scalar..

                // um. we do need to flatten it.

                //var xasNewExpression = xasLambdaExpression.Body as NewExpression;
                // 


                WriteExpressionAlias(
                    state,
                    ref s,
                    xLambdaExpression.Body,
                    that
                );

                return;
            }
            #endregion

            #region xConstantExpression
            var xConstantExpression = asExpression as ConstantExpression;
            if (xConstantExpression != null)
            {
                // lets not try to proxy the constant.
                // the upper select will render it itself
                return;
            }
            #endregion

            // +		asExpression	{nt.Last().t.OtherPartyIBAN}	System.Linq.Expressions.Expression {System.Linq.Expressions.FieldExpression}
            var xMemberExpression = asExpression as MemberExpression;
            if (xMemberExpression != null)
            {
                // scalar field selection?

                s += "\n" + QueryStrategyOfTRowExtensions.CommentLineNumber() + "\t"
                    // shall we look at 'that' and try to understand whats the name of the member?
                    + xMemberExpression.Member.Name + " as " + xMemberExpression.Member.Name;




                return;
            }

            Debugger.Break();

        }
    }
}

