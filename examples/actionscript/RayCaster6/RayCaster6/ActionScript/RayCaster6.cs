using ScriptCoreLib;
using ScriptCoreLib.ActionScript.flash.display;
using ScriptCoreLib.ActionScript.flash.text;
using System.Collections.Generic;
using ScriptCoreLib.ActionScript;
using ScriptCoreLib.ActionScript.flash.filters;
using System;
using ScriptCoreLib.ActionScript.flash.events;
using ScriptCoreLib.ActionScript.flash.net;
using ScriptCoreLib.ActionScript.flash.ui;




namespace RayCaster6.ActionScript
{


    /// <summary>
    /// Default flash player entrypoint class. See 'tools/build.bat' for adding more entrypoints.
    /// </summary>
    [Script, ScriptApplicationEntryPoint]
    [SWF(width = DefaultWidth * DefaultScale, height = DefaultHeight * DefaultScale, frameRate = 60)]
    public class RayCaster6 : RayCaster4base
    {
        // http://www.digital-ist-besser.de/
        // http://www.fredheintz.com/sitefred/main.html

        // 120x90
        // 160x120
        const int DefaultWidth = DefaultHeight * 3 / 2;
        const int DefaultHeight = 160;

        const int DefaultScale = 3;

        public RayCaster6()
        {
            w = DefaultWidth;
            h = DefaultHeight;

            this.scaleX = DefaultScale;
            this.scaleY = DefaultScale;
            //this.filters = new[] { new BlurFilter() };

        }

    }
}