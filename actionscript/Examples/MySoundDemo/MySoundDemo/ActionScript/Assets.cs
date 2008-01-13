using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ScriptCoreLib;
using ScriptCoreLib.ActionScript;
using ScriptCoreLib.ActionScript.mx.core;

//[assembly: ScriptResources(MySoundDemo.ActionScript.Assets.Path)]

namespace MySoundDemo.ActionScript
{
    [Script]
    public static class Assets
    {
        public const string Path = "/assets/MySoundDemo";

        #region world
        [Embed(source = Path + "/world.mp3")]
        static Class _world;

        public static SoundAsset world { get { return (SoundAsset)_world.CreateType(); } }
        #endregion

        #region Preview
        [Embed(source = Path + "/Preview.png")]
        static Class _Preview;

        public static BitmapAsset Preview { get { return (BitmapAsset)_Preview.CreateType(); } }
        #endregion
    }
}
