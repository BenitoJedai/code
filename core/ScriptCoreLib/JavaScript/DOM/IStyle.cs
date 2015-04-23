using ScriptCoreLib.JavaScript;
using ScriptCoreLib.JavaScript.DOM.HTML;
using ScriptCoreLib.JavaScript.Extensions;
using ScriptCoreLib.JavaScript.Runtime;
using ScriptCoreLib.Shared;
using System;
using System.Threading.Tasks;
using System.Xml.Linq;


namespace ScriptCoreLib.JavaScript.DOM
{
    // see: http://www.w3.org/TR/DOM-Level-2-Style/idl-definitions.html

    [Script]
    internal class __IStyle
    {
        public static void set_contentAsync(IStyle that, Task<string> value)
        {
            value.ContinueWith(
                 task =>
                 {
                     that.content = task.Result;
                 }
             );
        }
    }

    // http://www.w3.org/TR/DOM-Level-2-Style/css.html
    // CSSStyleDeclaration  
    // CSS2Properties 
    [Script(InternalConstructor = true)]
    public partial class IStyle
    {
        //  http://www.w3schools.com/cssref/pr_gen_content.asp
        // http://caniuse.com/css-gencontent
        public string content;


        [Obsolete("experimental")]
        public Task<string> contentAsync
        {
            [Script(DefineAsStatic = true)]
            set
            {
                __IStyle.set_contentAsync(this, value);
            }
        }

        public XAttribute contentXAttribute
        {

            [Script(DefineAsStatic = true)]
            set
            {
                // X:\jsc.svn\examples\javascript\CSS\CSSXAttributeAsConditional\CSSXAttributeAsConditional\Application.cs
                this.content = "attr(" + value.Name.LocalName + ")";
            }
        }


        #region Constructor

        public IStyle()
        {
            // InternalConstructor
        }

        static IStyle InternalConstructor()
        {
			// X:\jsc.svn\examples\javascript\CSS\Test\CSSNewIStyle\CSSNewIStyle\Application.cs
			// X:\jsc.svn\examples\javascript\chrome\extensions\ChromeExtensionHopToTabThenIFrame\ChromeExtensionHopToTabThenIFrame\Application.cs

			// or :root instead?
			return IStyleSheet.all["*"].style;
        }



        public IStyle(IStyle e)
        {
            // InternalConstructor
        }

        static IStyle InternalConstructor(IStyle e)
        {
            // X:\jsc.svn\core\ScriptCoreLib.Windows.Forms\ScriptCoreLib.Windows.Forms\JavaScript\BCLImplementation\System\Windows\Forms\TreeView\TreeView.cs
            return e;
        }


        public IStyle(IHTMLElement.HTMLElementEnum e)
        {
            // InternalConstructor
        }

        static IStyle InternalConstructor(IHTMLElement.HTMLElementEnum e)
        {
            // X:\jsc.svn\examples\javascript\CSS\Test\CSSNewIStyle\CSSNewIStyle\Application.cs

            return IStyleSheet.all[e].style;
        }

        public IStyle(Type e)
        {
            // InternalConstructor
        }

        static IStyle InternalConstructor(Type e)
        {
            // X:\jsc.svn\examples\javascript\CSS\Test\CSSNewIStyle\CSSNewIStyle\Application.cs
            // x:\jsc.svn\examples\javascript\webgl\heatzeekerrts\heatzeekerrts\application.cs

            // c = jQgABjls6jSgl0UFIggGlQ(hwgABjls6jSgl0UFIggGlQ(), b).pwQABmASXzSNfpl_bHJLUOA();
            // we are being called by static ctor?
            // are we in a worker?

            if (Native.document == null)
            {
                // send a fake type back
                return (IStyle)new object();
            }

            return IStyleSheet.all[e].style;
        }


        public IStyle(CSSStyleRuleMonkier css)
        {
            // InternalConstructor
        }

        [Obsolete("jsc experience. allows field initializer to be used with bre build css")]
        static IStyle InternalConstructor(CSSStyleRuleMonkier css)
        {
            // X:\jsc.svn\core\ScriptCoreLib.Windows.Forms\ScriptCoreLib.Windows.Forms\JavaScript\BCLImplementation\System\Windows\Forms\DataGridView\DataGridView..ctor.cs
            // X:\jsc.svn\examples\javascript\CSS\Test\CSSNewIStyle\CSSNewIStyle\Application.cs

            return css.style;
        }

