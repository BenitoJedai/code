﻿using ScriptCoreLib;
using ScriptCoreLib.Shared;

[assembly: ScriptResources(RetroCanvas.js.Assets.Path)]

namespace RetroCanvas.js
{
    [Script]
    public static class Assets
    {
        public  const string Path = "assets/RetroCanvas";
    }
}