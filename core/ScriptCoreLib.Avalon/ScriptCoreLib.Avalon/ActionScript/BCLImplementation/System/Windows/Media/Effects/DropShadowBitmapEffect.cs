using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ScriptCoreLib.ActionScript.flash.filters;

namespace ScriptCoreLib.ActionScript.BCLImplementation.System.Windows.Media.Effects
{
	[Script(Implements = typeof(global::System.Windows.Media.Effects.DropShadowBitmapEffect))]
	internal class __DropShadowBitmapEffect : __BitmapEffect
	{
		DropShadowFilter InternalBitmapFilter = new DropShadowFilter();

		public override BitmapFilter InternalGetBitmapFilter()
		{
			return InternalBitmapFilter;
		}
	}
}