        public IStyle(IHTMLElement e)
        {
            // InternalConstructor
        }

        static IStyle InternalConstructor(IHTMLElement e)
        {
            // X:\jsc.svn\core\ScriptCoreLib.Windows.Forms\ScriptCoreLib.Windows.Forms\JavaScript\BCLImplementation\System\Windows\Forms\DataGridView\DataGridView..ctor.cs
            // X:\jsc.svn\examples\javascript\CSS\Test\CSSNewIStyle\CSSNewIStyle\Application.cs

            return e.style;
        }


        #endregion


        // CSS2Properties 

        // todo:
        // http://samples.msdn.microsoft.com/workshop/samples/author/dhtml/filters/matrix.htm
        // http://msdn.microsoft.com/en-us/library/ms533014%28VS.85%29.aspx


        // http://kennedia.svnrepository.com/Basecamp.wdgt/trac.cgi/browser/AppleClasses/AppleButton.js?rev=5#L133
        /// <summary>
        /// Example values: "dashboard-region(control rectangle)" "none"
        /// </summary>
        public string appleDashboardRegion;

        public string lineHeight;

        // http://www.w3schools.com/CSS/pr_pos_clip.asp
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
            // http://www.w3schools.com/cssref/pr_class_cursor.asp

            @default, auto, crosshair, pointer, move, text, wait, help,

            [Script(ExternalTarget = "se-resize")]
            se_resize,


            progress

        }

        /// <summary>
        /// <see>http://www.w3schools.com/css/pr_class_cursor.asp</see>
        /// </summary>
        public CursorEnum cursor;
        #endregion


        [Obsolete("what about offset")]
        public IHTMLImage cursorImage
        {

            [Script(DefineAsStatic = true)]
            set
            {
                // tested by
                // X:\jsc.svn\examples\javascript\CSS\Test\CSSCursorImage\CSSCursorImage\Application.cs

                // X:\jsc.svn\examples\javascript\Test\Test435CoreDynamic\Test435CoreDynamic\Class1.cs
                (this as dynamic).cursor = "url('" + value.src + "'), auto";
            }
        }

        [Obsolete("what about offset")]
        public IHTMLDiv cursorElement
        {

            [Script(DefineAsStatic = true)]
            set
            {
                // tested by
                // X:\jsc.svn\examples\javascript\css\Test\CSSSVGCursor\CSSSVGCursor\Application.cs


                this.cursorImage = (IHTMLImage)value;
            }
        }


        // http://www.w3schools.com/css/css_background.asp
        public string background;
        public string backgroundImage;
        public string backgroundColor;
        //public string backgroundLeft;
        //public string backgroundTop;
        public string backgroundRepeat;
        public string backgroundPosition;
        public string backgroundSize;

        public string boxShadow;

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
            empty,


            [Script(ExternalTarget = "inline-block")]
            inline_block

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

        // CSSFontFaceRule
        // X:\jsc.svn\examples\javascript\css\CSSFontFaceExperiment\CSSFontFaceExperiment\Application.cs
        // https://sites.google.com/a/jsc-solutions.net/backlog/knowledge-base/2013/201311/20131119/ttf
        // can we type forward this to ScriptCoreLib.Extensions
        // and list available fonts by popularity in this enum?
        // with the hint that jsc can auto embed the new fonts as used?
        [Script(IsStringEnum = true)]
        [Obsolete(".ttf")]
        public enum FontFamilyEnum
        {
            Times,
            Helvetica, /*Zapf-Chancery,*/
            Western,

            [Script(ExternalTarget = "Consolas, Courier New, Courier")]
            Consolas,

            Courier,
            Verdana,
            Tahoma,
            Arial,

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
        public string borderRightColor;

        public string borderLeft;
        public string borderLeftColor;

        public string borderTop;
        public string borderTopColor;

        public string borderBottom;
        public string borderBottomColor;

        public string borderWidth;
        public string borderColor;

        public string borderTopRightRadius;
        public string borderTopLeftRadius;

        public string borderBottomRightRadius;
        public string borderBottomLeftRadius;

        #endregion

        public string color;






        // http://dolfo.org/2008/07/disable-safaris-textarea-grip/
        public string resize;
        public string right;
        public string bottom;
        public string left;
        public string top;
        public string width;
        public string height;

