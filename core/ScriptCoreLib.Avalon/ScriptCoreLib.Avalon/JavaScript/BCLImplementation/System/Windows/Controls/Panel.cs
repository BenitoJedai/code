using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Controls;
using System.Windows.Media;
using ScriptCoreLib.JavaScript.DOM.HTML;
using ScriptCoreLib.JavaScript.BCLImplementation.System.Windows.Markup;
using ScriptCoreLib.JavaScript.BCLImplementation.System.Windows.Media;
using System.Windows;
using System.Windows.Input;
using System.Windows.Shapes;

namespace ScriptCoreLib.JavaScript.BCLImplementation.System.Windows.Controls
{
	[Script(Implements = typeof(global::System.Windows.Controls.Panel))]
	internal class __Panel : __FrameworkElement, __IAddChild
	{
		public readonly IHTMLDiv InternalSprite = new IHTMLDiv();
		public readonly IHTMLDiv InternalContent = new IHTMLDiv();

		public override IHTMLElement InternalGetDisplayObject()
		{
			return InternalSprite;
		}

		public override IHTMLElement InternalGetOpacityTarget()
		{
			return InternalContent;
		}


		public double InternalWidthValue = 200;
		public double InternalHeightValue = 200;

		public override double InternalGetWidth()
		{
			return InternalWidthValue;
		}

		public override double InternalGetHeight()
		{
			return InternalHeightValue;
		}

		public sealed override void InternalSetHeight(double value)
		{
			var height = Convert.ToInt32(value) + "px";
			InternalHeightValue = value;

			InternalSprite.style.height = height;
			InternalContent.style.height = height;
		}

		public sealed override void InternalSetWidth(double value)
		{
			var width = Convert.ToInt32(value) + "px";
			InternalWidthValue = value;

			InternalSprite.style.width = width;
			InternalContent.style.width = width;
		}

		public __Panel()
		{
			//InternalSprite.style.overflow = ScriptCoreLib.JavaScript.DOM.IStyle.OverflowEnum.hidden;

			InternalSprite.style.width = "600px";
			InternalSprite.style.height = "400px";

			InternalSprite.style.position = ScriptCoreLib.JavaScript.DOM.IStyle.PositionEnum.absolute;

			InternalSprite.appendChild(InternalContent);

			InternalContent.style.position = ScriptCoreLib.JavaScript.DOM.IStyle.PositionEnum.relative;

			_Children = new __UIElementCollection(this) { InternalSprite = InternalContent };
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
					__Color _Color = _SolidColorBrush.Color;

					if (_Color.A == 0)
						this.InternalSprite.style.backgroundColor = "transparent";
					else
						this.InternalSprite.style.backgroundColor = _Color;
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

		#region __IAddChild Members

		public void AddChild(object value)
		{
			var e = value as UIElement;

			if (e == null)
			{
				throw new NotSupportedException("AddChild supports UIElement instead of " + value.GetType().Name);
			}

			this.Children.Add(e);
		}

		#endregion
	}
}
