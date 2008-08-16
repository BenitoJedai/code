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

		public double Opacity
		{
			get
			{
				throw new NotImplementedException();
			}
			set
			{
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
						//value(this, (__MouseEventArgs) e);
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
						//value(this, (__MouseEventArgs)e);
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
						//value(this, (__MouseEventArgs)e);
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
						//value(this, (__MouseButtonEventArgs)e);
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

						//value(this, (__MouseButtonEventArgs)e);
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
	}
}