        // http://www.w3schools.com/css/pr_dim_min-width.asp
        public string minWidth;
        public string minHeight;

        #region position
        [Script(IsStringEnum = true)]
        public enum PositionEnum
        {
            @static,
            absolute,
            relative,
            @fixed
        }

        public PositionEnum position;
        #endregion


        #region overflow
        // http://msdn.microsoft.com/en-us/library/ms530826(VS.85).aspx
        // http://www.brunildo.org/test/Overflowxy2.html

        [Script(IsStringEnum = true)]
        public enum OverflowEnum
        {
            visible, hidden, scroll, auto
        }

        public OverflowEnum overflow;
        public OverflowEnum overflowX;
        public OverflowEnum overflowY;

        #endregion

        public string outline;


        #region textAlgin
        [Script(IsStringEnum = true)]
        public enum TextAlignEnum { left, right, center, justify }
        public TextAlignEnum textAlign;
        #endregion

        // http://www.quirksmode.org/css/textshadow.html
        public string textShadow;
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
        //http://www.w3schools.com/cssref/pr_text_text-indent.asp
        // X:\jsc.svn\core\ScriptCoreLib.Windows.Forms\ScriptCoreLib.Windows.Forms\JavaScript\BCLImplementation\System\Windows\Forms\TreeView\TreeView.cs
        public string textIndent;

        // https://developer.mozilla.org/en-US/docs/Web/CSS/text-overflow
        public string textOverflow;

        public string verticalAlign;

        public int zIndex;



        public string perspective
        {
            [Script(DefineAsStatic = true)]
            get
            {
                var style = (InternalXIStyle)(object)this;
                return style.perspective;
            }
            [Script(DefineAsStatic = true)]
            set
            {
                var style = (InternalXIStyle)(object)this;

                style.MozPerspective = value;
                style.webkitPerspective = value;
                style.perspective = value;
            }
        }

        public string backfaceVisibility
        {
            [Script(DefineAsStatic = true)]
            get
            {
                var style = (InternalXIStyle)(object)this;
                return style.backfaceVisibility;
            }
            [Script(DefineAsStatic = true)]
            set
            {
                var style = (InternalXIStyle)(object)this;

                style.backfaceVisibility = value;
                style.webkitBackfaceVisibility = value;
                style.MozBackfaceVisibility = value;
            }
        }

        public string transformStyle
        {
            [Script(DefineAsStatic = true)]
            get
            {
                var style = (InternalXIStyle)(object)this;
                return style.transformStyle;
            }
            [Script(DefineAsStatic = true)]
            set
            {
                var style = (InternalXIStyle)(object)this;

                style.transformStyle = value;
                style.MozTransformStyle = value;
                style.webkitTransformStyle = value;
            }
        }

        // tested by
        // X:\jsc.svn\examples\javascript\PageNavigationExperiment\PageNavigationExperiment\Application.cs
        public string transition;

        public string transform
        {
            [Script(DefineAsStatic = true)]
            get
            {
                var style = (InternalXIStyle)(object)this;
                return style.transform;
            }
            [Script(DefineAsStatic = true)]
            set
            {
                var style = (InternalXIStyle)(object)this;

                style.transform = value;
                style.webkitTransform = value;
                style.MozTransform = value;
            }
        }

        public string transformOrigin
        {
            [Script(DefineAsStatic = true)]
            get
            {
                var style = (InternalXIStyle)(object)this;
                return style.transformOrigin;
            }
            [Script(DefineAsStatic = true)]
            set
            {
                var style = (InternalXIStyle)(object)this;

                style.transformOrigin = value;
                style.webkitTransformOrigin = value;
                style.MozTransformOrigin = value;
            }
        }
    }

    [Script(HasNoPrototype = true)]
    internal class InternalXIStyle
    {
        public string perspective;
        public string webkitPerspective;
        public string MozPerspective;

        public string MozTransformStyle;
        public string webkitTransformStyle;
        public string transformStyle;

        public string MozTransform;
        public string webkitTransform;
        public string transform;

        public string MozTransformOrigin;
        public string webkitTransformOrigin;
        public string transformOrigin;

        public string webkitBackfaceVisibility;
        public string backfaceVisibility;
        public string MozBackfaceVisibility;
    }

}
