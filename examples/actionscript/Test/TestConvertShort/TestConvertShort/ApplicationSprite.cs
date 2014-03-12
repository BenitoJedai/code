using ScriptCoreLib.ActionScript.Extensions;
using ScriptCoreLib.ActionScript.flash.display;
using ScriptCoreLib.Extensions;
using System;

namespace TestConvertShort
{
    public sealed class ApplicationSprite : Sprite
    {
        public ApplicationSprite()
        {
            //script: error JSC1000: ActionScript :
            // BCL needs another method, please define it.
            // Cannot call type without script attribute :
            // System.Int16 for Int16 Parse(System.String) used at
            // ScriptCoreLib.Shared.BCLImplementation.System.__Convert.ToInt16 at offset 0002.

            //script: error JSC1000: ActionScript :
            // BCL needs another method, please define it.
            // Cannot call type without script attribute :
            // System.Byte for Byte Parse(System.String) used at
            // ScriptCoreLib.Shared.BCLImplementation.System.__Convert.ToByte at offset 0002.

            var x = Convert.ToInt16("7");
            var z = Convert.ToString(x);

        }

    }
}
