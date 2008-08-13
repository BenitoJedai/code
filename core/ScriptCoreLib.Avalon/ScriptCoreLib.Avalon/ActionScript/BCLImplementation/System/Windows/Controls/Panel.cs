using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Controls;
using ScriptCoreLib.ActionScript.BCLImplementation.System.Windows.Markup;
using ScriptCoreLib.ActionScript.flash.display;

namespace ScriptCoreLib.ActionScript.BCLImplementation.System.Windows.Controls
{
	[Script(Implements = typeof(global::System.Windows.Controls.Panel))]
	internal class __Panel : __FrameworkElement, __IAddChild
	{
		public readonly Sprite InternalSprite = new Sprite();

		public __Panel()
		{
			_Children = new __UIElementCollection { InternalSprite = InternalSprite };
		}

		UIElementCollection _Children;

		public UIElementCollection Children
		{
			get
			{
				return _Children;
			}
		}

		public static implicit operator __Panel(Panel e)
		{
			return (__Panel)(object)e;
		}
	}
}
