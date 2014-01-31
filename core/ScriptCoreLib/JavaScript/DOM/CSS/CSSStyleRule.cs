using ScriptCoreLib.JavaScript.DOM.HTML;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using System.Xml.Linq;

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






        //{ cssText =  } 
        //public string cssText;





    }

    [Obsolete("are we using this?")]
    [Script(InternalConstructor = true)]
    public partial class CSSStyleRule<TElement> : CSSStyleRuleMonkier where TElement : IHTMLElement
    {
        public new CSSStyleRuleMonkier this[Expression<Func<TElement, bool>> f]
        {
            [Script(DefineAsStatic = true)]
            get
            {
                return this[IStyleSheet.GetAttributeSelectorText(f)];
            }
        }
    }



    [Script]
    public class CSSStyleRuleProxy
    {
        // since creating actual rules is slow.
        // we need to refrain from creating in flight rules
        // and wait the .style property to be accessed to build it


        protected CSSStyleRule __rule;

        public string selectorText;


        public CSSStyleDeclaration style
        {
            get
            {
                return ((CSSStyleRule)this).style;
            }
        }

        internal IStyleSheet __parentStyleSheet;

        public IStyleSheet parentStyleSheet
        {
            get
            {
                if (__parentStyleSheet == null)
                {
                    __parentStyleSheet = ((CSSStyleRule)this).parentStyleSheet;
                }

                return __parentStyleSheet;
            }
        }

        #region pseudo-classes
        // http://www.w3.org/TR/CSS2/selector.html
        public CSSStyleRuleProxy this[string pseudoSelector]
        {
            [Script(DefineAsStatic = true)]
            get
            {
                var x = this.selectorText + pseudoSelector;

                if (this.__rule == null)
                    return new CSSStyleRuleProxy
                    {
                        selectorText = x,

                        // remember our stylesheet
                        __parentStyleSheet = __parentStyleSheet
                    };



                // no longer a proxy, are we a media rule?

                var p = this.__rule.parentRule;
                if (p != null)
                    if (p.type == CSSRuleTypes.MEDIA_RULE)
                        return ((CSSMediaRule)p)[x].rule;

                // when, how oftern is this used?
                return this.__rule.parentStyleSheet[x].rule;



            }
        }
        #endregion


        public static implicit operator CSSStyleRuleProxy(CSSStyleRule rule)
        {
            return new CSSStyleRuleProxy { __rule = rule };
        }

        public static explicit operator CSSStyleRule(CSSStyleRuleProxy rule)
        {
            if (rule.__rule == null)
            {

                // its time to build the rule!
                rule.__rule = rule.__parentStyleSheet.AddRule(rule.selectorText);

            }

            return rule.__rule;
        }
    }

    [Obsolete("is it a good name?")]
    [Script]
    public class CSSStyleRuleMonkier
    {
        //[Script]
        //public class PseudoSelector : CSSStyleRuleMonkier
        //{
        //    // allows us to check if this vesion of the monkier is a pseudo selector
        //    // this wont work if the selector is changed in the fly tho
        //}

        //[Obsolete("when can we do 'x is pseudo selector', c# 6?")]
        //public bool IsPseudoSelector
        //{
        //    get
        //    {
        //        // 27ms looking at { selectorText = input[style-id="1"]:checked ~ input:nth-of-type(2):checked, IsPseudoSelector = true, selectorElement =  } 

        //        if (this.selectorText == null)
        //            return false;

        //        // :nth-of-type(2)
        //        // is not a pseudo selector in that sense, since it selects another element

        //        if (this.selectorText == ":checked")
        //            return true;

        //        if (this.selectorText == ":hover")
        //            return true;

        //        return false;
        //    }
        //}


        // http://www.w3schools.com/cssref/sel_target.asp
        //public IHTMLElement target;

        #region rule
        public CSSStyleRuleProxy rule;

        public static implicit operator CSSStyleRuleMonkier(CSSStyleRuleProxy rule)
        {
            return new CSSStyleRuleMonkier { rule = rule };
        }

        public static implicit operator CSSStyleRuleMonkier(CSSStyleRule rule)
        {
            return (CSSStyleRuleProxy)rule;
        }
        #endregion



        #region [selectorText]
        public CSSStyleRuleMonkier this[string selectorText]
        {
            get
            {
                #region regroup
                if (this.parents.Count > 0)
                {
                    // 32ms { withoutPseudoSelector = 
                    // { selectorText = input[style-id="1"]:checked ~ input:nth-of-type(2), selectorElement = [object HTMLInputElement] } }
                    // 33ms looking at 
                    // { selectorText = input[style-id="1"]:checked ~ input:nth-of-type(2):checked, IsPseudoSelector = true, selectorElement =  }
                    // 33ms { withoutpseudo = 
                    // { selectorText = input[style-id="1"]:checked ~ input:nth-of-type(2), selectorElement =  } } 

                    Console.WriteLine("css regroup " + new { this.parents.Count, selectorText });

                    var p = default(CSSStyleRuleMonkier);

                    foreach (var item in this.parents)
                    {
                        var pp = item[selectorText];
                        p |= pp;
                    }

                    return p;
                }
                #endregion

                var child = new CSSStyleRuleMonkier
                {
                    parent = this,
                    selectorText = selectorText
                };

                return child;
            }
        }
        #endregion


        public CSSStyleDeclaration style
        {
            get
            {
                // https://sites.google.com/a/jsc-solutions.net/backlog/knowledge-base/2014/201401/20140131

                //Console.WriteLine("css.style " + new { this.rule.selectorText });


                return this.rule.style;
            }
        }



        public new CSSStyleRuleMonkier this[XAttribute x]
        {
            get
            {
                // X:\jsc.svn\examples\javascript\CSS\CSSXAttributeAsConditional\CSSXAttributeAsConditional\Application.cs


                var selector = "[" + x.Name.LocalName + "='" +

                   x.Value
                        .Replace("\\", "\\\\")
                       .Replace("'", "\\'")

                   + "']";

                return this[selector];
            }
        }

        [Obsolete("what about .getAttribute vs dynamic")]
        public new CSSStyleRuleMonkier this[Expression<Func<IHTMLElement, bool>> f]
        {
            get
            {
                return this[IStyleSheet.GetAttributeSelectorText(f)];
            }
        }


        #region print
        [Obsolete("print is special, it will regroup the selector into a special group")]
        public CSSStyleRuleMonkier print
        {
            get
            {
                // tested by
                // X:\jsc.svn\examples\javascript\Test\TestInteractiveStyleRule\TestInteractiveStyleRule\Application.cs

                // android webview gives us trouble
                // revert to a dedicated stylesheet?
                // X:\jsc.svn\examples\javascript\Test\TestCSSPrint\TestCSSPrint\Application.cs

                //return IStyleSheet.print[this.selectorText];


                var p = this.rule.parentStyleSheet[CSSMediaTypes.print];

                if (p == null)
                {
                    Console.WriteLine("creating a disabled style rule as android webview does not know any better?");

                    // there can be only 31 per IE?
                    var x = new IStyleSheet { disabled = true };

                    return x[selectorText];
                }

                return p[selectorText];
            }
        }
        #endregion

        // once set, this monkier is supposed to target this element
        // should we be able to modify this element later?
        // set by .css
        public IHTMLElement selectorElement;

        #region selectorText
        public event Action selectorTextChanged;

        string __selectorText;
        public string selectorText
        {
            get { return __selectorText; }
            set
            {
                __selectorText = value;

                if (this.parent == null)
                {
                    this.rule.selectorText = __selectorText;
                }
                else
                {
                    if (this.rule == null)
                    {
                        this.rule = this.parent.rule[__selectorText];
                    }
                    else
                    {
                        this.rule.selectorText = this.parent.rule.selectorText + __selectorText;
                    }
                }

                if (selectorTextChanged != null)
                    selectorTextChanged();
            }
        }
        #endregion



        #region >:nth-child
        [Script]
        public sealed class CSSStyleRuleMonkier_nthChild_index : CSSStyleRuleMonkier
        {
            public string op = ">:nth-child";

            int __index;
            public int index
            {
                get { return __index; }
                set
                {
                    __index = value;

                    // c# 0 based, css is 1 based?

                    this.selectorText = op + "(" + (__index + 1) + ")";


                    //Console.WriteLine(new { this.rule.selectorText });
                }
            }
        }

        [Script]
        public sealed class CSSStyleRuleMonkier_nthChild
        {
            public string op = ">:nth-child";

            public CSSStyleRuleMonkier parent;

            public CSSStyleRuleMonkier odd
            {
                get
                {
                    return
                         new CSSStyleRuleMonkier
                         {
                             parent = parent,
                             selectorText = op + "(odd)"
                         };
                }
            }

            public CSSStyleRuleMonkier even
            {
                get
                {
                    return
                         new CSSStyleRuleMonkier
                         {
                             parent = parent,
                             selectorText = op + "(even)"
                         };
                }
            }

            public CSSStyleRuleMonkier_nthChild_index this[int index]
            {
                get
                {
                    return
                        new CSSStyleRuleMonkier_nthChild_index
                        {
                            op = op,
                            parent = parent,
                            index = index,
                        };
                }
            }


            public CSSStyleRuleMonkier_nthChild_index this[Expression<Func<int>> yindexer]
            {
                get
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
                        var lambda = yindexer as LambdaExpression;
                        if (lambda != null)
                        {
                            var member = lambda.Body as MemberExpression;
                            if (member != null)
                            {
                                // { yindexer = { Body = MemberExpression { expression = Constant { value = [object Object], type =  }, field = yindexer_index }, parameters =  } } 

                                var member_field0 = member.Member as FieldInfo;

                                var member_constant0 = member.Expression as ConstantExpression;
                                if (member_constant0 != null)
                                {
                                    var field0_value = member_field0.GetValue(member_constant0.Value);
                                    i4_value = (int)field0_value;
                                }


                                // { yindexer = { Body = MemberExpression { expression = MemberExpression { 
                                // expression = Constant { value = [object Object], type =  }, field = SelectedRowIndex }, 
                                // field = index }, parameters =  } } 

                                var membermember = member.Expression as MemberExpression;
                                if (membermember != null)
                                {
                                    var membermember_field1 = membermember.Member as FieldInfo;

                                    var membermember_constant = membermember.Expression as ConstantExpression;
                                    if (membermember_constant != null)
                                    {
                                        //Console.WriteLine(new { membermember_constant });

                                        var field1_value = membermember_field1.GetValue(membermember_constant.Value);

                                        //Console.WriteLine(new { field1_value });

                                        var field0_value = member_field0.GetValue(field1_value);

                                        //Console.WriteLine(new { field0_value });

                                        i4_value = (int)field0_value;

                                    }
                                }

                            }
                        }
                        #endregion

                        w.Stop();

                        return i4_value;
                    };
                    #endregion


                    var i = i4();

                    // http://www.w3schools.com/cssref/sel_nth-child.asp
                    //var selectorText = ">:nth-child(" + i + ")";
                    var rp = this[i];

                    #region bind
                    Native.window.onframe += delegate
                    {
                        var ii = i4();
                        if (ii == rp.index)
                            return;

                        rp.index = ii;
                    };
                    #endregion


                    return rp;
                }
            }

        }

        public CSSStyleRuleMonkier_nthChild nthChild
        {
            get
            {
                return new CSSStyleRuleMonkier_nthChild { parent = this };
            }
        }

        internal bool nthChildInlineMode;

        public CSSStyleRuleMonkier odd
        {
            get
            {
                // tested by
                // X:\jsc.svn\examples\javascript\css\CSSnthSelectorByExpression\CSSnthSelectorByExpression\Application.cs
                var n = new CSSStyleRuleMonkier_nthChild { parent = this };

                if (this.nthChildInlineMode)
                {
                    //n.op = ":nth-child";
                    n.op = ":nth-of-type";
                }

                return n.odd;
            }
        }

        public CSSStyleRuleMonkier even
        {
            get
            {
                // tested by
                // X:\jsc.svn\examples\javascript\css\CSSnthSelectorByExpression\CSSnthSelectorByExpression\Application.cs
                var n = new CSSStyleRuleMonkier_nthChild { parent = this };

                if (this.nthChildInlineMode)
                {
                    //n.op = ":nth-child";
                    n.op = ":nth-of-type";
                }

                return n.even;
            }
        }

        public CSSStyleRuleMonkier_nthChild_index this[Expression<Func<int>> yindexer]
        {
            get
            {
                // tested by
                // X:\jsc.svn\examples\javascript\css\CSSnthSelectorByExpression\CSSnthSelectorByExpression\Application.cs
                var n = new CSSStyleRuleMonkier_nthChild { parent = this };

                if (this.nthChildInlineMode)
                {
                    n.op = ":nth-of-type";
                }

                return n[yindexer];
            }
        }

        public CSSStyleRuleMonkier_nthChild_index this[int index]
        {
            get
            {
                // tested by
                // X:\jsc.svn\examples\javascript\css\CSSnthSelector\CSSnthSelector\Application.cs

                var n = new CSSStyleRuleMonkier_nthChild { parent = this };

                if (this.nthChildInlineMode)
                {
                    n.op = ":nth-of-type";
                }

                return n[index];
            }
        }


        #endregion


        public CSSStyleRuleMonkier active
        {
            get
            {
                // X:\jsc.svn\examples\javascript\CSS\CSSActivePseudoSelector\CSSActivePseudoSelector\Application.cs

                return this[":active"];
            }
        }

        public CSSStyleRuleMonkier hover
        {
            get
            {
                return this[":hover"];
            }
        }

        public CSSStyleRuleMonkier before
        {
            get
            {
                return this[":before"];
            }
        }

        public CSSStyleRuleMonkier after
        {
            get
            {
                return this[":after"];
            }
        }

        // https://www.w3.org/community/webed/wiki/Advanced_CSS_selectors

        public CSSStyleRuleMonkier @checked
        {
            get
            {
                return this[":checked"];
            }
        }

        public CSSStyleRuleMonkier @unchecked
        {
            get
            {
                // what about providing .not also? prefix or suffix?
                return this[":not(:checked)"];
            }
        }

        public CSSStyleRuleMonkier disabled
        {
            get
            {
                return this[":disabled"];
            }
        }



        public CSSStyleRuleMonkier focus
        {
            get
            {
                return this[":focus"];
            }
        }



        /// <summary>
        /// The :empty pseudo-class represents any element that has no children at all. 
        /// Only element nodes and text (including whitespace) are considered. 
        /// Comments or processing instructions do not affect whether an element is considered empty or not.
        /// </summary>
        public CSSStyleRuleMonkier empty
        {
            get
            {
                return this[":empty"];
            }
        }

        public CSSStyleRuleMonkier required
        {
            get
            {
                return this[":required"];
            }
        }

        public CSSStyleRuleMonkier optional
        {
            get
            {
                return this[":optional"];
            }
        }

        public CSSStyleRuleMonkier visited
        {
            get
            {
                return this[":visited"];
            }
        }

        public CSSStyleRuleMonkier valid
        {
            get
            {
                return this[":valid"];
            }
        }

        public CSSStyleRuleMonkier invalid
        {
            get
            {
                return this[":invalid"];
            }
        }

        public CSSStyleRuleMonkier link
        {
            get
            {
                return this[":link"];
            }
        }


        #region content
        // contentString
        public string content
        {
            set
            {
                // does this need to be set for :after element to appear at all?
                this.style.content = "'" +
                    value
                        .Replace("\\", "\\\\")
                        .Replace("'", "\\'")

                    + "'";

            }
        }

        public IHTMLImage contentImage
        {
            set
            {
                this.style.content = "url('" +
                    value.src
                    + "')";

            }
        }

        public XAttribute contentXAttribute
        {
            set
            {
                // what about multiple attributes?

                // X:\jsc.svn\examples\javascript\CSS\CSSXAttributeAsConditional\CSSXAttributeAsConditional\Application.cs
                this.style.content = "attr(" + value.Name.LocalName + ")";
            }
        }
        #endregion




        #region first

        [Script]
        public sealed class CSSStyleRuleMonkier_first
        {
            public CSSStyleRuleMonkier parent;

            // default provider
            public CSSStyleDeclaration style
            {
                get
                {
                    //Console.WriteLine("css.style " + new { this.rule.selectorText });


                    return this.child.style;
                }
            }


            // http://www.w3schools.com/cssref/sel_firstchild.asp
            // http://stackoverflow.com/questions/2717480/css-selector-for-first-element-with-class/8539107#8539107
            // jsc can you collect anwsers from users and notify?
            //[Obsolete("should we rename this to firstElement or keep css naming? ")]
            public CSSStyleRuleMonkier child
            {
                get
                {
                    // should this be the default?

                    // isnt this like nth child?
                    return parent[">:first-child"];
                }
            }

            /// <summary>
            /// This modifies the first line of ? elements. There's no real DOM element that is selected, that is why it is a pseudo-element.
            /// </summary>
            public CSSStyleRuleMonkier line
            {
                get
                {

                    // http://stackoverflow.com/questions/11359728/what-does-this-operator-in-css-mean
                    return parent["::first-line"];
                }
            }


            public CSSStyleRuleMonkier letter
            {
                get
                {
                    Console.WriteLine(":first-letter");

                    //http://www.w3.org/TR/selectors/#pseudo-elements
                    // X:\jsc.svn\examples\javascript\Test\TestFirstLine\TestFirstLine\Application.cs
                    // http://stackoverflow.com/questions/14075274/issue-with-css-first-of-type-first-letter

                    //return parent["::first-letter"];
                    return parent[":first-letter"];
                }
            }

            [Obsolete("when can we also do typeof(div) ?")]
            public CSSStyleRuleMonkier this[ScriptCoreLib.JavaScript.DOM.HTML.IHTMLElement.HTMLElementEnum className]
            {
                [Script(DefineAsStatic = true)]
                get
                {
                    // child nodes?
                    var selectorText = ">" + className + ":first-of-type";

                    var z = this.parent[selectorText];

                    // this is like type of nth?
                    z.nthChildInlineMode = true;

                    return z;
                }
            }

        }


        // see also even
        public CSSStyleRuleMonkier_first first
        {
            get
            {

                //http://www.w3.org/TR/selectors/#pseudo-elements
                // X:\jsc.svn\examples\javascript\Test\TestFirstLine\TestFirstLine\Application.cs

                return new CSSStyleRuleMonkier_first { parent = this };
            }
        }







        #endregion

        #region last

        [Script]
        public sealed class CSSStyleRuleMonkier_last
        {
            public CSSStyleRuleMonkier parent;

            // http://www.w3schools.com/cssref/sel_firstchild.asp
            // http://stackoverflow.com/questions/2717480/css-selector-for-first-element-with-class/8539107#8539107
            // jsc can you collect anwsers from users and notify?
            //[Obsolete("should we rename this to firstElement or keep css naming? ")]
            public CSSStyleRuleMonkier child
            {
                get
                {
                    // isnt this like nth child?
                    return parent[">:last-child"];
                }
            }


            [Obsolete("when can we also do typeof(div) ?")]
            public CSSStyleRuleMonkier this[ScriptCoreLib.JavaScript.DOM.HTML.IHTMLElement.HTMLElementEnum className]
            {
                [Script(DefineAsStatic = true)]
                get
                {
                    // child nodes?
                    var selectorText = ">" + className + ":last-of-type";

                    var z = this.parent[selectorText];

                    // this is like type of nth?
                    z.nthChildInlineMode = true;

                    return z;
                }
            }

        }


        // see also even
        public CSSStyleRuleMonkier_last last
        {
            get
            {

                //http://www.w3.org/TR/selectors/#pseudo-elements
                // X:\jsc.svn\examples\javascript\Test\TestFirstLine\TestFirstLine\Application.cs

                return new CSSStyleRuleMonkier_last { parent = this };
            }
        }







        #endregion




        // adjacent-sibling selector

        #region siblings
        [Script]
        public sealed class CSSStyleRuleMonkier_siblings
        {
            // http://www.w3.org/TR/css3-selectors/#adjacent-sibling-combinators

            public CSSStyleRuleMonkier parent;

            public string op = "+";

            public CSSStyleDeclaration style
            {
                get
                {
                    return this["*"].style;
                }
            }

            public CSSStyleRuleMonkier this[ScriptCoreLib.JavaScript.DOM.HTML.IHTMLElement.HTMLElementEnum className]
            {
                get
                {
                    var selectorText = "" + className;

                    // tested by
                    // X:\jsc.svn\examples\javascript\CSS\CSSSpecificDescendant\CSSSpecificDescendant\Application.cs

                    var x = parent[this.op + selectorText];
                    x.nthChildInlineMode = true;
                    return x;
                }
            }

            public CSSStyleRuleMonkier this[string selectorText]
            {
                get
                {
                    return parent[this.op + selectorText];
                }
            }
        }

        // http://meyerweb.com/eric/articles/webrev/200007a.html
        public CSSStyleRuleMonkier_siblings adjacentSibling
        {
            [Script(DefineAsStatic = true)]
            get
            {
                return new CSSStyleRuleMonkier_siblings { parent = this, op = "+" };
            }
        }

        // http://www.w3.org/TR/css3-selectors/#general-sibling-combinators
        public CSSStyleRuleMonkier_siblings siblings
        {
            [Script(DefineAsStatic = true)]
            get
            {
                return new CSSStyleRuleMonkier_siblings { parent = this, op = "~" };
            }
        }
        #endregion



        public CSSStyleRuleMonkier children
        {
            [Script(DefineAsStatic = true)]
            get
            {
                // X:\jsc.svn\market\javascript\Abstractatech.JavaScript.Avatar\Abstractatech.JavaScript.Avatar\Application.cs
                return this[">*"];
            }
        }


        public CSSStyleRuleMonkier descendant
        {
            get
            {
                var x = this[""];

                x.descendantMode = true;

                return x;
            }
        }



        public override string ToString()
        {
            return new { this.rule.selectorText, this.selectorElement }.ToString();
        }



        #region parent
        CSSStyleRuleMonkier __parent;
        [Obsolete("what about multiple parents, when  | operator is used?")]
        public CSSStyleRuleMonkier parent
        {
            get { return __parent; }
            set
            {
                __parent = value;

                __parent.selectorTextChanged +=
                    delegate
                    {
                        //Console.WriteLine("__parent.selectorTextChanged " + new { __parent = __parent.selectorText, child = selectorText });

                        // force refresh
                        this.selectorText = this.selectorText;
                    };
            }
        }
        #endregion


        public BindingList<CSSStyleRuleMonkier> parents = new BindingList<CSSStyleRuleMonkier>();

        public CSSStyleRuleMonkier()
        {
            // X:\jsc.svn\examples\javascript\CSS\CSSOrOperatorNestedStyle\CSSOrOperatorNestedStyle\Application.cs

            parents.AddingNew +=
                (sender, args) =>
                {
                    // do we allow to remove parents?

                    var parent1 = (CSSStyleRuleMonkier)args.NewObject;

                    parent1.selectorTextChanged +=
                       delegate
                       {
                           var selectorText = GetSelectorTextFromParents(parents.ToArray());

                           this.rule.selectorText = selectorText;
                       };

                };
        }

        private static string GetSelectorTextFromParents(CSSStyleRuleMonkier[] parents)
        {
            var w = new StringBuilder();

            for (int i = 0; i < parents.Length; i++)
            {
                if (i > 0)
                    w.Append(",");

                w.Append(parents[i].rule.selectorText);
            }

            var selectorText = w.ToString();
            return selectorText;
        }

        // Error	55	'ScriptCoreLib.JavaScript.DOM.CSSStyleRuleMonkier.implicit operator ScriptCoreLib.JavaScript.DOM.CSSStyleRuleMonkier(System.Collections.Generic.IEnumerable<ScriptCoreLib.JavaScript.DOM.CSSStyleRuleMonkier>)': user-defined conversions to or from an interface are not allowed	X:\jsc.svn\core\ScriptCoreLib\JavaScript\DOM\CSS\CSSStyleRule.cs	1010	23	ScriptCoreLib
        //public static implicit operator CSSStyleRuleMonkier(IEnumerable<CSSStyleRuleMonkier> parents)
        public static implicit operator CSSStyleRuleMonkier(CSSStyleRuleMonkier[] collection)
        {
            // what about group of groups?
            var rule = IStyleSheet.all[GetSelectorTextFromParents(collection.ToArray())];

            foreach (var item in collection)
            {
                rule.parents.Add(item);
            }

            return rule;
        }

        //public static CSSStyleRuleMonkier operator +(CSSStyleRuleMonkier parent1, CSSStyleRuleMonkier parent2)
        public static CSSStyleRuleMonkier operator |(CSSStyleRuleMonkier parent1, CSSStyleRuleMonkier parent2)
        {
            if (parent1 == null)
                return parent2;

            if (parent2 == null)
                return parent1;

            // tested by
            // X:\jsc.svn\core\ScriptCoreLib.Windows.Forms\ScriptCoreLib.Windows.Forms\JavaScript\BCLImplementation\System\Windows\Forms\DataGridView..ctor.cs
            // X:\jsc.svn\examples\javascript\css\CSSTableSelector\CSSTableSelector\Application.cs

            return new[] { parent1, parent2 };
        }


        // Error	4	The call is ambiguous between the following methods or properties: 'ScriptCoreLib.JavaScript.DOM.CSSStyleRuleMonkier.this[int]' and 'ScriptCoreLib.JavaScript.DOM.CSSStyleRuleMonkier.this[ScriptCoreLib.JavaScript.DOM.HTML.IHTMLElement]'	X:\jsc.svn\examples\javascript\CSS\CSSSpecificDescendant\CSSSpecificDescendant\Application.cs	44	13	CSSSpecificDescendant
        // span has conficting operators?
        public CSSStyleRuleMonkier this[IHTMLSpan e]
        {
            get
            {
                return this[(IHTMLElement)e];
            }
        }



        public CSSStyleRuleMonkier this[IHTMLElement target]
        {
            get
            {
                // X:\jsc.svn\examples\javascript\CSS\CSSDetatchedSiblingOperator\CSSDetatchedSiblingOperator\Application.cs

                //Console.WriteLine(" either a sibling or a decendant. our task is to find the location and remember it");


                // could this be the + operator?

                var TargetRules = new List<CSSStyleRuleMonkier>();


                // X:\jsc.svn\examples\javascript\CSS\CSSSpecificDescendant\CSSSpecificDescendant\Application.cs
                // X:\jsc.svn\examples\javascript\CSS\CSSConditionalStyle\CSSConditionalStyle\Application.cs

                var SourceRules = new List<CSSStyleRuleMonkier>();

                // this rule is a group!
                if (this.parents.Count > 0)
                {
                    //Console.WriteLine("lets look at each parent separatly as they form the or operator");

                    SourceRules.AddRange(
                        this.parents
                    );
                }
                else
                {
                    SourceRules.Add(
                        this
                    );
                }

                foreach (var SourceRule in SourceRules)
                {
                    //Console.WriteLine("looking at " + new { SourceRule.rule.selectorText, SourceRule.IsPseudoSelector, SourceRule.selectorElement });

                    var withoutpseudo = SourceRule;

                    // what about other pseudo selectors?
                    // what about multilevel pseudos?
                    //while (withoutpseudo.IsPseudoSelector)
                    while (withoutpseudo.selectorElement == null)
                    {
                        //Console.WriteLine(" we need to go one level up");
                        withoutpseudo = withoutpseudo.parent;
                    }

                    // um what if the source element is not yet attached?
                    var collection = default(IHTMLElement[]);

                    //Console.WriteLine(new { withoutpseudo });

                    if (withoutpseudo.selectorElement == null)
                    {
                        // well, lets ast the document. which document? :P
                        // what about popups and iframes?
                        //collection = Native.document.querySelectorAll(withoutpseudo.rule.selectorText);

                        throw new InvalidOperationException("selectorElement == null");
                    }
                    else
                    {
                        collection = new[] { withoutpseudo.selectorElement };
                    }

                    foreach (var item in collection)
                    {
                        //Console.WriteLine("before GetRelativeSelector " + new { SourceRule });

                        var pp = GetRelativeSelector(SourceRule, item, target);

                        if (pp == null)
                        {
                            Console.WriteLine("path not found!");
                        }
                        else
                        {
                            TargetRules.Add(pp);
                        }
                    }
                }

                // fail?
                if (TargetRules.Count == 0)
                    return null;

                if (TargetRules.Count == 1)
                    return TargetRules[0];

                // more tests needed
                return TargetRules.ToArray();
            }
        }

        public CSSStyleRuleMonkier this[params IHTMLElement[] collection]
        {
            get
            {
                var p = default(CSSStyleRuleMonkier);

                foreach (var item in collection)
                {
                    p |= this[item];
                }

                return p;
            }
        }

        internal bool descendantMode;


        //public CSSStyleRule this[ScriptCoreLib.JavaScript.DOM.HTML.IHTMLElement.HTMLElementEnum className]
        //{
        //    [Script(DefineAsStatic = true)]
        //    get
        //    {
        //        // child nodes?
        //        var selectorText = ">" + className;

        //        return this[selectorText];
        //    }
        //}

        [Obsolete("when can we also do typeof(div) ?")]
        public CSSStyleRuleMonkier this[ScriptCoreLib.JavaScript.DOM.HTML.IHTMLElement.HTMLElementEnum className]
        {
            [Script(DefineAsStatic = true)]
            get
            {
                // child nodes?
                var selectorText = ">" + className;

                if (descendantMode)
                    selectorText = " " + className;

                var z = this[selectorText];

                // this is like type of nth?
                z.nthChildInlineMode = true;

                return z;
            }
        }


        public static CSSStyleRuleMonkier GetRelativeSelector(CSSStyleRuleMonkier css, IHTMLElement source, IHTMLElement target)
        {
            if (target == null)
                throw new InvalidOperationException();

            // if the target element has style id then should we use it?
            // or us it if the source is document element?
            // this would allow document level attributes to work
            // as filters for state

            if (source.parentNode == target.parentNode)
            {
                //Console.WriteLine(" ah. we are talking about a direct sibling? if so, whats the index?");

                var yindex = source.parentNode.childNodes
                    .AsEnumerable()
                    .Where(x => x.nodeType == target.nodeType)
                    .Where(x => x.nodeName == target.nodeName)
                    .TakeWhile(x => x != target)
                    .Count();

                //Console.WriteLine(new { yindex });

                var z = css["~" + target.localName];
                z.nthChildInlineMode = true;
                return z[yindex];
            }

            if (source == target.parentNode)
            {
                //Console.WriteLine(" ah. direct descendant.");

                var yindex = source.childNodes
                    .AsEnumerable()
                    .Where(x => x.nodeType == target.nodeType)
                    .Where(x => x.nodeName == target.nodeName)
                    .TakeWhile(x => x != target)
                    .Count();

                //Console.WriteLine(new { yindex });

                var z = css[">" + target.localName];
                z.nthChildInlineMode = true;
                return z[yindex];
            }

            //Console.WriteLine(" lets check the parent as a link ");

            // what about different documents?
            if (target.parentNode != null)
            {
                var p = GetRelativeSelector(css, source, target.parentNode);
                if (p != null)
                {
                    var yindex = target.parentNode.childNodes
                     .AsEnumerable()
                     .Where(x => x.nodeType == target.nodeType)
                     .Where(x => x.nodeName == target.nodeName)
                     .TakeWhile(x => x != target)
                     .Count();

                    //Console.WriteLine(new { yindex });

                    var z = p[">" + target.localName];
                    z.nthChildInlineMode = true;
                    return z[yindex];
                }
            }

            Console.WriteLine(" parents do not match yet ");

            return null;
        }



        [Obsolete("experimental")]
        public static CSSStyleRuleMonkier operator &(CSSStyleRuleMonkier left, CSSStyleRuleMonkier right)
        {
            // op_BitwiseAnd

            Console.WriteLine("enter css op_BitwiseAnd \n"
             + new
             {
                 left,
                 right,
                 //right.IsPseudoSelector,
                 right.selectorElement,
                 right.selectorText
             });

            // X:\jsc.svn\examples\javascript\CSS\CSSConditionalStyle\CSSConditionalStyle\Application.cs

            //       page.c1.css.@checked[page.c2].@checked[
            //    page.PageContainer
            //].style.borderBottom = "1em solid red";

            // for now lets assume the other rule is about checkbox too..


            // escape 1 level
            if (right.selectorElement == null)
            {
                // 28ms { left = { selectorText = input[style-id="0"]:checked }, 
                // right = { selectorText = input[style-id="1"]:checked }, 
                //  IsPseudoSelector = true, selectorElement = , selectorText = :checked } 
                //Console.WriteLine(new { right.parent });
                //Console.WriteLine(new { right.parent.selectorElement });

                var withoutPseudoSelector = left[right.parent.selectorElement];
                withoutPseudoSelector.selectorElement = right.parent.selectorElement;


                Console.WriteLine(new { withoutPseudoSelector });

                var withPseudoSelector = withoutPseudoSelector[right.selectorText];
                //Console.WriteLine(new { withPseudoSelector });

                // 27ms { withPseudoSelector = { selectorText = input[style-id="1"]:checked ~ input:nth-of-type(2):checked, selectorElement =  } } 
                //Console.WriteLine(" ah. lets remember the element,");

                // we the query will not work with all those
                // checked pseudoselectors already attached


                return withPseudoSelector;

            }

            return left[right.selectorElement];
        }

    }
}
