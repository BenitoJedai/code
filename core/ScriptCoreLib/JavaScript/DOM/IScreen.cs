using ScriptCoreLib.JavaScript;

namespace ScriptCoreLib.JavaScript.DOM
{
    // http://mxr.mozilla.org/mozilla-central/source/dom/webidl/Screen.webidl

    [Script(HasNoPrototype=true)]
    public class IScreen
    {
        public int width;
        public int height;
    }
}
