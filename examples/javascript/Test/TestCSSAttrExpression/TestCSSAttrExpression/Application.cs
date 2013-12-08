using ScriptCoreLib;
using ScriptCoreLib.Delegates;
using ScriptCoreLib.Extensions;
using ScriptCoreLib.JavaScript;
using ScriptCoreLib.JavaScript.Components;
using ScriptCoreLib.JavaScript.DOM;
using ScriptCoreLib.JavaScript.DOM.HTML;
using ScriptCoreLib.JavaScript.Extensions;
using ScriptCoreLib.JavaScript.Windows.Forms;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using TestCSSAttrExpression;
using TestCSSAttrExpression.Design;
using TestCSSAttrExpression.HTML.Pages;

namespace TestCSSAttrExpression
{
    /// <summary>
    /// Your client side code running inside a web browser as JavaScript.
    /// </summary>
    public sealed class Application : ApplicationWebService
    {
        static object by(Expression<Func<IHTMLElement, bool>> f)
        {
            Console.WriteLine(new { f });

            return null;
        }

        static void test()
        {
            var x = by(a => a.title == "foo");

        }

        /// <summary>
        /// This is a javascript application.
        /// </summary>
        /// <param name="page">HTML document rendered by the web server which can now be enhanced.</param>
        public Application(IApp page)
        {
            // https://sites.google.com/a/jsc-solutions.net/backlog/knowledge-base/2013/201312/20131208-expression

            // script: error JSC1000: No implementation found for this native method, please implement 
            // [static System.Linq.Expressions.Expression.Parameter(System.Type, System.String)]

            //            arg[0] is typeof System.RuntimeFieldHandle
            //script: error JSC1000: No implementation found for this native method, please implement [static System.Reflection.FieldInfo.GetFieldFromHandle(System.RuntimeFieldHandle)]

            // script: error JSC1000: No implementation found for this native method, please implement 
            // [static System.Linq.Expressions.Expression.Equal(System.Linq.Expressions.Expression, System.Linq.Expressions.Expression, System.Boolean, System.Reflection.MethodInfo)]

            // script: error JSC1000: No implementation found for this native method, please implement [static System.Reflection.MethodBase.GetMethodFromHandle(System.RuntimeMethodHandle)]

            // Error	2	'string.operator ==(string, string)': cannot explicitly call operator or accessor	X:\jsc.svn\examples\javascript\Test\TestCSSAttrExpression\TestCSSAttrExpression\Application.cs	60	51	TestCSSAttrExpression
            //Func<string, string, bool> x = string.op_Equality;
            //Func<string, string, bool> x = string.Equals;

            ////            { x = [object Object] } view-source:30351
            //// view-source:30351
            //////{ Method = { MethodToken = _9BoABtNdQz66ZYUODttTfw } } 

            //Console.WriteLine(new { x });
            //Console.WriteLine(new { x.Method });

            test();

            var css = this[f: x => x.title == "findme"];

            //page.With(
            //     delegate
            //     {
            //         css.style.color = "cyan";


            //         //set_style_color(x => x.title == "findme", "red");
            //         set_style_color(x => x.title == "findme1", "green");
            //         set_style_color(x => x.title == "findme2", "blue");
            //     }
            //);


            //xx.style.color = "red";
        }

