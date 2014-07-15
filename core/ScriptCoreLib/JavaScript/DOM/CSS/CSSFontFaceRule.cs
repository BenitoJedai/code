using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ScriptCoreLib.JavaScript.DOM
{
    // http://src.chromium.org/viewvc/blink/trunk/Source/core/css/CSSFontFaceRule.idl

    [Script(InternalConstructor = true)]
    public partial class CSSFontFaceRule : CSSRule
    {
        public CSSStyleDeclaration style;
    }


}
