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
        public Stage stage;
        public Rectangle viewPort;


        public void loadString(string htmlString, string contentType)
        {
        }
    }
}
