using ScriptCoreLib.JavaScript.Runtime;
using ScriptCoreLib.JavaScript.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ScriptCoreLib.JavaScript.DOM
{
    // see: http://www.w3.org/TR/DOM-Level-2-Style/idl-definitions.html

    [Script(InternalConstructor = true)]
    public partial class IStyleSheetRule
    {
        public string selectorText;
        public IStyle style;

    }

    [Script(InternalConstructor = true)]
    public partial class IStyleSheet
    {
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

        public readonly string media;
        public bool disabled;

        internal IStyleSheetRule[] rules;
        internal IStyleSheetRule[] cssRules;

        public IStyleSheetRule[] Rules
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


        #region Constructor

        public IStyleSheet()
        {
            // InternalConstructor
        }

        static IStyleSheet InternalConstructor()
        {
            HTML.IHTMLStyle s = new HTML.IHTMLStyle();

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
        public IStyleSheetRule AddRule(string selector, string declaration, int index)
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
        public IStyleSheetRule AddRule(string selector)
        {
            return AddRule(selector, "/**/", this.Rules.Length);
        }

        [Script(DefineAsStatic = true)]
        public IStyleSheetRule AddRule(global::System.Collections.Generic.KeyValuePair<string, System.Action<IStyleSheetRule>> r)
        {

            return this.AddRule(r.Key, r.Value);

        }

        [Script(DefineAsStatic = true)]
        public IStyleSheetRule AddRule(string selector, System.Action<IStyleSheetRule> r)
        {
            var x = AddRule(selector);

            r(x);

            return x;
        }

        public IStyleSheetRule this[string selectorText]
        {
            [Script(DefineAsStatic = true)]
            get
            {
                return this.__get_item(selectorText);
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

    [Script]
    internal static class __IStyleSheet
    {
        public static IStyleSheetRule __get_item(this IStyleSheet e, string selectorText)
        {
            var a = e.Rules.FirstOrDefault(k => k.selectorText == selectorText);

            if (a == null)
            {
                a = e.AddRule(selectorText);
            }

            return a;
        }
    }
}
