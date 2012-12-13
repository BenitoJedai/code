using ScriptCoreLib;
using ScriptCoreLib.ActionScript.flash.display;
using ScriptCoreLib.ActionScript.flash.text;
using ScriptCoreLib.ActionScript.flash.events;
using ScriptCoreLib.ActionScript.Extensions;
using ScriptCoreLib.ActionScript.flash.media;
using System;
using System.Linq;
using ScriptCoreLib.ActionScript.flash.filters;
using ScriptCoreLib.ActionScript.mx.core;
using ScriptCoreLib.ActionScript.flash.utils;
using ScriptCoreLib.Shared.Lambda;
using System.Collections.Generic;
using ScriptCoreLib.ActionScript.flash.ui;
using ScriptCoreLib.ActionScript;
using ScriptCoreLib.ActionScript.flash.geom;
using FlashTowerDefense.ActionScript.Actors;
using FlashTowerDefense.ActionScript.Assets;


namespace FlashTowerDefense.ActionScript
{

    /// <summary>
    /// testing...
    /// </summary>
    [Script, ScriptApplicationEntryPoint(Width = DefaultWidth, Height = DefaultHeight)]
    [SWF(width = DefaultWidth, height = DefaultHeight, backgroundColor = ColorWhite)]
    public partial class FlashTowerDefense : FlashTowerDefenseSized
    {
        // 850 is max
        // for splitscreen
        //public const int DefaultWidth = 420;

        public const int DefaultWidth = 560;

        public const int DefaultHeight = 480;

        public FlashTowerDefense() : base(DefaultWidth, DefaultHeight)
        {

        }


    }












}
