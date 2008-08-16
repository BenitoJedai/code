using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Controls;
using System.Windows.Media;
using ScriptCoreLib.JavaScript.DOM.HTML;
using ScriptCoreLib.JavaScript.BCLImplementation.System.Windows.Markup;
using ScriptCoreLib.JavaScript.BCLImplementation.System.Windows.Media;

namespace ScriptCoreLib.JavaScript.BCLImplementation.System.Windows.Controls
{
	[Script(Implements = typeof(global::System.Windows.Controls.Panel))]
	internal class __Panel : __FrameworkElement, __IAddChild
	{
		public readonly IHTMLDiv InternalSprite = new IHTMLDiv();

		public override IHTMLElement InternalGetDisplayObject()
		{
			return InternalSprite;
		}

		public __Panel()
		{
			InternalSprite.style.backgroundColor = "gray";
			InternalSprite.style.width = "600px";
			InternalSprite.style.height = "400px";

			_Children = new __UIElementCollection { InternalSprite = InternalSprite };
		}

		public Brush Background
		{
			get { throw new NotImplementedException(); }
			set
			{
				var AsSolidColorBrush = value as SolidColorBrush;

				if (AsSolidColorBrush != null)
				{
					var _SolidColorBrush = (__SolidColorBrush)AsSolidColorBrush;
					uint _Color = (__Color)_SolidColorBrush.Color;

					this.InternalSprite.style.backgroundColor = "red";
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
