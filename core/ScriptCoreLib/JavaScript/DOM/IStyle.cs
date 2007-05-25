using ScriptCoreLib.JavaScript;
using ScriptCoreLib.JavaScript.Runtime;
using ScriptCoreLib.Shared;


namespace ScriptCoreLib.JavaScript.DOM
{
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

        public bool disabled;

        internal IStyleSheetRule[] rules;
        internal IStyleSheetRule[] cssRules;

        public IStyleSheetRule[] Rules
        {
            [Script(DefineAsStatic = true)]
            get
            {
                if (Expando.InternalIsMember(this, "rules"))
                    return this.rules;

                if (Expando.InternalIsMember(this, "cssRules"))
                    return this.cssRules;

                return null;
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

            s.attachToDocument();

            return s.StyleSheet;
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


        // http://www.javascriptkit.com/domref/stylesheet.shtml
        [Script(DefineAsStatic = true)]
        public IStyleSheetRule AddRule(string selector, string declaration, int index)
        {

            if (Expando.InternalIsMember(this, "addRule"))
                this.addRule(selector, declaration, index);

            if (Expando.InternalIsMember(this, "insertRule"))
                this.insertRule(selector + "{" + declaration + "}", index);

            return this.Rules[index];
        }

        [Script(DefineAsStatic = true)]
        public IStyleSheetRule AddRule(string selector)
        {
            return AddRule(selector, "/**/", this.Rules.Length);
        }

        [Script(DefineAsStatic = true)]
        public IStyleSheetRule AddRule(global::System.Collections.Generic.KeyValuePair<string, Action<IStyleSheetRule>> r)
        {

            return this.AddRule(r.Key, r.Value);

        }

        [Script(DefineAsStatic = true)]
        public IStyleSheetRule AddRule(string selector, Action<IStyleSheetRule> r)
        {
            var x = AddRule(selector);

            r(x);

            return x;
        }

    }

    [Script(HasNoPrototype = true)]
    public partial class IStyle
    {

        public string clip;
        public string clear;

        #region white-space

        public WhiteSpaceEnum whiteSpace;

        [Script(IsStringEnum = true)]
        public enum WhiteSpaceEnum
        {
            /// <summary>
            /// White-space is ignored by the browser
            /// </summary>
            normal,
            /// <summary>
            /// White-space is preserved by the browser. Acts like the <pre> tag in HTML
            /// </summary>

            pre,
            /// <summary>
            /// The text will never wrap, it continues on the same line until a <br> tag is encountered
            /// </summary>
            nowrap
        }
        #endregion


        #region cursor
        // TODO support per member values in enum
        [Script(IsStringEnum = true)]
        public enum CursorEnum
        {
            @default, auto, crosshair, pointer, move, text, wait, help

        }

        /// <summary>
        /// <see>http://www.w3schools.com/css/pr_class_cursor.asp</see>
        /// </summary>
        public CursorEnum cursor;
        #endregion

        // http://www.w3schools.com/css/css_background.asp
        public string background;
        public string backgroundImage;
        public string backgroundColor;
        //public string backgroundLeft;
        //public string backgroundTop;
        public string backgroundRepeat;
        public string backgroundPosition;

        #region display

        /// <summary>
        /// http://www.w3schools.com/css/pr_class_display.asp
        /// </summary>
        [Script(IsStringEnum = true)]
        public enum DisplayEnum
        {
            none,
            block,
            inline,
            [Script(ExternalTarget = "")]
            empty

        }
        public DisplayEnum display;
        #endregion

        #region visibility
        [Script(IsStringEnum = true)]
        public enum VisibilityEnum
        {
            visible,
            hidden,
            collapse
        }
        public VisibilityEnum visibility;
        #endregion


        #region padding
        public string padding;
        public string paddingTop;
        public string paddingBottom;
        public string paddingLeft;
        public string paddingRight;
        #endregion

        #region fontFamily
        [Script(IsStringEnum = true)]
        public enum FontFamilyEnum
        {
            Times, Helvetica, /*Zapf-Chancery,*/ Western,
            [Script(ExternalTarget = "Consolas, Courier New, Courier")]
            Consolas,
            Courier, Verdana, Tahoma, Arial,
            Fixedsys
        }
        public FontFamilyEnum fontFamily;

        //[Script(ExternalTarget = "fontFamily")]
        //public string fontFamilyValue;
        #endregion


        public string fontWeight;
        public string fontSize;
        public string font;

        public string margin;
        public string marginLeft;
        public string marginTop;
        public string marginRight;
        public string marginBottom;

        #region border
        public string border;
        public string borderStyle;
        public string borderRight;
        public string borderLeft;
        public string borderTop;
        public string borderBottom;
        public string borderWidth;
        public string borderColor;
        #endregion

        public string color;







        public string right;
        public string bottom;
        public string left;
        public string top;
        public string width;
        public string height;

        #region position
        [Script(IsStringEnum = true)]
        public enum PositionEnum
        {
            @static,
            absolute,
            relative
        }

        public PositionEnum position;
        #endregion


        #region overflow
        [Script(IsStringEnum = true)]
        public enum OverflowEnum
        {
            visible, hidden, scroll, auto
        }

        public OverflowEnum overflow;

        #endregion

        public string outline;


        #region textAlgin
        [Script(IsStringEnum = true)]
        public enum TextAlignEnum { left, right, center, justify }
        public TextAlignEnum textAlign;
        #endregion

        public string textDecoration;

        [Script(IsStringEnum = true)]
        public enum TextTransformEnum
        {
            /// <summary>
            /// Default. Defines normal text, with lower case letters and capital letters
            /// </summary>
            none,
            /// <summary>
            /// Each word in a text starts with a capital letter
            /// </summary>
            capitalize,
            /// <summary>
            /// Defines only capital letters
            /// </summary>
            uppercase
                ,
            /// <summary>
            /// Defines no capital letters, only lower case letters
            /// </summary>
            lowercase
        }
        public TextTransformEnum textTransform;

        public string verticalAlign;

        public int zIndex;

    }
}