        public CSSStyleRule this[Expression<Func<IHTMLElement, bool>> f]
        {
            get
            {
                var selector = "[title='findme']";

                (f.Body as BinaryExpression).With(
                    equal =>
                    {
                        var right = equal.Right as ConstantExpression;
                        var left = equal.Left as MemberExpression;

                        var Method = equal.Method;

                        // { right = Constant { value = findme, type = [native] String }, 
                        // left = MemberExpression { expression = { type = [native] IHTMLElement, name = x }, field = title }, 
                        // Method = { MethodToken = __bRoABtNdQz66ZYUODttTfw } }


                        Expression<Func<string, string, bool>> __String_op_Equality = (y, z) => y == z;

                        //{ Value = findme, Member = title, Method = { MethodToken = __bxoABtNdQz66ZYUODttTfw }, 
                        // __String_op_Equality = { 
                        //  Body = BinaryExpression { 
                        //      left = ParameterExpression { type = [native] String, name = y },
                        //      right = ParameterExpression { type = [native] String, name = z },
                        //      liftToNull = 0,
                        //      method = { MethodToken = __bxoABtNdQz66ZYUODttTfw } 
                        // }, 
                        // parameters = ParameterExpression { type = [native] String, name = y },ParameterExpression { type = [native] String, name = z } } }

                        var __String_op_Equality_method = ((BinaryExpression)__String_op_Equality.Body).Method;

                        var e = equal.Method == __String_op_Equality_method;

                        //new IHTMLPre
                        //{
                        //    innerText = new { right.Value, left.Member, e, Method, __String_op_Equality_method }.ToString()
                        //}.AttachToDocument();

                        // { Value = findme, Member = title, e = true, Method = { MethodToken = ARsABtNdQz66ZYUODttTfw }, __String_op_Equality_method = { MethodToken = ARsABtNdQz66ZYUODttTfw } }

                        if (e)
                        {
                            selector = "[" + left.Member + "='" + right.Value + "']";

                        }
                    }
                );

                var x = IStyleSheet.all[selector];



                return x;
            }
        }

        public static void set_style_color(Expression<Func<IHTMLElement, bool>> f, string value)
        {
            new IHTMLPre
            {
                innerText = new { f }.ToString()
            }.AttachToDocument();

            // { f = 
            // Lambda { body =
            //
            //          Equal 
            //          { left = 
            //                      { expression = 
            //                              { type = [native] IHTMLElement, 
            //                                name = x }, 
            //                        field = title }, 
            //
            //            right =   {   value = findme, 
            //                          type = [native] String }, 
            //       liftToNull = 0, 
            //           method =   { MethodToken = _8xoABtNdQz66ZYUODttTfw } 
            // }, 
            // parameters = { 
            //      type = [native] IHTMLElement, 
            //      name = x } 
            // } }

            var selector = "[title='findme']";

            (f.Body as BinaryExpression).With(
                equal =>
                {
                    var right = equal.Right as ConstantExpression;
                    var left = equal.Left as MemberExpression;

                    var Method = equal.Method;

                    // { right = Constant { value = findme, type = [native] String }, 
                    // left = MemberExpression { expression = { type = [native] IHTMLElement, name = x }, field = title }, 
                    // Method = { MethodToken = __bRoABtNdQz66ZYUODttTfw } }


                    Expression<Func<string, string, bool>> __String_op_Equality = (y, z) => y == z;

                    //{ Value = findme, Member = title, Method = { MethodToken = __bxoABtNdQz66ZYUODttTfw }, 
                    // __String_op_Equality = { 
                    //  Body = BinaryExpression { 
                    //      left = ParameterExpression { type = [native] String, name = y },
                    //      right = ParameterExpression { type = [native] String, name = z },
                    //      liftToNull = 0,
                    //      method = { MethodToken = __bxoABtNdQz66ZYUODttTfw } 
                    // }, 
                    // parameters = ParameterExpression { type = [native] String, name = y },ParameterExpression { type = [native] String, name = z } } }

                    var __String_op_Equality_method = ((BinaryExpression)__String_op_Equality.Body).Method;

                    var e = equal.Method == __String_op_Equality_method;

                    new IHTMLPre
                            {
                                innerText = new { right.Value, left.Member, e, Method, __String_op_Equality_method }.ToString()
                            }.AttachToDocument();

                    // { Value = findme, Member = title, e = true, Method = { MethodToken = ARsABtNdQz66ZYUODttTfw }, __String_op_Equality_method = { MethodToken = ARsABtNdQz66ZYUODttTfw } }

                    if (e)
                    {
                        selector = "[" + left.Member + "='" + right.Value + "']";

                    }
                }
            );

            var x = IStyleSheet.all[selector];



            x.style.color = value;
        }
    }
}
