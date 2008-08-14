using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ScriptCoreLib.ActionScript.flash.filters;
using System.Windows.Media.Effects;

namespace ScriptCoreLib.ActionScript.BCLImplementation.System.Windows.Media.Effects
{
	[Script(Implements = typeof(global::System.Windows.Media.Effects.BitmapEffect))]
	internal class __BitmapEffect
	{
		public virtual BitmapFilter InternalGetBitmapFilter()
		{
			throw new NotImplementedException();
		}

		public static implicit operator __BitmapEffect(BitmapEffect e)
		{
			return (__BitmapEffect)(object)e;
		}
	}
}
