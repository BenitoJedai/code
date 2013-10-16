using ScriptCoreLib.JavaScript.Runtime;
using ScriptCoreLib.JavaScript.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ScriptCoreLib.JavaScript.DOM.HTML;

namespace ScriptCoreLib.JavaScript.DOM
{
    // see: http://www.w3.org/TR/DOM-Level-2-Style/idl-definitions.html

    public enum CSSRuleTypes
    {
        UNKNOWN_RULE,

        // CSSStyleRule
        STYLE_RULE,
        CHARSET_RULE,
        IMPORT_RULE,
        MEDIA_RULE,
        FONT_FACE_RULE,
        PAGE_RULE
    }

    [Script(InternalConstructor = true)]
    public partial class CSSRule
    {
        public readonly CSSRuleTypes type;

        public readonly IStyleSheet parentStyleSheet;
        public readonly CSSRule parentRule;
    }


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
        }
    }

    [Script(InternalConstructor = true)]
    public partial class CSSStyleRule : CSSRule
    {
        public string selectorText;

        // CSSStyleDeclaration
        public CSSStyleDeclaration style;

        //{ cssText =  } 
        //public string cssText;
    }


    [Script(InternalConstructor = true)]
    public partial class CSSMediaRule : CSSRule
    {
        // http://www.w3.org/TR/1998/REC-CSS2-19980512/media.html#media-types
        public string[] media;

        // Uncaught TypeError: Cannot read property 'cssRules' of undefined 
        // not for chrome??

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



        public long insertRule(string rule, long index)
        {
            return default(long);
        }

        static int __style_id = 0;

        public CSSStyleRule this[IHTMLElement e]
        {
            [Script(DefineAsStatic = true)]
            get
            {
                //      page.Header.setAttribute("style-id", "45");
                //IStyleSheet.Default[CSSMediaTypes.print][
                //    //"#" + page.Header.id
                //    "[style-id='45']"

                var x = (string)e.getAttribute("style-id");

                if (string.IsNullOrEmpty(x))
                {
                    x = "" + __style_id;

                    __style_id++;
                }

                e.setAttribute("style-id", x);


                return this["[style-id='" + x + "']"];

            }
        }
        public CSSStyleRule this[string selectorText]
        {
            [Script(DefineAsStatic = true)]
            get
            {
                return __IStyleSheet.__get_item(this, selectorText);
            }
        }
    }

    // rename to CSSStyleSheet ?
    // too public. collect examples using this type name beforehand
    [Script(InternalConstructor = true)]
    public partial class IStyleSheet
    {
        // http://www.w3.org/TR/DOM-Level-2-Style/css.html

        #region Default
        static IStyleSheet _Default;

        public static IStyleSheet Default
        {
            get
            {
                if (_Default == null)
                    _Default = new IStyleSheet();


                return _Default;
            }
        }
        #endregion


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
            if (Expando.InternalIsMember(this, "insertRule"))
                this.insertRule(selector + "{" + declaration + "}", index);
            else if (Expando.InternalIsMember(this, "addRule"))
                this.addRule(selector, declaration, index);
            else
                throw new System.Exception("fault at IStyleSheetRule.AddRule");


            return this.Rules[index];
        }

        [Script(DefineAsStatic = true)]
        public CSSStyleRule AddRule(string selector)
        {
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

                return (CSSMediaRule)(object)this.__get_item(selectorText);
            }
        }


        #region Owner
        internal DOM.HTML.IHTMLStyle owningElement;
        internal DOM.HTML.IHTMLStyle ownerNode;

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
