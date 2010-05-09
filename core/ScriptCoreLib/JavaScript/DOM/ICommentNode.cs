using ScriptCoreLib.JavaScript;

using ScriptCoreLib.JavaScript.DOM.HTML;

namespace ScriptCoreLib.JavaScript.DOM
{
	// http://msdn.microsoft.com/en-us/library/ms535229(VS.85).aspx
	[Script(InternalConstructor = true)]
	public class ICommentNode : INode
	{
		// To suffix this type with Node, Element or nothing?

		public ICommentNode()
		{
		}

		public ICommentNode(string e)
		{

		}

		public ICommentNode(IDocument doc)
		{

		}

		public ICommentNode(IDocument doc, string e)
		{

		}

		internal static ICommentNode InternalConstructor()
		{
			return InternalConstructor("");
		}

		internal static ICommentNode InternalConstructor(string e)
		{
			return InternalConstructor(Native.Document, e);
		}

		internal static ICommentNode InternalConstructor(IDocument doc, string e)
		{
			return doc.createComment(e);
		}

		internal static ICommentNode InternalConstructor(IDocument doc)
		{
			if (doc == null)
				doc = Native.Document;

			return doc.createComment("");
		}
	}
}
