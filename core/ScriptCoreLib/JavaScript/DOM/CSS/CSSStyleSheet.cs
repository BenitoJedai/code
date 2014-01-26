using ScriptCoreLib.JavaScript.Runtime;
using ScriptCoreLib.JavaScript.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ScriptCoreLib.JavaScript.DOM.HTML;
using System.Linq.Expressions;

namespace ScriptCoreLib.JavaScript.DOM
{


    // http://www.w3.org/TR/DOM-Level-2-Style/css.html#CSS-CSSStyleDeclaration
    // http://dev.w3.org/csswg/cssom/
    // http://dev.w3.org/csswg/cssom/#cssstyledeclaration
    [Script(InternalConstructor = true)]
    public partial class CSSStyleDeclaration
        // . Furthermore, implementations that support a specific level of CSS 
        // should correctly handle CSS shorthand properties for that level. 
        : IStyle
    {
        // does this work?
        // The cssText attribute must return the result of serializing the declarations.
        public readonly string cssText;

        // this is not usually set?
        public readonly CSSRule parentRule;

        // Example: styleObj.setProperty('color', 'red', 'important')
        public void setProperty(string propertyName,
                                string value,
                                string priority) { }
    }

    namespace HTML
    {
        public /* abstract */ partial class IHTMLElement
        {

            //   [SameObject, PutForwards=cssText] readonly attribute CSSStyleDeclaration style;
            public readonly CSSStyleDeclaration style;


            // should we use className instead?
            static int __style_id = 0;

            //[Obsolete("css")]
            //public CSSStyleRule stylerule
            //{
            //    [Script(DefineAsStatic = true)]
            //    get
            //    {
            //        return css;
            //    }
            //}

            //[Obsolete("experimental, is css better than stylerule? should return a proxy object instead of an actual rule a this point")]

            public CSSStyleRuleMonkier css
            {
                // could we send the monkier to web worker for additional manipulation?
                [Script(DefineAsStatic = true)]
                get
                {
                    var selectorText = InternalGetExplicitRuleSelector();

                    //Console.WriteLine(".css " + new { selectorText });

                    // how fast is the selection?
                    var value = IStyleSheet.all[selectorText];

                    value.selectorElement = this;

                    //Console.WriteLine(".css " + new { value, @this = this });

                    //32ms css.style { selectorText = input[style-id="0"]:checked ~ div:nth-of-type(1), input[style-id="1"]:checked ~ div:nth-of-type(1) } view-source:34910

                    // view-source:34910
                    //33ms { right_withElement = { selectorText = input[style-id="1"], selectorElement =  } } view-source:34910

                    // view-source:34910
                    //34ms { right = { selectorText = input[style-id="1"]:checked, selectorElement =  } } 

                    // should we cache the monkier on the element?
                    return value;
                }
            }

            [Script(DefineAsStatic = true)]
            internal string InternalGetExplicitRuleSelector()
            {
                //      page.Header.setAttribute("style-id", "45");
                //IStyleSheet.Default[CSSMediaTypes.print][
                //    //"#" + page.Header.id
                //    "[style-id='45']"


                IHTMLElement that = this;

                if (that == null)
                    throw new InvalidOperationException();



                #region style-id
                var x = (string)that.getAttribute("style-id");


                if (string.IsNullOrEmpty(x))
                {
                    // https://sites.google.com/a/jsc-solutions.net/backlog/knowledge-base/2014/201401/20140125/speed
                    // localName: "div"

                    //x = this.tagName + __style_id;
                    // localName: "div"
                    // tagName: "DIV"
                    // className: ""
                    // lassList: DOMTokenList

                    // are we building an Expression<> here?
                    //x = this.localName + "." + this.classList.FirstOrDefault() + "[" + __style_id + "]";
                    x = "" + __style_id;
                    this.setAttribute("style-id", x);

                    // 95ms css.style { selectorText = [style-id="table.__ContentTable[2]"] > tbody > tr > td } 

                    __style_id++;
                }
                #endregion


                #region selectorText
                var selectorText = this.localName;

                // looks like cookie api access. lets talk to only the primary class thp
                var className = this.classList.FirstOrDefault();
                if (className != null)
                {
                    // do we need to do css escape?
                    selectorText += "." + className;
                }

                selectorText += "[style-id='" + x + "']";
                #endregion


                return selectorText;
            }
        }
    }



    // rename to CSSStyleSheet ?
    // too public. collect examples using this type name beforehand

    [Script(InternalConstructor = true)]
    public partial class CSSStyleSheet
    {
        // http://www.w3.org/TR/DOM-Level-2-Style/idl-definitions.html
    }

    [Script(InternalConstructor = true)]
    [Obsolete("CSSStyleSheet")]
    public partial class IStyleSheet : CSSStyleSheet
    {
        // http://www.w3.org/TR/DOM-Level-2-Style/css.html



