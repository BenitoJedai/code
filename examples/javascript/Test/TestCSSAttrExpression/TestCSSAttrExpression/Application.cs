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
using System.Reflection;
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
            // https://sites.google.com/a/jsc-solutions.net/backlog/knowledge-base/2013/201312/20131208-expression

            var findme1_text = "findme1_text";
            var findme1_number = 1;
            var findme1 = findme1_text + findme1_number;



            //Parameter { type = [native] IHTMLElement, name = a }
            //Field { expression = ParameterExpression { type = [native] IHTMLElement, name = a }, field = title }

            //Constant { value = [object Object] }
            //Field { expression = Constant { value = [object Object], type =  }, field = findme1 }

            //Equal { left = MemberExpression { expression = ParameterExpression { type = [native] IHTMLElement, name = a }, field = title }, right = MemberExpression { expression = Constant { value = [object Object], type =  }, field = findme1 }, liftToNull = 0, method = { MethodToken = AxsABtNdQz66ZYUODttTfw } }
            //Lambda { body = BinaryExpression { left = MemberExpression { expression = ParameterExpression { type = [native] IHTMLElement,
            // name = a },
            // field = title },
            // right = MemberExpression { expression = Constant { value = [object Object],
            // type =  },
            // field = findme1 },
            // liftToNull = 0,
            // method = { MethodToken = AxsABtNdQz66ZYUODttTfw } }, parameters = ParameterExpression { type = [native] IHTMLElement, name = a } }

            //{ f = { Body = BinaryExpression { left = MemberExpression { expression = ParameterExpression { type = [native] IHTMLElement,
            // name = a },
            // field = title },
            // right = MemberExpression { expression = Constant { value = [object Object],
            // type =  },
            // field = findme1 },
            // liftToNull = 0,
            // method = { MethodToken = AxsABtNdQz66ZYUODttTfw } }, parameters = ParameterExpression { type = [native] IHTMLElement, name = a } } } 

            var x = by(a => a.title == findme1);

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
            //return;

            //var css = this[f: x => x.title == "findme"];
            //css.style.color = "cyan";

            var findme1_text = "findme";
            var findme1_number = 1;

            // field.const
            var findme1 = findme1_text + findme1_number;

            this[f: x => x.title == findme1].style.color = "red";

            var findme3 = findme1_text + "3";


            IStyleSheet.all[x => x.title == findme3].style.color = "purple";
            IStyleSheet.all[x => x.title == findme3].style.backgroundColor = "yellow";


            this[x => x.title == "findme2"].style.color = "blue";

            //css.style.color = "cyan";
            page.With(
                 async delegate
                 {
                     //css.style.color = "cyan";


                     ////set_style_color(x => x.title == "findme", "red");
                     //set_style_color(x => x.title == "findme1", "green");
                     //set_style_color(x => x.title == "findme2", "blue");


                     this[x => x.title == "findme2"].style.color = "blue";

                     await Task.Delay(200);

                     this[x => x.title == "findme2"].style.backgroundColor = "yellow";


                 }
            );


            page.foo.css[input => input.value == "foo"].style.color = "red";

            //xx.style.color = "red";
        }

        public CSSStyleRuleMonkier this[Expression<Func<IHTMLElement, bool>> f]
        {
            get
            {
                // X:\jsc.svn\examples\javascript\Test\TestCSSAttrExpression\TestCSSAttrExpression\Application.cs
                // https://sites.google.com/a/jsc-solutions.net/backlog/knowledge-base/2013/201312/20131208-expression
                var right_value = "";

                var selector = "[title='?']";

                (f.Body as BinaryExpression).With(
                    equal =>
                    {
                        #region right_value
                        var right_Constant = equal.Right as ConstantExpression;
                        var right_Member = equal.Right as MemberExpression;

                        if (right_Constant != null)
                            right_value = Convert.ToString(right_Constant.Value);

                        // { right_Constant = , right_Member =
                        // MemberExpression { 
                        //      expression = Constant { value = [object Object], type =  }, 
                        // field = findme1 } }

                        //Console.WriteLine(new { right_Constant, right_Member });

                        if (right_Member != null)
                        {
                            var right_Member_Constant = right_Member.Expression as ConstantExpression;


                            if (right_Member_Constant != null)
                            {
                                var ff = right_Member_Constant.Value.GetType().GetField(
                                    right_Member.Member.Name
                                );

                                right_value = Convert.ToString(
                                    ff.GetValue(right_Member_Constant.Value)
                                    );

                            }
                        }
                        #endregion

                        //Console.WriteLine(new { right_Constant, right_Member, right_value });

                        //return;

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
                            selector = "[" + left.Member + "='" + right_value + "']";

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
