using ScriptCoreLib;
using ScriptCoreLib.ActionScript.Extensions;
using ScriptCoreLib.ActionScript.flash.display;
using ScriptCoreLib.Extensions;
using SimpleSoundEffectGeneratorExperiment;

namespace SimpleSoundEffectGeneratorExperiment
{
    public sealed class ApplicationSprite : SfxrApp
    {
        public ApplicationSprite()
        {
        }

    }
}

[Script(IsNative = true)]
[SWCImport]
public class SfxrApp : Sprite
{
    // Carlo: generation was disabled due to a bug. re-enable once fixed. 
    // Arvo: JSC could make use of Roslyn to pick up such code comments :)
}