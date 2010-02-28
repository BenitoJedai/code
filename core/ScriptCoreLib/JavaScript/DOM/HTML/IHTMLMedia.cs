using ScriptCoreLib.JavaScript.Runtime;
using ScriptCoreLib.JavaScript.DOM;
using ScriptCoreLib.JavaScript;

using ScriptCoreLib.JavaScript.DOM.HTML;

namespace ScriptCoreLib.JavaScript.DOM.HTML
{
    [Script(InternalConstructor = true)]
    public abstract class IHTMLMedia : IHTMLElement
    {
		// compiler: generate from IDL at http://www.whatwg.org/specs/web-apps/current-work/#htmlmediaelement

		public string src;
		public bool controls;

		public void load()
		{
		}

		public void play()
		{
		}

		public void pause()
		{
		}
    }
}
