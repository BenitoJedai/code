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
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using CSSnthSelectorByExpression;
using CSSnthSelectorByExpression.Design;
using CSSnthSelectorByExpression.HTML.Pages;
using System.Linq.Expressions;
using System.Reflection;
using ScriptCoreLib.JavaScript.Runtime;
using System.Diagnostics;

namespace CSSnthSelectorByExpression
{
    /// <summary>
    /// Your client side code running inside a web browser as JavaScript.
    /// </summary>
    public sealed class Application : ApplicationWebService
    {
        /// <summary>
        /// This is a javascript application.
        /// </summary>
        /// <param name="page">HTML document rendered by the web server which can now be enhanced.</param>
        public Application(IApp page)
        {
            var spans = page.Header.innerText.ToCharArray().Select(
                  c => (IHTMLSpan)c
              ).ToArray();

            page.Header.Clear();
            spans.AsEnumerable().AttachTo(page.Header);

            var yindexer = new IndexBox { index = 3 };

            page.Header.css[yindexer.index].style.backgroundColor = "cyan";

            foo(yindexer);


            //yindexer.index = 7;
            var yindexer_index = 7;

            //var nth = page.Header.css.nthChild(() => yindexer.index);
            //var nth = page.Header.css.nthChild(() => yindexer_index);
            var nth = page.Header.css[() => yindexer_index];

            nth.style.backgroundColor = "red";
            nth.hover.style.backgroundColor = "yellow";
            nth.hover.before.content = "<hover>";
            nth.hover.after.content = "</hover>";


            new IHTMLButton { "+" }.AttachToDocument().WhenClicked(
                delegate
                {
                    //yindexer.index++;
                    yindexer_index++;
                }
            );

            new IHTMLButton { "-" }.AttachToDocument().WhenClicked(
             delegate
             {
                 //yindexer.index--;
                 yindexer_index--;
             }
         );
        }

        public static void xss(Expression<Func<int>> yindexer)
        {

            // { yindexer = { 

            // Body = MemberExpression { 
            //      expression = MemberExpression { 
            //          expression = Constant { 

            //              value = [object Object], 

            //              type =  
            //          }, 

            //          field = arg0 

            //      }, 
            //      field = index 
            //  }, 

            //  parameters =  

            //  } } 

            Console.WriteLine(
                new { yindexer }
            );

            #region i4
            Func<int> i4 = delegate
          {
              var i4_value = 0;

              (yindexer as LambdaExpression).With(
                  lambda =>
                  {
                      (lambda.Body as MemberExpression).With(
                          member =>
                          {
                              var field0 = member.Member as FieldInfo;

                              // { field = index } 
                              Console.WriteLine(new { field0 });

                              (member.Expression as MemberExpression).With(
                                  x =>
                                  {
                                      var field1 = x.Member as FieldInfo;

                                      (x.Expression as ConstantExpression).With(
                                          constant =>
                                          {
                                              var value = constant.Value;

                                              //{ field = index } view-source:30891
                                              //{ value = [object Object] } 

                                              Console.WriteLine(new { value });


                                              var field1_value = field1.GetValue(value);

                                              // { MemberName = index } 

                                              //Console.WriteLine(new { field_value });


                                              //Expando.Of(field_value).GetMemberNames().WithEach(
                                              //       MemberName =>
                                              //       {
                                              //           Console.WriteLine(new { MemberName });
                                              //       }
                                              //   );

                                              var field0_value = field0.GetValue(field1_value);

                                              i4_value = (int)field0_value;
                                              //Console.WriteLine(new { field0_value });

                                              //{ field0_value = 3 } 
                                          }
                                      );
                                  }
                              );



                          }
                      );
                  }
              );

              return i4_value;
          };
            #endregion

            var w = new Stopwatch();
            w.Start();
            var i = i4();

            w.Stop();

            // { i = 3, ElapsedMilliseconds = 2 } 
            Console.WriteLine(
                new { i, w.ElapsedMilliseconds }
            );


            //{ i = 3 } 
        }

        public static void foo(IndexBox arg0)
        {
            xss(() => arg0.index);
        }



        public sealed class IndexBox
        {
            public int index;
        }
    }

    public static class X
    {
        public static CSSStyleRuleMonkier nthChild(this CSSStyleRuleMonkier rule, Expression<Func<int>> yindexer, bool bind = true)
        {
            Console.WriteLine(
     new { yindexer }
 );

            #region i4
            Func<int> i4 = delegate
            {
                var i4_value = 0;

                var w = new Stopwatch();
                w.Start();

                #region LambdaExpression
                (yindexer as LambdaExpression).With(
                    lambda =>
                    {
                        (lambda.Body as MemberExpression).With(
                            member =>
                            {
                                // { yindexer = { Body = MemberExpression { expression = Constant { value = [object Object], type =  }, field = yindexer_index }, parameters =  } } 

                                var member_field0 = member.Member as FieldInfo;

                                var member_constant0 = member.Expression as ConstantExpression;
                                if (member_constant0 != null)
                                {
                                    var field0_value = member_field0.GetValue(member_constant0.Value);
                                    i4_value = (int)field0_value;
                                }

                                var membermember = member.Expression as MemberExpression;
                                if (membermember != null)
                                {
                                    var membermember_field1 = membermember.Member as FieldInfo;
                                    var membermember_constant = membermember.Expression as ConstantExpression;

                                    if (membermember_constant != null)
                                    {
                                        var field1_value = membermember_field1.GetValue(membermember_constant.Value);
                                        var field0_value = member_field0.GetValue(field1_value);

                                        i4_value = (int)field0_value;

                                    }
                                }




                            }
                        );
                    }
                );
                #endregion

                w.Stop();

                return i4_value;
            };
            #endregion


            var i = i4();

            // http://www.w3schools.com/cssref/sel_nth-child.asp
            //var selectorText = ">:nth-child(" + i + ")";
            var rp = rule[i];

            if (bind)
            {
                Native.window.onframe += delegate
                {
                    var ii = i4();
                    if (ii == rp.index)
                        return;

                    rp.index = ii;
                };
            }

            //return rvalue;

            return rp;
        }
    }
}