        #region print
        static IStyleSheet _print;
        public static IStyleSheet print
        {
            get
            {
                // does this work for android webview?
                if (_print == null)
                {
                    // X:\jsc.svn\examples\javascript\Test\TestCSSPrint\TestCSSPrint\Application.cs

                    _print = new IStyleSheet();

                    // android webview does not respect this
                    _print.Owner.media = "print";
                }

                return _print;
            }
        }
        #endregion

        #region all
        static IStyleSheet _all;
        public static IStyleSheet all
        {
            get
            {
                if (_all == null)
                    _all = new IStyleSheet();

                return _all;
            }
        }
        #endregion

        [Obsolete("all")]
        public static IStyleSheet Default
        {
            get
            {
                return all;
            }
        }


        public readonly string media;
        public bool disabled;

        #region Rules
        internal CSSStyleRule[] rules;
        internal CSSStyleRule[] cssRules;

        public CSSStyleRule[] Rules
        {
            [Script(DefineAsStatic = true)]
            get
            {


                if (Expando.InternalIsMember(this, "cssRules"))
                    return this.cssRules;

                if (Expando.InternalIsMember(this, "rules"))
                    return this.rules;

                throw new System.Exception("member IStyleSheet.Rules not found");
            }
        }
        #endregion



        #region Constructor

        public IStyleSheet()
        {
            // InternalConstructor
        }

        static IStyleSheet InternalConstructor()
        {
            var s = new HTML.IHTMLStyle();

            // http://phrogz.net/JS/AddCSS_test.html

            var h = Native.Document.getElementsByTagName("head");

            if (h.Length > 0)
                h[0].appendChild(s);
            else
                s.AttachToDocument();

            return s.StyleSheet;
        }

        #endregion

        #region RemoveRule
        internal void removeRule(int i)
        {
        }

        internal void deleteRule(int i)
        {
        }

        [Script(DefineAsStatic = true)]
        public void RemoveRule(int index)
        {
            if (Expando.InternalIsMember(this, "removeRule"))
                this.removeRule(index);
            else if (Expando.InternalIsMember(this, "deleteRule"))
                this.deleteRule(index);
            else
                throw new System.NotSupportedException("RemoveRule");

        }
        #endregion

        #region AddRule
        internal object addRule(string s, string d, int i)
        {
            return null;
        }

        internal object insertRule(string r, int i)
        {
            return null;
        }



        // http://www.susaaland.dk/sharedoc/kdelibs-devel-3/khtml/html/classDOM_1_1CSSStyleSheet.html#a9
        // http://www.javascriptkit.com/domref/stylesheet.shtml
        [Script(DefineAsStatic = true)]
        public CSSStyleRule AddRule(string selector, string declaration, int index)
        {
            // https://developer.mozilla.org/en-US/docs/Web/CSS/@media
            // http://davidwalsh.name/add-rules-stylesheets

            if (Expando.InternalIsMember(this, "insertRule"))
            {
                // I/Web Console(32117): IStyleSheetRule.AddRule error { text = @media print{/**/} }


                var text = selector + "{" + declaration + "}";




                try
                {
                    this.insertRule(text, index);
                }
                catch
                {
                    // tested by
                    // X:\jsc.svn\examples\javascript\Test\TestCSSPrint\TestCSSPrint\Application.cs


                    Console.WriteLine("IStyleSheetRule.AddRule error " + new { text });
                    throw;
                }

                return this.Rules[index];
            }

            if (Expando.InternalIsMember(this, "addRule"))
            {
                this.addRule(selector, declaration, index);
                return this.Rules[index];
            }




            throw new System.Exception("fault at IStyleSheetRule.AddRule");


        }



        [Script(DefineAsStatic = true)]
        public CSSFontFaceRule AddFontFaceRule(string fontFamily, string src)
        {
            // tested by 
            // X:\jsc.svn\examples\javascript\css\CSSFontFaceExperiment\CSSFontFaceExperiment\Application.cs

            var r = this.AddRule("@font-face");

            r.style.setProperty("font-family", fontFamily, "");
            r.style.setProperty("src", "url('" + src + "')", "");

            // crude cast
            return (CSSFontFaceRule)(object)r;
        }

        [Script(DefineAsStatic = true)]
        public CSSStyleRule AddRule(string selector)
        {
            // does webview support this?
            return AddRule(selector, "/**/", this.Rules.Length);
        }

        [Script(DefineAsStatic = true)]
        public CSSStyleRule AddRule(global::System.Collections.Generic.KeyValuePair<string, System.Action<CSSStyleRule>> r)
        {

            return this.AddRule(r.Key, r.Value);

        }

        [Script(DefineAsStatic = true)]
        public CSSStyleRule AddRule(string selector, System.Action<CSSStyleRule> r)
        {
            var x = AddRule(selector);

            r(x);

            return x;
        }
        #endregion





