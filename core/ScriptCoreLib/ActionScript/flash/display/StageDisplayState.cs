using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ScriptCoreLib.ActionScript.flash.display
{
    // http://help.adobe.com/en_US/FlashPlatform/reference/actionscript/3/flash/display/StageDisplayState.html
    [Script(IsNative = true)]
    public class StageDisplayState
    {
        // see also: http://dl.dropbox.com/u/7009356/ByteArray.org%20-%20Actionscript%203%20Experiments%20and%20Flash%20Player%20love.pdf
        public static readonly string FULL_SCREEN_INTERACTIVE = "fullScreenInteractive";

        public static readonly string FULL_SCREEN = "fullScreen";
        public static readonly string NORMAL = "normal";
    }
}
