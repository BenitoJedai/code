using ScriptCoreLib.JavaScript.DOM.HTML;
using ScriptCoreLib.JavaScript;



namespace ScriptCoreLib.JavaScript.DOM.HTML
{
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

	}
}
