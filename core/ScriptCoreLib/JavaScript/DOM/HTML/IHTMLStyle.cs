using ScriptCoreLib.JavaScript.Runtime;
using ScriptCoreLib.JavaScript.DOM;
using ScriptCoreLib.JavaScript;
using ScriptCoreLib.JavaScript.Extensions;

using ScriptCoreLib.JavaScript.DOM.HTML;

namespace ScriptCoreLib.JavaScript.DOM.HTML
{
    // http://src.chromium.org/viewvc/blink/trunk/Source/core/html/HTMLStyleElement.idl

    // StyleSheet 
    [Script(InternalConstructor = true)]
    public class IHTMLStyle : IHTMLElement
    {
        // http://dev.w3.org/csswg/cssom/
        // http://www.w3.org/TR/html-markup/style.html

        public bool disabled;

        // tested by
        // X:\jsc.svn\examples\javascript\svg\SVGNavigationTiming\SVGNavigationTiming\Design\PerformanceResourceTimingElement.htm

        public bool scoped;
        public string media;
        public string type;

        public CSSStyleDeclaration this[string selectorText]
        {
            [Script(DefineAsStatic = true)]
            get
            {
                // X:\jsc.svn\examples\javascript\css\CSSPrintMediaExperiment\CSSPrintMediaExperiment\Application.cs

                return this.StyleSheet[selectorText].style;
            }
        }

        #region StyleSheet
        internal IStyleSheet sheet;
        internal IStyleSheet styleSheet;


        public IStyleSheet StyleSheet
        {
            [Script(DefineAsStatic = true)]
            get
            {
                // X:\jsc.svn\core\ScriptCoreLib.Windows.Forms\ScriptCoreLib.Windows.Forms\JavaScript\BCLImplementation\System\Windows\Forms\TreeView\TreeView.cs

                if (this.parentNode == null)
                    this.AttachTo(Native.document.head);

                if (this.parentNode == null)
                    this.AttachTo(Native.document.body);


                if (Expando.InternalIsMember(this, "sheet"))
                    return this.sheet;

                if (Expando.InternalIsMember(this, "styleSheet"))
                    return this.styleSheet;

                throw new System.Exception("fault at IHTMLStyle.StyleSheet, members: " +
                    Expando.InternalGetMemberNames(this));
            }
        }
        #endregion



        #region Constructor

        public IHTMLStyle()
        {
            // InternalConstructor
        }

        static IHTMLStyle InternalConstructor()
        {
            var s = (IHTMLStyle)new IHTMLElement(HTMLElementEnum.style);

            // safari wont init sheet if not child is added
            // http://www.cs.washington.edu/research/projects/se/www/kde/reuse_patterns/source_code/kdelibs.src/classes/kdelibs'HTMLStyleElementImpl.html
            try { s.appendChild("/**/"); }
            catch { };

            s.type = "text/css";

            return s;
        }

        #endregion

    }
}
