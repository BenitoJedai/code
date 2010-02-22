using ScriptCoreLib.JavaScript.Runtime;
using ScriptCoreLib.JavaScript.DOM;
using ScriptCoreLib.JavaScript;

using ScriptCoreLib.JavaScript.DOM.HTML;

namespace ScriptCoreLib.JavaScript.DOM.HTML
{
    [Script(InternalConstructor = true)]
    public class IHTMLAudio : IHTMLMedia
    {
		// see: http://www.whatwg.org/specs/web-apps/current-work/#audio
		// see: http://www.happyworm.com/jquery/jplayer/HTML5.Audio.Support/

        #region Constructor

		public IHTMLAudio()
        {
            // InternalConstructor
        }

		static IHTMLAudio InternalConstructor()
        {
			return (IHTMLAudio)IHTMLElement.InternalConstructor(HTMLElementEnum.audio);
        }

        #endregion


    }
}
