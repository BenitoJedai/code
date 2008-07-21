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
    [SWF(width = 640, height = 480)]
    public class RayCaster6 : RayCaster4base
    {
        // http://www.digital-ist-besser.de/
        // http://www.fredheintz.com/sitefred/main.html

        public RayCaster6()
        {

            this.scaleX = 2;
            this.scaleY = 2;
            //s.filters = new[] { new BlurFilter() };

        }

    }
}