using System;
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
using ScriptCoreLib.Shared.Avalon.Extensions;

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

						// we could track the mouseup - yet we wont

						//if (InternalMouseLeftButtonUp_Out == null)
						//    return;

						//var g = InternalGetDisplayObject().stage;

						//if (g == null)
						//    return;

						//var mouseUp = default(Action<MouseEvent>);
						//var mouseUp_Previous = this.InternalMouseLeftButtonUp_In;


						//mouseUp =
						//    mouseUp_e =>
						//    {
						//        g.mouseUp -= mouseUp;
						//        InternalGetDisplayObject().mouseUp += mouseUp_Previous;

						//        this.InternalMouseLeftButtonUp_Out(this, (__MouseButtonEventArgs)mouseUp_e);

						//    };

						//InternalGetDisplayObject().mouseUp -= mouseUp_Previous;
						//g.mouseUp += mouseUp;
					};
			}
			remove
			{
				throw new NotImplementedException();
			}
		}

		#region MouseLeftButtonUp
		Action<MouseEvent> InternalMouseLeftButtonUp_In;
		MouseButtonEventHandler InternalMouseLeftButtonUp_Out;

		public event MouseButtonEventHandler MouseLeftButtonUp
		{
			add
			{
				if (InternalMouseLeftButtonUp_Out == null)
				{
					InternalMouseLeftButtonUp_In =
						e =>
						{
							if (InternalMouseLeftButtonUp_Out != null)
								InternalMouseLeftButtonUp_Out(this, (__MouseButtonEventArgs)e);
						};

					InternalGetDisplayObject().mouseUp += InternalMouseLeftButtonUp_In;
				}

				InternalMouseLeftButtonUp_Out += value;
			}
			remove
			{
				InternalMouseLeftButtonUp_Out -= value;

				if (InternalMouseLeftButtonUp_Out == null)
				{
					InternalGetDisplayObject().mouseUp -= InternalMouseLeftButtonUp_In;
				}
			}
		}
		#endregion


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
				if (this.InternalGetDisplayObject().visible)
					return Visibility.Visible;
				else
					return Visibility.Hidden;
			}
			set
			{
				if (value == Visibility.Visible)
					this.InternalGetDisplayObject().visible = true;
				else
					this.InternalGetDisplayObject().visible = false;
			}
		}

		bool InternalGotFocusSilent;
		public event RoutedEventHandler GotFocus
		{
			add
			{

				InternalGetDisplayObject().focusIn +=
					e =>
					{
						if (InternalGotFocusSilent)
							return;

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
				var target = InternalGetDisplayObject();

				target.focusOut +=
					e =>
					{
						//var s = target.stage;
						//var current = s.focus;

						//if (current == null)
						//    if (!s.isFocusInaccessible())
						//    {
						//        InternalGotFocusSilent = true;
						//        s.focus = current;
						//        InternalGotFocusSilent = false;
						//        return;
						//    }

						//throw new Exception("current: " + current);

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
			k.focusRect = false;

			if (k.stage == null)
			{
				k.InvokeWhenStageIsReady(
					() =>
					{
						//k.stage.stageFocusRect =false;
						k.stage.focus = k;
					}
				);
			}
			else
			{
				//k.stage.stageFocusRect = false;
				k.stage.focus = k;
			}

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
						if (e.parent == null)
							return;

						e.parent.addChild(c);
						e.mask = c;

						e.addedToStage -= added;
					};

					e.addedToStage += added;
				}
				else
				{

					e.parent.addChild(c);
					e.mask = c;
				}
			}
		}

		// fixme: jsc:actionscript has some issues with overridden virtual methods
		// it looks like we are using exsessive base keyword

		internal double VirtualGetWidth()
		{
			return InternalGetWidth();
		}

		internal double VirtualGetHeight()
		{
			return InternalGetHeight();
		}


		public virtual double InternalGetWidth()
		{
			throw new NotImplementedException();
		}

		public virtual double InternalGetHeight()
		{
			throw new NotImplementedException();
		}

		bool InternalClipToBounds;

		public bool ClipToBounds
		{
			get
			{
				return InternalClipToBounds;
			}
			set
			{
				InternalClipToBounds = value;

				// fixme: value = false

				if (value)
					((UIElement)this).ClipTo(0, 0, Convert.ToInt32(InternalGetWidth()), Convert.ToInt32(InternalGetHeight()));


			}
		}

		public bool Focusable
		{
			get
			{
				throw new NotImplementedException();
			}
			set
			{
				var x = this.InternalGetDisplayObject();

				x.tabEnabled = true;

			}
		}

		public static implicit operator UIElement(__UIElement e)
		{
			return (UIElement)(object)e;
		}
	}
}
