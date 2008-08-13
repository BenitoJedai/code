using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ScriptCoreLib.ActionScript.BCLImplementation.System.Windows.Media;
using ScriptCoreLib.ActionScript.BCLImplementation.System.Windows.Media.Animation;
using System.Windows;
using ScriptCoreLib.ActionScript.flash.display;

namespace ScriptCoreLib.ActionScript.BCLImplementation.System.Windows
{

	[Script(Implements = typeof(global::System.Windows.UIElement))]
	internal class __UIElement : __Visual, __IAnimatable
	{
		public virtual DisplayObject InternalGetDisplayObject()
		{
			throw new NotImplementedException();
		}

		public static implicit operator __UIElement(UIElement e)
		{
			return (__UIElement)(object)e;
		}
	}
}
