using ScriptCoreLib.JavaScript.DOM.HTML;
using ScriptCoreLib.JavaScript.Extensions;

namespace UltraTutorial01
{
	public sealed class UltraApplication
	{
		public UltraApplication(IHTMLElement e)
		{
			new IHTMLDiv { innerHTML = "Hello world" }.AttachToDocument();
		}
	}

	public sealed class AlphaWebService
	{

	}
}
