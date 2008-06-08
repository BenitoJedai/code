using ScriptCoreLib;
using ScriptCoreLib.ActionScript.flash.display;
using ScriptCoreLib.ActionScript.flash.text;
using ScriptCoreLib.ActionScript.Extensions;
using ScriptCoreLib.ActionScript;
using ScriptCoreLib.ActionScript.flash.ui;
using ScriptCoreLib.ActionScript.flash.utils;
using ScriptCoreLib.ActionScript.flash.filters;


namespace FlashKeyboard.ActionScript
{
    /// <summary>
    /// testing...
    /// </summary>
    [Script, ScriptApplicationEntryPoint]
    public class FlashKeyboard : Sprite
    {
   
        public FlashKeyboard()
        {
            var Images = new
            {
                Default = Assets.img_default.ToBitmapAsset(),
                Alt = Assets.img_alt.ToBitmapAsset(),
                Shift = Assets.img_shift.ToBitmapAsset()
            };

            Images.Default.AttachTo(this);
        }

       
        
    }

}
