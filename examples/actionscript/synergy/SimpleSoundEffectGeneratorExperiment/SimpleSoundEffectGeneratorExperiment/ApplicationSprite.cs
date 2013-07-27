using ScriptCoreLib;
using ScriptCoreLib.ActionScript;
using ScriptCoreLib.ActionScript.Extensions;
using ScriptCoreLib.ActionScript.flash.display;
using ScriptCoreLib.Extensions;
using SimpleSoundEffectGeneratorExperiment;

namespace SimpleSoundEffectGeneratorExperiment
{
    [SWF(width = 640, height = 500)]
    public sealed class ApplicationSprite : SfxrApp
    {
        public ApplicationSprite()
        {
        }

    }
}

//[Script(IsNative = true)]
//[SWCImport]
//public class SfxrApp : Sprite
//{
//    // for partial sdk builds use /DisableActionScriptNatives:true
//}