        public CSSStyleRuleMonkier this[Type t]
        {
            [Script(DefineAsStatic = true)]
            get
            {
                // child nodes?
                var selectorText = "." + t.Name;

                // https://sites.google.com/a/jsc-solutions.net/backlog/knowledge-base/2014/201401/20140124
                var z = this[selectorText];

                // this is like type of nth?
                // dont know
                //z.nthChildInlineMode = true;

                return z;
            }
        }

        public CSSStyleRuleMonkier this[IHTMLElement e]
        {
            [Script(DefineAsStatic = true)]
            get
            {
                return this[e.InternalGetExplicitRuleSelector()];
            }
        }


        public CSSStyleRuleMonkier this[ScriptCoreLib.JavaScript.DOM.HTML.IHTMLElement.HTMLElementEnum className]
        {
            [Script(DefineAsStatic = true)]
            get
            {
                var selectorText = "" + className;

                return this.__get_item(selectorText);
            }
        }

        [Obsolete("assigned by InternalApplicationBootstrap since ScriptCoreLib does not simplfy code yet")]
        public static Expression<Func<string, string, bool>> __String_op_Equality;
        //public static Expression<Func<string, string, bool>> __String_op_Equality = (y, z) => y == z;

        public CSSStyleRuleMonkier this[Expression<Func<IHTMLElement, bool>> f]
        {
            [Script(DefineAsStatic = true)]
            get
            {
                return this[GetAttributeSelectorText<IHTMLElement>(f)];
            }
        }

        //TElement> : CSSStyleRule where TElement : IHTMLElement
        //public static string GetAttributeSelectorText(Expression<Func<IHTMLElement, bool>> f)
        public static string GetAttributeSelectorText<TElement>(Expression<Func<TElement, bool>> f)
         where TElement : IHTMLElement
        {
            // X:\jsc.svn\examples\javascript\Test\TestCSSAttrExpression\TestCSSAttrExpression\Application.cs
            // https://sites.google.com/a/jsc-solutions.net/backlog/knowledge-base/2013/201312/20131208-expression
            var right_value = "";

            var selector = "[title='?']";

            var equal = (f.Body as BinaryExpression);
            {
                #region right_value
                var right_Constant = equal.Right as ConstantExpression;
                var right_Member = equal.Right as MemberExpression;

                if (right_Constant != null)
                    right_value = global::System.Convert.ToString(right_Constant.Value);

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

                        right_value = global::System.Convert.ToString(
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
                    selector = "[" + left.Member + "='" +

                        right_value
                             .Replace("\\", "\\\\")
                            .Replace("'", "\\'")

                        + "']";

                }
            }
            return selector;
        }


        public CSSStyleRuleMonkier this[string selectorText]
        {
            [Script(DefineAsStatic = true)]
            get
            {
                return this.__get_item(selectorText);
            }
        }


        public CSSStyleRuleMonkier this[CSSMediaTypes x]
        {
            [Script(DefineAsStatic = true)]
            get
            {
                var selectorText = "@media " + x;

                var value = default(CSSStyleRuleMonkier);

                try
                {
                    value = (CSSStyleRuleMonkier)(object)this.__get_item(selectorText);
                }
                catch
                {
                    // android webview does not understand media
                }

                return value;
            }
        }


        #region Owner
        internal DOM.HTML.IHTMLStyle owningElement;
        internal DOM.HTML.IHTMLStyle ownerNode;

        [Obsolete("rename to Node?")]
        public DOM.HTML.IHTMLStyle Owner
        {
            [Script(DefineAsStatic = true)]
            get
            {
                if (Expando.InternalIsMember(this, "ownerNode"))
                    return this.ownerNode;

                if (Expando.InternalIsMember(this, "owningElement"))
                    return this.owningElement;

                throw new System.Exception("fault at IStyleSheet.Owner");
            }
        }
        #endregion
    }

    // http://www.w3.org/TR/1998/REC-CSS2-19980512/media.html#at-media-rule
    [Script(IsStringEnum = true)]
    public enum CSSMediaTypes
    //: string
    {
        all,
        print,
        screen,
        tv
    }

    [Script]
    internal static class __IStyleSheet
    {
        public static CSSStyleRuleMonkier __get_item(this IStyleSheet e, string selectorText)
        {
            var rule = e.Rules.FirstOrDefault(k => k.selectorText == selectorText);

            if (rule == null)
            {
                rule = e.AddRule(selectorText);
            }

            return rule;
        }

        public static CSSStyleRuleMonkier __get_item(this CSSMediaRule e, string selectorText)
        {
            // IE not supported?
            var rule = e.Rules.FirstOrDefault(k => k.selectorText == selectorText);

            if (rule == null)
            {
                //   this.insertRule(selector + "{" + declaration + "}", index);
                var i = e.insertRule(
                    selectorText + " { /**/ }",
                    e.cssRules.Length
                );

                rule = e.Rules[i];
            }

            return rule;
        }
    }
}
