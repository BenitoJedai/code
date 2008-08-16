using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;

namespace ScriptCoreLib.ActionScript.BCLImplementation.System.Windows
{
	[Script(Implements = typeof(global::System.Windows.Thickness))]
	internal class __Thickness : IEquatable<__Thickness>
	{
		public double InternalValue;

		public __Thickness()
			: this(1)
		{
		}

		public __Thickness(double e)
		{
			InternalValue = e;
		}

		#region IEquatable<__Thickness> Members

		public bool Equals(__Thickness other)
		{
			throw new NotImplementedException();
		}

		#endregion


		public static implicit operator __Thickness(Thickness e)
		{
			return (__Thickness)(object)e;
		}
	}
}
