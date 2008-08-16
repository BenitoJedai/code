using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Input;

namespace ScriptCoreLib.JavaScript.BCLImplementation.System.Windows
{
	[Script(Implements = typeof(global::System.Windows.FrameworkElement))]
	internal class __FrameworkElement : __UIElement
	{
		public virtual void InternalSetWidth(double value)
		{
			throw new NotImplementedException();
		}

		public virtual void InternalSetHeight(double value)
		{
			throw new NotImplementedException();
		}


		public double Width
		{
			get
			{
				throw new NotImplementedException();

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
				throw new NotImplementedException();

			}
			set
			{
				InternalSetHeight(value);
			}
		}


		public virtual void InternalSetCursor(Cursor value)
		{
			throw new NotImplementedException();
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
	}
}
