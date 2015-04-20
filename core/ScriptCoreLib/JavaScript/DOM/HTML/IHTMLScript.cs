using ScriptCoreLib.JavaScript.Runtime;
using ScriptCoreLib.JavaScript.DOM;
using ScriptCoreLib.JavaScript;

using ScriptCoreLib.JavaScript.DOM.HTML;
using System;

namespace ScriptCoreLib.JavaScript.DOM.HTML
{
    // http://mxr.mozilla.org/mozilla-central/source/dom/webidl/HTMLScriptElement.webidl
    // http://src.chromium.org/viewvc/blink/trunk/Source/core/html/HTMLScriptElement.idl

    [Script(InternalConstructor = true)]
    public class IHTMLScript : IHTMLElement
    {
		// http://blog.chromium.org/2015/03/new-javascript-techniques-for-rapid.html
		// https://developers.google.com/speed/docs/insights/BlockingJS

		// https://w3c.github.io/webappsec/specs/subresourceintegrity/

		#region Constructor

		public IHTMLScript()
        {
            // InternalConstructor
        }


        static IHTMLObject InternalConstructor()
        {
            return (IHTMLObject)IHTMLElement.InternalConstructor(HTMLElementEnum.script);
        }

        #endregion

        public string src;
        public string type;

        #region event onload
        public event Action onload
        {
            // http://www.rgagnon.com/javadetails/java-0176.html
            // http://bytes.com/topic/javascript/answers/147231-applet-onload-alert-hi
            // http://www.irt.org/script/4013.htm
            // http://www.blooberry.com/indexdot/html/tagpages/attributes/onload.htm

            [Script(DefineAsStatic = true)]
            add
            {
                // tested by X:\jsc.svn\examples\javascript\Test\TestScriptLoadedEvent\TestScriptLoadedEvent\Application.cs

                //Native.Window.alert("add_onload!");

                // does it work for IE10 and file://
                __onload.CombineDelegate(this, value);
            }
            [Script(DefineAsStatic = true)]
            remove
            {
                throw new NotSupportedException();
            }
        }
        #endregion

        public string readyState;

        [Script]
        internal static class __onload
        {
            // will HTML5 enable a more nicer solution?

            internal static void CombineDelegate(IHTMLScript a, Action value)
            {
                var whenloaded_null = false;
                var whenloaded = true;

                // http://stackoverflow.com/questions/1929742/can-script-readystate-be-trusted-to-detect-the-end-of-dynamic-script-loading
                var done = false;

                Action yield = delegate
                {
                    var readyState = a.readyState;

                    //Console.WriteLine(new { readyState });

                    if (readyState == null)
                        done = whenloaded_null;

                    if (readyState == "loaded")
                        done = whenloaded;


                    if (readyState == "complete")
                        done = whenloaded;

                    if (done)
                    {
                        whenloaded = false;
                        whenloaded_null = false;
                        value();
                    }
                };

                yield();

                // loading from file:// causes IE 10 to load it instantly?
                if (done)
                    return;

                // enable trapping the event
                whenloaded_null = true;

                a.InternalEvent(true,
                    yield,
                    "load",
                    "onreadystatechange"
                );



            }
        }
    }
}
