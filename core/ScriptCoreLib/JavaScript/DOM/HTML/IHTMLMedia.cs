using ScriptCoreLib.JavaScript.Runtime;
using ScriptCoreLib.JavaScript.DOM;
using ScriptCoreLib.JavaScript;

using ScriptCoreLib.JavaScript.DOM.HTML;
using System;

namespace ScriptCoreLib.JavaScript.DOM.HTML
{
    [Script(InternalConstructor = true)]
    public abstract class IHTMLMedia : IHTMLElement
    {
		// compiler: generate from IDL at http://www.whatwg.org/specs/web-apps/current-work/#htmlmediaelement
		// see: http://www.w3schools.com/html5/tag_audio.asp
		// see: http://www.position-absolute.com/articles/introduction-to-the-html5-audio-tag-javascript-manipulation/
		// see: https://developer.mozilla.org/En/Using_audio_and_video_in_Firefox
		// see: https://developer.mozilla.org/En/nsIDOMHTMLMediaElement

		public string src;

		public bool ended;
		public bool paused;
		public bool controls;
		public bool muted;
		public bool loop;
		public bool autobuffer;

		public double duration;
		public double currentTime;
		public double volume;

		public void load()
		{
		}

		public void play()
		{
		}

		public void pause()
		{
		}

		// event prefix on or at?
		public event Action<IEvent> onended
		{
			[Script(DefineAsStatic = true)]
			add
			{
				// probably no IE support
				base.InternalEvent(true, value, "ended", "ended");
			}
			[Script(DefineAsStatic = true)]
			remove
			{
				// probably no IE support
				base.InternalEvent(false, value, "ended", "ended");
			}
		}
    }
}
