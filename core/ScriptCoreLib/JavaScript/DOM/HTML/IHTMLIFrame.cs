using ScriptCoreLib.JavaScript.Runtime;
using ScriptCoreLib.JavaScript.DOM;
using ScriptCoreLib.JavaScript;
using ScriptCoreLib.Shared;

using ScriptCoreLib.JavaScript.DOM.HTML;
using System.Threading.Tasks;

namespace ScriptCoreLib.JavaScript.DOM.HTML
{
    // http://mxr.mozilla.org/mozilla-central/source/dom/webidl/HTMLIFrameElement.webidl
    // http://src.chromium.org/viewvc/blink/trunk/Source/core/html/HTMLIFrameElement.idl
    // http://www.chromestatus.com/features/5715536319086592
    // https://github.com/Reactive-Extensions/IL2JS/blob/master/Html/Microsoft/LiveLabs/Html/IFrame.cs
    // http://www.w3.org/TR/html4/present/frames.html#h-16.5

    [Script(InternalConstructor = true)]
    public class IHTMLIFrame : IHTMLElement
    {
        // X:\jsc.svn\core\ScriptCoreLibAndroid\ScriptCoreLibAndroid\android\webkit\WebView.cs

        // http://www.w3.org/TR/2011/WD-html5-20110525/the-iframe-element.html#attr-iframe-sandbox
        public string sandbox;

        #region
        [Script(HasNoPrototype = true)]
        class __IHTMLIFrame
        {
            public string src;
        }

        public string src
        {
            [Script(DefineAsStatic = true)]
            get
            {
                return ((__IHTMLIFrame)(object)this).src;
            }
            [Script(DefineAsStatic = true)]
            set
            {
                // do we need this workaround?
                //contentWindow.document.location.replace(value);

                ((__IHTMLIFrame)(object)this).src = value;
            }
        }
        #endregion


        // http://stackoverflow.com/questions/65034/remove-border-from-iframe
        public string frameBorder;
        public string border;

        public bool allowFullScreen
        {
            [Script(DefineAsStatic = true)]
            set
            {
                this.setAttribute("mozallowFullScreen", "");
                this.setAttribute("webkitAllowFullScreen", "");
                this.setAttribute("allowFullScreen", "");
            }
        }

        public bool allowTransparency;
        public string scrolling;

        #region Constructor

        public IHTMLIFrame()
        {
            // InternalConstructor
        }


        static IHTMLIFrame InternalConstructor()
        {
            return (IHTMLIFrame)new IHTMLElement(IHTMLElement.HTMLElementEnum.iframe);
        }

        #endregion

        // contentDocument ?

        public IWindow contentWindow;



        #region event onload
        public event System.Action<IEvent> onload
        {
            [Script(DefineAsStatic = true)]
            add
            {
                base.InternalEvent(true, value, "load");
            }
            [Script(DefineAsStatic = true)]
            remove
            {
                base.InternalEvent(false, value, "load");
            }
        }
        #endregion

        #region async
        [Script]
        public new class Tasks
        {
            internal IHTMLIFrame that_IHTMLIFrame;

            [System.Obsolete("should jsc expose events as async tasks until C# chooses to allow that?")]
            public Task<IEvent> onload
            {
                [Script(DefineAsStatic = true)]
                get
                {
                    var x = new TaskCompletionSource<IEvent>();

                    // tested by
                    // X:\jsc.svn\examples\javascript\android\TextToSpeechExperiment\TextToSpeechExperiment\Application.cs
                    that_IHTMLIFrame.onload +=
                        e =>
                        {
                            x.SetResult(e);
                        };

                    return x.Task;
                }
            }
        }

        [System.Obsolete("is this the best way to expose events as async?")]
        public new Tasks async
        {
            [Script(DefineAsStatic = true)]
            get
            {
                return new Tasks { that_IHTMLIFrame = this };
            }
        }
        #endregion
    }
}
