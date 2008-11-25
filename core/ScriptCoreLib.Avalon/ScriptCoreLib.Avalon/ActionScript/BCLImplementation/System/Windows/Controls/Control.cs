using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Media;
using System.Windows;

namespace ScriptCoreLib.ActionScript.BCLImplementation.System.Windows.Controls
{
	[Script(Implements = typeof(global::System.Windows.Controls.Control))]
	internal class __Control : __FrameworkElement
	{
		#region Foreground
		public virtual Brush InternalGetForeground()
		{
			throw new NotImplementedException();
		}

		public virtual void InternalSetForeground(Brush value)
		{
			throw new NotImplementedException();
		}

		public Brush Foreground { get { return InternalGetForeground(); } set { InternalSetForeground(value); } }

		#endregion

		#region Background
		public virtual Brush InternalGetBackground()
		{
			throw new NotImplementedException();
		}

		public virtual void InternalSetBackground(Brush value)
		{
			throw new NotImplementedException();
		}

		public Brush Background { get { return InternalGetBackground(); } set { InternalSetBackground(value); } }

		#endregion



		#region BorderThickness
		public virtual Thickness InternalGetBorderThickness()
		{
			throw new NotImplementedException();
		}

		public virtual void InternalSetBorderThickness(Thickness value)
		{
			throw new NotImplementedException();
		}

		public Thickness BorderThickness { get { return InternalGetBorderThickness(); } set { InternalSetBorderThickness(value); } }

		#endregion

		#region FontSize
		public virtual double InternalGetFontSize()
		{
			throw new NotImplementedException();
		}

		public virtual void InternalSetFontSize(double value)
		{
			throw new NotImplementedException();
		}

		public double FontSize { get { return InternalGetFontSize(); } set { InternalSetFontSize(value); } }

		#endregion


		#region FontFamily
		public virtual FontFamily InternalGetFontFamily()
		{
			throw new NotImplementedException();
		}

		public virtual void InternalSetFontFamily(FontFamily value)
		{
			throw new NotImplementedException();
		}

		public FontFamily FontFamily { get { return InternalGetFontFamily(); } set { InternalSetFontFamily(value); } }

		#endregion
	}
}
