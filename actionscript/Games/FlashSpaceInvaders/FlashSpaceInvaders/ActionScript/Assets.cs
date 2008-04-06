﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ScriptCoreLib;
using ScriptCoreLib.ActionScript;

//[assembly: ScriptResources(FlashSpaceInvaders.ActionScript.Assets.Path)]

namespace FlashSpaceInvaders.ActionScript
{
    [Script]
    internal static class Assets
    {
        public const string Path = "/assets/FlashSpaceInvaders";

        [Embed(source = Path + "/aenemy_1.png")]
        static public readonly Class aenemy_1;

        [Embed(source = Path + "/aenemy_2.png")]
        static public readonly Class aenemy_2;

        [Embed(source = Path + "/benemy_1.png")]
        static public readonly Class benemy_1;

        [Embed(source = Path + "/benemy_2.png")]
        static public readonly Class benemy_2;

    }
}
