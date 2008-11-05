using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Media.Effects;
using System.Windows.Media;
using System.Windows.Input;
using ScriptCoreLib.JavaScript.DOM.HTML;
using ScriptCoreLib.JavaScript.BCLImplementation.System.Windows.Media;
using ScriptCoreLib.JavaScript.BCLImplementation.System.Windows.Media.Animation;
using ScriptCoreLib.JavaScript.BCLImplementation.System.Windows.Input;
using ScriptCoreLib.Shared.Avalon.Extensions;

namespace ScriptCoreLib.JavaScript.BCLImplementation.System.Windows
{

	[Script(Implements = typeof(global::System.Windows.UIElement))]
	internal class __UIElement : __Visual, __IAnimatable, __IInputElement
	{
		#region __IInputElement Members

		public IHTMLElement InternalGetDisplayObjectDirect()
		{
			return InternalGetDisplayObject();
		}

		#endregion

		public virtual IHTMLElement InternalGetDisplayObject()
		{
			throw new NotImplementedException();
		}



		public static implicit operator __UIElement(UIElement e)
		{
			return (__UIElement)(object)e;
		}

		public virtual IHTMLElement InternalGetOpacityTarget()
		{
			return InternalGetDisplayObject();
		}

		double InternalOpacity;

		public double Opacity
		{
			get
			{
				// fixme: we cannot get the opacity value from DOM at the moment
				return InternalOpacity;
			}
			set
			{
				InternalOpacity = value;
				InternalGetOpacityTarget().style.Opacity = value;
			}
		}



		public event MouseEventHandler MouseMove
		{
			add
			{

				InternalGetDisplayObject().onmousemove +=
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

				InternalGetDisplayObject().onmouseover +=
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

				InternalGetDisplayObject().onmouseout +=
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

				InternalGetDisplayObject().onmousedown +=
					e =>
					{
						e.PreventDefault();

						if (e.MouseButton == ScriptCoreLib.JavaScript.DOM.IEvent.MouseButtonEnum.Left)
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

				InternalGetDisplayObject().onmouseup +=
					e =>
					{
						e.PreventDefault();

						if (e.MouseButton == ScriptCoreLib.JavaScript.DOM.IEvent.MouseButtonEnum.Left)
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

				InternalGetDisplayObject().onmousewheel +=
					e =>
					{
						//value(this, (__MouseWheelEventArgs)e);
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

				InternalGetDisplayObject().onkeydown +=
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

				InternalGetDisplayObject().onkeyup +=
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
				var s = this.InternalGetDisplayObject().style;

				if (s.display == ScriptCoreLib.JavaScript.DOM.IStyle.DisplayEnum.empty)
					return Visibility.Visible;

				return Visibility.Hidden;
			}
			set
			{
				if (value == Visibility.Visible)
					this.InternalGetDisplayObject().style.display = ScriptCoreLib.JavaScript.DOM.IStyle.DisplayEnum.empty;
				else
					this.InternalGetDisplayObject().style.display = ScriptCoreLib.JavaScript.DOM.IStyle.DisplayEnum.none;

			}
		}

		public event RoutedEventHandler GotFocus
		{
			add
			{

				InternalGetDisplayObject().onfocus +=
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

				InternalGetDisplayObject().onblur +=
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

			k.focus();

			return true;
		}

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


				// http://www.w3schools.com/CSS/pr_pos_clip.asp

				// shape  	Sets the shape of the element. The valid shape value is: rect (top, right, bottom, left)
				// auto 	Default. The browser sets the shape of the element
				var rect = "rect(";

				rect += Convert.ToInt32(r.Y) + "px, ";
				rect += Convert.ToInt32(r.Right) + "px, ";
				rect += Convert.ToInt32(r.Bottom) + "px, ";
				rect += Convert.ToInt32(r.X) + "px";

				rect += ")";

				e.style.clip = rect;
			}
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

		public static implicit operator UIElement(__UIElement e)
		{
			return (UIElement)(object)e;
		}
	}
}
