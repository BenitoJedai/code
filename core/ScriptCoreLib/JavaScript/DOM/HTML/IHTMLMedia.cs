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
		// see: http://www.w3schools.com/html5/tag_audio.asp

		public string src;
		public bool controls;
		public bool autobuffer;

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
