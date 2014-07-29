using ScriptCoreLib.JavaScript;

namespace ScriptCoreLib.JavaScript.DOM
{
    // http://mxr.mozilla.org/mozilla-central/source/dom/webidl/Screen.webidl
    // http://sharpkit.net/help/SharpKit.Html/SharpKit.Html/Screen/

    [Script(HasNoPrototype=true)]
    public class IScreen
    {
        public int width;
        public int height;
    }
}
