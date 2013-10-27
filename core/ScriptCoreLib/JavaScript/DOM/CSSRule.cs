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

}
