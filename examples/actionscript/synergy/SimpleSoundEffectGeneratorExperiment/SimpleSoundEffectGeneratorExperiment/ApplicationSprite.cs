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
            //Y:\opensource\googlecode\as3sfxr\ui\TinyButton.as:196
            //Warning: Assignment within conditional.  Did you mean == instead of =?
            //                        if(_selectable && (_selected = !_selected))
            //                                           ^

        }

    }
}

//[Script(IsNative = true)]
//[SWCImport]
//public class SfxrApp : Sprite
//{
//    // for partial sdk builds use /DisableActionScriptNatives:true
//}