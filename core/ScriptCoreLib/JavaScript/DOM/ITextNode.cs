using ScriptCoreLib.JavaScript;

using ScriptCoreLib.JavaScript.DOM.HTML;

namespace ScriptCoreLib.JavaScript.DOM
{
	[Script(InternalConstructor = true)]
	public class ITextNode : INode
	{
		public ITextNode()
		{
		}

		public ITextNode(string e)
		{

		}

		public ITextNode(IDocument doc, string e)
		{

		}

		internal static ITextNode InternalConstructor()
		{
			return InternalConstructor("");
		}

		internal static ITextNode InternalConstructor(string e)
		{
			return InternalConstructor(Native.Document, e);
		}

		internal static ITextNode InternalConstructor(IDocument doc, string e)
		{
			return doc.createTextNode(e);
		}
	}
}
