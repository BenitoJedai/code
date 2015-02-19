using ScriptCoreLib.JavaScript;

namespace ScriptCoreLib.JavaScript.DOM
{
    // http://mxr.mozilla.org/mozilla-central/source/dom/webidl/Screen.webidl
    // http://sharpkit.net/help/SharpKit.Html/SharpKit.Html/Screen/
    // https://src.chromium.org/viewvc/blink/trunk/Source/core/frame/Screen.idl
    // https://github.com/Reactive-Extensions/IL2JS/blob/master/Html/Microsoft/LiveLabs/Html/Screen.cs

    [Script(HasNoPrototype = true)]
    public class IScreen
    {
        // PPAPI to enable virtual desktop for special fullscreen mode?
        // https://nullpwd.wordpress.com/2013/05/25/writing-a-virtual-desktop-system-in-cs-with-winapi/
        // http://stackoverflow.com/questions/472161/moving-applications-between-desktops-in-windows
        // http://vdm.codeplex.com/

        // what if we are running in vr, via ndk over udp/tcp rtc broadcast?

        // X:\jsc.svn\examples\javascript\Test\TestServiceWorkerFetchHTML\TestServiceWorkerFetchHTML\Application.cs
        // X:\jsc.svn\examples\javascript\Test\TestIScreen\TestIScreen\Application.cs

        // X:\jsc.svn\examples\javascript\test\TestServiceWorkerVisualizedScreens\TestServiceWorkerVisualizedScreens\Application.cs
        // X:\jsc.svn\examples\javascript\Test\TestServiceWorkerScreens\TestServiceWorkerScreens\Application.cs


        // X:\jsc.svn\examples\javascript\forms\FormsForSecondaryScreen\FormsForSecondaryScreen\Application.cs
        // we should also have a demo
        // for svg multimouse/drawing
        // "X:\jsc.svn\examples\javascript\android\MultiMouse\MultiMouse.sln"

        // tested by?
        // X:\jsc.svn\examples\javascript\ReportScreenSizeToServer\ReportScreenSizeToServer\Application.cs
        // "X:\jsc.svn\examples\javascript\NewTabContinuation\NewTabContinuation.sln"
        // X:\jsc.svn\examples\javascript\forms\FakeWindowsLoginExperiment\FakeWindowsLoginExperiment.AssetsLibrary.Extensions\ScriptCoreLib\JavaScript\ComponentModel\ApplicationExitFullscreen.cs
        // X:\jsc.svn\examples\javascript\forms\FakeWindowsLoginExperiment\FakeWindowsLoginExperiment\Design\FakeMultimonitorDesktop.cs
        // "X:\jsc.svn\examples\javascript\forms\FormsForSecondaryScreen\FormsForSecondaryScreen.sln"

        // "X:\jsc.svn\examples\javascript\android\JellyworldExperiment\JellyworldExperiment.sln"

        public int width;
        public int height;
    }
}
