using ScriptCoreLib.JavaScript.DOM.HTML;
using ScriptCoreLib.JavaScript.Runtime;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ScriptCoreLib.JavaScript.DOM
{
    // http://src.chromium.org/viewvc/blink/trunk/Source/core/css/CSSMediaRule.idl
    // see: http://www.w3.org/TR/DOM-Level-2-Style/idl-definitions.html
    // https://src.chromium.org/viewvc/blink/trunk/Source/core/css/CSSMediaRule.idl

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


        public CSSStyleRuleMonkier this[IHTMLElement e]
        {
            [Script(DefineAsStatic = true)]
            get
            {
                return this[e.InternalGetExplicitRuleSelector()];
            }
        }

        public CSSStyleRuleMonkier this[string selectorText]
        {
            [Script(DefineAsStatic = true)]
            get
            {
                return __IStyleSheet.__get_item(this, selectorText);
            }
        }
    }


}
