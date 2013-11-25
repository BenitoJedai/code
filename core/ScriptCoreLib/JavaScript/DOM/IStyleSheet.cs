using ScriptCoreLib.JavaScript.Runtime;
using ScriptCoreLib.JavaScript.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ScriptCoreLib.JavaScript.DOM.HTML;

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

            static int __style_id = 0;

            [Obsolete("css")]
            public CSSStyleRule stylerule
            {
                [Script(DefineAsStatic = true)]
                get
                {
                    return css;
                }
            }

            //[Obsolete("experimental, is css better than stylerule? should return a proxy object instead of an actual rule a this point")]
            public CSSStyleRule css
            {
                [Script(DefineAsStatic = true)]
                get
                {
                    return IStyleSheet.all[InternalGetExplicitRuleSelector()];
                }
            }

            [Script(DefineAsStatic = true)]
            internal string InternalGetExplicitRuleSelector()
            {
                //      page.Header.setAttribute("style-id", "45");
                //IStyleSheet.Default[CSSMediaTypes.print][
                //    //"#" + page.Header.id
                //    "[style-id='45']"



                var x = (string)this.getAttribute("style-id");

                if (string.IsNullOrEmpty(x))
                {
                    x = "" + __style_id;

                    __style_id++;
                }

                this.setAttribute("style-id", x);



                return "[style-id='" + x + "']";
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


        public CSSStyleRule this[IHTMLElement e]
        {
            [Script(DefineAsStatic = true)]
            get
            {
                return this[e.InternalGetExplicitRuleSelector()];
            }
        }


        public CSSStyleRule this[ScriptCoreLib.JavaScript.DOM.HTML.IHTMLElement.HTMLElementEnum className]
        {
            [Script(DefineAsStatic = true)]
            get
            {
                var selectorText = "" + className;

                return this.__get_item(selectorText);
            }
        }

        public CSSStyleRule this[string selectorText]
        {
            [Script(DefineAsStatic = true)]
            get
            {
                return this.__get_item(selectorText);
            }
        }


        public CSSMediaRule this[CSSMediaTypes x]
        {
            [Script(DefineAsStatic = true)]
            get
            {
                var selectorText = "@media " + x;

                var value = default(CSSMediaRule);

                try
                {
                    value = (CSSMediaRule)(object)this.__get_item(selectorText);
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
        public static CSSStyleRule __get_item(this IStyleSheet e, string selectorText)
        {
            var a = e.Rules.FirstOrDefault(k => k.selectorText == selectorText);

            if (a == null)
            {
                a = e.AddRule(selectorText);
            }

            return a;
        }

        public static CSSStyleRule __get_item(this CSSMediaRule e, string selectorText)
        {
            // IE not supported?
            var a = e.Rules.FirstOrDefault(k => k.selectorText == selectorText);

            if (a == null)
            {
                //   this.insertRule(selector + "{" + declaration + "}", index);
                var i = e.insertRule(
                    selectorText + " { /**/ }",
                    e.cssRules.Length
                );

                a = e.Rules[i];
            }

            return a;
        }
    }
}
