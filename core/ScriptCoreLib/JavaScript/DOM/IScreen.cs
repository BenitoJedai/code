using ScriptCoreLib.JavaScript;

namespace ScriptCoreLib.JavaScript.DOM
{
    // http://mxr.mozilla.org/mozilla-central/source/dom/webidl/Screen.webidl
    // http://sharpkit.net/help/SharpKit.Html/SharpKit.Html/Screen/
    // https://src.chromium.org/viewvc/blink/trunk/Source/core/frame/Screen.idl

    [Script(HasNoPrototype=true)]
    public class IScreen
    {
        public int width;
        public int height;
    }
}
