using ScriptCoreLib.JavaScript;

namespace ScriptCoreLib.JavaScript.DOM
{
    // http://mxr.mozilla.org/mozilla-central/source/dom/webidl/Screen.webidl
    // http://sharpkit.net/help/SharpKit.Html/SharpKit.Html/Screen/
    // https://src.chromium.org/viewvc/blink/trunk/Source/core/frame/Screen.idl

    [Script(HasNoPrototype = true)]
    public class IScreen
    {
        // X:\jsc.svn\examples\javascript\forms\FormsForSecondaryScreen\FormsForSecondaryScreen\Application.cs
        // we should also have a demo
        // for svg multimouse/drawing

        // tested by?
        // X:\jsc.svn\examples\javascript\test\TestIScreen\TestIScreen\Application.cs
        // X:\jsc.svn\examples\javascript\ReportScreenSizeToServer\ReportScreenSizeToServer\Application.cs
        // "X:\jsc.svn\examples\javascript\NewTabContinuation\NewTabContinuation.sln"
        // X:\jsc.svn\examples\javascript\forms\FakeWindowsLoginExperiment\FakeWindowsLoginExperiment.AssetsLibrary.Extensions\ScriptCoreLib\JavaScript\ComponentModel\ApplicationExitFullscreen.cs
        // X:\jsc.svn\examples\javascript\forms\FakeWindowsLoginExperiment\FakeWindowsLoginExperiment\Design\FakeMultimonitorDesktop.cs
        // "X:\jsc.svn\examples\javascript\forms\FormsForSecondaryScreen\FormsForSecondaryScreen.sln"

        public int width;
        public int height;
    }
}
