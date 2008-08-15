using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ScriptCoreLib.ActionScript.BCLImplementation.System.Windows
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


	}
}
