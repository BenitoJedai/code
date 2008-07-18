using ScriptCoreLib;
using ScriptCoreLib.ActionScript.flash.display;
using ScriptCoreLib.ActionScript.flash.text;
using System.Collections.Generic;
using ScriptCoreLib.ActionScript;
using ScriptCoreLib.ActionScript.flash.filters;

[Script(IsNative = true)]
public class Main : Sprite
{

}

[Script(IsNative = true)]
public class Main2 : Sprite
{

}

namespace RayCaster4.ActionScript
{
    /// <summary>
    /// Default flash player entrypoint class. See 'tools/build.bat' for adding more entrypoints.
    /// </summary>
    [Script, ScriptApplicationEntryPoint]
    [SWF(width = 800, height = 420)]
    public class RayCaster4 : Sprite
    {
        // http://www.digital-ist-besser.de/
        // http://www.fredheintz.com/sitefred/main.html

        public RayCaster4()
        {
            var s = new Main2();

            s.scaleX = 2;
            s.scaleY = 2;
            //s.filters = new[] { new BlurFilter() };

            addChild(s);
        }

    }
}