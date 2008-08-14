using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Controls;
using ScriptCoreLib.ActionScript.BCLImplementation.System.Windows.Markup;
using ScriptCoreLib.ActionScript.flash.display;
using System.Windows.Media;
using ScriptCoreLib.ActionScript.BCLImplementation.System.Windows.Media;

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

		public Brush Background
		{
			get { throw new NotImplementedException(); }
			set
			{
				InternalSprite.graphics.clear();

				var AsSolidColorBrush = value as SolidColorBrush;

				if (AsSolidColorBrush != null)
				{
					var _SolidColorBrush = (__SolidColorBrush)AsSolidColorBrush;
					uint _Color = (__Color)_SolidColorBrush.Color;

					InternalSprite.graphics.beginFill(_Color);
					InternalSprite.graphics.drawRect(0, 0, 200, 200);
				}
			}
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
