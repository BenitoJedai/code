using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

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

    // https://developer.mozilla.org/en-US/docs/Web/API/CSSRule
    [Script(InternalConstructor = true)]
    public partial class CSSRule
    {
        public readonly CSSRuleTypes type;

        public readonly IStyleSheet parentStyleSheet;
        public readonly CSSRule parentRule;
    }

    [Script]
    public static class CSSRuleExtensions
    {
        // C# generic overload does not work correctly, as such explcitly say whats the function about
        public static TRule OrphanizeRule<TRule>(this TRule x) where TRule : CSSRule
        //public static CSSStyleRule Orphanize<TRule>(this CSSStyleRule x)
        {
            // http://css-tricks.com/a-call-for-nth-everything/

            if (x != null)
            {
                var parentStyleSheet = x.parentStyleSheet;

                if (parentStyleSheet != null)
                {
                    for (int i = 0; i < parentStyleSheet.Rules.Length; i++)
                    {
                        if (parentStyleSheet.Rules[i] == x)
                        {
                            parentStyleSheet.RemoveRule(i);
                            break;
                        }
                    }
                }
            }

            return x;
        }
    }
}
