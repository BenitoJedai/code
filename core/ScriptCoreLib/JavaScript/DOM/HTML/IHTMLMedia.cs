using ScriptCoreLib.JavaScript.Runtime;
using ScriptCoreLib.JavaScript.DOM;
using ScriptCoreLib.JavaScript;

using ScriptCoreLib.JavaScript.DOM.HTML;
using System;
using System.Threading.Tasks;

namespace ScriptCoreLib.JavaScript.DOM.HTML
{
    // http://mxr.mozilla.org/mozilla-central/source/dom/interfaces/html/nsIDOMHTMLMediaElement.idl
    // http://src.chromium.org/viewvc/blink/trunk/Source/core/html/HTMLMediaElement.idl
    // http://src.chromium.org/viewvc/blink/trunk/Source/modules/encryptedmedia/HTMLMediaElementEncryptedMedia.idl

    [Script(InternalConstructor = true)]
    public abstract class IHTMLMedia : IHTMLElement
    {
        // X:\jsc.svn\examples\javascript\WebGL\WebGLTiltShift\WebGLTiltShift\Application.cs


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

        // IE 9 will fault on this
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

        // X:\jsc.svn\examples\javascript\android\com.abstractatech.gamification.gir\com.abstractatech.gamification.gir\Application.cs
        // could jsc automatically expose DOM events as async until roslyn implements it for C# ?
        // who else has special async block?
        // X:\jsc.svn\core\ScriptCoreLib\JavaScript\DOM\HTML\IHTMLButton.cs

        #region async
        [Script]
        // when will C# allow us to referece TUpperType ?
        public new class Tasks : IHTMLElement.Tasks<IHTMLMedia>
        {

            [System.Obsolete("should jsc expose events as async tasks until C# chooses to allow that?")]
            public Task<IEvent> onended
            {
                [Script(DefineAsStatic = true)]
                get
                {
                    var i = that;
                    var y = new TaskCompletionSource<IEvent>();

                    i.onended +=
                        e =>
                        {
                            y.SetResult(e);
                        };

                    return y.Task;
                }
            }
        }

        [System.Obsolete("is this the best way to expose events as async?")]
        public new Tasks async
        {
            [Script(DefineAsStatic = true)]
            get
            {
                return new Tasks { that = this };
            }
        }
        #endregion
    }
}
