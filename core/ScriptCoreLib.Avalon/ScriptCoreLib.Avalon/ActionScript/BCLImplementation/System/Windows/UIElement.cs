using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ScriptCoreLib.ActionScript.BCLImplementation.System.Windows.Media;
using ScriptCoreLib.ActionScript.BCLImplementation.System.Windows.Media.Animation;
using System.Windows;
using ScriptCoreLib.ActionScript.flash.display;
using System.Windows.Media.Effects;
using ScriptCoreLib.ActionScript.BCLImplementation.System.Windows.Media.Effects;
using System.Windows.Media;
using System.Windows.Input;
using ScriptCoreLib.ActionScript.BCLImplementation.System.Windows.Input;

namespace ScriptCoreLib.ActionScript.BCLImplementation.System.Windows
{

	[Script(Implements = typeof(global::System.Windows.UIElement))]
	internal class __UIElement : __Visual, __IAnimatable, __IInputElement
	{
		#region __IInputElement Members

		public InteractiveObject InternalGetDisplayObjectDirect()
		{
			return InternalGetDisplayObject();
		}

		#endregion

		public virtual InteractiveObject InternalGetDisplayObject()
		{
			throw new NotImplementedException();
		}

		public BitmapEffect BitmapEffect
		{
			get
			{
				throw new NotImplementedException();
			}
			set
			{
				var _BitmapEffect = (__BitmapEffect)value;

				InternalGetDisplayObject().filters = new[] { _BitmapEffect.InternalGetBitmapFilter() };
			}
		}

		public static implicit operator __UIElement(UIElement e)
		{
			return (__UIElement)(object)e;
		}

		public virtual DisplayObject InternalGetOpacityTarget()
		{
			return InternalGetDisplayObject();
		}

		public double Opacity
		{
			get
			{
				return InternalGetOpacityTarget().alpha;
			}
			set
			{
				InternalGetOpacityTarget().alpha = value;
			}
		}

		public Transform RenderTransform
		{
			get
			{
				throw new NotImplementedException();
			}
			set
			{
				var AsScaleTransform = value as ScaleTransform;

				if (AsScaleTransform != null)
				{

					var o = InternalGetDisplayObject();

					o.scaleX = AsScaleTransform.ScaleX;
					o.scaleY = AsScaleTransform.ScaleY;

				}
			}
		}


		public event MouseEventHandler MouseMove
		{
			add
			{

				InternalGetDisplayObject().mouseMove +=
					e =>
					{
						value(this, (__MouseEventArgs)e);
					};
			}
			remove
			{
				throw new NotImplementedException();
			}
		}

		public event MouseEventHandler MouseEnter
		{
			add
			{

				InternalGetDisplayObject().mouseOver +=
					e =>
					{
						value(this, (__MouseEventArgs)e);
					};
			}
			remove
			{
				throw new NotImplementedException();
			}
		}

		public event MouseEventHandler MouseLeave
		{
			add
			{

				InternalGetDisplayObject().mouseOut +=
					e =>
					{
						value(this, (__MouseEventArgs)e);
					};
			}
			remove
			{
				throw new NotImplementedException();
			}
		}

		public event MouseButtonEventHandler MouseLeftButtonDown
		{
			add
			{

				InternalGetDisplayObject().mouseDown +=
					e =>
					{
						value(this, (__MouseButtonEventArgs)e);
					};
			}
			remove
			{
				throw new NotImplementedException();
			}
		}

		public event MouseButtonEventHandler MouseLeftButtonUp
		{
			add
			{

				InternalGetDisplayObject().mouseUp +=
					e =>
					{
						value(this, (__MouseButtonEventArgs)e);
					};
			}
			remove
			{
				throw new NotImplementedException();
			}
		}

		public event MouseWheelEventHandler MouseWheel
		{
			add
			{

				InternalGetDisplayObject().mouseWheel +=
					e =>
					{
						value(this, (__MouseWheelEventArgs)e);
					};
			}
			remove
			{
				throw new NotImplementedException();
			}
		}

		public Visibility Visibility
		{
			get
			{
				throw new NotImplementedException();
			}
			set
			{
				if (value == Visibility.Visible)
					this.InternalGetDisplayObject().visible = true;
				else
					this.InternalGetDisplayObject().visible = false;
			}
		}

		public event RoutedEventHandler GotFocus
		{
			add
			{

				InternalGetDisplayObject().focusIn +=
					e =>
					{
						value(this, null);
					};
			}
			remove
			{
				throw new NotImplementedException();
			}
		}


		public event RoutedEventHandler LostFocus
		{
			add
			{

				InternalGetDisplayObject().focusOut +=
					e =>
					{
						value(this, null);
					};
			}
			remove
			{
				throw new NotImplementedException();
			}
		}
	}
}
