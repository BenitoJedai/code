using ScriptCoreLib.ActionScript.flash.display;
using ScriptCoreLib.ActionScript.flash.geom;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ScriptCoreLib.ActionScript.flash.media
{
    // http://help.adobe.com/en_US/FlashPlatform/reference/actionscript/3/flash/media/StageWebView.html
    //[AIR]
    [Script(IsNative = true)]
    public class StageWebView
    {
        // "X:\jsc.svn\examples\actionscript\air\AIRStageWebViewExperiment\AIRStageWebViewExperiment.sln"

        // http://www.flashandmath.com/mobile/swv/

        // did we have a test for it?
        // is this webview as good as the chrome?



        // In Android 3.0+, an application must enable hardware acceleration in the Android manifestAdditions 
        // element of the AIR application descriptor to display plug-in content in a StageWebView object.

        public Stage stage;
        public Rectangle viewPort;


        public void loadString(string htmlString, string contentType)
        {
        }
    }
}
