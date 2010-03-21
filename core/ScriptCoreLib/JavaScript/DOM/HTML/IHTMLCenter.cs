using ScriptCoreLib.JavaScript.Runtime;
using ScriptCoreLib.JavaScript.DOM;
using ScriptCoreLib.JavaScript;

using ScriptCoreLib.JavaScript.DOM.HTML;

namespace ScriptCoreLib.JavaScript.DOM.HTML
{
	[Script(InternalConstructor = true)]
	public class IHTMLCenter : IHTMLElement
	{


		#region Constructor

		public IHTMLCenter()
		{
			// InternalConstructor
		}

		static IHTMLCenter InternalConstructor()
		{
			return (IHTMLCenter)new IHTMLElement(HTMLElementEnum.center);
		}

		#endregion


	}
}
