using ScriptCoreLib.JavaScript.DOM.HTML;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace ScriptCoreLib.JavaScript.DOM
{
    [Script(InternalConstructor = true)]
    public partial class CSSStyleRule : CSSRule
    {
        // http://css-tricks.com/useful-nth-child-recipies/

        public string selectorText;

        // CSSStyleDeclaration
        public CSSStyleDeclaration style;


        // if we move our
        // extensions to a proxy type
        // then we can delay the decsision to use inline style or rule

        #region print
        [Obsolete("experimental")]
        public CSSStyleRule print
        {
            [Script(DefineAsStatic = true)]
            get
            {
                // tested by
                // X:\jsc.svn\examples\javascript\Test\TestInteractiveStyleRule\TestInteractiveStyleRule\Application.cs

                // android webview gives us trouble
                // revert to a dedicated stylesheet?
                // X:\jsc.svn\examples\javascript\Test\TestCSSPrint\TestCSSPrint\Application.cs

                //return IStyleSheet.print[this.selectorText];

                var p = this.parentStyleSheet[CSSMediaTypes.print];

                if (p == null)
                {
                    Console.WriteLine("creating a disabled style rule as android webview does not know any better?");

                    var x = new IStyleSheet { disabled = true };

                    return x[selectorText];
                }

                return p[selectorText];
            }
        }
        #endregion


        public CSSStyleRule this[ScriptCoreLib.JavaScript.DOM.HTML.IHTMLElement.HTMLElementEnum className]
        {
            [Script(DefineAsStatic = true)]
            get
            {
                // child nodes?
                var selectorText = ">" + className;

                return this[selectorText];
            }
        }

        #region pseudo-classes
        // http://www.w3.org/TR/CSS2/selector.html
        public CSSStyleRule this[string pseudoSelector]
        {
            [Script(DefineAsStatic = true)]
            get
            {
                var x = selectorText + pseudoSelector;
                var p = this.parentRule;
                if (p != null)
                    if (p.type == CSSRuleTypes.MEDIA_RULE)
                        return ((CSSMediaRule)p)[x];


                return this.parentStyleSheet[x];
            }
        }

        // http://www.w3schools.com/cssref/sel_firstchild.asp
        // http://stackoverflow.com/questions/2717480/css-selector-for-first-element-with-class/8539107#8539107
        // jsc can you collect anwsers from users and notify?
        [Obsolete("should we rename this to firstElement or keep css naming? ")]
        public CSSStyleRule firstChild
        {
            [Script(DefineAsStatic = true)]
            get
            {
                return this[">:first-child"];
            }
        }



        /// <summary>
        /// This modifies the first line of ? elements. There's no real DOM element that is selected, that is why it is a pseudo-element.
        /// </summary>
        public CSSStyleRule firstLine
        {
            [Script(DefineAsStatic = true)]
            get
            {

                // http://stackoverflow.com/questions/11359728/what-does-this-operator-in-css-mean
                return this["::first-line"];
            }
        }



        public CSSStyleRule firstLetter
        {
            [Script(DefineAsStatic = true)]
            get
            {

                //http://www.w3.org/TR/selectors/#pseudo-elements
                // X:\jsc.svn\examples\javascript\Test\TestFirstLine\TestFirstLine\Application.cs

                return this["::first-letter"];
            }
        }






        // https://www.w3.org/community/webed/wiki/Advanced_CSS_selectors

        public CSSStyleRule @checked
        {
            [Script(DefineAsStatic = true)]
            get
            {
                return this[":checked"];
            }
        }

        public CSSStyleRule @unchecked
        {
            [Script(DefineAsStatic = true)]
            get
            {
                return this[":not(:checked)"];
            }
        }

        public CSSStyleRule disabled
        {
            [Script(DefineAsStatic = true)]
            get
            {
                return this[":disabled"];
            }
        }

        public CSSStyleRule hover
        {
            [Script(DefineAsStatic = true)]
            get
            {
                return this[":hover"];
            }
        }


        public CSSStyleRule focus
        {
            [Script(DefineAsStatic = true)]
            get
            {
                return this[":focus"];
            }
        }

        public CSSStyleRule before
        {
            [Script(DefineAsStatic = true)]
            get
            {
                return this[":before"];
            }
        }

        public CSSStyleRule after
        {
            [Script(DefineAsStatic = true)]
            get
            {
                return this[":after"];
            }
        }


        /// <summary>
        /// The :empty pseudo-class represents any element that has no children at all. 
        /// Only element nodes and text (including whitespace) are considered. 
        /// Comments or processing instructions do not affect whether an element is considered empty or not.
        /// </summary>
        public CSSStyleRule empty
        {
            [Script(DefineAsStatic = true)]
            get
            {
                return this[":emprty"];
            }
        }

        public CSSStyleRule visited
        {
            [Script(DefineAsStatic = true)]
            get
            {
                return this[":visited"];
            }
        }


        public CSSStyleRule link
        {
            [Script(DefineAsStatic = true)]
            get
            {
                return this[":link"];
            }
        }
        #endregion

        //{ cssText =  } 
        //public string cssText;

        // adjacent-sibling selector

        // http://meyerweb.com/eric/articles/webrev/200007a.html
        public CSSStyleRuleSiblingCombinator adjacentSibling
        {
            [Script(DefineAsStatic = true)]
            get
            {
                return new CSSStyleRuleSiblingCombinator { rule = this };
            }
        }

        // http://www.w3.org/TR/css3-selectors/#general-sibling-combinators
        public CSSStyleRuleSiblingCombinator siblings
        {
            [Script(DefineAsStatic = true)]
            get
            {
                return new CSSStyleRuleSiblingCombinator { rule = this, op = "~" };
            }
        }


        public CSSStyleRule_nthChild nthChild
        {
            [Script(DefineAsStatic = true)]
            get
            {
                return new CSSStyleRule_nthChild { rule = this };
            }
        }

        public string content
        {
            [Script(DefineAsStatic = true)]
            set
            {
                this.style.content = "'" +
                    value
                        .Replace("\\", "\\\\")
                        .Replace("'", "\\'")

                    + "'";

            }
        }


        public new CSSStyleRule this[Expression<Func<IHTMLElement, bool>> f]
        {
            [Script(DefineAsStatic = true)]
            get
            {
                return this[IStyleSheet.GetAttributeSelectorText(f)];
            }
        }
    }

    [Script(InternalConstructor = true)]
    public partial class CSSStyleRule<TElement> : CSSStyleRule where TElement : IHTMLElement
    {
        public new CSSStyleRule this[Expression<Func<TElement, bool>> f]
        {
            [Script(DefineAsStatic = true)]
            get
            {
                return this[IStyleSheet.GetAttributeSelectorText(f)];
            }
        }
    }

    [Script]
    public sealed class CSSStyleRuleSiblingCombinator
    {
        // http://www.w3.org/TR/css3-selectors/#adjacent-sibling-combinators

        public CSSStyleRule rule;

        public string op = "+";

        public CSSStyleDeclaration style
        {
            get
            {
                return this["*"].style;
            }
        }

        public CSSStyleRule this[ScriptCoreLib.JavaScript.DOM.HTML.IHTMLElement.HTMLElementEnum className]
        {
            get
            {
                var selectorText = "" + className;

                return rule[this.op + selectorText];
            }
        }

        public CSSStyleRule this[string selectorText]
        {
            get
            {
                return rule[this.op + selectorText];
            }
        }
    }

    [Script]
    public sealed class CSSStyleRule_nthChild
    {
        public CSSStyleRule rule;

        public CSSStyleRule this[int index]
        {
            get
            {
                return rule[">:nth-child(" + index + ")"];
            }
        }
    }


}
