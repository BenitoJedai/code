using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Input;
using System.Windows;

namespace ScriptCoreLib.ActionScript.BCLImplementation.System.Windows
{
	[Script(Implements = typeof(global::System.Windows.FrameworkElement))]
	internal class __FrameworkElement : __UIElement
	{

		public string Name
		{
			set
			{
				this.InternalGetDisplayObjectDirect().name = value;
			}
		}

		public virtual void InternalSetWidth(double value)
		{
			throw new NotImplementedException();
		}

		public virtual void InternalSetHeight(double value)
		{
			throw new NotImplementedException();
		}


		public virtual double InternalGetWidth()
		{
			throw new NotImplementedException();
		}

		public virtual double InternalGetHeight()
		{
			throw new NotImplementedException();
		}


		public double Width
		{
			get
			{
				return InternalGetWidth();

			}
			set
			{
				InternalSetWidth(value);
			}
		}

		public double Height
		{
			get
			{
				return InternalGetHeight();

			}
			set
			{
				InternalSetHeight(value);
			}
		}


		public Cursor InternalCursorValue;

		public void InternalSetCursor(Cursor value)
		{
			if (InternalCursorValue == null)
			{
				this.InternalGetDisplayObjectDirect().mouseOver +=
					delegate
					{
						if (InternalCursorValue == Cursors.None)
							global::ScriptCoreLib.ActionScript.flash.ui.Mouse.hide();
					};

				this.InternalGetDisplayObjectDirect().mouseOut +=
					delegate
					{
						if (InternalCursorValue == Cursors.None) 
							global::ScriptCoreLib.ActionScript.flash.ui.Mouse.show();
					};
			}

			
			InternalCursorValue = value;

			if (InternalCursorValue == Cursors.Hand)
			{
				var Sprite = this.InternalGetDisplayObjectDirect() as global::ScriptCoreLib.ActionScript.flash.display.Sprite;

				if (Sprite != null)
				{
					Sprite.buttonMode = true;
					Sprite.useHandCursor = true;
				}

			
			}

		}

		public Cursor Cursor
		{
			get
			{
				throw new NotImplementedException();
			}
			set
			{
				InternalSetCursor(value);
			}
		}

		public DependencyObject InternalParent;

		public DependencyObject Parent { get { return this.InternalParent; } }

		public static implicit operator global::System.Windows.FrameworkElement(__FrameworkElement e)
		{
			return (global::System.Windows.FrameworkElement)(object)e;
		}
	}
}
