using ScriptCoreLib.JavaScript.Runtime;
using ScriptCoreLib.JavaScript.DOM;
using ScriptCoreLib.JavaScript;

using ScriptCoreLib.JavaScript.DOM.HTML;

namespace ScriptCoreLib.JavaScript.DOM.HTML
{

    [Script(InternalConstructor = true)]
    public class IHTMLStyle : IHTMLElement
    {
        internal IStyleSheet sheet;

        internal IStyleSheet styleSheet;

        public string media;
        public string type;

        public IStyleSheet StyleSheet
        {
            [Script(DefineAsStatic = true)]
            get
            {
                if (Expando.InternalIsMember(this, "sheet"))
                    return this.sheet;

                if (Expando.InternalIsMember(this, "styleSheet"))
                    return this.styleSheet;

                throw new System.Exception("fault at IHTMLStyle.StyleSheet, members: " + Expando.InternalGetMember(this, "sheet"));
            }
        }


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
