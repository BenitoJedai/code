using ScriptCoreLib.JavaScript.DOM.HTML;
using ScriptCoreLib.JavaScript;
using ScriptCoreLib.JavaScript.Runtime;



namespace ScriptCoreLib.JavaScript.DOM.HTML
{
    // http://src.chromium.org/viewvc/blink/trunk/Source/core/html/HTMLLinkElement.idl

	[Script(InternalConstructor = true)]
	public class IHTMLLink : IHTMLElement
	{
        
		public string rel;
		public string href;
		public string type;

		#region ctor
		public IHTMLLink()
		{
		}

		public IHTMLLink(string rel, string href, string type)
		{

		}

		static IHTMLLink InternalConstructor()
		{
			return (IHTMLLink)new IHTMLElement(HTMLElementEnum.link);
		}

		static IHTMLLink InternalConstructor(string rel, string href, string type)
		{
			IHTMLLink n = new IHTMLLink();

			n.rel = rel;
			n.href = href;
			n.type = type;

			return n;
		}
		#endregion

        #region StyleSheet
        internal IStyleSheet sheet;
        internal IStyleSheet styleSheet;

        // sheet: CSSStyleSheet
        public IStyleSheet StyleSheet
        {
            [Script(DefineAsStatic = true)]
            get
            {
                if (Expando.InternalIsMember(this, "sheet"))
                    return this.sheet;

                if (Expando.InternalIsMember(this, "styleSheet"))
                    return this.styleSheet;

                throw new System.Exception("fault at IHTMLLink.StyleSheet, members: " +
                    Expando.InternalGetMemberNames(this));
            }
        }
        #endregion
	}
}
