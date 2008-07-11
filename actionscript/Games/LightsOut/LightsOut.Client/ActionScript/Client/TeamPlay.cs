using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ScriptCoreLib;
using ScriptCoreLib.ActionScript;

namespace LightsOut.ActionScript.Client
{
    [Script, ScriptApplicationEntryPoint(Width = LightsOut.ControlWidth + NonobaChatWidth, Height = LightsOut.ControlHeight)]
    [SWF(width = LightsOut.ControlWidth + NonobaChatWidth, height = LightsOut.ControlHeight)]
    public partial class TeamPlay 
    {
     
        public const int NonobaChatWidth = 200;

    }
}
