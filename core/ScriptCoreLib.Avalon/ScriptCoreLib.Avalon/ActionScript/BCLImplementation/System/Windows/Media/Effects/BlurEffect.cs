using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ScriptCoreLib.ActionScript.flash.filters;
using System.Windows.Media.Effects;

namespace ScriptCoreLib.ActionScript.BCLImplementation.System.Windows.Media.Effects
{
    [Script(Implements = typeof(global::System.Windows.Media.Effects.BlurEffect))]
    internal class __BlurEffect : __Effect
    {
        // http://help.adobe.com/en_US/FlashPlatform/reference/actionscript/3/flash/filters/BlurFilter.html
        BlurFilter InternalBitmapFilter = new BlurFilter();


        public double Radius
        {
            get
            {
                return this.InternalBitmapFilter.blurX;
            }
            set
            {

                this.InternalBitmapFilter.blurX = value;
                this.InternalBitmapFilter.blurY = value;
            }
        }
        public override BitmapFilter InternalGetBitmapFilter()
        {
            return InternalBitmapFilter;
        }
    }
}
