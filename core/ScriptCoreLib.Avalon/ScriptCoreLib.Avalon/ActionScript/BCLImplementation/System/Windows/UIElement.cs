﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Effects;
using ScriptCoreLib.ActionScript.BCLImplementation.System.Windows.Input;
using ScriptCoreLib.ActionScript.BCLImplementation.System.Windows.Media;
using ScriptCoreLib.ActionScript.BCLImplementation.System.Windows.Media.Animation;
using ScriptCoreLib.ActionScript.BCLImplementation.System.Windows.Media.Effects;
using ScriptCoreLib.ActionScript.flash.display;
using ScriptCoreLib.ActionScript.flash.events;
using ScriptCoreLib.ActionScript.Extensions;

namespace ScriptCoreLib.ActionScript.BCLImplementation.System.Windows
{

	[Script(Implements = typeof(global::System.Windows.UIElement))]
	internal class __UIElement : __Visual, __IAnimatable, __IInputElement
	{
		public void InternalSetLeft(double e)
		{
			var k = this.InternalGetDisplayObject();
			k.x = e;

			if (this.InternalClipMask != null)
				this.InternalClipMask.MoveTo(k);
		}

		public void InternalSetTop(double e)
		{
			var k = this.InternalGetDisplayObject();
			k.y = e;

			if (this.InternalClipMask != null)
				this.InternalClipMask.MoveTo(k);
		}

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

		public event KeyEventHandler KeyDown
		{
			add
			{

				InternalGetDisplayObject().keyDown +=
					e =>
					{
						__KeyEventArgs.InternalInvoke(value, this, e);
					};
			}
			remove
			{
				throw new NotImplementedException();
			}
		}

		public event KeyEventHandler KeyUp
		{
			add
			{

				InternalGetDisplayObject().keyUp +=
					e =>
					{
						__KeyEventArgs.InternalInvoke(value, this, e);
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

		public bool Focus()
		{
			var k = this.InternalGetDisplayObjectDirect();

			k.stage.focus = k;

			return true;
		}

		Shape InternalClipMask;

		public Geometry Clip
		{
			get
			{
				throw new NotImplementedException();
			}
			set
			{
				var rg = value as RectangleGeometry;

				if (rg == null)
					throw new NotSupportedException();

				var r = rg.Rect;

				var e = this.InternalGetDisplayObject();

				if (InternalClipMask == null)
					InternalClipMask = new Shape().MoveTo(e);

				var c = InternalClipMask;

				c.graphics.clear();
				c.graphics.beginFill(0x00ffffff);
				c.graphics.drawRect(r.X, r.Y, r.Width, r.Height);
				c.graphics.endFill();

				if (e.parent == null)
				{
					var added = default(Action<Event>);

					added = delegate
					{
						e.parent.addChild(c);
						e.mask = c;

						e.added -= added;
					};
					e.added += added;
				}
				else
				{

					e.parent.addChild(c);
					e.mask = c;
				}
			}
		}
	}
}
